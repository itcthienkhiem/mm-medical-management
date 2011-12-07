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
    public partial class dlgAddCompany : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Company _company = new Company();
        private List<string> _addedPatients = new List<string>();
        private List<string> _deletedPatients = new List<string>();
        private List<DataRow> _deletedPatientRows = new List<DataRow>();
        #endregion

        #region Constructor
        public dlgAddCompany()
        {
            InitializeComponent();
            DisplayMembersAsThread(Guid.Empty.ToString());
            GenerateCode();
        }

        public dlgAddCompany(DataRow drCompany)
        {
            InitializeComponent();
            _isNew  = false;
            this.Text = "Sua cong ty";
            DisplayInfo(drCompany);
        }
        #endregion

        #region Properties
        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = CompanyBus.GetCompanyCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaCongTy.Text = Utility.GetCode("CTY", count + 1);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyCount"));
            }
        }
       
        private void DisplayInfo(DataRow drCompany)
        {
            try
            {
                txtMaCongTy.Text = drCompany["MaCty"] as string;
                txtTenCongTy.Text = drCompany["TenCty"] as string;
                txtDiaChi.Text = drCompany["DiaChi"] as string;
                txtDienThoai.Text = drCompany["DienThoai"] as string;
                txtFax.Text = drCompany["Fax"] as string;
                txtWebsite.Text = drCompany["Website"] as string;

                _company.CompanyGUID = Guid.Parse(drCompany["CompanyGUID"].ToString());

                if (drCompany["CreatedDate"] != null && drCompany["CreatedDate"] != DBNull.Value)
                    _company.CreatedDate = Convert.ToDateTime(drCompany["CreatedDate"]);

                if (drCompany["CreatedBy"] != null && drCompany["CreatedBy"] != DBNull.Value)
                    _company.CreatedBy = Guid.Parse(drCompany["CreatedBy"].ToString());

                if (drCompany["UpdatedDate"] != null && drCompany["UpdatedDate"] != DBNull.Value)
                    _company.UpdatedDate = Convert.ToDateTime(drCompany["UpdatedDate"]);

                if (drCompany["UpdatedBy"] != null && drCompany["UpdatedBy"] != DBNull.Value)
                    _company.UpdatedBy = Guid.Parse(drCompany["UpdatedBy"].ToString());

                if (drCompany["DeletedDate"] != null && drCompany["DeletedDate"] != DBNull.Value)
                    _company.DeletedDate = Convert.ToDateTime(drCompany["DeletedDate"]);

                if (drCompany["DeletedBy"] != null && drCompany["DeletedBy"] != DBNull.Value)
                    _company.DeletedBy = Guid.Parse(drCompany["DeletedBy"].ToString());

                _company.Status = Convert.ToByte(drCompany["Status"]);

                DisplayMembersAsThread(_company.CompanyGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayMembersAsThread(string companyGUID)
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayMembersProc), companyGUID);
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

        private void OnDisplayMembers(string companyGUID)
        {
            Result result = CompanyBus.GetCompanyMemberList(companyGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgMembers.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtMaCongTy.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã công ty.", IconType.Information);
                txtMaCongTy.Focus();
                return false;
            }

            if (txtTenCongTy.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                txtTenCongTy.Focus();
                return false;
            }

            string comGUID = _isNew ? string.Empty : _company.CompanyGUID.ToString();
            Result result = CompanyBus.CheckCompanyExistCode(comGUID, txtMaCongTy.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã công ty này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaCongTy.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.CheckCompanyExistCode"), IconType.Error);
                return false;
            }

            foreach (string patientGUID in _addedPatients)
            {
                result = CompanyBus.CheckMemberExist(patientGUID);

                if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    if (result.Error.Code == ErrorCode.EXIST)
                    {
                        string fullName = GetFullName(patientGUID);
                        MsgBox.Show(this.Text, string.Format("Bệnh nhân: '{0}' đã thuộc 1 công ty khác.", fullName), IconType.Information);

                        DataTable dt = dgMembers.DataSource as DataTable;
                        if (dt != null)
                        {
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                            if (rows != null && rows.Length > 0)
                                dt.Rows.Remove(rows[0]);
                        }

                        return false;
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.CheckMemberExist"), IconType.Error);
                    return false;
                }
            }

            return true;
        }

        private string GetFullName(string patientGUID)
        {
            string fullName = string.Empty;
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt != null)
            {
                DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                if (rows != null && rows.Length > 0)
                    fullName = rows[0]["FullName"].ToString();
            }

            return fullName;
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
                _company.MaCty = txtMaCongTy.Text;
                _company.TenCty = txtTenCongTy.Text;
                _company.DiaChi = txtDiaChi.Text;
                _company.Dienthoai = txtDienThoai.Text;
                _company.Fax = txtFax.Text;
                _company.Website = txtWebsite.Text;
                _company.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _company.CreatedDate = DateTime.Now;
                    _company.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _company.UpdatedDate = DateTime.Now;
                    _company.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = CompanyBus.InsertCompany(_company, _addedPatients, _deletedPatients);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.InsertCompany"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.InsertCompany"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnAddMember()
        {
            dlgMembers dlg = new dlgMembers(_addedPatients, _deletedPatientRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.CheckedMembers;
                DataTable dataSource = dgMembers.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["PatientGUID"] = patientGUID;
                        newRow["FileNum"] = row["FileNum"];
                        newRow["FullName"] = row["FullName"];
                        newRow["DobStr"] = row["DobStr"];
                        newRow["GenderAsStr"] = row["GenderAsStr"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedPatients.Contains(patientGUID))
                            _addedPatients.Add(patientGUID);

                        _deletedPatients.Remove(patientGUID);
                        foreach (DataRow r in _deletedPatientRows)
                        {
                            if (r["PatientGUID"].ToString() == patientGUID)
                            {
                                _deletedPatientRows.Remove(r);
                                break;
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
                    deletedMemList.Add(row["PatientGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedMemList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhân viên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string patientGUID = row["PatientGUID"].ToString();
                        if (!_deletedPatients.Contains(patientGUID))
                        {
                            _deletedPatients.Add(patientGUID);

                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _deletedPatientRows.Add(r);
                        }

                        _addedPatients.Remove(patientGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những nhân viên cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void dlgAddCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
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

        private void OnDisplayMembersProc(object state)
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
