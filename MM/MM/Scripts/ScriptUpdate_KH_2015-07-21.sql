USE [MM]
GO
/****** Object:  Table [dbo].[GhiNhanTraNo]    Script Date: 07/22/2015 09:16:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GhiNhanTraNo](
	[GhiNhanTraNoGUID] [uniqueidentifier] NOT NULL,
	[MaPhieuThuGUID] [uniqueidentifier] NOT NULL,
	[NgayTra] [datetime] NOT NULL,
	[SoTien] [float] NOT NULL,
	[LoaiPT] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_GhiNhanTraNo] PRIMARY KEY CLUSTERED 
(
	[GhiNhanTraNoGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GhiNhanTraNo] ADD  CONSTRAINT [DF_GhiNhanTraNo_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  View [dbo].[GhiNhanTraNoView]    Script Date: 07/22/2015 09:16:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GhiNhanTraNoView]
AS
SELECT     dbo.GhiNhanTraNo.GhiNhanTraNoGUID, dbo.GhiNhanTraNo.MaPhieuThuGUID, dbo.GhiNhanTraNo.NgayTra, dbo.GhiNhanTraNo.SoTien, dbo.GhiNhanTraNo.LoaiPT, 
                      dbo.GhiNhanTraNo.CreatedDate, dbo.GhiNhanTraNo.CreatedBy, dbo.GhiNhanTraNo.UpdatedDate, dbo.GhiNhanTraNo.UpdatedBy, dbo.GhiNhanTraNo.DeletedDate, 
                      dbo.GhiNhanTraNo.DeletedBy, dbo.GhiNhanTraNo.Status, DocStaffView_1.FullName AS NguoiTao, dbo.DocStaffView.FullName AS NguoiCapNhat, 
                      dbo.GhiNhanTraNo.GhiChu
FROM         dbo.GhiNhanTraNo LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.GhiNhanTraNo.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.GhiNhanTraNo.CreatedBy = dbo.DocStaffView.DocStaffGUID

GO

INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'0ac61d03-46fb-4aff-a003-0646761f7b71', N'GhiNhanTraNo', N'Ghi nhận trả nợ')

