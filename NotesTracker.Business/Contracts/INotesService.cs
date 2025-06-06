﻿// *********************************************************************************
//	<copyright file="INotesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Service Interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Contracts
{
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Notes Service Interface.
	/// </summary>
	public interface INotesService
	{
		/// <summary>
		/// Gets the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note id.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>The note entity</returns>
		Task<Note> GetNoteAsync(int noteId, string userName);

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
		/// <param name="userName">The User name.</param>
		/// <returns>The boolean for success/failure</returns>
		Task<bool> DeleteNoteAsync(int noteId, string userName);

		/// <summary>
		/// Updates the note asynchronous.
		/// </summary>
		/// <param name="updatedNote">The updated note.</param>
		/// <returns>The updated note</returns>
		Task<Note> UpdateNoteAsync(UpdateNoteDTO updatedNote);

		/// <summary>
		/// Gets all notes asynchronous.
		/// </summary>
		/// <param name="userName">The user identifier.</param>
		/// <returns>The list of note entity</returns>
		Task<IEnumerable<Note>> GetAllNotesAsync(string userName);
	}
}
