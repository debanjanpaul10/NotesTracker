// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests Helper Class.</summary>
// *********************************************************************************

namespace NotesTracker.Tests
{
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Tests Helper Class.
	/// </summary>
	static class TestsHelper
	{
		public static UsersDataDTO PrepareUsersDataDTO(int userNumber)
		{
			return new UsersDataDTO
			{
				Email = "user@mail.com",
				Identities = [
					new IdentityDto()
					{
						IsSocial = false,
						Provider = "Microsoft",
						UserId = Guid.NewGuid().ToString(),
					},
					new IdentityDto()
					{
						IsSocial = false,
						Provider = "Google",
						UserId = Guid.NewGuid().ToString(),
					},
					new IdentityDto()
					{
						IsSocial = false,
						Provider = "Facebook",
						UserId = Guid.NewGuid().ToString(),
					}

				],
				UserName = $"Sample User {userNumber}",
			};
		}

		/// <summary>
		/// Prepares the users data list dto.
		/// </summary>
		/// <returns>The list of <see cref="UsersDataDTO"/></returns>
		public static List<UsersDataDTO> PrepareUsersDataListDTO()
		{
			return new List<UsersDataDTO>()
			{
				PrepareUsersDataDTO(1),
				PrepareUsersDataDTO(2),
				PrepareUsersDataDTO(3)
			};
		}

		/// <summary>
		/// Prepares the note dto.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>The note dto.</returns>
		public static NoteDTO PrepareNoteDTO(string userName)
		{
			return new NoteDTO { NoteTitle = "Test", NoteDescription = "Desc", UserName = userName };
		}

		/// <summary>
		/// Prepares the notes data.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>The list of <see cref="Note"/></returns>
		public static List<Note> PrepareNotesData(string userName)
		{
			return [new Note { NoteId = 1, NoteTitle = "A", UserName = userName }];
		}

		/// <summary>
		/// Prepares the update note dto.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>The update note dto.</returns>
		public static UpdateNoteDTO PrepareUpdateNoteDTO(string userName)
		{
			return new UpdateNoteDTO { NoteId = 1, NoteTitle = "Updated", NoteDescription = "Desc", UserName = userName };
		}
	}
}
