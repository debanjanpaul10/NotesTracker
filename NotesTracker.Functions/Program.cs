// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************

namespace NotesTracker.Functions
{
    using Azure.Identity;
    using Microsoft.Azure.Functions.Worker.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureAppConfiguration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NotesTracker.Shared.Constants;
    using static NotesTracker.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// The Program Class.
    /// </summary>
    public static class Program
    {
        /// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var builder = FunctionsApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

            var credentials = builder.Environment.IsDevelopment()
                ? new DefaultAzureCredential()
                : new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = builder.Configuration[ManagedIdentityClientIdConstant]
                });

            builder.ConfigureFunctionsWebApplication();
            builder.Configuration.ConfigureAzureAppConfiguration(credentials);
            builder.Services.AddHttpClient();
            
            builder.Build().Run();
        }

        /// <summary>
        /// Configures azure app configuration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="InvalidOperationException">InvalidOperationException error.</exception>
        public static void ConfigureAzureAppConfiguration(this ConfigurationManager configuration, DefaultAzureCredential credentials)
        {
            var appConfigurationEndpoint = configuration[AppConfigurationEndpointKeyConstant];
            if (string.IsNullOrEmpty(appConfigurationEndpoint))
            {
                throw new InvalidOperationException(ExceptionConstants.MissingConfigurationMessage);
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
    }
}



