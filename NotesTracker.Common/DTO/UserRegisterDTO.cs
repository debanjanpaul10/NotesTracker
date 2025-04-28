// *********************************************************************************
//	<copyright file="UserRegisterDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Register Data Transfer Object.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
    /// <summary>
    /// The User Register Data Transfer Object.
    /// </summary>
    public class UserRegisterDTO
    {
        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        /// <value>
        /// The user password.
        /// </value>
        public string UserPassword { get; set; } = string.Empty;
    }

}

