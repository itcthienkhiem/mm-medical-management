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
    public partial class uHoaDonThuocList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uHoaDonThuocList()
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
            Result result = HoaDonThuocBus.GetHoaDonThuocList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type);
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonThuocBus.GetHoaDonThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonThuocBus.GetHoaDonThuocList"));
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
                    deletedInvoiceList.Add(row["HoaDonThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedInvoiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = HoaDonThuocBus.DeleteHoaDonThuoc(deletedInvoiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonThuocBus.DeleteHoaDonThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonThuocBus.DeleteHoaDonThuoc"));
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
                    checkedInvoicetList.Add(row["HoaDonThuocGUID"].ToString());
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
                            foreach (string hoaDonThuocGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 1: Lưu"))
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
                                    if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 2: Giao người mua"))
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
                                    if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 3: Nội bộ"))
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
            dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(drInvoice, true);
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
            dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = dgInvoice.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["HoaDonThuocGUID"] = dlg.HoaDonThuoc.HoaDonThuocGUID.ToString();
                newRow["SoHoaDon"] = dlg.HoaDonThuoc.SoHoaDon;
                newRow["NgayXuatHoaDon"] = dlg.HoaDonThuoc.NgayXuatHoaDon;
                newRow["TenNguoiMuaHang"] = dlg.HoaDonThuoc.TenNguoiMuaHang;
                newRow["TenDonVi"] = dlg.HoaDonThuoc.TenDonVi;
                newRow["MaSoThue"] = dlg.HoaDonThuoc.MaSoThue;
                newRow["DiaChi"] = dlg.HoaDonThuoc.DiaChi;
                newRow["SoTaiKhoan"] = dlg.HoaDonThuoc.SoTaiKhoan;
                newRow["HinhThucThanhToan"] = dlg.HoaDonThuoc.HinhThucThanhToan;
                newRow["VAT"] = dlg.HoaDonThuoc.VAT;
                newRow["HinhThucThanhToanStr"] = ((PaymentType)dlg.HoaDonThuoc.HinhThucThanhToan) == PaymentType.TienMat ? "TM" : "CK";

                if (dlg.HoaDonThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.HoaDonThuoc.CreatedDate;

                if (dlg.HoaDonThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.HoaDonThuoc.CreatedBy.ToString();

                if (dlg.HoaDonThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.HoaDonThuoc.UpdatedDate;

                if (dlg.HoaDonThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.HoaDonThuoc.UpdatedBy.ToString();

                if (dlg.HoaDonThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.HoaDonThuoc.DeletedDate;

                if (dlg.HoaDonThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.HoaDonThuoc.DeletedBy.ToString();

                newRow["Status"] = dlg.HoaDonThuoc.Status;
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
