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
using MM.Bussiness;
using MM.Databasae;
using MM.Exports;

namespace MM.Controls
{
    public partial class uChiTietPhieuThuDichVu : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uChiTietPhieuThuDichVu()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUID()
        {
            btnPrintPreview.Enabled = AllowPrint;
            btnPrint.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUID();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayChTietPhieuThuDichVuListProc));
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

        private void OnDisplayChTietPhieuThuDichVuList()
        {
            Result result = ReceiptBus.GetChiTietPhieuThuDichVuList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgChiTiet.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetChiTietPhieuThuDichVuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetChiTietPhieuThuDichVuList"));
            }
        }

        private void OnPrint(bool isPreview)
        {
            if (dgChiTiet.RowCount <= 0) return;

            string exportFileName = string.Format("{0}\\Temp\\DanhSachDichVuXuatPhieuThu.xls", Application.StartupPath);
            if (isPreview)
            {
                DataTable dt = dgChiTiet.DataSource as DataTable;
                if (ExportExcel.ExportDanhSachDichVuXuatPhieuThuToExcel(exportFileName, dt))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachDichVuXuatPhieuThuTemplate));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                    }
                }
            }
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = dgChiTiet.DataSource as DataTable;
                    if (ExportExcel.ExportDanhSachDichVuXuatPhieuThuToExcel(exportFileName, dt))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachDichVuXuatPhieuThuTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
            }
        }

        private void OnExportExcel()
        {
            if (dgChiTiet.RowCount <= 0) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = dgChiTiet.DataSource as DataTable;
                ExportExcel.ExportDanhSachDichVuXuatPhieuThuToExcel(dlg.FileName, dt);
            }
        }
        #endregion

        #region Window Event Handlers
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
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên bệnh nhân.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayChTietPhieuThuDichVuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayChTietPhieuThuDichVuList();
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
