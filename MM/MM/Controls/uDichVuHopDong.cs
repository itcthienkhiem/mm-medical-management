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
    public partial class uDichVuHopDong : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uDichVuHopDong()
        {
            InitializeComponent();
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
            if (cboHopDong.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hợp đồng.", IconType.Information);
                return;
            }

            string contractGUID = cboHopDong.SelectedValue.ToString();


        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            OnView();
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
        #endregion
    }
}
