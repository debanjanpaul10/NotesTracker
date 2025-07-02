// *********************************************************************************
//	<copyright file="BugReportDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Report Data Transfer Object (DTO) Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
	/// <summary>
	/// The Bug Report Data Transfer Object (DTO) Class.
	/// </summary>
	public class BugReportDTO
	{
		/// <summary>
		/// The Bug Title.
		/// </summary>
		public string BugTitle { get; set; } = string.Empty;

		/// <summary>
		/// The Bug Description.
		/// </summary>
		public string BugDescription { get; set; } = string.Empty;

		/// <summary>
		/// The Bug Severity.
		/// </summary>
		public string BugSeverity { get; set; } = string.Empty;

		/// <summary>
		/// The Page URL.
		/// </summary>
		public string PageUrl { get; set; } = string.Empty;

	}
}
