CREATE TABLE [dbo].[CustomerProfile](
	[CustomerProfileId] int IDENTITY(1,1),
	[CustomerFullName] [varchar](250) NULL,
	[CustomerAddress] [varchar](500) NULL,
	[CustomerEmail] [varchar](500) NULL,
	[CustomerPhone] [varchar](50) NULL,
	[CustomerType] [varchar](50) NULL,
	[CustomerGender] [varchar](10) NULL,
	[CustomerActive] [bit] NULL,
	[CustomerDOB] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [varchar](50) NULL,
	[LastUpdatedTime] [datetime] NULL,
	[VersionNo] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_CustomerProfile] PRIMARY KEY CLUSTERED 
(
	[CustomerProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


