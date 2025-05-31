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
			HandleUserValidation(newNote.UserName);
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
		/// <param name="userName">The user id.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		/// <exception cref="System.ArgumentNullException">noteId</exception>
		public Task<bool> DeleteNoteAsync(int noteId, string userName)
		{
			HandleUserValidation(userName);
			if (noteId > 0)
			{
				return this._notesDataService.DeleteNoteAsync(noteId, userName);
			}

			throw new ArgumentNullException(nameof(noteId), ExceptionConstants.NoteNotFoundException);
		}

		/// <summary>
		/// Gets all notes asynchronous.
		/// </summary>
		/// <param name="userName">The user id.</param>
		/// <returns>
		/// The list of note entity
		/// </returns>
		public Task<IEnumerable<Note>> GetAllNotesAsync(string userName)
		{
			HandleUserValidation(userName);
			return this._notesDataService.GetAllNotesAsync(userName);
		}

		/// <summary>
		/// Gets the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note id.</param>
		/// <param name="userName">The user id.</param>
		/// <returns>
		/// The note entity
		/// </returns>
		/// <exception cref="System.ArgumentNullException">noteId</exception>
		public Task<Note> GetNoteAsync(int noteId, string userName)
		{
			HandleUserValidation(userName);
			if (noteId > 0)
			{
				return this._notesDataService.GetNoteAsync(noteId, userName);
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
			HandleUserValidation(updatedNote.UserName);
			if (updatedNote is not null)
			{
				return this._notesDataService.UpdateNoteAsync(updatedNote);
			}

			throw new ArgumentNullException(nameof(updatedNote), ExceptionConstants.NoteNotFoundException);
		}

		#region PRIVATE Methods

		/// <summary>
		/// Handles user validation.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <exception cref="ArgumentNullException">ArgumentNullException error.</exception>
		private static void HandleUserValidation(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentNullException(nameof(userName), ExceptionConstants.AttemptedToEnterBlankUserMessageConstant);
			}
		}

		#endregion
	}
}
