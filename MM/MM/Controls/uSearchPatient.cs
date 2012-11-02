using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uSearchPatient : uBase
    {
        #region Members
        private object _dataSource = null;
        private bool _isAscending = true;
        private bool _isMulti = false;
        #endregion

        #region Constructor
        public uSearchPatient()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public object PatientRow
        {
            get
            {
                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                    return (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;

                return null;
            }
        }

        public bool IsMulti
        {
            get { return _isMulti; }
            set 
            { 
                _isMulti = value;
                chkChecked.Visible = _isMulti;
                colChecked.Visible = _isMulti;
            }
        }

        public List<DataRow> CheckedPatientRows
        {
            get
            {
                if (_dataSource == null) return null;
                UpdateChecked();
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dataSource = _dataSource as DataTable;
                DataRow[] rows = dataSource.Select("Checked='True'");
                if (rows != null && rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        string patientGUID = row["PatientGUID"].ToString();
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        private void UpdateChecked()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            DataTable dataSource = _dataSource as DataTable;
            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                DataRow[] rows2 = dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void ClearDataSource()
        {
            DataTable dtOld = dgPatient.DataSource as DataTable;
            if (dtOld != null)
            {
                dtOld.Rows.Clear();
                dtOld.Clear();
                dtOld = null;
            }
        }

        public void OnSearch()
        {
            if (_isMulti) UpdateChecked();

            ClearDataSource();

            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgPatient.DataSource = null;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            if (txtSearchPatient.Text.Trim() == "*")
            {
                DataTable dtSource = _dataSource as DataTable;
                results = (from p in dtSource.AsEnumerable()
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                newDataSource = dtSource.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgPatient.DataSource = newDataSource;
                _isAscending = true;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            string str = txtSearchPatient.Text.ToLower();
            DataTable dt = _dataSource as DataTable;
            if (dt == null)
            {
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }
            newDataSource = dt.Clone();

            if (chkMaBenhNhan.Checked)
            {
                //FileNum
                results = (from p in dt.AsEnumerable()
                           where p.Field<string>("FileNum") != null &&
                               p.Field<string>("FileNum").Trim() != string.Empty &&
                               //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }

            if (chkTheoSoDienThoai.Checked)
            {
                //FileNum
                results = (from p in dt.AsEnumerable()
                           where p.Field<string>("Mobile") != null &&
                               p.Field<string>("Mobile").Trim() != string.Empty &&
                               //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                               //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }
            
            //FullName
            results = (from p in dt.AsEnumerable()
                        where p.Field<string>("FullName") != null &&
                        p.Field<string>("FullName").Trim() != string.Empty &&
                        //(p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                        //str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0)
                        p.Field<string>("FullName").ToLower().IndexOf(str) >= 0
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();


            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            //HomePhone
            /*results = (from p in dt.AsEnumerable()
                        where p.Field<string>("HomePhone") != null &&
                        p.Field<string>("HomePhone").Trim() != string.Empty &&
                        (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //WorkPhone
            results = (from p in dt.AsEnumerable()
                        where p.Field<string>("WorkPhone") != null &&
                            p.Field<string>("WorkPhone").Trim() != string.Empty &&
                            (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //Mobile
            results = (from p in dt.AsEnumerable()
                        where p.Field<string>("Mobile") != null &&
                            p.Field<string>("Mobile").Trim() != string.Empty &&
                            (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }*/

            dgPatient.DataSource = newDataSource;
            lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
        }

        private void RaiseOpentPatient()
        {
            object patientRow = this.PatientRow;
            if (patientRow != null)
                base.RaiseOpentPatient(patientRow);
        }
        #endregion

        #region Window Event Handlers
        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            RaiseOpentPatient();
        }

        private void dgPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RaiseOpentPatient();
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
                        dgPatient.CurrentCell = dgPatient[3, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Enter)
                RaiseOpentPatient();
        }

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;
                DataTable dt = dgPatient.DataSource as DataTable;

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

                dgPatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnSearch();
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
        #endregion
    }
}
