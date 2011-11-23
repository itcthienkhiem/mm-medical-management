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

namespace MM.Controls
{
    public partial class uPrintLabel : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uPrintLabel()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<DataRow> Members
        {
            get
            {
                if (_dataSource == null) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSource.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }

        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgMembers.DataSource = null;
        }

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

        private void OnSearchPatient()
        {
            UpdateChecked();

            chkChecked.Checked = false;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgMembers.DataSource = _dataSource;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            //FullName
            var results = from p in _dataSource.AsEnumerable()
                          where (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                          p.Field<string>("FullName") != null &&
                          p.Field<string>("FullName").Trim() != string.Empty
                          select p;

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgMembers.DataSource = newDataSource;
                return;
            }


            //FileNum
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0) &&
                          p.Field<string>("FileNum") != null &&
                          p.Field<string>("FileNum").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgMembers.DataSource = newDataSource;
                return;
            }

            dgMembers.DataSource = newDataSource;
        }

        private void UpdateChecked()
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSource.Rows)
                {
                    string patientGUID2 = row2["PatientGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }
        }

        private void OnPrint()
        {
            List<DataRow> members = this.Members;
            if (members == null || members.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng dánh dấu ít nhất 1 bệnh nhân.");
                return;
            }


        }
        #endregion

        #region Window Event Handlers
        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
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
