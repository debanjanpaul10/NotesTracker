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
    }
}
