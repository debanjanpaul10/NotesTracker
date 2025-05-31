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
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NotesTracker.Functions.Middleware;
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
            builder.ConfigureDatabaseConnection();
            builder.ConfigureHttpClientFactory();
            builder.Services.ConfigureFunctionDependencies();

            builder.Build().Run();
        }


    }
}



