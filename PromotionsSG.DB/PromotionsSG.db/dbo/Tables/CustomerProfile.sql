CREATE TABLE [dbo].[CustomerProfile] (
    [CustomerProfileId]         VARCHAR (50)  NOT NULL,
    [CustomerName]              VARCHAR (255) NOT NULL,
    [CustomerAddress]          VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_CustomerProfile] PRIMARY KEY CLUSTERED ([CustomerProfileId] ASC)
);

