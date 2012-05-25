USE MM
GO
/****** Object:  Table [dbo].[MaxNgayXetNghiem]    Script Date: 05/25/2012 14:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaxNgayXetNghiem](
	[MaxNgayXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[MaxNgayXetNghiem] [datetime] NOT NULL,
	[LoaiXN] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MaxNgayXetNghiem] PRIMARY KEY CLUSTERED 
(
	[MaxNgayXetNghiemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]