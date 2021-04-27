CREATE TABLE [dbo].[ShopProfiles]
(
	[ShopProfileId] int identity(1,1) not null primary key,
	[UserId] int not null unique,
	[ShopName] varchar(255),
	[ShopAddress] varchar(500),
	[PostalCode] [varchar](500)
)
