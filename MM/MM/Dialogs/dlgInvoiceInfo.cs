using System;
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
using SpreadsheetGear.Windows.Forms;

namespace MM.Dialogs
{
    public partial class dlgInvoiceInfo : Form
    {
        #region Members
        private DataRow _drInvoice;
        private double _totalPrice = 0;
        private bool _isPrinted = false;
        private string _invoiceCode = string.Empty;
        #endregion

        #region Constructor
        public dlgInvoiceInfo(DataRow drInvoice)
        {
            InitializeComponent();
            _drInvoice = drInvoice;
            cboHinhThucThanhToan.SelectedIndex = 0;
            btnExportAndPrint.Enabled = Global.AllowPrintInvoice;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = InvoiceBus.GetInvoiceCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                _invoiceCode = Utility.GetCode("HĐ", count + 1, 7);
                lbInvoiceCode.Text = string.Format("Số: {0}", _invoiceCode);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("InvoiceBus.GetInvoiceCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceCount"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgDetail.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            GenerateCode();
            lbPatientName.Text = string.Format("Họ tên người mua hàng: {0}", _drInvoice["FullName"].ToString());
            lbAddress.Text = string.Format("Địa chỉ: {0}", _drInvoice["Address"].ToString());

            Result result = InvoiceBus.GetInvoiceDetailList(_drInvoice["ReceiptGUID"].ToString());
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgDetail.DataSource = dt;

                foreach (DataRow row in dt.Rows)
                {
                    double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                    _totalPrice += thanhTien;
                }

                if (_totalPrice > 0)
                    lbTotalAmount.Text = string.Format("{0}", _totalPrice.ToString("#,###"));

                double vat = ((double)numVAT.Value * _totalPrice) / 100;
                if (vat > 0)
                    lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

                double totalPayment = _totalPrice + vat;
                lbTotalPayment.Text = string.Format("{0}", totalPayment.ToString("#,###"));
                lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());

                RefreshNo();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceDetailList"));
            }
        }

        private bool ExportToExcel(string exportFileName, string invoiceGUID, string lien)
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
                    range.Value =totalPayment.ToString("#,###");
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

        private bool OnPrint(string invoiceGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            dlgPrintType dlg = new dlgPrintType();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string exportFileName = string.Format("{0}\\Temp\\HDGTGT.xls", Application.StartupPath);
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Lien1)
                    {
                        if (ExportToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (dlg.Lien2)
                    {
                        if (ExportToExcel(exportFileName, invoiceGUID, "                                   Liên 2: Giao người mua"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (dlg.Lien3)
                    {
                        if (ExportToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }
                }
            }

            return true;
        }

        private bool ExportInvoice()
        {
            try
            {
                Invoice invoice = new Invoice();
                invoice.ReceiptGUID = Guid.Parse(_drInvoice["ReceiptGUID"].ToString());
                invoice.InvoiceCode = _invoiceCode;
                invoice.InvoiceDate = DateTime.Now;
                invoice.TenDonVi = txtTenDonVi.Text;
                invoice.SoTaiKhoan = txtSoTaiKhoan.Text;
                invoice.HinhThucThanhToan = (byte)cboHinhThucThanhToan.SelectedIndex;
                invoice.VAT = (double)numVAT.Value;
                invoice.CreatedDate = DateTime.Now;
                invoice.CreatedBy = Guid.Parse(Global.UserGUID);
                invoice.Status = (byte)Status.Actived;

                Result result = InvoiceBus.InsertInvoice(invoice);
                if (result.IsOK)
                {
                    if (!_isPrinted) return true;
                    else if (OnPrint(invoice.InvoiceGUID.ToString()))
                        return true;
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.InsertInvoice"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.InsertInvoice"));
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }

            return false;
        }
        #endregion

        #region Window Event Handlers
        private void dlgInvoiceInfo_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgInvoiceInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!ExportInvoice()) e.Cancel = true;
                else
                    _drInvoice["IsExportedInvoice"] = true;
            }
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            _isPrinted = false;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnExportAndPrint_Click(object sender, EventArgs e)
        {
            _isPrinted = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dgDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }

        private void numVAT_ValueChanged(object sender, EventArgs e)
        {
            double vat = ((double)numVAT.Value * _totalPrice) / 100;
            vat = Math.Round(vat + 0.05);
            if (vat > 0)
                lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

            double totalPayment = _totalPrice + vat;
            lbTotalPayment.Text = string.Format("{0}", totalPayment.ToString("#,###"));
            lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());
        }

        private void numVAT_Leave(object sender, EventArgs e)
        {
            double vat = ((double)numVAT.Value * _totalPrice) / 100;
            vat = Math.Round(vat + 0.05);
            if (vat > 0)
                lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

            double totalPayment = _totalPrice + vat;
            lbTotalPayment.Text = string.Format("{0}", totalPayment.ToString("#,###"));
            lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());
        }

        private void numVAT_KeyUp(object sender, KeyEventArgs e)
        {
            string strVat = numVAT.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (strVat == null || strVat.Trim() == string.Empty)
                {
                    numVAT.Value = 0;
                    numVAT.Text = "0.0";
                }
            }
        }
        #endregion
    }
}
