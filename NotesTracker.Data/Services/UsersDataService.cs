// *********************************************************************************
//	<copyright file="UsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Services
{
    using Microsoft.EntityFrameworkCore;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Entities;

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
        /// Adds users async.
        /// </summary>
        /// <param name="newUsers">The users data.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task AddUsersAsync(List<User> newUsers)
        {
            // Get the list of existing UserIds from the database.
            var existingUserIds = await this._dbContext.Users.Where(u => newUsers.Select(nu => nu.UserId).Contains(u.UserId))
                .Select(u => u.UserId).ToListAsync();

            // Filter out users that already exist.
            var usersToAdd = newUsers.Where(newUser => !existingUserIds.Contains(newUser.UserId)).ToList();

            // Add only the new users to the database.
            if (usersToAdd.Count > 0)
            {
                await this._dbContext.Users.AddRangeAsync(usersToAdd);
                await this._dbContext.SaveChangesAsync();
            }
        }
    }

}

