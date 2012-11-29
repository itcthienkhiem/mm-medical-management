using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uSearchPatient : uBase
    {
        #region Members
        private bool _isAscending = true;
        private bool _isMulti = false;
        private PatientSearchType _patientSearchType = PatientSearchType.BenhNhan;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private DataTable _dtTemp = null;
        private string _hopDongGUID = string.Empty;
        private string _serviceGUID = string.Empty;
        private string _patientGUID = string.Empty;
        private Object _thisLock = new Object();
        #endregion

        #region Constructor
        public uSearchPatient()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string HopDongGUID
        {
            get { return _hopDongGUID; }
            set { _hopDongGUID = value; }
        }

        public string ServiceGUID
        {
            get { return _serviceGUID; }
            set { _serviceGUID= value; }
        }

        public string PatientGUID
        {
            get { return _patientGUID; }
            set { _patientGUID = value; }
        }

        public PatientSearchType PatientSearchType
        {
            get { return _patientSearchType; }
            set { _patientSearchType = value; }
        }

        public DataRow PatientRow
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
            get { return _dictPatient.Values.ToList(); }
        }
        #endregion

        #region UI Command
        private void ClearDataSource()
        {
            DataTable dtOld = dgPatient.DataSource as DataTable;
            if (dtOld != null)
            {
                dtOld.Rows.Clear();
                dtOld.Clear();
                dtOld = null;
                dgPatient.DataSource = null;
            }
        }

        private void RaiseOpentPatient()
        {
            if (Global.dtOpenPatient == null) return;
            DataRow patientRow = this.PatientRow;
            if (patientRow != null)
            {
                string patientGUID = patientRow["PatientGUID"].ToString();
                DataRow[] rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", patientGUID));
                if (rows == null || rows.Length <= 0)
                {
                    DataRow newRow = Global.dtOpenPatient.NewRow();
                    newRow["PatientGUID"] = patientRow["PatientGUID"];
                    newRow["FileNum"] = patientRow["FileNum"];
                    newRow["FullName"] = patientRow["FullName"];
                    newRow["GenderAsStr"] = patientRow["GenderAsStr"];
                    newRow["DobStr"] = patientRow["DobStr"];
                    newRow["IdentityCard"] = patientRow["IdentityCard"];
                    newRow["WorkPhone"] = patientRow["WorkPhone"];
                    newRow["Mobile"] = patientRow["Mobile"];
                    newRow["Email"] = patientRow["Email"];
                    newRow["Address"] = patientRow["Address"];
                    newRow["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    Global.dtOpenPatient.Rows.Add(newRow);
                    base.RaiseOpentPatient(newRow);
                }
                else
                {
                    rows[0]["PatientGUID"] = patientRow["PatientGUID"];
                    rows[0]["FileNum"] = patientRow["FileNum"];
                    rows[0]["FullName"] = patientRow["FullName"];
                    rows[0]["GenderAsStr"] = patientRow["GenderAsStr"];
                    rows[0]["DobStr"] = patientRow["DobStr"];
                    rows[0]["IdentityCard"] = patientRow["IdentityCard"];
                    rows[0]["WorkPhone"] = patientRow["WorkPhone"];
                    rows[0]["Mobile"] = patientRow["Mobile"];
                    rows[0]["Email"] = patientRow["Email"];
                    rows[0]["Address"] = patientRow["Address"];
                    rows[0]["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    base.RaiseOpentPatient(rows[0]);
                }
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

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

        private void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayPatientList()
        {
            lock (_thisLock)
            {
                Result result = null;
                if (_patientSearchType == Common.PatientSearchType.BenhNhan)
                    result = PatientBus.GetPatientList(_name, _type);
                else if (_patientSearchType == Common.PatientSearchType.BenhNhanThanThuoc)
                    result = PatientBus.GetBenhNhanThanThuocList(_name, _type);
                else if (_patientSearchType == Common.PatientSearchType.BenhNhanKhongThanThuoc)
                    result = PatientBus.GetBenhNhanKhongThanThuocList(_name, _type);
                else if (_patientSearchType == Common.PatientSearchType.NhanVienHopDong)
                    result = CompanyContractBus.GetContractMemberList(_hopDongGUID, _serviceGUID, _patientGUID, _name, _type);

                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearDataSource();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        if (_isMulti) UpdateChecked(dt);
                        dgPatient.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!_isMulti) return;

            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            SearchAsThread();
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
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        
    }
}
