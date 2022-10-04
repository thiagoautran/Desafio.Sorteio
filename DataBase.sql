CREATE DATABASE DoorPrize;
GO
USE DoorPrize
GO
CREATE TABLE Participant
(
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [varchar](300) NOT NULL,
    [CPF] [bigint] NOT NULL,
    [BirthDate] [datetime] NOT NULL,
    [Income] [decimal] NULL,
    [Quota] [varchar](100) NOT NULL,
    [CID] [varchar](50) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Participant] ADD  CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO