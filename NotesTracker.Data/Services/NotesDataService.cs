// *********************************************************************************
//	<copyright file="NotesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Data Service Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Services
{
	using Microsoft.EntityFrameworkCore;
	using NotesTracker.Data.Contracts;
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Notes Data Service Class.
	/// </summary>
	/// <param name="dbContext">The SQL DB Context</param>
	public class NotesDataService(SqlDbContext dbContext) : INotesDataService
	{
		/// <summary>
		/// The database context
		/// </summary>
		private readonly SqlDbContext _dbContext = dbContext;

		/// <summary>
		/// Gets all notes asynchronous.
		/// </summary>
		/// <param name="userName">The user id.</param>
		/// <returns>
		/// The list of note entity
		/// </returns>
		/// <exception cref="System.Exception"></exception>
		public async Task<IEnumerable<Note>> GetAllNotesAsync(string userName)
		{
			var notes = await this._dbContext.Notes.Where(note => note.IsActive && note.UserName == userName).ToListAsync();
			if (notes.Count > 0)
			{
				return notes;
			}
			else
			{
				return [];
			}
		}

		/// <summary>
		/// Gets the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note id.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>
		/// The note entity
		/// </returns>
		public async Task<Note> GetNoteAsync(int noteId, string userName)
		{
			var note = await this._dbContext.Notes.FirstOrDefaultAsync(n => n.IsActive && n.NoteId == noteId && n.UserName == userName);
			if (note is not null)
			{
				return note;
			}

			throw new Exception(ExceptionConstants.NoteNotFoundException);
		}

		/// <summary>
		/// Adds the new note asynchronous.
		/// </summary>
		/// <param name="newNote">The new note.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		/// <exception cref="System.Exception"></exception>
		public async Task<bool> AddNewNoteAsync(NoteDTO newNote)
		{
			try
			{
				var newNoteEntity = new Note
				{
					NoteTitle = newNote.NoteTitle,
					NoteDescription = newNote.NoteDescription,
					CreatedDate = DateTime.UtcNow,
					LastModifiedDate = DateTime.UtcNow,
					IsActive = true,
					UserName = newNote.UserName
				};
				await this._dbContext.Notes.AddAsync(newNoteEntity);
				await this._dbContext.SaveChangesAsync();
				return true;
			}
			catch
			{
				throw new Exception(ExceptionConstants.NoteCreationException);
			}
		}

		/// <summary>
		/// Deletes the note asynchronous.
		/// </summary>
		/// <param name="noteId">The note identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>
		/// The boolean for success/failure
		/// </returns>
		/// <exception cref="System.Exception"></exception>
		public async Task<bool> DeleteNoteAsync(int noteId, string userName)
		{
			var noteToDelete = await this._dbContext.Notes.FirstOrDefaultAsync(note => note.NoteId == noteId && note.IsActive && note.UserName == userName);
			if (noteToDelete is not null)
			{
				noteToDelete.IsActive = false;
				await this._dbContext.SaveChangesAsync();
				return true;
			}

			throw new Exception(ExceptionConstants.NoteNotFoundException);
		}

		/// <summary>
		/// Updates the note asynchronous.
		/// </summary>
		/// <param name="updatedNote">The updated note.</param>
		/// <returns>
		/// The updated note
		/// </returns>
		/// <exception cref="System.Exception"></exception>
		public async Task<Note> UpdateNoteAsync(UpdateNoteDTO updatedNote)
		{
			var noteToUpdate = await this._dbContext.Notes.FirstOrDefaultAsync(note => note.NoteId == updatedNote.NoteId && note.IsActive && note.UserName == updatedNote.UserName);
			if (noteToUpdate is not null)
			{
				noteToUpdate.NoteTitle = updatedNote.NoteTitle;
				noteToUpdate.NoteDescription = updatedNote.NoteDescription;
				noteToUpdate.LastModifiedDate = DateTime.UtcNow;

				await this._dbContext.SaveChangesAsync();
				return noteToUpdate;
			}

			throw new Exception(ExceptionConstants.NoteNotFoundException);
		}
	}
}
