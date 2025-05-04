// *********************************************************************************
//	<copyright file="IUsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users business service interface.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Contracts
{
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Users business service interface.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Adds users async.
        /// </summary>
        /// <param name="usersData">The users data.</param>
        Task AddUsersAsync(List<UsersDataDTO> usersData);
    }
}


