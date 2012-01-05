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
        private bool _flag = true;
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public dlgAddContract()
        {
            InitializeComponent();
            InitData();
            DisplayDetailAsThread(Guid.Empty.ToString());
            GenerateCode();
        }

        public dlgAddContract(DataRow drContract)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            btnDSNVChuaKham.Visible = true;
            btnDSNVChuaKhamDu.Visible = true;
            btnDSNVKhamDu.Visible = true;
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

                if (chkCompleted.Checked)
                {
                    dtpkEndDate.Value = Convert.ToDateTime(drContract["EndDate"]);
                    dtpkEndDate.Enabled = true;
                }

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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 0;
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 0;
                txtName.Focus();
                return false;
            }

            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.", IconType.Information);
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
                    MsgBox.Show(this.Text, "Mã hợp đồng này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    tabContract.SelectedTabIndex = 0;
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckContractExistCode"), IconType.Error);
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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
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

                if (_contract.Completed.Value)
                    _contract.EndDate = dtpkEndDate.Value;
                else
                    _contract.EndDate = null;

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
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.InsertContract"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.InsertContract"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = CompanyContractBus.GetContractCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtCode.Text = Utility.GetCode("HD", count + 1, 4);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractCount"));
            }
        }

        private void InitData()
        {
            dtpkBeginDate.Value = DateTime.Now;
            dtpkEndDate.Value = DateTime.Now.AddDays(1);

            //Company
            Result result = CompanyBus.GetCompanyList();
            if (result.IsOK)
            {
                cboCompany.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyList"), IconType.Error);
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
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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

                    if (dgMembers.RowCount <= 0)
                        pService.Enabled = false;
                    else
                        pService.Enabled = true;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
            }
        }

        private void OnAddMember()
        {
            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.", IconType.Information);
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return;
            }


            dlgMembers dlg = new dlgMembers(cboCompany.SelectedValue.ToString(), _contract.CompanyContractGUID.ToString(), 
                _selectedCompanyInfo.AddedMemberKeys, _selectedCompanyInfo.DeletedMemberRows);
            dlg.OnAddMemberEvent += new AddMemberHandler(dlg_OnAddMember);
            dlg.ShowDialog(this);
            /*if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.CheckedMembers;
                DataTable dataSource = dgMembers.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("CompanyMemberGUID='{0}'", companyMemberGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                        {
                            Member member = new Member();
                            member.ConstractGUID = _contract.CompanyContractGUID.ToString();
                            member.CompanyMemberGUID = companyMemberGUID;
                            member.AddedServices = dlg.AddedServices.ToList<string>();
                            member.DataSource = dlg.ServiceDataSource.Copy();
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
                                            MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                                            Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                                        }
                                    }

                                    break;
                                }
                            }
                        }

                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["CompanyMemberGUID"] = companyMemberGUID;
                        newRow["FileNum"] = row["FileNum"];
                        newRow["FullName"] = row["FullName"];
                        newRow["DobStr"] = row["DobStr"];
                        newRow["GenderAsStr"] = row["GenderAsStr"];
                        dataSource.Rows.Add(newRow);
                        
                    }
                }
            }*/
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

                        if (dgMembers.CurrentRow != null && dgMembers.CurrentRow.Index >= 0)
                            dgMembers.Rows[dgMembers.CurrentRow.Index].Selected = true;
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những nhân viên cần xóa.", IconType.Information);
        }

        private void OnDisplayCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_selectedCompanyInfo == null)
            {
                pService.Enabled = false;
                return;
            }

            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0)
            {
                dgService.DataSource = null;
                pService.Enabled = false;
                return;
            }

            DataRow drMember = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            string contractMemberGUID = drMember["ContractMemberGUID"].ToString();
            string patientGUID = drMember["PatientGUID"].ToString();           

            Member member = null;

            if (_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];
            else
            {
                member = new Member();
                member.CompanyMemberGUID = companyMemberGUID;
                member.ConstractGUID = _contract.CompanyContractGUID.ToString();

                Result result = null;
                if (_isNew)
                    result = CompanyContractBus.GetCheckList(contractMemberGUID);
                else
                    result = CompanyContractBus.GetCheckList(_contract.CompanyContractGUID.ToString(), patientGUID);

                _selectedCompanyInfo.AddedMembers.Add(companyMemberGUID, member);

                if (result.IsOK)
                {
                    DataTable dataSource = result.QueryResult as DataTable;
                    member.DataSource = dataSource;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                }
            }

            dgService.DataSource = member.DataSource;
            RefreshUsingService();
            pService.Enabled = true;
        }

        private void RefreshUsingService()
        {
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataGridViewImageCell cell = row.Cells["Using"] as DataGridViewImageCell;
                DataRow drRow = (row.DataBoundItem as DataRowView).Row;
                if (!drRow.Table.Columns.Contains("Using"))
                    cell.Value = imgList.Images[1];
                else if (drRow["Using"] != null && drRow["Using"] != DBNull.Value && Convert.ToBoolean(drRow["Using"]))
                    cell.Value = imgList.Images[0];
                else
                    cell.Value = imgList.Images[1];
            }
        }

        private void OnAddService()
        {
            if (_selectedCompanyInfo == null) return;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;
            DataRow drMember = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID)) return;
            Member member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];

            dlgServices dlg = new dlgServices(member.ConstractGUID, member.CompanyMemberGUID, member.AddedServices, member.DeletedServiceRows);
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
                        //newRow["Price"] = row["Price"];
                        dataSource.Rows.Add(newRow);

                        if (!member.AddedServices.Contains(serviceGUID))
                            member.AddedServices.Add(serviceGUID);

                        member.DeletedServices.Remove(serviceGUID);
                        foreach (DataRow r in member.DeletedServiceRows)
                        {
                            if (r["ServiceGUID"].ToString() == serviceGUID)
                            {
                                member.DeletedServiceRows.Remove(r);
                                break;
                            }
                        }
                    }
                }

                RefreshUsingService();
            }
        }

        private void OnDeleteService()
        {
            if (_selectedCompanyInfo == null) return;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;
            DataRow drMember = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID)) return;
            Member member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];

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
                        if (!member.DeletedServices.Contains(serviceGUID))
                        {
                            member.DeletedServices.Add(serviceGUID);
                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            member.DeletedServiceRows.Add(r);
                        }

                        member.AddedServices.Remove(serviceGUID);

                        dt.Rows.Remove(row);
                    }

                    RefreshUsingService();
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlg_OnAddMember(List<DataRow> checkedMembers, List<string> addedServices, DataTable serviceDataSource)
        {
            List<DataRow> checkedRows = checkedMembers;
            DataTable dataSource = dgMembers.DataSource as DataTable;
            foreach (DataRow row in checkedRows)
            {
                string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                DataRow[] rows = dataSource.Select(string.Format("CompanyMemberGUID='{0}'", companyMemberGUID));
                if (rows == null || rows.Length <= 0)
                {
                    if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                    {
                        Member member = new Member();
                        member.ConstractGUID = _contract.CompanyContractGUID.ToString();
                        member.CompanyMemberGUID = companyMemberGUID;
                        member.AddedServices = addedServices;
                        member.DataSource = serviceDataSource;
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
                                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                                    }
                                }

                                break;
                            }
                        }
                    }

                    DataRow newRow = dataSource.NewRow();
                    newRow["Checked"] = false;
                    newRow["CompanyMemberGUID"] = companyMemberGUID;
                    newRow["FileNum"] = row["FileNum"];
                    newRow["FullName"] = row["FullName"];
                    newRow["DobStr"] = row["DobStr"];
                    newRow["GenderAsStr"] = row["GenderAsStr"];
                    dataSource.Rows.Add(newRow);

                }
            }
        }

        private void dlgAddContract_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (!_flag)
                {
                    _flag = true;
                    return;
                }

                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin hợp đồng ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
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
            if (_isNew) return;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;

            DataRow patientRow = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            base.RaiseOpentPatient(patientRow);
            _flag = false;
            this.Close();
        }

        private void dgMembers_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayCheckList();
        }

        private void btnDSNVChuaKham_Click(object sender, EventArgs e)
        {
            dlgDanhSachNhanVien dlg = new dlgDanhSachNhanVien(_contract.CompanyContractGUID.ToString(), 0);
            dlg.ShowDialog();
        }

        private void btnDSNVChuaKhamDu_Click(object sender, EventArgs e)
        {
            dlgDanhSachNhanVien dlg = new dlgDanhSachNhanVien(_contract.CompanyContractGUID.ToString(), 1);
            dlg.ShowDialog();
        }

        private void btnDSNVKhamDu_Click(object sender, EventArgs e)
        {
            dlgDanhSachNhanVien dlg = new dlgDanhSachNhanVien(_contract.CompanyContractGUID.ToString(), 2);
            dlg.ShowDialog();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dgService_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshUsingService();
        }

        private void chkCheckedService_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedMember.Checked;
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

        private void chkCompleted_CheckedChanged(object sender, EventArgs e)
        {
            dtpkEndDate.Enabled = chkCompleted.Checked;
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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
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
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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
