CREATE TABLE [dbo].[Feedback](
	[FeedbackId] int IDENTITY(1,1),
	[PromotionId] int NULL,
	[ClaimId] int NULL,
	[CustomerId] int NULL,
	[Title] [varchar](500) NULL,
	[Description] [varchar](500) NULL,
	[Rating] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedTime] datetime NULL
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


