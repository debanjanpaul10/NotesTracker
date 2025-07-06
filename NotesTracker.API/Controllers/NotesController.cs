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
    /// <param name="httpContextAccessor">The Http Context accessor.</param>
    [ApiController]
    [Route(RouteConstants.ApiRoutePrefix)]
    public class NotesController(INotesService notesService, ILogger<NotesController> logger, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
    {
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
                var notes = await notesService.GetAllNotesAsync(this.UserName);
                if (notes is not null)
                {
                    return this.PrepareSuccessResponse(notes);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NotesNotFoundException);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAllNotesAsync), DateTime.UtcNow, ex.Message));
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
                var note = await notesService.GetNoteAsync(noteId, this.UserName);
                if (note is not null)
                {
                    return this.PrepareSuccessResponse(note);
                }
                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NoteNotFoundException);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetNoteByIdAsync), DateTime.UtcNow, ex.Message));
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
                var noteData = await notesService.AddNewNoteAsync(newNote);
                return this.PrepareSuccessResponse(noteData);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(AddNewNoteAsync), DateTime.UtcNow, ex.Message));
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
                var updatedNote = await notesService.UpdateNoteAsync(updateNoteDTO);
                if (updatedNote is not null)
                {
                    return this.PrepareSuccessResponse(updatedNote);
                }
                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.NoteNotFoundException);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(UpdateNoteAsync), DateTime.UtcNow, ex.Message));
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
        public async Task<ResponseDTO> DeleteNoteAsync([FromBody] int noteId)
        {
            try
            {
                var deletedNote = await notesService.DeleteNoteAsync(noteId, this.UserName);
                return this.PrepareSuccessResponse(deletedNote);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(DeleteNoteAsync), DateTime.UtcNow, ex.Message));
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
