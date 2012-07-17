USE MM
GO
/****** Object:  Table [dbo].[LoaiSieuAm]    Script Date: 07/17/2012 21:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSieuAm](
	[LoaiSieuAmGUID] [uniqueidentifier] NOT NULL,
	[TenSieuAm] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ThuTu] [int] NOT NULL CONSTRAINT [DF_LoaiSieuAm_ThuTu]  DEFAULT ((0)),
	[Path] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_LoaiSieuAm_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_LoaiSieuAm] PRIMARY KEY CLUSTERED 
(
	[LoaiSieuAmGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MauBaoCao]    Script Date: 07/17/2012 21:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MauBaoCao](
	[MauBaoCaoGUID] [uniqueidentifier] NOT NULL,
	[LoaiSieuAmGUID] [uniqueidentifier] NOT NULL,
	[Template] [image] NOT NULL,
	[DoiTuong] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_MauBaoCao_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_MauBaoCao] PRIMARY KEY CLUSTERED 
(
	[MauBaoCaoGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[MauBaoCao]  WITH CHECK ADD  CONSTRAINT [FK_MauBaoCao_LoaiSieuAm] FOREIGN KEY([LoaiSieuAmGUID])
REFERENCES [dbo].[LoaiSieuAm] ([LoaiSieuAmGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[KetQuaSieuAm]    Script Date: 07/17/2012 21:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaSieuAm](
	[KetQuaSieuAmGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[BacSiSieuAmGUID] [uniqueidentifier] NOT NULL,
	[BacSiChiDinhGUID] [uniqueidentifier] NULL,
	[LoaiSieuAmGUID] [uniqueidentifier] NOT NULL,
	[NgaySieuAm] [datetime] NULL,
	[LamSang] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KetQuaSieuAm] [image] NULL,
	[Hinh1] [image] NULL,
	[Hinh2] [image] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaSieuAm_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaSieuAm] PRIMARY KEY CLUSTERED 
(
	[KetQuaSieuAmGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[KetQuaSieuAm]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaSieuAm_DocStaff] FOREIGN KEY([BacSiSieuAmGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[KetQuaSieuAm]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaSieuAm_LoaiSieuAm] FOREIGN KEY([LoaiSieuAmGUID])
REFERENCES [dbo].[LoaiSieuAm] ([LoaiSieuAmGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KetQuaSieuAm]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaSieuAm_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[KetQuaSieuAmView]    Script Date: 07/17/2012 21:24:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaSieuAmView]
AS
SELECT     dbo.KetQuaSieuAm.KetQuaSieuAmGUID, dbo.KetQuaSieuAm.PatientGUID, dbo.KetQuaSieuAm.BacSiSieuAmGUID, dbo.KetQuaSieuAm.BacSiChiDinhGUID, 
                      dbo.KetQuaSieuAm.LoaiSieuAmGUID, dbo.KetQuaSieuAm.NgaySieuAm, dbo.KetQuaSieuAm.LamSang, dbo.KetQuaSieuAm.KetQuaSieuAm, dbo.KetQuaSieuAm.Hinh1, 
                      dbo.KetQuaSieuAm.Hinh2, dbo.KetQuaSieuAm.CreatedDate, dbo.KetQuaSieuAm.CreatedBy, dbo.KetQuaSieuAm.UpdatedDate, dbo.KetQuaSieuAm.UpdatedBy, 
                      dbo.KetQuaSieuAm.DeletedDate, dbo.KetQuaSieuAm.DeletedBy, dbo.KetQuaSieuAm.Status, dbo.LoaiSieuAm.TenSieuAm, 
                      dbo.LoaiSieuAm.Status AS LoaiSieuAmStatus, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.FileNum, dbo.PatientView.Address, 
                      dbo.PatientView.GenderAsStr, dbo.PatientView.Archived AS PatientArchived, dbo.DocStaffView.FullName AS BacSiSieuAm, 
                      dbo.DocStaffView.Archived AS BacSiSieuAmArchived, DocStaffView_1.FullName AS BacSiChiDinh, DocStaffView_1.Archived AS BacSiChiDinhArchived
FROM         dbo.KetQuaSieuAm INNER JOIN
                      dbo.LoaiSieuAm ON dbo.KetQuaSieuAm.LoaiSieuAmGUID = dbo.LoaiSieuAm.LoaiSieuAmGUID INNER JOIN
                      dbo.PatientView ON dbo.KetQuaSieuAm.PatientGUID = dbo.PatientView.PatientGUID INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaSieuAm.BacSiSieuAmGUID = dbo.DocStaffView.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.KetQuaSieuAm.BacSiChiDinhGUID = DocStaffView_1.DocStaffGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'825be604-69ad-466c-a3bd-639061ea0bb6', N'LoaiSieuAm', N'Loại siêu âm')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'e74ccc76-c7e4-495b-8bf4-137c298e8dd1', N'KetQuaSieuAm', N'Kết quả siêu âm')
