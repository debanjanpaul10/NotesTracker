// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************

using Azure.Identity;
using Microsoft.OpenApi.Models;
using NotesTracker.API.Middleware;
using static NotesTracker.Shared.Constants.ConfigurationConstants;

namespace NotesTracker.API;

/// <summary>
/// The Program Class.
/// </summary>
public static class Program
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	/// <param name="args">The arguments</param>
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureServices();

		var app = builder.Build();
		app.ConfigureApplication();
	}

	/// <summary>
	/// Configures the services for the application.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	internal static void ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

		var credentials = builder.Environment.IsDevelopment()
			? new DefaultAzureCredential()
			: new DefaultAzureCredential(new DefaultAzureCredentialOptions
			{
				ManagedIdentityClientId = builder.Configuration[ManagedIdentityClientIdConstant],
			});

		builder.Services.AddControllers();
		builder.Services.AddOpenApi();
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(
				p =>
				{
					p.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
		});

		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc(ApiVersion, new OpenApiInfo
			{
				Title = NotesTrackerApiName,
				Version = ApiVersion,
				Description = "API documentation for Notes Tracker",
				Contact = new OpenApiContact
				{
					Name = "Debanjan Paul",
					Email = "debanjanpaul10@gmail.com"
				}

			});
		});

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();
		builder.Services.AddHttpContextAccessor();

		builder.ConfigureAzureAppConfiguration(credentials);
		builder.ConfigureApiServices();
	}

	/// <summary>
	/// Configures the application.
	/// </summary>
	/// <param name="app">The web application.</param>
	internal static void ConfigureApplication(this WebApplication app)
	{
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
		}
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint(SwaggerEndpoint, $"{NotesTrackerApiName}.{ApiVersion}");
			c.RoutePrefix = SwaggerUiPrefix;
		});

		app.UseExceptionHandler();
		app.UseHttpsRedirection();
		app.UseCors();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}



