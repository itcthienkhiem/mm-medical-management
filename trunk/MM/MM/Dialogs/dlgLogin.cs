using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgLogin : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void DisplayUserListAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayUserListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayUserList()
        {
            Result result = DocStaffBus.GetUserList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = Const.AdminGUID;
                newRow[1] = "Admin";
                dt.Rows.InsertAt(newRow, 0);

                MethodInvoker method = delegate
                {
                    cboUserName.DataSource = dt;
                    cboUserName.DisplayMember = "Fullname";
                    cboUserName.ValueMember = "DocStaffGUID";
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetUserList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetUserList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgLogin_Load(object sender, EventArgs e)
        {
            DisplayUserListAsThread();
        }

        private void dlgLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Global.UserGUID = cboUserName.SelectedValue.ToString();
                Global.Fullname = cboUserName.Text;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayUserListProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayUserList();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message);
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
