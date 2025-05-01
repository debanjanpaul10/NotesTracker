// *********************************************************************************
//	<copyright file="UsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users business service class.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Services
{
    using System;
    using System.Threading.Tasks;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.Constants;
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
        /// Adds new user async.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<bool> AddNewUserAsync(UserRegisterDTO newUser)
        {
            if (newUser is null)
            {
                throw new Exception(ExceptionConstants.NullUserMessageConstant);
            }

            var dbUser = new User()
            {
                IsActive = true,
                UserEmail = newUser.UserEmail,
                UserPassword = newUser.UserPassword,
                UserName = newUser.UserName
            };
            var result = await this._usersDataService.AddNewUserAsync(dbUser);
            return result;
        }

        /// <summary>
        /// Deletes user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new Exception(ExceptionConstants.UserIdNotCorrectMessageConstant);
            }

            var result = await this._usersDataService.DeleteUserAsync(userId);
            return result;
        }

        /// <summary>
        /// Gets user async.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<User> GetUserAsync(UserLoginDTO userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserEmail) || string.IsNullOrEmpty(userLogin.UserPassword))
            {
                throw new Exception(ExceptionConstants.UserIdNotCorrectMessageConstant);
            }

            var result = await this._usersDataService.GetUserAsync(userLogin);
            if (result is not null && result.UserId > 0)
            {
                return result;
            }
            else
            {
                throw new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
            }
        }
    }
}


