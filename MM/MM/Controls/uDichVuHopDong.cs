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
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;

namespace MM.Controls
{
    public partial class uDichVuHopDong : uBase
    {
        #region Members
        private string _contractGUID = string.Empty;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private int _type = 0;
        private List<spDichVuHopDongResult> _results = null;
        #endregion

        #region Constructor
        public uDichVuHopDong()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayContractListProc));
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

        private void OnDisplayContractList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    cboHopDong.DataSource = dt;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private void OnView()
        {
            Result result = ReportBus.GetDichVuHopDong(_contractGUID, _tuNgay, _denNgay, _type);
            if (result.IsOK)
            {
                _results = (List<spDichVuHopDongResult>)result.QueryResult;

                ReportDataSource reportDataSource = new ReportDataSource("spDichVuHopDongResult", _results);
                
                MethodInvoker method = delegate
                {
                    if (_results == null || _results.Count <= 0)
                        txtKetQua.Text = "0";
                    else
                        txtKetQua.Text = _results.Count.ToString();

                   _ucReportViewer.ViewReport("MM.Templates.rptDichVuHopDong.rdlc", reportDataSource);
                   btnExportExcel.Enabled = AllowExport && _results.Count > 0;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDichVuHopDong"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDichVuHopDong"));
            }
        }

        private void ViewAsThread()
        {
            try
            {

                if (cboHopDong.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hợp đồng.", IconType.Information);
                    return;
                }


                _contractGUID = cboHopDong.SelectedValue.ToString();
                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                if (raTatCa.Checked) _type = 0;
                else if (raChuaKham.Checked) _type = 1;
                else if (raDaKham.Checked) _type = 2;

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
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_results == null || _results.Count <= 0) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ExportExcel.ExportDichVuHopDongToExcel(dlg.FileName, _results);   
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayContractListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayContractList();
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
