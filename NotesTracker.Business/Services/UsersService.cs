// *********************************************************************************
//	<copyright file="UsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users business service class.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Services
{
    using System.Threading.Tasks;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Users data service.
    /// </summary>
    /// <param name="usersDataService">The users data service.</param>
    /// <seealso cref="IUsersService"/>
    public class UsersService(IUsersDataService usersDataService) : IUsersService
    {
        /// <summary>
        /// The users data service.
        /// </summary>
        private readonly IUsersDataService _usersDataService = usersDataService;

        /// <summary>
        /// Adds users async.
        /// </summary>
        /// <param name="usersData">The users data.</param>
        public async Task AddUsersAsync(List<UsersDataDTO> usersData)
        {
            // Flatten the list of identities from all users.
            var newUsers = usersData
                .SelectMany(user => user.Identities.Select(identity => new User
                {
                    UserId = identity.UserId,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    IsActive = true,
                    IsSocial = identity.IsSocial,
                    Provider = identity.Provider
                })).ToList();

            await this._usersDataService.AddUsersAsync(newUsers);
        }
    }
}


