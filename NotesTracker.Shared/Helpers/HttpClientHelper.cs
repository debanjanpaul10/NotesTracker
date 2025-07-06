namespace NotesTracker.Shared.Helpers
{
	using System.Diagnostics.CodeAnalysis;
	using System.Net.Http;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
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
				var authToken = await TokenHelper.GetAuth0ManagementApiTokenAsync(this._httpClientFactory, this._configuration, this._logger);
				managementClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(BearerConstant, authToken);

				var response = await managementClient.GetAsync(api);
				if (!response.IsSuccessStatusCode)
				{
					var exception = new ApplicationException();
					this._logger.LogError(string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiUsersAsync), DateTime.UtcNow, exception.Message));

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


	}
}