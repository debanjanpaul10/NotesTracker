// *********************************************************************************
//	<copyright file="INotesTrackerService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Service Interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Contracts
{
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// The Notes Tracker Service Interface.
    /// </summary>
    public interface INotesTrackerService
    {
        /// <summary>
        /// Gets the about us application data asynchronously.
        /// </summary>
        /// <returns>The list of <see cref="ApplicationInfoDataDTO"/></returns>
        Task<List<ApplicationInfoDataDTO>> GetAboutUsDataAsync();
    }
}


