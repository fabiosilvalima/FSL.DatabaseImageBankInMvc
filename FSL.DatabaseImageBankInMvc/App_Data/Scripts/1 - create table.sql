USE [ImageBankDatabase]
GO

/****** Object:  Table [dbo].[ImageBankFile]    Script Date: 11/16/2016 12:36:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ImageBankFile](
	[FileId] [nvarchar](50) NOT NULL,
	[AliasId] [nvarchar](100) NULL,
	[FileBody] [varbinary](max) NULL,
	[Extension] [nvarchar](5) NULL,
	[ContentType] [nvarchar](50) NULL,
 CONSTRAINT [PK_ImageBankFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

