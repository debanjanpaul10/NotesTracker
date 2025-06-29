// *********************************************************************************
//	<copyright file="NotesTrackerService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Service Class.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Services
{
	using DnsClient.Internal;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using MongoDB.Bson;
	using MongoDB.Driver;
	using NotesTracker.Business.Contracts;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;
	using System.Globalization;

	/// <summary>
	/// The Notes Tracker Service Class.
	/// </summary>
	/// <seealso cref="INotesTrackerService"/>
	public class NotesTrackerService(IMongoClient mongoClient, IConfiguration configuration, ILogger<NotesTrackerService> logger) : INotesTrackerService
	{
		/// <summary>
		/// The mongo database
		/// </summary>
		private readonly IMongoDatabase _mongoDatabase = mongoClient.GetDatabase(configuration[ConfigurationConstants.MongoDatabaseNameConstant]);

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<NotesTrackerService> _logger = logger;

		/// <summary>
		/// Gets the about us application data asynchronously.
		/// </summary>
		/// <returns>The list of <see cref="ApplicationInfoDataDTO"/></returns>
		public async Task<List<ApplicationInfoDataDTO>> GetAboutUsDataAsync()
		{
			try
			{
				var applicationInformationData = this._mongoDatabase.GetCollection<ApplicationInfoDataDTO>(NotesTrackerConstants.ApplicationInformationCollectionConstant);
				if (applicationInformationData is not null)
				{
					var applicationInformationResponseData = await applicationInformationData.Find(new BsonDocument()).ToListAsync();
					return applicationInformationResponseData;
				}
				else
				{
					var ex = new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
					this._logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, ex.Message));
					throw ex;
				}
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
		}
	}

}

