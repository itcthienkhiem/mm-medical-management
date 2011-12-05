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
        private bool _isContractMember = false;
        private string _companyGUID = string.Empty;
        private string _contractGUID = string.Empty;
        private List<string> _addedMembers = null;
        private List<DataRow> _deletedMemberRows = null;
        private List<string> _addedServices = new List<string>();
        private List<string> _deletedServices = new List<string>();
        private List<DataRow> _deletedServiceRows = new List<DataRow>();
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

            pageService.Visible = true;
        }

        public dlgMembers(List<string> addedMembers, List<DataRow> deletedMemberRows)
        {
            InitializeComponent();
            _addedMembers = addedMembers;
            _deletedMemberRows = deletedMemberRows;
            pageService.Visible = false;
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedMembers
        {
            get
            {
                if (_dataSource == null) return null;

                UpdateChecked();

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

        public List<string> AddedServices
        {
            get { return _addedServices; }
        }

        public List<string> DeletedServices
        {
            get { return _deletedServices; }
        }

        public DataTable ServiceDataSource
        {
            get { return dgService.DataSource as DataTable; }
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
                    _dataSource = GetDataSource((DataTable)result.QueryResult);//result.QueryResult as DataTable;
                    dgMembers.DataSource = _dataSource;
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
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                          where p.Field<string>("FullName") != null &&
                          p.Field<string>("FullName").Trim() != string.Empty &&
                          (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0)
                          select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgMembers.DataSource = newDataSource;
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

        private void OnAddService()
        {
            dlgServices dlg = new dlgServices(Guid.Empty.ToString(), _addedServices, _deletedServiceRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Services;
                DataTable dataSource = dgService.DataSource as DataTable;
                if (dataSource == null)
                {
                    dataSource = checkedRows[0].Table.Clone();
                    dgService.DataSource = dataSource;
                }

                foreach (DataRow row in checkedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["ServiceGUID"] = serviceGUID;
                        newRow["Code"] = row["Code"];
                        newRow["Name"] = row["Name"];
                        newRow["Price"] = row["Price"];
                        newRow["Description"] = row["Description"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedServices.Contains(serviceGUID))
                            _addedServices.Add(serviceGUID);

                        _deletedServices.Remove(serviceGUID);
                        foreach (DataRow r in _deletedServiceRows)
                        {
                            if (r["ServiceGUID"].ToString() == serviceGUID)
                            {
                                _deletedServiceRows.Remove(r);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDeleteService()
        {
            List<string> deletedSrvList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedSrvList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSrvList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        if (!_deletedServices.Contains(serviceGUID))
                        {
                            _deletedServices.Add(serviceGUID);
                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _deletedServiceRows.Add(r);
                        }

                        _addedServices.Remove(serviceGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void dlgMembers_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgMembers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = CheckedMembers;
                if (checkedRows == null || checkedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 bệnh nhân.", IconType.Information);
                    e.Cancel = true;
                    tabMember.SelectedTabIndex = 0;
                    return;
                }

                if (_isContractMember)
                {
                    if (dgService.RowCount <= 0)
                    {
                        MsgBox.Show(this.Text, "Vui lòng thêm ít nhất 1 dịch vụ.", IconType.Information);
                        e.Cancel = true;
                        tabMember.SelectedTabIndex = 1;
                    }
                }
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

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            OnDeleteService();
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
