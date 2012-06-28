using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uHoaDonHopDongList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenKhacHang = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uHoaDonHopDongList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnExportInvoice.Enabled = AllowExport;
        }

        public void ClearData()
        {
            dgInvoice.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenKhacHang = txtTenKhachHang.Text;

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
            Result result = HoaDonHopDongBus.GetHoaDonHopDongList(_isFromDateToDate, _fromDate, _toDate, _tenKhacHang, _type);
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonHopDongBus.GetHoaDonHopDongList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonHopDongBus.GetHoaDonHopDongList"));
            }
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
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();

                    foreach (DataRow row in deletedRows)
                    {
                        string soHoaDon = row["SoHoaDon"].ToString();
                        string hoaDonHopDongGUID = row["HoaDonHopDongGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(soHoaDon, 1);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedInvoiceList.Add(hoaDonHopDongGUID);
                        }
                    }

                    if (deletedInvoiceList.Count > 0)
                    {
                        Result result = HoaDonHopDongBus.DeleteHoaDonHopDong(deletedInvoiceList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string hoaDonHopDongGUID = row["HoaDonHopDongGUID"].ToString();
                                if (deletedInvoiceList.Contains(hoaDonHopDongGUID))
                                    dt.Rows.Remove(row);
                            }
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonHopDongBus.DeleteHoaDonHopDong"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonHopDongBus.DeleteHoaDonHopDong"));
                        }
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
                    checkedInvoicetList.Add(row["HoaDonHopDongGUID"].ToString());
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
                            foreach (string hoaDonHopDongGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportHoaDonHopDongToExcel(exportFileName, hoaDonHopDongGUID, "                                   Liên 1: Lưu"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HDGTGTTemplate));
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
                                    if (ExportExcel.ExportHoaDonHopDongToExcel(exportFileName, hoaDonHopDongGUID, "                                   Liên 2: Giao người mua"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HDGTGTTemplate));
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
                                    if (ExportExcel.ExportHoaDonHopDongToExcel(exportFileName, hoaDonHopDongGUID, "                                   Liên 3: Nội bộ"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HDGTGTTemplate));
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
            dlgHoaDonHopDong dlg = new dlgHoaDonHopDong(drInvoice, true);
            dlg.ShowDialog();
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenKhachHang.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            if (raTenKhachHang.Checked && txtTenKhachHang.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên tên khách hàng.", IconType.Information);
                txtTenKhachHang.Focus();
                return;
            }

            DisplayAsThread();
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

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            dlgHoaDonHopDong dlg = new dlgHoaDonHopDong(null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
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

        private void dgInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
