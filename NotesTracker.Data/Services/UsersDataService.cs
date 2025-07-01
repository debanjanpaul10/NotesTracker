// *********************************************************************************
//	<copyright file="UsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using NotesTracker.Data.Contracts;
    using NotesTracker.Data.Entities;
    using NotesTracker.Shared.Constants;

    /// <summary>
    /// Users data service.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <seealso cref="IUsersDataService"/>
    public class UsersDataService(SqlDbContext dbContext, ILogger<UsersDataService> logger) : IUsersDataService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly SqlDbContext _dbContext = dbContext;

        private readonly ILogger<UsersDataService> _logger = logger;

        /// <summary>
        /// Adds users async.
        /// </summary>
        /// <param name="newUsers">The users data.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public async Task<bool> AddUsersAsync(List<User> newUsers)
        {
            try
            {
                this._logger.LogInformation(string.Format(ExceptionConstants.MethodStartedMessageConstant, nameof(AddUsersAsync), DateTime.UtcNow, JsonConvert.SerializeObject(newUsers)));

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

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(AddUsersAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(ExceptionConstants.MethodEndedMessageConstant, nameof(AddUsersAsync), DateTime.UtcNow, JsonConvert.SerializeObject(newUsers)));
            }

        }
    }

}

