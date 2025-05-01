// *********************************************************************************
//	<copyright file="NoteDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Note Created by User DTO.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
	/// <summary>
	/// The Note Created by User DTO.
	/// </summary>
	public class NoteDTO
	{
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
		/// Gets or sets the user name.
		/// </summary>
		/// <value>
		/// The user id.
		/// </value>
		public string UserName { get; set; } = string.Empty;
	}
}
