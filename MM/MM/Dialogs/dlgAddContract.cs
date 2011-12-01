using System;
using System.Collections;
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
    public partial class dlgAddContract : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private CompanyContract _contract = new CompanyContract();
        private List<string> _addedServices = new List<string>();
        private List<string> _deletedServices = new List<string>();
        private List<DataRow> _deletedServiceRows = new List<DataRow>();
        private Hashtable _htCompany = new Hashtable();
        private CompanyInfo _selectedCompanyInfo = null;
        #endregion

        #region Constructor
        public dlgAddContract()
        {
            InitializeComponent();
            InitData();
            DisplayDetailAsThread(Guid.Empty.ToString());
        }

        public dlgAddContract(DataRow drContract)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            this.Text = "Sua hop dong";
            DisplayInfo(drContract);
        }
        #endregion

        #region Properties
        public CompanyContract Contract
        {
            get { return _contract; }
        }

        public string ComName
        {
            get { return cboCompany.Text; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drContract)
        {
            try
            {
                txtCode.Text = drContract["ContractCode"] as string;
                txtName.Text = drContract["ContractName"] as string;
                cboCompany.SelectedValue = drContract["CompanyGUID"].ToString();
                dtpkBeginDate.Value = Convert.ToDateTime(drContract["BeginDate"]);
                chkCompleted.Checked = Convert.ToBoolean(drContract["Completed"]);

                _contract.CompanyContractGUID = Guid.Parse(drContract["CompanyContractGUID"].ToString());
                _contract.CompanyGUID = Guid.Parse(drContract["CompanyGUID"].ToString());

                if (drContract["CreatedDate"] != null && drContract["CreatedDate"] != DBNull.Value)
                    _contract.CreatedDate = Convert.ToDateTime(drContract["CreatedDate"]);

                if (drContract["CreatedBy"] != null && drContract["CreatedBy"] != DBNull.Value)
                    _contract.CreatedBy = Guid.Parse(drContract["CreatedBy"].ToString());

                if (drContract["UpdatedDate"] != null && drContract["UpdatedDate"] != DBNull.Value)
                    _contract.UpdatedDate = Convert.ToDateTime(drContract["UpdatedDate"]);

                if (drContract["UpdatedBy"] != null && drContract["UpdatedBy"] != DBNull.Value)
                    _contract.UpdatedBy = Guid.Parse(drContract["UpdatedBy"].ToString());

                if (drContract["DeletedDate"] != null && drContract["DeletedDate"] != DBNull.Value)
                    _contract.DeletedDate = Convert.ToDateTime(drContract["DeletedDate"]);

                if (drContract["DeletedBy"] != null && drContract["DeletedBy"] != DBNull.Value)
                    _contract.DeletedBy = Guid.Parse(drContract["DeletedBy"].ToString());

                _contract.Status = Convert.ToByte(drContract["ContractStatus"]);

                DisplayDetailAsThread(_contract.CompanyContractGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã hợp đồng.");
                tabContract.SelectedTabIndex = 0;
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên hợp đồng.");
                tabContract.SelectedTabIndex = 0;
                txtName.Focus();
                return false;
            }

            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.");
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return false;
            }

            string conGUID = _isNew ? string.Empty : _contract.CompanyContractGUID.ToString();
            Result result = CompanyContractBus.CheckContractExistCode(conGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã hợp đồng này đã tồn tại rồi. Vui lòng nhập mã khác.");
                    tabContract.SelectedTabIndex = 0;
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckContractExistCode"));
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                _contract.ContractCode = txtCode.Text;
                _contract.ContractName = txtName.Text;
                _contract.BeginDate = dtpkBeginDate.Value;
                _contract.Completed = chkCompleted.Checked;
                _contract.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _contract.CreatedDate = DateTime.Now;
                    _contract.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _contract.UpdatedDate = DateTime.Now;
                    _contract.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _contract.CompanyGUID = Guid.Parse(cboCompany.SelectedValue.ToString());

                    Result result = CompanyContractBus.InsertContract(_contract, _selectedCompanyInfo);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.InsertContract"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.InsertContract"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void InitData()
        {
            //Company
            Result result = CompanyBus.GetCompanyList();
            if (result.IsOK)
            {
                cboCompany.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyList"));
            }
        }

        private void DisplayDetailAsThread(string contractGUID)
        {
            try
            {
                chkCheckedMember.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDetailProc), contractGUID);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayMembers(string contractGUID)
        {
            Result result = CompanyContractBus.GetContractMemberList(contractGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgMembers.DataSource = result.QueryResult;

                    if (cboCompany.Text == string.Empty) return;
                    string companyGUID = cboCompany.SelectedValue.ToString();
                    if (!_htCompany.ContainsKey(companyGUID))
                    {
                        _selectedCompanyInfo = new CompanyInfo();
                        _selectedCompanyInfo.CompanyGUID = companyGUID;
                        _selectedCompanyInfo.DataSource = result.QueryResult as DataTable;
                        _htCompany.Add(companyGUID, _selectedCompanyInfo);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
            }
        }

        private void OnAddMember()
        {
            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.");
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return;
            }


            dlgMembers dlg = new dlgMembers(cboCompany.SelectedValue.ToString(), _contract.CompanyContractGUID.ToString(), 
                _selectedCompanyInfo.AddedMemberKeys, _selectedCompanyInfo.DeletedMemberRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.CheckedMembers;
                DataTable dataSource = dgMembers.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("CompanyMemberGUID='{0}'", companyMemberGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["CompanyMemberGUID"] = companyMemberGUID;
                        newRow["FileNum"] = row["FileNum"];
                        newRow["FullName"] = row["FullName"];
                        newRow["DobStr"] = row["DobStr"];
                        newRow["GenderAsStr"] = row["GenderAsStr"];
                        dataSource.Rows.Add(newRow);

                        if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                        {
                            Member member = new Member();
                            member.CompanyMemberGUID = companyMemberGUID;
                            member.AddedServices = dlg.AddedServices;
                            member.DataSource = dlg.ServiceDataSource;
                            _selectedCompanyInfo.AddedMembers.Add(companyMemberGUID, member);
                        }

                        if (_selectedCompanyInfo.DeletedMembers.Contains(companyMemberGUID))
                        {
                            _selectedCompanyInfo.DeletedMembers.Remove(companyMemberGUID);

                            foreach (DataRow r in _selectedCompanyInfo.DeletedMemberRows)
                            {
                                if (r["CompanyMemberGUID"].ToString() == companyMemberGUID)
                                {
                                    _selectedCompanyInfo.DeletedMemberRows.Remove(r);

                                    if (r["ContractMemberGUID"] != null && r["ContractMemberGUID"] != DBNull.Value)
                                    {
                                        string contractMemberGUID = r["ContractMemberGUID"].ToString();
                                        Result result = CompanyContractBus.GetCheckList(contractMemberGUID);
                                        if (result.IsOK)
                                        {
                                            DataTable dt = result.QueryResult as DataTable;
                                            Member member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];
                                            foreach (DataRow clRow in dt.Rows)
                                            {
                                                string serviceGUID = clRow["ServiceGUID"].ToString();
                                                if (!member.DeletedServices.Contains(serviceGUID))
                                                    member.DeletedServices.Add(serviceGUID);
                                            }
                                        }
                                        else
                                        {
                                            MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                                            Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        private void OnDeleteMember()
        {
            List<string> deletedMemList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgMembers.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedMemList.Add(row["CompanyMemberGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedMemList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhân viên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                        if (!_selectedCompanyInfo.DeletedMembers.Contains(companyMemberGUID))
                        {
                            _selectedCompanyInfo.DeletedMembers.Add(companyMemberGUID);
                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _selectedCompanyInfo.DeletedMemberRows.Add(r);
                        }

                        _selectedCompanyInfo.AddedMembers.Remove(companyMemberGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những nhân viên cần xóa.");
        }

        private void OnDisplayCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;
            DataRow drMember = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            string contractMemberGUID = drMember["ContractMemberGUID"].ToString();

            Member member = null;

            if (_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];
            else
            {
                member = new Member();
                member.CompanyMemberGUID = companyMemberGUID;
                Result result = CompanyContractBus.GetCheckList(contractMemberGUID);
                if (result.IsOK)
                {
                    DataTable dataSource = result.QueryResult as DataTable;
                    member.DataSource = dataSource;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                }

                _selectedCompanyInfo.AddedMembers.Add(companyMemberGUID, member);
            }

            dlgUpdateCheckList dlg = new dlgUpdateCheckList(member);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        /*private void OnAddService()
        {
            dlgServices dlg = new dlgServices(_contract.CompanyContractGUID.ToString(), _addedServices, _deletedServiceRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Services;
                DataTable dataSource = dgService.DataSource as DataTable;
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
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.");
        }*/
        #endregion

        #region Window Event Handlers
        private void dlgAddContract_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void chkCheckedMember_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedMember.Checked;
            }
        }

        private void cboCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCompany.Text == string.Empty) return;
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            string companyGUID = cboCompany.SelectedValue.ToString();
            if (_htCompany.ContainsKey(companyGUID))
            {
                _selectedCompanyInfo = _htCompany[companyGUID] as CompanyInfo;
                dgMembers.DataSource = _selectedCompanyInfo.DataSource;
            }
            else
            {
                _selectedCompanyInfo = new CompanyInfo();
                _selectedCompanyInfo.CompanyGUID = companyGUID;
                _selectedCompanyInfo.DataSource = dt.Clone();
                _htCompany.Add(companyGUID, _selectedCompanyInfo);
                dgMembers.DataSource = _selectedCompanyInfo.DataSource;
            }
        }

        private void dgMembers_DoubleClick(object sender, EventArgs e)
        {
            OnDisplayCheckList();
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayDetailProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayMembers(state.ToString());
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message);
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
