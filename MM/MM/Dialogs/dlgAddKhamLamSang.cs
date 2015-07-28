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
        private int _type = 0; //0: Ngoại khoa; 1: Nội khoa; 2: Phụ khoa
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddKhamLamSang(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKhamLamSang(string patientGUID, DataRow drKetQuaLamSang, bool allowEdit)
        {
            InitializeComponent();
            _allowEdit = allowEdit;
            _patientGUID = patientGUID;
            _drKetQuaLamSang = drKetQuaLamSang;
            _isNew = false;
            this.Text = "Sua kham lam san";
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgay.Value = DateTime.Now;

            /*//DocStaff
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
            }*/

            InitNhanXet(CoQuan.TaiMuiHong, txtNhanXet_TaiMuiHong);
            InitNhanXet(CoQuan.RangHamMat, txtNhanXet_RangHamMat);
            InitNhanXet(CoQuan.Mat, txtNhanXet_Mat);
            InitNhanXet(CoQuan.HoHap, txtNhanXet_HoHap);
            InitNhanXet(CoQuan.TimMach, txtNhanXet_TimMach);
            InitNhanXet(CoQuan.TieuHoa, txtNhanXet_TieuHoa);
            InitNhanXet(CoQuan.TietNieuSinhDuc, txtNhanXet_TietNieuSinhDuc);
            InitNhanXet(CoQuan.CoXuongKhop, txtNhanXet_CoXuongKhop);
            InitNhanXet(CoQuan.DaLieu, txtNhanXet_DaLieu);
            InitNhanXet(CoQuan.ThanKinh, txtNhanXet_ThanKinh);
            InitNhanXet(CoQuan.NoiTiet, txtNhanXet_NoiTiet);
            InitNhanXet(CoQuan.Khac, txtNhanXet_CoQuanKhac);
            InitNhanXet(CoQuan.KhamPhuKhoa, txtKetQuaKhamPhuKhoa);
        }

        private void InitNhanXet(CoQuan coQuan, ComboBox cboNhanXet)
        {
            Result result = NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist((int)coQuan);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist"));
                return;
            }
            else
            {
                DataTable dtNhanXet = result.QueryResult as DataTable;
                DataRow newRow = dtNhanXet.NewRow();
                newRow["NhanXetKhamLamSangGUID"] = Guid.Empty;
                newRow["NhanXet"] = string.Empty;
                dtNhanXet.Rows.InsertAt(newRow, 0);

                cboNhanXet.DataSource = dtNhanXet;
            }
        }

        private void DisplayBacSi()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            if (_type == 0)
                staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            else if (_type == 1)
                staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            else
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

            if (_type == 0)
            {
                if (Global.StaffType == StaffType.BacSiNgoaiTongQuat)
                {
                    cboDocStaff.SelectedValue = Global.UserGUID;
                    cboDocStaff.Enabled = false;
                }
                else
                    cboDocStaff.Enabled = true;
            }
            else if (_type == 1)
            {
                if (Global.StaffType == StaffType.BacSiNoiTongQuat)
                {
                    cboDocStaff.SelectedValue = Global.UserGUID;
                    cboDocStaff.Enabled = false;
                }
                else
                    cboDocStaff.Enabled = true;
            }
            else
            {
                if (Global.StaffType == StaffType.BacSiPhuKhoa)
                {
                    cboDocStaff.SelectedValue = Global.UserGUID;
                    cboDocStaff.Enabled = false;
                }
                else
                    cboDocStaff.Enabled = true;
            }
        }

        private void DisplayInfo(DataRow drKetQuaLamSang)
        {
            try
            {
                _ketQuaLamSang.KetQuaLamSangGUID = Guid.Parse(drKetQuaLamSang["KetQuaLamSangGUID"].ToString());
                dtpkNgay.Value = Convert.ToDateTime(drKetQuaLamSang["NgayKham"]);

                CoQuan coQuan = (CoQuan)Convert.ToInt32(drKetQuaLamSang["CoQuan"]);
                bool normal = Convert.ToBoolean(drKetQuaLamSang["Normal"]);
                bool abnormal = Convert.ToBoolean(drKetQuaLamSang["Abnormal"]);
                string nhanXet = drKetQuaLamSang["Note"].ToString();
                string para = string.Empty;
                if (drKetQuaLamSang["PARA"] != null && drKetQuaLamSang["PARA"] != DBNull.Value)
                    para = drKetQuaLamSang["PARA"].ToString();

                string phuKhoaNote = string.Empty;
                if (drKetQuaLamSang["PhuKhoaNote"] != null && drKetQuaLamSang["PhuKhoaNote"] != DBNull.Value)
                    phuKhoaNote = drKetQuaLamSang["PhuKhoaNote"].ToString();

                DateTime ngayKinhChot = DateTime.Now;
                if (drKetQuaLamSang["NgayKinhChot"] != null && drKetQuaLamSang["NgayKinhChot"] != DBNull.Value)
                {
                    chkKinhChot.Checked = true;
                    ngayKinhChot = Convert.ToDateTime(drKetQuaLamSang["NgayKinhChot"]);
                }
                else
                    chkKinhChot.Checked = false;

                string soiTuoiHuyetTrang = string.Empty;
                if (drKetQuaLamSang["SoiTuoiHuyetTrang"] != null && drKetQuaLamSang["SoiTuoiHuyetTrang"] != DBNull.Value)
                    soiTuoiHuyetTrang = drKetQuaLamSang["SoiTuoiHuyetTrang"].ToString();

                switch (coQuan)
                {
                    case CoQuan.Mat:
                        raMat.Checked = true;
                        chkNormal_Mat.Checked = normal;
                        chkAbnormal_Mat.Checked = abnormal;
                        txtNhanXet_Mat.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.TaiMuiHong:
                        raTaiMuiHong.Checked = true;
                        chkNormal_TaiMuiHong.Checked = normal;
                        chkAbnormal_TaiMuiHong.Checked = abnormal;
                        txtNhanXet_TaiMuiHong.Text = nhanXet;
                        //_isNgoaiKhoa = true;
                        _type = 0;
                        break;
                    case CoQuan.RangHamMat:
                        raRangHamMat.Checked = true;
                        chkNormal_RangHamMat.Checked = normal;
                        chkAbnormal_RangHamMat.Checked = abnormal;
                        txtNhanXet_RangHamMat.Text = nhanXet;
                        //_isNgoaiKhoa = true;
                        _type = 0;
                        break;
                    case CoQuan.HoHap:
                        raHoHap.Checked = true;
                        chkNormal_HoHap.Checked = normal;
                        chkAbnormal_HoHap.Checked = abnormal;
                        txtNhanXet_HoHap.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.TimMach:
                        raTimMach.Checked = true;
                        chkNormal_TimMach.Checked = normal;
                        chkAbnormal_TimMach.Checked = abnormal;
                        txtNhanXet_TimMach.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.TieuHoa:
                        raTieuHoa.Checked = true;
                        chkNormal_TieuHoa.Checked = normal;
                        chkAbnormal_TieuHoa.Checked = abnormal;
                        txtNhanXet_TieuHoa.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.TietNieuSinhDuc:
                        raTietNieuSinhDuc.Checked = true;
                        chkNormal_TietNieuSinhDuc.Checked = normal;
                        chkAbnormal_TietNieuSinhDuc.Checked = abnormal;
                        txtNhanXet_TietNieuSinhDuc.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.CoXuongKhop:
                        raCoXuongKhop.Checked = true;
                        chkNormal_CoXuongKhop.Checked = normal;
                        chkAbnormal_CoXuongKhop.Checked = abnormal;
                        txtNhanXet_CoXuongKhop.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.DaLieu:
                        raDaLieu.Checked = true;
                        chkNormal_DaLieu.Checked = normal;
                        chkAbnormal_DaLieu.Checked = abnormal;
                        txtNhanXet_DaLieu.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.ThanKinh:
                        raThanKinh.Checked = true;
                        chkNormal_ThanKinh.Checked = normal;
                        chkAbnormal_ThanKinh.Checked = abnormal;
                        txtNhanXet_ThanKinh.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.NoiTiet:
                        raNoiTiet.Checked = true;
                        chkNormal_NoiTiet.Checked = normal;
                        chkAbnormal_NoiTiet.Checked = abnormal;
                        txtNhanXet_NoiTiet.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.Khac:
                        raCacCoQuanKhac.Checked = true;
                        txtNhanXet_CoQuanKhac.Text = nhanXet;
                        //_isNgoaiKhoa = false;
                        _type = 1;
                        break;
                    case CoQuan.KhamPhuKhoa:
                        raKhamPhuKhoa.Checked = true;
                        txtPARA.Text = para;
                        dtpkNgayKinhChot.Value = ngayKinhChot;
                        txtKetQuaKhamPhuKhoa.Text = nhanXet;
                        txtPhuKhoaNote.Text = phuKhoaNote;
                        txtSoiTuoiHuyetTrang.Text = soiTuoiHuyetTrang;
                        chkNormal_KhamPhuKhoa.Checked = normal;
                        chkAbnormal_KhamPhuKhoa.Checked = abnormal;
                        //_isNgoaiKhoa = false;
                        _type = 2;
                        break;
                }

                DisplayBacSi();

                cboDocStaff.SelectedValue = drKetQuaLamSang["DocStaffGUID"].ToString();

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
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            if (!raMat.Checked && !raTaiMuiHong.Checked && !raRangHamMat.Checked && !raHoHap.Checked && !raTimMach.Checked && !raTieuHoa.Checked && 
                !raTietNieuSinhDuc.Checked && !raCoXuongKhop.Checked && !raDaLieu.Checked && !raThanKinh.Checked && !raNoiTiet.Checked &&
                !raCacCoQuanKhac.Checked && !raKhamPhuKhoa.Checked)
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

        private bool InsertNhanXetKhamLamSang(string nhanXet, int loai)
        {
            if (nhanXet.Trim() == string.Empty) return true;
            Result result = NhanXetKhamLamSangBus.CheckNhanXetExist(null, loai, nhanXet);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    NhanXetKhamLamSang nhanXetKhamLamSang = new NhanXetKhamLamSang();
                    nhanXetKhamLamSang.Status = (byte)Status.Actived;
                    nhanXetKhamLamSang.CreatedDate = DateTime.Now;
                    nhanXetKhamLamSang.CreatedBy = Guid.Parse(Global.UserGUID);
                    nhanXetKhamLamSang.NhanXet = nhanXet;
                    nhanXetKhamLamSang.Loai = loai;
                    result = NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang(nhanXetKhamLamSang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang"));
                        return false;
                    }
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhanXetKhamLamSangBus.CheckNhanXetExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.CheckNhanXetExist"));
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
                    else if (raKhamPhuKhoa.Checked)
                    {
                        _ketQuaLamSang.CoQuan = (byte)CoQuan.KhamPhuKhoa;
                        _ketQuaLamSang.PARA = txtPARA.Text;
                        if (chkKinhChot.Checked)
                            _ketQuaLamSang.NgayKinhChot = dtpkNgayKinhChot.Value;
                        else
                            _ketQuaLamSang.NgayKinhChot = null;

                        _ketQuaLamSang.Note = txtKetQuaKhamPhuKhoa.Text;
                        _ketQuaLamSang.PhuKhoaNote = txtPhuKhoaNote.Text;
                        _ketQuaLamSang.Normal = chkNormal_KhamPhuKhoa.Checked;
                        _ketQuaLamSang.Abnormal = chkAbnormal_KhamPhuKhoa.Checked;
                    }

                    Result result = KetQuaLamSangBus.InsertKetQuaLamSang(_ketQuaLamSang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaLamSangBus.InsertKetQuaLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.InsertKetQuaLamSang"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    else
                    {
                        InsertNhanXetKhamLamSang(txtNhanXet_TaiMuiHong.Text, (int)CoQuan.TaiMuiHong);
                        InsertNhanXetKhamLamSang(txtNhanXet_RangHamMat.Text, (int)CoQuan.RangHamMat);
                        InsertNhanXetKhamLamSang(txtNhanXet_Mat.Text, (int)CoQuan.Mat);
                        InsertNhanXetKhamLamSang(txtNhanXet_HoHap.Text, (int)CoQuan.HoHap);
                        InsertNhanXetKhamLamSang(txtNhanXet_TimMach.Text, (int)CoQuan.TimMach);
                        InsertNhanXetKhamLamSang(txtNhanXet_TieuHoa.Text, (int)CoQuan.TieuHoa);
                        InsertNhanXetKhamLamSang(txtNhanXet_TietNieuSinhDuc.Text, (int)CoQuan.TietNieuSinhDuc);
                        InsertNhanXetKhamLamSang(txtNhanXet_CoXuongKhop.Text, (int)CoQuan.CoXuongKhop);
                        InsertNhanXetKhamLamSang(txtNhanXet_DaLieu.Text, (int)CoQuan.DaLieu);
                        InsertNhanXetKhamLamSang(txtNhanXet_ThanKinh.Text, (int)CoQuan.ThanKinh);
                        InsertNhanXetKhamLamSang(txtNhanXet_NoiTiet.Text, (int)CoQuan.NoiTiet);
                        InsertNhanXetKhamLamSang(txtNhanXet_CoQuanKhac.Text, (int)CoQuan.Khac);
                        InsertNhanXetKhamLamSang(txtKetQuaKhamPhuKhoa.Text, (int)CoQuan.KhamPhuKhoa);
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
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin khám lâm sàng ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void raMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_Mat.Enabled = raMat.Checked;
            chkAbnormal_Mat.Enabled = raMat.Checked;
            txtNhanXet_Mat.Enabled = raMat.Checked;

            if (raMat.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raMat.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raTaiMuiHong_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TaiMuiHong.Enabled = raTaiMuiHong.Checked;
            chkAbnormal_TaiMuiHong.Enabled = raTaiMuiHong.Checked;
            txtNhanXet_TaiMuiHong.Enabled = raTaiMuiHong.Checked;

            if (raTaiMuiHong.Checked && _type != 0)
            {
                _type = 0;
                DisplayBacSi();
            }

            if (raTaiMuiHong.Checked)
            {
                raMat.Checked = false;
                raHoHap.Checked = false;
                raTimMach.Checked = false;
                raTieuHoa.Checked = false;
                raTietNieuSinhDuc.Checked = false;
                raCoXuongKhop.Checked = false;
                raDaLieu.Checked = false;
                raThanKinh.Checked = false;
                raNoiTiet.Checked = false;
                raCacCoQuanKhac.Checked = false;
                raKhamPhuKhoa.Checked = false;
            }
        }

        private void raRangHamMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_RangHamMat.Enabled = raRangHamMat.Checked;
            chkAbnormal_RangHamMat.Enabled = raRangHamMat.Checked;
            txtNhanXet_RangHamMat.Enabled = raRangHamMat.Checked;

            if (raRangHamMat.Checked && _type != 0)
            {
                _type = 0;
                DisplayBacSi();
            }

            if (raRangHamMat.Checked)
            {
                raMat.Checked = false;
                raHoHap.Checked = false;
                raTimMach.Checked = false;
                raTieuHoa.Checked = false;
                raTietNieuSinhDuc.Checked = false;
                raCoXuongKhop.Checked = false;
                raDaLieu.Checked = false;
                raThanKinh.Checked = false;
                raNoiTiet.Checked = false;
                raCacCoQuanKhac.Checked = false;
                raKhamPhuKhoa.Checked = false;
            }
        }

        private void raHoHap_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_HoHap.Enabled = raHoHap.Checked;
            chkAbnormal_HoHap.Enabled = raHoHap.Checked;
            txtNhanXet_HoHap.Enabled = raHoHap.Checked;

            if (raHoHap.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raHoHap.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raTimMach_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TimMach.Enabled = raTimMach.Checked;
            chkAbnormal_TimMach.Enabled = raTimMach.Checked;
            txtNhanXet_TimMach.Enabled = raTimMach.Checked;

            if (raTimMach.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raTimMach.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raTieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TieuHoa.Enabled = raTieuHoa.Checked;
            chkAbnormal_TieuHoa.Enabled = raTieuHoa.Checked;
            txtNhanXet_TieuHoa.Enabled = raTieuHoa.Checked;

            if (raTieuHoa.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raTieuHoa.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raTietNieuSinhDuc_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TietNieuSinhDuc.Enabled = raTietNieuSinhDuc.Checked;
            chkAbnormal_TietNieuSinhDuc.Enabled = raTietNieuSinhDuc.Checked;
            txtNhanXet_TietNieuSinhDuc.Enabled = raTietNieuSinhDuc.Checked;

            if (raTietNieuSinhDuc.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raTietNieuSinhDuc.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raCoXuongKhop_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_CoXuongKhop.Enabled = raCoXuongKhop.Checked;
            chkAbnormal_CoXuongKhop.Enabled = raCoXuongKhop.Checked;
            txtNhanXet_CoXuongKhop.Enabled = raCoXuongKhop.Checked;

            if (raCoXuongKhop.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raCoXuongKhop.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raDaLieu_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_DaLieu.Enabled = raDaLieu.Checked;
            chkAbnormal_DaLieu.Enabled = raDaLieu.Checked;
            txtNhanXet_DaLieu.Enabled = raDaLieu.Checked;

            if (raDaLieu.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raDaLieu.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raThanKinh_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_ThanKinh.Enabled = raThanKinh.Checked;
            chkAbnormal_ThanKinh.Enabled = raThanKinh.Checked;
            txtNhanXet_ThanKinh.Enabled = raThanKinh.Checked;

            if (raThanKinh.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raThanKinh.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raNoiTiet_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_NoiTiet.Enabled = raNoiTiet.Checked;
            chkAbnormal_NoiTiet.Enabled = raNoiTiet.Checked;
            txtNhanXet_NoiTiet.Enabled = raNoiTiet.Checked;

            if (raNoiTiet.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raNoiTiet.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raCacCoQuanKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtNhanXet_CoQuanKhac.Enabled = raCacCoQuanKhac.Checked;

            if (raCacCoQuanKhac.Checked && _type != 1)
            {
                _type = 1;
                DisplayBacSi();
            }

            if (raCacCoQuanKhac.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void raKhamPhuKhoa_CheckedChanged(object sender, EventArgs e)
        {
            txtPARA.ReadOnly = !raKhamPhuKhoa.Checked;
            chkKinhChot.Enabled = raKhamPhuKhoa.Checked;
            dtpkNgayKinhChot.Enabled = raKhamPhuKhoa.Checked && chkKinhChot.Checked;
            txtKetQuaKhamPhuKhoa.Enabled = raKhamPhuKhoa.Checked;
            txtPhuKhoaNote.ReadOnly = !raKhamPhuKhoa.Checked;
            txtSoiTuoiHuyetTrang.ReadOnly = !raKhamPhuKhoa.Checked;
            chkNormal_KhamPhuKhoa.Enabled = raKhamPhuKhoa.Checked;
            chkAbnormal_KhamPhuKhoa.Enabled = raKhamPhuKhoa.Checked;

            if (raKhamPhuKhoa.Checked && _type != 2)
            {
                _type = 2;
                DisplayBacSi();
            }

            if (raKhamPhuKhoa.Checked)
            {
                raTaiMuiHong.Checked = false;
                raRangHamMat.Checked = false;
            }
        }

        private void chkNormal_TaiMuiHong_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_TaiMuiHong.Checked && !chkAbnormal_TaiMuiHong.Checked)
                chkNormal_TaiMuiHong.Checked = true;

            if (chkNormal_TaiMuiHong.Checked) chkAbnormal_TaiMuiHong.Checked = false;
        }

        private void chkAbnormal_TaiMuiHong_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_TaiMuiHong.Checked && !chkNormal_TaiMuiHong.Checked)
                chkAbnormal_TaiMuiHong.Checked = true;

            if (chkAbnormal_TaiMuiHong.Checked) chkNormal_TaiMuiHong.Checked = false;
        }

        private void chkNormal_RangHamMat_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_RangHamMat.Checked && !chkAbnormal_RangHamMat.Checked)
                chkNormal_RangHamMat.Checked = true;

            if (chkNormal_RangHamMat.Checked) chkAbnormal_RangHamMat.Checked = false;
        }

        private void chkAbnormal_RangHamMat_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_RangHamMat.Checked && !chkNormal_RangHamMat.Checked)
                chkAbnormal_RangHamMat.Checked = true;

            if (chkAbnormal_RangHamMat.Checked) chkNormal_RangHamMat.Checked = false;
        }

        private void chkNormal_Mat_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_Mat.Checked && !chkAbnormal_Mat.Checked)
                chkNormal_Mat.Checked = true;

            if (chkNormal_Mat.Checked) chkAbnormal_Mat.Checked = false;
        }

        private void chkAbnormal_Mat_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_Mat.Checked && !chkNormal_Mat.Checked)
                chkAbnormal_Mat.Checked = true;

            if (chkAbnormal_Mat.Checked) chkNormal_Mat.Checked = false;
        }

        private void chkNormal_HoHap_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_HoHap.Checked && !chkAbnormal_HoHap.Checked)
                chkNormal_HoHap.Checked = true;

            if (chkNormal_HoHap.Checked) chkAbnormal_HoHap.Checked = false;
        }

        private void chkAbnormal_HoHap_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_HoHap.Checked && !chkNormal_HoHap.Checked)
                chkAbnormal_HoHap.Checked = true;

            if (chkAbnormal_HoHap.Checked) chkNormal_HoHap.Checked = false;
        }

        private void chkNormal_TimMach_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_TimMach.Checked && !chkAbnormal_TimMach.Checked)
                chkNormal_TimMach.Checked = true;

            if (chkNormal_TimMach.Checked) chkAbnormal_TimMach.Checked = false;
        }

        private void chkAbnormal_TimMach_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_TimMach.Checked && !chkNormal_TimMach.Checked)
                chkAbnormal_TimMach.Checked = true;

            if (chkAbnormal_TimMach.Checked) chkNormal_TimMach.Checked = false;
        }

        private void chkNormal_TieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_TieuHoa.Checked && !chkAbnormal_TieuHoa.Checked)
                chkNormal_TieuHoa.Checked = true;

            if (chkNormal_TieuHoa.Checked) chkAbnormal_TieuHoa.Checked = false;
        }

        private void chkAbnormal_TieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_TieuHoa.Checked && !chkNormal_TieuHoa.Checked)
                chkAbnormal_TieuHoa.Checked = true;

            if (chkAbnormal_TieuHoa.Checked) chkNormal_TieuHoa.Checked = false;
        }

        private void chkNormal_TietNieuSinhDuc_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_TietNieuSinhDuc.Checked && !chkAbnormal_TietNieuSinhDuc.Checked)
                chkNormal_TietNieuSinhDuc.Checked = true;

            if (chkNormal_TietNieuSinhDuc.Checked) chkAbnormal_TietNieuSinhDuc.Checked = false;
        }

        private void chkAbnormal_TietNieuSinhDuc_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_TietNieuSinhDuc.Checked && !chkNormal_TietNieuSinhDuc.Checked)
                chkAbnormal_TietNieuSinhDuc.Checked = true;

            if (chkAbnormal_TietNieuSinhDuc.Checked) chkNormal_TietNieuSinhDuc.Checked = false;
        }

        private void chkNormal_CoXuongKhop_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_CoXuongKhop.Checked && !chkAbnormal_CoXuongKhop.Checked)
                chkNormal_CoXuongKhop.Checked = true;

            if (chkNormal_CoXuongKhop.Checked) chkAbnormal_CoXuongKhop.Checked = false;
        }

        private void chkAbnormal_CoXuongKhop_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_CoXuongKhop.Checked && !chkNormal_CoXuongKhop.Checked)
                chkAbnormal_CoXuongKhop.Checked = true;

            if (chkAbnormal_CoXuongKhop.Checked) chkNormal_CoXuongKhop.Checked = false;
        }

        private void chkNormal_DaLieu_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_DaLieu.Checked && !chkAbnormal_DaLieu.Checked)
                chkNormal_DaLieu.Checked = true;

            if (chkNormal_DaLieu.Checked) chkAbnormal_DaLieu.Checked = false;
        }

        private void chkAbnormal_DaLieu_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_DaLieu.Checked && !chkNormal_DaLieu.Checked)
                chkAbnormal_DaLieu.Checked = true;

            if (chkAbnormal_DaLieu.Checked) chkNormal_DaLieu.Checked = false;
        }

        private void chkNormal_ThanKinh_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_ThanKinh.Checked && !chkAbnormal_ThanKinh.Checked)
                chkNormal_ThanKinh.Checked = true;

            if (chkNormal_ThanKinh.Checked) chkAbnormal_ThanKinh.Checked = false;
        }

        private void chkAbnormal_ThanKinh_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_ThanKinh.Checked && !chkNormal_ThanKinh.Checked)
                chkAbnormal_ThanKinh.Checked = true;

            if (chkAbnormal_ThanKinh.Checked) chkNormal_ThanKinh.Checked = false;
        }

        private void chkNormal_NoiTiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal_NoiTiet.Checked && !chkAbnormal_NoiTiet.Checked)
                chkNormal_NoiTiet.Checked = true;

            if (chkNormal_NoiTiet.Checked) chkAbnormal_NoiTiet.Checked = false;
        }

        private void chkAbnormal_NoiTiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_NoiTiet.Checked && !chkNormal_NoiTiet.Checked)
                chkAbnormal_NoiTiet.Checked = true;

            if (chkAbnormal_NoiTiet.Checked) chkNormal_NoiTiet.Checked = false;
        }

        private void chkNormal_KhamPhuKhoa_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkNormal_KhamPhuKhoa.Checked && !chkAbnormal_KhamPhuKhoa.Checked)
            //    chkNormal_KhamPhuKhoa.Checked = true;

            //if (chkNormal_KhamPhuKhoa.Checked) chkAbnormal_KhamPhuKhoa.Checked = false;
        }

        private void chkAbnormal_KhamPhuKhoa_CheckedChanged(object sender, EventArgs e)
        {
            //if (!chkAbnormal_KhamPhuKhoa.Checked && !chkNormal_KhamPhuKhoa.Checked)
            //    chkAbnormal_KhamPhuKhoa.Checked = true;

            //if (chkAbnormal_KhamPhuKhoa.Checked) chkNormal_KhamPhuKhoa.Checked = false;
        }

        private void chkKinhChot_CheckedChanged(object sender, EventArgs e)
        {
            dtpkNgayKinhChot.Enabled = chkKinhChot.Checked && raKhamPhuKhoa.Checked;
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
