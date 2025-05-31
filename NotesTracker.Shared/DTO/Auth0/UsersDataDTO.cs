// *********************************************************************************
//	<copyright file="UsersDataDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Auth0 Users Data DTO.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Auth0 Users Data DTO.
    /// </summary>
    public class UsersDataDTO
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the identities.
        /// </summary>
        /// <value>
        /// The identities.
        /// </value>
        [JsonPropertyName("identities")]
        public List<IdentityDto> Identities { get; set; }
    }
}


