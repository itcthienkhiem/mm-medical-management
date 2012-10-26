using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using SpreadsheetGear;
using SpreadsheetGear.Shapes;

namespace MM.Exports
{
    public class ExportExcel
    {
        public static bool ExportReceiptToExcel(string exportFileName, string receiptGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = ReceiptBus.GetReceipt(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceipt"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceipt"));
                    return false;
                }

                ReceiptView receipt = result.QueryResult as ReceiptView;
                if (receipt == null) return false;

                result = ReceiptBus.GetReceiptDetailList(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = string.Format("Số: {0}", receipt.ReceiptCode);
                workSheet.Cells["B5"].Value = string.Format("Người nộp tiền: {0} - {1}", receipt.FullName, receipt.FileNum);
                workSheet.Cells["B6"].Value = string.Format("Ngày: {0} - Lý do giảm: {1}", 
                    receipt.ReceiptDate.ToString("dd/MM/yyyy HH:mm:ss"), receipt.LyDoGiam);
                if (receipt.Address != null) workSheet.Cells["B7"].Value = string.Format("Địa chỉ: {0}", receipt.Address);
                else workSheet.Cells["B7"].Value = "Địa chỉ:";

                int rowIndex = 9;
                int no = 1;
                double totalPrice = 0;
                IRange range;

                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    double price = Convert.ToDouble(row["Price"]);
                    double disCount = Convert.ToDouble(row["Discount"]);
                    double amount = Convert.ToDouble(row["Amount"]);
                    totalPrice += amount;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = serviceName;

                    workSheet.Cells[rowIndex, 3].Value = 1;
                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (price > 0)
                        workSheet.Cells[rowIndex, 4].Value = price.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = price.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (disCount > 0)
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (amount > 0)
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice));
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
                
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportPhieuThuHopDongToExcel(string exportFileName, string phieuThuHopDongGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = PhieuThuHopDongBus.GetPhieuThuHopDong(phieuThuHopDongGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDong"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDong"));
                    return false;
                }

                PhieuThuHopDong receipt = result.QueryResult as PhieuThuHopDong;
                if (receipt == null) return false;

                result = PhieuThuHopDongBus.GetChiTietPhieuThuHopDong(phieuThuHopDongGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuHopDongBus.GetChiTietPhieuThuHopDong"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetChiTietPhieuThuHopDong"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = string.Format("Số: {0}", receipt.MaPhieuThuHopDong);
                workSheet.Cells["B5"].Value = string.Format("Người nộp tiền: {0}", receipt.TenNguoiNop);
                workSheet.Cells["B6"].Value = string.Format("Ngày: {0}", receipt.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"));
                if (receipt.DiaChi != null) workSheet.Cells["B7"].Value = string.Format("Địa chỉ: {0}", receipt.DiaChi);
                else workSheet.Cells["B7"].Value = "Địa chỉ:";

                int rowIndex = 9;
                int no = 1;
                double totalPrice = 0;
                IRange range;

                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string serviceName = row["DichVu"].ToString();
                    double price = Convert.ToDouble(row["DonGia"]);
                    double disCount = Convert.ToDouble(row["Giam"]);
                    double amount = Convert.ToDouble(row["ThanhTien"]);
                    totalPrice += amount;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = serviceName;

                    workSheet.Cells[rowIndex, 3].Value = 1;
                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (price > 0)
                        workSheet.Cells[rowIndex, 4].Value = price.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = price.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (disCount > 0)
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = disCount.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (amount > 0)
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = amount.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice));
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:G{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }


        public static bool ExportSymptomToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\SymptomTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 1;

                foreach (DataRow row in checkedRows)
                {
                    string symptom = row["SymptomName"].ToString();
                    string advice = row["Advice"].ToString();
                    workSheet.Cells[rowIndex, 0].Value = rowIndex;
                    workSheet.Cells[rowIndex, 1].Value = symptom.Replace("\r", "").Replace("\t", "");
                    workSheet.Cells[rowIndex, 2].Value = advice.Replace("\r", "").Replace("\t", "");
                    rowIndex++;
                }

                IRange range = workSheet.Cells[string.Format("A2:C{0}", checkedRows.Count + 1)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A2:A{0}", checkedRows.Count + 1)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportInvoiceToExcel(string exportFileName, string invoiceGUID, string lien)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = InvoiceBus.GetInvoice(invoiceGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoice"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoice"));
                    return false;
                }

                InvoiceView invoice = result.QueryResult as InvoiceView;
                if (invoice == null) return false;

                result = InvoiceBus.GetInvoiceDetail(invoice.InvoiceGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceDetail"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceDetail"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);
                IRange range = null;
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E1"].Value = string.Format("          Mẫu số: {0}", invoice.MauSo);
                workSheet.Cells["E2"].Value = string.Format("          Kí hiệu: {0}", invoice.KiHieu);
                workSheet.Cells["E3"].Value = string.Format("          Số: {0}", invoice.InvoiceCode);

                DateTime dt = invoice.InvoiceDate;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);

                if (invoice.TenNguoiMuaHang != null && invoice.TenNguoiMuaHang != string.Empty)
                    workSheet.Cells["A10"].Value = string.Format("  Họ tên người mua hàng: {0}", invoice.TenNguoiMuaHang);
                else
                    workSheet.Cells["A10"].Value = string.Format("  Họ tên người mua hàng: {0}", string.Empty);

                range = workSheet.Cells["A11:F11"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;

                workSheet.Cells["A11"].Value = string.Format("  Tên đơn vị: {0}", invoice.TenDonVi);
                Font font = new Font("Times New Roman", 12);
                Size size = TextRenderer.MeasureText(string.Format("  Tên đơn vị: {0}", invoice.TenDonVi), font);
                double w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A11"].RowHeight = 34.50;
                                
                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A12"].Value = string.Format("  Mã số thuế: {0}", invoice.MaSoThue);

                range = workSheet.Cells["A13:F13"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;
                
                string diaChi = string.Empty;
                if (invoice.DiaChi != null) diaChi = invoice.DiaChi;
                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", diaChi);
                size = TextRenderer.MeasureText(string.Format("  Địa chỉ: {0}", diaChi), font);
                w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A13"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", invoice.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", invoice.HinhThucThanhToanStr);

                
                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A19"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 18;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["TenDichVu"].ToString();
                    string donViTinh = row["DonViTinh"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    int donGia = Convert.ToInt32(row["DonGia"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    totalPrice += thanhTien;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = no++;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;
                    range.HorizontalAlignment = HAlign.Left;
                    range.Font.Bold = false;
                    range.WrapText = true;

                    int div = serviceName.Length / 32;
                    int mod = serviceName.Length % 32;
                    if (mod != 0) div++;
                    range.RowHeight = 15 * div;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    range.NumberFormat = "@";
                    if (donGia > 0)
                        range.Value = Utility.ToStringFormat(donGia);//donGia.ToString("#.###");
                    else
                        range.Value = donGia.ToString();

                    
                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    range.NumberFormat = "@";
                    if (thanhTien > 0)
                        range.Value = Utility.ToStringFormat(thanhTien);//thanhTien.ToString("#.###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                if (totalPrice > 0)
                    range.Value = Utility.ToStringFormat(totalPrice);//totalPrice.ToString("#.###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                if (invoice.VAT > 0)
                    range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", invoice.VAT);
                else
                    range.Value = string.Format("  Thuế suất GTGT: .....%, Tiền thuế GTGT:", invoice.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double vat = (invoice.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = Utility.ToStringFormat(vat); //vat.ToString("#.###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = Utility.ToStringFormat(totalPayment);//totalPayment.ToString("#.###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportHoaDonThuocToExcel(string exportFileName, string hoaDonThuocGUID, string lien)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = HoaDonThuocBus.GetHoaDonThuoc(hoaDonThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonThuocBus.GetHoaDonThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonThuocBus.GetHoaDonThuoc"));
                    return false;
                }

                HoaDonThuocView hdt = result.QueryResult as HoaDonThuocView;
                if (hdt == null) return false;

                result = HoaDonThuocBus.GetChiTietHoaDonThuoc(hdt.HoaDonThuocGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonThuocBus.GetChiTietHoaDonThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonThuocBus.GetChiTietHoaDonThuoc"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);
                IRange range = null;
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E1"].Value = string.Format("          Mẫu số: {0}", hdt.MauSo);
                workSheet.Cells["E2"].Value = string.Format("          Kí hiệu: {0}", hdt.KiHieu);
                workSheet.Cells["E3"].Value = string.Format("          Số: {0}", hdt.SoHoaDon);
                
                DateTime dt = hdt.NgayXuatHoaDon.Value;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                workSheet.Cells["A10"].Value = string.Format("  Họ tên người mua hàng: {0}", hdt.TenNguoiMuaHang);

                range = workSheet.Cells["A11:F11"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;

                workSheet.Cells["A11"].Value = string.Format("  Tên đơn vị: {0}", hdt.TenDonVi);
                Font font = new Font("Times New Roman", 12);
                Size size = TextRenderer.MeasureText(string.Format("  Tên đơn vị: {0}", hdt.TenDonVi), font);
                double w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A11"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A12"].Value = string.Format("  Mã số thuế: {0}", hdt.MaSoThue);

                range = workSheet.Cells["A13:F13"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;

                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", hdt.DiaChi);
                size = TextRenderer.MeasureText(string.Format("  Địa chỉ: {0}", hdt.DiaChi), font);
                w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A13"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", hdt.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", hdt.HinhThucThanhToanStr);

                
                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A19"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 18;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["TenThuoc"].ToString();
                    string donViTinh = row["DonViTinh"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    int donGia = Convert.ToInt32(Math.Round(thanhTien / soLuong, 0));//Convert.ToInt32(row["DonGia"]);
                    totalPrice += thanhTien;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = no++;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;
                    range.HorizontalAlignment = HAlign.Left;
                    range.Font.Bold = false;
                    range.WrapText = true;
                    int div = serviceName.Length / 32;
                    int mod = serviceName.Length % 32;
                    if (mod != 0) div++;
                    range.RowHeight = 15 * div;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    range.NumberFormat = "@";
                    if (donGia > 0)
                        range.Value = Utility.ToStringFormat(donGia);//donGia.ToString("#,###");
                    else
                        range.Value = donGia.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    range.NumberFormat = "@";
                    if (thanhTien > 0)
                        range.Value = Utility.ToStringFormat(thanhTien);//thanhTien.ToString("#,###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                if (totalPrice > 0)
                    range.Value = Utility.ToStringFormat(totalPrice);//totalPrice.ToString("#,###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                if (hdt.VAT > 0)
                    range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", hdt.VAT);
                else
                    range.Value = string.Format("  Thuế suất GTGT: .....%, Tiền thuế GTGT:", hdt.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double vat = (hdt.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = Utility.ToStringFormat(vat);//vat.ToString("#,###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = Utility.ToStringFormat(totalPayment);//totalPayment.ToString("#,###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportHoaDonHopDongToExcel(string exportFileName, string hoaDonHopDongGUID, string lien)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = HoaDonHopDongBus.GetHoaDonHopDong(hoaDonHopDongGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonHopDongBus.GetHoaDonHopDong"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonHopDongBus.GetHoaDonHopDong"));
                    return false;
                }

                HoaDonHopDongView hdt = result.QueryResult as HoaDonHopDongView;
                if (hdt == null) return false;

                result = HoaDonHopDongBus.GetChiTietHoaDonHopDong(hdt.HoaDonHopDongGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonHopDongBus.GetChiTietHoaDonHopDong"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonHopDongBus.GetChiTietHoaDonHopDong"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);
                IRange range = null;
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E1"].Value = string.Format("          Mẫu số: {0}", hdt.MauSo);
                workSheet.Cells["E2"].Value = string.Format("          Kí hiệu: {0}", hdt.KiHieu);
                workSheet.Cells["E3"].Value = string.Format("          Số: {0}", hdt.SoHoaDon);

                DateTime dt = hdt.NgayXuatHoaDon.Value;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                workSheet.Cells["A10"].Value = string.Format("  Họ tên người mua hàng: {0}", hdt.TenNguoiMuaHang);

                range = workSheet.Cells["A11:F11"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;

                workSheet.Cells["A11"].Value = string.Format("  Tên đơn vị: {0}", hdt.TenDonVi);
                Font font = new Font("Times New Roman", 12);
                Size size = TextRenderer.MeasureText(string.Format("  Tên đơn vị: {0}", hdt.TenDonVi), font);
                double w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A11"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A12"].Value = string.Format("  Mã số thuế: {0}", hdt.MaSoThue);

                range = workSheet.Cells["A13:F13"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;
                
                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", hdt.DiaChi);
                size = TextRenderer.MeasureText(string.Format("  Địa chỉ: {0}", hdt.DiaChi), font);
                w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A13"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", hdt.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", hdt.HinhThucThanhToanStr);

                
                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A19"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 18;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["TenMatHang"].ToString();
                    string donViTinh = row["DonViTinh"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    int donGia = Convert.ToInt32(row["DonGia"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    totalPrice += thanhTien;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = no++;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;
                    range.HorizontalAlignment = HAlign.Left;
                    range.Font.Bold = false;
                    range.WrapText = true;
                    int div = serviceName.Length / 32;
                    int mod = serviceName.Length % 32;
                    if (mod != 0) div++;
                    range.RowHeight = 15 * div;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    range.NumberFormat = "@";
                    if (donGia > 0)
                        range.Value = Utility.ToStringFormat(donGia);//donGia.ToString("#,###");
                    else
                        range.Value = donGia.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    range.NumberFormat = "@";
                    if (thanhTien > 0)
                        range.Value = Utility.ToStringFormat(thanhTien);//thanhTien.ToString("#,###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                if (totalPrice > 0)
                    range.Value = Utility.ToStringFormat(totalPrice);//totalPrice.ToString("#,###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                if (hdt.VAT > 0)
                    range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", hdt.VAT);
                else
                    range.Value = string.Format("  Thuế suất GTGT: .....%, Tiền thuế GTGT:", hdt.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double vat = (hdt.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = Utility.ToStringFormat(vat);//vat.ToString("#,###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = Utility.ToStringFormat(totalPayment);//totalPayment.ToString("#,###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportHoaDonXuatTruocToExcel(string exportFileName, string hoaDonXuatTruocGUID, string lien)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = HoaDonXuatTruocBus.GetHoaDonXuatTruoc(hoaDonXuatTruocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruoc"));
                    return false;
                }

                HoaDonXuatTruocView hdt = result.QueryResult as HoaDonXuatTruocView;
                if (hdt == null) return false;

                result = HoaDonXuatTruocBus.GetChiTietHoaDonXuatTruoc(hdt.HoaDonXuatTruocGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetChiTietHoaDonXuatTruoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetChiTietHoaDonXuatTruoc"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);
                IRange range = null;
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E1"].Value = string.Format("          Mẫu số: {0}", hdt.MauSo);
                workSheet.Cells["E2"].Value = string.Format("          Kí hiệu: {0}", hdt.KiHieu);
                workSheet.Cells["E3"].Value = string.Format("          Số: {0}", hdt.SoHoaDon);

                DateTime dt = hdt.NgayXuatHoaDon;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                workSheet.Cells["A10"].Value = string.Format("  Họ tên người mua hàng: {0}", hdt.TenNguoiMuaHang);

                range = workSheet.Cells["A11:F11"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;
                
                workSheet.Cells["A11"].Value = string.Format("  Tên đơn vị: {0}", hdt.TenDonVi);
                Font font = new Font("Times New Roman", 12);
                Size size = TextRenderer.MeasureText(string.Format("  Tên đơn vị: {0}", hdt.TenDonVi), font);
                double w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A11"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A12"].Value = string.Format("  Mã số thuế: {0}", hdt.MaSoThue);

                range = workSheet.Cells["A13:F13"];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.WrapText = true;
                
                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", hdt.DiaChi);
                size = TextRenderer.MeasureText(string.Format("  Địa chỉ: {0}", hdt.DiaChi), font);
                w = range.Width * 72 / 60;
                if (size.Width > w) workSheet.Cells["A13"].RowHeight = 34.50;

                range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", hdt.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", hdt.HinhThucThanhToanStr);

                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A19"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 18;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["TenMatHang"].ToString();
                    string donViTinh = row["DonViTinh"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    int donGia = Convert.ToInt32(row["DonGia"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    totalPrice += thanhTien;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = no++;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;
                    range.HorizontalAlignment = HAlign.Left;
                    range.Font.Bold = false;
                    range.WrapText = true;
                    int div = serviceName.Length / 32;
                    int mod = serviceName.Length % 32;
                    if (mod != 0) div++;
                    range.RowHeight = 15 * div;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    range.NumberFormat = "@";
                    if (donGia > 0)
                        range.Value = Utility.ToStringFormat(donGia);//donGia.ToString("#,###");
                    else
                        range.Value = donGia.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    range.NumberFormat = "@";
                    if (thanhTien > 0)
                        range.Value = Utility.ToStringFormat(thanhTien);//thanhTien.ToString("#,###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                if (totalPrice > 0)
                    range.Value = Utility.ToStringFormat(totalPrice);//totalPrice.ToString("#,###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                if (hdt.VAT > 0)
                    range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", hdt.VAT);
                else
                    range.Value = string.Format("  Thuế suất GTGT: .....%, Tiền thuế GTGT:", hdt.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double vat = (hdt.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = Utility.ToStringFormat(vat);//vat.ToString("#,###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.NumberFormat = "@";
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = Utility.ToStringFormat(totalPayment);//totalPayment.ToString("#,###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportToaThuocToExcel(string exportFileName, string toaThuocGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Empty;
            IWorkbook workBook = null;

            try
            {
                Result result = KeToaBus.GetToaThuoc(toaThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuoc"));
                    return false;
                }

                ToaThuocView toaThuoc = result.QueryResult as ToaThuocView;
                if (toaThuoc == null) return false;

                result = KeToaBus.GetChiTietToaThuocList(toaThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
                    return false;
                }

                

                if (toaThuoc.Loai == (byte)LoaiToaThuoc.SanKhoa)
                {
                    excelTemplateName = string.Format("{0}\\Templates\\ToaThuocSanKhoaTemplate.xls", Application.StartupPath);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    IRange range = workSheet.Cells["A2"];
                    range.Value = string.Format("Full name: {0}", toaThuoc.TenBenhNhan);
                    
                    range = workSheet.Cells["C2"];
                    range.Value = string.Format("SEX: {0}", toaThuoc.GenderAsStr);

                    range = workSheet.Cells["D2"];
                    range.Value = string.Format("              D.O.B: {0}", toaThuoc.DobStr);

                    range = workSheet.Cells["A4"];
                    range.Value = string.Format("Address: {0}", toaThuoc.Address);

                    range = workSheet.Cells["D4"];
                    range.Value = string.Format("              Tel: {0}", toaThuoc.Mobile);

                    range = workSheet.Cells["A6"];
                    range.Value = string.Format("Clinical diagnosis: {0}", toaThuoc.ChanDoan.Replace("\r", "").Replace("\t", ""));

                    range = workSheet.Cells["D6"];
                    range.Value = string.Format("              Date of Checking: {0}", toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy"));

                    int rowIndex = 9;

                    DataTable dt = result.QueryResult as DataTable;
                    int stt = 1;
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenThuoc = row["TenThuoc"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        string lieuDung = row["LieuDung"].ToString();
                        string note = row["Note"] as string;

                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = stt;
                        range.HorizontalAlignment = HAlign.Center;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = tenThuoc;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = soLuong;
                        range.HorizontalAlignment = HAlign.Center;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = lieuDung.Replace("\r", "").Replace("\t", "");
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = note.Replace("\r", "").Replace("\t", "");
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;

                        rowIndex++;
                        stt++;
                    }

                    range = workSheet.Cells[string.Format("A10:E{0}", dt.Rows.Count + 9)];
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;

                    range = workSheet.Cells[string.Format("D{0}", dt.Rows.Count + 11)];
                    range.Value = "                    Doctor's signature";
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;

                    range = workSheet.Cells[string.Format("D{0}", dt.Rows.Count + 12)];
                    range.Value = "                    (BS điều trị)";
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;


                    range = workSheet.Cells[string.Format("D{0}", dt.Rows.Count + 15)];
                    range.Value = string.Format("                    {0}", toaThuoc.TenBacSi);
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                }
                else
                {
                    excelTemplateName = string.Format("{0}\\Templates\\ToaThuocChungTemplate.xls", Application.StartupPath);
                    workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    IRange range = workSheet.Cells["D2"];
                    range.Value = toaThuoc.TenBenhNhan;

                    range = workSheet.Cells["C3"];
                    range.Value = toaThuoc.DobStr;

                    if (toaThuoc.GenderAsStr == "Nam")
                        workSheet.Shapes[1].ControlFormat.Value = 1;
                    else
                        workSheet.Shapes[0].ControlFormat.Value = 1;

                    range = workSheet.Cells["C4"];
                    range.Value = toaThuoc.Address;

                    range = workSheet.Cells["C6"];
                    range.Value = toaThuoc.ChanDoan.Replace("\r", "").Replace("\t", "");

                    int rowIndex = 7;
                    DataTable dt = result.QueryResult as DataTable;
                    int stt = 1;
                    IShape shape = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenThuoc = row["TenThuoc"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        bool Sang = Convert.ToBoolean(row["Sang"]);
                        bool Trua = Convert.ToBoolean(row["Trua"]);
                        bool Chieu = Convert.ToBoolean(row["Chieu"]);
                        bool Toi = Convert.ToBoolean(row["Toi"]);
                        bool TruocAn = Convert.ToBoolean(row["TruocAn"]);
                        bool SauAn = Convert.ToBoolean(row["SauAn"]);
                        bool Khac_TruocSauAn = Convert.ToBoolean(row["Khac_TruocSauAn"]);
                        bool Uong = Convert.ToBoolean(row["Uong"]);
                        bool Boi = Convert.ToBoolean(row["Boi"]);
                        bool DatAD = Convert.ToBoolean(row["Dat"]);
                        bool Khac_CachDung = Convert.ToBoolean(row["Khac_CachDung"]);
                        string SangNote = row["SangNote"].ToString();
                        string TruaNote = row["TruaNote"].ToString();
                        string ChieuNote = row["ChieuNote"].ToString();
                        string ToiNote = row["ToiNote"].ToString();
                        string TruocAnNote = row["TruocAnNote"].ToString();
                        string SauAnNote = row["SauAnNote"].ToString();
                        string Khac_TruocSauAnNote = row["Khac_TruocSauAnNote"].ToString();
                        string UongNote = row["UongNote"].ToString();
                        string BoiNote = row["BoiNote"].ToString();
                        string DatADNote = row["DatNote"].ToString();
                        string Khac_CachDungNote = row["Khac_CachDungNote"].ToString();

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.Value = string.Format("{0}. {1}", stt, tenThuoc);

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.Value = "Số lượng (Quantity):";

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.Value = soLuong;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Sáng (AM)";

                        range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = SangNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox, 
                            workSheet.WindowInfo.ColumnToPoints(1) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty; 
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Sang ? 1 : 0;

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Trưa (Noon)";

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = TruaNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(3) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Trua ? 1 : 0;

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Chiều (PM)";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = ChieuNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Chieu ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Tối (Night)";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = ToiNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Toi ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Trước ăn (Before meal)";

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = TruocAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(2) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = TruocAn ? 1 : 0;

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Sau ăn (After meal)";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = SauAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = SauAn ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Khác (Others)";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = Khac_TruocSauAnNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) + 13, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Khac_TruocSauAn ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Uống";

                        range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = UongNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(1) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Uong ? 1 : 0;

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Bôi";

                        range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = BoiNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(3) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Boi ? 1 : 0;

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Đặt AĐ";

                        range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = DatADNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = DatAD ? 1 : 0;

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "Khác";

                        range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Right;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = Khac_CachDungNote;

                        shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(7) - 4, workSheet.WindowInfo.RowToPoints(rowIndex), 15, 15);
                        shape.Name = string.Empty;
                        shape.Line.Visible = false;
                        shape.ControlFormat.Value = Khac_CachDung ? 1 : 0;

                        rowIndex++;

                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Take)";

                        range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Apply)";

                        range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Put inside vagina)";

                        range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Value = "(Orthers)";

                        stt++;
                        rowIndex += 2;
                    }

                    range = workSheet.Cells[string.Format("A{0}:H{1}", rowIndex + 1, rowIndex + 3)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = string.Format("Lời dặn (Recommendation): {0}", toaThuoc.Note.Replace("\r", "").Replace("\t", ""));

                    rowIndex += 3;

                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];

                    string ngayTaiKhamStr = string.Empty;
                    if (toaThuoc.NgayTaiKham != null && toaThuoc.NgayTaiKham.HasValue)
                        ngayTaiKhamStr = toaThuoc.NgayTaiKham.Value.ToString("dd/MM/yyyy");
                    range.Value = string.Format("* Tái khám ngày (Re-examination date): {0}", ngayTaiKhamStr);
                    rowIndex++;

                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                    range.Value = "* Khi tái khám vui lòng mang theo toa này (Please, keep it belong you for re-examination).";
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = string.Format("Date: {0}", toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy"));
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                    range.Font.Bold = true;
                    range.Value = "Doctor's signature";
                    rowIndex++;

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = "(BS điều trị)";

                    range = workSheet.Cells[string.Format("F{0}:G{0}", rowIndex + 4)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = toaThuoc.TenBacSi;
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportPhieuThuThuocToExcel(string exportFileName, string phieuThuThuocGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = PhieuThuThuocBus.GetPhieuThuThuoc(phieuThuThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"));
                    return false;
                }

                PhieuThuThuoc ptThuoc = result.QueryResult as PhieuThuThuoc;
                if (ptThuoc == null) return false;

                result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(phieuThuThuocGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\PhieuThuThuocTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = string.Format("Số: {0}", ptThuoc.MaPhieuThuThuoc);
                if (ptThuoc.MaBenhNhan != null && ptThuoc.MaBenhNhan.Trim() != string.Empty)
                    workSheet.Cells["B5"].Value = string.Format("Người nộp tiền: {0} - {1}", ptThuoc.TenBenhNhan, ptThuoc.MaBenhNhan);
                else
                    workSheet.Cells["B5"].Value = string.Format("Người nộp tiền: {0}", ptThuoc.TenBenhNhan);
                workSheet.Cells["B6"].Value = string.Format("Ngày: {0} - Lý do giảm: {1}", 
                    ptThuoc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"), ptThuoc.LyDoGiam);
                if (ptThuoc.DiaChi != null) workSheet.Cells["B7"].Value = string.Format("Địa chỉ: {0}", ptThuoc.DiaChi);
                else workSheet.Cells["B7"].Value = "Địa chỉ:";

                int rowIndex = 9;
                int no = 1;
                double totalPrice = 0;
                IRange range;
                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string tenThuoc = row["TenThuoc"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    string donViTinh = row["DonViTinh"].ToString();
                    double donGia = Convert.ToDouble(row["DonGia"]);
                    double giam = Convert.ToDouble(row["Giam"]);
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);

                    totalPrice += thanhTien;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = tenThuoc;

                    workSheet.Cells[rowIndex, 3].Value = soLuong;
                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    workSheet.Cells[rowIndex, 4].Value = donViTinh;
                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;

                    if (donGia > 0)
                        workSheet.Cells[rowIndex, 5].Value = donGia.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = donGia.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (giam > 0)
                        workSheet.Cells[rowIndex, 6].Value = giam.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = giam.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    if (thanhTien > 0)
                        workSheet.Cells[rowIndex, 7].Value = thanhTien.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 7].Value = thanhTien.ToString();

                    workSheet.Cells[rowIndex, 7].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:H{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice));
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:H{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex++;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = "Thuốc mua rồi miễn đổi hoặc trả lại.";
                range.Font.Italic = true;
                range.Font.Size = 9;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDichVuHopDongToExcel(string exportFileName, List<spDichVuHopDongResult> results)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DichVuHopDongTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                
                string tenHopDong = results[0].ContractName;
                DateTime tuNgay = results[0].TuNgay.Value;
                DateTime denNgay = results[0].DenNgay.Value;

                workSheet.Cells["B2"].Value = string.Format("Hợp đồng: {0}", tenHopDong);
                workSheet.Cells["B3"].Value = string.Format("Từ ngày: {0} Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 5;
                int stt = 1;
                foreach (var result in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = result.FullName;
                    if (result.NgayKham.HasValue)
                        workSheet.Cells[rowIndex, 2].Value = result.NgayKham.Value.ToString("dd/MM/yyyy");

                    workSheet.Cells[rowIndex, 3].Value = result.Mobile;
                    workSheet.Cells[rowIndex, 4].Value = result.TinhTrang;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A5:E{0}", results.Count + 5)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A5:A{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C5:C{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("E5:E{0}", results.Count + 5)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDichVuTuTucToExcel(string exportFileName, List<spDichVuTuTucResult> results)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DichVuTuTucTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                DateTime tuNgay = results[0].TuNgay.Value;
                DateTime denNgay = results[0].DenNgay.Value;

                workSheet.Cells["B2"].Value = string.Format("Từ ngày: {0} Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 4;
                int stt = 1;
                foreach (var result in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = result.FullName;
                    if (result.NgayKham.HasValue)
                        workSheet.Cells[rowIndex, 2].Value = result.NgayKham.Value.ToString("dd/MM/yyyy");

                    workSheet.Cells[rowIndex, 3].Value = result.Mobile;
                    workSheet.Cells[rowIndex, 4].Value = result.CompanyName;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A5:E{0}", results.Count + 4)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A5:A{0}", results.Count + 4)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C5:C{0}", results.Count + 4)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachBenhNhanToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DanhSachBenhNhanTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string maBenhNhan = row["FileNum"].ToString();
                    string tenBenhNhan = row["FullName"].ToString();
                    string ngaySinh = row["DobStr"].ToString();
                    string gioiTinh = row["GenderAsStr"].ToString();
                    string tinhTrangGiaDinh = row["Tinh_Trang_Gia_Dinh"].ToString();
                    string mobile = row["Mobile"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = stt;
                    workSheet.Cells[rowIndex, 1].Value = maBenhNhan;
                    workSheet.Cells[rowIndex, 2].Value = tenBenhNhan;
                    workSheet.Cells[rowIndex, 3].Value = ngaySinh;
                    workSheet.Cells[rowIndex, 4].Value = gioiTinh;
                    workSheet.Cells[rowIndex, 5].Value = tinhTrangGiaDinh;
                    workSheet.Cells[rowIndex, 8].Value = mobile;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A3:J{0}", checkedRows.Count + 2)];
                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C3:C{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("D3:E{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.ShrinkToFit = true;

                range = workSheet.Cells[string.Format("I3:I{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportCheckListToExcel(string exportFileName, DataRow drMember, DataTable dtCheckList)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\CheckListTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                string maBenhNham = drMember["FileNum"].ToString();
                string tenBenhNhan = drMember["FullName"].ToString();
                string gioiTinh = drMember["GenderAsStr"].ToString();
                string ngaySinh = drMember["DobStr"].ToString();
                string diaChi = drMember["Address"].ToString();
                string mobile = drMember["Mobile"].ToString();
                string email = drMember["Email"].ToString();

                workSheet.Cells["A1"].Value = maBenhNham;
                workSheet.Cells["B3"].Value = tenBenhNhan;
                workSheet.Cells["D3"].Value = gioiTinh;
                workSheet.Cells["F3"].Value = ngaySinh;
                workSheet.Cells["B5"].Value = diaChi;
                workSheet.Cells["B7"].Value = mobile;
                workSheet.Cells["D7"].Value = email;

                List<string> servicesNameList = new List<string>();
                Hashtable htServiceGroup = new Hashtable();
                foreach (DataRow row in dtCheckList.Rows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    string nativeServiceName = row["Name"].ToString();
                    string englishServiceName = string.Empty;
                    if (row["EnglishName"] != null && row["EnglishName"] != DBNull.Value)
                        englishServiceName = row["EnglishName"].ToString();

                    string fullServiceName = string.Empty;
                    if (englishServiceName.Trim() == string.Empty)
                        fullServiceName = nativeServiceName;
                    else
                        fullServiceName = string.Format("{0} ({1})", englishServiceName, nativeServiceName);

                    Result result = ServiceGroupBus.GetServiceGroup(serviceGUID);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"));
                        return false;
                    }
                    else
                    {
                        if (result.QueryResult == null)
                            servicesNameList.Add(fullServiceName);
                        else
                        {
                            ServiceGroup serviceGroup = result.QueryResult as ServiceGroup;
                            string groupName = serviceGroup.Name;
                            if (htServiceGroup.ContainsKey(groupName))
                            {
                                List<string> serviceList = (List<string>)htServiceGroup[groupName];
                                serviceList.Add(fullServiceName);
                            }
                            else
                            {
                                List<string> serviceList = new List<string>();
                                serviceList.Add(fullServiceName);
                                htServiceGroup.Add(groupName, serviceList);
                            }
                        }
                    }
                }

                servicesNameList.Sort();

                int rowIndex = 11;
                IRange range = null;
                int stt = 2;

                foreach (string groupName in htServiceGroup.Keys)
                {
                    List<string> serviceList = (List<string>)htServiceGroup[groupName];
                    string strValue = string.Format("   {0}. {1}\n", stt++, groupName);
                    int chr = 65;
                    foreach (string serviceName in serviceList)
                    {
                        strValue += string.Format("       {0}. {1}\n", ((char)chr++).ToString().ToLower(), serviceName);
                    }

                    range = workSheet.Cells[string.Format("A{0}:D{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.RowHeight = 16.50 * (serviceList.Count + 1);
                    range.Value = strValue;
                    range.Font.Bold = true;

                    range = workSheet.Cells[string.Format("E{0}:F{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;

                    range = workSheet.Cells[string.Format("A{0}:F{0}", rowIndex + 1)];
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;

                    rowIndex++;
                }

                foreach (string serviceName in servicesNameList)
                {
                    string strValue = string.Format("   {0}. {1}\n", stt++, serviceName);

                    range = workSheet.Cells[string.Format("A{0}:D{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Value = strValue;
                    range.Font.Bold = true;

                    range = workSheet.Cells[string.Format("E{0}:F{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;

                    range = workSheet.Cells[string.Format("A{0}:F{0}", rowIndex + 1)];
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("A{0}:D{0}", rowIndex + 1)];
                range.Merge();
                range.RowHeight = 16.50 * 3;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Center;
                range.Value = string.Format("   {0}. Others (Khác)\n", stt);
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("E{0}:F{0}", rowIndex + 1)];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("A{0}:F{0}", rowIndex + 1)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                rowIndex++;

                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = "Notes:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:F{0}", rowIndex + 1)];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;
                range.Value = "Please kindly return this medical report to the reception desk.";
                range.Font.Bold = true;

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = "Lưu ý:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:F{1}", rowIndex + 1, rowIndex + 2)];
                range.Merge();
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;
                range.WrapText = true;
                range.Value = "Sau khi hoàn tất các mục khám xin quý khách vui lòng nộp lại hồ sơ tại bàn hướng dẫn.";
                range.Font.Bold = true;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportChiTietPhieuThuToExcel(string exportFileName, List<string> phieuThuKeyList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\ChiTietPhieuThuTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (string key in phieuThuKeyList)
                {
                    Result result = ReceiptBus.GetReceipt(key);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceipt"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceipt"));
                        return false;
                    }

                    ReceiptView receipt = result.QueryResult as ReceiptView;
                    if (receipt == null) continue;

                    //result = CompanyBus.GetTenCongTy(key);
                    //if (!result.IsOK)
                    //{
                    //    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetTenCongTy"), IconType.Error);
                    //    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetTenCongTy"));
                    //    return false;
                    //}

                    //string tenCongTy = string.Empty;
                    //if (tenCongTy == null) tenCongTy = "Tự túc";
                    //if (result.QueryResult != null && result.QueryResult.ToString() != string.Empty)
                    //    tenCongTy = result.QueryResult.ToString();

                    string tenCongTy = string.Empty;
                    tenCongTy = Utility.GetMaCongTy(receipt.FileNum);
                    if (tenCongTy.Trim() == string.Empty || tenCongTy.ToLower() == "tt" || tenCongTy.ToLower() == "vgh")
                        tenCongTy = "Tự túc";

                    result = ReceiptBus.GetReceiptDetailList(key);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
                        return false;
                    }

                    DataTable dtSource = result.QueryResult as DataTable;
                   
                    foreach (DataRow row in dtSource.Rows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        result = GiaVonDichVuBus.GetGiaVonDichVuMoiNhat(serviceGUID, receipt.ReceiptDate);
                        double giaVon = 0;
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaVonDichVuBus.GetGiaVonDichVuMoiNhat"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.GetGiaVonDichVuMoiNhat"));
                            return false;
                        }
                        else
                        {
                            DataTable dt = result.QueryResult as DataTable;
                            if (dt.Rows.Count > 0)
                                giaVon = Convert.ToDouble(dt.Rows[0]["GiaVon"]);
                        }

                        string bacSiChiDinh = string.Empty;
                        result = ReceiptBus.GetBacSiChiDinh(row["ServiceHistoryGUID"].ToString());
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetBacSiChiDinh"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetBacSiChiDinh"));
                            return false;
                        }
                        else if (result.QueryResult != null)
                            bacSiChiDinh = result.QueryResult.ToString();

                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = receipt.ReceiptCode;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = receipt.ReceiptDate.ToString("dd/MM/yyyy");

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = tenCongTy;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = receipt.FullName;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = row["Name"].ToString();
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 5];
                        range.Value = Convert.ToDouble(row["Amount"]);

                        range = workSheet.Cells[rowIndex, 6];
                        range.Value = giaVon;

                        range = workSheet.Cells[rowIndex, 7];
                        range.Value = bacSiChiDinh;

                        rowIndex++;
                    }
                }

                range = workSheet.Cells[string.Format("A3:H{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportChiTietPhieuThuThuocToExcel(string exportFileName, List<string> phieuThuKeyList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\ChiTietPhieuThuThuocTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (string key in phieuThuKeyList)
                {
                    Result result = PhieuThuThuocBus.GetPhieuThuThuoc(key);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuoc"));
                        return false;
                    }

                    PhieuThuThuoc phieuThuThuoc = result.QueryResult as PhieuThuThuoc;
                    if (phieuThuThuoc == null) continue;
                    if (phieuThuThuoc.TenCongTy == null) phieuThuThuoc.TenCongTy = "Tự túc";

                    result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(key);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
                        return false;
                    }

                    DataTable dtSource = result.QueryResult as DataTable;

                    foreach (DataRow row in dtSource.Rows)
                    {
                        range = workSheet.Cells[rowIndex, 0];
                        range.Value = phieuThuThuoc.MaPhieuThuThuoc;

                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = phieuThuThuoc.NgayThu.ToString("dd/MM/yyyy");

                        string maCongTy = Utility.GetMaCongTy(phieuThuThuoc.MaBenhNhan);
                        if (maCongTy.ToLower() == "tt" || maCongTy.ToLower() == "vgh" || maCongTy.Trim() == string.Empty)
                            maCongTy = "Tự túc";

                        range = workSheet.Cells[rowIndex, 2];
                        range.Value = maCongTy;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 3];
                        range.Value = phieuThuThuoc.TenBenhNhan;
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 4];
                        range.Value = row["TenThuoc"].ToString();
                        range.WrapText = true;

                        range = workSheet.Cells[rowIndex, 5];
                        range.Value = Convert.ToDouble(row["ThanhTien"]);

                        rowIndex++;
                    }
                }

                range = workSheet.Cells[string.Format("A3:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKhamSucKhoeTongQuatToExcel(string exportFileName, DataRow patientRow, DateTime fromDate, DateTime toDate)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                //Lấy thông tin bệnh nhân
                string patientGUID = patientRow["PatientGUID"].ToString();
                string tenBenhNhan = patientRow["FullName"].ToString();
                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string ngaySinh = patientRow["DobStr"].ToString();
                string diaChi = patientRow["Address"].ToString();

                Result result = CompanyBus.GetTenCongTy(patientGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetTenCongTy"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetTenCongTy"));
                    return false;
                }

                string tenCongTy = "Tự túc";
                if (patientRow["CompanyName"] != null && patientRow["CompanyName"] != DBNull.Value)
                    tenCongTy = patientRow["CompanyName"].ToString();

                if (result.QueryResult != null && result.QueryResult.ToString() != string.Empty)
                    tenCongTy = result.QueryResult.ToString();

                //Lấy thông tin cân đo
                result = CanDoBus.GetLastCanDo(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CanDoBus.GetLastCanDo"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.GetLastCanDo"));
                    return false;
                }

                CanDo canDo = result.QueryResult as CanDo;

                //Lấy thông tin khám lâm sàng
                result = KetQuaLamSangBus.GetLastKetQuaLamSang(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaLamSangBus.GetLastKetQuaLamSang"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.GetLastKetQuaLamSang"));
                    return false;
                }

                Hashtable htKetQuaLamSang = result.QueryResult as Hashtable;

                //Lấy thông tin dịch vụ sử dụng lâm sàng
                result = ServiceHistoryBus.GetServiceHistory(patientGUID, fromDate, toDate, ServiceType.LamSang);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
                    return false;
                }

                List<ServiceHistoryView> serviceLamSangList = (List<ServiceHistoryView>)result.QueryResult;

                //Lấy thông tin dịch vụ sử dụng cận lâm sàng
                result = ServiceHistoryBus.GetServiceHistory(patientGUID, fromDate, toDate, ServiceType.CanLamSang);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
                    return false;
                }

                List<ServiceHistoryView> serviceCanLamSangList = (List<ServiceHistoryView>)result.QueryResult;

                //Lấy thông tin lời khuyên
                result = LoiKhuyenBus.GetLoiKhuyenList2(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList2"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList2"));
                    return false;
                }

                List<LoiKhuyenView> loiKhuyenList = (List<LoiKhuyenView>)result.QueryResult;

                //Lấy thông tin kết luận
                result = KetLuanBus.GetLastKetLuan(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetLuanBus.GetLastKetLuan"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.GetLastKetLuan"));
                    return false;
                }

                KetLuan ketLuan = result.QueryResult as KetLuan;

                string excelTemplateName = string.Format("{0}\\Templates\\KhamSucKhoeTongQuatTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                IRange range;
                IShape shape;

                //Fill thông tin bệnh nhân
                workSheet.Cells["B2"].Value = tenBenhNhan;
                workSheet.Cells["F2"].Value = gioiTinh;
                workSheet.Cells["I2"].Value = ngaySinh;
                workSheet.Cells["B4"].Value = diaChi;
                workSheet.Cells["F4"].Value = tenCongTy;

                //Fill thông tin cân đo
                if (canDo != null)
                {
                    workSheet.Cells["A8"].Value = canDo.ChieuCao;
                    workSheet.Cells["B8"].Value = canDo.CanNang;
                    workSheet.Cells["C8"].Value = canDo.BMI;
                    workSheet.Cells["D8"].Value = canDo.HuyetAp;
                    workSheet.Cells["E8"].Value = canDo.TimMach;
                    workSheet.Cells["F8"].Value = canDo.HoHap;
                    workSheet.Cells["G8"].Value = canDo.MuMau;
                    workSheet.Cells["H7"].Value = string.Format("R(P): {0}\nL(T): {1}", canDo.MatTrai, canDo.MatTrai);

                    if (!canDo.HieuChinh) workSheet.Shapes[0].ControlFormat.Value = 1;
                    else workSheet.Shapes[1].ControlFormat.Value = 1;
                }

                //Fill thông tin khám lâm sàng
                CoQuan[] coQuanList = (CoQuan[])Enum.GetValues(typeof(CoQuan));
                int checkIndex = 4;
                int rowIndex = 10;
                foreach (CoQuan coQuan in coQuanList)
                {
                    if (htKetQuaLamSang.ContainsKey(coQuan))
                    {
                        KetQuaLamSang kq = (KetQuaLamSang)htKetQuaLamSang[coQuan];
                        if (kq.Note == null) kq.Note = string.Empty;
                        if (kq.SoiTuoiHuyetTrang == null) kq.SoiTuoiHuyetTrang = string.Empty;
                        if (kq.PhuKhoaNote == null) kq.PhuKhoaNote = string.Empty;

                        if (coQuan != CoQuan.Khac && coQuan != CoQuan.KhamPhuKhoa)
                        {
                            if (kq.Normal) workSheet.Shapes[checkIndex].ControlFormat.Value = 1;
                            if (kq.Abnormal) workSheet.Shapes[checkIndex + 1].ControlFormat.Value = 1;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = kq.Note.Replace("\r", "").Replace("\t", "");
                        }
                        else if (coQuan == CoQuan.Khac)
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = kq.Note.Replace("\r", "").Replace("\t", "");
                        else
                        {
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("PARA: {0}", kq.PARA);
                            if (kq.NgayKinhChot.HasValue)
                                workSheet.Cells[string.Format("I{0}", rowIndex)].Value = kq.NgayKinhChot.Value.ToString("dd/MM/yyyy");
                            rowIndex++;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("Ghi chú: {0}", kq.PhuKhoaNote.Replace("\r", "").Replace("\t", ""));
                            rowIndex++;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("Kết quả khám phụ khoa: {0}", kq.Note.Replace("\r", "").Replace("\t", ""));
                            rowIndex++;
                            workSheet.Cells[string.Format("F{0}", rowIndex)].Value = string.Format("Soi tươi huyết trắng: {0}", kq.SoiTuoiHuyetTrang.Replace("\r", "").Replace("\t", ""));
                            if (kq.Normal) workSheet.Shapes[2].ControlFormat.Value = 1;
                            if (kq.Abnormal) workSheet.Shapes[3].ControlFormat.Value = 1;
                        }
                    }

                    checkIndex += 2;
                    rowIndex += 2;
                }

                //Fill thông tin dịch vụ sử dụng lâm sàng
                rowIndex = 38;
                foreach (ServiceHistoryView srvHistory in serviceLamSangList)
                {
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                    range.Insert(InsertShiftDirection.Down);

                    if (srvHistory.Note == null) srvHistory.Note = string.Empty;
                    string serviceName = string.Empty;
                    if (srvHistory.EnglishName == null || srvHistory.EnglishName.Trim() == string.Empty)
                        serviceName = srvHistory.Name;
                    else
                        serviceName = string.Format("{0} ({1})", srvHistory.EnglishName, srvHistory.Name);

                    range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Font.Bold = true;
                    range.Value = serviceName;

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 1, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 100, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Normal (Bình thường)";
                        shape.ControlFormat.Value = srvHistory.Normal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Negative (Âm tính)";
                        shape.ControlFormat.Value = srvHistory.Negative ? 1 : 0;
                    }

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 119, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 110, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Abnormal (Bất thường)";
                        shape.ControlFormat.Value = srvHistory.Abnormal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Positive (Dương tính)";
                        shape.ControlFormat.Value = srvHistory.Positive ? 1 : 0;
                    }

                    range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Value = srvHistory.Note.Replace("\r", "").Replace("\t", "");

                    rowIndex += 2;
                }

                if (rowIndex < 41)
                {
                    int count = 41 - rowIndex;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    for (int i = 0; i < count; i++)
                    {
                        range.Insert(InsertShiftDirection.Down);
                    }

                    rowIndex = serviceLamSangList.Count * 2 + 39 + count;
                }
                else
                    rowIndex = serviceLamSangList.Count * 2 + 39;

                //Fill thông tin dịch vụ sử dụng cận lâm sàng
                //rowIndex = serviceLamSangList.Count * 2 + 39;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)].EntireRow;
                range.Insert(InsertShiftDirection.Down);

                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "PARACLINICAL RESULTS\nKẾT QUẢ CẬN LÂM SÀNG";
                range.RowHeight = 33.75;

                range = workSheet.Cells[string.Format("F{0}:I{0}", rowIndex)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "COMMENTS\nNHẬN XÉT";

                List<ServiceHistoryView> serviceNoGroupList = new List<ServiceHistoryView>();
                Hashtable htServiceGroup = new Hashtable();
                foreach (ServiceHistoryView srvHistory in serviceCanLamSangList)
                {
                    result = ServiceGroupBus.GetServiceGroup(srvHistory.ServiceGUID.ToString());
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"));
                        return false;
                    }

                    ServiceGroup serviceGroup = result.QueryResult as ServiceGroup;
                    if (serviceGroup == null)
                        serviceNoGroupList.Add(srvHistory);
                    else
                    {
                        if (!htServiceGroup.ContainsKey(serviceGroup.ServiceGroupGUID.ToString()))
                        {
                            List<ServiceHistoryView> serviceGroupList = new List<ServiceHistoryView>();
                            serviceGroupList.Add(srvHistory);
                            htServiceGroup.Add(serviceGroup.ServiceGroupGUID.ToString(), serviceGroupList);
                        }
                        else
                        {
                            List<ServiceHistoryView> serviceGroupList = (List<ServiceHistoryView>)htServiceGroup[serviceGroup.ServiceGroupGUID.ToString()];
                            serviceGroupList.Add(srvHistory);
                        }
                    }
                }

                foreach (List<ServiceHistoryView> serviceGroupList in htServiceGroup.Values)
                {
                    foreach (ServiceHistoryView srvHistory in serviceGroupList)
                    {
                        serviceNoGroupList.Add(srvHistory);
                    }
                }

                foreach (ServiceHistoryView srvHistory in serviceNoGroupList)
                {
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 2)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                    range.Insert(InsertShiftDirection.Down);

                    if (srvHistory.Note == null) srvHistory.Note = string.Empty;
                    string serviceName = string.Empty;
                    if (srvHistory.EnglishName == null || srvHistory.EnglishName.Trim() == string.Empty)
                        serviceName = srvHistory.Name;
                    else
                        serviceName = string.Format("{0} ({1})", srvHistory.EnglishName, srvHistory.Name);

                    range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Font.Bold = true;
                    range.Value = serviceName;

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 1, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 100, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Normal (Bình thường)";
                        shape.ControlFormat.Value = srvHistory.Normal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Negative (Âm tính)";
                        shape.ControlFormat.Value = srvHistory.Negative ? 1 : 0;
                    }

                    shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(0) + 119, workSheet.WindowInfo.RowToPoints(rowIndex) + 15.5, 110, 15);
                    shape.Line.Visible = false;
                    shape.TextFrame.HorizontalAlignment = HAlign.Left;
                    shape.TextFrame.VerticalAlignment = VAlign.Center;

                    if (srvHistory.IsNormalOrNegative)
                    {
                        shape.TextFrame.Characters.Text = "Abnormal (Bất thường)";
                        shape.ControlFormat.Value = srvHistory.Abnormal ? 1 : 0;
                    }
                    else
                    {
                        shape.TextFrame.Characters.Text = "Positive (Dương tính)";
                        shape.ControlFormat.Value = srvHistory.Positive ? 1 : 0;
                    }

                    range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                    range.Merge();
                    range.WrapText = true;
                    range.HorizontalAlignment = HAlign.Left;
                    range.VerticalAlignment = VAlign.Top;
                    range.Borders.Color = Color.Black;
                    range.Borders.LineStyle = LineStyle.Continuous;
                    range.Borders.Weight = BorderWeight.Thin;
                    range.Value = srvHistory.Note.Replace("\r", "").Replace("\t", "");

                    rowIndex += 2;
                }

                range = workSheet.Cells[string.Format("A{0}", rowIndex + 2)].EntireRow;
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                
                range = workSheet.Cells[string.Format("A{0}:E{1}", rowIndex + 1, rowIndex + 2)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Center;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;
                range.Font.Bold = true;
                range.Value = "Others (Cận lâm sàng khác)";

                range = workSheet.Cells[string.Format("F{0}:I{1}", rowIndex + 1, rowIndex + 2)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                rowIndex += 3;

                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);

                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "1. CÁC XÉT NGHIỆM LÀM THÊM";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasLamThemXetNghiem ? 1 : 0;

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasLamThemXetNghiem ? 0 : 1;

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}:I{1}", rowIndex, rowIndex + 1)];
                range.Merge();
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Top;
                if (ketLuan != null) range.Value = ketLuan.CacXetNghiemLamThem;

                //range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;
                //range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dot;
                //range.Borders[BordersIndex.EdgeBottom].Weight = BorderWeight.Thin;

                //rowIndex++;
                //range = workSheet.Cells[string.Format("A{0}:I{0}", rowIndex)];
                //range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;
                //range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dot;
                //range.Borders[BordersIndex.EdgeBottom].Weight = BorderWeight.Thin;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "2. ĐÃ LÀM ĐỦ CẬN LÂM SÀNG TRONG GÓI KHÁM";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasLamDuCanLamSang ? 1 : 0;

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasLamDuCanLamSang ? 0 : 1;

                range = workSheet.Cells[string.Format("H{0}", rowIndex)];
                if (ketLuan == null)
                    range.Value = "      Lý do:………….";
                else
                    range.Value = string.Format("      Lý do: {0}", ketLuan.LyDo_CanLamSang);

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Font.Bold = true;
                range.Value = "3. ĐỦ SỨC KHỎE LÀM VIỆC";

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(5) + 25, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 40, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Có";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasDuSucKhoe ? 1 : 0;

                shape = workSheet.Shapes.AddFormControl(SpreadsheetGear.Shapes.FormControlType.CheckBox,
                            workSheet.WindowInfo.ColumnToPoints(6) + 15, workSheet.WindowInfo.RowToPoints(rowIndex - 1), 50, 15);
                shape.Line.Visible = false;
                shape.TextFrame.HorizontalAlignment = HAlign.Left;
                shape.TextFrame.VerticalAlignment = VAlign.Center;
                shape.TextFrame.Characters.Text = "Không";
                if (ketLuan != null) shape.ControlFormat.Value = ketLuan.HasDuSucKhoe ? 0 : 1;

                range = workSheet.Cells[string.Format("H{0}", rowIndex)];

                if (ketLuan == null)
                    range.Value = "      Loại:………….";
                else
                    range.Value = string.Format("      Loại: {0}", ketLuan.LyDo_SucKhoe);

                if (rowIndex < 90)
                {
                    int count = 90 - rowIndex;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    for (int i = 0; i < count - 1; i++)
                    {
                        range.Insert(InsertShiftDirection.Down);
                    }

                    rowIndex += count - 1;    
                }

                //Fill thông tin lời khuyên
                rowIndex += 2;
                range = workSheet.Cells[string.Format("A{0}", rowIndex)].EntireRow;
                range.Insert(InsertShiftDirection.Down);
                range.Insert(InsertShiftDirection.Down);

                range = workSheet.Cells[string.Format("A{0}", rowIndex)];
                range.Value = "ĐỀ NGHỊ THEO DÕI THÊM";
                range.Font.Bold = true;
                range.Font.Underline = UnderlineStyle.Single;
                
                if (loiKhuyenList.Count > 0)
                {
                    rowIndex += 1;
                    range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                    range.Insert(InsertShiftDirection.Down);

                    foreach (LoiKhuyenView loiKhuyen in loiKhuyenList)
                    {
                        string symptomName = loiKhuyen.SymptomName.Replace("\r", "").Replace("\t", "");
                        string advice = loiKhuyen.Advice.Replace("\r", "").Replace("\t", "");
                        range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                        range.Merge();
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Center;
                        range.Borders.Color = Color.Black;
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Weight = BorderWeight.Thin;
                        range.Value = symptomName;

                        range = workSheet.Cells[string.Format("F{0}:I{0}", rowIndex + 1)];
                        range.Merge();
                        range.WrapText = true;
                        range.WrapText = true;
                        range.HorizontalAlignment = HAlign.Left;
                        range.VerticalAlignment = VAlign.Top;
                        range.Borders.Color = Color.Black;
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Weight = BorderWeight.Thin;
                        range.Value = advice;

                        int lineCount = advice.Length / 45;
                        if (lineCount % 40 != 0) lineCount++;

                        range.RowHeight = 15.75 * lineCount;
                        rowIndex += 1;
                        range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)].EntireRow;
                        range.Insert(InsertShiftDirection.Down);
                        range.RowHeight = 15.75;
                    }
                }

                rowIndex += 7;
                range = workSheet.Cells[string.Format("G{0}", rowIndex)];
                range.Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy"));

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDoanhThuTheoNgayToExcel(string exportFileName, DateTime tuNgay, DateTime denNgay)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = ReportBus.GetChiTietPhieuThuDichVu(tuNgay, denNgay);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetChiTietPhieuThuDichVu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetChiTietPhieuThuDichVu"));
                    return false;
                }

                List<ReceiptDetailView> receiptDetailList = (List<ReceiptDetailView>)result.QueryResult;
                if (receiptDetailList == null) return false;

                result = ReportBus.GetChiTietPhieuThuThuoc(tuNgay, denNgay);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetChiTietPhieuThuThuoc"));
                    return false;
                }

                List<ChiTietPhieuThuThuocView> chiTietPhieuThuThuocList = (List<ChiTietPhieuThuThuocView>)result.QueryResult;
                if (chiTietPhieuThuThuocList == null) return false;

                string excelTemplateName = string.Format("{0}\\Templates\\DoanhThuTheoNgayTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A2"].Value = string.Format("Từ ngày: {0} - Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 3;
                int no = 1;
                double totalPrice = 0.0;
                double totalLaiLoDV = 0.0;
                IRange range;
                foreach (ReceiptDetailView detail in receiptDetailList)
                {
                    string serviceName = detail.Name;
                    double price = detail.Price.Value;
                    double disCount = detail.Discount;
                    double amount = price - (price * disCount) / 100;
                    double giaVon = detail.GiaVon;
                    double laiLo = amount - giaVon;

                    totalPrice += amount;
                    totalLaiLoDV += laiLo;

                    workSheet.Cells[rowIndex,0].Value = no++;
                    workSheet.Cells[rowIndex, 0].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 1].Value = serviceName;

                    workSheet.Cells[rowIndex, 2].Value = 1;
                    workSheet.Cells[rowIndex, 2].HorizontalAlignment = HAlign.Right;

                    if (price > 0)
                        workSheet.Cells[rowIndex, 3].Value = price.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 3].Value = price.ToString();

                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (disCount > 0)
                        workSheet.Cells[rowIndex, 4].Value = disCount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = disCount.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (amount > 0)
                        workSheet.Cells[rowIndex, 5].Value = amount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = amount.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (giaVon > 0)
                        workSheet.Cells[rowIndex, 6].Value = giaVon.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = giaVon.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    if (laiLo > 0)
                        workSheet.Cells[rowIndex, 7].Value = laiLo.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 7].Value = laiLo.ToString();

                    workSheet.Cells[rowIndex, 7].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("A{0}:H{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng tiền dịch vụ:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                if (totalLaiLoDV > 0)
                    range.Value = string.Format("{0} VNĐ", totalLaiLoDV.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalLaiLoDV.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = "STT";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = "Thuốc";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Số lượng";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                range.Value = "Giá";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Giảm (%)";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.Value = "Thành tiền";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("G{0}", rowIndex + 1)];
                range.Value = "Giá nhập";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                range.Value = "Lãi/Lỗ";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("A{0}:H{0}", rowIndex + 1)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;

                rowIndex++;
                no = 1;
                double tongTienThuoc = 0.0;
                double totalLaiLoThuoc = 0.0;

                foreach (ChiTietPhieuThuThuocView detail in chiTietPhieuThuThuocList)
                {
                    string tenThuoc = detail.TenThuoc;
                    int soLuong = Convert.ToInt32(detail.SoLuong);
                    double donGia = detail.DonGia;
                    double giam = detail.Giam;
                    double thanhTien = detail.ThanhTien;
                    double giaNhap = detail.DonGiaNhap;
                    double laiLo = thanhTien - (giaNhap * soLuong);

                    totalPrice += thanhTien;
                    tongTienThuoc += thanhTien;
                    totalLaiLoThuoc += laiLo;

                    workSheet.Cells[rowIndex, 0].Value = no++;
                    workSheet.Cells[rowIndex, 0].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 1].Value = tenThuoc;

                    workSheet.Cells[rowIndex, 2].Value = soLuong;
                    workSheet.Cells[rowIndex, 2].HorizontalAlignment = HAlign.Right;

                    if (donGia > 0)
                        workSheet.Cells[rowIndex, 3].Value = donGia.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 3].Value = donGia.ToString();

                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (giam > 0)
                        workSheet.Cells[rowIndex, 4].Value = giam.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = giam.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (thanhTien > 0)
                        workSheet.Cells[rowIndex, 5].Value = thanhTien.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = thanhTien.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    if (giaNhap > 0)
                        workSheet.Cells[rowIndex, 6].Value = giaNhap.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 6].Value = giaNhap.ToString();

                    workSheet.Cells[rowIndex, 6].HorizontalAlignment = HAlign.Right;

                    if (laiLo > 0)
                        workSheet.Cells[rowIndex, 7].Value = laiLo.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 7].Value = laiLo.ToString();

                    workSheet.Cells[rowIndex, 7].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("A{0}:H{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeTop].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeTop].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeLeft].Color = Color.Black;

                    range.Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.EdgeRight].Color = Color.Black;

                    range.Borders[BordersIndex.InsideVertical].LineStyle = LineStyle.Continuous;
                    range.Borders[BordersIndex.InsideVertical].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng tiền thuốc:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (tongTienThuoc > 0)
                    range.Value = string.Format("{0} VNĐ", tongTienThuoc.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", tongTienThuoc.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                if (totalLaiLoThuoc > 0)
                    range.Value = string.Format("{0} VNĐ", totalLaiLoThuoc.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalLaiLoThuoc.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex++;
                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("H{0}", rowIndex + 1)];
                totalLaiLoDV += totalLaiLoThuoc;
                if (totalLaiLoDV > 0)
                    range.Value = string.Format("{0} VNĐ", totalLaiLoDV.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalLaiLoDV.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaNoiSoiTaiToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("    Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("    Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                if (ketQuaNoiSoi["LyDoKham"] != null && ketQuaNoiSoi["LyDoKham"] != DBNull.Value)
                    workSheet.Cells["A8"].Value = string.Format("Lý do khám: {0}", ketQuaNoiSoi["LyDoKham"].ToString());

                if (ketQuaNoiSoi["TenBacSiChiDinh"] != null && ketQuaNoiSoi["TenBacSiChiDinh"] != DBNull.Value)
                    workSheet.Cells["D8"].Value = string.Format("BSCĐ: {0}", ketQuaNoiSoi["TenBacSiChiDinh"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaNoiSoi["Hinh1"] != null && ketQuaNoiSoi["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh1"]);

                if (ketQuaNoiSoi["Hinh2"] != null && ketQuaNoiSoi["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh2"]);

                if (ketQuaNoiSoi["Hinh3"] != null && ketQuaNoiSoi["Hinh3"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh3"]);

                if (ketQuaNoiSoi["Hinh4"] != null && ketQuaNoiSoi["Hinh4"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh4"]);

                double left = 0.73803149606299212;
                double top = 198;
                double width = 128.50485241420625;
                double height = 129.2431496062992;

                if (hinhList.Count == 4)
                {
                    left = 0.73803149606299212;
                    top = 198;
                    width = 128.50485241420625;
                    height = 129.2431496062992;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 134.99997052444246;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 269.12107375824547;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);

                    left = 403.32905700657312;
                    workSheet.Shapes.AddPicture(hinhList[3], left, top, width, height);
                }
                else if (hinhList.Count == 3)
                {
                    left = 0.73803149606299212;
                    width = 163.64469756847291;
                    height = 171.0;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 184.49902827713433;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 368.15975936217569;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);
                }
                else if (hinhList.Count == 2)
                {
                    left = 96.134184222258924;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 272.01223594316349;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);
                }
                else
                {
                    left = 183.74839835587449;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);
                }

                workSheet.Cells["A13"].Value = ketQuaNoiSoi["OngTaiPhai"].ToString();
                workSheet.Cells["D13"].Value = ketQuaNoiSoi["OngTaiTrai"].ToString();

                workSheet.Cells["A14"].Value = ketQuaNoiSoi["MangNhiPhai"].ToString();
                workSheet.Cells["D14"].Value = ketQuaNoiSoi["MangNhiTrai"].ToString();

                workSheet.Cells["A15"].Value = ketQuaNoiSoi["CanBuaPhai"].ToString();
                workSheet.Cells["D15"].Value = ketQuaNoiSoi["CanBuaTrai"].ToString();

                workSheet.Cells["A16"].Value = ketQuaNoiSoi["HomNhiPhai"].ToString();
                workSheet.Cells["D16"].Value = ketQuaNoiSoi["HomNhiTrai"].ToString();

                workSheet.Cells["A17"].Value = ketQuaNoiSoi["ValsavaPhai"].ToString();
                workSheet.Cells["D17"].Value = ketQuaNoiSoi["ValsavaTrai"].ToString();

                workSheet.Cells["B19"].Value = ketQuaNoiSoi["KetLuan"].ToString();
                workSheet.Cells["B21"].Value = ketQuaNoiSoi["DeNghi"].ToString();

                workSheet.Cells["F23"].Value = string.Format("Ngày: {0}", 
                    Convert.ToDateTime(ketQuaNoiSoi["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F27"].Value = ketQuaNoiSoi["TenBacSiNoiSoi"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaNoiSoiMuiToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaNoiSoiMuiTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("    Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("    Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                if (ketQuaNoiSoi["LyDoKham"] != null && ketQuaNoiSoi["LyDoKham"] != DBNull.Value)
                    workSheet.Cells["A8"].Value = string.Format("Lý do khám: {0}", ketQuaNoiSoi["LyDoKham"].ToString());

                if (ketQuaNoiSoi["TenBacSiChiDinh"] != null && ketQuaNoiSoi["TenBacSiChiDinh"] != DBNull.Value)
                    workSheet.Cells["D8"].Value = string.Format("BSCĐ: {0}", ketQuaNoiSoi["TenBacSiChiDinh"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaNoiSoi["Hinh1"] != null && ketQuaNoiSoi["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh1"]);

                if (ketQuaNoiSoi["Hinh2"] != null && ketQuaNoiSoi["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh2"]);

                if (ketQuaNoiSoi["Hinh3"] != null && ketQuaNoiSoi["Hinh3"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh3"]);

                if (ketQuaNoiSoi["Hinh4"] != null && ketQuaNoiSoi["Hinh4"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh4"]);

                double left = 0.73803149606299212;
                double top = 198;
                double width = 128.50485241420625;
                double height = 129.2431496062992;

                if (hinhList.Count == 4)
                {
                    left = 0.73803149606299212;
                    top = 198;
                    width = 128.50485241420625;
                    height = 129.2431496062992;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 134.99997052444246;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 269.12107375824547;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);

                    left = 403.32905700657312;
                    workSheet.Shapes.AddPicture(hinhList[3], left, top, width, height);
                }
                else if (hinhList.Count == 3)
                {
                    left = 0.73803149606299212;
                    width = 163.64469756847291;
                    height = 171.0;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 184.49902827713433;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 368.15975936217569;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);
                }
                else if (hinhList.Count == 2)
                {
                    left = 96.134184222258924;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 272.01223594316349;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);
                }
                else
                {
                    left = 183.74839835587449;
                   width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);
                }

                workSheet.Cells["A13"].Value = ketQuaNoiSoi["NiemMacPhai"].ToString();
                workSheet.Cells["D13"].Value = ketQuaNoiSoi["NiemMacTrai"].ToString();

                workSheet.Cells["A14"].Value = ketQuaNoiSoi["VachNganPhai"].ToString();
                workSheet.Cells["D14"].Value = ketQuaNoiSoi["VachNganTrai"].ToString();

                workSheet.Cells["A15"].Value = ketQuaNoiSoi["KheTrenPhai"].ToString();
                workSheet.Cells["D15"].Value = ketQuaNoiSoi["KheTrenTrai"].ToString();

                workSheet.Cells["A16"].Value = ketQuaNoiSoi["KheGiuaPhai"].ToString();
                workSheet.Cells["D16"].Value = ketQuaNoiSoi["KheGiuaTrai"].ToString();

                workSheet.Cells["A17"].Value = ketQuaNoiSoi["CuonGiuaPhai"].ToString();
                workSheet.Cells["D17"].Value = ketQuaNoiSoi["CuonGiuaTrai"].ToString();

                workSheet.Cells["A18"].Value = ketQuaNoiSoi["CuonDuoiPhai"].ToString();
                workSheet.Cells["D18"].Value = ketQuaNoiSoi["CuonDuoiTrai"].ToString();

                workSheet.Cells["A19"].Value = ketQuaNoiSoi["MomMocPhai"].ToString();
                workSheet.Cells["D19"].Value = ketQuaNoiSoi["MomMocTrai"].ToString();

                workSheet.Cells["A20"].Value = ketQuaNoiSoi["BongSangPhai"].ToString();
                workSheet.Cells["D20"].Value = ketQuaNoiSoi["BongSangTrai"].ToString();

                workSheet.Cells["A21"].Value = ketQuaNoiSoi["VomPhai"].ToString();
                workSheet.Cells["D21"].Value = ketQuaNoiSoi["VomTrai"].ToString();

                workSheet.Cells["B23"].Value = ketQuaNoiSoi["KetLuan"].ToString();
                workSheet.Cells["B25"].Value = ketQuaNoiSoi["DeNghi"].ToString();

                workSheet.Cells["F27"].Value = string.Format("Ngày: {0}",
                    Convert.ToDateTime(ketQuaNoiSoi["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F31"].Value = ketQuaNoiSoi["TenBacSiNoiSoi"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaNoiSoiTongQuatToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaNoiSoiTongQuatTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("    Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("    Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                if (ketQuaNoiSoi["LyDoKham"] != null && ketQuaNoiSoi["LyDoKham"] != DBNull.Value)
                    workSheet.Cells["A8"].Value = string.Format("Lý do khám: {0}", ketQuaNoiSoi["LyDoKham"].ToString());

                if (ketQuaNoiSoi["TenBacSiChiDinh"] != null && ketQuaNoiSoi["TenBacSiChiDinh"] != DBNull.Value)
                    workSheet.Cells["D8"].Value = string.Format("BSCĐ: {0}", ketQuaNoiSoi["TenBacSiChiDinh"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaNoiSoi["Hinh1"] != null && ketQuaNoiSoi["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh1"]);

                if (ketQuaNoiSoi["Hinh2"] != null && ketQuaNoiSoi["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh2"]);

                if (ketQuaNoiSoi["Hinh3"] != null && ketQuaNoiSoi["Hinh3"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh3"]);

                if (ketQuaNoiSoi["Hinh4"] != null && ketQuaNoiSoi["Hinh4"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh4"]);

                double left = 0.73803149606299212;
                double top = 198;
                double width = 128.50485241420625;
                double height = 129.2431496062992;

                if (hinhList.Count == 4)
                {
                    left = 0.73803149606299212;
                    top = 198;
                    width = 128.50485241420625;
                    height = 129.2431496062992;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 134.99997052444246;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 269.12107375824547;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);

                    left = 403.32905700657312;
                    workSheet.Shapes.AddPicture(hinhList[3], left, top, width, height);
                }
                else if (hinhList.Count == 3)
                {
                    left = 0.73803149606299212;
                    width = 163.64469756847291;
                    height = 171.0;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 184.49902827713433;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 368.15975936217569;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);
                }
                else if (hinhList.Count == 2)
                {
                    left = 96.134184222258924;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 272.01223594316349;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);
                }
                else
                {
                    left = 183.74839835587449;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);
                }

                workSheet.Cells["A13"].Value = ketQuaNoiSoi["OngTaiPhai"].ToString();
                workSheet.Cells["D13"].Value = ketQuaNoiSoi["OngTaiTrai"].ToString();

                workSheet.Cells["A14"].Value = ketQuaNoiSoi["MangNhiPhai"].ToString();
                workSheet.Cells["D14"].Value = ketQuaNoiSoi["MangNhiTrai"].ToString();

                workSheet.Cells["A15"].Value = ketQuaNoiSoi["CanBuaPhai"].ToString();
                workSheet.Cells["D15"].Value = ketQuaNoiSoi["CanBuaTrai"].ToString();

                workSheet.Cells["A16"].Value = ketQuaNoiSoi["HomNhiPhai"].ToString();
                workSheet.Cells["D16"].Value = ketQuaNoiSoi["HomNhiTrai"].ToString();

                workSheet.Cells["B18"].Value = ketQuaNoiSoi["KetLuan"].ToString();
                workSheet.Cells["B20"].Value = ketQuaNoiSoi["DeNghi"].ToString();

                workSheet.Cells["F22"].Value = string.Format("Ngày: {0}",
                    Convert.ToDateTime(ketQuaNoiSoi["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F26"].Value = ketQuaNoiSoi["TenBacSiNoiSoi"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaNoiSoiHongThanhQuanToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaNoiSoiHongThanhQuanTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("          Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("          Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                if (ketQuaNoiSoi["LyDoKham"] != null && ketQuaNoiSoi["LyDoKham"] != DBNull.Value)
                    workSheet.Cells["A8"].Value = string.Format("Lý do khám: {0}", ketQuaNoiSoi["LyDoKham"].ToString());

                if (ketQuaNoiSoi["TenBacSiChiDinh"] != null && ketQuaNoiSoi["TenBacSiChiDinh"] != DBNull.Value)
                    workSheet.Cells["D8"].Value = string.Format("BSCĐ: {0}", ketQuaNoiSoi["TenBacSiChiDinh"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaNoiSoi["Hinh1"] != null && ketQuaNoiSoi["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh1"]);

                if (ketQuaNoiSoi["Hinh2"] != null && ketQuaNoiSoi["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh2"]);

                if (ketQuaNoiSoi["Hinh3"] != null && ketQuaNoiSoi["Hinh3"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh3"]);

                if (ketQuaNoiSoi["Hinh4"] != null && ketQuaNoiSoi["Hinh4"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh4"]);

                double left = 0.73803149606299212;
                double top = 195;
                double width = 128.50485241420625;
                double height = 129.2431496062992;

                if (hinhList.Count == 4)
                {
                    left = 0.73803149606299212;
                    top = 195;
                    width = 128.50485241420625;
                    height = 129.2431496062992;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 134.99997052444246;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 269.12107375824547;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);

                    left = 403.32905700657312;
                    workSheet.Shapes.AddPicture(hinhList[3], left, top, width, height);
                }
                else if (hinhList.Count == 3)
                {
                    left = 0.73803149606299212;
                    width = 163.64469756847291;
                    height = 171.0;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 184.49902827713433;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 368.15975936217569;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);
                }
                else if (hinhList.Count == 2)
                {
                    left = 96.134184222258924;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 272.01223594316349;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);
                }
                else
                {
                    left = 183.74839835587449;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);
                }


                workSheet.Cells["C13"].Value = ketQuaNoiSoi["Amydale"].ToString();
                workSheet.Cells["C14"].Value = ketQuaNoiSoi["XoangLe"].ToString();
                workSheet.Cells["C15"].Value = ketQuaNoiSoi["MiengThucQuan"].ToString();
                workSheet.Cells["C16"].Value = ketQuaNoiSoi["SunPheu"].ToString();
                workSheet.Cells["C17"].Value = ketQuaNoiSoi["DayThanh"].ToString();
                workSheet.Cells["C18"].Value = ketQuaNoiSoi["BangThanhThat"].ToString();

                workSheet.Cells["B20"].Value = ketQuaNoiSoi["KetLuan"].ToString();
                workSheet.Cells["B22"].Value = ketQuaNoiSoi["DeNghi"].ToString();

                workSheet.Cells["F24"].Value = string.Format("Ngày: {0}",
                    Convert.ToDateTime(ketQuaNoiSoi["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F28"].Value = ketQuaNoiSoi["TenBacSiNoiSoi"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaNoiSoiTaiMuiHongToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiMuiHongTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("          Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("          Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                if (ketQuaNoiSoi["LyDoKham"] != null && ketQuaNoiSoi["LyDoKham"] != DBNull.Value)
                    workSheet.Cells["A8"].Value = string.Format("Lý do khám: {0}", ketQuaNoiSoi["LyDoKham"].ToString());

                if (ketQuaNoiSoi["TenBacSiChiDinh"] != null && ketQuaNoiSoi["TenBacSiChiDinh"] != DBNull.Value)
                    workSheet.Cells["D8"].Value = string.Format("SBSCĐ: {0}", ketQuaNoiSoi["TenBacSiChiDinh"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaNoiSoi["Hinh1"] != null && ketQuaNoiSoi["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh1"]);

                if (ketQuaNoiSoi["Hinh2"] != null && ketQuaNoiSoi["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh2"]);

                if (ketQuaNoiSoi["Hinh3"] != null && ketQuaNoiSoi["Hinh3"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh3"]);

                if (ketQuaNoiSoi["Hinh4"] != null && ketQuaNoiSoi["Hinh4"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaNoiSoi["Hinh4"]);

                double left = 0.73803149606299212;
                double top = 195;
                double width = 128.50485241420625;
                double height = 129.2431496062992;

                if (hinhList.Count == 4)
                {
                    left = 0.73803149606299212;
                    top = 195;
                    width = 128.50485241420625;
                    height = 129.2431496062992;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 134.99997052444246;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 269.12107375824547;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);

                    left = 403.32905700657312;
                    workSheet.Shapes.AddPicture(hinhList[3], left, top, width, height);
                }
                else if (hinhList.Count == 3)
                {
                    left = 0.73803149606299212;
                    width = 163.64469756847291;
                    height = 171.0;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 184.49902827713433;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                    left = 368.15975936217569;
                    workSheet.Shapes.AddPicture(hinhList[2], left, top, width, height);
                }
                else if (hinhList.Count == 2)
                {
                    left = 96.134184222258924;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                    left = 272.01223594316349;
                    workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);
                }
                else
                {
                    left = 183.74839835587449;
                    width = 163.64469756847291;
                    height = 171.0; ;
                    workSheet.Cells["A10"].RowHeight = height;

                    workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);
                }

                workSheet.Cells["C13"].Value = ketQuaNoiSoi["OngTaiNgoai"].ToString();
                workSheet.Cells["C14"].Value = ketQuaNoiSoi["MangNhi"].ToString();
                workSheet.Cells["C15"].Value = ketQuaNoiSoi["NiemMac"].ToString();
                workSheet.Cells["C16"].Value = ketQuaNoiSoi["VachNgan"].ToString();
                workSheet.Cells["C17"].Value = ketQuaNoiSoi["KheTren"].ToString();
                workSheet.Cells["C18"].Value = ketQuaNoiSoi["KheGiua"].ToString();
                workSheet.Cells["C19"].Value = ketQuaNoiSoi["MomMoc_BongSang"].ToString();
                workSheet.Cells["C20"].Value = ketQuaNoiSoi["Vom"].ToString();
                workSheet.Cells["C21"].Value = ketQuaNoiSoi["Amydale"].ToString();
                workSheet.Cells["C22"].Value = ketQuaNoiSoi["ThanhQuan"].ToString();

                workSheet.Cells["B24"].Value = ketQuaNoiSoi["KetLuan"].ToString();
                workSheet.Cells["B26"].Value = ketQuaNoiSoi["DeNghi"].ToString();

                workSheet.Cells["F28"].Value = string.Format("Ngày: {0}",
                    Convert.ToDateTime(ketQuaNoiSoi["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F32"].Value = ketQuaNoiSoi["TenBacSiNoiSoi"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportThuocTonKhoToExcel(string exportFileName, List<ThuocResult> results)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\ThuocTonKhoTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                Hashtable htThuoc = new Hashtable();
                foreach (var result in results)
                {
                    if (htThuoc.ContainsKey(result.MaThuoc))
                    {
                        List<ThuocResult> thuocResuts = (List<ThuocResult>)htThuoc[result.MaThuoc];
                        thuocResuts.Add(result);
                    }
                    else
                    {
                        List<ThuocResult> thuocResults = new List<ThuocResult>();
                        thuocResults.Add(result);
                        htThuoc.Add(result.MaThuoc, thuocResults);
                    }
                }

                List<string> keys = new List<string>();
                foreach (string key in htThuoc.Keys)
                {
                    keys.Add(key);
                }

                keys.Sort();

                int rowIndex = 2;
                IRange range = null;
                foreach (string key in keys)
                {
                    List<ThuocResult> thuocResults = (List<ThuocResult>)htThuoc[key];
                    range = workSheet.Cells[string.Format("A{0}:A{1}", rowIndex + 1, rowIndex + thuocResults.Count)];

                    foreach (var thuocResult in thuocResults)
                    {
                        workSheet.Cells[rowIndex, 1].Value = thuocResult.TenLoThuoc;
                        workSheet.Cells[rowIndex, 2].Value = thuocResult.DonViTinh;

                        workSheet.Cells[rowIndex, 3].Value = thuocResult.SoLuongNhap;
                        workSheet.Cells[rowIndex, 4].Value = thuocResult.SoLuongXuat;
                        workSheet.Cells[rowIndex, 5].Value = thuocResult.SoLuongTon;
                        rowIndex++;
                    }

                    range.Merge();
                    range.Value = thuocResults[0].TenThuoc;
                    range.HorizontalAlignment = HAlign.Left;
                }

                range = workSheet.Cells[string.Format("A3:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportThuocTonKhoTheoKhoangThoiGianToExcel(string exportFileName, List<spThuocTonKhoResult> results, DateTime tuNgay, DateTime denNgay)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\ThuocTonKhoTheoKhoangThoiGianTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                workSheet.Cells["A2"].Value = string.Format("Từ ngày: {0} - Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 3;
                IRange range = null;
                foreach (var row in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = row.TenThuoc;
                    workSheet.Cells[rowIndex, 1].Value = row.DonViTinh;
                    workSheet.Cells[rowIndex, 2].Value = row.SoDu;
                    workSheet.Cells[rowIndex, 3].Value = row.SLNhap;
                    workSheet.Cells[rowIndex, 4].Value = row.SLXuat;
                    workSheet.Cells[rowIndex, 5].Value = row.SLTon;
                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("A4:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportCapCuuTonKhoToExcel(string exportFileName, List<spCapCuuTonKhoResult> results, DateTime tuNgay, DateTime denNgay)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\BaoCaoTonKhoCapCuuTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                workSheet.Cells["A2"].Value = string.Format("Từ ngày: {0} - Đến ngày: {1}", tuNgay.ToString("dd/MM/yyyy"), denNgay.ToString("dd/MM/yyyy"));

                int rowIndex = 3;
                IRange range = null;
                foreach (var row in results)
                {
                    workSheet.Cells[rowIndex, 0].Value = row.TenCapCuu;
                    workSheet.Cells[rowIndex, 1].Value = row.DonViTinh;
                    workSheet.Cells[rowIndex, 2].Value = row.SoDu;
                    workSheet.Cells[rowIndex, 3].Value = row.SLNhap;
                    workSheet.Cells[rowIndex, 4].Value = row.SLXuat;
                    workSheet.Cells[rowIndex, 5].Value = row.SLTon;
                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("A4:F{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportYKienKhachHangToExcel(string exportFileName, List<DataRow> yKienKhachHangList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\YKienKhachHangTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (DataRow row in yKienKhachHangList)
                {
                    string tenKhachHang = row["TenKhachHang"].ToString();
                    string soDienThoai = row["SoDienThoai"].ToString();
                    string diaChi = row["DiaChi"].ToString();
                    string yeuCau = row["YeuCau"].ToString();
                    DateTime ngayLienHe = Convert.ToDateTime(row["ContactDate"]);
                    string nguon = row["Nguon"].ToString();
                    string nguoiTao = row["NguoiTao"].ToString();
                    string huongGiaiQuyet = string.Empty;
                    string daXong = row["DaXongStr"].ToString();

                    if (row["KetLuan"] != null && row["KetLuan"] != DBNull.Value)
                        huongGiaiQuyet = row["KetLuan"].ToString();

                    string bacSiPhuTrach = string.Empty;
                    if (row["BacSiPhuTrach"] != null && row["BacSiPhuTrach"] != DBNull.Value)
                        bacSiPhuTrach = row["BacSiPhuTrach"].ToString();

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = ngayLienHe.ToString("dd/MM/yyyy HH:mm:ss"); ;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = tenKhachHang;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = soDienThoai;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = yeuCau;

                    range = workSheet.Cells[rowIndex, 4];
                    range.Value = huongGiaiQuyet;

                    range = workSheet.Cells[rowIndex, 5];
                    range.Value = nguoiTao;

                    range = workSheet.Cells[rowIndex, 6];
                    range.Value = bacSiPhuTrach;

                    range = workSheet.Cells[rowIndex, 7];
                    range.Value = daXong;

                    rowIndex++;
                    
                }

                range = workSheet.Cells[string.Format("A3:H{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportNhatKyLienHeCongTyToExcel(string exportFileName, List<DataRow> nhatKyLienHeCongTyList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\NhatKyLienHeCongTyTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                foreach (DataRow row in nhatKyLienHeCongTyList)
                {
                    DateTime ngayLienHe = Convert.ToDateTime(row["NgayGioLienHe"]);
                    string congTyLienHe = row["CongTyLienHe"].ToString();
                    string diaChi = string.Empty;
                    if (row["DiaChi"] != null && row["DiaChi"] != DBNull.Value)
                        diaChi = row["DiaChi"].ToString();

                    string nguoiLienHe = row["TenNguoiLienHe"].ToString();
                    string soDienThoaiLienHe = row["SoDienThoaiLienHe"].ToString();

                    string email = string.Empty;
                    if (row["Email"] != null && row["Email"] != DBNull.Value)
                        email = row["Email"].ToString();

                    string soNguoiKham = row["SoNguoiKham"].ToString();
                    
                    string thangKham = string.Empty;
                    if (row["ThangKham"] != null && row["ThangKham"] != DBNull.Value)
                        thangKham = row["ThangKham"].ToString();

                    string noiDungLienHe = row["NoiDungLienHe"].ToString();
                    string nhanVienLienHe = row["FullName"].ToString();

                    string lienHeCapNhat = string.Empty;
                    if (row["UpdatedDate"] != null && row["UpdatedDate"] != DBNull.Value)
                        lienHeCapNhat = Convert.ToDateTime(row["UpdatedDate"]).ToString("dd/MM/yyyy HH:mm:ss");

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = ngayLienHe.ToString("dd/MM/yyyy HH:mm:ss");

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = lienHeCapNhat;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = congTyLienHe;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = diaChi;

                    range = workSheet.Cells[rowIndex, 4];
                    range.Value = nguoiLienHe;

                    range = workSheet.Cells[rowIndex, 5];
                    range.Value = soDienThoaiLienHe;

                    range = workSheet.Cells[rowIndex, 6];
                    range.Value = email;

                    range = workSheet.Cells[rowIndex, 7];
                    range.Value = soNguoiKham;

                    range = workSheet.Cells[rowIndex, 8];
                    range.Value = thangKham;

                    range = workSheet.Cells[rowIndex, 9];
                    range.Value = noiDungLienHe;

                    range = workSheet.Cells[rowIndex, 10];
                    range.Value = nhanVienLienHe;

                    rowIndex++;

                }

                range = workSheet.Cells[string.Format("A3:K{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaSoiCTCToExcel(string exportFileName, DataRow patientRow, DataRow ketQuaSoiCTC)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaSoiCTCTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A6"].Value = string.Format("Họ tên: {0}", patientRow["FullName"].ToString());
                workSheet.Cells["D6"].Value = string.Format("Tuổi: {0}", patientRow["DobStr"].ToString());
                workSheet.Cells["F6"].Value = string.Format("          Giới tính: {0}", patientRow["GenderAsStr"].ToString());

                if (patientRow["FileNum"] != null && patientRow["FileNum"] != DBNull.Value)
                    workSheet.Cells["A7"].Value = string.Format("Địa chỉ: {0}", patientRow["FileNum"].ToString());

                if (patientRow["Mobile"] != null && patientRow["Mobile"] != DBNull.Value)
                    workSheet.Cells["D7"].Value = string.Format("ĐT: {0}", patientRow["Mobile"].ToString());

                if (patientRow["Occupation"] != null && patientRow["Occupation"] != DBNull.Value)
                    workSheet.Cells["F7"].Value = string.Format("          Nghề nghiệp: {0}", patientRow["Occupation"].ToString());

                List<byte[]> hinhList = new List<byte[]>();
                if (ketQuaSoiCTC["Hinh1"] != null && ketQuaSoiCTC["Hinh1"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaSoiCTC["Hinh1"]);

                if (ketQuaSoiCTC["Hinh2"] != null && ketQuaSoiCTC["Hinh2"] != DBNull.Value)
                    hinhList.Add((byte[])ketQuaSoiCTC["Hinh2"]);

                double left = 96.134184222258924;
                double top = 175;
                double width = 163.64469756847291;
                double height = 171.0; ;
                workSheet.Cells["A9"].RowHeight = height;

                workSheet.Shapes.AddPicture(hinhList[0], left, top, width, height);

                left = 272.01223594316349;
                workSheet.Shapes.AddPicture(hinhList[1], left, top, width, height);

                workSheet.Cells["C13"].Value = ketQuaSoiCTC["AmHo"].ToString();
                workSheet.Cells["C14"].Value = ketQuaSoiCTC["AmDao"].ToString();
                workSheet.Cells["C15"].Value = ketQuaSoiCTC["CTC"].ToString();
                workSheet.Cells["C16"].Value = ketQuaSoiCTC["BieuMoLat"].ToString();
                workSheet.Cells["C17"].Value = ketQuaSoiCTC["MoDem"].ToString();
                workSheet.Cells["C18"].Value = ketQuaSoiCTC["RanhGioiLatTru"].ToString();
                workSheet.Cells["C19"].Value = ketQuaSoiCTC["SauAcidAcetic"].ToString();
                workSheet.Cells["C20"].Value = ketQuaSoiCTC["SauLugol"].ToString();

                workSheet.Cells["B22"].Value = ketQuaSoiCTC["KetLuan"].ToString();
                workSheet.Cells["B24"].Value = ketQuaSoiCTC["DeNghi"].ToString();

                workSheet.Cells["F26"].Value = string.Format("Ngày: {0}",
                    Convert.ToDateTime(ketQuaSoiCTC["NgayKham"]).ToString("dd/MM/yyyy"));

                workSheet.Cells["F30"].Value = ketQuaSoiCTC["FullName"].ToString();

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportGiaVonDichVuToExcel(string exportFileName, List<DataRow> giaVonDichVuList)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\GiaVonDichVuTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;
                IRange range;
                foreach (DataRow row in giaVonDichVuList)
                {
                    string serviceName = row["Name"].ToString();
                    double giaVon = Convert.ToDouble(row["GiaVon"]);
                    DateTime ngayApDung = Convert.ToDateTime(row["NgayApDung"]);

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = stt;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = serviceName;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = giaVon;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = ngayApDung.ToString("dd/MM/yyyy");

                    rowIndex++;
                    stt++;
                }

                range = workSheet.Cells[string.Format("A3:D{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaXetNghiemCellDyn3200ToExcel(string exportFileName, DataRow patientRow, DateTime fromDate, DateTime toDate, 
            List<string> uncheckedList, bool isPrint, bool hasLine, ref bool isData, ref DateTime maxNgayXN, ref List<string> keys)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string patientGUID = patientRow["PatientGUID"].ToString();
                string maBenhNhan = patientRow["FileNum"].ToString();
                string ngaySinh = patientRow["DobStr"].ToString();
                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string tenBenhNhan = patientRow["FullName"].ToString();
                string diaChi = patientRow["Address"].ToString();
                double rowHeight = 21;

                Result result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemCellDyn3200List(fromDate, toDate, patientGUID, ngaySinh, gioiTinh);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemCellDyn3200List"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemCellDyn3200List"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaXetNghiemCellDyn3200Template.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = maBenhNhan;
                workSheet.Cells["B3"].Value = tenBenhNhan;
                workSheet.Cells["B4"].Value = ngaySinh;
                workSheet.Cells["E4"].Value = string.Format("       Sex: {0}", gioiTinh);
                workSheet.Cells["B5"].Value = diaChi;

                int rowIndex = 8;
                IRange range;
                workSheet.Cells["A8:E8"].RowHeight = rowHeight;
                workSheet.Cells["A8:E8"].VerticalAlignment = VAlign.Center;

                if (!hasLine)
                {
                    range = workSheet.Cells["A7:E8"];
                    range.Borders.LineStyle = LineStyle.None;
                }
                else
                {
                    range = workSheet.Cells["A7:E8"];
                    range.Borders.Color = Color.Gray;
                }

                DataTable dtKQXN = result.QueryResult as DataTable;
                int groupID = 0;

                keys = new List<string>();

                isData = false;
                maxNgayXN = DateTime.MinValue;
                //List<DataRow> percentRows = new List<DataRow>();

                foreach (DataRow row in dtKQXN.Rows)
                {
                    string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                    if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                        continue;

                    isData = true;
                    DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                    if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                    keys.Add(chiTietKQXNGUID);

                    string tenXetNghiem = string.Empty;
                    double testResult = 0;
                    string testPercent = string.Empty;
                    int gID = Convert.ToInt32(row["GroupID"]);
                    byte tinhTrang = 0;
                    string binhThuong = string.Empty;

                    //if (groupID != 0 && groupID != gID)
                    //{
                    //    foreach (DataRow r in percentRows)
                    //    {
                    //        tenXetNghiem = string.Format("{0}%", r["Fullname"].ToString());
                    //        testResult = Convert.ToDouble(r["TestPercent"]);
                    //        tinhTrang = Convert.ToByte(r["TinhTrang"]);
                    //        binhThuong = r["Percent"].ToString();
                    //        int index = binhThuong.IndexOf(" ", 0);
                    //        if (index >= 0) binhThuong = binhThuong.Substring(index + 1);

                    //        range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                    //        range.Merge();
                    //        range.HorizontalAlignment = HAlign.Left;
                    //        range.Value = tenXetNghiem;
                    //        range.RowHeight = rowHeight;
                    //        range.VerticalAlignment = VAlign.Center;
                    //        //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                    //        range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                    //        range.Merge();
                    //        range.HorizontalAlignment = HAlign.Center;
                    //        range.Value = testResult;
                    //        range.RowHeight = rowHeight;
                    //        range.VerticalAlignment = VAlign.Center;
                    //        if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                    //        workSheet.Cells[rowIndex, 4].Value = binhThuong;
                    //        workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                    //        workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                    //        workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                    //        //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                    //        if (hasLine)
                    //        {
                    //            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                    //            range.Borders.LineStyle = LineStyle.Continuous;
                    //            range.Borders.Color = Color.Black;
                    //        }

                    //        rowIndex++;
                    //    }

                    //    percentRows.Clear();
                    //}

                    groupID = gID;

                    tenXetNghiem = row["Fullname"].ToString();
                    testResult = Convert.ToDouble(row["TestResult"]);
                    //testPercent = row["TestPercent"].ToString();
                    tinhTrang = Convert.ToByte(row["TinhTrang"]);
                    binhThuong = row["BinhThuong"].ToString();

                    range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Left;
                    range.Value = tenXetNghiem;
                    range.RowHeight = rowHeight;
                    range.VerticalAlignment = VAlign.Center;

                    if (tenXetNghiem.ToUpper() == "WBC" || tenXetNghiem.ToUpper() == "RBC" ||
                        tenXetNghiem.ToUpper() == "PLT")
                        range.Font.Bold = true;
                    //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                    range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                    range.Merge();
                    range.HorizontalAlignment = HAlign.Center;
                    range.Value = testResult;
                    range.RowHeight = rowHeight;
                    range.VerticalAlignment = VAlign.Center;
                    if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                    //if (testPercent.Trim() != string.Empty)
                    //    percentRows.Add(row);    

                    workSheet.Cells[rowIndex, 4].Value = binhThuong;
                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                    workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                    workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                    //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                    if (hasLine)
                    {
                        range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                        range.Borders.LineStyle = LineStyle.Continuous;
                        range.Borders.Color = Color.Gray;
                    }

                    rowIndex++;
                }

                if (isData)
                {
                    range = workSheet.Cells[string.Format("E{0}", rowIndex + 2)];
                    range.Value = string.Format("Report date: {0}", maxNgayXN.ToString("dd/MM/yyyy"));
                    range.Font.Italic = true;
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                }
                

                if (isPrint)
                {
                    Result r = XetNghiem_CellDyn3200Bus.UpdateDaIn(keys);
                    if (!r.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaIn"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaIn"));
                        return false;
                    }
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportKetQuaXetNghiemSinhToExcel(string exportFileName, DataRow patientRow, DateTime fromDate, DateTime toDate, 
            List<string> uncheckedList, bool isPrint, bool hasLine, ref bool isData, ref DateTime maxNgayXN, ref List<string> hitachi917Keys, ref List<string> manualKeys)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string patientGUID = patientRow["PatientGUID"].ToString();
                string ngaySinh = patientRow["DobStr"].ToString();
                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string maBenhNhan = patientRow["FileNum"].ToString();
                string tenBenhNhan = patientRow["FullName"].ToString();
                string diaChi = patientRow["Address"].ToString();
                double rowHeight = 21;

                Result result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemSinhHoaList(fromDate, toDate, patientGUID, ngaySinh, gioiTinh);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemSinhHoaList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemSinhHoaList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = maBenhNhan;
                workSheet.Cells["B3"].Value = tenBenhNhan;
                workSheet.Cells["B4"].Value = ngaySinh;
                workSheet.Cells["E4"].Value = string.Format("      Sex: {0}", gioiTinh);
                workSheet.Cells["B5"].Value = diaChi;

                int rowIndex = 8;
                IRange range;

                workSheet.Cells["A8:E8"].RowHeight = rowHeight;
                workSheet.Cells["A8:E8"].VerticalAlignment = VAlign.Center;
                workSheet.Cells["A8"].Value = "TEST";

                if (!hasLine)
                {
                    range = workSheet.Cells["A7:E8"];
                    range.Borders.LineStyle = LineStyle.None;
                }
                else
                {
                    range = workSheet.Cells["A7:E8"];
                    range.Borders.Color = Color.Gray;
                }

                DataTable dtKQXN = result.QueryResult as DataTable;
                DataRow[] rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.Biochemistry.ToString()), "Fullname");
                hitachi917Keys = new List<string>();
                manualKeys = new List<string>();
                maxNgayXN = DateTime.MinValue;
                isData = false;
                if (rows != null && rows.Length > 0)
                {

                    List<DataRow> sinhHoaRows = new List<DataRow>();
                    foreach (DataRow row in rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                            continue;

                        sinhHoaRows.Add(row);
                    }

                    if (sinhHoaRows.Count > 0)
                    {
                        foreach (DataRow row in sinhHoaRows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                            if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                                continue;

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            isData = true;
                            string loaiXN = row["LoaiXN"].ToString();
                            if (loaiXN == "Manual")
                                manualKeys.Add(chiTietKQXNGUID);
                            else
                                hitachi917Keys.Add(chiTietKQXNGUID);

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

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Left;
                            range.Value = tenXetNghiem;
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Center;
                            if (isNumeric)
                                range.Value = testResult;
                            else
                                range.Value = row["TestResult"].ToString();

                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            workSheet.Cells[rowIndex, 4].Value = binhThuong;
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                            workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                            if (hasLine)
                            {
                                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                    }
                }

                rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.Urine.ToString()), "Fullname");
                if (rows != null && rows.Length > 0)
                {
                    List<DataRow> urineRows = new List<DataRow>();
                    foreach (DataRow row in rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                            continue;

                        urineRows.Add(row);
                    }

                    if (urineRows.Count > 0)
                    {
                        if (isData)
                        {
                            workSheet.Cells[rowIndex, 0].Value = "URINE (NƯỚC TIỂU)";
                            workSheet.Cells[rowIndex, 0].RowHeight = 26.25;
                            workSheet.Cells[rowIndex, 0].VerticalAlignment = VAlign.Center;
                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Merge();
                            range.Font.Bold = true;
                            rowIndex++;

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "TEST";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "RESULT";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            workSheet.Cells[rowIndex, 4].Value = "NORMAL";
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Font.Bold = true;
                            range.HorizontalAlignment = HAlign.Center;

                            if (hasLine)
                            {
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }
                            
                            rowIndex++;
                        }
                        else
                        {
                            workSheet.Cells[rowIndex - 2, 0].Value = "URINE (NƯỚC TIỂU)";
                        }

                        foreach (DataRow row in urineRows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                            if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                                continue;

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            isData = true;
                            string loaiXN = row["LoaiXN"].ToString();
                            if (loaiXN == "Manual")
                                manualKeys.Add(chiTietKQXNGUID);
                            else
                                hitachi917Keys.Add(chiTietKQXNGUID);

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

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Left;
                            range.Value = tenXetNghiem;
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Center;
                            if (isNumeric)
                                range.Value = testResult;
                            else
                                range.Value = row["TestResult"].ToString();

                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            workSheet.Cells[rowIndex, 4].Value = binhThuong;
                            workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                            if (hasLine)
                            {
                                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                    }
                }

                rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.MienDich.ToString()), "Fullname");
                if (rows != null && rows.Length > 0)
                {
                    List<DataRow> mienDichRows = new List<DataRow>();
                    foreach (DataRow row in rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                            continue;

                        mienDichRows.Add(row);
                    }

                    if (mienDichRows.Count > 0)
                    {
                        if (isData)
                        {
                            workSheet.Cells[rowIndex, 0].Value = "MIỄN DỊCH";
                            workSheet.Cells[rowIndex, 0].RowHeight = 26.25;
                            workSheet.Cells[rowIndex, 0].VerticalAlignment = VAlign.Center;
                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Merge();
                            range.Font.Bold = true;
                            rowIndex++;

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "TEST";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "RESULT";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            workSheet.Cells[rowIndex, 4].Value = "NORMAL";
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Font.Bold = true;
                            range.HorizontalAlignment = HAlign.Center;

                            if (hasLine)
                            {
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                        else
                        {
                            workSheet.Cells[rowIndex - 2, 0].Value = "MIỄN DỊCH";
                        }

                        foreach (DataRow row in mienDichRows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                            if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                                continue;

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            isData = true;
                            string loaiXN = row["LoaiXN"].ToString();
                            if (loaiXN == "Manual")
                                manualKeys.Add(chiTietKQXNGUID);
                            else
                                hitachi917Keys.Add(chiTietKQXNGUID);

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

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Left;
                            range.Value = tenXetNghiem;
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Center;
                            if (isNumeric)
                                range.Value = testResult;
                            else
                                range.Value = row["TestResult"].ToString();

                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            workSheet.Cells[rowIndex, 4].Value = binhThuong;
                            workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                            if (hasLine)
                            {
                                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                    }
                }

                //rows = dtKQXN.Select(string.Format("Type = '{0}'", LoaiXetNghiem.SoiTuoiHuyetTrang.ToString()), "Fullname");
                if (rows != null && rows.Length > 0)
                {
                    List<DataRow> soiTuoiHuyetTrangRows = new List<DataRow>();
                    foreach (DataRow row in rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                            continue;

                        soiTuoiHuyetTrangRows.Add(row);
                    }

                    if (soiTuoiHuyetTrangRows.Count > 0)
                    {
                        if (isData)
                        {
                            workSheet.Cells[rowIndex, 0].Value = "SOI TƯƠI HUYẾT TRẮNG";
                            workSheet.Cells[rowIndex, 0].RowHeight = 26.25;
                            workSheet.Cells[rowIndex, 0].VerticalAlignment = VAlign.Center;
                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Merge();
                            range.Font.Bold = true;
                            rowIndex++;

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "TEST";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.Value = "RESULT";
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            workSheet.Cells[rowIndex, 4].Value = "NORMAL";
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;

                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Font.Bold = true;
                            range.HorizontalAlignment = HAlign.Center;

                            if (hasLine)
                            {
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                        else
                        {
                            workSheet.Cells[rowIndex - 2, 0].Value = "SOI TƯƠI HUYẾT TRẮNG";
                        }

                        foreach (DataRow row in soiTuoiHuyetTrangRows)
                        {
                            string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                            if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                                continue;

                            DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                            if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                            isData = true;
                            string loaiXN = row["LoaiXN"].ToString();
                            if (loaiXN == "Manual")
                                manualKeys.Add(chiTietKQXNGUID);
                            else
                                hitachi917Keys.Add(chiTietKQXNGUID);

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

                            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Left;
                            range.Value = tenXetNghiem;
                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                            range.Merge();
                            range.HorizontalAlignment = HAlign.Center;
                            if (isNumeric)
                                range.Value = testResult;
                            else
                                range.Value = row["TestResult"].ToString();

                            range.RowHeight = rowHeight;
                            range.VerticalAlignment = VAlign.Center;

                            if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                            workSheet.Cells[rowIndex, 4].Value = binhThuong;
                            workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;
                            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                            //if (tinhTrang == (byte)TinhTrang.BatThuong) workSheet.Cells[rowIndex, 4].Font.Bold = true;

                            if (hasLine)
                            {
                                range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                                range.Borders.LineStyle = LineStyle.Continuous;
                                range.Borders.Color = Color.Gray;
                            }

                            rowIndex++;
                        }
                    }
                }

                if (isData)
                {
                    range = workSheet.Cells[string.Format("E{0}", rowIndex + 2)];
                    range.Value = string.Format("Report date: {0}", maxNgayXN.ToString("dd/MM/yyyy"));
                    range.Font.Italic = true;
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                }

                if (isPrint)
                {
                    if (hitachi917Keys.Count > 0)
                    {
                        Result r = XetNghiem_Hitachi917Bus.UpdateDaIn(hitachi917Keys);
                        if (!r.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaIn"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaIn"));
                            return false;
                        }
                    }

                    if (manualKeys.Count > 0)
                    {
                        Result r = KetQuaXetNghiemTayBus.UpdateDaIn(manualKeys);
                        if (!r.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaIn"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaIn"));
                            return false;
                        }
                    }
                }
                
                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        private static void InitHeader(IWorksheet workSheet, bool hasLine, int rowIndex, string nhomXetNghiem)
        {
            double rowHeight = 21;
            workSheet.Cells[rowIndex, 0].Value = nhomXetNghiem;
            workSheet.Cells[rowIndex, 0].RowHeight = 26.25;
            workSheet.Cells[rowIndex, 0].VerticalAlignment = VAlign.Center;
            IRange range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
            range.Merge();
            range.Font.Bold = true;
            rowIndex++;

            range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
            range.Merge();
            range.Value = "TEST";
            range.RowHeight = rowHeight;
            range.VerticalAlignment = VAlign.Center;

            range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
            range.Merge();
            range.Value = "RESULT";
            range.RowHeight = rowHeight;
            range.VerticalAlignment = VAlign.Center;

            workSheet.Cells[rowIndex, 4].Value = "NORMAL";
            workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
            workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;

            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
            range.Font.Bold = true;
            range.HorizontalAlignment = HAlign.Center;

            if (hasLine)
            {
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Color = Color.Gray;
            }
        }

        public static bool ExportKetQuaXetNghiemTongHopExcel(string exportFileName, List<DataRow> selectedNhomXNList, DataRow patientRow, DateTime fromDate, DateTime toDate,
            List<string> uncheckedList, bool isPrint, bool hasLine, ref bool isData, ref DateTime maxNgayXN, ref List<string> cellDyn3200Keys,
            ref List<string> hitachi917Keys, ref List<string> manualKeys)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                cellDyn3200Keys = new List<string>();
                hitachi917Keys = new List<string>();
                manualKeys = new List<string>();
                string patientGUID = patientRow["PatientGUID"].ToString();
                string ngaySinh = patientRow["DobStr"].ToString();
                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string maBenhNhan = patientRow["FileNum"].ToString();
                string tenBenhNhan = patientRow["FullName"].ToString();
                string diaChi = patientRow["Address"].ToString();
                double rowHeight = 21;

                Result result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList(fromDate, toDate, patientGUID, ngaySinh, gioiTinh);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["B2"].Value = maBenhNhan;
                workSheet.Cells["B3"].Value = tenBenhNhan;
                workSheet.Cells["B4"].Value = ngaySinh;
                workSheet.Cells["E4"].Value = string.Format("      Sex: {0}", gioiTinh);
                workSheet.Cells["B5"].Value = diaChi;

                int rowIndex = 6;
                IRange range;

                DataTable dtKQXN = result.QueryResult as DataTable;
                isData = false;
                foreach (DataRow drNhomXN in selectedNhomXNList)
                {
                    string nhomXetNghiem = drNhomXN["GroupName"].ToString();
                    string type = drNhomXN["Type"].ToString();

                    DataRow[] rows = dtKQXN.Select(string.Format("GroupName = '{0}'", nhomXetNghiem));
                    if (rows == null || rows.Length <= 0) continue;

                    List<DataRow> xetNghiemList = new List<DataRow>();
                    foreach (DataRow row in rows)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();
                        if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID)) continue;

                        xetNghiemList.Add(row);
                    }

                    if (xetNghiemList.Count <= 0) continue;

                    InitHeader(workSheet, hasLine, rowIndex, nhomXetNghiem);
                    rowIndex += 2;

                    foreach (DataRow row in xetNghiemList)
                    {
                        string chiTietKQXNGUID = row["ChiTietKQXNGUID"].ToString();

                        DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                        if (ngayXN > maxNgayXN) maxNgayXN = ngayXN;

                        isData = true;
                        string loaiXN = row["LoaiXN"].ToString();
                        if (loaiXN == "Manual")
                            manualKeys.Add(chiTietKQXNGUID);
                        else if (loaiXN == "Hitachi917")
                            hitachi917Keys.Add(chiTietKQXNGUID);
                        else
                            cellDyn3200Keys.Add(chiTietKQXNGUID);

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

                        range = workSheet.Cells[string.Format("A{0}:B{0}", rowIndex + 1)];
                        range.Merge();
                        range.HorizontalAlignment = HAlign.Left;
                        range.Value = tenXetNghiem;
                        range.RowHeight = rowHeight;
                        range.VerticalAlignment = VAlign.Center;

                        if (tenXetNghiem.ToUpper() == "WBC" || tenXetNghiem.ToUpper() == "RBC" || tenXetNghiem.ToUpper() == "PLT")
                            range.Font.Bold = true;

                        range = workSheet.Cells[string.Format("C{0}:D{0}", rowIndex + 1)];
                        range.Merge();
                        range.HorizontalAlignment = HAlign.Center;
                        if (isNumeric) range.Value = testResult;
                        else range.Value = row["TestResult"].ToString();

                        range.RowHeight = rowHeight;
                        range.VerticalAlignment = VAlign.Center;

                        if (tinhTrang == (byte)TinhTrang.BatThuong) range.Font.Bold = true;

                        workSheet.Cells[rowIndex, 4].Value = binhThuong;
                        workSheet.Cells[rowIndex, 4].RowHeight = rowHeight;
                        workSheet.Cells[rowIndex, 4].VerticalAlignment = VAlign.Center;
                        workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Center;

                        if (hasLine)
                        {
                            range = workSheet.Cells[string.Format("A{0}:E{0}", rowIndex + 1)];
                            range.Borders.LineStyle = LineStyle.Continuous;
                            range.Borders.Color = Color.Gray;
                        }

                        rowIndex++;
                    }
                }

                if (isData)
                {
                    range = workSheet.Cells[string.Format("E{0}", rowIndex + 2)];
                    range.Value = string.Format("Report date: {0}", maxNgayXN.ToString("dd/MM/yyyy"));
                    range.Font.Italic = true;
                    range.Font.Bold = true;
                    range.HorizontalAlignment = HAlign.Center;
                }

                if (isPrint)
                {
                    if (cellDyn3200Keys.Count > 0)
                    {
                        Result r = XetNghiem_CellDyn3200Bus.UpdateDaIn(cellDyn3200Keys);
                        if (!r.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaIn"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaIn"));
                            return false;
                        }
                    }

                    if (hitachi917Keys.Count > 0)
                    {
                        Result r = XetNghiem_Hitachi917Bus.UpdateDaIn(hitachi917Keys);
                        if (!r.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaIn"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaIn"));
                            return false;
                        }
                    }

                    if (manualKeys.Count > 0)
                    {
                        Result r = KetQuaXetNghiemTayBus.UpdateDaIn(manualKeys);
                        if (!r.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaIn"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaIn"));
                            return false;
                        }
                    }
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachBenhNhanDenKhamToExcel(string exportFileName, DataTable dtBenhNhan, bool isDenKham)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\DanhSachBenhNhanDenKhamTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                int stt = 1;

                if (!isDenKham)
                    workSheet.Cells["A1"].Value = "DANH SÁCH BỆNH NHÂN CHƯA ĐẾN KHÁM";

                foreach (DataRow row in dtBenhNhan.Rows)
                {
                    DateTime ngayKham = DateTime.Now; 
                    if (row["NgayKham"] != null && row["NgayKham"] != DBNull.Value)
                        ngayKham = Convert.ToDateTime(row["NgayKham"]);

                    string maBenhNhan = row["FileNum"].ToString();
                    string tenBenhNhan = row["FullName"].ToString();
                    string ngaySinh = string.Empty;
                    if (row["DobStr"] != null && row["DobStr"] != DBNull.Value)
                        ngaySinh = row["DobStr"].ToString();

                    string gioiTinh = string.Empty;
                    if (row["GenderAsStr"] != null && row["GenderAsStr"] != DBNull.Value)
                        gioiTinh = row["GenderAsStr"].ToString();

                    string mobile = row["Mobile"] as string;

                    string diaChi = string.Empty;
                    if (row["Address"] != null && row["Address"] != DBNull.Value)
                        diaChi = row["Address"].ToString();

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = stt;

                    if (isDenKham)
                    {
                        range = workSheet.Cells[rowIndex, 1];
                        range.Value = ngayKham.ToString("dd/MM/yyyy");
                    }

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = maBenhNhan;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = tenBenhNhan;

                    range = workSheet.Cells[rowIndex, 4];
                    range.Value = ngaySinh;

                    range = workSheet.Cells[rowIndex, 5];
                    range.Value = gioiTinh;

                    range = workSheet.Cells[rowIndex, 6];
                    range.Value = mobile;

                    range = workSheet.Cells[rowIndex, 7];
                    range.Value = diaChi;

                    rowIndex++;
                    stt++;
                }

                range = workSheet.Cells[string.Format("A3:H{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachDichVuXuatPhieuThuToExcel(string exportFileName, DataTable dtDichVu)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                string excelTemplateName = string.Format("{0}\\Templates\\DanhSachDichVuXuatPhieuThuTemplate.xls", Application.StartupPath);
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                IRange range;
                int stt = 1;
                foreach (DataRow row in dtDichVu.Rows)
                {
                    string ngayXuat = Convert.ToDateTime(row["ReceiptDate"]).ToString("dd/MM/yyyy");
                    string tenDichVu = row["Name"].ToString();
                    string tenBenhNhan = row["FullName"].ToString();
                    string maBenhNhan = row["FileNum"].ToString();
                    string ngaySinh = string.Empty;
                    if (row["DobStr"] != null && row["DobStr"] != DBNull.Value)
                        ngaySinh = row["DobStr"].ToString();

                    string gioiTinh = string.Empty;
                    if (row["GenderAsStr"] != null && row["GenderAsStr"] != DBNull.Value)
                        gioiTinh = row["GenderAsStr"].ToString();

                    string diaChi = string.Empty;
                    if (row["Address"] != null && row["Address"] != DBNull.Value)
                        diaChi = row["Address"].ToString();

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = ngayXuat;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = tenDichVu;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = tenBenhNhan;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = maBenhNhan;

                    range = workSheet.Cells[rowIndex, 4];
                    range.Value = ngaySinh;

                    range = workSheet.Cells[rowIndex, 5];
                    range.Value = gioiTinh;

                    range = workSheet.Cells[rowIndex, 6];
                    range.Value = diaChi;

                    rowIndex++;
                    stt++;
                }

                range = workSheet.Cells[string.Format("A3:G{0}", rowIndex)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachNhanVienToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DanhSachNhanVienTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string tenNhanVien = row["FullName"].ToString();
                    
                    string ngaySinh = row["DobStr"].ToString();
                    string gioiTinh = row["GenderAsStr"].ToString();
                    string cmnd = string.Empty;
                    if (row["IdentityCard"] != null && row["IdentityCard"] != DBNull.Value)
                        cmnd = row["IdentityCard"].ToString();

                    string diaChi = string.Empty;
                    if (row["Address"] != null && row["Address"] != DBNull.Value)
                        diaChi = row["Address"].ToString();

                    string homePhone = string.Empty;
                    if (row["HomePhone"] != null && row["HomePhone"] != DBNull.Value)
                        homePhone = row["HomePhone"].ToString();

                    string workPhone = string.Empty;
                    if (row["WorkPhone"] != null && row["WorkPhone"] != DBNull.Value)
                        workPhone = row["HomePhone"].ToString();

                    string mobile = string.Empty;
                    if (row["Mobile"] != null && row["Mobile"] != DBNull.Value)
                        mobile = row["Mobile"].ToString();

                    string email = string.Empty;
                    if (row["Email"] != null && row["Email"] != DBNull.Value)
                        email = row["Email"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = tenNhanVien;
                    workSheet.Cells[rowIndex, 1].Value = diaChi;
                    workSheet.Cells[rowIndex, 2].Value = ngaySinh;
                    workSheet.Cells[rowIndex, 3].Value = gioiTinh;
                    workSheet.Cells[rowIndex, 4].Value = cmnd;
                    workSheet.Cells[rowIndex, 5].Value = homePhone;
                    workSheet.Cells[rowIndex, 6].Value = workPhone;
                    workSheet.Cells[rowIndex, 7].Value = mobile;
                    workSheet.Cells[rowIndex, 8].Value = email;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A3:I{0}", checkedRows.Count + 2)];
                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("B3:B{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("C3:H{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("I3:I{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachThuocToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DanhSachThuocTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string maThuoc = string.Empty;
                    if (row["MaThuoc"] != null && row["MaThuoc"] != DBNull.Value)
                        maThuoc = row["MaThuoc"].ToString();

                    string tenThuoc = row["TenThuoc"].ToString();

                    string hamLuong = string.Empty;
                    if (row["HamLuong"] != null && row["HamLuong"] != DBNull.Value)
                        hamLuong = row["HamLuong"].ToString();

                    string dvt = string.Empty;
                    if (row["DonViTinh"] != null && row["DonViTinh"] != DBNull.Value)
                        dvt = row["DonViTinh"].ToString();

                    string ghiChu = string.Empty;
                    if (row["Note"] != null && row["Note"] != DBNull.Value)
                        ghiChu = row["Note"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = maThuoc;
                    workSheet.Cells[rowIndex, 1].Value = tenThuoc;
                    workSheet.Cells[rowIndex, 2].Value = hamLuong;
                    workSheet.Cells[rowIndex, 3].Value = dvt;
                    workSheet.Cells[rowIndex, 4].Value = ghiChu;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A3:E{0}", checkedRows.Count + 2)];
                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("B3:B{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("C3:D{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("E3:E{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachDichVuToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\DanhSachDichVuCoGiaTemplate.xls", Application.StartupPath);
            if (!Global.AllowShowServiePrice) 
                excelTemplateName = string.Format("{0}\\Templates\\DanhSachDichVuKhongGiaTemplate.xls", Application.StartupPath);

            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string maDichVu = string.Empty;
                    if (row["Code"] != null && row["Code"] != DBNull.Value)
                        maDichVu = row["Code"].ToString();

                    string tenDichVu = row["Name"].ToString();

                    int gia = Convert.ToInt32(row["Price"]);

                    string loaiDichVu = string.Empty;
                    if (row["TypeStr"] != null && row["TypeStr"] != DBNull.Value)
                        loaiDichVu = row["TypeStr"].ToString();

                    string nhomThucHien = string.Empty;
                    if (row["StaffTypeStr"] != null && row["StaffTypeStr"] != DBNull.Value)
                        nhomThucHien = row["StaffTypeStr"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = maDichVu;
                    workSheet.Cells[rowIndex, 1].Value = tenDichVu;
                    if (Global.AllowShowServiePrice)
                    {
                        workSheet.Cells[rowIndex, 2].Value = gia;
                        workSheet.Cells[rowIndex, 3].Value = loaiDichVu;
                        workSheet.Cells[rowIndex, 4].Value = nhomThucHien;
                    }
                    else
                    {
                        workSheet.Cells[rowIndex, 2].Value = loaiDichVu;
                        workSheet.Cells[rowIndex, 3].Value = nhomThucHien;
                    }
                    
                    rowIndex++;
                    stt++;
                }

                IRange range = null;
                if (Global.AllowShowServiePrice)
                    range = workSheet.Cells[string.Format("A3:E{0}", checkedRows.Count + 2)];
                else
                    range = workSheet.Cells[string.Format("A3:D{0}", checkedRows.Count + 2)];

                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("B3:B{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                if (Global.AllowShowServiePrice)
                {
                    range = workSheet.Cells[string.Format("C3:C{0}", checkedRows.Count + 2)];
                    range.HorizontalAlignment = HAlign.Right;
                    range.VerticalAlignment = VAlign.Top;

                    range = workSheet.Cells[string.Format("D3:E{0}", checkedRows.Count + 2)];
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                }
                else
                {
                    range = workSheet.Cells[string.Format("C3:D{0}", checkedRows.Count + 2)];
                    range.HorizontalAlignment = HAlign.Center;
                    range.VerticalAlignment = VAlign.Top;
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportCongTacNgoaiGioToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\CongTacNgoaiGioTemplate.xls", Application.StartupPath);

            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 5;

                foreach (DataRow row in checkedRows)
                {
                    DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                    string tenNhanVien = row["TenNguoiLam"] as string;
                    string mucDich = row["MucDich"] as string;
                    DateTime gioVao = Convert.ToDateTime(row["GioVao"]);
                    DateTime gioRa = Convert.ToDateTime(row["GioRa"]);
                    string ketQuaDanhGia = row["KetQuaDanhGia"] as string;
                    string nguoiDeXuat = row["TenNguoiDeXuat"].ToString();
                    string ghiChu = row["GhiChu"] as string;

                    workSheet.Cells[rowIndex, 0].Value = ngay;
                    workSheet.Cells[rowIndex, 1].Value = tenNhanVien;
                    workSheet.Cells[rowIndex, 2].Value = mucDich;
                    workSheet.Cells[rowIndex, 3].Value = gioVao;
                    workSheet.Cells[rowIndex, 4].Value = gioRa;
                    workSheet.Cells[rowIndex, 5].Value = ketQuaDanhGia;
                    workSheet.Cells[rowIndex, 6].Value = nguoiDeXuat;
                    workSheet.Cells[rowIndex, 7].Value = ghiChu;

                    rowIndex++;
                }

                IRange range = workSheet.Cells[string.Format("A6:H{0}", checkedRows.Count + 5)];
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.VerticalAlignment = VAlign.Top;
                range.RowHeight = 18.75;

                rowIndex = checkedRows.Count + 5;
                range = workSheet.Cells[rowIndex, 0];
                range.Value = "* Đề nghị người đề xuất cập nhật  và nộp lại cho bộ phận kế toán trong ngày.";
                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Left;
                range.VerticalAlignment = VAlign.Center;
                range.RowHeight = 18.75;

                range = workSheet.Cells[string.Format("F{0}:H{0}", checkedRows.Count + 6)];
                range.Merge();
                range.HorizontalAlignment = HAlign.Center;
                range.Value = string.Format("TP. Hồ Chí Minh, ngày    tháng    năm {0}", DateTime.Now.Year);

                range = workSheet.Cells[string.Format("F{0}:H{0}", checkedRows.Count + 7)];
                range.Merge();
                range.HorizontalAlignment = HAlign.Center;
                range.Value = "Giám đốc";
                range.VerticalAlignment = VAlign.Center;
                range.RowHeight = 18.75;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportLichKhamToExcel(string exportFileName, SourceGrid2.Grid dgLichKham, string thangStr, string namStr)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\LichKhamTemplate.xls", Application.StartupPath);

            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 3;

                List<int> noDataRows = new List<int>();
                workSheet.Cells[0, 0].Value = string.Format("LỊCH KHÁM THÁNG {0} NĂM {1} TẠI PHÒNG KHÁM VIGOR HEALTH", thangStr, namStr);
                for (int i = 2; i < dgLichKham.RowsCount; i++)
                {
                    SourceGrid2.Cells.Real.Cell cell = dgLichKham[i, 0] as SourceGrid2.Cells.Real.Cell;
                    if (cell == null)
                    {
                        if (rowIndex > 3)
                        {
                            bool isUp = true;
                            foreach (int index in noDataRows)
                            {
                                if (index + 1 == rowIndex)
                                {
                                    isUp = false;
                                    break;
                                }
                            }

                            if (isUp)
                            {
                                noDataRows.Add(rowIndex);
                                rowIndex++;
                            }
                        }
                        continue;
                    }

                    bool hasData = false;
                    for (int j = 1; j < dgLichKham.ColumnsCount; j++)
                    {
                        object value = dgLichKham[i, j].Value;
                        if (value != null && value.ToString().Trim() != string.Empty)
                        {
                            hasData = true;
                            break;
                        }
                    }

                    if (!hasData) continue;

                    DateTime ngay = Convert.ToDateTime(cell.Tag);
                    for (int j = 0; j < dgLichKham.ColumnsCount; j++)
                    {
                        IRange range = workSheet.Cells[rowIndex, j];
                        range.Value = dgLichKham[i, j].Value;
                        range.Borders.Color = Color.Black;
                        range.Borders.LineStyle = LineStyle.Continuous;
                    }

                    rowIndex++;
                }

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        public static bool ExportDanhSachKhoCapCuuToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            string excelTemplateName = string.Format("{0}\\Templates\\KhoCapCuuTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 2;
                int stt = 1;

                foreach (DataRow row in checkedRows)
                {
                    string tenCapCuu = row["TenCapCuu"].ToString();
                    string dvt = string.Empty;
                    if (row["DonViTinh"] != null && row["DonViTinh"] != DBNull.Value)
                        dvt = row["DonViTinh"].ToString();

                    string ghiChu = string.Empty;
                    if (row["Note"] != null && row["Note"] != DBNull.Value)
                        ghiChu = row["Note"].ToString();

                    workSheet.Cells[rowIndex, 0].Value = tenCapCuu;
                    workSheet.Cells[rowIndex, 1].Value = dvt;
                    workSheet.Cells[rowIndex, 2].Value = ghiChu;
                    rowIndex++;
                    stt++;
                }

                IRange range = workSheet.Cells[string.Format("A3:C{0}", checkedRows.Count + 2)];
                range.WrapText = false;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A3:A{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                range = workSheet.Cells[string.Format("B3:B{0}", checkedRows.Count + 2)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                range = workSheet.Cells[string.Format("C3:C{0}", checkedRows.Count + 2)];
                range.WrapText = true;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }
    }
}
