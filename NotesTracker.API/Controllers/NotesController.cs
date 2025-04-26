// *********************************************************************************
//	<copyright file="NotesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Controller API Class.</summary>
// *********************************************************************************

namespace NotesTracker.API.Controllers
{
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Shared.Constants;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// The Notes Controller API Class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <param name="notesService">The notes service.</param>
    /// <param name="logger">The logger.</param>
    [ApiController]
    [Route(RouteConstants.NotesApiRoutePrefix)]
    public class NotesController(INotesService notesService, ILogger<NotesController> logger) : BaseController
    {
        /// <summary>
        /// The notes service
        /// </summary>
        private readonly INotesService _notesService = notesService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<NotesController> _logger = logger;

        /// <summary>
        /// Gets all notes asynchronous.
        /// </summary>
        /// <returns>The response dto.</returns>
        [HttpGet]
        [Route(RouteConstants.GetAllNotes_ApiRoute)]
        public async Task<ResponseDTO> GetAllNotesAsync()
        {
            try
            {
                var notes = await this._notesService.GetAllNotesAsync();
                if (notes is not null)
                {
                    return this.PrepareSuccessResponse(notes);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NotesNotFoundException);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the note by identifier asynchronous.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>The response dto.</returns>
        [HttpGet]
        [Route(RouteConstants.GetNoteById_ApiRoute)]
        public async Task<ResponseDTO> GetNoteByIdAsync(int noteId)
        {
            try
            {
                var note = await this._notesService.GetNoteAsync(noteId);
                if (note is not null)
                {
                    return this.PrepareSuccessResponse(note);
                }
                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NoteNotFoundException);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Adds the new note asynchronous.
        /// </summary>
        /// <param name="newNote">The new note.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.AddNewNote_ApiRoute)]
        public async Task<ResponseDTO> AddNewNoteAsync(NoteDTO newNote)
        {
            try
            {
                var noteData = await this._notesService.AddNewNoteAsync(newNote);
                return this.PrepareSuccessResponse(noteData);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the note asynchronous.
        /// </summary>
        /// <param name="updateNoteDTO">The update note dto.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.UpdateNote_ApiRoute)]
        public async Task<ResponseDTO> UpdateNoteAsync(UpdateNoteDTO updateNoteDTO)
        {
            try
            {
                var updatedNote = await this._notesService.UpdateNoteAsync(updateNoteDTO);
                if (updatedNote is not null)
                {
                    return this.PrepareSuccessResponse(updatedNote);
                }
                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NoteNotFoundException);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the note asynchronously.
        /// </summary>
        /// <param name="noteId">The note id to be deleted.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.DeleteNote_ApiRoute)]
        public async Task<ResponseDTO> DeleteNoteAsync([FromBody]int noteId)
        {
            try
            {
                var deletedNote = await this._notesService.DeleteNoteAsync(noteId);
                return this.PrepareSuccessResponse(deletedNote);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
