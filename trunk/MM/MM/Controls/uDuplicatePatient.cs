using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using System.Threading;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uDuplicatePatient : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public uDuplicatePatient()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get { return dgDuplicatePatient.DataSource; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnMerge.Enabled = AllowEdit;
        }

        public void ClearData()
        {
            dgDuplicatePatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDuplicatePatientListProc));
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

        private void OnDisplayDuplicatePatientList()
        {
            Result result = PatientBus.GetDuplicatePatientList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchPatient();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnSearchPatient()
        {
            List<DataRow> results = null;
            DataTable newDataSource = null;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDuplicatePatient.DataSource = newDataSource;
                if (dgDuplicatePatient.RowCount > 0) dgDuplicatePatient.Rows[0].Selected = true;
                _isAscending = true;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();
            newDataSource = _dataSource.Clone();

            if (chkMaBenhNhan.Checked)
            {
                //FileNum
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("FileNum") != null &&
                               p.Field<string>("FileNum").Trim() != string.Empty &&
                               (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);

                if (newDataSource.Rows.Count > 0)
                {
                    dgDuplicatePatient.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //FullName
                results = (from p in _dataSource.AsEnumerable()
                           where (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                           p.Field<string>("FullName") != null &&
                           p.Field<string>("FullName").Trim() != string.Empty
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);

                if (newDataSource.Rows.Count > 0)
                {
                    dgDuplicatePatient.DataSource = newDataSource;
                    return;
                }

                //HomePhone
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("HomePhone") != null &&
                           p.Field<string>("HomePhone").Trim() != string.Empty &&
                           (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);

                if (newDataSource.Rows.Count > 0)
                {
                    dgDuplicatePatient.DataSource = newDataSource;
                    return;
                }

                //WorkPhone
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("WorkPhone") != null &&
                               p.Field<string>("WorkPhone").Trim() != string.Empty &&
                               (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);

                if (newDataSource.Rows.Count > 0)
                {
                    dgDuplicatePatient.DataSource = newDataSource;
                    return;
                }

                //Mobile
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("Mobile") != null &&
                               p.Field<string>("Mobile").Trim() != string.Empty &&
                               (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);


                if (newDataSource.Rows.Count > 0)
                {
                    dgDuplicatePatient.DataSource = newDataSource;
                    return;
                }
            }

            dgDuplicatePatient.DataSource = newDataSource;
        }
        #endregion
        
        #region Working Thread
        private void OnDisplayDuplicatePatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDuplicatePatientList();
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

        #region Window Event Handers
        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;

            if (dgDuplicatePatient.SelectedRows == null || dgDuplicatePatient.SelectedRows.Count <= 1)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 2 bệnh nhân để merge.", IconType.Information);
                return;
            }
            else
            {
                DataTable dt = _dataSource.Clone();
                for (int i = 0; i < dgDuplicatePatient.SelectedRows.Count; i++)
                {
                    DataRow dr = (dgDuplicatePatient.SelectedRows[i].DataBoundItem as DataRowView).Row;
                    dt.ImportRow(dr);
                }
                dlgMergePatient dlg = new dlgMergePatient();
                dlg.SetDataSource(dt);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //re-bind data soource
                    OnDisplayDuplicatePatientList();
                }

            }
        }

        private void dgDuplicatePatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;
                DataTable dt = dgDuplicatePatient.DataSource as DataTable;

                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDuplicatePatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }
        #endregion
    }
}
