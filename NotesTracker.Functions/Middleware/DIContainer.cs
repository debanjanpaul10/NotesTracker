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
    using NotesTracker.Business.Contracts;
    using NotesTracker.Business.Services;
    using NotesTracker.Data;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Services;
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
            var sqlConnectionString = builder.Configuration[SqlConnectionStringConstant];
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer(connectionString: sqlConnectionString);
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

            // Data Manager Dependencies
            services.AddScoped<IUsersDataService, UsersDataService>();
        }
    }


}

