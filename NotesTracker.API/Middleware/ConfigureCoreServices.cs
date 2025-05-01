// *********************************************************************************
//	<copyright file="ConfigureCoreServices.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configures the core services for API.</summary>
// *********************************************************************************

namespace NotesTracker.API.Middleware
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using NotesTracker.API.Controllers;
    using NotesTracker.Shared.Constants;
    using static NotesTracker.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Configure core services.
    /// </summary>
    public static class ConfigureCoreServices
    {
        /// <summary>
        /// Configures api services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureApiServices(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureServices();
            builder.ConfigureApplicationDependencies();
            builder.Services.ConfigureBusinessDependencies();
            builder.Services.ConfigureDataDependencies();
            builder.ConfigureAuthenticationServices();
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


