﻿// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

namespace NotesTracker.API.Middleware
{
	using Microsoft.EntityFrameworkCore;
	using NotesTracker.Business.Contracts;
	using NotesTracker.Business.Services;
	using NotesTracker.Data;
	using NotesTracker.Data.Contracts;
	using NotesTracker.Data.Services;
	using NotesTracker.Shared.Helpers;
	using MongoDB.Driver;
	using static NotesTracker.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// The Dependency Injection Container Class.
	/// </summary>
	public static class DIContainer
	{
		/// <summary>
		/// Configures the sql database dependencies.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public static void ConfigureAzureSqlServer(this WebApplicationBuilder builder)
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
		/// Configures the mongo database server.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public static void ConfigureMongoDbServer(this WebApplicationBuilder builder)
		{
			var mongoDbConnectionString = builder.Configuration[MongoDbConnectionStringConstant];
			if (!string.IsNullOrEmpty(mongoDbConnectionString))
			{
				var mongoConnectionString = builder.Configuration[MongoDbConnectionStringConstant];
				if (!string.IsNullOrEmpty(mongoConnectionString))
				{
					var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
					settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
					builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));
				}
			}
		}

		/// <summary>
		/// Configures the business dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureBusinessDependencies(this IServiceCollection services)
		{
			services.AddScoped<IHttpClientHelper, HttpClientHelper>();
			services.AddScoped<INotesService, NotesService>();
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<INotesTrackerService, NotesTrackerService>();
			services.AddScoped<ICacheService, CacheService>();
		}

		/// <summary>
		/// Configures the data dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureDataDependencies(this IServiceCollection services)
		{
			services.AddScoped<INotesDataService, NotesDataService>();
			services.AddScoped<IUsersDataService, UsersDataService>();
			services.AddScoped<INotesTrackerDataService, NotesTrackerDataService>();
		}
	}
}
