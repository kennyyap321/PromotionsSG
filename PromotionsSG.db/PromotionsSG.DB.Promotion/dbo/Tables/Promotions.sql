CREATE TABLE [dbo].[Promotions]
(
	[PromotionId] INT NOT NULL identity(1,1)  primary key,
	[ShopProfileId] int not null ,
	[Type] int not null,
	[Header] varchar(255),
	[Description] varchar(255),
	[StartDate] DateTime,
	[EndDate] DateTime,
	[Qty] int not null,
	[Region] varchar(255) not null,
	[IsActive] bit)
