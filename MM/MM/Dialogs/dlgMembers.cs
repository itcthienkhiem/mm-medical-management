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
        private DataTable _dataSourceService = null;
        private bool _isContractMember = false;
        private string _companyGUID = string.Empty;
        private string _contractGUID = string.Empty;
        private List<string> _addedMembers = null;
        private List<DataRow> _deletedMemberRows = null;
        private bool _isAscending = true;
        private DataTable _giaDichVuDataSource = null;
        private Dictionary<string, DataRow> _dictMembers = new Dictionary<string,DataRow>();
        private Dictionary<string, DataRow> _dictServices = null;
        private DataTable _dtTemp = null;
        private string _name = string.Empty;
        private int _type = 0;
        private int _doiTuong = 0;
        #endregion

        #region Constructor
        public dlgMembers(string companyGUID, string contractGUID, List<string> addedMembers, 
            List<DataRow> deletedMemberRows, DataTable giaDichVuDataSource)
        {
            InitializeComponent();
            _addedMembers = addedMembers;
            _deletedMemberRows = deletedMemberRows;
            _isContractMember = true;
            _giaDichVuDataSource = giaDichVuDataSource;
            _companyGUID = companyGUID;
            _contractGUID = contractGUID;
            if (_companyGUID == string.Empty)
                _companyGUID = Guid.Empty.ToString();

            pService.Visible = true;
        }

        public dlgMembers(List<string> addedMembers, List<DataRow> deletedMemberRows)
        {
            InitializeComponent();
            _addedMembers = addedMembers;
            _deletedMemberRows = deletedMemberRows;
            pService.Visible = false;
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedMembers
        {
            get { return _dictMembers.Values.ToList(); }
        }

        public List<DataRow> CheckedServices
        {
            get
            {
                if (_dataSourceService == null) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSourceService.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
        }

        public List<string> AddedServices
        {
            get 
            {
                List<string> addedServices = new List<string>();
                if (_dataSourceService == null) return addedServices;
                foreach (DataRow row in _dataSourceService.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        addedServices.Add(row["ServiceGUID"].ToString());
                }

                return addedServices; 
            }
        }

        public DataTable ServiceDataSource
        {
            get 
            {
                if (_dataSourceService == null) return null;
                DataTable dt = _dataSourceService.Clone();
                foreach (DataRow row in _dataSourceService.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        DataRow newRow = dt.NewRow();
                        newRow.ItemArray = row.ItemArray;
                        dt.Rows.Add(newRow);
                    }
                }

                return dt; 
            }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;

                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

                if (raAll.Checked) _doiTuong = 0;
                else if (raNam.Checked) _doiTuong = 1;
                else if (raNu.Checked) _doiTuong = 2;
                else _doiTuong = 3;

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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;

                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

                if (raAll.Checked) _doiTuong = 0;
                else if (raNam.Checked) _doiTuong = 1;
                else if (raNu.Checked) _doiTuong = 2;
                else if (raNuCoGiaDinh.Checked) _doiTuong = 3;
                else _doiTuong = 4;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private DataTable GetDataSource(DataTable dt)
        {
            string fieldName = _isContractMember ? "CompanyMemberGUID" : "PatientGUID";

            //Delete
            List<DataRow> deletedRows = new List<DataRow>();
            foreach (string key in _addedMembers)
            {
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'", fieldName, key));
                if (rows == null || rows.Length <= 0) continue;

                deletedRows.AddRange(rows);
            }

            foreach (DataRow row in deletedRows)
            {
                dt.Rows.Remove(row);
            }

            //Add
            foreach (DataRow row in _deletedMemberRows)
            {
                if (row.Table.Columns.Contains("Status"))
                {
                    if (row["Status"] != null && row["Status"] != DBNull.Value)
                    {
                        byte status = Convert.ToByte(row["Status"]);
                        if (status == (byte)Status.Deactived) continue;
                    }
                }

                string key = row[fieldName].ToString();
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'" , fieldName, key));
                if (rows != null && rows.Length > 0) continue;

                DataRow newRow = dt.NewRow();
                newRow["PatientGUID"] = row["PatientGUID"];
                newRow["Checked"] = false;
                newRow[fieldName] = key;
                newRow["FileNum"] = row["FileNum"];
                newRow["FullName"] = row["FullName"];
                newRow["DobStr"] = row["DobStr"];
                newRow["GenderAsStr"] = row["GenderAsStr"];
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public void ClearData()
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgMembers.DataSource = null;
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result;
                if (!_isContractMember)
                    result = PatientBus.GetPatientListNotInCompany(_name, _type, _doiTuong);
                else
                    result = CompanyBus.GetCompanyMemberListNotInContractMember(_companyGUID, _contractGUID, _name, _type, _doiTuong);

                if (result.IsOK)
                {
                    dgMembers.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dt = GetDataSource(dt);
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgMembers.DataSource = dt;
                    }));
                }
                else
                {
                    if (!_isContractMember)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientListNotInCompany"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientListNotInCompany"));
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetCompanyMemberListNotInContractMember"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberListNotInContractMember"));
                    }
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictMembers.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnDisplayServiceList()
        {
            MethodInvoker method = delegate
            {
                _dataSourceService = _giaDichVuDataSource;

                if (_dictServices == null) _dictServices = new Dictionary<string, DataRow>();
                foreach (DataRow row in _dataSourceService.Rows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    _dictServices.Add(serviceGUID, row);
                }

                dgService.DataSource = _dataSourceService;
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();
        }

        private void OnSearchService()
        {
            chkChecked.Checked = false;
            if (txtSearchService.Text.Trim() == string.Empty)
            {
                dgService.DataSource = _dataSourceService;
                return;
            }

            string str = txtSearchService.Text.ToLower();

            //Code
            List<DataRow> results = (from p in _dataSourceService.AsEnumerable()
                                     where p.Field<string>("Code") != null &&
                                     p.Field<string>("Code").Trim() != string.Empty &&
                                     (p.Field<string>("Code").ToLower().IndexOf(str) >= 0 ||
                                     str.IndexOf(p.Field<string>("Code").ToLower()) >= 0)
                                     select p).ToList<DataRow>();

            DataTable newDataSource = _dataSourceService.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }

            //Name
            results = (from p in _dataSourceService.AsEnumerable()
                       where p.Field<string>("Name") != null &&
                           p.Field<string>("Name").Trim() != string.Empty &&
                           (p.Field<string>("Name").ToLower().IndexOf(str) >= 0 ||
                       str.IndexOf(p.Field<string>("Name").ToLower()) >= 0)
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }

            dgService.DataSource = newDataSource;
        }

        private void RefreshGUI()
        {
            //Service
            if (_dataSourceService != null && _dataSourceService.Rows.Count >= 0)
            {
                foreach (DataRow row in _dataSourceService.Rows)
                {
                    row["Checked"] = false;
                }

                DataTable dt = dgService.DataSource as DataTable;
                if (dt != null && dt.Rows.Count >= 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Checked"] = false;
                    }
                }
            }

            //Members
            List<DataRow> checkedMembers = this.CheckedMembers;
            DataTable dtMember = dgMembers.DataSource as DataTable;
            if (dtMember == null) return;
            if (checkedMembers != null && checkedMembers.Count > 0 && dgMembers.RowCount > 0)
            {
                foreach (DataRow row in checkedMembers)
                {
                    _addedMembers.Add(row["CompanyMemberGUID"].ToString());

                    DataRow[] rows = dtMember.Select(string.Format("PatientGUID='{0}'", row["PatientGUID"].ToString()));
                    if (rows != null && rows.Length > 0)
                        dtMember.Rows.Remove(rows[0]);
                }

                _dictMembers.Clear();
                _dtTemp.Rows.Clear();
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgMembers_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgMembers.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictMembers.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictMembers.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictMembers.ContainsKey(patientGUID))
                {
                    _dictMembers.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void dgService_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            if (_dataSourceService == null) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgService.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string serviceGUID = row["ServiceGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            _dictServices[serviceGUID]["Checked"] = isChecked;
        }

        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            OnSearchService();
        }

        private void txtSearchService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void dlgMembers_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
            if (_isContractMember) OnDisplayServiceList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DataRow> checkedRows = CheckedMembers;
            if (checkedRows == null || checkedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 bệnh nhân.", IconType.Information);
                return;
            }

            if (_isContractMember)
            {
                if (CheckedServices == null || CheckedServices.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 dịch vụ.", IconType.Information);
                    return;
                }

                base.RaiseAddMember(this.CheckedMembers, this.AddedServices.ToList<string>(), this.ServiceDataSource.Copy());
                RefreshGUI();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictMembers.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictMembers.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictMembers.ContainsKey(patientGUID))
                    {
                        _dictMembers.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void chkServiceChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkServiceChecked.Checked;

                string serviceGUID = row["ServiceGUID"].ToString();
                _dictServices[serviceGUID]["Checked"] = chkServiceChecked.Checked;
            }
        }

        private void dgMembers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgMembers.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;
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

                dgMembers.DataSource = newDataSource;

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

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            if (raAll.Checked) SearchAsThread();
        }

        private void raNam_CheckedChanged(object sender, EventArgs e)
        {
            if (raNam.Checked) SearchAsThread();
        }

        private void raNu_CheckedChanged(object sender, EventArgs e)
        {
            if (raNu.Checked) SearchAsThread();
        }

        private void raNuCoGiaDinh_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuCoGiaDinh.Checked) SearchAsThread();
        }

        private void raNamTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNamTren40.Checked) SearchAsThread();
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
