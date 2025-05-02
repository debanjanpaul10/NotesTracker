CREATE TABLE [dbo].[Users]
(
  [UserId] [int] IDENTITY(1,1) NOT NULL,
  [UserEmail] [nvarchar](max) NOT NULL,
  [UserName] [nvarchar](max) NOT NULL,
  [UserPassword] [nvarchar](max) NOT NULL,
  [IsActive] [bit] NOT NULL,
)
