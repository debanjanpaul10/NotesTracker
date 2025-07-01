// *********************************************************************************
//	<copyright file="IdentityDto.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Auth0 Identity Data DTO.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Auth0 Identity Data DTO.
    /// </summary>
    public class IdentityDto
    {
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [JsonPropertyName("provider")]
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the is social.
        /// </summary>
        /// <value>
        /// The is social.
        /// </value>
        [JsonPropertyName("isSocial")]
        public bool IsSocial { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
    }
}
