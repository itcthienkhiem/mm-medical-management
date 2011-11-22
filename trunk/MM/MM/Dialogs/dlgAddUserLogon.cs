﻿using System;
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
    public partial class dlgAddUserLogon : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Logon _logon = new Logon();
        private DataRow _drLogon = null;
        #endregion

        #region Constructor
        public dlgAddUserLogon()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddUserLogon(DataRow drLogon)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nguoi su dung";
            InitData();
            _drLogon = drLogon;
            
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
                        case StaffType.Doctor:
                            return "Bác sĩ";
                        case StaffType.Nurse:
                            return "Y tá";
                        case StaffType.Reception:
                            return "Lễ tân";
                        case StaffType.Patient:
                            return "Bệnh nhân";
                        case StaffType.Admin:
                            return "Admin";
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
            Result result = DocStaffBus.GetDocStaffList();
            if (result.IsOK)
            {
                cboDocStaff.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
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
                MsgBox.Show(this.Text, e.Message);
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
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void UpdateGUI()
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                string functionCode = row.Cells["FunctionCode"].Value.ToString();
                if (functionCode == Const.DocStaff)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Patient)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Speciality)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Company)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Services)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ServicePrice)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Contract)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.OpenPatient)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Permission)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Symptom)
                {

                }
            }
        }

        private void OnDisplayPermission(string logonGUID)
        {
            Result result = LogonBus.GetPermission(logonGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    if (_isNew)
                    {
                        Result funcResult = LogonBus.GetFunction();
                        if (funcResult.IsOK)
                        {
                            DataTable dtPermission = result.QueryResult as DataTable;
                            DataTable dtFunction = funcResult.QueryResult as DataTable;
                            foreach (DataRow row in dtFunction.Rows)
                            {
                                DataRow newRow = dtPermission.NewRow();
                                newRow["FunctionGUID"] = row["FunctionGUID"];
                                newRow["FunctionCode"] = row["FunctionCode"];
                                newRow["FunctionName"] = row["FunctionName"];
                                newRow["IsView"] = false;
                                newRow["IsAdd"] = false;
                                newRow["IsEdit"] = false;
                                newRow["IsDelete"] = false;
                                newRow["IsPrint"] = false;
                                newRow["IsExport"] = false;
                                dtPermission.Rows.Add(newRow);
                            }

                            dgPermission.DataSource = dtPermission;
                            UpdateGUI();
                        }
                        else
                        {
                            MsgBox.Show(this.Text, funcResult.GetErrorAsString("LogonBus.GetFunction"));
                            Utility.WriteToTraceLog(funcResult.GetErrorAsString("LogonBus.GetFunction"));
                        }
                    }
                    else
                    {
                        dgPermission.DataSource = result.QueryResult as DataTable;
                        UpdateGUI();
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.GetPermission"));
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetPermission"));
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.");
                cboDocStaff.Focus();
                return false;
            }

            if (!Utility.IsValidPassword(txtPassword.Text))
            {
                MsgBox.Show(this.Text, "Mật khẩu không hợp lệ (4-12 kí tự). Vui lòng nhập lại.");
                txtPassword.Focus();
                return false;
            }

            string logonGUID = _isNew ? string.Empty : _logon.LogonGUID.ToString();
            Result result = LogonBus.CheckUserLogonExist(logonGUID, cboDocStaff.SelectedValue.ToString());

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Bác sĩ này đã được cấp tài khoản đăng nhập rồi. Vui lòng chọn bác sĩ khác.");
                    cboDocStaff.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.CheckUserLogonExist"));
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
                    Result result = LogonBus.InsertUserLogon(_logon, dtPermission);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.InsertUserLogon"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.InsertUserLogon"));
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
                MsgBox.Show(this.Text, e.Message);
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
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
