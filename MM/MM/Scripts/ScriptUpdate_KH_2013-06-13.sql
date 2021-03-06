USE MM
GO
/****** Object:  Table [dbo].[MauHoSo]    Script Date: 06/13/2013 08:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MauHoSo](
	[MauHoSoGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MauHoSo_MauHoSoGUID]  DEFAULT (newid()),
	[TenMauHoSo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Loai] [int] NOT NULL,
 CONSTRAINT [PK_MauHoSo] PRIMARY KEY CLUSTERED 
(
	[MauHoSoGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietMauHoSo]    Script Date: 06/13/2013 08:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietMauHoSo](
	[ChiTietMauHoSoGUID] [uniqueidentifier] NOT NULL,
	[MauHoSoGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ChiTietMauHoSo] PRIMARY KEY CLUSTERED 
(
	[ChiTietMauHoSoGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietMauHoSo]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietMauHoSo_MauHoSo] FOREIGN KEY([MauHoSoGUID])
REFERENCES [dbo].[MauHoSo] ([MauHoSoGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietMauHoSo] CHECK CONSTRAINT [FK_ChiTietMauHoSo_MauHoSo]
GO
/****** Object:  View [dbo].[ChiTietMauHoSoView]    Script Date: 06/13/2013 10:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietMauHoSoView]
AS
SELECT     dbo.ChiTietMauHoSo.*, dbo.Services.Code, dbo.Services.Name
FROM         dbo.ChiTietMauHoSo INNER JOIN
                      dbo.Services ON dbo.ChiTietMauHoSo.ServiceGUID = dbo.Services.ServiceGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'c6d8acd4-ea79-41e2-ace4-fa59f24e6407', N'MapMauHoSoVoiDichVu', N'Map mẫu hồ sơ với dịch vụ')
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('a7f64549-c453-462c-a041-3c19745c6c61', N'LABORATORY REQUEST FORM', 1)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('109ca02c-f1ef-49fd-964c-fcb00c3ab959', N'CHECK LIST', 2)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('666cf25d-1a80-45f3-91af-44c113f6a924', N'GENERAL EXAMINATION REPORT', 3)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('0eaf7857-362f-4806-b6e6-d58509213345', N'ECG FORM', 4)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('02e7b42f-d212-434c-a842-8ee76eccf014', N'X RAY', 5)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('3795b120-77d0-40bf-be05-55bcf92099c1', N'AUDIOMETRY', 6)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('2b0d97aa-4e03-4ea8-a47d-4cced5b4da07', N'SƠ ĐỒ RĂNG', 7)
GO
INSERT INTO [dbo].[MauHoSo]([MauHoSoGUID],[TenMauHoSo],[Loai])
VALUES('b89207b8-771c-4f55-b00e-7e1eada46762', N'TẬT KHÚC XẠ', 8)
GO



