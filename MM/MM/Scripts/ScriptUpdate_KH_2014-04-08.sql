USE [MM]
GO
ALTER TABLE LoThuoc
ADD [SystemDate] [datetime] NULL
GO
UPDATE LoThuoc
SET SystemDate = CreatedDate










