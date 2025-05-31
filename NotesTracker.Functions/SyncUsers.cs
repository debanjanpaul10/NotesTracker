// *********************************************************************************
//	<copyright file="SyncUsers.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Sync Users Azure Function Class.</summary>
// *********************************************************************************

namespace NotesTracker.Functions
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.Logging;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Shared.Constants;
    using NotesTracker.Shared.DTO;
    using NotesTracker.Shared.Helpers;

    /// <summary>
    /// Azure Function responsible for Syncing the user data from `Auth0` to `SQL DB`
    /// </summary>
    /// <param name="loggerFactory">The Logger Factory</param>
    /// <param name="httpClientHelper">The Http Client helper</param>
    /// <param name="usersService">The users business services</param>
    public class SyncUsers(ILoggerFactory loggerFactory, IHttpClientHelper httpClientHelper, IUsersService usersService)
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger = loggerFactory.CreateLogger<SyncUsers>();

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

        /// <summary>
        /// The users service.
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// The Main function.
        /// </summary>
        /// <param name="myTimer">The timer value</param>
        [Function("SyncUsers")]
        public async Task Run([TimerTrigger("00:00:05")] TimerInfo myTimer)
        {
            try
            {
                this._logger.LogInformation("{functionname} function started at {time}", nameof(SyncUsers), DateTime.UtcNow);

                var response = await this._httpClientHelper.GetAuth0ManagementApiUsersAsync(RouteConstants.Auth0Users_ApiRoute);
                var usersData = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(usersData))
                {
                    var exception = new ApplicationException(ExceptionConstants.UserDoesNotExistsMessageConstant);
                    this._logger.LogError(string.Format(
                        ExceptionConstants.MethodFailedWithMessageConstant, nameof(SyncUsers), DateTime.UtcNow, exception.Message));

                    throw exception;
                }

                var auth0Users = JsonSerializer.Deserialize<List<UsersDataDTO>>(usersData);
                if (auth0Users is not null && auth0Users.Count > 0)
                {
                    await this._usersService.AddUsersAsync(usersData: auth0Users);
                    _logger.LogInformation("Successfully synced {count} users to the database.", auth0Users.Count);
                }
                else
                {
                    this._logger.LogWarning("No new users to sync");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format(
                    ExceptionConstants.MethodFailedWithMessageConstant, nameof(SyncUsers), DateTime.UtcNow, ex.Message));
                throw;
            }

        }
    }

}
