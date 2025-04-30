CREATE TABLE [dbo].[Notes]
(
  [NoteId] [int] IDENTITY(1,1) NOT NULL,
  [NoteTitle] [nvarchar](max) NOT NULL,
  [NoteDescription] [nvarchar](max) NOT NULL,
  [CreatedDate] [datetime] NOT NULL,
  [LastModifiedDate] [datetime] NOT NULL,
  [IsActive] [bit] NOT NULL,
  [UserName] [nvarchar](max) NOT NULL,
  CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Notes_Users] FOREIGN KEY ([UserName]) REFERENCES [dbo].[Users]([UserName])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
