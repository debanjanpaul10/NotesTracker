// *********************************************************************************
//	<copyright file="BugReport.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Report Entity Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Entities
{
	using static NotesTracker.Shared.Constants.Enums;

	/// <summary>
	/// The Bug Report Entity Class.
	/// </summary>
	public class BugReport
	{
		/// <summary>
		/// The unique identifier for the bug report.
		/// </summary>
		public int BugId { get; set; }

		/// <summary>
		/// The title of the bug report.
		/// </summary>
		public string BugTitle { get; set; } = string.Empty;

		/// <summary>
		/// The description of the bug report.
		/// </summary>
		public string BugDescription { get; set; } = string.Empty;

		/// <summary>
		/// The severity of the bug report.
		/// </summary>
		public string BugSeverity { get; set; } = string.Empty;

		/// <summary>
		/// The URL of the page where the bug was found.
		/// </summary>
		public string PageUrl { get; set; } = string.Empty;

		/// <summary>
		/// The status of the bug report.
		/// </summary>
		public BugStatus BugStatus { get; set; } = 0; 

		/// <summary>
		/// The bug logged by user name.
		/// </summary>
		public string LoggedByUserName { get; set; } = string.Empty;

	}
}
