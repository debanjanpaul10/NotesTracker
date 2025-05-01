// *********************************************************************************
//	<copyright file="UsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Services
{
    using BCrypt.Net;
    using Microsoft.EntityFrameworkCore;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.Constants;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Users data service.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <seealso cref="IUsersDataService"/>
    public class UsersDataService(SqlDbContext dbContext) : IUsersDataService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly SqlDbContext _dbContext = dbContext;

        /// <summary>
        /// Adds new user async.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<bool> AddNewUserAsync(User newUser)
        {
            try
            {
                var existingUser = await this._dbContext.Users.AnyAsync(exu => exu.IsActive && (exu.UserName == newUser.UserName || exu.UserEmail == newUser.UserEmail));
                if (!existingUser)
                {
                    newUser.UserPassword = BCrypt.HashPassword(newUser.UserPassword);
                    await this._dbContext.Users.AddAsync(newUser);
                    await this._dbContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    var exception = new Exception(ExceptionConstants.UserAlreadyExistsMessageConstant);
                    throw exception;
                }

            }
            catch
            {
                throw new Exception(ExceptionConstants.UserAdditionFailedException);
            }
        }

        /// <summary>
        /// Deletes user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var dbUserData = await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive);
            if (dbUserData is not null)
            {
                dbUserData.IsActive = false;
                await this._dbContext.SaveChangesAsync();

                return true;
            }

            throw new Exception(ExceptionConstants.UserNotExistsException);
        }

        /// <summary>
        /// Gets user async.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<User> GetUserAsync(UserLoginDTO userLogin)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(u => u.IsActive == true && userLogin.UserEmail == u.UserEmail);
            if (user is not null && BCrypt.Verify(userLogin.UserPassword, user.UserPassword))
            {
                return new User()
                {
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserId = user.UserId
                };
            }

            throw new Exception(ExceptionConstants.UserNotExistsException);
        }
    }

}

