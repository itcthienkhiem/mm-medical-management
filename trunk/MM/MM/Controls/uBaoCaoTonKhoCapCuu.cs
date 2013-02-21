using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Reporting.WinForms;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Exports;

namespace MM.Controls
{
    public partial class uBaoCaoTonKhoCapCuu : uBase
    {
        #region Members
        private string _maCapCuus = string.Empty;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        #endregion

        #region Constructor
        public uBaoCaoTonKhoCapCuu()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            _uKhoCapCuu.ClearData();
        }

        private void UpdateGUI()
        {
            btnExportExcel.Enabled = AllowExport;

            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _uKhoCapCuu.DisplayAsThread();
        }

        private void OnView()
        {
            Result result = ReportBus.GetTonKhoCapCuu(_tuNgay, _denNgay, _maCapCuus);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    tabReport.SelectedTabIndex = 1;
                    dgCapCuuTonKho.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetTonKhoCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetTonKhoCapCuu"));
            }
        }

        private void ViewAsThread()
        {
            try
            {
                if (_uKhoCapCuu.CheckedRows == null || _uKhoCapCuu.CheckedRows.Count <= 0)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thông tin cấp cứu để xem báo cáo.", IconType.Information);
                    return;
                }

                if (dtpkDenNgay.Value < dtpkTuNgay.Value)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                    dtpkTuNgay.Focus();
                    return;
                }

                _maCapCuus = string.Empty;
                foreach (DataRow row in _uKhoCapCuu.CheckedRows)
                {
                    _maCapCuus += string.Format("{0},", row["MaCapCuu"].ToString());
                }

                _maCapCuus = _maCapCuus.Substring(0, _maCapCuus.Length - 1);

                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnViewProc));
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

        private void OnExportToExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_uKhoCapCuu.CheckedRows == null || _uKhoCapCuu.CheckedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thông tin cấp cứu để xem báo cáo.", IconType.Information);
                return;
            }

            if (dtpkDenNgay.Value < dtpkTuNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _maCapCuus = string.Empty;
                foreach (DataRow row in _uKhoCapCuu.CheckedRows)
                {
                    _maCapCuus += string.Format("{0},", row["MaCapCuu"].ToString());
                }

                _maCapCuus = _maCapCuus.Substring(0, _maCapCuus.Length - 1);

                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                Result result = ReportBus.GetTonKhoCapCuu(_tuNgay, _denNgay, _maCapCuus);
                if (result.IsOK)
                {
                    List<spCapCuuTonKhoResult> capCuuTonKhoList = (List<spCapCuuTonKhoResult>)result.QueryResult;
                    ExportExcel.ExportCapCuuTonKhoToExcel(dlg.FileName, capCuuTonKhoList, _tuNgay, _denNgay);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetTonKhoCapCuu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetTonKhoCapCuu"));
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }
        #endregion

        #region Working Thread
        private void OnViewProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnView();
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
