// *********************************************************************************
//	<copyright file="User.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Entity Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data.Entities
{
    /// <summary>
    /// The User Entity Class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the is social flag.
        /// </summary>
        /// <value>
        /// The is social.
        /// </value>
        public bool IsSocial { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool IsActive { get; set; }
    }

}

