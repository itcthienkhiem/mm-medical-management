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
    public partial class dlgAddKetQuaCanLamSang : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private KetQuaCanLamSang _ketQuaCanLamSang = new KetQuaCanLamSang();
        private string _patientGUID = string.Empty;
        private DataRow _drKetQuaCanLamSang = null;
        private StaffType _staffType = StaffType.None;
        private string _serviceGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddKetQuaCanLamSang(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetQuaCanLamSang(string patientGUID, DataRow drKetQuaCanLamSang)
        {
            InitializeComponent();
            _isNew = false;
            _patientGUID = patientGUID;
            this.Text = "Sua ket qua can lam sang";
            _drKetQuaCanLamSang = drKetQuaCanLamSang;
        }
        #endregion

        #region Properties
        public KetQuaCanLamSang KetQuaCanLamSang
        {
            get { return _ketQuaCanLamSang; }
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
            Cursor.Current = Cursors.WaitCursor;
            dtpkActiveDate.Value = DateTime.Now;

            //Service
            Result result = ServicesBus.GetServicesList(ServiceType.CanLamSang);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                return;
            }
            else
            {
                cboService.DataSource = result.QueryResult;
            }
        }

        private void DisplayDocStaffList()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            if (_staffType != StaffType.None)
            {
                if (_staffType == StaffType.BacSi)
                {
                    staffTypes.Add((byte)StaffType.BacSi);
                    staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
                    staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
                    staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
                    staffTypes.Add((byte)StaffType.BacSiSieuAm);
                    staffTypes.Add((byte)StaffType.XetNghiem);
                }
                else
                    staffTypes.Add((byte)_staffType);
            }
            else
            {
                staffTypes.Add((byte)StaffType.BacSi);
                staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
                staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
                staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
                staffTypes.Add((byte)StaffType.BacSiSieuAm);
                staffTypes.Add((byte)StaffType.XetNghiem);
            }

            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboDocStaff.DataSource = dt;
            }

            cboDocStaff.SelectedValue = Global.UserGUID;

            if (cboDocStaff.SelectedValue != null && cboDocStaff.SelectedValue.ToString() != Guid.Empty.ToString())
                cboDocStaff.Enabled = false;
            else
                cboDocStaff.Enabled = true;
        }

        private void DisplayInfo(DataRow drKetQuaCanLamSang)
        {
            try
            {
                _ketQuaCanLamSang.KetQuaCanLamSangGUID = Guid.Parse(drKetQuaCanLamSang["KetQuaCanLamSangGUID"].ToString());

                cboService.SelectedValue = drKetQuaCanLamSang["ServiceGUID"].ToString();
                cboDocStaff.SelectedValue = drKetQuaCanLamSang["BacSiThucHienGUID"].ToString();

                bool isNormalOrNegative = Convert.ToBoolean(drKetQuaCanLamSang["IsNormalOrNegative"]);
                bool normal = Convert.ToBoolean(drKetQuaCanLamSang["Normal"]);
                bool abnormal = Convert.ToBoolean(drKetQuaCanLamSang["Abnormal"]);
                bool negative = Convert.ToBoolean(drKetQuaCanLamSang["Negative"]);
                bool positive = Convert.ToBoolean(drKetQuaCanLamSang["Positive"]);

                raNormal.Checked = isNormalOrNegative;
                raNegative.Checked = !isNormalOrNegative;
                chkNormal.Checked = normal;
                chkAbnormal.Checked = abnormal;
                chkNegative.Checked = negative;
                chkPositive.Checked = positive;

                txtDescription.Text = drKetQuaCanLamSang["Note"] as string;
                
                dtpkActiveDate.Value = Convert.ToDateTime(drKetQuaCanLamSang["NgayKham"]);

                if (drKetQuaCanLamSang["CreatedDate"] != null && drKetQuaCanLamSang["CreatedDate"] != DBNull.Value)
                    _ketQuaCanLamSang.CreatedDate = Convert.ToDateTime(drKetQuaCanLamSang["CreatedDate"]);

                if (drKetQuaCanLamSang["CreatedBy"] != null && drKetQuaCanLamSang["CreatedBy"] != DBNull.Value)
                    _ketQuaCanLamSang.CreatedBy = Guid.Parse(drKetQuaCanLamSang["CreatedBy"].ToString());

                if (drKetQuaCanLamSang["UpdatedDate"] != null && drKetQuaCanLamSang["UpdatedDate"] != DBNull.Value)
                    _ketQuaCanLamSang.UpdatedDate = Convert.ToDateTime(drKetQuaCanLamSang["UpdatedDate"]);

                if (drKetQuaCanLamSang["UpdatedBy"] != null && drKetQuaCanLamSang["UpdatedBy"] != DBNull.Value)
                    _ketQuaCanLamSang.UpdatedBy = Guid.Parse(drKetQuaCanLamSang["UpdatedBy"].ToString());

                if (drKetQuaCanLamSang["DeletedDate"] != null && drKetQuaCanLamSang["DeletedDate"] != DBNull.Value)
                    _ketQuaCanLamSang.DeletedDate = Convert.ToDateTime(drKetQuaCanLamSang["DeletedDate"]);

                if (drKetQuaCanLamSang["DeletedBy"] != null && drKetQuaCanLamSang["DeletedBy"] != DBNull.Value)
                    _ketQuaCanLamSang.DeletedBy = Guid.Parse(drKetQuaCanLamSang["DeletedBy"].ToString());

                _ketQuaCanLamSang.Status = Convert.ToByte(drKetQuaCanLamSang["KetQuaCanLamSangStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboService.SelectedValue == null || cboService.Text == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
                return false;
            }

            return true;
        }

        private void OnSaveInfo()
        {
            try
            {
                if (_isNew)
                {
                    _ketQuaCanLamSang.CreatedDate = DateTime.Now;
                    _ketQuaCanLamSang.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaCanLamSang.UpdatedDate = DateTime.Now;
                    _ketQuaCanLamSang.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _ketQuaCanLamSang.PatientGUID = Guid.Parse(_patientGUID);
                    _ketQuaCanLamSang.Note = txtDescription.Text;
                    _ketQuaCanLamSang.NgayKham = dtpkActiveDate.Value;
                    
                    if (cboDocStaff.SelectedValue != null && cboDocStaff.Text.Trim() != string.Empty)
                        _ketQuaCanLamSang.BacSiThucHienGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    else
                        _ketQuaCanLamSang.BacSiThucHienGUID = null;

                    _ketQuaCanLamSang.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());

                    _ketQuaCanLamSang.IsNormalOrNegative = raNormal.Checked;
                    if (raNormal.Checked)
                    {
                        _ketQuaCanLamSang.Normal = chkNormal.Checked;
                        _ketQuaCanLamSang.Abnormal = chkAbnormal.Checked;
                        _ketQuaCanLamSang.Negative = false;
                        _ketQuaCanLamSang.Positive = false;
                    }
                    else
                    {
                        _ketQuaCanLamSang.Normal = false;
                        _ketQuaCanLamSang.Abnormal = false;
                        _ketQuaCanLamSang.Negative = chkNegative.Checked;
                        _ketQuaCanLamSang.Positive = chkPositive.Checked;
                    }

                    Result result = KetQuaCanLamSangBus.InsertKetQuaCanLamSang(_ketQuaCanLamSang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaCanLamSangBus.InsertKetQuaCanLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaCanLamSangBus.InsertKetQuaCanLamSang"));
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
            {
                if (rows[0]["StaffType"] != null && rows[0]["StaffType"] != DBNull.Value)
                    _staffType = (StaffType)Convert.ToByte(rows[0]["StaffType"]);
                else
                    _staffType = StaffType.None;

                DisplayDocStaffList();
            }
        }

        private void dlgAddServiceHistory_Load(object sender, EventArgs e)
        {
            InitData();

            if (!_isNew) DisplayInfo(_drKetQuaCanLamSang);
        }

        private void raNormal_CheckedChanged(object sender, EventArgs e)
        {
            gbNormal.Enabled = raNormal.Checked;
        }

        private void raNegative_CheckedChanged(object sender, EventArgs e)
        {
            gbNegative.Enabled = raNegative.Checked;
        }

        private void chkNormal_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkNormal.Checked && !chkAbnormal.Checked)
            //    chkNormal.Checked = true;

            if (chkNormal.Checked) chkAbnormal.Checked = false;
        }

        private void chkAbnormal_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkAbnormal.Checked && !chkNormal.Checked) chkAbnormal.Checked = true;

            if (chkAbnormal.Checked) chkNormal.Checked = false;
        }

        private void chkNegative_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkNegative.Checked && !chkPositive.Checked)
            //    chkNegative.Checked = true;

            if (chkNegative.Checked) chkPositive.Checked = false;
        }

        private void chkPositive_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkNegative.Checked && !chkPositive.Checked)
            //    chkPositive.Checked = true;

            if (chkPositive.Checked) chkNegative.Checked = false;
        }

        private void btnChonDichVu_Click(object sender, EventArgs e)
        {
            DataTable dtService = cboService.DataSource as DataTable;
            dlgSelectSingleDichVu dlg = new dlgSelectSingleDichVu(dtService);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboService.SelectedValue = dlg.ServiceGUID;
            }
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
        #endregion
    }
}
