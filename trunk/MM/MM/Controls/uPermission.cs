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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uPermission : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPermission()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgLogon.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayUserLogonListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayUserLogonList()
        {
            Result result = LogonBus.GetUserListWithoutAdmin();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgLogon.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LogonBus.GetUserListWithoutAdmin"));
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetUserListWithoutAdmin"));
            }
        }

        private void OnAddUserLogon()
        {
            dlgAddUserLogon dlg = new dlgAddUserLogon();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {

            }
        }

        private void OnEditUserLogon()
        {

        }

        private void OnDeleteUserLogon()
        {

        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLogon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddUserLogon();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditUserLogon();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteUserLogon();
        }

        private void dgLogon_DoubleClick(object sender, EventArgs e)
        {
            OnEditUserLogon();
        }
        #endregion

        #region Working Thread
        private void OnDisplayUserLogonListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayUserLogonList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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
