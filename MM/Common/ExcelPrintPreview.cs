using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;

namespace MM.Common
{
    public class ExcelPrintPreview
    {
        #region Members
        private static CultureInfo _currentCultural = System.Threading.Thread.CurrentThread.CurrentCulture;
        #endregion

        public static Excel.Application ExcelInit()
        {
            Excel.Application excelApp = null;
            try
            {
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                excelApp.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return excelApp;
        }

        public static void ExcelTerminal(Excel.Application excelApp)
        {
            try
            {
                if (excelApp == null) return;

                excelApp.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp.Workbooks);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PageSetup GetPageSetup(string template)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            string fileName = string.Empty;
            PageSetup p = null;

            try
            {
                switch (template)
                {
                    case "Theo dõi thực hiện":
                        fileName = string.Format("{0}\\Templates\\CheckListTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân đến khám":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanDenKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ hợp đồng":
                        fileName = string.Format("{0}\\Templates\\DichVuHopDongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ tự túc":
                        fileName = string.Format("{0}\\Templates\\DichVuTuTucTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Doanh thu theo ngày":
                        fileName = string.Format("{0}\\Templates\\DoanhThuTheoNgayTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Giá vốn dịch vụ":
                        fileName = string.Format("{0}\\Templates\\GiaVonDichVuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Hóa đơn giá trị gia tăng":
                        fileName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi thanh quản":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiHongThanhQuanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi mũi":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiMuiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai mũi họng":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiMuiHongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tổng quát":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi cổ tử cung":
                        fileName = string.Format("{0}\\Templates\\KetQuaSoiCTCTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Khám sức khỏe tổng quát":
                        fileName = string.Format("{0}\\Templates\\KhamSucKhoeTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Nhật kí liên hệ công ty":
                        fileName = string.Format("{0}\\Templates\\NhatKyLienHeCongTyTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\PhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Triệu chứng":
                        fileName = string.Format("{0}\\Templates\\SymptomTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Thuốc tồn kho theo khoảng thời gian":
                        fileName = string.Format("{0}\\Templates\\ThuocTonKhoTheoKhoangThoiGianTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc":
                        fileName = string.Format("{0}\\Templates\\ToaThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc chung":
                        fileName = string.Format("{0}\\Templates\\ToaThuocChungTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc sản khoa":
                        fileName = string.Format("{0}\\Templates\\ToaThuocSanKhoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Ý kiến khách hàng":
                        fileName = string.Format("{0}\\Templates\\YKienKhachHangTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm CellDyn3200":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemCellDyn3200Template.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm sinh hóa":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                }

                if (fileName != string.Empty && File.Exists(fileName))
                {
                    object objOpt = System.Reflection.Missing.Value;
                    excelApp = ExcelInit();
                    workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                               objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);

                    int sheetCount = workBook.Sheets.Count;
                    Excel.Worksheet workSheet = null;
                    int i = 0;
                    while (i <= sheetCount)
                    {
                        try
                        {
                            workSheet = workBook.Sheets[i];
                            break;
                        }
                        catch
                        {
                            i++;
                        }
                    }

                    p = new PageSetup();
                    p.LeftMargin = workSheet.PageSetup.LeftMargin / 72;
                    p.RightMargin = workSheet.PageSetup.RightMargin / 72;
                    p.TopMargin = workSheet.PageSetup.TopMargin / 72;
                    p.BottomMargin = workSheet.PageSetup.BottomMargin / 72;

                    excelApp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }

                ExcelTerminal(excelApp);
            }

            return p;
        }

        public static void PrintPreview(string fileName, PageSetup p)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);

                if (p != null)
                {
                    //if (p.TopMargin != 0 || p.LeftMargin != 0 || p.RightMargin != 0 || p.BottomMargin != 0)
                    {
                        int sheetCount = workBook.Sheets.Count;
                        Excel.Worksheet workSheet = null;
                        int i = 0;
                        while (i <= sheetCount)
                        {
                            try
                            {
                                workSheet = workBook.Sheets[i];
                                break;
                            }
                            catch
                            {
                                i++;
                            }
                        }

                        workSheet.PageSetup.LeftMargin = p.GetLeftMargin();
                        workSheet.PageSetup.RightMargin = p.GetRightMargin();
                        workSheet.PageSetup.TopMargin = p.GetTopMargin();
                        workSheet.PageSetup.BottomMargin = p.GetBottomMargin();
                    }
                }

                excelApp.Visible = true;
                workBook.PrintPreview(objOpt);
                excelApp.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }
                
                ExcelTerminal(excelApp);
            }
        }

        public static string ConvertToExcelPrinterFriendlyName(string printerName)
        {
            var key = Registry.CurrentUser;
            var subkey = key.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices");

            var value = subkey.GetValue(printerName);
            if (value == null) throw new Exception(string.Format("Device not found: {0}", printerName));

            var portName = value.ToString().Substring(9);  //strip away the winspool, 

            return string.Format("{0} on {1}", printerName, portName);
        }

        public static void Print(string fileName, string printerName, PageSetup p)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);

                if (p != null)
                {
                    //if (p.TopMargin != 0 || p.LeftMargin != 0 || p.RightMargin != 0 || p.BottomMargin != 0)
                    {
                        int sheetCount = workBook.Sheets.Count;
                        Excel.Worksheet workSheet = null;
                        int i = 0;
                        while (i <= sheetCount)
                        {
                            try
                            {
                                workSheet = workBook.Sheets[i];
                                break;
                            }
                            catch
                            {
                                i++;
                            }
                        }

                        workSheet.PageSetup.LeftMargin = p.GetLeftMargin();
                        workSheet.PageSetup.RightMargin = p.GetRightMargin();
                        workSheet.PageSetup.TopMargin = p.GetTopMargin();
                        workSheet.PageSetup.BottomMargin = p.GetBottomMargin();
                    }
                }

                excelApp.Visible = false;
                excelApp.ActivePrinter = ConvertToExcelPrinterFriendlyName(printerName);
                workBook.PrintOut(objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }

                ExcelTerminal(excelApp);
            }
        }

        public static void SetCulturalWithEN_US()
        {
            if (_currentCultural.Name != "en-US")
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        public static void SetCulturalWithCurrent()
        {
            if (_currentCultural.Name != "en-US")
                System.Threading.Thread.CurrentThread.CurrentCulture = _currentCultural;
        }
    }
}
