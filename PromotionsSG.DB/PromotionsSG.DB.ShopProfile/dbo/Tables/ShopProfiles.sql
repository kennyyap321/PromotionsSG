CREATE TABLE [dbo].[ShopProfiles]
(
	[ShopProfileId] int identity(1,1) not null primary key,
	[UserId] int not null,
	[ShopName] varchar(255),
	[ShopEmail] varchar (255),
	[ShopAddress] varchar(500)
)
