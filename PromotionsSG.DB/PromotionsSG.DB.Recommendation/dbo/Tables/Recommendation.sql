CREATE TABLE [dbo].[Recommendation](
	[RecommendationId] int IDENTITY(1,1),
	[CustomerId] int NULL,
	[PromotionId] int NULL,
	[ShopId] int NULL,
	[FeedbackId] int NULL,
	[ClaimId] int NULL,
    [Region] VARCHAR(50) NULL, 	
    [NoOfClicks] INT NULL, 
    [Category] VARCHAR(500) NULL,
    CONSTRAINT [PK_Recommendation] PRIMARY KEY CLUSTERED 
(
	[RecommendationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


