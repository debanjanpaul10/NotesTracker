// *********************************************************************************
// <copyright file="NotesTrackerServiceTests.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>Notes Tracker Service Tests Class.</summary>
// *********************************************************************************

namespace NotesTracker.Tests.Business;

using Microsoft.Extensions.Configuration;
using Moq;
using MongoDB.Driver;
using NotesTracker.Shared.Constants;
using NotesTracker.Business.Contracts;
using AutoMapper;

/// <summary>
/// Notes Tracker Service Tests Class.
/// </summary>
public class NotesTrackerServiceTests
{
	/// <summary>
	/// The user name for the tests.
	/// </summary>
	private static readonly string UserName = "SampleUser";

	/// <summary>
	/// The mock of notes tracker data service.
	/// </summary>
	private readonly Mock<INotesTrackerDataService> _mockNotesTrackerDataService;

	/// <summary>
	/// The mock of mongo client.
	/// </summary>
	private readonly Mock<IMongoClient> _mockMongoClient;

	/// <summary>
	/// The mock of configuration.
	/// </summary>
	private readonly Mock<IConfiguration> _mockConfiguration;

	/// <summary>
	/// The mock of logger.
	/// </summary>
	private readonly Mock<ILogger<NotesTrackerService>> _mockLogger;

	/// <summary>
	/// The mock of cache service.
	/// </summary>
	private readonly Mock<ICacheService> _mockCacheService;

	/// <summary>
	/// The mock of mongo database.
	/// </summary>
	private readonly Mock<IMongoDatabase> _mockMongoDatabase;

	/// <summary>
	/// The mock of auto mapper.
	/// </summary>
	private readonly Mock<IMapper> _mockAutoMapper;

	/// <summary>
	/// The notes tracker service.
	/// </summary>
	private readonly NotesTrackerService _service;

