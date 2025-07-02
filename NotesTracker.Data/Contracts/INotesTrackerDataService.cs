// *********************************************************************************
//	<copyright file="INotesTrackerDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Data Service Interface.</summary>
// *********************************************************************************


namespace NotesTracker.Data.Contracts
{
	using NotesTracker.Data.Entities;

	/// <summary>
	/// The Notes Tracker Data Service Interface.
	/// </summary>
	public interface INotesTrackerDataService
	{
		/// <summary>
		/// Adds the new bug report data asynchronously.
		/// </summary>
		/// <param name="bugReportData">The bug report data.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> AddNewBugReportDataAsync(BugReport bugReportData);
	}
}
