CREATE TABLE [dbo].[UserLogin]
(
	[UserId]       VARCHAR (50)  NOT NULL,
    [UserLoginId]    VARCHAR (100) NOT NULL,
    [UserName]     VARCHAR (250) NOT NULL,
    [Password] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
