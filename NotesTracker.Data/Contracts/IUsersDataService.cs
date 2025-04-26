// *********************************************************************************
//	<copyright file="IUsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service interface.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Contracts
{
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Users data service interface.
    /// </summary>
    public interface IUsersDataService
    {
        /// <summary>
        /// Gets user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        Task<User> GetUserAsync(UserLoginDTO userLogin);

        /// <summary>
        /// Adds new user async.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        Task<bool> AddNewUserAsync(User newUser);

        /// <summary>
        /// Deletes user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        Task<bool> DeleteUserAsync(int userId);
    }
}


