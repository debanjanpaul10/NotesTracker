// *********************************************************************************
//	<copyright file="ExceptionConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Exception Constants Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Constants
{
	/// <summary>
	/// The Exception Constants Class.
	/// </summary>
	public static class ExceptionConstants
	{
		/// <summary>
		/// The notes not found exception
		/// </summary>
		public const string NotesNotFoundException = "No Notes not found";

		/// <summary>
		/// The note not found exception
		/// </summary>
		public const string NoteNotFoundException = "Note not found";

		/// <summary>
		/// The note creation exception
		/// </summary>
		public const string NoteCreationException = "Something went wrong while creating the note!";

		/// <summary>
		/// Creates new notenullexception.
		/// </summary>
		public const string NewNoteNullException = "The new note cannot be null.";
	}
}
