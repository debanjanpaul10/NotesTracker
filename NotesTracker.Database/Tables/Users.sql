CREATE TABLE [dbo].[Users]
(
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [UserEmail] NVARCHAR(max) NOT NULL,
  [UserName] NVARCHAR(max) NOT NULL,
  [UserId] NVARCHAR (MAX) NOT NULL,
  [Provider] NVARCHAR(max) NOT NULL,
  [IsSocial] BIT NOT NULL,
  [IsActive] BIT NOT NULL,
)
