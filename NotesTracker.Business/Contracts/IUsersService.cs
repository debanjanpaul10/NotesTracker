// *********************************************************************************
//	<copyright file="IUsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users business service interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Contracts
{
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Users business service interface.
    /// </summary>
    public interface IUsersService
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
        Task<bool> AddNewUserAsync(UserRegisterDTO newUser);

        /// <summary>
        /// Deletes user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        Task<bool> DeleteUserAsync(int userId);
    }
}


