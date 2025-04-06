// *********************************************************************************
//	<copyright file="NotesTrackerExceptions.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Exceptions Handler Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Helpers
{
	/// <summary>
	/// The Notes Tracker Exceptions Handler Class.
	/// </summary>
	public class NotesTrackerExceptions : Exception
	{
		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string? ExceptionMessage { get; set; }

		/// <summary>
		/// Gets or sets the details.
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public string? Details { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NotesTrackerExceptions"/> class.
		/// </summary>
		/// <param name="statusCode">The status code.</param>
		/// <param name="message">The message.</param>
		/// <param name="details">The details.</param>
		public NotesTrackerExceptions(string? message) : base(message)
		{
			this.ExceptionMessage = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotesTrackerExceptions"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="statusCode">The status code.</param>
		/// <param name="details">The details.</param>
		public NotesTrackerExceptions(string? message, int statusCode, string? details) : base(message)
		{
			this.ExceptionMessage = message;
			this.StatusCode = statusCode;
			this.Details = details;
		}
	}
}
