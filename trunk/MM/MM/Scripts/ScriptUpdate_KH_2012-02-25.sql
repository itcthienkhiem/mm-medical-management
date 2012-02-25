USE MM
GO
/****** Object:  Table [dbo].[Bookmark]    Script Date: 02/25/2012 18:35:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmark](
	[BookmarkGUID] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [int] NOT NULL CONSTRAINT [DF_Bookmark_Type]  DEFAULT ((0)),
 CONSTRAINT [PK_Bookmark] PRIMARY KEY CLUSTERED 
(
	[BookmarkGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
