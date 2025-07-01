// *********************************************************************************
//	<copyright file="IUsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service interface.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Contracts
{
    using NotesTracker.Data.Entities;

    /// <summary>
    /// Users data service interface.
    /// </summary>
    public interface IUsersDataService
    {
        /// <summary>
        /// Adds users async.
        /// </summary>
        /// <param name="newUsers">The users data.</param>
        Task<bool> AddUsersAsync(List<User> newUsers);
    }
}


