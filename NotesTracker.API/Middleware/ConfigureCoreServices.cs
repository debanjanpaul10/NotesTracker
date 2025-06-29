// *********************************************************************************
//	<copyright file="ConfigureCoreServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configures the core services for API.</summary>
// *********************************************************************************

namespace NotesTracker.API.Middleware
{
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Azure.Identity;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.Extensions.Configuration.AzureAppConfiguration;
	using NotesTracker.API.Controllers;
	using NotesTracker.Shared.Constants;
	using static NotesTracker.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// Configure core services.
	/// </summary>
	public static class ConfigureCoreServices
	{
		/// <summary>
		/// Configures azure app configuration.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="credentials">The credentials.</param>
		/// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
		public static void ConfigureAzureAppConfiguration(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
		{
			var appConfigurationEndpoint = builder.Configuration[AppConfigurationEndpointKeyConstant];
			if (string.IsNullOrEmpty(appConfigurationEndpoint))
			{
				throw new InvalidOperationException(ExceptionConstants.MissingConfigurationMessage);
			}

			builder.Configuration.AddAzureAppConfiguration(options =>
			{
				options.Connect(new Uri(appConfigurationEndpoint), credentials)
					.Select(KeyFilter.Any).Select(KeyFilter.Any, BaseConfigurationAppConfigKeyConstant)
					.Select(KeyFilter.Any, NotesAPIAppConfigKeyConstant)
					.ConfigureKeyVault(configure =>
					{
						configure.SetCredential(credentials);
					});
			});
		}

		/// <summary>
		/// Configures api services.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public static void ConfigureApiServices(this WebApplicationBuilder builder)
		{
			builder.ConfigureAzureSqlServer();
			builder.ConfigureMongoDbServer();
			builder.ConfigureAuthenticationServices();
			builder.ConfigureHttpClientFactory();
			builder.Services.AddMemoryCache();
			builder.Services.ConfigureBusinessDependencies();
			builder.Services.ConfigureDataDependencies();
		}

		#region PRIVATE Methods

		/// <summary>
		/// Configures http client factory.
		/// </summary>
		/// <param name="builder">The builder.</param>
		private static void ConfigureHttpClientFactory(this WebApplicationBuilder builder)
		{
			builder.Services.AddHttpClient(Auth0TokenClientConstant, tokenClient =>
			{
				var tokenApiBaseAddress = builder.Configuration[Auth0TokenUrl];
				if (!string.IsNullOrEmpty(tokenApiBaseAddress))
				{
					tokenClient.BaseAddress = new Uri(tokenApiBaseAddress);
				}
			});
			builder.Services.AddHttpClient(Auth0ManagementHttpClientConstant, managementClient =>
			{
				var managementApiBaseAddress = builder.Configuration[Auth0ManagementApiAudienceConstant];
				if (!string.IsNullOrEmpty(managementApiBaseAddress))
				{
					managementClient.BaseAddress = new Uri(managementApiBaseAddress);
				}
			});
		}

		/// <summary>
		/// Configures authentication services.
		/// </summary>
		/// <param name="builder">The builder.</param>
		private static void ConfigureAuthenticationServices(this WebApplicationBuilder builder)
		{
			var configuration = builder.Configuration;
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateLifetime = true,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidAudience = configuration[Auth0ClientIdConstant],
						ValidIssuer = $"https://{configuration[DomainConstant]}/",
						RequireExpirationTime = true,
						SignatureValidator = (token, _) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
					};
					options.Events = new JwtBearerEvents
					{
						OnTokenValidated = HandleAuthTokenValidationSuccessAsync,
						OnAuthenticationFailed = HandleAuthTokenValidationFailedAsync
					};
				});
		}

		/// <summary>
		/// Handles auth token validation success async.
		/// </summary>
		/// <param name="context">The token validation context.</param>
		private static async Task HandleAuthTokenValidationSuccessAsync(this TokenValidatedContext context)
		{
			var claimsPrincipal = context.Principal;
			if (claimsPrincipal?.Identity is not ClaimsIdentity claimsIdentity || !claimsIdentity.IsAuthenticated)
			{
				context.Fail(ExceptionConstants.InvalidTokenExceptionConstant);
				return;
			}

			context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
			await Task.CompletedTask;
		}

		/// <summary>
		/// Handles auth token validation failed async.
		/// </summary>
		/// <param name="context">The auth failed context.</param>
		private static async Task HandleAuthTokenValidationFailedAsync(this AuthenticationFailedContext context)
		{
			var authenticationFailedException = new UnauthorizedAccessException(ExceptionConstants.InvalidTokenExceptionConstant);
			var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
			logger.LogError(authenticationFailedException, context.Exception.Message);

			context.Fail(context.Exception.Message);
			await Task.CompletedTask;
		}

		#endregion
	}
}


