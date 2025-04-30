// *********************************************************************************
//	<copyright file="INotesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Data Service Interface.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Contracts
{
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Notes Data Service Interface.
	/// </summary>
	public interface INotesDataService
	{
		/// <summary>
		/// Gets the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note id.</param>
		/// <param name="userId">The user id.</param>
		/// <returns>The note entity</returns>
		Task<Note> GetNoteAsync(int noteId, int userId);

		/// <summary>
		/// Adds the new note asynchronous.
		/// </summary>
		/// <param name="newNote">The new note.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> AddNewNoteAsync(NoteDTO newNote);

		/// <summary>
		/// Deletes the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <param name="userId">The user id.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> DeleteNoteAsync(int noteId, int userId);

		/// <summary>
		/// Updates the note asynchronous.
		/// </summary>
		/// <param name="updatedNote">The updated note.</param>
		/// <returns>The updated note</returns>
		Task<Note> UpdateNoteAsync(UpdateNoteDTO updatedNote);

		/// <summary>
		/// Gets all notes asynchronous.
		/// </summary>
		/// <param name="userId">The user id</param>
		/// <returns>The list of note entity</returns>
		Task<IEnumerable<Note>> GetAllNotesAsync(int userId);
	}
}
