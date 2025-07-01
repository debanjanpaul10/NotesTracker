// *********************************************************************************
//	<copyright file="NotesDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Data Service Tests Class.</summary>
// *********************************************************************************

namespace NotesTracker.Tests.Data;

/// <summary>
/// The Notes Data Service Tests Class.
/// </summary>
public class NotesDataServiceTests
{
	/// <summary>
	/// The database context for the tests.
	/// </summary>
	private readonly SqlDbContext _dbContext;

	/// <summary>
	/// The notes data service instance used for testing.
	/// </summary>
	private readonly NotesDataService _notesDataService;

	/// <summary>
	/// The user name for the tests.
	/// </summary>
	private readonly string _userName = "TestUser";

	/// <summary>
	/// The other user name for the tests, used to ensure notes are not shared between users.
	/// </summary>
	private readonly string _otherUserName = "OtherUser";

	/// <summary>
	/// The unique database name for the in-memory database used in tests.
	/// </summary>
	private readonly string _databaseName;

	/// <summary>
	/// Initializes a new instance of the <see cref="NotesDataServiceTests"/> class.
	/// </summary>
	public NotesDataServiceTests()
	{
		this._databaseName = Guid.NewGuid().ToString();
		var options = new DbContextOptionsBuilder<SqlDbContext>()
			.UseInMemoryDatabase(databaseName: this._databaseName)
			.Options;
		this._dbContext = new SqlDbContext(options);
		this._notesDataService = new NotesDataService(this._dbContext);
	}

	/// <summary>
	/// Tests the get all notes async method to ensure it returns only active notes for the specified user.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetAllNotesAsync_ReturnsActiveNotesForUser()
	{
		// Arrange
		var notes = TestsHelper.PrepareNotesDataList(this._userName, this._otherUserName);
		await _dbContext.Notes.AddRangeAsync(notes);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await _notesDataService.GetAllNotesAsync(_userName);

		// Assert
		Assert.Single(result);
		Assert.All(result, n => Assert.Equal(_userName, n.UserName));
		Assert.All(result, n => Assert.True(n.IsActive));
	}

	/// <summary>
	/// Tests the get all notes async method to ensure it does not return notes for other users.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetNoteAsync_ReturnsNote_WhenExistsAndActive()
	{
		// Arrange
		var note = TestsHelper.PrepareNoteData(this._userName);
		await _dbContext.Notes.AddAsync(note);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await _notesDataService.GetNoteAsync(10, _userName);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(10, result.NoteId);
		Assert.Equal(_userName, result.UserName);
	}

	/// <summary>
	/// Tests the get note async method to ensure it throws an exception when the note does not exist or is inactive.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task GetNoteAsync_ThrowsException_WhenNoteNotFound()
	{
		// Act & Assert
		var ex = await Assert.ThrowsAsync<Exception>(() => _notesDataService.GetNoteAsync(999, _userName));
		Assert.Equal(ExceptionConstants.NoteNotFoundException, ex.Message);
	}

	/// <summary>
	/// Tests the AddNewNoteAsync method to ensure it adds a new note successfully.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task AddNewNoteAsync_AddsNoteSuccessfully()
	{
		// Arrange
		var noteDto = TestsHelper.PrepareNoteDTO(_userName);

		// Act
		var result = await _notesDataService.AddNewNoteAsync(noteDto);
		var notes = await _dbContext.Notes.ToListAsync();

		// Assert
		Assert.True(result);
		Assert.Single(notes);
		Assert.Equal(_userName, notes[0].UserName);
		Assert.True(notes[0].IsActive);
	}

	/// <summary>
	/// Tests the AddNewNoteAsync method to ensure it throws an exception when the database context is disposed or encounters an error.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task AddNewNoteAsync_ThrowsException_OnDbError()
	{
		// Arrange: Use a disposed context to force an exception
		var options = new DbContextOptionsBuilder<SqlDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
		var context = new SqlDbContext(options);
		var service = new NotesDataService(context);
		context.Dispose();
		var noteDto = TestsHelper.PrepareNoteDTO(_userName);

		// Act & Assert
		await Assert.ThrowsAsync<Exception>(() => service.AddNewNoteAsync(noteDto));
	}

	/// <summary>
	/// Tests the DeleteNoteAsync method to ensure it deletes an active note successfully.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task DeleteNoteAsync_DeletesActiveNote()
	{
		// Arrange
		var note = TestsHelper.PrepareNoteData(this._userName);
		await _dbContext.Notes.AddAsync(note);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await this._notesDataService.DeleteNoteAsync(10, _userName);
		var deletedNote = await this._dbContext.Notes.FindAsync(10);

		// Assert
		Assert.True(result);
		Assert.False(deletedNote!.IsActive);
	}

	/// <summary>
	/// Tests the DeleteNoteAsync method to ensure it throws an exception when the note does not exist.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task DeleteNoteAsync_ThrowsException_WhenNoteNotFound()
	{
		// Act & Assert
		var ex = await Assert.ThrowsAsync<Exception>(() => _notesDataService.DeleteNoteAsync(999, _userName));
		Assert.Equal(ExceptionConstants.NoteNotFoundException, ex.Message);
	}

	/// <summary>
	/// Tests the UpdateNoteAsync method to ensure it updates an existing note successfully.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task UpdateNoteAsync_UpdatesNoteSuccessfully()
	{
		// Arrange
		var note = TestsHelper.PrepareNoteData(this._userName);
		await _dbContext.Notes.AddAsync(note);
		await _dbContext.SaveChangesAsync();
		var updateDto = TestsHelper.PrepareUpdateNoteDataDTO(this._userName);

		// Act
		var updated = await _notesDataService.UpdateNoteAsync(updateDto);

		// Assert
		Assert.NotNull(updated);
		Assert.Equal("NewTitle", updated.NoteTitle);
		Assert.Equal("NewDesc", updated.NoteDescription);
		Assert.Equal(_userName, updated.UserName);
		Assert.False(updated.LastModifiedDate > note.LastModifiedDate);
	}

	/// <summary>
	/// Tests the UpdateNoteAsync method to ensure it throws an exception when the note does not exist.
	/// </summary>
	/// <returns>A task to wait on</returns>
	[Fact]
	public async Task UpdateNoteAsync_ThrowsException_WhenNoteNotFound()
	{
		// Arrange
		var updateDto = TestsHelper.PrepareUpdateNoteDataDTO(this._userName);

		// Act & Assert
		var ex = await Assert.ThrowsAsync<Exception>(() => _notesDataService.UpdateNoteAsync(updateDto));
		Assert.Equal(ExceptionConstants.NoteNotFoundException, ex.Message);
	}
}
