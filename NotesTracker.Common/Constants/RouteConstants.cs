// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Route Constants Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Constants
{
	/// <summary>
	/// The Route Constants Class.
	/// </summary>
	public static class RouteConstants
	{
		/// <summary>
		/// The notes API route prefix
		/// </summary>
		public const string ApiRoutePrefix = "api/[controller]";

		#region NOTES

		/// <summary>
		/// The get all notes API route
		/// </summary>
		public const string GetAllNotes_ApiRoute = "GetAllNotes";

		/// <summary>
		/// The get note by identifier API route
		/// </summary>
		public const string GetNoteById_ApiRoute = "GetNoteById";

		/// <summary>
		/// The add new note API route
		/// </summary>
		public const string AddNewNote_ApiRoute = "AddNewNote";

		/// <summary>
		/// The update note API route
		/// </summary>
		public const string UpdateNote_ApiRoute = "UpdateNote";

		/// <summary>
		/// The delete note API route
		/// </summary>
		public const string DeleteNote_ApiRoute = "DeleteNote";

		#endregion

		#region USERS

		/// <summary>
		/// The get user api route.
		/// </summary>
		public const string GetUser_ApiRoute = "GetUser";

		/// <summary>
		/// The add new user api route.
		/// </summary>
		public const string AddNewUser_ApiRoute = "AddNewUser";

		/// <summary>
		/// The delete user api route.
		/// </summary>
		public const string DeleteUser_ApiRoute = "DeleteUser";

		#endregion
	}
}
