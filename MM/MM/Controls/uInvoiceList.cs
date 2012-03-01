using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;
using MM.Dialogs;
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uInvoiceList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uInvoiceList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Commnad
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnExportInvoice.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInvoiceListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayInvoiceList()
        {
            Result result = InvoiceBus.GetInvoiceList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgInvoice.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceList"));
            }
        }

        public void ClearData()
        {
            dgInvoice.DataSource = null;
        }

        private void OnDeleteInvoice()
        {
            List<string> deletedInvoiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedInvoiceList.Add(row["invoiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedInvoiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = InvoiceBus.DeleteInvoices(deletedInvoiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.DeleteInvoices"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.DeleteInvoices"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần xóa.", IconType.Information);
        }

        private void OnPrint()
        {
            List<string> checkedInvoicetList = new List<string>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedInvoicetList.Add(row["invoiceGUID"].ToString());
                }
            }

            if (checkedInvoicetList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn in những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgPrintType dlg = new dlgPrintType();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (_printDialog.ShowDialog() == DialogResult.OK)
                        {
                            string exportFileName = string.Format("{0}\\Temp\\HDGTGT.xls", Application.StartupPath);
                            foreach (string invoiceGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }

                                if (dlg.Lien2)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 2: Giao người mua"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }

                                if (dlg.Lien3)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần in.", IconType.Information);
        }

        private void OnDisplayInvoiceInfo()
        {
            if (dgInvoice.SelectedRows == null || dgInvoice.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hóa đơn.", IconType.Information);
                return;
            }

            DataRow drInvoice = (dgInvoice.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgInvoiceInfo dlg = new dlgInvoiceInfo(drInvoice, true);
            dlg.ShowDialog();
        }
        #endregion

        #region Window Event Handlers
        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {
            OnDisplayInvoiceInfo();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên tên khách hàng.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            dlgInvoiceInfo dlg = new dlgInvoiceInfo(null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = dgInvoice.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["InvoiceGUID"] = dlg.Invoice.InvoiceGUID.ToString();
                newRow["InvoiceCode"] = dlg.Invoice.InvoiceCode;
                newRow["InvoiceDate"] = dlg.Invoice.InvoiceDate;
                newRow["TenNguoiMuaHang"] = dlg.Invoice.TenNguoiMuaHang;
                newRow["TenDonVi"] = dlg.Invoice.TenDonVi;
                newRow["MaSoThue"] = dlg.Invoice.MaSoThue;
                newRow["DiaChi"] = dlg.Invoice.DiaChi;
                newRow["SoTaiKhoan"] = dlg.Invoice.SoTaiKhoan;
                newRow["HinhThucThanhToan"] = dlg.Invoice.HinhThucThanhToan;
                newRow["VAT"] = dlg.Invoice.VAT;
                newRow["HinhThucThanhToanStr"] = ((PaymentType)dlg.Invoice.HinhThucThanhToan) == PaymentType.TienMat ? "TM" : "CK";

                if (dlg.Invoice.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Invoice.CreatedDate;

                if (dlg.Invoice.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Invoice.CreatedBy.ToString();

                if (dlg.Invoice.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Invoice.UpdatedDate;

                if (dlg.Invoice.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Invoice.UpdatedBy.ToString();

                if (dlg.Invoice.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Invoice.DeletedDate;

                if (dlg.Invoice.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Invoice.DeletedBy.ToString();

                newRow["Status"] = dlg.Invoice.Status;
                dt.Rows.Add(newRow);
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayInvoiceListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayInvoiceList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
