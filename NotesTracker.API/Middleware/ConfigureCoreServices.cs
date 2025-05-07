// *********************************************************************************
//	<copyright file="ConfigureCoreServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configures the core services for API.</summary>
// *********************************************************************************

namespace NotesTracker.API.Middleware
{
    using System.Security.Claims;
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
            builder.ConfigureApplicationDependencies();
            builder.ConfigureAuthenticationServices();
            builder.Services.ConfigureBusinessDependencies();
            builder.Services.ConfigureDataDependencies();
        }

        /// <summary>
        /// Configures authentication services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureAuthenticationServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.Authority = $"https://{configuration[DomainConstant]}/";
                    options.Audience = configuration[AudienceConstant];
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var claimsPrincipal = context.Principal;
                            var claimsIdentity = claimsPrincipal?.Identity as ClaimsIdentity;
                            if (claimsIdentity is null || !claimsIdentity.IsAuthenticated)
                            {
                                context.Fail(ExceptionConstants.InvalidTokenExceptionConstant);
                                return;
                            }

                            // Extracting the 'sub' claim
                            var subClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                            if (string.IsNullOrEmpty(subClaim))
                            {
                                context.Fail(ExceptionConstants.UserIdNotPresentExceptionConstant);
                                return;
                            }

                            context.HttpContext.Items["UserId"] = subClaim.Split('|')[1];
                            context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
                            await Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var authenticationFailedException = new UnauthorizedAccessException(ExceptionConstants.InvalidTokenExceptionConstant);
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
                            logger.LogError(authenticationFailedException, authenticationFailedException.Message);

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}


