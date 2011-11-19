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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgMembers : dlgBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgMembers()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<DataRow> Members
        {
            get
            {

                return null;
            }

        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    dgMembers.DataSource = _dataSource;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        #endregion

        #region Window Event Handlers
        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {

        }

        private void dlgMembers_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgMembers_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgMembers.Focus();

                if (dgMembers.SelectedRows != null && dgMembers.SelectedRows.Count > 0)
                {
                    int index = dgMembers.SelectedRows[0].Index;
                    if (index < dgMembers.RowCount - 1)
                    {
                        index++;
                        dgMembers.CurrentCell = dgMembers[1, index];
                        dgMembers.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgMembers.Focus();

                if (dgMembers.SelectedRows != null && dgMembers.SelectedRows.Count > 0)
                {
                    int index = dgMembers.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgMembers.CurrentCell = dgMembers[1, index];
                        dgMembers.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayPatientList();
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
