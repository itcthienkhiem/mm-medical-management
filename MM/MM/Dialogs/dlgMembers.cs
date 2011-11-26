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
        }

        public dlgMembers(List<string> addedMembers, List<DataRow> deletedMemberRows)
        {
            InitializeComponent();
            _addedMembers = addedMembers;
            _deletedMemberRows = deletedMemberRows;
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
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientListNotInCompany"));
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientListNotInCompany"));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetCompanyMemberListNotInContractMember"));
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
                List<DataRow> checkedRows = Members;
                if (checkedRows == null || checkedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 bệnh nhân.");
                    e.Cancel = true;
                }
            }
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
           /* if (e.KeyCode == Keys.Down)
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
            }*/
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
