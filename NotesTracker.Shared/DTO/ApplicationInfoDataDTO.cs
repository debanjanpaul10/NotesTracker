// *********************************************************************************
//	<copyright file="ApplicationTechnologies.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The application technologies data dto.</summary>
// *********************************************************************************

namespace NotesTracker.Shared.DTO
{
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The application technologies data dto.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ApplicationInfoDataDTO
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the heading.
		/// </summary>
		/// <value>
		/// The heading.
		/// </value>
		[BsonElement("Heading")]
		public string Heading { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[BsonElement("Description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		[BsonElement("Image")]
		public string Image { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		[BsonElement("Link")]
		public string Link { get; set; } = string.Empty;
	}
}


