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

namespace MM.Controls
{
    public partial class uDuplicatePatient : uBase
    {
        private DataTable _dataSource = null;
        public uDuplicatePatient()
        {
            InitializeComponent();
        }
        #region Properties
        public object DataSource
        {
            get
            {
                //if (dgDuplicatePatient.RowCount <= 0)
                //    DisplayAsThread();

                return dgDuplicatePatient.DataSource;
            }
        }
        #endregion
        private void UpdateGUI()
        {
            //btnAdd.Enabled = AllowAdd;
            //btnEdit.Enabled = AllowEdit;
            //btnDelete.Enabled = AllowDelete;
            //btnOpenPatient.Enabled = AllowOpenPatient;
            //btnImportExcel.Enabled = AllowImport;
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
                //chkChecked.Checked = false;
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
        private DataRow GetDataRow(string patientGUID)
        {
            DataTable dt = dgDuplicatePatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return null;
            DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
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
            //UpdateChecked();
            //chkChecked.Checked = false;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgDuplicatePatient.DataSource = _dataSource;
                if (dgDuplicatePatient.RowCount > 0) dgDuplicatePatient.Rows[0].Selected = true;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            //FullName
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                                     where (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                                     str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                                     p.Field<string>("FullName") != null &&
                                     p.Field<string>("FullName").Trim() != string.Empty
                                     select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgDuplicatePatient.DataSource = newDataSource;
                return;
            }


            //FileNum
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("FileNum") != null &&
                           p.Field<string>("FileNum").Trim() != string.Empty &&
                           (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                       str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
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
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            dgDuplicatePatient.DataSource = newDataSource;
        }
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

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;

            if (dgDuplicatePatient.SelectedRows == null || dgDuplicatePatient.SelectedRows.Count <= 1)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 2 bệnh nhân để merge.", IconType.Information);
                return;
            }

            //string patientGUID = (dgDuplicatePatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            //DataRow drPatient = GetDataRow(patientGUID);
            //if (drPatient == null) return;
        }
    }
}
