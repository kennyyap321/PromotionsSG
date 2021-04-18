CREATE TABLE [dbo].[Promotions]
(
	[PromotionId] INT NOT NULL identity(1,1)  primary key,
	[ShopProfileId] int not null ,
	[Type] int not null,
	[Header] varchar(255),
	[Description] varchar(255),
	[StartDate] DateTime,
	[Qty] int not null,
	[EndDate] DateTime,
	[IsActive] bit)
