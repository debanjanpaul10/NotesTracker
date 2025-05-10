CREATE TABLE [dbo].[Notes]
(
  [NoteId] [int] IDENTITY(1,1) NOT NULL,
  [NoteTitle] [nvarchar](max) NOT NULL,
  [NoteDescription] [nvarchar](max) NOT NULL,
  [CreatedDate] [datetime] NOT NULL,
  [LastModifiedDate] [datetime] NOT NULL,
  [IsActive] [bit] NOT NULL,
  [UserId] [nvarchar](max) NOT NULL,
)
