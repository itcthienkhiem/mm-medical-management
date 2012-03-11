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
    public partial class uPhucHoiBenhNhan : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public uPhucHoiBenhNhan()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnPhucHoi.Enabled = AllowEdit;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgPatient.DataSource = null;
        }

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientBiXoaList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
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

        private void UpdateChecked()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchPatient()
        {
            UpdateChecked();
            chkChecked.Checked = false;
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

                dgPatient.DataSource = newDataSource;
                if (dgPatient.RowCount > 0) dgPatient.Rows[0].Selected = true;
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
                             p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0
                           //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //FullName
                results = (from p in _dataSource.AsEnumerable()
                           where //(p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                               //str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                           p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 &&
                           p.Field<string>("FullName") != null &&
                           p.Field<string>("FullName").Trim() != string.Empty
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    return;
                }
            }

            dgPatient.DataSource = newDataSource;
        }

        private void OnPhucHoiBenhNhan()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> checkedPatientList = new List<string>();
            List<DataRow> checkedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    checkedPatientList.Add(patientGUID);
                    checkedRows.Add(row);
                }
            }

            if (checkedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn phục hồi những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.PhucHoiPatient(checkedPatientList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchPatient();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.PhucHoiPatient"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.PhucHoiPatient"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần phục hồi.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            OnPhucHoiBenhNhan();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
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
                //Thread.Sleep(500);
                OnDisplayPatientList();
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
