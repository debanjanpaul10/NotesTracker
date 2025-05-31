namespace NotesTracker.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using NotesTracker.Shared.Constants;
    using static NotesTracker.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Http client helper interface.
    /// </summary>
    public interface IHttpClientHelper
    {

        /// <summary>
        /// Gets auth0 management api users async.
        /// </summary>
        /// <param name="api">The api.</param>
        /// <returns>The http response message</returns>
        Task<HttpResponseMessage> GetAuth0ManagementApiUsersAsync(string api);
    }

    /// <summary>
    /// Http client helper class.
    /// </summary>
    /// <param name="httpClientFactory">The http client factory.</param>
    /// <param name="logger">The logger.</param>
    /// <seealso cref="IHttpClientHelper"/>
    [ExcludeFromCodeCoverage]
    public class HttpClientHelper(IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper> logger, IConfiguration configuration) : IHttpClientHelper
    {
        /// <summary>
        /// The _http client factory.
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <summary>
        /// The _logger.
        /// </summary>
        private readonly ILogger<HttpClientHelper> _logger = logger;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Gets auth0 management api users async.
        /// </summary>
        /// <param name="api">The api.</param>
        /// <param name="authToken">The auth token.</param>
        /// <returns>The http response message</returns>
        public async Task<HttpResponseMessage> GetAuth0ManagementApiUsersAsync(string api)
        {
            try
            {
                var managementClient = this._httpClientFactory.CreateClient(Auth0ManagementHttpClientConstant);
                var authToken = await this.GetAuth0ManagementApiTokenAsync();
                managementClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(BearerConstant, authToken);

                var response = await managementClient.GetAsync(api);
                if (!response.IsSuccessStatusCode)
                {
                    var exception = new ApplicationException();
                    this._logger.LogError(string.Format(
                        ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiUsersAsync), DateTime.UtcNow, exception.Message));

                    throw exception;
                }

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiUsersAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
        }

        #region PRIVATE Methods

        /// <summary>
        /// Gets auth0 management api token async.
        /// </summary>
        /// <returns>The http response message.</returns>
        private async Task<string> GetAuth0ManagementApiTokenAsync()
        {
            try
            {
                var tokenClient = this._httpClientFactory.CreateClient(Auth0TokenClientConstant);
                var requestBody = new
                {
                    client_id = this._configuration[Auth0ManagementApiClientIdConstant],
                    client_secret = this._configuration[Auth0ManagementApiClientSecretConstant],
                    audience = this._configuration[Auth0ManagementApiAudienceConstant],
                    grant_type = ClientCredentialsGrant
                };
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, ApplicationJsonConstant);

                var response = await tokenClient.PostAsync(string.Empty, content);
                if (!response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var exception = new UnauthorizedAccessException();
                    this._logger.LogError(string.Format(
                        ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiTokenAsync), DateTime.UtcNow, exception.Message));

                    throw exception;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseJson = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseContent);
                return responseJson.GetProperty(AccessTokenConstant).GetString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiTokenAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
        }

        #endregion
    }
}