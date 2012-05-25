using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Drawing;
using SpreadsheetGear;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MMService
{
    public class ExportXetNghiem
    {
        public static void ExportKetQuaXetNghiem(DateTime fromDate, DateTime toDate)
        {
            ExportKetQuaXetNghiemSinhHoa(fromDate, toDate);
            ExportKetQuaXetNghiemCellDyn3200(fromDate, toDate);
        }

        public static void ExportKetQuaXetNghiemCellDyn3200(DateTime fromDate, DateTime toDate)
        {
            IWorkbook workBook = null;

            try
            {
                Result result = KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemCellDyn3200List(fromDate, toDate);
                if (!result.IsOK)
                {
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemCellDyn3200List"));
                    return;
                }

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;

                foreach (DataRow patientRow in dt.Rows)
                {
                    string patientGUID = patientRow["PatientGUID"].ToString();
                    string maBenhNhan = patientRow["FileNum"].ToString();
                    string ngaySinh = patientRow["DobStr"].ToString();
                    string gioiTinh = patientRow["GenderAsStr"].ToString();
                    string tenBenhNhan = patientRow["FullName"].ToString();
                    string diaChi = patientRow["Address"].ToString();

                    result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemCellDyn3200List(fromDate, toDate, patientGUID, ngaySinh, gioiTinh);
                    if (!result.IsOK)
                    {
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemCellDyn3200List"));
                        continue;
                    }

                    DataTable dtKQXN = result.QueryResult as DataTable;
                    if (dtKQXN == null || dtKQXN.Rows.Count <= 0) continue;

                    string excelTemplateName = string.Format("{0}\\Templates\\KetQuaXetNghiemCellDyn3200Template.xls", AppDomain.CurrentDomain.BaseDirectory);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];
                    workSheet.Cells["B2"].Value = string.Format("Mã bệnh nhân: {0}", maBenhNhan);
                    workSheet.Cells["B3"].Value = string.Format("Họ tên: {0}", tenBenhNhan);
                    workSheet.Cells["B4"].Value = string.Format("Ngày sinh: {0}", ngaySinh);
                    workSheet.Cells["D4"].Value = string.Format("Giới tính: {0}", gioiTinh);
                    workSheet.Cells["B5"].Value = string.Format("Địa chỉ: {0}", diaChi);

                    int rowIndex = 8;
                    IRange range;
                    
                    DateTime maxNgayXN = DateTime.MinValue;
                    foreach (DataRow row in dtKQXN.Rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                        if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                        string tenXetNghiem = row["Fullname"].ToString();
                        double testResult = Convert.ToDouble(row["TestResult"]);
                        string testPercent = row["TestPercent"].ToString();
                        int gID = Convert.ToInt32(row["GroupID"]);
                        byte tinhTrang = Convert.ToByte(row["TinhTrang"]);
                        string binhThuong = row["BinhThuong"].ToString();

                        workSheet.Cells[rowIndex, 0].Value = tenXetNghiem;
                        workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Left;
                        if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 0].Font.Bold = true;
                        workSheet.Cells[rowIndex, 1].Value = testResult;
                        workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;
                        if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 1].Font.Bold = true;
                        if (testPercent.Trim() != string.Empty)
                            workSheet.Cells[rowIndex, 2].Value = Convert.ToDouble(testPercent);
                        workSheet.Cells[rowIndex, 2].HorizontalAlignment = HAlign.Center;
                        if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 2].Font.Bold = true;
                        workSheet.Cells[rowIndex, 3].Value = binhThuong;
                        workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;
                        if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 3].Font.Bold = true;

                        range = workSheet.Cells[string.Format("A{0}:D{0}", rowIndex + 1)];
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Color = Color.Black;

                        rowIndex++;
                    }

                    range = workSheet.Cells[string.Format("D{0}", rowIndex + 2)];
                    range.Value = string.Format("Ngày xét nghiệm: {0}", maxNgayXN.ToString("dd/MM/yyyy"));
                    range.Font.Italic = true;
                    range.HorizontalAlignment = HAlign.Center;

                    string exportFileName = string.Format("{0}\\FTPUpload\\{1}_{2}_{3}.xls", AppDomain.CurrentDomain.BaseDirectory,
                        maBenhNhan, maxNgayXN.ToString("dd_MM_yyyy_HH_mm_ss"), "CellDyn3200");

                    result = MaxNgayXetNghiemBus.CheckMaxNgayXNExist(patientGUID, "CellDyn3200", maxNgayXN);
                    if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                    {
                        Utility.WriteToTraceLog(result.GetErrorAsString("MaxNgayXetNghiemBus.CheckMaxNgayXNExist"));
                        continue;
                    }
                    else if (result.Error.Code == ErrorCode.EXIST)
                        continue;
                    else
                    {
                        result = MaxNgayXetNghiemBus.InsertMaxNgayXN(patientGUID, "CellDyn3200", maxNgayXN);
                        if (!result.IsOK)
                        {
                            Utility.WriteToTraceLog(result.GetErrorAsString("MaxNgayXetNghiemBus.InsertMaxNgayXN"));
                            continue;
                        }
                    }

                    workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteToTraceLog(ex.Message);
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }
        }

        public static void ExportKetQuaXetNghiemSinhHoa(DateTime fromDate, DateTime toDate)
        {
            IWorkbook workBook = null;

            try
            {
                Result result = KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemSinhHoaList(fromDate, toDate);
                if (!result.IsOK)
                {
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemSinhHoaList"));
                    return;
                }

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;

                foreach (DataRow patientRow in dt.Rows)
                {
                    string patientGUID = patientRow["PatientGUID"].ToString();
                    string ngaySinh = patientRow["DobStr"].ToString();
                    string gioiTinh = patientRow["GenderAsStr"].ToString();
                    string maBenhNhan = patientRow["FileNum"].ToString();
                    string tenBenhNhan = patientRow["FullName"].ToString();
                    string diaChi = patientRow["Address"].ToString();

                    result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemSinhHoaList(fromDate, toDate, patientGUID, ngaySinh, gioiTinh);
                    if (!result.IsOK)
                    {
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemSinhHoaList"));
                        continue;
                    }

                    DataTable dtKQXN = result.QueryResult as DataTable;
                    if (dtKQXN == null || dtKQXN.Rows.Count <= 0) continue;

                    string excelTemplateName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];
                    workSheet.Cells["A2"].Value = string.Format("                      Mã bệnh nhân: {0}", maBenhNhan);
                    workSheet.Cells["A3"].Value = string.Format("                      Họ tên: {0}", tenBenhNhan);
                    workSheet.Cells["A4"].Value = string.Format("                      Ngày sinh: {0}", ngaySinh);
                    workSheet.Cells["C4"].Value = string.Format("                      Giới tính: {0}", gioiTinh);
                    workSheet.Cells["A5"].Value = string.Format("                      Địa chỉ: {0}", diaChi);

                    int rowIndex = 8;
                    IRange range;

                    DataRow[] rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.Biochemistry.ToString()), "Fullname");
                    DateTime maxNgayXN = DateTime.MinValue;
                    if (rows != null && rows.Length > 0)
                    {
                        foreach (DataRow row in rows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            bool isNumeric = false;
                            double testResult = 0;
                            try
                            {
                                testResult = Convert.ToDouble(row["TestResult"]);
                                isNumeric = true;
                            }
                            catch { }

                            string tenXetNghiem = row["Fullname"].ToString();

                            byte tinhTrang = Convert.ToByte(row["TinhTrang"]);
                            string binhThuong = row["BinhThuong"].ToString();

                            workSheet.Cells[rowIndex, 0].Value = tenXetNghiem;
                            workSheet.Cells[rowIndex, 0].HorizontalAlignment = HAlign.Left;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 0].Font.Bold = true;
                            if (isNumeric)
                                workSheet.Cells[rowIndex, 1].Value = testResult;
                            else
                                workSheet.Cells[rowIndex, 1].Value = row["TestResult"].ToString();

                            workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 1].Font.Bold = true;
                            workSheet.Cells[rowIndex, 2].Value = binhThuong;
                            workSheet.Cells[rowIndex, 2].HorizontalAlignment = HAlign.Right;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 2].Font.Bold = true;

                            range = workSheet.Cells[string.Format("A{0}:C{0}", rowIndex + 1)];
                            range.Borders.LineStyle = LineStyle.Continuous;
                            range.Borders.Color = Color.Black;

                            rowIndex++;
                        }
                    }

                    workSheet.Cells[rowIndex, 0].Value = "URINE (NƯỚC TIỂU)";
                    workSheet.Cells[rowIndex, 0].RowHeight = 26.25;
                    workSheet.Cells[rowIndex, 0].VerticalAlignment = VAlign.Center;
                    range = workSheet.Cells[string.Format("A{0}:C{0}", rowIndex + 1)];
                    range.Merge();
                    range.Font.Bold = true;
                    rowIndex++;
                    workSheet.Cells[rowIndex, 0].Value = "TEST RESULT";
                    workSheet.Cells[rowIndex, 1].Value = "RESULT";
                    workSheet.Cells[rowIndex, 2].Value = "NORMAL";
                    range = workSheet.Cells[string.Format("A{0}:C{0}", rowIndex + 1)];
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Color = Color.Black;
                    rowIndex++;

                    rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.Urine.ToString()), "Fullname");
                    if (rows != null && rows.Length > 0)
                    {
                        foreach (DataRow row in rows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            bool isNumeric = false;
                            double testResult = 0;
                            try
                            {
                                testResult = Convert.ToDouble(row["TestResult"]);
                                isNumeric = true;
                            }
                            catch { }

                            string tenXetNghiem = row["Fullname"].ToString();
                            byte tinhTrang = Convert.ToByte(row["TinhTrang"]);
                            string binhThuong = row["BinhThuong"].ToString();

                            workSheet.Cells[rowIndex, 0].Value = tenXetNghiem;
                            workSheet.Cells[rowIndex, 0].HorizontalAlignment = HAlign.Left;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 0].Font.Bold = true;
                            if (isNumeric)
                                workSheet.Cells[rowIndex, 1].Value = testResult;
                            else
                                workSheet.Cells[rowIndex, 1].Value = row["TestResult"].ToString();

                            workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 1].Font.Bold = true;
                            workSheet.Cells[rowIndex, 2].Value = binhThuong;
                            workSheet.Cells[rowIndex, 2].HorizontalAlignment = HAlign.Right;
                            if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 2].Font.Bold = true;

                            range = workSheet.Cells[string.Format("A{0}:C{0}", rowIndex + 1)];
                            range.Borders.LineStyle = LineStyle.Continuous;
                            range.Borders.Color = Color.Black;

                            rowIndex++;
                        }
                    }

                    range = workSheet.Cells[string.Format("C{0}", rowIndex + 2)];
                    range.Value = string.Format("Ngày xét nghiệm: {0}", maxNgayXN.ToString("dd/MM/yyyy"));
                    range.Font.Italic = true;
                    range.HorizontalAlignment = HAlign.Center;

                    string exportFileName = string.Format("{0}\\FTPUpload\\{1}_{2}_{3}.xls", AppDomain.CurrentDomain.BaseDirectory, 
                        maBenhNhan, maxNgayXN.ToString("dd_MM_yyyy_HH_mm_ss"), "SinhHoa");

                    result = MaxNgayXetNghiemBus.CheckMaxNgayXNExist(patientGUID, "Hitachi917", maxNgayXN);
                    if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                    {
                        Utility.WriteToTraceLog(result.GetErrorAsString("MaxNgayXetNghiemBus.CheckMaxNgayXNExist"));
                        continue;
                    }
                    else if (result.Error.Code == ErrorCode.EXIST)
                        continue;
                    else
                    {
                        result = MaxNgayXetNghiemBus.InsertMaxNgayXN(patientGUID, "Hitachi917", maxNgayXN);
                        if (!result.IsOK)
                        {
                            Utility.WriteToTraceLog(result.GetErrorAsString("MaxNgayXetNghiemBus.InsertMaxNgayXN"));
                            continue;
                        }
                    }

                    workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteToTraceLog(ex.Message);
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }
        }
    }
}
