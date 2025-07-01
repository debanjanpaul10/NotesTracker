// *********************************************************************************
// <copyright file="NotesServiceTests.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>Notes Service Tests Class.</summary>
// *********************************************************************************

namespace NotesTracker.Tests.Business;

using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NotesTracker.Business.Services;
using NotesTracker.Data.Contracts;
using NotesTracker.Data.Entities;
using NotesTracker.Shared.DTO;
using Xunit;

/// <summary>
/// Notes Service Tests Class.
/// </summary>
public class NotesServiceTests
{
	/// <summary>
	/// The mock notes data service
	/// </summary>
	private readonly Mock<INotesDataService> _mockNotesDataService;

	/// <summary>
	/// The notes service
	/// </summary>
	private readonly NotesService _notesService;

	/// <summary>
	/// The user name
	/// </summary>
	private readonly string UserName = "TestUser";

	/// <summary>
	/// Initializes a new instance of the <see cref="NotesServiceTests"/> class.
	/// </summary>
	public NotesServiceTests()
	{
		this._mockNotesDataService = new Mock<INotesDataService>();
		this._notesService = new NotesService(this._mockNotesDataService.Object);
	}

	/// <summary>
	/// Adds the new note asynchronous with valid note returns true.
	/// </summary>
	[Fact]
	public async Task AddNewNoteAsync_WithValidNote_ReturnsTrue()
	{
		// Arrange
		var noteDto = TestsHelper.PrepareNoteDTO(this.UserName);
		this._mockNotesDataService.Setup(x => x.AddNewNoteAsync(noteDto)).ReturnsAsync(true);

		// Act
		var result = await this._notesService.AddNewNoteAsync(noteDto);

		// Assert
		Assert.True(result);
		this._mockNotesDataService.Verify(x => x.AddNewNoteAsync(noteDto), Times.Once);
	}

	/// <summary>
	/// Adds the new note asynchronous with null note throws argument null exception.
	/// </summary>
	[Fact]
	public async Task AddNewNoteAsync_WithNullNote_ThrowsArgumentNullException()
	{
		// Arrange
		NoteDTO noteDto = null!;

		// Act & Assert
		await Assert.ThrowsAsync<NullReferenceException>(() => this._notesService.AddNewNoteAsync(noteDto));
	}

	/// <summary>
	/// Deletes the note asynchronous with valid identifier returns true.
	/// </summary>
	[Fact]
	public async Task DeleteNoteAsync_WithValidId_ReturnsTrue()
	{
		// Arrange
		int noteId = 1;
		this._mockNotesDataService.Setup(x => x.DeleteNoteAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

		// Act
		var result = await this._notesService.DeleteNoteAsync(noteId, this.UserName);

		// Assert
		Assert.True(result);
		this._mockNotesDataService.Verify(x => x.DeleteNoteAsync(noteId, this.UserName), Times.Once);
	}

	/// <summary>
	/// Deletes the note asynchronous with invalid identifier throws argument null exception.
	/// </summary>
	[Fact]
	public async Task DeleteNoteAsync_WithInvalidId_ThrowsArgumentNullException()
	{
		// Arrange
		int noteId = 0;

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.DeleteNoteAsync(noteId, this.UserName));
	}

	/// <summary>
	/// Gets all notes asynchronous returns notes list.
	/// </summary>
	[Fact]
	public async Task GetAllNotesAsync_ReturnsNotesList()
	{
		// Arrange
		var notes = TestsHelper.PrepareNotesData(this.UserName);
		this._mockNotesDataService.Setup(x => x.GetAllNotesAsync(It.IsAny<string>())).ReturnsAsync(notes);

		// Act
		var result = await this._notesService.GetAllNotesAsync(this.UserName);

		// Assert
		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal("A", result.First().NoteTitle);
	}

	/// <summary>
	/// Gets the note asynchronous with valid identifier returns note.
	/// </summary>
	[Fact]
	public async Task GetNoteAsync_WithValidId_ReturnsNote()
	{
		// Arrange
		int noteId = 1;
		var note = new Note { NoteId = noteId, NoteTitle = "A", UserName = this.UserName };
		this._mockNotesDataService.Setup(x => x.GetNoteAsync(noteId, It.IsAny<string>())).ReturnsAsync(note);

		// Act
		var result = await this._notesService.GetNoteAsync(noteId, this.UserName);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(noteId, result.NoteId);
	}

	/// <summary>
	/// Gets the note asynchronous with invalid identifier throws argument null exception.
	/// </summary>
	[Fact]
	public async Task GetNoteAsync_WithInvalidId_ThrowsArgumentNullException()
	{
		// Arrange
		int noteId = 0;

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.GetNoteAsync(noteId, this.UserName));
	}

	/// <summary>
	/// Updates the note asynchronous with valid note returns updated note.
	/// </summary>
	[Fact]
	public async Task UpdateNoteAsync_WithValidNote_ReturnsUpdatedNote()
	{
		// Arrange
		var updateDto = TestsHelper.PrepareUpdateNoteDTO(this.UserName);
		var updatedNote = new Note { NoteId = 1, NoteTitle = "Updated", NoteDescription = "Desc", UserName = this.UserName };
		this._mockNotesDataService.Setup(x => x.UpdateNoteAsync(updateDto)).ReturnsAsync(updatedNote);

		// Act
		var result = await this._notesService.UpdateNoteAsync(updateDto);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Updated", result.NoteTitle);
	}

	/// <summary>
	/// Updates the note asynchronous with null note throws argument null exception.
	/// </summary>
	[Fact]
	public async Task UpdateNoteAsync_WithNullNote_ThrowsArgumentNullException()
	{
		// Arrange
		UpdateNoteDTO updateDto = null!;

		// Act & Assert
		await Assert.ThrowsAsync<NullReferenceException>(() => this._notesService.UpdateNoteAsync(updateDto));
	}

	/// <summary>
	/// Alls the methods with blank user name throws argument null exception.
	/// </summary>
	[Fact]
	public async Task AllMethods_WithBlankUserName_ThrowsArgumentNullException()
	{
		// Arrange
		var noteDto = TestsHelper.PrepareNoteDTO(string.Empty);
		var updateDto = TestsHelper.PrepareUpdateNoteDTO(string.Empty);

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.AddNewNoteAsync(noteDto));
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.DeleteNoteAsync(1, string.Empty));
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.GetAllNotesAsync(string.Empty));
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.GetNoteAsync(1, string.Empty));
		await Assert.ThrowsAsync<ArgumentNullException>(() => this._notesService.UpdateNoteAsync(updateDto));
	}
}