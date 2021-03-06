USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'fca9d6ce-bfa6-4d5d-91b2-089879b6c8ba', N'BaoCaoSoLuongKham', N'Báo cáo số lượng khám')
GO
/****** Object:  Table [dbo].[NgayBatDauLamMoiSoHoaDon]    Script Date: 05/24/2012 21:55:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgayBatDauLamMoiSoHoaDon](
	[MaNgayBatDauGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_NgayBatDauLamMoiSoHoaDon_MaNgayBatDauGUID]  DEFAULT (newid()),
	[NgayBatDau] [datetime] NOT NULL,
 CONSTRAINT [PK_NgayBatDauLamMoiSoHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaNgayBatDauGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE QuanLySoHoaDon 
ADD [NgayBatDau] [datetime] NULL
GO
INSERT INTO NgayBatDauLamMoiSoHoaDon (MaNgayBatDauGUID, NgayBatDau)
VALUES ('4a409782-8e19-4e6a-af0f-ff457832ce1d', '3/1/2012 12:00:00 AM')
GO
UPDATE QuanLySoHoaDon
SET NgayBatDau = '3/1/2012 12:00:00 AM'
