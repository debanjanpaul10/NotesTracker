// *********************************************************************************
//	<copyright file="ExceptionConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Exception Constants Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Constants
{
	/// <summary>
	/// The Exception Constants Class.
	/// </summary>
	public static class ExceptionConstants
	{
		#region System Exceptions

		/// <summary>
		/// The user id not present exception constant.
		/// </summary>
		public const string UserIdNotPresentExceptionConstant = "User id is not present in the headers.";

		/// <summary>
		/// The invalid token exception constant.
		/// </summary>
		public const string InvalidTokenExceptionConstant = "Invalid token: Identity is not authenticated.";

		/// <summary>
		/// The missing configuration message.
		/// </summary>
		public const string MissingConfigurationMessage = "The Configuration Key is missing";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		public const string MethodFailedWithMessageConstant = "{0} failed at {1} with {2}";

		#endregion

		#region Custom Exceptions

		/// <summary>
		/// The notes not found exception
		/// </summary>
		public const string NotesNotFoundException = "No Notes not found";

		/// <summary>
		/// The note not found exception
		/// </summary>
		public const string NoteNotFoundException = "Note not found";

		/// <summary>
		/// The note creation exception
		/// </summary>
		public const string NoteCreationException = "Something went wrong while creating the note!";

		/// <summary>
		/// Creates new notenullexception.
		/// </summary>
		public const string NewNoteNullException = "The new note cannot be null.";

		/// <summary>
		/// The user does not exists message constant
		/// </summary>
		public const string UserDoesNotExistsMessageConstant = "The user data does not exist with us anymore!";

		/// <summary>
		/// The attempted to enter blank user message constant.
		/// </summary>
		public const string AttemptedToEnterBlankUserMessageConstant = "Attempted to get notes for null user";

		/// <summary>
		/// Something went wrong exception message constants.
		/// </summary>
		public const string SomethingWentWrongMessageConstant = "Oops! It seems something went wrong!";

		#endregion

		#region Logging Messages

		/// <summary>
		/// The method started message constant
		/// </summary>
		public static readonly string MethodStartedMessageConstant = "Method {0} started at {1} for {2}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		public static readonly string MethodEndedMessageConstant = "Method {0} ended at {1} for {2}";

		#endregion
	}
}
