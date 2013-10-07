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
    public partial class uBaoCaoThuocTonKhoTheoKhoangThoiGian : uBase
    {
        #region Members
        private List<string> _maThuocList = new List<string>();
        private List<string> _maThuocStrList = new List<string>();
        private string _maThuocs = string.Empty;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        #endregion

        #region Constructor
        public uBaoCaoThuocTonKhoTheoKhoangThoiGian()
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
            _uThuocList.ClearData();
        }

        private void UpdateGUI()
        {
            btnExportExcel.Enabled = AllowExport;

            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _uThuocList.DisplayAsThread();
        }

        private void OnView()
        {
            Result result = ReportBus.GetThuocTonKhoTheoKhoangThoiGian(_tuNgay, _denNgay, _maThuocStrList);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    tabReport.SelectedTabIndex = 1;
                    dgThuocTonKho.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetThuocTonKhoTheoKhoangThoiGian"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetThuocTonKhoTheoKhoangThoiGian"));
            }
        }

        private void ViewAsThread()
        {
            try
            {
                if (_uThuocList.CheckedRows == null || _uThuocList.CheckedRows.Count <= 0)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thuốc để xem báo cáo.", IconType.Information);
                    return;
                }

                if (dtpkDenNgay.Value < dtpkTuNgay.Value)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                    dtpkTuNgay.Focus();
                    return;
                }

                _maThuocList.Clear();
                _maThuocs = string.Empty;
                _maThuocStrList.Clear();
                foreach (DataRow row in _uThuocList.CheckedRows)
                {
                    _maThuocList.Add(row["MaThuoc"].ToString());
                    string temp = _maThuocs + string.Format("{0},", row["MaThuoc"].ToString());
                    if (temp.Length < 4000)
                        _maThuocs += string.Format("{0},", row["MaThuoc"].ToString());
                    else
                    {
                        _maThuocs = _maThuocs.Substring(0, _maThuocs.Length - 1);
                        _maThuocStrList.Add(_maThuocs);
                        _maThuocs = string.Format("{0},", row["MaThuoc"].ToString());
                    }
                }

                if (_maThuocs != string.Empty)
                {
                    _maThuocs = _maThuocs.Substring(0, _maThuocs.Length - 1);
                    _maThuocStrList.Add(_maThuocs);
                }

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
            if (_uThuocList.CheckedRows == null || _uThuocList.CheckedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thuốc để xem báo cáo.", IconType.Information);
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
                _maThuocList.Clear();
                _maThuocs = string.Empty;
                _maThuocStrList.Clear();
                foreach (DataRow row in _uThuocList.CheckedRows)
                {
                    _maThuocList.Add(row["MaThuoc"].ToString());
                    string temp = _maThuocs + string.Format("{0},", row["MaThuoc"].ToString());
                    if (temp.Length < 4000)
                        _maThuocs += string.Format("{0},", row["MaThuoc"].ToString());
                    else
                    {
                        _maThuocs = _maThuocs.Substring(0, _maThuocs.Length - 1);
                        _maThuocStrList.Add(_maThuocs);
                        _maThuocs = string.Format("{0},", row["MaThuoc"].ToString());
                    }
                }

                if (_maThuocs != string.Empty)
                {
                    _maThuocs = _maThuocs.Substring(0, _maThuocs.Length - 1);
                    _maThuocStrList.Add(_maThuocs);
                }

                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                Result result = ReportBus.GetThuocTonKhoTheoKhoangThoiGian(_tuNgay, _denNgay, _maThuocStrList);
                if (result.IsOK)
                {
                    List<spThuocTonKhoResult> thuocTonKhoList = (List<spThuocTonKhoResult>)result.QueryResult;
                    ExportExcel.ExportThuocTonKhoTheoKhoangThoiGianToExcel(dlg.FileName, thuocTonKhoList, _tuNgay, _denNgay);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetThuocTonKhoTheoKhoangThoiGian"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetThuocTonKhoTheoKhoangThoiGian"));
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
