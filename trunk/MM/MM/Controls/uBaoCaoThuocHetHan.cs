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
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uBaoCaoThuocHetHan : uBase
    {
        #region Members
        private int _soNgayHetHan = 0;
        private List<string> _thuocKeyList = new List<string>();
        #endregion

        #region Constructor
        public uBaoCaoThuocHetHan()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _uThuocList.DisplayAsThread();
        }

        private void OnView()
        {
            Result result = ReportBus.GetThuocHetHanList(_soNgayHetHan, _thuocKeyList);
            if (result.IsOK)
            {
                ReportDataSource reportDataSource = new ReportDataSource("ThuocResult",
                    (List<ThuocResult>)result.QueryResult);

                MethodInvoker method = delegate
                {
                    tabReport.SelectedTabIndex = 1;
                    _ucReportViewer.ViewReport("MM.Templates.rptThuocHetHan.rdlc", reportDataSource);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetThuocHetHanList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetThuocHetHanList"));
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

                _thuocKeyList.Clear();
                _soNgayHetHan = (int)numSoNgayHetHan.Value;

                foreach (DataRow row in _uThuocList.CheckedRows)
                {
                    _thuocKeyList.Add(row["ThuocGUID"].ToString());
                }

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
