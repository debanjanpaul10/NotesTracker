// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************

namespace NotesTracker.API
{
	using Microsoft.OpenApi.Models;
	using NotesTracker.API.Middleware;
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
			var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();
                
			builder.Services.ConfigureServices();
			builder.ConfigureApplicationDependencies();
			builder.Services.ConfigureBusinessDependencies();
			builder.Services.ConfigureDataDependencies();

			var app = builder.Build();
			app.ConfigureApplication();
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public static void ConfigureServices(this IServiceCollection services)
		{
			services.AddControllers();
			services.AddOpenApi();
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					p =>
					{
						p.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Notes Tracker API",
					Version = "v1",
					Description = "API documentation for Notes Tracker",
					Contact = new OpenApiContact
					{
						Name = "Debanjan Paul",
						Email = "debanjanpaul10@gmail.com"
					}

				});
			});

			services.AddExceptionHandler<GlobalExceptionHandler>();
			services.AddProblemDetails();
		}

		/// <summary>
		/// Configures the application.
		/// </summary>
		/// <param name="app">The application.</param>
		public static void ConfigureApplication(this WebApplication app)
		{
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notes Tracker API v1");
				c.RoutePrefix = "swaggerui";
			});

			app.UseExceptionHandler();
			app.UseHttpsRedirection();
			app.UseCors();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}

}

