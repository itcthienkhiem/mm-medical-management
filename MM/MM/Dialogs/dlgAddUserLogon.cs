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
    public partial class dlgAddUserLogon : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Logon _logon = new Logon();
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
            DisplayInfo(drLogon);
        }
        #endregion

        #region Properties
        public Logon Logon
        {
            get { return _logon; }
            set { _logon = value; }
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
                        }
                        else
                        {
                            MsgBox.Show(this.Text, funcResult.GetErrorAsString("LogonBus.GetFunction"));
                            Utility.WriteToTraceLog(funcResult.GetErrorAsString("LogonBus.GetFunction"));
                        }
                    }
                    else
                        dgPermission.DataSource = result.QueryResult as DataTable;
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
        #endregion

        #region Window Event Handlers
        private void dlgAddUserLogon_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayPermissionAsThread(Guid.Empty.ToString());
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
        #endregion
    }
}
