USE MM
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 10/22/2012 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[ThongBaoGUID] [uniqueidentifier] NOT NULL,
	[ThongBaoBuff] [image] NULL,
	[NgayDuyet1] [datetime] NULL,
	[ThongBaoBuff1] [image] NULL,
	[NgayDuyet2] [datetime] NULL,
	[ThongBaoBuff2] [image] NULL,
	[NgayDuyet3] [datetime] NULL,
	[ThongBaoBuff3] [image] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ThongBao_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ThongBao] PRIMARY KEY CLUSTERED 
(
	[ThongBaoGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'800cc1a4-a838-4331-9f4d-31936f4b2e43', N'ThongBao', N'Thông báo')











