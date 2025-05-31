// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

namespace NotesTracker.Functions.Middleware
{
    using Azure.Identity;
    using Microsoft.Azure.Functions.Worker.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureAppConfiguration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Business.Services;
    using NotesTracker.Data;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Services;
    using NotesTracker.Shared.Helpers;
    using static NotesTracker.Shared.Constants.ConfigurationConstants;
    using static NotesTracker.Shared.Constants.ExceptionConstants;

    /// <summary>
    /// The Dependency Injection Container Class.
    /// </summary>
    public static class DIContainer
    {
        /// <summary>
        /// Configures azure app configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
        public static void ConfigureAzureAppConfiguration(this ConfigurationManager configuration, DefaultAzureCredential credentials)
        {
            var appConfigurationEndpoint = configuration[AppConfigurationEndpointKeyConstant];
            if (string.IsNullOrEmpty(appConfigurationEndpoint))
            {
                throw new InvalidOperationException(MissingConfigurationMessage);
            }

            configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(appConfigurationEndpoint), credentials)
                .Select(KeyFilter.Any).Select(KeyFilter.Any, BaseConfigurationAppConfigKeyConstant)
                .Select(KeyFilter.Any, NotesFunctionAppConfigKeyConstant)
                .ConfigureKeyVault(configure =>
                {
                    configure.SetCredential(credentials);
                });
            });
        }

        /// <summary>
        /// Configures the database connection.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureDatabaseConnection(this FunctionsApplicationBuilder builder)
        {
            var sqlConnectionString = builder.Environment.IsDevelopment()
                ? builder.Configuration[LocalSqlConnectionStringConstant]
                : builder.Configuration[SqlConnectionStringConstant];
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer
                    (
                        connectionString: sqlConnectionString,
                        options => options.EnableRetryOnFailure
                        (
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        )
                    );
                });
            }
        }

        /// <summary>
        /// Configures function dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureFunctionDependencies(this IServiceCollection services)
        {
            // Business Manager Dependencies
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IHttpClientHelper, HttpClientHelper>();

            // Data Manager Dependencies
            services.AddScoped<IUsersDataService, UsersDataService>();
        }

        /// <summary>
        /// Configures http client factory.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureHttpClientFactory(this FunctionsApplicationBuilder builder)
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
    }

}

