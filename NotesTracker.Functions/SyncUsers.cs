// *********************************************************************************
//	<copyright file="SyncUsers.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Sync Users Azure Function Class.</summary>
// *********************************************************************************

namespace NotesTracker.Functions
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Shared.Constants;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// Azure Function responsible for Syncing the user data from `Auth0` to `SQL DB`
    /// </summary>
    /// <param name="loggerFactory">The Logger Factory</param>
    /// <param name="configuration">The Configuration</param>
    /// <param name="httpClient">The Http Client</param>
    /// <param name="usersService">The users business services</param>
    public class SyncUsers(
        ILoggerFactory loggerFactory, HttpClient httpClient,
        IConfiguration configuration, IUsersService usersService)
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger = loggerFactory.CreateLogger<SyncUsers>();

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The users service.
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// The Main function.
        /// </summary>
        /// <param name="myTimer">The timer value</param>
        [Function("SyncUsers")]
        public async Task Run([TimerTrigger("0 10 * * *")] TimerInfo myTimer)
        {
            try
            {
                this._logger.LogInformation("{functionname} function started at {time}", nameof(SyncUsers), DateTime.UtcNow);

                var authToken = await this.GetAuth0ManagementApiTokenAsync();
                if (string.IsNullOrEmpty(authToken))
                {
                    var exception = new UnauthorizedAccessException();
                    this._logger.LogError(string.Format(
                        ExceptionConstants.MethodFailedWithMessageConstant, nameof(SyncUsers), DateTime.UtcNow, exception.Message));

                    throw exception;
                }

                var response = await this.GetAuth0ManagementApiUsersAsync(authToken);
                if (string.IsNullOrEmpty(response))
                {
                    var exception = new ApplicationException(ExceptionConstants.UserDoesNotExistsMessageConstant);
                    this._logger.LogError(string.Format(
                        ExceptionConstants.MethodFailedWithMessageConstant, nameof(SyncUsers), DateTime.UtcNow, exception.Message));

                    throw exception;
                }

                var auth0Users = JsonSerializer.Deserialize<List<UsersDataDTO>>(response);
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

        #region PRIVATE Methods

        /// <summary>
        /// Gets auth0 management api token async.
        /// </summary>
        private async Task<string> GetAuth0ManagementApiTokenAsync()
        {
            var requestBody = new
            {
                client_id = this._configuration[ConfigurationConstants.Auth0ManagementApiClientIdConstant],
                client_secret = this._configuration[ConfigurationConstants.Auth0ManagementApiClientSecretConstant],
                audience = this._configuration[ConfigurationConstants.Auth0ManagementApiAudienceConstant],
                grant_type = ConfigurationConstants.ClientCredentialsGrant
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, ConfigurationConstants.ApplicationJsonConstant);
            var tokenApi = this._configuration[ConfigurationConstants.Auth0TokenUrl];

            var response = await this._httpClient.PostAsync(tokenApi, content);
            if (!response.IsSuccessStatusCode)
            {
                var exception = new UnauthorizedAccessException();
                this._logger.LogError(string.Format(
                    ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiTokenAsync), DateTime.UtcNow, exception.Message));

                throw exception;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

            return responseJson.GetProperty(ConfigurationConstants.AccessTokenConstant).GetString() ?? string.Empty;
        }

        /// <summary>
        /// Gets auth0 management api users async.
        /// </summary>
        /// <param name="authToken">The auth token.</param>
        private async Task<string> GetAuth0ManagementApiUsersAsync(string authToken)
        {
            var usersApiUrl = $"{this._configuration[ConfigurationConstants.Auth0ManagementApiAudienceConstant]}{RouteConstants.Auth0Users_ApiRoute}";
            var request = new HttpRequestMessage(HttpMethod.Get, usersApiUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ConfigurationConstants.BearerConstant, authToken);

            var response = await this._httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var exception = new ApplicationException();
                this._logger.LogError(string.Format(
                    ExceptionConstants.MethodFailedWithMessageConstant, nameof(SyncUsers), DateTime.UtcNow, exception.Message));

                throw exception;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }

    #endregion
}
