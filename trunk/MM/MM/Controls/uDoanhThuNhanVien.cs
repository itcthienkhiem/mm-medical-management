using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Reporting.WinForms;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uDoanhThuNhanVien : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uDoanhThuNhanVien()
        {
            InitializeComponent();
            dtpkToDate.Value = DateTime.Now;
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDocStaffListProc));
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

        private void OnDisplayDocStaffList()
        {
            Result result = DocStaffBus.GetDocStaffList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    DataRow newRow = dt.NewRow();
                    newRow["DocStaffGUID"] = Guid.Empty.ToString();
                    newRow["FullName"] = "--------All--------";
                    dt.Rows.InsertAt(newRow, 0);

                    cboNhanVien.DataSource = dt;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private bool CheckInfo()
        {
            if (dtpkFromDate.Value > dtpkToDate.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkFromDate.Focus();
                return false;
            }

            return true;
        }

        private void OnView()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!CheckInfo()) return;
            DateTime fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
            DateTime toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
            string docStaffGUID = cboNhanVien.SelectedValue.ToString();

            Result result = null;
            if (raTongHop.Checked)
            {
                result = ReportBus.GetDoanhThuNhanVienTongHop(fromDate, toDate, docStaffGUID);
                if (result.IsOK)
                {
                    ReportDataSource reportDataSource = new ReportDataSource("spDoanhThuNhanVienTongHopResult", 
                        (List<spDoanhThuNhanVienTongHopResult>)result.QueryResult);
                    _ucReportViewer.ViewReport("MM.Templates.rptDoanhThuNhanVienTongHop.rdlc", reportDataSource);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienTongHop"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienTongHop"));
                }
            }
            else
            {
                result = ReportBus.GetDoanhThuNhanVienChiTiet(fromDate, toDate, docStaffGUID);
                if (result.IsOK)
                {
                    ReportDataSource reportDataSource = new ReportDataSource("spDoanhThuNhanVienChiTietResult",
                        (List<spDoanhThuNhanVienChiTietResult>)result.QueryResult);
                    _ucReportViewer.ViewReport("MM.Templates.rptDoanhThuNhanVienChiTiet.rdlc", reportDataSource);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienChiTiet"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienChiTiet"));
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            OnView();
        }
        #endregion

        #region Working Thread
        private void OnDisplayDocStaffListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDocStaffList();
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
