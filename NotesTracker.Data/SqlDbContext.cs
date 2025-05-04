﻿// *********************************************************************************
//	<copyright file="SqlDbContext.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The SQL DB Context Class.</summary>
// *********************************************************************************

namespace NotesTracker.Data
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using NotesTracker.Data.Entities;
	using static NotesTracker.Shared.Constants.ConfigurationConstants;
	using static NotesTracker.Shared.Constants.DatabaseConstants;

	/// <summary>
	/// The SQL DB Context Class.
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
	public class SqlDbContext : DbContext
	{
		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDbContext"/> class.
		/// </summary>
		/// <remarks>
		/// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
		/// for more information and examples.
		/// </remarks>
		public SqlDbContext()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDbContext"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <param name="configuration">The configuration.</param>
		public SqlDbContext(DbContextOptions<SqlDbContext> options, IConfiguration configuration) : base(options)
		{
			this._configuration = configuration;
		}

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public DbSet<Note> Notes { get; set; }

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Override this method to configure the database (and other options) to be used for this context.
		/// This method is called for each instance of the context that is created.
		/// The base implementation does nothing.
		/// </summary>
		/// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
		/// typically define extension methods on this object that allow you to configure the context.</param>
		/// <remarks>
		/// <para>
		/// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
		/// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
		/// the options have already been set, and skip some or all of the logic in
		/// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
		/// </para>
		/// <para>
		/// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
		/// for more information and examples.
		/// </para>
		/// </remarks>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = this._configuration[SqlConnectionStringConstant];
			optionsBuilder.UseSqlServer(connectionString);
		}

		/// <summary>
		/// Override this method to further configure the model that was discovered by convention from the entity types
		/// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
		/// and re-used for subsequent instances of your derived context.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		/// <remarks>
		/// <para>
		/// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
		/// then this method will not be run. However, it will still run when creating a compiled model.
		/// </para>
		/// <para>
		/// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
		/// examples.
		/// </para>
		/// </remarks>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Note>(entity =>
			{
				entity.ToTable(NotesTableConstant);
				entity.HasKey(e => e.NoteId).HasName(PrimaryKeyNotesConstant);
				entity.Property(e => e.NoteId).HasColumnName(NoteIdColumnNameConstant).HasColumnType(IntegerDataTypeConstant).ValueGeneratedOnAdd();

				entity.Property(e => e.NoteTitle).HasColumnName(NoteTitleColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.NoteDescription).HasColumnName(NoteDescriptionColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.CreatedDate).HasColumnName(CreatedDateColumnNameConstant).HasColumnType(DateTimeDataTypeConstant).IsRequired();
				entity.Property(e => e.LastModifiedDate).HasColumnName(LastModifiedDateColumnNameConstant).HasColumnType(DateTimeDataTypeConstant).IsRequired();
				entity.Property(e => e.IsActive).HasColumnName(IsActiveColumnNameConstant).HasColumnType(BitDataTypeConstant).IsRequired();
				entity.Property(e => e.UserName).HasColumnName(UserNameColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();

			});
			modelBuilder.Entity<User>(entity => {
				entity.ToTable(UsersTableConstant);
				entity.HasKey(e => e.Id).HasName(PrimaryKeyUsersConstant);
				entity.Property(e => e.Id).HasColumnName(IdColumnNameConstant).HasColumnType(IntegerDataTypeConstant).ValueGeneratedOnAdd();

				entity.Property(e => e.UserEmail).HasColumnName(UserEmailColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.UserName).HasColumnName(UserNameColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.UserId).HasColumnName(UserIdColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.Provider).HasColumnName(ProviderColumnNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.IsSocial).HasColumnName(IsSocialColumnNameConstant).HasColumnType(BitDataTypeConstant).IsRequired();
				entity.Property(e => e.IsActive).HasColumnName(IsActiveColumnNameConstant).HasColumnType(BitDataTypeConstant).IsRequired();

			});
		}

	}
}
