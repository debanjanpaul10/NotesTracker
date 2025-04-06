// *********************************************************************************
//	<copyright file="UpdateNoteDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Update Note DTO.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
	/// <summary>
	/// The Update Note DTO.
	/// </summary>
	public class UpdateNoteDTO
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
	}
}
