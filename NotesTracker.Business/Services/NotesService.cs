// *********************************************************************************
//	<copyright file="NotesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Service Interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Services
{
	using NotesTracker.Business.Contracts;
	using NotesTracker.Data.Contracts;
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Notes Service Class.
	/// </summary>
	/// <seealso cref="NotesTracker.Business.Contracts.INotesService" />
	public class NotesService(INotesDataService notesDataService) : INotesService
	{
		/// <summary>
		/// The notes data service
		/// </summary>
		private readonly INotesDataService _notesDataService = notesDataService;

		/// <summary>
		/// Adds the new note asynchronous.
		/// </summary>
		/// <param name="newNote">The new note.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		/// <exception cref="System.ArgumentNullException">newNote</exception>
		public Task<bool> AddNewNoteAsync(NoteDTO newNote)
		{
			if (newNote is not null)
			{
				return this._notesDataService.AddNewNoteAsync(newNote);
			}

			throw new ArgumentNullException(nameof(newNote), ExceptionConstants.NewNoteNullException);
		}

		/// <summary>
		/// Deletes the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		/// <exception cref="System.ArgumentNullException">noteId</exception>
		public Task<bool> DeleteNoteAsync(int noteId)
		{
			if (noteId > 0)
			{
				return this._notesDataService.DeleteNoteAsync(noteId);
			}

			throw new ArgumentNullException(nameof(noteId), ExceptionConstants.NoteNotFoundException);
		}

		/// <summary>
		/// Gets all notes asynchronous.
		/// </summary>
		/// <returns>
		/// The list of note entity
		/// </returns>
		public Task<IEnumerable<Note>> GetAllNotesAsync()
		{
			return this._notesDataService.GetAllNotesAsync();
		}

		/// <summary>
		/// Gets the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note id.</param>
		/// <returns>
		/// The note entity
		/// </returns>
		/// <exception cref="System.ArgumentNullException">noteId</exception>
		public Task<Note> GetNoteAsync(int noteId)
		{
			if (noteId > 0)
			{
				return this._notesDataService.GetNoteAsync(noteId);
			}

			throw new ArgumentNullException(nameof(noteId), ExceptionConstants.NoteNotFoundException);
		}

		/// <summary>
		/// Updates the note asynchronous.
		/// </summary>
		/// <param name="updatedNote">The updated note.</param>
		/// <returns>
		/// The updated note
		/// </returns>
		/// <exception cref="System.ArgumentNullException">updatedNote</exception>
		public Task<Note> UpdateNoteAsync(UpdateNoteDTO updatedNote)
		{
			if (updatedNote is not null)
			{
				return this._notesDataService.UpdateNoteAsync(updatedNote);
			}
			else
			{
				throw new ArgumentNullException(nameof(updatedNote), ExceptionConstants.NoteNotFoundException);
			}
		}
	}
}
