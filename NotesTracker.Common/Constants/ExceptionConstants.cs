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
		/// The user not exists exception.
		/// </summary>
		public const string UserNotExistsException = "The entered username or password is incorrect";

		/// <summary>
		/// The user addition failed exception.
		/// </summary>
		public const string UserAdditionFailedException = "Something went wrong while creating the user!";

		/// <summary>
		/// The user already exists message constant.
		/// </summary>
		public const string UserAlreadyExistsMessageConstant = "The given user alias and the user email is already being used!";

		/// <summary>
		/// The user identifier not correct message constant
		/// </summary>
		public const string UserIdNotCorrectMessageConstant = "The user id is not correct. Please enter a valid user id";

		/// <summary>
		/// The user does not exists message constant
		/// </summary>
		public const string UserDoesNotExistsMessageConstant = "The user data does not exist with us anymore!";

		/// <summary>
		/// The null user message constant
		/// </summary>
		public const string NullUserMessageConstant = "Attempted to add null user data.";

		/// <summary>
		/// The user deletion failed message constant.
		/// </summary>
		public const string UserDeletionFailedMessageConstant = "The user deletion operation failed!";

		#endregion
	}
}
