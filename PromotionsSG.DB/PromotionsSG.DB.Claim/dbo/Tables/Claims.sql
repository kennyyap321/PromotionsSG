CREATE TABLE [dbo].[Claims]
(
	[ClaimId] int identity(1,1) not null primary key,
	[CustomerProfileId] int not null,
	[PromotionId] int not null,
	[ClaimDate] datetime,
	[Consumed] bit,
	[ConsumeDate] datetime,
	[TotalClaim] int null
)
