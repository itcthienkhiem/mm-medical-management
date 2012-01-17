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
    public partial class dlgAddLoiKhuyen : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private LoiKhuyen _loiKhuyen = new LoiKhuyen();
        private DataRow _drLoiKhuyen = null;
        #endregion

        #region Constructor
        public dlgAddLoiKhuyen(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddLoiKhuyen(string patientGUID, DataRow drLoiKhuyen)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _isNew = false;
            this.Text = "Sua loi khuyen";
            _drLoiKhuyen = drLoiKhuyen;
        }
        #endregion

        #region Properties
        public LoiKhuyen LoiKhuyen
        {
            get { return _loiKhuyen; }
            set { _loiKhuyen = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgay.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }

            //Symptom
            result = SymptomBus.GetSymptomList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SymptomBus.GetSymptomList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.GetSymptomList"));
                return;
            }
            else
            {
                cboTrieuChung.DataSource = result.QueryResult;
            }
        }

        private void DisplayInfo(DataRow drLoiKhuyen)
        {
            try
            {
                _loiKhuyen.LoiKhuyenGUID = Guid.Parse(drLoiKhuyen["LoiKhuyenGUID"].ToString());
                dtpkNgay.Value = Convert.ToDateTime(drLoiKhuyen["Ngay"]);
                cboDocStaff.SelectedValue = drLoiKhuyen["DocStaffGUID"].ToString();
                cboTrieuChung.SelectedValue = drLoiKhuyen["SymptomGUID"].ToString();

                if (drLoiKhuyen["CreatedDate"] != null && drLoiKhuyen["CreatedDate"] != DBNull.Value)
                    _loiKhuyen.CreatedDate = Convert.ToDateTime(drLoiKhuyen["CreatedDate"]);

                if (drLoiKhuyen["CreatedBy"] != null && drLoiKhuyen["CreatedBy"] != DBNull.Value)
                    _loiKhuyen.CreatedBy = Guid.Parse(drLoiKhuyen["CreatedBy"].ToString());

                if (drLoiKhuyen["UpdatedDate"] != null && drLoiKhuyen["UpdatedDate"] != DBNull.Value)
                    _loiKhuyen.UpdatedDate = Convert.ToDateTime(drLoiKhuyen["UpdatedDate"]);

                if (drLoiKhuyen["UpdatedBy"] != null && drLoiKhuyen["UpdatedBy"] != DBNull.Value)
                    _loiKhuyen.UpdatedBy = Guid.Parse(drLoiKhuyen["UpdatedBy"].ToString());

                if (drLoiKhuyen["DeletedDate"] != null && drLoiKhuyen["DeletedDate"] != DBNull.Value)
                    _loiKhuyen.DeletedDate = Convert.ToDateTime(drLoiKhuyen["DeletedDate"]);

                if (drLoiKhuyen["DeletedBy"] != null && drLoiKhuyen["DeletedBy"] != DBNull.Value)
                    _loiKhuyen.DeletedBy = Guid.Parse(drLoiKhuyen["DeletedBy"].ToString());

                _loiKhuyen.Status = Convert.ToByte(drLoiKhuyen["LoiKhuyenStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            if (cboTrieuChung.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn triệu chứng.", IconType.Information);
                cboTrieuChung.Focus();
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
                if (_isNew)
                {
                    _loiKhuyen.CreatedDate = DateTime.Now;
                    _loiKhuyen.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _loiKhuyen.UpdatedDate = DateTime.Now;
                    _loiKhuyen.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _loiKhuyen.PatientGUID = Guid.Parse(_patientGUID);

                MethodInvoker method = delegate
                {
                    _loiKhuyen.Ngay = dtpkNgay.Value;
                    _loiKhuyen.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    _loiKhuyen.SymptomGUID = Guid.Parse(cboTrieuChung.SelectedValue.ToString());

                    Result result = LoiKhuyenBus.InsertLoiKhuyen(_loiKhuyen);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("LoiKhuyenBus.InsertLoiKhuyen"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.InsertLoiKhuyen"));
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
        private void dlgAddLoiKhuyen_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drLoiKhuyen);
        }

        private void dlgAddLoiKhuyen_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin lời khuyên ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void cboTrieuChung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTrieuChung.Text.Trim() == string.Empty) 
            {
                txtLoiKhuyen.Text = string.Empty;
                return;
            }

            string symptomGUID = cboTrieuChung.SelectedValue.ToString();
            DataTable dt = cboTrieuChung.DataSource as DataTable;
            DataRow[] rows = dt.Select(string.Format("SymptomGUID='{0}'", symptomGUID));
            if (rows == null || rows.Length <= 0)
                txtLoiKhuyen.Text = string.Empty;
            else
                txtLoiKhuyen.Text = rows[0]["Advice"].ToString();
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
