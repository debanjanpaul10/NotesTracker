// *********************************************************************************
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
	using NotesTracker.Shared.Constants;

	/// <summary>
	/// The Dependency Injection Container Class.
	/// </summary>
	public static class DIContainer
	{
		/// <summary>
		/// Configures the application dependencies.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public static void ConfigureApplicationDependencies(this WebApplicationBuilder builder)
		{
			var sqlConnectionString = builder.Configuration[ConfigurationConstants.SqlConnectionStringConstant];
			if (!string.IsNullOrEmpty(sqlConnectionString))
			{
				builder.Services.AddDbContext<SqlDbContext>(options =>
				{
					options.UseSqlServer(connectionString: sqlConnectionString);
				});
			}
		}

		/// <summary>
		/// Configures the business dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureBusinessDependencies(this IServiceCollection services)
		{
			services.AddScoped<INotesService, NotesService>();
		}

		/// <summary>
		/// Configures the data dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureDataDependencies(this IServiceCollection services)
		{
			services.AddScoped<INotesDataService, NotesDataService>();
		}
	}
}
