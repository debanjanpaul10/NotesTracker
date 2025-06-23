// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Helpers
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using NotesTracker.Shared.Constants;
	using System;
	using System.Text;
	using System.Text.Json;
	using System.Threading.Tasks;
	using static NotesTracker.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// Token Helper class.
	/// </summary>
	public static class TokenHelper
	{
		/// <summary>
		/// Gets the auth0 management API token asynchronous.
		/// </summary>
		/// <param name="httpClientFactory">The HTTP client factory.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="logger">The logger.</param>
		/// <returns>The token for the API.</returns>
		public static async Task<string> GetAuth0ManagementApiTokenAsync<T>(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<T> logger)
		{
			try
			{
				logger.LogInformation(string.Format(ExceptionConstants.MethodStartedMessageConstant, nameof(TokenHelper), DateTime.UtcNow, nameof(GetAuth0ManagementApiTokenAsync)));
				var tokenClient = httpClientFactory.CreateClient(Auth0TokenClientConstant);
				var requestBody = new
				{
					client_id = configuration[Auth0ManagementApiClientIdConstant],
					client_secret = configuration[Auth0ManagementApiClientSecretConstant],
					audience = configuration[Auth0ManagementApiAudienceConstant],
					grant_type = ClientCredentialsGrant
				};
				var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, ApplicationJsonConstant);

				var response = await tokenClient.PostAsync(string.Empty, content);
				if (!response.IsSuccessStatusCode)
				{
					response.EnsureSuccessStatusCode();
					var exception = new UnauthorizedAccessException();
					logger.LogError(string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiTokenAsync), DateTime.UtcNow, exception.Message));

					throw exception;
				}

				var responseContent = await response.Content.ReadAsStringAsync();
				var responseJson = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseContent);
				return responseJson.GetProperty(AccessTokenConstant).GetString() ?? string.Empty;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, string.Format(ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAuth0ManagementApiTokenAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				logger.LogInformation(string.Format(ExceptionConstants.MethodEndedMessageConstant, nameof(TokenHelper), DateTime.UtcNow, nameof(GetAuth0ManagementApiTokenAsync)));
			}
		}
	}
}
