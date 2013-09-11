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
using MM.Bussiness;
using MM.Databasae;
using MM.Controls;

namespace MM.Dialogs
{
    public partial class dlgAddNguoiSuDung : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Logon _logon = new Logon();
        private DataRow _drLogon = null;
        #endregion

        #region Constructor
        public dlgAddNguoiSuDung()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddNguoiSuDung(DataRow drLogon)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nguoi su dung";
            _drLogon = drLogon;
            cboDocStaff.Enabled = false;
            InitData();
        }
        #endregion

        #region Properties
        public Logon Logon
        {
            get { return _logon; }
            set { _logon = value; }
        }

        public string StaffTypeStr
        {
            get
            {
                DataTable dt = cboDocStaff.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", cboDocStaff.SelectedValue.ToString()));
                if (rows != null && rows.Length > 0)
                {
                    StaffType type = (StaffType)Convert.ToInt32(rows[0]["StaffType"]);
                    switch (type)
                    {
                        case StaffType.BacSi:
                            return "Bác sĩ";
                        case StaffType.DieuDuong:
                            return "Điều dưỡng";
                        case StaffType.LeTan:
                            return "Lễ tân";
                        case StaffType.BenhNhan:
                            return "Bệnh nhân";
                        case StaffType.Admin:
                            return "Admin";
                        case StaffType.KeToan:
                            return "Kế toán";
                        case StaffType.ThuKyYKhoa:
                            return "Thư ký y khoa";
                        case StaffType.XetNghiem:
                            return "Xét nghiệm";
                        case StaffType.Sale:
                            return "Sale";
                        case StaffType.BacSiSieuAm:
                            return "Bác sĩ siêu âm";
                        case StaffType.BacSiNgoaiTongQuat:
                            return "Bác sĩ ngoại tổng quát";
                        case StaffType.BacSiNoiTongQuat:
                            return "Bác sĩ nội tổng quát";
                        case StaffType.BacSiPhuKhoa:
                            return "Bác sĩ phụ khoa";
                        default:
                            return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
        }

        public string FullName
        {
            get
            {
                DataTable dt = cboDocStaff.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", cboDocStaff.SelectedValue.ToString()));
                if (rows != null && rows.Length > 0)
                    return rows[0]["FullName"].ToString();
                else
                    return string.Empty;
            }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            string docStaffGUID = _isNew ? Guid.Empty.ToString() : _drLogon["DocStaffGUID"].ToString();
            Result result = DocStaffBus.GetDocStaffListWithoutLogon(docStaffGUID);
            if (result.IsOK)
            {
                cboDocStaff.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private void DisplayInfo(DataRow drLogon)
        {
            try
            {
                RijndaelCrypto crypt = new RijndaelCrypto();
                cboDocStaff.SelectedValue = drLogon["DocStaffGUID"].ToString();
                txtPassword.Text = crypt.Decrypt(drLogon["Password"].ToString());

                _logon.LogonGUID = Guid.Parse(drLogon["LogonGUID"].ToString());

                if (drLogon["CreatedDate"] != null && drLogon["CreatedDate"] != DBNull.Value)
                    _logon.CreatedDate = Convert.ToDateTime(drLogon["CreatedDate"]);

                if (drLogon["CreatedBy"] != null && drLogon["CreatedBy"] != DBNull.Value)
                    _logon.CreatedBy = Guid.Parse(drLogon["CreatedBy"].ToString());

                if (drLogon["UpdatedDate"] != null && drLogon["UpdatedDate"] != DBNull.Value)
                    _logon.UpdatedDate = Convert.ToDateTime(drLogon["UpdatedDate"]);

                if (drLogon["UpdatedBy"] != null && drLogon["UpdatedBy"] != DBNull.Value)
                    _logon.UpdatedBy = Guid.Parse(drLogon["UpdatedBy"].ToString());

                if (drLogon["DeletedDate"] != null && drLogon["DeletedDate"] != DBNull.Value)
                    _logon.DeletedDate = Convert.ToDateTime(drLogon["DeletedDate"]);

                if (drLogon["DeletedBy"] != null && drLogon["DeletedBy"] != DBNull.Value)
                    _logon.DeletedBy = Guid.Parse(drLogon["DeletedBy"].ToString());

                _logon.Status = Convert.ToByte(drLogon["Status"]);

                DisplayPermissionAsThread(_logon.LogonGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayPermissionAsThread(string logonGUID)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPermissionProc), logonGUID);
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

        private void OnDisplayPermission(string logonGUID)
        {
            Result result = UserGroupBus.GetUserGroupList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dtUserGroup = result.QueryResult as DataTable;
                    dgPermission.DataSource = dtUserGroup;

                    if (!_isNew)
                    {
                        result = UserGroupBus.GetNhomNguoiSuDung(logonGUID);
                        if (result.IsOK)
                        {
                            List<UserGroup> userGroups = result.QueryResult as List<UserGroup>;
                            foreach (var usrgr in userGroups)
                            {
                                DataRow[] rows = dtUserGroup.Select(string.Format("UserGroupGUID='{0}'", usrgr.UserGroupGUID.ToString()));
                                if (rows != null && rows.Length > 0)
                                    rows[0]["Checked"] = true;
                            }
                        }
                        else
                        {
                            MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.GetNhomNguoiSuDung"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.GetNhomNguoiSuDung"));
                        }
                    }

                    if (Global.UserGUID != Guid.Empty.ToString())
                    {
                        UpdateIgnorePermission();
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.GetUserGroupList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.GetUserGroupList"));
            }
        }

        private void UpdateIgnorePermission()
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                string userGroupGUID = dr["UserGroupGUID"].ToString();
                Result result = UserGroupBus.CheckIgnorePermission(userGroupGUID);
                DataGridViewDisableCheckBoxCell cell = row.Cells[1] as DataGridViewDisableCheckBoxCell;
                if (result.IsOK)
                {
                    bool isOK = Convert.ToBoolean(result.QueryResult);
                    cell.Enabled = isOK;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.CheckIgnorePermission"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.CheckIgnorePermission"));
                    cell.Enabled = false;
                }
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            if (!Utility.IsValidPassword(txtPassword.Text))
            {
                MsgBox.Show(this.Text, "Mật khẩu không hợp lệ (4-12 kí tự). Vui lòng nhập lại.", IconType.Information);
                txtPassword.Focus();
                return false;
            }

            string logonGUID = _isNew ? string.Empty : _logon.LogonGUID.ToString();
            Result result = LogonBus.CheckUserLogonExist(logonGUID, cboDocStaff.SelectedValue.ToString());

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Bác sĩ này đã được cấp tài khoản đăng nhập rồi. Vui lòng chọn bác sĩ khác.", IconType.Information);
                    cboDocStaff.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.CheckUserLogonExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.CheckUserLogonExist"));
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

                RijndaelCrypto crypt = new RijndaelCrypto();
                _logon.Status = (byte)Status.Actived;
                _logon.Password = crypt.Encrypt(txtPassword.Text);


                if (_isNew)
                {
                    _logon.CreatedDate = DateTime.Now;
                    _logon.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _logon.UpdatedDate = DateTime.Now;
                    _logon.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _logon.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());

                    DataTable dtPermission = dgPermission.DataSource as DataTable;
                    Result result = LogonBus.InsertUserLogon2(_logon, dtPermission);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.InsertUserLogon2"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.InsertUserLogon2"));
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
        #endregion

        #region Window Event Handlers
        private void dlgAddUserLogon_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayPermissionAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drLogon);
        }

        private void dlgAddUserLogon_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin người sử dụng ?") == System.Windows.Forms.DialogResult.Yes)
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
        #endregion

        #region Working Thread
        private void OnDisplayPermissionProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayPermission(state.ToString());
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
        #endregion

        
    }
}
