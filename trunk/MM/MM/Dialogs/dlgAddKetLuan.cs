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
    public partial class dlgAddKetLuan : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetLuan _ketLuan = new KetLuan();
        private DataRow _drKetLuan = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddKetLuan(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetLuan(string patientGUID, DataRow drKetLuan, bool allowEdit)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _drKetLuan = drKetLuan;
            _allowEdit = allowEdit;
            _isNew = false;
            this.Text = "Sua can do";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayKetLuan.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
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
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }
        }

        private void DisplayInfo(DataRow drKetLuan)
        {
            try
            {
                _ketLuan.KetLuanGUID = Guid.Parse(drKetLuan["KetLuanGUID"].ToString());
                dtpkNgayKetLuan.Value = Convert.ToDateTime(drKetLuan["NgayKetLuan"]);
                cboDocStaff.SelectedValue = drKetLuan["DocStaffGUID"].ToString();
                chkCo_1.Checked = _ketLuan.HasLamThemXetNghiem;
                chkKhong_1.Checked = !_ketLuan.HasLamThemXetNghiem;
                if (drKetLuan["CacXetNghiemLamThem"] != null && drKetLuan["CacXetNghiemLamThem"] != DBNull.Value)
                    txtCacXetNghiemLamThem.Text = drKetLuan["CacXetNghiemLamThem"] as string;

                chkCo_2.Checked = _ketLuan.HasLamDuCanLamSang;
                chkKhong_2.Checked = !_ketLuan.HasLamDuCanLamSang;

                if (drKetLuan["LyDo_CanLamSang"] != null && drKetLuan["LyDo_CanLamSang"] != DBNull.Value)
                    txtLyDo_1.Text = drKetLuan["LyDo_CanLamSang"] as string;

                chkCo_3.Checked = _ketLuan.HasDuSucKhoe;
                chkKhong_3.Checked = !_ketLuan.HasDuSucKhoe;

                if (drKetLuan["LyDo_SucKhoe"] != null && drKetLuan["LyDo_SucKhoe"] != DBNull.Value)
                    txtLyDo_2.Text = drKetLuan["LyDo_SucKhoe"] as string;

                if (drKetLuan["CreatedDate"] != null && drKetLuan["CreatedDate"] != DBNull.Value)
                    _ketLuan.CreatedDate = Convert.ToDateTime(drKetLuan["CreatedDate"]);

                if (drKetLuan["CreatedBy"] != null && drKetLuan["CreatedBy"] != DBNull.Value)
                    _ketLuan.CreatedBy = Guid.Parse(drKetLuan["CreatedBy"].ToString());

                if (drKetLuan["UpdatedDate"] != null && drKetLuan["UpdatedDate"] != DBNull.Value)
                    _ketLuan.UpdatedDate = Convert.ToDateTime(drKetLuan["UpdatedDate"]);

                if (drKetLuan["UpdatedBy"] != null && drKetLuan["UpdatedBy"] != DBNull.Value)
                    _ketLuan.UpdatedBy = Guid.Parse(drKetLuan["UpdatedBy"].ToString());

                if (drKetLuan["DeletedDate"] != null && drKetLuan["DeletedDate"] != DBNull.Value)
                    _ketLuan.DeletedDate = Convert.ToDateTime(drKetLuan["DeletedDate"]);

                if (drKetLuan["DeletedBy"] != null && drKetLuan["DeletedBy"] != DBNull.Value)
                    _ketLuan.DeletedBy = Guid.Parse(drKetLuan["DeletedBy"].ToString());

                _ketLuan.Status = Convert.ToByte(drKetLuan["Status"]);

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    groupBox1.Enabled = _allowEdit;
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
            if (cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
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
                    _ketLuan.CreatedDate = DateTime.Now;
                    _ketLuan.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketLuan.UpdatedDate = DateTime.Now;
                    _ketLuan.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _ketLuan.PatientGUID = Guid.Parse(_patientGUID);
                
                MethodInvoker method = delegate
                {
                    _ketLuan.NgayKetLuan = dtpkNgayKetLuan.Value;
                    _ketLuan.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    _ketLuan.HasLamThemXetNghiem = chkCo_1.Checked;
                    _ketLuan.CacXetNghiemLamThem = txtCacXetNghiemLamThem.Text;
                    _ketLuan.HasLamDuCanLamSang = chkCo_2.Checked;
                    _ketLuan.LyDo_CanLamSang = txtLyDo_1.Text;
                    _ketLuan.HasDuSucKhoe = chkCo_3.Checked;
                    _ketLuan.LyDo_SucKhoe = txtLyDo_2.Text;

                    Result result = KetLuanBus.InsertKetLuan(_ketLuan);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetLuanBus.InsertKetLuan"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.InsertKetLuan"));
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
        private void dlgAddKetLuan_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drKetLuan);
        }

        private void dlgAddKetLuan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin kết luận ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void chkCo_1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCo_1.Checked && !chkKhong_1.Checked)
                chkCo_1.Checked = true;

            if (chkCo_1.Checked) chkKhong_1.Checked = false;
        }

        private void chkKhong_1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkKhong_1.Checked && !chkCo_1.Checked)
                chkKhong_1.Checked = true;

            if (chkKhong_1.Checked) chkCo_1.Checked = false;
        }

        private void chkCo_2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCo_2.Checked && !chkKhong_2.Checked)
                chkCo_2.Checked = true;

            if (chkCo_2.Checked) chkKhong_2.Checked = false;
        }

        private void chkKhong_2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkKhong_2.Checked && !chkCo_2.Checked)
                chkKhong_2.Checked = true;

            if (chkKhong_2.Checked) chkCo_2.Checked = false;
        }

        private void chkCo_3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCo_3.Checked && !chkKhong_3.Checked)
                chkCo_3.Checked = true;

            if (chkCo_3.Checked) chkKhong_3.Checked = false;
        }

        private void chkKhong_3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkKhong_3.Checked && !chkCo_3.Checked)
                chkKhong_3.Checked = true;

            if (chkKhong_3.Checked) chkCo_3.Checked = false;
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
