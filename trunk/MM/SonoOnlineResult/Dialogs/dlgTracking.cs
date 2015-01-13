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

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgTracking : dlgBase
    {
        #region Members
        private string _username = string.Empty;
        private DateTime _from = DateTime.Now;
        private DateTime _to = DateTime.Now;
        #endregion

        #region Constructor
        public dlgTracking()
        {
            InitializeComponent();
            dtpkTo.Value = DateTime.Now;
            dtpkFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }
        #endregion

        #region UI Command
        public void LoadUserListAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnLoadUserListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnLoadUserList()
        {
            Result result = MySQL.GetAllUserLogonList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    DataRow row = dt.NewRow();
                    row["Username"] = "[All]";
                    dt.Rows.InsertAt(row, 0);

                    cboUserLogon.DataSource = dt;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetAllUserLogonList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SearchAsThread()
        {
            try
            {
                if (chkFrom.Checked)
                    _from = dtpkFrom.Value;
                else
                    _from = new DateTime(1900, 1, 1, 0, 0, 0);

                if (chkTo.Checked)
                    _to = dtpkTo.Value;
                else
                    _to = new DateTime(9000, 1, 1, 0, 0, 0);

                _username = string.Empty;
                if (cboUserLogon.SelectedValue != null)
                    _username = cboUserLogon.SelectedValue.ToString();

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
                base.ShowWaiting();

                lbCount.Text = string.Format("{0} row(s)", dgTracking.RowCount);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSearch()
        {
            Result result = MySQL.GetTracking(_username, _from, _to);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgTracking.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetTracking"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Window Event Handlers
        private void dlgTracking_Load(object sender, EventArgs e)
        {
            LoadUserListAsThread();
        }

        private void chkFrom_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFrom.Enabled = chkFrom.Checked;
            chkTo.Enabled = chkFrom.Checked;
            dtpkTo.Enabled = chkTo.Enabled & chkTo.Checked;
        }

        private void chkTo_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTo.Enabled = chkTo.Checked;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAsThread();
        }
        #endregion

        #region Working Thread
        private void OnLoadUserListProc(object state)
        {
            try
            {
                OnLoadUserList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSearchProc(object state)
        {
            try
            {
                OnSearch();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        

        
    }
}
