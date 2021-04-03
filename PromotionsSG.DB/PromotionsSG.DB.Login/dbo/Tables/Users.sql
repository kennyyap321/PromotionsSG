

-- Create table
------------------------------
CREATE TABLE [dbo].[Users]
(
	[UserId] int identity(1,1) primary key,
	[UserType] int not null,
	[UserName] varchar(255) not null unique,
	[Password] varchar(255) not null
	--constraint [PK_UserLogin] primary key clustered ([UserName] asc)
)