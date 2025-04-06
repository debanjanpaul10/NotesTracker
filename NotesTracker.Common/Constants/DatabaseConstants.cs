// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Configuration Constants Class.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.Constants
{
	/// <summary>
	/// The Database Constants Class.
	/// </summary>
	public static class DatabaseConstants
	{
		#region Data Type Constants

		/// <summary>
		/// The integer data type constant
		/// </summary>
		public const string IntegerDataTypeConstant = "int";

		/// <summary>
		/// The n variable character maximum data type constant
		/// </summary>
		public const string NVarCharMaxDataTypeConstant = "nvarchar(max)";

		/// <summary>
		/// The bit data type constant
		/// </summary>
		public const string BitDataTypeConstant = "bit";

		/// <summary>
		/// The date time data type constant
		/// </summary>
		public const string DateTimeDataTypeConstant = "datetime";

		#endregion

		/// <summary>
		/// The notes table constant
		/// </summary>
		public const string NotesTableConstant = "Notes";

		/// <summary>
		/// The primary key notes constant
		/// </summary>
		public const string PrimaryKeyNotesConstant = "PK_Notes";

		/// <summary>
		/// The note identifier column name constant
		/// </summary>
		public const string NoteIdColumnNameConstant = "NoteId";

		/// <summary>
		/// The note title column name constant
		/// </summary>
		public const string NoteTitleColumnNameConstant = "NoteTitle";

		/// <summary>
		/// The note description column name constant
		/// </summary>
		public const string NoteDescriptionColumnNameConstant = "NoteDescription";

		/// <summary>
		/// The created date column name constant
		/// </summary>
		public const string CreatedDateColumnNameConstant = "CreatedDate";

		/// <summary>
		/// The last modified date column name constant
		/// </summary>
		public const string LastModifiedDateColumnNameConstant = "LastModifiedDate";

		/// <summary>
		/// The is active column name constant
		/// </summary>
		public const string IsActiveColumnNameConstant = "IsActive";

	}
}
