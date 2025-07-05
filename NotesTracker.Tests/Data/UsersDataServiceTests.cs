using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NotesTracker.Data;
using NotesTracker.Data.Entities;
using NotesTracker.Data.Services;

namespace NotesTracker.Tests.Data;

/// <summary>
/// The Users Data Service Tests class.
/// </summary>
public class UsersDataServiceTests
{
    /// <summary>
    /// The mock db context.
    /// </summary>
    private readonly SqlDbContext _mockDbContext;

    /// <summary>
    /// The mock of logger.
    /// </summary>
    private readonly Mock<ILogger<UsersDataService>> _mockLogger;

    /// <summary>
    /// The users data service.
    /// </summary>
    private readonly UsersDataService _usersDataService;

    /// <summary>
    /// The database name for in-memory database.
    /// </summary>
    private readonly string _databaseName;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersDataServiceTests"/> class.
    /// </summary>
    public UsersDataServiceTests()
    {
        this._databaseName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<SqlDbContext>()
            .UseInMemoryDatabase(databaseName: this._databaseName)
            .Options;
        this._mockDbContext = new SqlDbContext(options);
        this._mockLogger = new Mock<ILogger<UsersDataService>>();

        this._usersDataService = new UsersDataService(this._mockDbContext, this._mockLogger.Object);
    }

    /// <summary>
    /// Tests that SaveUsersDataAsync saves new users successfully.
    /// </summary>
    [Fact]
    public async Task SaveUsersDataAsync_SavesNewUsers_Successfully()
    {
        // Arrange
        var usersData = TestsHelper.PrepareListofGraphUserData();

        // Act
        var result = await this._usersDataService.AddUsersAsync(usersData);
        var savedUsers = await this._mockDbContext.Users.ToListAsync();

        // Assert
        Assert.True(result);
        Assert.Equal(3, savedUsers.Count);
        Assert.All(savedUsers, user => Assert.True(user.IsActive));
        Assert.Contains(savedUsers, u => u.UserName == "userName1");
        Assert.Contains(savedUsers, u => u.UserName == "userName2");
        Assert.Contains(savedUsers, u => u.UserName == "userName3");
    }

    /// <summary>
    /// Tests that SaveUsersDataAsync handles empty user list correctly.
    /// </summary>
    [Fact]
    public async Task SaveUsersDataAsync_EmptyUserList_ReturnsTrue()
    {
        // Arrange
        var usersData = new List<User>();

        // Act
        await this._usersDataService.AddUsersAsync(usersData);
        var savedUsers = await this._mockDbContext.Users.ToListAsync();

        // Assert
        Assert.Empty(savedUsers);
    }
}