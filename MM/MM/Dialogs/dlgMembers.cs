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
        private DataTable _dataSourceMember = null;
        private DataTable _dataSourceService = null;
        private bool _isContractMember = false;
        private string _companyGUID = string.Empty;
        private string _contractGUID = string.Empty;
        private List<string> _addedMembers = null;
        private List<DataRow> _deletedMemberRows = null;
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public dlgMembers(string companyGUID, string contractGUID, List<string> addedMembers, List<DataRow> deletedMemberRows)
        {
            InitializeComponent();
            _addedMembers = addedMembers;
            _deletedMemberRows = deletedMemberRows;
            _isContractMember = true;
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
            get
            {
                if (_dataSourceMember == null) return null;

                UpdateCheckedMembers();

                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSourceMember.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }

        }

        public List<DataRow> CheckedServices
        {
            get
            {
                if (_dataSourceService == null) return null;
                UpdateCheckedServices();
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
                UpdateCheckedServices();
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
                UpdateCheckedServices();
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
                string key = row[fieldName].ToString();
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'" , fieldName, key));
                if (rows != null && rows.Length > 0) continue;

                DataRow newRow = dt.NewRow();
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

        private void OnDisplayPatientList()
        {
            Result result;

            if (!_isContractMember)
                result = PatientBus.GetPatientListNotInCompany();
            else
                result = CompanyBus.GetCompanyMemberListNotInContractMember(_companyGUID, _contractGUID);

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSourceMember = GetDataSource((DataTable)result.QueryResult);//result.QueryResult as DataTable;
                    dgMembers.DataSource = _dataSourceMember;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
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

        private void OnDisplayServiceList()
        {
            Result result = ServicesBus.GetServicesList();

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSourceService = (DataTable)result.QueryResult;
                    dgService.DataSource = _dataSourceService;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"));
            }
        }

        private void OnSearchPatient()
        {
            UpdateCheckedMembers();
            List<DataRow> results = null;
            DataTable newDataSource = null;
            chkChecked.Checked = false;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSourceMember.AsEnumerable()
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                newDataSource = _dataSourceMember.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgMembers.DataSource = newDataSource;
                _isAscending = true;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();
            newDataSource = _dataSourceMember.Clone();

            if (chkMaBenhNhan.Checked)
            {
                //FileNum
                results = (from p in _dataSourceMember.AsEnumerable()
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
                    dgMembers.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //FullName
                results = (from p in _dataSourceMember.AsEnumerable()
                           where p.Field<string>("FullName") != null &&
                           p.Field<string>("FullName").Trim() != string.Empty &&
                           (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.Rows.Add(row.ItemArray);

                if (newDataSource.Rows.Count > 0)
                {
                    dgMembers.DataSource = newDataSource;
                    return;
                }
            }

            dgMembers.DataSource = newDataSource;
        }

        private void UpdateCheckedServices()
        {
            /*DataTable dt = dgService.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["ServiceGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSourceService.Rows)
                {
                    string patientGUID2 = row2["ServiceGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }*/

            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["ServiceGUID"].ToString();
                DataRow[] rows2 = _dataSourceService.Select(string.Format("ServiceGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void UpdateCheckedMembers()
        {
            /*DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSourceMember.Rows)
                {
                    string patientGUID2 = row2["PatientGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }*/

            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                DataRow[] rows2 = _dataSourceMember.Select(string.Format("PatientGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchService()
        {
            UpdateCheckedServices();

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
            if (checkedMembers != null && checkedMembers.Count >= 0 && _dataSourceMember != null)
            {
                foreach (DataRow row in checkedMembers)
                {
                    _dataSourceMember.Rows.Remove(row);
                }

                OnSearchPatient();
            }
        }
        #endregion

        #region Window Event Handlers
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
            OnSearchPatient();
        }

        private void dlgMembers_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dlgMembers_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = CheckedMembers;
                if (checkedRows == null || checkedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 bệnh nhân.", IconType.Information);
                    e.Cancel = true;
                    return;
                }

                if (_isContractMember)
                {
                    if (CheckedServices == null || CheckedServices.Count <= 0)
                    {
                        MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 dịch vụ.", IconType.Information);
                        e.Cancel = true;
                    }
                }
            }*/
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
            }
        }

        private void chkServiceChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkServiceChecked.Checked;
            }
        }

        private void dgMembers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgMembers.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;
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

                dgMembers.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPatientList();
                if (_isContractMember)
                    OnDisplayServiceList();
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
