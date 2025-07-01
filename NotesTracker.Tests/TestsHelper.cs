// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests Helper Class.</summary>
// *********************************************************************************

using NotesTracker.Data.Entities;

namespace NotesTracker.Tests;

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

	/// <summary>
	/// Prepares the list of graph users data dto.
	/// </summary>
	/// <returns>The list of user data dto.</returns>
	public static List<User> PrepareListofGraphUserData()
	{
		return
		[
			new()
			{
				Id = 1,
				IsActive = true,
				IsSocial = false,
				Provider = "Microsoft",
				UserEmail = "user1@email.com",
				UserId = Guid.NewGuid().ToString(),
				UserName = "userName1"
			},
			new()
			{
				Id = 2,
				IsActive = true,
				IsSocial = false,
				Provider = "Microsoft",
				UserEmail = "user2@email.com",
				UserId = Guid.NewGuid().ToString(),
				UserName = "userName2"
			},
			new()
			{
				Id = 3,
				IsActive = true,
				IsSocial = false,
				Provider = "Microsoft",
				UserEmail = "user3@email.com",
				UserId = Guid.NewGuid().ToString(),
				UserName = "userName3"
			}
		];
	}

	/// <summary>
	/// Prepares the list of existing graph users data.
	/// </summary>
	/// <param name="userId">The passed user id.</param>
	/// <returns>The list of users.</returns>
	public static List<User> PrepareExistingGraphUserData()
	{
		return
		[
			new()
			{
				Id = 1,
				IsActive = true,
				IsSocial = false,
				Provider = "Microsoft",
				UserEmail = "user1@email.com",
				UserId = Guid.NewGuid().ToString(),
				UserName = "userName1"
			},
			new()
			{
				Id = 2,
				IsActive = true,
				IsSocial = false,
				Provider = "FaceBook",
				UserEmail = "user2@email.com",
				UserId = Guid.NewGuid().ToString(),
				UserName = "userName2"
			}
		];
	}

	/// <summary>
	/// Prepares the notes data list.
	/// </summary>
	/// <param name="userName">The user name</param>
	/// <param name="otherUserName">The user name for other user.</param>
	/// <returns>The list of note dto.</returns>
	public static List<Note> PrepareNotesDataList(string userName, string otherUserName)
	{
		return
		[
			new() { NoteId = 1, NoteTitle = "A", NoteDescription = "Desc", UserName = userName, IsActive = true, CreatedDate = DateTime.UtcNow, LastModifiedDate = DateTime.UtcNow },
			new() { NoteId = 2, NoteTitle = "B", NoteDescription = "Desc", UserName = userName, IsActive = false, CreatedDate = DateTime.UtcNow, LastModifiedDate = DateTime.UtcNow },
			new() { NoteId = 3, NoteTitle = "C", NoteDescription = "Desc", UserName = otherUserName, IsActive = true, CreatedDate = DateTime.UtcNow, LastModifiedDate = DateTime.UtcNow }
		];
	}

	/// <summary>
	/// Prepares the note data.
	/// </summary>
	/// <param name="userName">The username.</param>
	/// <returns>The note data dto.</returns>
	public static Note PrepareNoteData(string userName)
	{
		return new Note { NoteId = 10, NoteTitle = "Test", NoteDescription = "Desc", UserName = userName, IsActive = true, CreatedDate = DateTime.UtcNow, LastModifiedDate = DateTime.UtcNow };
	}

	public static UpdateNoteDTO PrepareUpdateNoteDataDTO(string userName)
	{
		return new UpdateNoteDTO { NoteId = 10, NoteTitle = "NewTitle", NoteDescription = "NewDesc", UserName = userName };
	}
}
