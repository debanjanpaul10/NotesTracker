// *********************************************************************************
//	<copyright file="UsersServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users Service Tests Class.</summary>
// *********************************************************************************

namespace NotesTracker.Tests.Business;

/// <summary>
/// Users Service Tests Class.
/// </summary>
public class UsersServiceTests
{
	/// <summary>
	/// The mock users data service
	/// </summary>
	private readonly Mock<IUsersDataService> _mockUsersDataService;

	/// <summary>
	/// The user service
	/// </summary>
	private readonly UsersService _userService;

	public UsersServiceTests()
	{
		this._mockUsersDataService = new Mock<IUsersDataService>();
		this._userService = new UsersService(this._mockUsersDataService.Object);
	}

	/// <summary>
	/// Adds the users asynchronous maps and calls data service with correct users.
	/// </summary>
	[Fact]
	public async Task AddUsersAsync_MapsAndCallsDataService_WithCorrectUsers()
	{
		// Arrange
		var mockUsersData = TestsHelper.PrepareUsersDataListDTO();
		this._mockUsersDataService.Setup(x => x.AddUsersAsync(It.IsAny<List<User>>())).ReturnsAsync(true);

		// Act
		await this._userService.AddUsersAsync(mockUsersData);

		// Assert
		this._mockUsersDataService.Verify(x => x.AddUsersAsync(It.IsAny<List<User>>()), Times.Once);
	}


}

