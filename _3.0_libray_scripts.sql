USE [Library]
GO
/****** Object:  Table [dbo].[tbl_CheckOutBooks]    Script Date: 17-11-2017 11:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CheckOutBooks](
	[BookId] [nvarchar](50) NULL,
	[BookName] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[lastUpdated] [nvarchar](50) NULL,
	[IsActive] [bigint] NULL
) ON [PRIMARY]

GO
nitin is good