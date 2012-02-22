﻿using System;
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
        private bool _isContinue = false;
        private WebCam _webCam = null;
        private int _imgCount = 0;
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

            _webCam = new WebCam();
            _webCam.InitializeWebCam(ref picWebCam);
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
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                return bmp;
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
            
            return bmp;
        }

        private byte[] GetBinaryFromImage(Image img)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.GetBuffer();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }

            return null;
        }

        private void ChonHinh()
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            dlgChonHinh dlg = new dlgChonHinh();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Image img = imgListCapture.Images[lvCapture.SelectedItems[0].ImageIndex];

                switch (dlg.ImageIndex)
                {
                    case 1:
                        picHinh1.Image = img;
                        break;
                    case 2:
                        picHinh2.Image = img;
                        break;
                    case 3:
                        picHinh3.Image = img;
                        break;
                    case 4:
                        picHinh4.Image = img;
                        break;
                }
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

        private void OnSaveInfo()
        {
            try
            {
                _ketQuaNoiSoi.PatientGUID = Guid.Parse(_patientGUID);
                _ketQuaNoiSoi.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _ketQuaNoiSoi.CreatedDate = DateTime.Now;
                    _ketQuaNoiSoi.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaNoiSoi.UpdatedDate = DateTime.Now;
                    _ketQuaNoiSoi.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _ketQuaNoiSoi.SoPhieu = txtSoPhieu.Text;
                    _ketQuaNoiSoi.NgayKham = dtpkNgayKham.Value;
                    _ketQuaNoiSoi.BacSiChiDinh = Guid.Parse(cboBSCD.SelectedValue.ToString());
                    _ketQuaNoiSoi.BacSiSoi = Guid.Parse(cboBSSoi.SelectedValue.ToString());
                    _ketQuaNoiSoi.LyDoKham = txtLyDoKham.Text;
                    _ketQuaNoiSoi.LoaiNoiSoi = Convert.ToByte(cboLoaiNoiSoi.SelectedIndex);
                    _ketQuaNoiSoi.KetLuan = cboKetLuan.Text;
                    _ketQuaNoiSoi.DeNghi = cboDeNghi.Text;

                    _ketQuaNoiSoi.Hinh1 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh1.Image));
                    _ketQuaNoiSoi.Hinh2 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh2.Image));
                    _ketQuaNoiSoi.Hinh3 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh3.Image));
                    _ketQuaNoiSoi.Hinh4 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh4.Image));

                    LoaiNoiSoi type = (LoaiNoiSoi)cboLoaiNoiSoi.SelectedIndex;
                    switch (type)
                    {
                        case LoaiNoiSoi.Tai:
                            _ketQuaNoiSoi.OngTaiTrai = _uKetQuaNoiSoiTai.OngTaiTrai;
                            _ketQuaNoiSoi.OngTaiPhai = _uKetQuaNoiSoiTai.OngTaiPhai;
                            _ketQuaNoiSoi.MangNhiTrai = _uKetQuaNoiSoiTai.MangNhiTrai;
                            _ketQuaNoiSoi.MangNhiPhai = _uKetQuaNoiSoiTai.MangNhiPhai;
                            _ketQuaNoiSoi.CanBuaTrai =  _uKetQuaNoiSoiTai.CanBuaTrai;
                            _ketQuaNoiSoi.CanBuaPhai = _uKetQuaNoiSoiTai.CanBuaPhai;
                            _ketQuaNoiSoi.HomNhiTrai = _uKetQuaNoiSoiTai.HomNhiTrai;
                            _ketQuaNoiSoi.HomNhiPhai = _uKetQuaNoiSoiTai.HomNhiPhai;
                            _ketQuaNoiSoi.ValsavaTrai = _uKetQuaNoiSoiTai.ValsavaTrai;
                            _ketQuaNoiSoi.ValsavaPhai = _uKetQuaNoiSoiTai.ValsavaPhai;
                            break;
                        case LoaiNoiSoi.Mui:
                             _ketQuaNoiSoi.NiemMacTrai = _uKetQuaNoiSoiMui.NiemMacTrai;
                            _ketQuaNoiSoi.NiemMacPhai = _uKetQuaNoiSoiMui.NiemMacPhai;
                            _ketQuaNoiSoi.VachNganTrai = _uKetQuaNoiSoiMui.VachNganTrai;
                            _ketQuaNoiSoi.VachNganPhai = _uKetQuaNoiSoiMui.VachNganPhai;
                            _ketQuaNoiSoi.KheTrenTrai = _uKetQuaNoiSoiMui.KheTrenTrai;
                            _ketQuaNoiSoi.KheTrenPhai = _uKetQuaNoiSoiMui.KheTrenPhai;
                            _ketQuaNoiSoi.KheGiuaTrai = _uKetQuaNoiSoiMui.KheGiuaTrai;
                            _ketQuaNoiSoi.KheGiuaPhai = _uKetQuaNoiSoiMui.KheGiuaPhai;
                            _ketQuaNoiSoi.CuonGiuaTrai = _uKetQuaNoiSoiMui.CuonGiuaTrai;
                            _ketQuaNoiSoi.CuonGiuaPhai = _uKetQuaNoiSoiMui.CuonGiuaPhai;
                            _ketQuaNoiSoi.CuonDuoiTrai = _uKetQuaNoiSoiMui.CuonDuoiTrai;
                            _ketQuaNoiSoi.CuonDuoiPhai = _uKetQuaNoiSoiMui.CuonDuoiPhai;
                            _ketQuaNoiSoi.MomMocTrai = _uKetQuaNoiSoiMui.MomMocTrai;
                            _ketQuaNoiSoi.MomMocPhai = _uKetQuaNoiSoiMui.MomMocPhai;
                            _ketQuaNoiSoi.BongSangTrai = _uKetQuaNoiSoiMui.BongSangTrai;
                            _ketQuaNoiSoi.BongSangPhai = _uKetQuaNoiSoiMui.BongSangPhai;
                            _ketQuaNoiSoi.VomTrai = _uKetQuaNoiSoiMui.VomTrai;
                            _ketQuaNoiSoi.VomPhai = _uKetQuaNoiSoiMui.VomPhai;
                            break;
                        case LoaiNoiSoi.Hong_ThanhQuan:
                            _ketQuaNoiSoi.Amydale = _uKetQuaNoiSoiHongThanhQuan.Amydale;
                            _ketQuaNoiSoi.XoangLe = _uKetQuaNoiSoiHongThanhQuan.XoangLe;
                            _ketQuaNoiSoi.MiengThucQuan = _uKetQuaNoiSoiHongThanhQuan.MiengThucQuan;
                            _ketQuaNoiSoi.SunPheu = _uKetQuaNoiSoiHongThanhQuan.SunPheu;
                            _ketQuaNoiSoi.DayThanh = _uKetQuaNoiSoiHongThanhQuan.DayThanh;
                            _ketQuaNoiSoi.BangThanhThat = _uKetQuaNoiSoiHongThanhQuan.BangThanhThat;
                            break;
                        case LoaiNoiSoi.TaiMuiHong:
                            _ketQuaNoiSoi.OngTaiNgoai = _uKetQuaNoiSoiTaiMuiHong.OngTaiNgoai;
                            _ketQuaNoiSoi.MangNhi = _uKetQuaNoiSoiTaiMuiHong.MangNhi;
                            _ketQuaNoiSoi.NiemMac = _uKetQuaNoiSoiTaiMuiHong.NiemMacMui;
                            _ketQuaNoiSoi.VachNgan = _uKetQuaNoiSoiTaiMuiHong.VachNgan;
                            _ketQuaNoiSoi.KheTren = _uKetQuaNoiSoiTaiMuiHong.KheTren;
                            _ketQuaNoiSoi.KheGiua = _uKetQuaNoiSoiTaiMuiHong.KheGiua;
                            _ketQuaNoiSoi.MomMoc_BongSang = _uKetQuaNoiSoiTaiMuiHong.MomMocBongSang;
                            _ketQuaNoiSoi.Vom = _uKetQuaNoiSoiTaiMuiHong.Vom;
                            _ketQuaNoiSoi.Amydale = _uKetQuaNoiSoiTaiMuiHong.Amydale;
                            _ketQuaNoiSoi.ThanhQuan = _uKetQuaNoiSoiTaiMuiHong.ThanhQuan;
                            break;
                        case LoaiNoiSoi.TongQuat:
                            _ketQuaNoiSoi.OngTaiTrai = _uKetQuaNoiSoiTongQuat.OngTaiTrai;
                            _ketQuaNoiSoi.OngTaiPhai = _uKetQuaNoiSoiTongQuat.OngTaiPhai;
                            _ketQuaNoiSoi.MangNhiTrai = _uKetQuaNoiSoiTongQuat.MangNhiTrai;
                            _ketQuaNoiSoi.MangNhiPhai = _uKetQuaNoiSoiTongQuat.MangNhiPhai;
                            _ketQuaNoiSoi.CanBuaTrai = _uKetQuaNoiSoiTongQuat.CanBuaTrai;
                            _ketQuaNoiSoi.CanBuaPhai = _uKetQuaNoiSoiTongQuat.CanBuaPhai;
                            _ketQuaNoiSoi.HomNhiTrai = _uKetQuaNoiSoiTongQuat.HomNhiTrai;
                            _ketQuaNoiSoi.HomNhiPhai = _uKetQuaNoiSoiTongQuat.HomNhiPhai;
                            break;
                    }

                    Result result = KetQuaNoiSoiBus.InsertKetQuaNoiSoi(_ketQuaNoiSoi);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaNoiSoiBus.InsertKetQuaNoiSoi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.InsertKetQuaNoiSoi"));
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
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông khám nội soi ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isContinue)
                {
                    _webCam.Start();
                    _isContinue = true;
                }
                else
                    _webCam.Continue();

                btnStop.Enabled = true;
                btnCapture.Enabled = true;
                btnPlay.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                _webCam.Stop();

                btnStop.Enabled = false;
                btnCapture.Enabled = false;
                btnPlay.Enabled = true;
            }
            catch (Exception ex)
            {
                 MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (picWebCam.Image == null) return;

            Image img = picWebCam.Image;
            imgListCapture.Images.Add(img);

            _imgCount++;
            ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
            lvCapture.Items.Add(item);
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            foreach (ListViewItem item in lvCapture.SelectedItems)
            {
                int imgIndex = item.ImageIndex;
                lvCapture.Items.Remove(item);
                imgListCapture.Images.RemoveAt(imgIndex);
            }
        }

        private void xóaTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvCapture.Items.Clear();
            imgListCapture.Images.Clear();
            _imgCount = 0;
        }

        private void lvCapture_DoubleClick(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void chọnHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChonHinh();
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
