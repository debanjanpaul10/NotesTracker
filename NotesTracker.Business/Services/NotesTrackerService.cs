// *********************************************************************************
//	<copyright file="NotesTrackerService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Service Class.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Services
{
	using AutoMapper;
	using DnsClient.Internal;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using MongoDB.Bson;
	using MongoDB.Driver;
	using NotesTracker.Business.Contracts;
	using NotesTracker.Data.Contracts;
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;
	using System.Collections.Generic;
	using System.Globalization;

	/// <summary>
	/// The Notes Tracker Service Class.
	/// </summary>
	/// <param name="notesTrackerDataService">The notes tracker data service.</param>
	/// <param name="cacheService">The cache service.</param>
	/// <param name="configuration">The configuration.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="mongoClient">The Mongo DB Client.</param>
	/// <param name="mapper">The auto mapper.</param>
	/// <seealso cref="INotesTrackerService"/>
	public class NotesTrackerService(
		INotesTrackerDataService notesTrackerDataService, IMongoClient mongoClient,
		IConfiguration configuration, ILogger<NotesTrackerService> logger, ICacheService cacheService, IMapper mapper) : INotesTrackerService
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
		/// The cache service
		/// </summary>
		private readonly ICacheService _cacheService = cacheService;

		/// <summary>
		/// The notes tracker data service
		/// </summary>
		private readonly INotesTrackerDataService _notesTrackerDataService = notesTrackerDataService;

		/// <summary>
		/// The automapper.
		/// </summary>
		private readonly IMapper _mapper = mapper;

		/// <summary>
		/// Gets the about us application data asynchronously.
		/// </summary>
		/// <param name="userName">The user name of the current logged in user.</param>
		/// <returns>The list of <see cref="ApplicationInfoDataDTO"/></returns>
		public async Task<List<ApplicationInfoDataDTO>> GetAboutUsDataAsync(string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodStartedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, userName));

				var cachedData = this._cacheService.GetCachedData<List<ApplicationInfoDataDTO>>(CacheKeys.AboutUsDataCacheKey);
				if (cachedData is not null)
				{
					return cachedData;
				}
				else
				{
					var applicationInformationData = this._mongoDatabase.GetCollection<ApplicationInfoDataDTO>(NotesTrackerConstants.ApplicationInformationCollectionConstant);
					if (applicationInformationData is not null)
					{
						var applicationInformationResponseData = await applicationInformationData.Find(new BsonDocument()).ToListAsync();
						this._cacheService.SetCacheData(CacheKeys.AboutUsDataCacheKey, applicationInformationResponseData, TimeSpan.FromMinutes(30));
						return applicationInformationResponseData;
					}

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
			finally
			{
				this._logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodEndedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, userName));
			}
		}

		/// <summary>
		/// Adds the new bug report data asynchronously.
		/// </summary>
		/// <param name="bugReportData">The bug report data.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The boolean for success/failure</returns>
		public async Task<bool> AddNewBugReportDataAsync(BugReportDTO bugReportData, string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodStartedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, userName));
				
				var mappedData = this._mapper.Map<BugReport>(bugReportData);
				mappedData.LoggedByUserName = userName;
				mappedData.BugStatus = Enums.BugStatus.Open;
				var result = await this._notesTrackerDataService.AddNewBugReportDataAsync(mappedData);
				
				return result;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, userName));
			}
		}
	}

}

