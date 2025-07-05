// *********************************************************************************
//	<copyright file="INotesTrackerService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Service Interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Contracts
{
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Notes Tracker Service Interface.
	/// </summary>
	public interface INotesTrackerService
	{
		/// <summary>
		/// Gets the about us application data asynchronously.
		/// </summary>
		/// <returns>The list of <see cref="ApplicationInfoDataDTO"/></returns>
		Task<List<ApplicationInfoDataDTO>> GetAboutUsDataAsync();

		/// <summary>
		/// Adds the new bug report data asynchronously.
		/// </summary>
		/// <param name="bugReportData">The bug report data.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> AddNewBugReportDataAsync(BugReportDTO bugReportData, string userName);
	}
}


