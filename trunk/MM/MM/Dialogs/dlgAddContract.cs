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
using MM.Exports;
using SpreadsheetGear;

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
        private DataRow _drContract = null;
        private bool _isLock = false;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddContract()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddContract(DataRow drContract, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            btnDSNVChuaKham.Visible = true;
            btnDSNVChuaKhamDu.Visible = true;
            btnDSNVKhamDu.Visible = true;
            this.Text = "Sua hop dong";
            _drContract = drContract;
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

        public List<DataRow> CheckedMemberRows
        {
            get
            {
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgMembers.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
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
                numSoTien.Value = (Decimal)Convert.ToDouble(drContract["SoTien"]);
                numDatCoc.Value = (Decimal)Convert.ToDouble(drContract["DatCoc"]);

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

                _isLock = Convert.ToBoolean(drContract["Lock"]);
                if (_isLock)
                {
                    txtCode.Enabled = !_isLock;
                    txtName.Enabled = !_isLock;
                    cboCompany.Enabled = !_isLock;
                    dtpkBeginDate.Enabled = !_isLock;
                    chkCompleted.Enabled = !_isLock;
                    dtpkEndDate.Enabled = !_isLock;
                    numSoTien.Enabled = !_isLock;
                    numDatCoc.Enabled = !_isLock;
                    dgGiaDichVu.ReadOnly = true;
                    panel5.Enabled = !_isLock;
                    dgMembers.ReadOnly = true;
                    dgService.ReadOnly = true;

                    btnAddMember.Enabled = !_isLock;
                    btnDeleteMember.Enabled = !_isLock;
                    btnImportDSNV.Enabled = !_isLock;

                    themNVToolStripMenuItem.Enabled = !_isLock;
                    xoaNVToolStripMenuItem.Enabled = !_isLock;
                    nhapDVHDToolStripMenuItem.Enabled = !_isLock;


                    btnAddService.Enabled = !_isLock;
                    btnDeleteService.Enabled = !_isLock;

                    themDVToolStripMenuItem.Enabled = !_isLock;
                    xoaDVToolStripMenuItem.Enabled = !_isLock;

                    btnOK.Enabled = !_isLock;

                    addToolStripMenuItem.Enabled = !_isLock;
                    editToolStripMenuItem.Enabled = !_isLock;
                    deleteToolStripMenuItem.Enabled = !_isLock;


                }

                DisplayDetailAsThread(_contract.CompanyContractGUID.ToString());

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    txtCode.Enabled = _allowEdit;
                    txtName.Enabled = _allowEdit;
                    cboCompany.Enabled = _allowEdit;
                    dtpkBeginDate.Enabled = _allowEdit;
                    chkCompleted.Enabled = _allowEdit;
                    dtpkEndDate.Enabled = _allowEdit;
                    numSoTien.Enabled = _allowEdit;
                    numDatCoc.Enabled = _allowEdit;

                    panel5.Enabled = _allowEdit;

                    addToolStripMenuItem.Enabled = _allowEdit;
                    editToolStripMenuItem.Enabled = _allowEdit;
                    deleteToolStripMenuItem.Enabled = _allowEdit;

                    btnAddMember.Enabled = _allowEdit;
                    btnDeleteMember.Enabled = _allowEdit;

                    themNVToolStripMenuItem.Enabled = _allowEdit;
                    xoaNVToolStripMenuItem.Enabled = _allowEdit;

                    btnAddService.Enabled = _allowEdit;
                    btnDeleteService.Enabled = _allowEdit;

                    themDVToolStripMenuItem.Enabled = _allowEdit;
                    xoaDVToolStripMenuItem.Enabled = _allowEdit;
                }
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

            if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty)
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

            if (numSoTien.Value <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số tiền của hợp đồng.", IconType.Information);
                numSoTien.Focus();
                return false;
            }

            if (dgGiaDichVu.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập danh sách dịch vụ theo hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 1;
                btnAdd.Focus();
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
                    _contract.SoTien = (double)numSoTien.Value;
                    _contract.DatCoc = (double)numDatCoc.Value;

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

                    if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty) return;
                    string companyGUID = cboCompany.SelectedValue.ToString();
                    if (!_htCompany.ContainsKey(companyGUID))
                    {
                        _selectedCompanyInfo = new CompanyInfo();
                        _selectedCompanyInfo.CompanyGUID = companyGUID;
                        _selectedCompanyInfo.DataSource = result.QueryResult as DataTable;
                        _htCompany.Add(companyGUID, _selectedCompanyInfo);
                    }
                    else if ((_htCompany[companyGUID] as CompanyInfo).DataSource == null)
                    {
                        (_htCompany[companyGUID] as CompanyInfo).DataSource = result.QueryResult as DataTable;
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

        private void OnDisplayGiaDichVuList(string contractGUID)
        {
            Result result = CompanyContractBus.GetDanhSachGiaDichVuList(contractGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {

                    DataTable dtGiaDichVu = result.QueryResult as DataTable;
                    dgGiaDichVu.DataSource = dtGiaDichVu;

                    if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty) return;
                    string companyGUID = cboCompany.SelectedValue.ToString();
                    if (!_htCompany.ContainsKey(companyGUID))
                    {
                        _selectedCompanyInfo = new CompanyInfo();
                        _selectedCompanyInfo.CompanyGUID = companyGUID;
                        _selectedCompanyInfo.GiaDichVuDataSource = dtGiaDichVu;
                        _htCompany.Add(companyGUID, _selectedCompanyInfo);
                    }
                    else if ((_htCompany[companyGUID] as CompanyInfo).GiaDichVuDataSource == null)
                    {
                        (_htCompany[companyGUID] as CompanyInfo).GiaDichVuDataSource = dtGiaDichVu;
                    }

                    if (dtGiaDichVu != null && dtGiaDichVu.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtGiaDichVu.Rows)
                        {
                            string giaDichVuHopDongGUID = row["GiaDichVuHopDongGUID"].ToString();
                            string serviceGUID = row["ServiceGUID"].ToString();

                            Result rs = CompanyContractBus.GetDichVuCon(giaDichVuHopDongGUID);
                            if (!rs.IsOK)
                            {
                                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetDichVuCon"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetDichVuCon"));
                                return;
                            }

                            if (_selectedCompanyInfo.DictDichVuCon == null) _selectedCompanyInfo.DictDichVuCon = new Dictionary<string, DataTable>();
                            if (!_selectedCompanyInfo.DictDichVuCon.ContainsKey(serviceGUID))
                                _selectedCompanyInfo.DictDichVuCon.Add(serviceGUID, rs.QueryResult as DataTable);
                            else
                                _selectedCompanyInfo.DictDichVuCon[serviceGUID] = rs.QueryResult as DataTable;
                        }
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetDanhSachGiaDichVuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetDanhSachGiaDichVuList"));
            }
        }

        private void OnAddMember()
        {
            if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.", IconType.Information);
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return;
            }

            if (dgGiaDichVu.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập nhập danh sách dịch vụ theo hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 1;
                btnAdd.Focus();
                return;
            }

            dlgMembers dlg = new dlgMembers(cboCompany.SelectedValue.ToString(), _contract.CompanyContractGUID.ToString(), 
                _selectedCompanyInfo.AddedMemberKeys, _selectedCompanyInfo.DeletedMemberRows, _selectedCompanyInfo.GiaDichVuDataSource);
            dlg.OnAddMemberEvent += new AddMemberHandler(dlg_OnAddMember);
            dlg.ShowDialog(this);
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

            if (!_isNew)
            {
                foreach (DataRow row in deletedRows)
                {
                    string patientGUID = row["patientGUID"].ToString();
                    Result result = CompanyContractBus.CheckNhanVienHopDongDaSuDungDichVu(patientGUID);
                    if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                    {
                        if (result.Error.Code == ErrorCode.EXIST)
                        {
                            MsgBox.Show(this.Text, string.Format("Nhân viên: '{0}' đã sử dụng dịch vụ trong hợp đồng, nên không thể xóa.", row["FullName"].ToString()), 
                                IconType.Information);
                            return;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckNhanVienHopDongDaSuDungDichVu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.CheckNhanVienHopDongDaSuDungDichVu"));
                        return;
                    }
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

            DataTable dt = member.DataSource.Copy();
            if (dgGiaDichVu.RowCount <= 0)
                dt.Rows.Clear();
            else
            {
                List<DataRow> deletedRows = new List<DataRow>();
                DataTable giaDichVuDataSource = dgGiaDichVu.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    DataRow[] rows = giaDichVuDataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows == null || rows.Length <= 0)
                        deletedRows.Add(row);
                }

                foreach (DataRow row in deletedRows)
                {
                    dt.Rows.Remove(row);
                }
            }

            dgService.DataSource = dt;
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
            if (dgGiaDichVu.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập danh sách dịch vụ cho hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 1;
                btnAdd.Focus();
                return;
            }

            if (_selectedCompanyInfo == null) return;

            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;
            DataRow drMember = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            if (!_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID)) return;
            Member member = (Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID];

            dlgServices dlg = new dlgServices(member.ConstractGUID, member.CompanyMemberGUID, member.AddedServices, 
                member.DeletedServiceRows, _selectedCompanyInfo.GiaDichVuDataSource);
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

            if (!_isNew)
            {
                string patientGUID = drMember["PatientGUID"].ToString();
                foreach (DataRow row in deletedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    Result result = CompanyContractBus.CheckDichVuHopDongDaSuDung(patientGUID, serviceGUID);
                    if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                    {
                        if (result.Error.Code == ErrorCode.EXIST)
                        {
                            MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã được sử dụng trong hợp đồng, nên không thể xóa.", row["Name"].ToString()),
                                IconType.Information);
                            return;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDung"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDung"));
                        return;
                    }
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

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = CheckedMemberRows;
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\DanhSachBenhNhan.xls", Application.StartupPath);
                if (ExportExcel.ExportDanhSachBenhNhanToExcel(exportFileName, checkedRows))
                    try
                    {
                        if (isPreview)
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanTemplate));
                        else
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanTemplate));
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                    }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = CheckedMemberRows;
            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                    ExportExcel.ExportDanhSachBenhNhanToExcel(dlg.FileName, checkedRows);
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private DataTable GetCheckList(DataRow drMember)
        {
            if (_selectedCompanyInfo == null) return null;

            string companyMemberGUID = drMember["CompanyMemberGUID"].ToString();
            string contractMemberGUID = drMember["ContractMemberGUID"].ToString();
            string patientGUID = drMember["PatientGUID"].ToString();

            DataTable dtCheckList = null;
            if (_selectedCompanyInfo.AddedMembers.ContainsKey(companyMemberGUID))
                dtCheckList = ((Member)_selectedCompanyInfo.AddedMembers[companyMemberGUID]).DataSource;
            else
            {
                Result result = null;
                if (_isNew)
                    result = CompanyContractBus.GetCheckList(contractMemberGUID);
                else
                    result = CompanyContractBus.GetCheckList(_contract.CompanyContractGUID.ToString(), patientGUID);

                if (result.IsOK)
                {
                    

                    dtCheckList = dgGiaDichVu.DataSource as DataTable;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                }
            }


            return dtCheckList;
        }

        private void OnPrintCheckList(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = CheckedMemberRows;
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\CheckList.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        DataTable dtCheckList = GetCheckList(row);
                        if (ExportExcel.ExportCheckListToExcel(exportFileName, row, dtCheckList))
                        {
                            try
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.CheckListTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }
                        }
                        else
                            return;
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            DataTable dtCheckList = GetCheckList(row);
                            if (ExportExcel.ExportCheckListToExcel(exportFileName, row, dtCheckList))
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.CheckListTemplate));
                                }
                                catch (Exception ex)
                                {
                                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                    return;
                                }
                            }
                            else
                                return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private void OnExportExcelCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = CheckedMemberRows;
            if (checkedRows.Count > 0)
            {
                foreach (DataRow row in checkedRows)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "Export Excel";
                    dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dtCheckList = GetCheckList(row);
                        ExportExcel.ExportCheckListToExcel(dlg.FileName, row, dtCheckList);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private void OnAddGiaDichVu()
        {
            if (_selectedCompanyInfo == null) return;

            if (_selectedCompanyInfo.GiaDichVuDataSource == null)
                _selectedCompanyInfo.GiaDichVuDataSource = dgGiaDichVu.DataSource as DataTable;

            dlgAddGiaDichVuHopDong dlg = new dlgAddGiaDichVuHopDong(_selectedCompanyInfo);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow newRow = _selectedCompanyInfo.GiaDichVuDataSource.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceGUID"] = dlg.ServiceGUID;
                newRow["Code"] = dlg.MaDichVu;
                newRow["Name"] = dlg.TenDichVu;
                newRow["Gia"] = dlg.Gia;

                _selectedCompanyInfo.GiaDichVuDataSource.Rows.Add(newRow);
            }
        }

        private void OnEditGiaDichVu()
        {
            if (_selectedCompanyInfo == null) return;
            if (dgGiaDichVu.SelectedRows == null || dgGiaDichVu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.", IconType.Information);
                return;
            }

            DataRow drGiaDichVu = (dgGiaDichVu.SelectedRows[0].DataBoundItem as DataRowView).Row;

            if (!_isNew)
            {
                string hopDongGUID = _drContract["CompanyContractGUID"].ToString();
                string serviceGUID = drGiaDichVu["ServiceGUID"].ToString();
                Result result = CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu(hopDongGUID, serviceGUID);
                if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    if (result.Error.Code == ErrorCode.EXIST)
                    {
                        MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã được sử dụng trong hợp đồng, nên không thể sửa.", drGiaDichVu["Name"].ToString()),
                            IconType.Information);
                        return;
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu"));
                    return;
                }
            }

           
            dlgAddGiaDichVuHopDong dlg = new dlgAddGiaDichVuHopDong(_selectedCompanyInfo, drGiaDichVu);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                drGiaDichVu["ServiceGUID"] = dlg.ServiceGUID;
                drGiaDichVu["Code"] = dlg.MaDichVu;
                drGiaDichVu["Name"] = dlg.TenDichVu;
                drGiaDichVu["Gia"] = dlg.Gia;

                DisplayDichVuCon();
            }
        }

        private void OnDeleteGiaDichVu()
        {
            if (_selectedCompanyInfo == null) return;
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgGiaDichVu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (!_isNew)
            {
                string hopDongGUID = _drContract["CompanyContractGUID"].ToString();
                foreach (DataRow row in deletedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    Result result = CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu(hopDongGUID, serviceGUID);
                    if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                    {
                        if (result.Error.Code == ErrorCode.EXIST)
                        {
                            MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã được sử dụng trong hợp đồng, nên không thể xóa.", row["Name"].ToString()),
                                IconType.Information);
                            return;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.CheckDichVuHopDongDaSuDungByGiaDichVu"));
                        return;
                    }
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        if (row["GiaDichVuHopDongGUID"] != null && row["GiaDichVuHopDongGUID"] != DBNull.Value)
                        {
                            string giaDichVuHopDongGUID = row["GiaDichVuHopDongGUID"].ToString();
                            string serviceGUID = row["ServiceGUID"].ToString();
                            if (!_selectedCompanyInfo.DeletedGiaDichVus.Contains(giaDichVuHopDongGUID))
                                _selectedCompanyInfo.DeletedGiaDichVus.Add(giaDichVuHopDongGUID);

                            
                        }

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }

        private string ReFormatDate(string type, string value)
        {
            string sRet = value;
            if (type.ToLower().StartsWith("m"))
            {
                char[] split = new char[] { '/' };
                string[] sTemp = value.Split(split, StringSplitOptions.None);
                if (sTemp.Count() == 3)
                {
                    sRet = sTemp[1] + "/" + sTemp[0] + "/" + sTemp[2];
                }
                else
                {
                    sRet = value;
                }
            }
            return sRet;
        }

        private string GetCellText(IRange cell)
        {
            try
            {
                string s = cell.Text;
                //process NULL text in excel 
                if (s.ToUpper() == "NULL")
                    s = "";

                string sType = cell.NumberFormat.Trim();
                if (sType.Contains("yy"))
                {
                    s = ReFormatDate(sType, s);
                }
                //process "'" character
                s = s.Replace("'", "''");
                if (cell.Font.Name.ToLower().IndexOf("vni") == 0)
                    s = Utility.ConvertVNI2Unicode(s);
                return s;
            }
            catch
            {
                return "";
            }
        }

        private void OnImportDVHD()
        {
            if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.", IconType.Information);
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return;
            }

            if (dgGiaDichVu.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập nhập danh sách dịch vụ theo hợp đồng.", IconType.Information);
                tabContract.SelectedTabIndex = 1;
                btnAdd.Focus();
                return;
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                IWorkbook workBook = null;
                string companyGUID = cboCompany.SelectedValue.ToString();
                DataTable dtService = dgGiaDichVu.DataSource as DataTable;

                try
                {
                    workBook = SpreadsheetGear.Factory.GetWorkbook(dlg.FileName);
                    string msg = string.Empty;
                    
                    foreach (IWorksheet workSheet in workBook.Worksheets)
                    {
                        //Check Header
                        //STT
                        string value = workSheet.Cells[0, 0].Text;
                        if (value.Trim() == string.Empty || value.ToLower() != "STT".ToLower())
                        {
                            msg += string.Format("Sheet '{0}' không đúng định dạng nên không được nhập.\n", workSheet.Name);
                            continue;
                        }

                        //Họ và tên
                        value = workSheet.Cells[0, 1].Text;
                        if (value.Trim() == string.Empty || (value.ToLower() != "Họ và tên".ToLower() && 
                            value.ToLower() != "Họ tên".ToLower() && value.ToLower() != "Fullname".ToLower() &&
                            value.ToLower() != "Full Name".ToLower()))
                        {
                            msg += string.Format("Sheet '{0}' không đúng định dạng nên không được nhập.\n", workSheet.Name);
                            continue;
                        }

                        //Năm sinh
                        value = workSheet.Cells[0, 2].Text;
                        if (value.Trim() == string.Empty || (value.ToLower() != "Năm sinh".ToLower() &&
                            value.ToLower() != "Dob".ToLower()))
                        {
                            msg += string.Format("Sheet '{0}' không đúng định dạng nên không được nhập.\n", workSheet.Name);
                            continue;
                        }

                        //Giới tính
                        value = workSheet.Cells[0, 3].Text;
                        if (value.Trim() == string.Empty || (value.ToLower() != "Giới tính".ToLower() &&
                            value.ToLower() != "Gender".ToLower()))
                        {
                            msg += string.Format("Sheet '{0}' không đúng định dạng nên không được nhập.\n", workSheet.Name);
                            continue;
                        }

                        int rowCount = workSheet.UsedRange.RowCount;
                        int colCount = workSheet.UsedRange.ColumnCount;

                        for (int i = 1; i < rowCount; i++)
                        {
                            string hoTen = GetCellText(workSheet.Cells[i, 1]);
                            if (hoTen.Trim() == string.Empty) continue;
                            string ngaySinh = GetCellText(workSheet.Cells[i, 2]);
                            string gioiTinh = GetCellText(workSheet.Cells[i, 3]);

                            Result result = CompanyContractBus.GetCompanyMember(companyGUID, hoTen, ngaySinh, gioiTinh);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCompanyMember"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCompanyMember"));
                                return;
                            }

                            DataTable dtMember = result.QueryResult as DataTable;
                            if (dtMember == null || dtMember.Rows.Count <= 0) continue;

                            List<string> addedServices = new List<string>();
                            DataTable dtAddedService = dtService.Clone();
                            List<DataRow> checkedMembers = new List<DataRow>();
                            checkedMembers.Add(dtMember.Rows[0]);

                            for (int j = 4; j < colCount; j++)
                            {
                                string isChecked =GetCellText(workSheet.Cells[i, j]);
                                if (isChecked.Trim().ToLower() != "X".ToLower()) continue;
                                string serviceName = GetCellText(workSheet.Cells[0, j]);
                                if (serviceName.Trim() == string.Empty) continue;

                                DataRow[] rows = dtService.Select(string.Format("Name = '{0}'", serviceName));
                                if (rows == null || rows.Length <= 0) continue;

                                addedServices.Add(rows[0]["ServiceGUID"].ToString());

                                DataRow newRow = dtAddedService.NewRow();
                                newRow.ItemArray = rows[0].ItemArray;
                                dtAddedService.Rows.Add(newRow);
                            }

                            dlg_OnAddMember(checkedMembers, addedServices, dtAddedService);
                        }
                    }

                    if (msg == string.Empty) msg = "Nhập dữ liệu từ Excel hoàn tất.";
                    MsgBox.Show(this.Text, msg, IconType.Information);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(this.Text, ex.Message, IconType.Error);
                    Utility.WriteToTraceLog(ex.Message);
                }
                finally
                {
                    if (workBook != null)
                    {
                        workBook.Close();
                        workBook = null;
                    }
                }
            }
        }

        private void DisplayDichVuCon()
        {
            if (_selectedCompanyInfo == null || dgGiaDichVu.SelectedRows == null || dgGiaDichVu.SelectedRows.Count <= 0 ||
                _selectedCompanyInfo.DictDichVuCon == null)
            {
                dgDichVuCon.DataSource = null;
                return;
            }

            DataRow drGiaDichVu = (dgGiaDichVu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string serviceGUID = drGiaDichVu["ServiceGUID"].ToString();

            DataTable dtDichVuCon = _selectedCompanyInfo.DictDichVuCon[serviceGUID];
            dgDichVuCon.DataSource = dtDichVuCon;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddContract_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                DisplayDetailAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drContract);

        }

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
                    newRow["PatientGUID"] = row["PatientGUID"];
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

                if (!_isLock && _allowEdit)
                {
                    if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin hợp đồng ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CheckInfo())
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            SaveInfoAsThread();
                        }
                        else
                            e.Cancel = true;
                    }
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
            if (cboCompany.SelectedValue == null || cboCompany.Text == string.Empty) return;
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

                _flag = false;
                this.Close();
            }
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgGiaDichVu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
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
            dtpkEndDate.Visible = chkCompleted.Checked;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnPrintPreviewCheckList_Click(object sender, EventArgs e)
        {
            OnPrintCheckList(true);
        }

        private void btnPrintCheckList_Click(object sender, EventArgs e)
        {
            OnPrintCheckList(false);
        }

        private void btnExportExcelCheckList_Click(object sender, EventArgs e)
        {
            OnExportExcelCheckList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddGiaDichVu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditGiaDichVu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteGiaDichVu();
        }

        private void dgGiaDichVu_DoubleClick(object sender, EventArgs e)
        {
            if (!_isNew && _isLock) return;
            if (!_allowEdit) return;
            OnEditGiaDichVu();
        }

        private void tabContract_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabContract.SelectedTabIndex == 2)
            {
                OnDisplayCheckList();
            }
        }

        private void btnImportDSNV_Click(object sender, EventArgs e)
        {
            OnImportDVHD();
        }

        private void dgGiaDichVu_SelectionChanged(object sender, EventArgs e)
        {
            DisplayDichVuCon();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddGiaDichVu();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditGiaDichVu();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteGiaDichVu();
        }

        private void themDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void xoaDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void themNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void xoaNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void xemBanInDSNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void inDSNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void xuatExcelDSNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void xemBanInChecklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrintCheckList(true);
        }

        private void inChecklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrintCheckList(false);
        }

        private void xuatExcelChecklistTtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcelCheckList();
        }

        private void nhapDVHDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnImportDVHD();
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
                OnDisplayGiaDichVuList(state.ToString());
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
