using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddServiceHistory : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private ServiceHistory _serviceHistory = new ServiceHistory();
        private string _patientGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddServiceHistory(string patientGUID)
        {
            InitializeComponent();
            InitData();
            _patientGUID = patientGUID;
        }

        public dlgAddServiceHistory(string patientGUID, DataRow drServiceHistory)
        {
            InitializeComponent();
            _isNew = false;
            InitData();
            _patientGUID = patientGUID;
            this.Text = "Sua su dung dich vu";
            DisplayInfo(drServiceHistory);
        }
        #endregion

        #region Properties
        public ServiceHistory ServiceHistory
        {
            get { return _serviceHistory; }
        }

        public string FullName
        {
            get { return cboDocStaff.Text; }
        }

        public string ServiceName
        {
            get { return cboService.Text; }
        }

        public string ServiceCode
        {
            get
            {
                DataTable dt = cboService.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return string.Empty;

                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                    return rows[0]["Code"].ToString();

                return string.Empty;
            }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkActiveDate.Value = DateTime.Now;

            //Service
            Result result = ServicesBus.GetServicesList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                return;
            }
            else
            {
                cboService.DataSource = result.QueryResult;
            }

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.Doctor);
            staffTypes.Add((byte)StaffType.Nurse);
            result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }
        }

        private void DisplayInfo(DataRow drServiceHistory)
        {
            try
            {
                cboDocStaff.SelectedValue = drServiceHistory["DocStaffGUID"].ToString();
                cboService.SelectedValue = drServiceHistory["ServiceGUID"].ToString();
                numPrice.Value = (decimal)Double.Parse(drServiceHistory["FixedPrice"].ToString());
                txtDescription.Text = drServiceHistory["Note"] as string;
                _serviceHistory.ServiceHistoryGUID = Guid.Parse(drServiceHistory["ServiceHistoryGUID"].ToString());

                if (drServiceHistory["ActivedDate"] != null && drServiceHistory["ActivedDate"] != DBNull.Value)
                {
                    _serviceHistory.ActivedDate = Convert.ToDateTime(drServiceHistory["ActivedDate"]);
                    dtpkActiveDate.Value = _serviceHistory.ActivedDate.Value;
                }

                if (drServiceHistory["CreatedDate"] != null && drServiceHistory["CreatedDate"] != DBNull.Value)
                    _serviceHistory.CreatedDate = Convert.ToDateTime(drServiceHistory["CreatedDate"]);

                if (drServiceHistory["CreatedBy"] != null && drServiceHistory["CreatedBy"] != DBNull.Value)
                    _serviceHistory.CreatedBy = Guid.Parse(drServiceHistory["CreatedBy"].ToString());

                if (drServiceHistory["UpdatedDate"] != null && drServiceHistory["UpdatedDate"] != DBNull.Value)
                    _serviceHistory.UpdatedDate = Convert.ToDateTime(drServiceHistory["UpdatedDate"]);

                if (drServiceHistory["UpdatedBy"] != null && drServiceHistory["UpdatedBy"] != DBNull.Value)
                    _serviceHistory.UpdatedBy = Guid.Parse(drServiceHistory["UpdatedBy"].ToString());

                if (drServiceHistory["DeletedDate"] != null && drServiceHistory["DeletedDate"] != DBNull.Value)
                    _serviceHistory.DeletedDate = Convert.ToDateTime(drServiceHistory["DeletedDate"]);

                if (drServiceHistory["DeletedBy"] != null && drServiceHistory["DeletedBy"] != DBNull.Value)
                    _serviceHistory.DeletedBy = Guid.Parse(drServiceHistory["DeletedBy"].ToString());

                _serviceHistory.Status = Convert.ToByte(drServiceHistory["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboService.Text == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.");
                cboService.Focus();
                return false;
            }

            if (cboDocStaff.Text == null || cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ");
                cboDocStaff.Focus();
                return false;
            }

            return true;
        }

        private void SetServiceHistoryInfo()
        {
            try
            {
                if (_isNew)
                {
                    _serviceHistory.CreatedDate = DateTime.Now;
                    _serviceHistory.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _serviceHistory.UpdatedDate = DateTime.Now;
                    _serviceHistory.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _serviceHistory.PatientGUID = Guid.Parse(_patientGUID);

                MethodInvoker method = delegate
                {
                    _serviceHistory.ActivedDate = dtpkActiveDate.Value;
                    _serviceHistory.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    _serviceHistory.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());
                    _serviceHistory.Price = (double)numPrice.Value;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
                
                _serviceHistory.Note = txtDescription.Text;
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnSaveInfo()
        {
            SetServiceHistoryInfo();
            Result result = ServiceHistoryBus.InsertServiceHistory(_serviceHistory);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceHistoryBus.InsertServiceHistory"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.InsertServiceHistory"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
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
        #endregion

        #region Window Event Handlers
        private void dlgAddServiceHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void cboService_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboService.SelectedValue == null || cboService.SelectedValue.ToString() == string.Empty) return;
            DataTable dt = cboService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string serviceGUID = cboService.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            if (rows != null && rows.Length > 0)
                 numPrice.Value = (decimal)Double.Parse(rows[0]["Price"].ToString());
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                Thread.Sleep(500);
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
