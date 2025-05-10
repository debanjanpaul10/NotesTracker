// *********************************************************************************
//	<copyright file="Note.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Note Entity Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Entities
{
	/// <summary>
	/// The Note Entity Class.
	/// </summary>
	public class Note
	{
		/// <summary>
		/// Gets or sets the note identifier.
		/// </summary>
		/// <value>
		/// The note identifier.
		/// </value>
		public int NoteId { get; set; }

		/// <summary>
		/// Gets or sets the note title.
		/// </summary>
		/// <value>
		/// The note title.
		/// </value>
		public string NoteTitle { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the note description.
		/// </summary>
		/// <value>
		/// The note description.
		/// </value>
		public string NoteDescription { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the created date.
		/// </summary>
		/// <value>
		/// The created date.
		/// </value>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the last modified date.
		/// </summary>
		/// <value>
		/// The last modified date.
		/// </value>
		public DateTime LastModifiedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the owner user name.
		/// </summary>
		/// <value>
		/// The owner user name.
		/// </value>
		public string UserName { get; set; } = string.Empty;
	}
}
