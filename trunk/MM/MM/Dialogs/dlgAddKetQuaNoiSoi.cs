using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddKetQuaNoiSoi : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaNoiSoi _ketQuaNoiSoi = new KetQuaNoiSoi();
        private DataRow _drKetQuaNoiSoi = null;
        #endregion

        #region Constructor
        public dlgAddKetQuaNoiSoi(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetQuaNoiSoi(string patientGUID, DataRow drKetQuaNoiSoi)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _drKetQuaNoiSoi = drKetQuaNoiSoi;
            _isNew = false;
            this.Text = "Sua kham noi soi";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayKham.Value = DateTime.Now;
            cboLoaiNoiSoi.SelectedIndex = 0;

            DisplayDSBacSiChiDinh();
            DisplayDSBasSiSoi();
        }

        private void ViewControl(Control view)
        {
            view.Visible = true;

            foreach (Control ctrl in panel5.Controls)
            {
                if (ctrl != view) ctrl.Visible = false;
            }
        }

        private void DisplayDSBacSiChiDinh()
        {
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
                cboBSCD.DataSource = result.QueryResult;
        }

        private void DisplayDSBasSiSoi()
        {
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
                cboBSSoi.DataSource = result.QueryResult;

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboBSSoi.SelectedValue = Global.UserGUID;
                cboBSSoi.Enabled = false;
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KetQuaNoiSoiBus.GetKetQuaNoiSoiCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                txtSoPhieu.Text = Utility.GetCode(strDate, count + 1, 4);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiCount"));
            }
        }

        private bool CheckInfo()
        {
            if (txtSoPhieu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng số phiếu.", IconType.Information);
                txtSoPhieu.Focus();
                return false;
            }

            if (cboBSCD.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bác sĩ chỉ định.", IconType.Information);
                cboBSCD.Focus();
                return false;
            }

            if (cboBSSoi.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bác sĩ soi.", IconType.Information);
                cboBSSoi.Focus();
                return false;
            }

            if (picHinh1.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 1.", IconType.Information);
                return false;
            }

            if (picHinh2.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 2.", IconType.Information);
                return false;
            }

            if (picHinh3.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 3.", IconType.Information);
                return false;
            }

            if (picHinh4.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 4.", IconType.Information);
                return false;
            }

            string ketQuaNoiSoiGUID = _isNew ? string.Empty : _ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString();
            Result result = KetQuaNoiSoiBus.CheckSoPhieuExistCode(ketQuaNoiSoiGUID, txtSoPhieu.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Số phiếu này đã tồn tại rồi. Vui lòng nhập số phiếu khác.", IconType.Information);
                    txtSoPhieu.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaNoiSoiBus.CheckSoPhieuExistCode"), IconType.Error);
                return false;
            }

            return true;
        }

        private void DisplayInfo(DataRow drKetQuaNoiSoi)
        {
            try
            {
                txtSoPhieu.Text = drKetQuaNoiSoi["SoPhieu"].ToString();
                dtpkNgayKham.Value = Convert.ToDateTime(drKetQuaNoiSoi["NgayKham"]);
                cboBSCD.SelectedValue = drKetQuaNoiSoi["BacSiChiDinh"].ToString();
                cboBSSoi.SelectedValue = drKetQuaNoiSoi["BacSiSoi"].ToString();
                txtLyDoKham.Text = drKetQuaNoiSoi["LyDoKham"].ToString();
                cboLoaiNoiSoi.SelectedIndex = Convert.ToByte(drKetQuaNoiSoi["LoaiNoiSoi"]);
                cboKetLuan.Text = drKetQuaNoiSoi["KetLuan"].ToString();
                cboDeNghi.Text = drKetQuaNoiSoi["DeNghi"].ToString();

                picHinh1.Image = ParseImage((byte[])drKetQuaNoiSoi["Hinh1"]);
                picHinh2.Image = ParseImage((byte[])drKetQuaNoiSoi["Hinh2"]);
                picHinh3.Image = ParseImage((byte[])drKetQuaNoiSoi["Hinh3"]);
                picHinh4.Image = ParseImage((byte[])drKetQuaNoiSoi["Hinh4"]);

                LoaiNoiSoi type = (LoaiNoiSoi)cboLoaiNoiSoi.SelectedIndex;
                switch (type)
                {
                    case LoaiNoiSoi.Tai:
                        _uKetQuaNoiSoiTai.OngTaiTrai = drKetQuaNoiSoi["OngTaiTrai"].ToString();
                        _uKetQuaNoiSoiTai.OngTaiPhai = drKetQuaNoiSoi["OngTaiPhai"].ToString();
                        _uKetQuaNoiSoiTai.MangNhiTrai = drKetQuaNoiSoi["MangNhiTrai"].ToString();
                        _uKetQuaNoiSoiTai.MangNhiPhai = drKetQuaNoiSoi["MangNhiPhai"].ToString();
                        _uKetQuaNoiSoiTai.CanBuaTrai = drKetQuaNoiSoi["CanBuaTrai"].ToString();
                        _uKetQuaNoiSoiTai.CanBuaPhai = drKetQuaNoiSoi["CanBuaPhai"].ToString();
                        _uKetQuaNoiSoiTai.HomNhiTrai = drKetQuaNoiSoi["HomNhiTrai"].ToString();
                        _uKetQuaNoiSoiTai.HomNhiPhai = drKetQuaNoiSoi["HomNhiPhai"].ToString();
                        _uKetQuaNoiSoiTai.ValsavaTrai = drKetQuaNoiSoi["ValsavaTrai"].ToString();
                        _uKetQuaNoiSoiTai.ValsavaPhai = drKetQuaNoiSoi["ValsavaPhai"].ToString();
                        break;
                    case LoaiNoiSoi.Mui:
                        _uKetQuaNoiSoiMui.NiemMacTrai = drKetQuaNoiSoi["NiemMacTrai"].ToString();
                        _uKetQuaNoiSoiMui.NiemMacPhai = drKetQuaNoiSoi["NiemMacPhai"].ToString();
                        _uKetQuaNoiSoiMui.VachNganTrai = drKetQuaNoiSoi["VachNganTrai"].ToString();
                        _uKetQuaNoiSoiMui.VachNganPhai = drKetQuaNoiSoi["VachNganPhai"].ToString();
                        _uKetQuaNoiSoiMui.KheTrenTrai = drKetQuaNoiSoi["KheTrenTrai"].ToString();
                        _uKetQuaNoiSoiMui.KheTrenPhai = drKetQuaNoiSoi["KheTrenPhai"].ToString();
                        _uKetQuaNoiSoiMui.KheGiuaTrai = drKetQuaNoiSoi["KheGiuaTrai"].ToString();
                        _uKetQuaNoiSoiMui.KheGiuaPhai = drKetQuaNoiSoi["KheGiuaPhai"].ToString();
                        _uKetQuaNoiSoiMui.CuonGiuaTrai = drKetQuaNoiSoi["CuonGiuaTrai"].ToString();
                        _uKetQuaNoiSoiMui.CuonGiuaPhai = drKetQuaNoiSoi["CuonGiuaPhai"].ToString();
                        _uKetQuaNoiSoiMui.CuonDuoiTrai = drKetQuaNoiSoi["CuonDuoiTrai"].ToString();
                        _uKetQuaNoiSoiMui.CuonDuoiPhai = drKetQuaNoiSoi["CuonDuoiPhai"].ToString();
                        _uKetQuaNoiSoiMui.MomMocTrai = drKetQuaNoiSoi["MomMocTrai"].ToString();
                        _uKetQuaNoiSoiMui.MomMocPhai = drKetQuaNoiSoi["MomMocPhai"].ToString();
                        _uKetQuaNoiSoiMui.BongSangTrai = drKetQuaNoiSoi["BongSangTrai"].ToString();
                        _uKetQuaNoiSoiMui.BongSangPhai = drKetQuaNoiSoi["BongSangPhai"].ToString();
                        _uKetQuaNoiSoiMui.VomTrai = drKetQuaNoiSoi["VomTrai"].ToString();
                        _uKetQuaNoiSoiMui.VomPhai = drKetQuaNoiSoi["VomPhai"].ToString();
                        break;
                    case LoaiNoiSoi.Hong_ThanhQuan:
                        _uKetQuaNoiSoiHongThanhQuan.Amydale = drKetQuaNoiSoi["Amydale"].ToString();
                        _uKetQuaNoiSoiHongThanhQuan.XoangLe = drKetQuaNoiSoi["XoangLe"].ToString();
                        _uKetQuaNoiSoiHongThanhQuan.MiengThucQuan = drKetQuaNoiSoi["MiengThucQuan"].ToString();
                        _uKetQuaNoiSoiHongThanhQuan.SunPheu = drKetQuaNoiSoi["SunPheu"].ToString();
                        _uKetQuaNoiSoiHongThanhQuan.DayThanh = drKetQuaNoiSoi["DayThanh"].ToString();
                        _uKetQuaNoiSoiHongThanhQuan.BangThanhThat = drKetQuaNoiSoi["BangThanhThat"].ToString();
                        break;
                    case LoaiNoiSoi.TaiMuiHong:
                        _uKetQuaNoiSoiTaiMuiHong.OngTaiNgoai = drKetQuaNoiSoi["OngTaiNgoai"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.MangNhi = drKetQuaNoiSoi["MangNhi"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.NiemMacMui = drKetQuaNoiSoi["NiemMac"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.VachNgan = drKetQuaNoiSoi["VachNgan"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.KheTren = drKetQuaNoiSoi["KheTren"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.KheGiua = drKetQuaNoiSoi["KheGiua"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.MomMocBongSang = drKetQuaNoiSoi["MomMoc_BongSang"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.Vom = drKetQuaNoiSoi["Vom"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.Amydale = drKetQuaNoiSoi["Amydale"].ToString();
                        _uKetQuaNoiSoiTaiMuiHong.ThanhQuan = drKetQuaNoiSoi["ThanhQuan"].ToString();
                        break;
                    case LoaiNoiSoi.TongQuat:
                        _uKetQuaNoiSoiTongQuat.OngTaiTrai = drKetQuaNoiSoi["OngTaiTrai"].ToString();
                        _uKetQuaNoiSoiTongQuat.OngTaiPhai = drKetQuaNoiSoi["OngTaiPhai"].ToString();
                        _uKetQuaNoiSoiTongQuat.MangNhiTrai = drKetQuaNoiSoi["MangNhiTrai"].ToString();
                        _uKetQuaNoiSoiTongQuat.MangNhiPhai = drKetQuaNoiSoi["MangNhiPhai"].ToString();
                        _uKetQuaNoiSoiTongQuat.CanBuaTrai = drKetQuaNoiSoi["CanBuaTrai"].ToString();
                        _uKetQuaNoiSoiTongQuat.CanBuaPhai = drKetQuaNoiSoi["CanBuaPhai"].ToString();
                        _uKetQuaNoiSoiTongQuat.HomNhiTrai = drKetQuaNoiSoi["HomNhiTrai"].ToString();
                        _uKetQuaNoiSoiTongQuat.HomNhiPhai = drKetQuaNoiSoi["HomNhiPhai"].ToString();
                        break;
                }

                _ketQuaNoiSoi.KetQuaNoiSoiGUID = Guid.Parse(drKetQuaNoiSoi["KetQuaNoiSoiGUID"].ToString());

                if (drKetQuaNoiSoi["CreatedDate"] != null && drKetQuaNoiSoi["CreatedDate"] != DBNull.Value)
                    _ketQuaNoiSoi.CreatedDate = Convert.ToDateTime(drKetQuaNoiSoi["CreatedDate"]);

                if (drKetQuaNoiSoi["CreatedBy"] != null && drKetQuaNoiSoi["CreatedBy"] != DBNull.Value)
                    _ketQuaNoiSoi.CreatedBy = Guid.Parse(drKetQuaNoiSoi["CreatedBy"].ToString());

                if (drKetQuaNoiSoi["UpdatedDate"] != null && drKetQuaNoiSoi["UpdatedDate"] != DBNull.Value)
                    _ketQuaNoiSoi.UpdatedDate = Convert.ToDateTime(drKetQuaNoiSoi["UpdatedDate"]);

                if (drKetQuaNoiSoi["UpdatedBy"] != null && drKetQuaNoiSoi["UpdatedBy"] != DBNull.Value)
                    _ketQuaNoiSoi.UpdatedBy = Guid.Parse(drKetQuaNoiSoi["UpdatedBy"].ToString());

                if (drKetQuaNoiSoi["DeletedDate"] != null && drKetQuaNoiSoi["DeletedDate"] != DBNull.Value)
                    _ketQuaNoiSoi.DeletedDate = Convert.ToDateTime(drKetQuaNoiSoi["DeletedDate"]);

                if (drKetQuaNoiSoi["DeletedBy"] != null && drKetQuaNoiSoi["DeletedBy"] != DBNull.Value)
                    _ketQuaNoiSoi.DeletedBy = Guid.Parse(drKetQuaNoiSoi["DeletedBy"].ToString());

                _ketQuaNoiSoi.Status = Convert.ToByte(drKetQuaNoiSoi["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private Image ParseImage(byte[] buffer)
        {
            Bitmap bmp = null;

            try
            {
                MemoryStream ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                return bmp;
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            
            return bmp;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaNoiSoi_Load(object sender, EventArgs e)
        {
            InitData();

            if (_isNew)
                GenerateCode();
            else
                DisplayInfo(_drKetQuaNoiSoi);
        }

        private void cboLoaiNoiSoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoaiNoiSoi type = (LoaiNoiSoi)cboLoaiNoiSoi.SelectedIndex;
            switch (type)
            {
                case LoaiNoiSoi.Tai:
                    ViewControl(_uKetQuaNoiSoiTai);
                    break;
                case LoaiNoiSoi.Mui:
                    ViewControl(_uKetQuaNoiSoiMui);
                    break;
                case LoaiNoiSoi.Hong_ThanhQuan:
                    ViewControl(_uKetQuaNoiSoiHongThanhQuan);
                    break;
                case LoaiNoiSoi.TaiMuiHong:
                    ViewControl(_uKetQuaNoiSoiTaiMuiHong);
                    break;
                case LoaiNoiSoi.TongQuat:
                    ViewControl(_uKetQuaNoiSoiTongQuat);
                    break;
            }
        }

        private void dlgAddKetQuaNoiSoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
        #endregion
    }
}
