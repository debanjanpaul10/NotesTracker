// *********************************************************************************
//	<copyright file="NotesTrackerDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Data Service Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Services
{
	using DnsClient.Internal;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using NotesTracker.Data.Contracts;
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.Constants;
	using System.Threading.Tasks;

	/// <summary>
	/// The Notes Tracker Data Service Class.
	/// </summary>
	/// <param name="context">The sql database context.</param>
	/// <param name="logger">The logger service.</param>
	/// <seealso cref="INotesTrackerDataService"/>
	public class NotesTrackerDataService(SqlDbContext context, ILogger<NotesTrackerDataService> logger) : INotesTrackerDataService
	{
		/// <summary>
		/// The SQL database context.
		/// </summary>
		private readonly SqlDbContext _context = context;

		/// <summary>
		/// The logger service.
		/// </summary>
		private readonly ILogger<NotesTrackerDataService> _logger = logger;

		/// <summary>
		/// Adds the new bug report data asynchronously.
		/// </summary>
		/// <param name="bugReportData">The bug report data.</param>
		/// <returns>The boolean for success/failure</returns>
		public async Task<bool> AddNewBugReportDataAsync(BugReport bugReportData)
		{
			try
			{
				this._logger.LogInformation(string.Format(ExceptionConstants.MethodStartedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(bugReportData)));
				await this._context.BugReports.AddAsync(bugReportData);
				await this._context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(ExceptionConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(bugReportData)));
			}
		}
	}
}
