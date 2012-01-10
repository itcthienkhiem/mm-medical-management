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
    public partial class dlgAddKhamLamSang : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaLamSang _ketQuaLamSang = new KetQuaLamSang();
        private DataRow _drKetQuaLamSang = null;
        #endregion

        #region Constructor
        public dlgAddKhamLamSang(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKhamLamSang(string patientGUID, DataRow drKetQuaLamSang)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _drKetQuaLamSang = drKetQuaLamSang;
            _isNew = false;
            this.Text = "Sua kham lam san";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgay.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
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

            if (Global.StaffType == StaffType.BacSi)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }
        }

        private void DisplayInfo(DataRow drKetQuaLamSang)
        {
            try
            {
                _ketQuaLamSang.KetQuaLamSangGUID = Guid.Parse(drKetQuaLamSang["KetQuaLamSangGUID"].ToString());
                dtpkNgay.Value = Convert.ToDateTime(drKetQuaLamSang["NgayKham"]);
                cboDocStaff.SelectedValue = drKetQuaLamSang["DocStaffGUID"].ToString();

                CoQuan coQuan = (CoQuan)Convert.ToInt32(drKetQuaLamSang["CoQuan"]);
                bool normal = Convert.ToBoolean(drKetQuaLamSang["Normal"]);
                bool abnormal = Convert.ToBoolean(drKetQuaLamSang["Abnormal"]);
                string nhanXet = drKetQuaLamSang["Note"].ToString();

                switch (coQuan)
                {
                    case CoQuan.Mat:
                        raMat.Checked = true;
                        chkNormal_Mat.Checked = normal;
                        chkAbnormal_Mat.Checked = abnormal;
                        txtNhanXet_Mat.Text = nhanXet;
                        break;
                    case CoQuan.TaiMuiHong:
                        raTaiMuiHong.Checked = true;
                        chkNormal_TaiMuiHong.Checked = normal;
                        chkAbnormal_TaiMuiHong.Checked = abnormal;
                        txtNhanXet_TaiMuiHong.Text = nhanXet;
                        break;
                    case CoQuan.RangHamMat:
                        raRangHamMat.Checked = true;
                        chkNormal_RangHamMat.Checked = normal;
                        chkAbnormal_RangHamMat.Checked = abnormal;
                        txtNhanXet_RangHamMat.Text = nhanXet;
                        break;
                    case CoQuan.HoHap:
                        raHoHap.Checked = true;
                        chkNormal_HoHap.Checked = normal;
                        chkAbnormal_HoHap.Checked = abnormal;
                        txtNhanXet_HoHap.Text = nhanXet;
                        break;
                    case CoQuan.TimMach:
                        raTimMach.Checked = true;
                        chkNormal_TimMach.Checked = normal;
                        chkAbnormal_TimMach.Checked = abnormal;
                        txtNhanXet_TimMach.Text = nhanXet;
                        break;
                    case CoQuan.TieuHoa:
                        raTieuHoa.Checked = true;
                        chkNormal_TieuHoa.Checked = normal;
                        chkAbnormal_TieuHoa.Checked = abnormal;
                        txtNhanXet_TieuHoa.Text = nhanXet;
                        break;
                    case CoQuan.TietNieuSinhDuc:
                        raTietNieuSinhDuc.Checked = true;
                        chkNormal_TietNieuSinhDuc.Checked = normal;
                        chkAbnormal_TietNieuSinhDuc.Checked = abnormal;
                        txtNhanXet_TietNieuSinhDuc.Text = nhanXet;
                        break;
                    case CoQuan.CoXuongKhop:
                        raCoXuongKhop.Checked = true;
                        chkNormal_CoXuongKhop.Checked = normal;
                        chkAbnormal_CoXuongKhop.Checked = abnormal;
                        txtNhanXet_CoXuongKhop.Text = nhanXet;
                        break;
                    case CoQuan.DaLieu:
                        raDaLieu.Checked = true;
                        chkNormal_DaLieu.Checked = normal;
                        chkAbnormal_DaLieu.Checked = abnormal;
                        txtNhanXet_DaLieu.Text = nhanXet;
                        break;
                    case CoQuan.ThanKinh:
                        raThanKinh.Checked = true;
                        chkNormal_ThanKinh.Checked = normal;
                        chkAbnormal_ThanKinh.Checked = abnormal;
                        txtNhanXet_ThanKinh.Text = nhanXet;
                        break;
                    case CoQuan.NoiTiet:
                        raNoiTiet.Checked = true;
                        chkNormal_NoiTiet.Checked = normal;
                        chkAbnormal_NoiTiet.Checked = abnormal;
                        txtNhanXet_NoiTiet.Text = nhanXet;
                        break;
                    case CoQuan.Khac:
                        txtNhanXet_CoQuanKhac.Text = nhanXet;
                        break;
                }

                if (drKetQuaLamSang["CreatedDate"] != null && drKetQuaLamSang["CreatedDate"] != DBNull.Value)
                    _ketQuaLamSang.CreatedDate = Convert.ToDateTime(drKetQuaLamSang["CreatedDate"]);

                if (drKetQuaLamSang["CreatedBy"] != null && drKetQuaLamSang["CreatedBy"] != DBNull.Value)
                    _ketQuaLamSang.CreatedBy = Guid.Parse(drKetQuaLamSang["CreatedBy"].ToString());

                if (drKetQuaLamSang["UpdatedDate"] != null && drKetQuaLamSang["UpdatedDate"] != DBNull.Value)
                    _ketQuaLamSang.UpdatedDate = Convert.ToDateTime(drKetQuaLamSang["UpdatedDate"]);

                if (drKetQuaLamSang["UpdatedBy"] != null && drKetQuaLamSang["UpdatedBy"] != DBNull.Value)
                    _ketQuaLamSang.UpdatedBy = Guid.Parse(drKetQuaLamSang["UpdatedBy"].ToString());

                if (drKetQuaLamSang["DeletedDate"] != null && drKetQuaLamSang["DeletedDate"] != DBNull.Value)
                    _ketQuaLamSang.DeletedDate = Convert.ToDateTime(drKetQuaLamSang["DeletedDate"]);

                if (drKetQuaLamSang["DeletedBy"] != null && drKetQuaLamSang["DeletedBy"] != DBNull.Value)
                    _ketQuaLamSang.DeletedBy = Guid.Parse(drKetQuaLamSang["DeletedBy"].ToString());

                _ketQuaLamSang.Status = Convert.ToByte(drKetQuaLamSang["Status"]);
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

            if (!raMat.Checked && !raTaiMuiHong.Checked && !raRangHamMat.Checked && !raHoHap.Checked && !raTimMach.Checked && !raTieuHoa.Checked && 
                !raTietNieuSinhDuc.Checked && !raCoXuongKhop.Checked && !raDaLieu.Checked && !raThanKinh.Checked && !raNoiTiet.Checked && !raCacCoQuanKhac.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 cơ quan để khám.", IconType.Information);
                raMat.Focus();
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
                    _ketQuaLamSang.CreatedDate = DateTime.Now;
                    _ketQuaLamSang.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaLamSang.UpdatedDate = DateTime.Now;
                    _ketQuaLamSang.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _ketQuaLamSang.PatientGUID = Guid.Parse(_patientGUID);

                MethodInvoker method = delegate
                {
                    _ketQuaLamSang.NgayKham = dtpkNgay.Value;
                    _ketQuaLamSang.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());

                    if (raMat.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.Mat;
                        _ketQuaLamSang.Normal = chkNormal_Mat.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_Mat.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_Mat.Text;
                    }
                    else if (raTaiMuiHong.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.TaiMuiHong;
                        _ketQuaLamSang.Normal = chkNormal_TaiMuiHong.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_TaiMuiHong.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_TaiMuiHong.Text;
                    }
                    else if (raRangHamMat.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.RangHamMat;
                        _ketQuaLamSang.Normal = chkNormal_RangHamMat.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_RangHamMat.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_RangHamMat.Text;
                    }
                    else if (raHoHap.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.HoHap;
                        _ketQuaLamSang.Normal = chkNormal_HoHap.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_HoHap.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_HoHap.Text;
                    }
                    else if (raTimMach.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.TimMach;
                        _ketQuaLamSang.Normal = chkNormal_TimMach.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_TimMach.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_TimMach.Text;
                    }
                    else if (raTieuHoa.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.TieuHoa;
                        _ketQuaLamSang.Normal = chkNormal_TieuHoa.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_TieuHoa.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_TieuHoa.Text;
                    }
                    else if (raTietNieuSinhDuc.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.TietNieuSinhDuc;
                        _ketQuaLamSang.Normal = chkNormal_TietNieuSinhDuc.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_TietNieuSinhDuc.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_TietNieuSinhDuc.Text;
                    }
                    else if (raCoXuongKhop.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.CoXuongKhop;
                        _ketQuaLamSang.Normal = chkNormal_CoXuongKhop.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_CoXuongKhop.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_CoXuongKhop.Text;
                    }
                    else if (raDaLieu.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.DaLieu;
                        _ketQuaLamSang.Normal = chkNormal_DaLieu.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_DaLieu.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_DaLieu.Text;
                    }
                    else if (raThanKinh.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.ThanKinh;
                        _ketQuaLamSang.Normal = chkNormal_ThanKinh.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_ThanKinh.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_ThanKinh.Text;
                    }
                    else if (raNoiTiet.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.NoiTiet;
                        _ketQuaLamSang.Normal = chkNormal_NoiTiet.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_NoiTiet.Checked;
                        _ketQuaLamSang.Note = txtNhanXet_NoiTiet.Text;
                    }
                    else if (raCacCoQuanKhac.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.Khac;
                        _ketQuaLamSang.Normal = false;
                        _ketQuaLamSang.Abnormal = false;
                        _ketQuaLamSang.Note = txtNhanXet_CoQuanKhac.Text;
                    }

                    Result result = KetQuaLamSangBus.InsertKetQuaLamSang(_ketQuaLamSang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaLamSangBus.InsertKetQuaLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.InsertKetQuaLamSang"));
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
        private void dlgAddKhamLamSang_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drKetQuaLamSang);
        }

        private void dlgAddKhamLamSang_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin khám lâm sàng ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
            }
        }

        private void raMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_Mat.Enabled = raMat.Checked;
            chkAbnormal_Mat.Enabled = raMat.Checked;
            txtNhanXet_Mat.ReadOnly = !raMat.Checked;
        }

        private void raTaiMuiHong_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TaiMuiHong.Enabled = raTaiMuiHong.Checked;
            chkAbnormal_TaiMuiHong.Enabled = raTaiMuiHong.Checked;
            txtNhanXet_TaiMuiHong.ReadOnly = !raTaiMuiHong.Checked;
        }

        private void raRangHamMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_RangHamMat.Enabled = raRangHamMat.Checked;
            chkAbnormal_RangHamMat.Enabled = raRangHamMat.Checked;
            txtNhanXet_RangHamMat.ReadOnly = !raRangHamMat.Checked;
        }

        private void raHoHap_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_HoHap.Enabled = raHoHap.Checked;
            chkAbnormal_HoHap.Enabled = raHoHap.Checked;
            txtNhanXet_HoHap.ReadOnly = !raHoHap.Checked;
        }

        private void raTimMach_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TimMach.Enabled = raTimMach.Checked;
            chkAbnormal_TimMach.Enabled = raTimMach.Checked;
            txtNhanXet_TimMach.ReadOnly = !raTimMach.Checked;
        }

        private void raTieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TieuHoa.Enabled = raTieuHoa.Checked;
            chkAbnormal_TieuHoa.Enabled = raTieuHoa.Checked;
            txtNhanXet_TieuHoa.ReadOnly = !raTieuHoa.Checked;
        }

        private void raTietNieuSinhDuc_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TietNieuSinhDuc.Enabled = raTietNieuSinhDuc.Checked;
            chkAbnormal_TietNieuSinhDuc.Enabled = raTietNieuSinhDuc.Checked;
            txtNhanXet_TietNieuSinhDuc.ReadOnly = !raTietNieuSinhDuc.Checked;
        }

        private void raCoXuongKhop_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_CoXuongKhop.Enabled = raCoXuongKhop.Checked;
            chkAbnormal_CoXuongKhop.Enabled = raCoXuongKhop.Checked;
            txtNhanXet_CoXuongKhop.ReadOnly = !raCoXuongKhop.Checked;
        }

        private void raDaLieu_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_DaLieu.Enabled = raDaLieu.Checked;
            chkAbnormal_DaLieu.Enabled = raDaLieu.Checked;
            txtNhanXet_DaLieu.ReadOnly = !raDaLieu.Checked;
        }

        private void raThanKinh_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_ThanKinh.Enabled = raThanKinh.Checked;
            chkAbnormal_ThanKinh.Enabled = raThanKinh.Checked;
            txtNhanXet_ThanKinh.ReadOnly = !raThanKinh.Checked;
        }

        private void raNoiTiet_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_NoiTiet.Enabled = raNoiTiet.Checked;
            chkAbnormal_NoiTiet.Enabled = raNoiTiet.Checked;
            txtNhanXet_NoiTiet.ReadOnly = !raNoiTiet.Checked;
        }

        private void raCacCoQuanKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtNhanXet_CoQuanKhac.ReadOnly = !raCacCoQuanKhac.Checked;
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