	/// <summary>
	/// Initializes a new instance of <see cref="NotesTrackerServiceTests"/> class.
	/// </summary>
	public NotesTrackerServiceTests()
	{
		this._mockNotesTrackerDataService = new Mock<INotesTrackerDataService>();
		this._mockMongoClient = new Mock<IMongoClient>();
		this._mockConfiguration = new Mock<IConfiguration>();
		this._mockLogger = new Mock<ILogger<NotesTrackerService>>();
		this._mockCacheService = new Mock<ICacheService>();
		this._mockMongoDatabase = new Mock<IMongoDatabase>();
		this._mockAutoMapper = new Mock<IMapper>();

		this._mockConfiguration.Setup(x => x[ConfigurationConstants.MongoDatabaseNameConstant])
			.Returns("TestDb");
		this._mockMongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null))
			.Returns(this._mockMongoDatabase.Object);

		this._service = new NotesTrackerService(
			this._mockNotesTrackerDataService.Object,
			this._mockMongoClient.Object,
			this._mockConfiguration.Object,
			this._mockLogger.Object,
			this._mockCacheService.Object,
			this._mockAutoMapper.Object
		);
	}

	/// <summary>
	/// Tests the get about us data when data is present in cache.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetAboutUsDataAsync_WhenDataInCache_ReturnsCachedData()
	{
		// Arrange
		var cachedList = new List<ApplicationInfoDataDTO> { new ApplicationInfoDataDTO() };
		this._mockCacheService.Setup(x => x.GetCachedData<List<ApplicationInfoDataDTO>>(CacheKeys.AboutUsDataCacheKey))
			.Returns(cachedList);

		// Act
		var result = await this._service.GetAboutUsDataAsync(UserName);

		// Assert
		Assert.Equal(cachedList, result);
		this._mockCacheService.Verify(x => x.GetCachedData<List<ApplicationInfoDataDTO>>(CacheKeys.AboutUsDataCacheKey), Times.Once);
		this._mockMongoDatabase.Verify(x => x.GetCollection<ApplicationInfoDataDTO>(It.IsAny<string>(), null), Times.Never);
	}

	/// <summary>
	/// Tests the get about us data when data is not present in cache and needs to be returned via MongoDB
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetAboutUsDataAsync_WhenDataNotInCache_ReturnsMongoDataAndCaches()
	{
		// Arrange
		var expectedAppInfo = new List<ApplicationInfoDataDTO> { new() { Id = Guid.NewGuid().ToString(), Heading = "Tech1" } };
		var mockAppInfoCollection = new Mock<IMongoCollection<ApplicationInfoDataDTO>>();

		var mockAppInfoCursor = new Mock<IAsyncCursor<ApplicationInfoDataDTO>>();
		mockAppInfoCursor.Setup(x => x.Current).Returns(expectedAppInfo);
		mockAppInfoCursor.Setup(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(false);
		mockAppInfoCursor.Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

		mockAppInfoCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<ApplicationInfoDataDTO>>(), It.IsAny<FindOptions<ApplicationInfoDataDTO, ApplicationInfoDataDTO>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockAppInfoCursor.Object);

		this._mockMongoDatabase.Setup(db => db.GetCollection<ApplicationInfoDataDTO>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockAppInfoCollection.Object);

		ApplicationInfoDataDTO cachedData = null!;
		this._mockCacheService.Setup(x => x.GetCachedData<ApplicationInfoDataDTO>(CacheKeys.AboutUsDataCacheKey)).Returns(() => cachedData);
		this._mockCacheService.Setup(x => x.SetCacheData(CacheKeys.AboutUsDataCacheKey, It.IsAny<ApplicationInfoDataDTO>(), It.IsAny<TimeSpan>()))
			.Callback<string, ApplicationInfoDataDTO, TimeSpan>((key, data, expiry) => cachedData = data);

		// Act
		var result = await this._service.GetAboutUsDataAsync(UserName);

		// Assert
		Assert.NotNull(result);
		this._mockCacheService.Verify(x => x.SetCacheData(CacheKeys.AboutUsDataCacheKey, result, It.IsAny<TimeSpan>()), Times.Once);
		this._mockMongoDatabase.Verify(x => x.GetCollection<ApplicationInfoDataDTO>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
	}

	/// <summary>
	/// Tests the get about us data when collection is null and throws exception.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetAboutUsDataAsync_WhenCollectionIsNull_ThrowsException()
	{
		// Arrange
		this._mockCacheService.Setup(x => x.GetCachedData<List<ApplicationInfoDataDTO>>(CacheKeys.AboutUsDataCacheKey))
			.Returns((List<ApplicationInfoDataDTO>)null!);
		this._mockMongoDatabase.Setup(x => x.GetCollection<ApplicationInfoDataDTO>(NotesTrackerConstants.ApplicationInformationCollectionConstant, null))
			.Returns((IMongoCollection<ApplicationInfoDataDTO>)null!);

		// Act & Assert
		var ex = await Assert.ThrowsAsync<Exception>(() => this._service.GetAboutUsDataAsync(UserName));
		Assert.Equal(ExceptionConstants.SomethingWentWrongMessageConstant, ex.Message);
	}

	/// <summary>
	/// Tests the get about us data when mongo db throws exception.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	[Fact]
	public async Task GetAboutUsDataAsync_WhenMongoThrows_LogsAndRethrows()
	{
		// Arrange
		this._mockCacheService.Setup(x => x.GetCachedData<List<ApplicationInfoDataDTO>>(CacheKeys.AboutUsDataCacheKey))
			.Returns((List<ApplicationInfoDataDTO>)null!);
		this._mockMongoDatabase.Setup(x => x.GetCollection<ApplicationInfoDataDTO>(NotesTrackerConstants.ApplicationInformationCollectionConstant, null))
			.Throws(new InvalidOperationException("Mongo error"));

		// Act & Assert
		await Assert.ThrowsAsync<InvalidOperationException>(() => this._service.GetAboutUsDataAsync(UserName));
		this._mockLogger.Verify(
			x => x.Log(
				LogLevel.Error,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Mongo error")),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.AtLeastOnce);
	}

}
