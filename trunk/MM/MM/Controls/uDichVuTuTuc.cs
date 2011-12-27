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

namespace MM.Controls
{
    public partial class uDichVuTuTuc : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private List<spDichVuTuTucResult> _results = null;
        #endregion

        #region Constructor
        public uDichVuTuTuc()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void InitData()
        {
            UpdateGUI();
        }

        private void ViewAsThread()
        {
            try
            {

                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 0, 0, 0);

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

        private void OnView()
        {
            Result result = ReportBus.GetDichVuTuTuc(_tuNgay, _denNgay);
            if (result.IsOK)
            {
                _results = (List<spDichVuTuTucResult>)result.QueryResult;
                ReportDataSource reportDataSource = new ReportDataSource("spDichVuTuTucResult", _results);

                MethodInvoker method = delegate
                {
                    _ucReportViewer.ViewReport("MM.Templates.rptDichVuTuTuc.rdlc", reportDataSource);
                    btnExportExcel.Enabled = AllowExport && _results.Count > 0;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDichVuTuTuc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDichVuTuTuc"));
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
