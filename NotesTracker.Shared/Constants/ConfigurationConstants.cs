// *********************************************************************************
//	<copyright file="ConfigurationConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Configuration Constants Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Constants
{
    /// <summary>
    /// The Configuration Constants Class.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
		/// The local sql connection string constant.
		/// </summary>
		public const string LocalSqlConnectionStringConstant = "LocalSqlServerConnection";

        /// <summary>
        /// The SQL connection string constant
        /// </summary>
        public const string SqlConnectionStringConstant = "SqlConnectionString";

        /// <summary>
        /// The local appsettings file constant.
        /// </summary>
        public const string LocalAppsettingsFileConstant = "appsettings.development.json";

        /// <summary>
        /// The user id header constant.
        /// </summary>
        public const string UserIdHeaderConstant = "X-User-Id";

        /// <summary>
        /// The audience constant.
        /// </summary>
        public const string AudienceConstant = "Auth0:Audience";

        /// <summary>
        /// The domain constant.
        /// Alternatively, https://{DomainConstant} would be the auth0 management api route.
        /// </summary>
        public const string DomainConstant = "Auth0:Domain";

        /// <summary>
        /// The user name claim constant.
        /// </summary>
        public const string UserNameClaimConstant = "username";

        /// <summary>
        /// The app configuration endpoint key constant.
        /// </summary>
        public const string AppConfigurationEndpointKeyConstant = "AppConfigurationEndpoint";

        /// <summary>
        /// The managed identity client id constant.
        /// </summary>
        public const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

        /// <summary>
        /// The base configuration app config key constant.
        /// </summary>
        public const string BaseConfigurationAppConfigKeyConstant = "BaseConfiguration";

        /// <summary>
        /// The notes a p i app config key constant.
        /// </summary>
        public const string NotesAPIAppConfigKeyConstant = "NotesAPI";

        /// <summary>
        /// The auth0 token url.
        /// </summary>
        public const string Auth0TokenUrl = "Auth0:TokenUrl";

        #region Notes Function

        /// <summary>
        /// The notes function app config key constant.
        /// </summary>
        public const string NotesFunctionAppConfigKeyConstant = "NotesFunction";

        /// <summary>
        /// The auth0 management api client id constant.
        /// </summary>
        public const string Auth0ManagementApiClientIdConstant = "Auth0:ManagementAPI:ClientId";

        /// <summary>
        /// The auth0 management api client secret constant.
        /// </summary>
        public const string Auth0ManagementApiClientSecretConstant = "Auth0:ManagementAPI:ClientSecret";

        /// <summary>
        /// The client credentials grant.
        /// </summary>
        public const string ClientCredentialsGrant = "client_credentials";

        /// <summary>
        /// The application json constant.
        /// </summary>
        public const string ApplicationJsonConstant = "application/json";

        /// <summary>
        /// The bearer constant.
        /// </summary>
        public const string BearerConstant = "Bearer";

        /// <summary>
        /// The access token constant.
        /// </summary>
        public const string AccessTokenConstant = "access_token";

        /// <summary>
        /// The auth0 management api audience constant.
        /// </summary>
        public const string Auth0ManagementApiAudienceConstant = "Auth0:ManagementAPI:Audience";

        /// <summary>
        /// The auth0 client constant.
        /// </summary>
        public const string Auth0TokenClientConstant = "Auth0TokenClient";

        /// <summary>
        /// The auth0 management client constant.
        /// </summary>
        public const string Auth0ManagementHttpClientConstant = "Auth0ManagementHttpClient";

        #endregion
    }
}
