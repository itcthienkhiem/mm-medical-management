﻿using System;
using System.Collections.Generic;
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
                workSheet.Cells["A2"].Value = string.Format("Số: {0}", receipt.ReceiptCode);
                workSheet.Cells["B6"].Value = string.Format("Họ tên: {0}", receipt.FullName);
                workSheet.Cells["B7"].Value = string.Format("Mã bệnh nhân: {0}", receipt.FileNum);
                workSheet.Cells["B8"].Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (receipt.Address != null) workSheet.Cells["B9"].Value = string.Format("Địa chỉ: {0}", receipt.Address);
                else workSheet.Cells["B9"].Value = "Địa chỉ:";

                int rowIndex = 11;
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

                result = InvoiceBus.GetInvoiceDetailList(invoice.ReceiptGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["E3"].Value = string.Format("Số: {0}", invoice.InvoiceCode);

                DateTime dt = DateTime.Now;
                string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                string strYear = dt.Year.ToString();

                workSheet.Cells["A3"].Value = lien;
                workSheet.Cells["A4"].Value = string.Format("                                   Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                workSheet.Cells["A11"].Value = string.Format("  Họ tên người mua hàng: {0}", invoice.FullName);
                workSheet.Cells["A12"].Value = string.Format("  Tên đơn vị: {0}", invoice.TenDonVi);
                workSheet.Cells["A13"].Value = string.Format("  Địa chỉ: {0}", invoice.Address);
                workSheet.Cells["A14"].Value = string.Format("  Số tài khoản: {0}", invoice.SoTaiKhoan);
                workSheet.Cells["A15"].Value = string.Format("  Hình thức thanh toán: {0}", invoice.HinhThucThanhToanStr);

                IRange range = null;
                DataTable dataSource = result.QueryResult as DataTable;
                foreach (DataRow row in dataSource.Rows)
                {
                    range = workSheet.Cells["A18"].EntireRow;
                    range.Insert(InsertShiftDirection.Down);
                }

                int no = 1;
                int rowIndex = 17;
                double totalPrice = 0;
                foreach (DataRow row in dataSource.Rows)
                {
                    string serviceName = row["Name"].ToString();
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

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = donViTinh;
                    range.HorizontalAlignment = HAlign.Center;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = soLuong;
                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 4];
                    if (donGia > 0)
                        range.Value = donGia.ToString("#,###");
                    else
                        range.Value = donGia.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    range = workSheet.Cells[rowIndex, 5];
                    if (thanhTien > 0)
                        range.Value = thanhTien.ToString("#,###");
                    else
                        range.Value = thanhTien.ToString();

                    range.HorizontalAlignment = HAlign.Right;
                    range.Font.Bold = false;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = totalPrice.ToString("#,###");
                else
                    range.Value = totalPrice.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Thuế suất GTGT: {0}%, Tiền thuế GTGT:", invoice.VAT);

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                double vat = (invoice.VAT.Value * totalPrice) / 100;
                vat = Math.Round(vat + 0.05);
                if (vat > 0)
                    range.Value = vat.ToString("#,###");
                else
                    range.Value = vat.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                double totalPayment = totalPrice + vat;
                if (totalPayment > 0)
                    range.Value = totalPayment.ToString("#,###");
                else
                    range.Value = totalPayment.ToString();

                rowIndex++;
                range = workSheet.Cells[string.Format("A{0}", rowIndex + 1)];
                range.Value = string.Format("  Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());

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
            string excelTemplateName = string.Format("{0}\\Templates\\ToaThuocTemplate.xls", Application.StartupPath);
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

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                IWorksheet workSheet = workBook.Worksheets[0];

                workSheet.Cells["A2"].Value = string.Format("Mã toa thuốc: {0}", toaThuoc.MaToaThuoc);
                workSheet.Cells["A3"].Value = string.Format("Ngày kê toa: {0}", toaThuoc.NgayKeToa.ToString("dd/MM/yyyy"));
                workSheet.Cells["A4"].Value = string.Format("Bác sĩ kê toa: {0}", toaThuoc.TenBacSi);
                workSheet.Cells["A5"].Value = string.Format("Bệnh nhân: {0}", toaThuoc.TenBenhNhan);

                int rowIndex = 6;

                DataTable dt = result.QueryResult as DataTable;
                int stt = 1;
                IRange range;
                foreach (DataRow row in dt.Rows)
                {
                    string tenThuoc = row["TenThuoc"] as string;
                    int ngayUong = Convert.ToInt32(row["SoNgayUong"]);
                    int lanTrongNgay = Convert.ToInt32(row["SoLanTrongNgay"]);
                    int lieuTrongLan = Convert.ToInt32(row["SoLuongTrongLan"]);
                    string note = row["Note"] as string;

                    range = workSheet.Cells[rowIndex, 0];
                    range.Value = stt;

                    range = workSheet.Cells[rowIndex, 1];
                    range.Value = tenThuoc;

                    range = workSheet.Cells[rowIndex, 2];
                    range.Value = ngayUong;

                    range = workSheet.Cells[rowIndex, 3];
                    range.Value = lanTrongNgay;

                    range = workSheet.Cells[rowIndex, 4];
                    range.Value = lieuTrongLan;

                    range = workSheet.Cells[rowIndex, 5];
                    range.Value = note.Replace("\r", "").Replace("\t", "");
                    range.WrapText = true;

                    rowIndex++;
                    stt++;
                }

                range = workSheet.Cells[string.Format("A7:F{0}", dt.Rows.Count + 6)];
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
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
                workSheet.Cells["A2"].Value = string.Format("Số: {0}", ptThuoc.MaPhieuThuThuoc);
                workSheet.Cells["B6"].Value = string.Format("Họ tên: {0}", ptThuoc.TenBenhNhan);
                workSheet.Cells["B7"].Value = string.Format("Mã bệnh nhân: {0}", ptThuoc.MaBenhNhan);
                workSheet.Cells["B8"].Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (ptThuoc.DiaChi != null) workSheet.Cells["B9"].Value = string.Format("Địa chỉ: {0}", ptThuoc.DiaChi);
                else workSheet.Cells["B9"].Value = "Địa chỉ:";

                int rowIndex = 11;
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
    }
}
