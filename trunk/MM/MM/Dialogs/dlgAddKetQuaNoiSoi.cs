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
using System.Drawing.Imaging;

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
        private bool _isPrint = false;
        private bool _allowEdit = true;
        public string MaBenhNhan = string.Empty;
        public string TenBenhNhan = string.Empty;
        private WatchingFolder _watchingFolder = null;
        #endregion

        #region Constructor
        public dlgAddKetQuaNoiSoi(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetQuaNoiSoi(string patientGUID, DataRow drKetQuaNoiSoi, bool allowEdit)
        {
            InitializeComponent();
            _allowEdit = allowEdit;
            _patientGUID = patientGUID;
            _drKetQuaNoiSoi = drKetQuaNoiSoi;
            _isNew = false;
            this.Text = "Sua kham noi soi";
        }
        #endregion

        #region Properties
        public bool IsPrint
        {
            get { return _isPrint; }
        }

        public KetQuaNoiSoi KetQuaNoiSoi
        {
            get { return _ketQuaNoiSoi; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtpkNgayKham.Value = DateTime.Now;
                cboLoaiNoiSoi.SelectedIndex = 0;

                //CleanCache();
                DisplayDSBacSiChiDinh();
                DisplayDSBasSiSoi();

                //_webCam = new WebCam();
                //_webCam.InitializeWebCam(ref picWebCam);

                //OnPlayWebCam();

                _watchingFolder = new WatchingFolder();
                _watchingFolder.OnCreatedFileEvent += new CreatedFileEventHandler(_watchingFolder_OnCreatedFileEvent);
                _watchingFolder.StartMoritoring(Global.HinhChupPath);

                if (!Utility.CheckRunningProcess(Const.TVHomeProcessName))
                    Utility.ExecuteFile(Global.TVHomeConfig.Path);
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
            
        }

        private void StopWatchingFolder()
        {
            try
            {
                if (_watchingFolder == null) return;
                _watchingFolder.OnCreatedFileEvent -= new CreatedFileEventHandler(_watchingFolder_OnCreatedFileEvent);
                _watchingFolder.StopMoritoring();
                _watchingFolder = null; 
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
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

        private bool CheckInfo()
        {
            if (cboBSCD.SelectedValue == null || cboBSCD.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bác sĩ chỉ định.", IconType.Information);
                cboBSCD.Focus();
                return false;
            }

            if (cboBSSoi.SelectedValue == null || cboBSSoi.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bác sĩ soi.", IconType.Information);
                cboBSSoi.Focus();
                return false;
            }

            if (picHinh1.Image == null && picHinh2.Image == null &&
                picHinh3.Image == null && picHinh4.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 hình nội soi.", IconType.Information);
                return false;
            }

            return true;
        }

        private void DisplayInfo(DataRow drKetQuaNoiSoi)
        {
            try
            {
                dtpkNgayKham.Value = Convert.ToDateTime(drKetQuaNoiSoi["NgayKham"]);
                cboBSCD.SelectedValue = drKetQuaNoiSoi["BacSiChiDinh"].ToString();
                cboBSSoi.SelectedValue = drKetQuaNoiSoi["BacSiSoi"].ToString();
                txtLyDoKham.Text = drKetQuaNoiSoi["LyDoKham"].ToString();
                cboLoaiNoiSoi.SelectedIndex = Convert.ToByte(drKetQuaNoiSoi["LoaiNoiSoi"]);
                cboKetLuan.Text = drKetQuaNoiSoi["KetLuan"].ToString();
                cboDeNghi.Text = drKetQuaNoiSoi["DeNghi"].ToString();

                if (drKetQuaNoiSoi["Hinh1"] != null && drKetQuaNoiSoi["Hinh1"] != DBNull.Value)
                    picHinh1.Image = Utility.ParseImage((byte[])drKetQuaNoiSoi["Hinh1"]);

                if (drKetQuaNoiSoi["Hinh2"] != null && drKetQuaNoiSoi["Hinh2"] != DBNull.Value)
                    picHinh2.Image = Utility.ParseImage((byte[])drKetQuaNoiSoi["Hinh2"]);

                if (drKetQuaNoiSoi["Hinh3"] != null && drKetQuaNoiSoi["Hinh3"] != DBNull.Value)
                    picHinh3.Image = Utility.ParseImage((byte[])drKetQuaNoiSoi["Hinh3"]);

                if (drKetQuaNoiSoi["Hinh4"] != null && drKetQuaNoiSoi["Hinh4"] != DBNull.Value)
                    picHinh4.Image = Utility.ParseImage((byte[])drKetQuaNoiSoi["Hinh4"]);

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
                    case LoaiNoiSoi.DaDay:
                        _uKetQuaNoiSoiDaDay.ThucQuan = drKetQuaNoiSoi["ThucQuan"].ToString();
                        _uKetQuaNoiSoiDaDay.DaDay = drKetQuaNoiSoi["DaDay"].ToString();
                        _uKetQuaNoiSoiDaDay.HangVi = drKetQuaNoiSoi["HangVi"].ToString();
                        _uKetQuaNoiSoiDaDay.MonVi = drKetQuaNoiSoi["MonVi"].ToString();
                        _uKetQuaNoiSoiDaDay.HanhTaTrang = drKetQuaNoiSoi["HanhTaTrang"].ToString();
                        _uKetQuaNoiSoiDaDay.Clotest = drKetQuaNoiSoi["Clotest"].ToString();
                        break;
                    case LoaiNoiSoi.TrucTrang:
                        _uKetQuaNoiSoiTrucTrang.TrucTrang = drKetQuaNoiSoi["TrucTrang"].ToString();
                        _uKetQuaNoiSoiTrucTrang.DaiTrangTrai = drKetQuaNoiSoi["DaiTrangTrai"].ToString();
                        _uKetQuaNoiSoiTrucTrang.DaiTrangGocLach = drKetQuaNoiSoi["DaiTrangGocLach"].ToString();
                        _uKetQuaNoiSoiTrucTrang.DaiTrangNgang = drKetQuaNoiSoi["DaiTrangNgang"].ToString();
                        _uKetQuaNoiSoiTrucTrang.DaiTrangGocGan = drKetQuaNoiSoi["DaiTrangGocGan"].ToString();
                        _uKetQuaNoiSoiTrucTrang.DaiTrangPhai = drKetQuaNoiSoi["DaiTrangPhai"].ToString();
                        _uKetQuaNoiSoiTrucTrang.ManhTrang = drKetQuaNoiSoi["ManhTrang"].ToString();
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

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    btnSaveAndPrint.Enabled = _allowEdit;
                    panel2.Enabled = _allowEdit;
                    panel4.Enabled = _allowEdit;
                    panel5.Enabled = _allowEdit;
                    panel6.Enabled = _allowEdit;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void ChonHinh()
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            dlgChonHinh dlg = new dlgChonHinh();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Image img = (Image)lvCapture.SelectedItems[0].Tag;//imgListCapture.Images[lvCapture.SelectedItems[0].ImageIndex];

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
                    _ketQuaNoiSoi.SoPhieu = string.Empty;
                    _ketQuaNoiSoi.NgayKham = dtpkNgayKham.Value;
                    _ketQuaNoiSoi.BacSiChiDinh = Guid.Parse(cboBSCD.SelectedValue.ToString());
                    _ketQuaNoiSoi.BacSiSoi = Guid.Parse(cboBSSoi.SelectedValue.ToString());
                    _ketQuaNoiSoi.LyDoKham = txtLyDoKham.Text;
                    _ketQuaNoiSoi.LoaiNoiSoi = Convert.ToByte(cboLoaiNoiSoi.SelectedIndex);
                    _ketQuaNoiSoi.KetLuan = cboKetLuan.Text;
                    _ketQuaNoiSoi.DeNghi = cboDeNghi.Text;

                    _ketQuaNoiSoi.Hinh1 = null;
                    _ketQuaNoiSoi.Hinh2 = null;
                    _ketQuaNoiSoi.Hinh3 = null;
                    _ketQuaNoiSoi.Hinh4 = null;

                    if (picHinh1.Image != null)
                        _ketQuaNoiSoi.Hinh1 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh1.Image));

                    if (picHinh2.Image != null)
                        _ketQuaNoiSoi.Hinh2 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh2.Image));

                    if (picHinh3.Image != null)
                        _ketQuaNoiSoi.Hinh3 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh3.Image));

                    if (picHinh4.Image != null)
                        _ketQuaNoiSoi.Hinh4 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh4.Image));

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
                        case LoaiNoiSoi.DaDay:
                            _ketQuaNoiSoi.ThucQuan = _uKetQuaNoiSoiDaDay.ThucQuan;
                            _ketQuaNoiSoi.DaDay = _uKetQuaNoiSoiDaDay.DaDay;
                            _ketQuaNoiSoi.HangVi = _uKetQuaNoiSoiDaDay.HangVi;
                            _ketQuaNoiSoi.MonVi = _uKetQuaNoiSoiDaDay.MonVi;
                            _ketQuaNoiSoi.HanhTaTrang = _uKetQuaNoiSoiDaDay.HanhTaTrang;
                            _ketQuaNoiSoi.Clotest = _uKetQuaNoiSoiDaDay.Clotest;
                            break;
                        case LoaiNoiSoi.TrucTrang:
                            _ketQuaNoiSoi.TrucTrang = _uKetQuaNoiSoiTrucTrang.TrucTrang;
                            _ketQuaNoiSoi.DaiTrangTrai = _uKetQuaNoiSoiTrucTrang.DaiTrangTrai;
                            _ketQuaNoiSoi.DaiTrangGocLach = _uKetQuaNoiSoiTrucTrang.DaiTrangGocLach;
                            _ketQuaNoiSoi.DaiTrangNgang = _uKetQuaNoiSoiTrucTrang.DaiTrangNgang;
                            _ketQuaNoiSoi.DaiTrangGocGan = _uKetQuaNoiSoiTrucTrang.DaiTrangGocGan;
                            _ketQuaNoiSoi.DaiTrangPhai = _uKetQuaNoiSoiTrucTrang.DaiTrangPhai;
                            _ketQuaNoiSoi.ManhTrang = _uKetQuaNoiSoiTrucTrang.ManhTrang;
                            break;
                    }

                    Result result = KetQuaNoiSoiBus.InsertKetQuaNoiSoi(_ketQuaNoiSoi);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaNoiSoiBus.InsertKetQuaNoiSoi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.InsertKetQuaNoiSoi"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    else
                    {
                        List<Bookmark> bookmarkList = null;
                        switch (type)
                        {
                            case LoaiNoiSoi.Tai:
                                bookmarkList = GetBoommarkTai();
                                break;
                            case LoaiNoiSoi.Mui:
                                bookmarkList = GetBoommarkMui();
                                break;
                            case LoaiNoiSoi.Hong_ThanhQuan:
                                bookmarkList = GetBoommarkHongThanhQuan();
                                break;
                            case LoaiNoiSoi.TaiMuiHong:
                                bookmarkList = GetBoommarkTaiMuiHong();
                                break;
                            case LoaiNoiSoi.TongQuat:
                                bookmarkList = GetBoommarkTongQuat();
                                break;
                            case LoaiNoiSoi.DaDay:
                                bookmarkList = GetBoommarkDaDay();
                                break;
                            case LoaiNoiSoi.TrucTrang:
                                bookmarkList = GetBoommarkTrucTrang();
                                break;
                        }

                        if (bookmarkList != null && bookmarkList.Count > 0)
                        {
                            result = BookmarkBus.InsertBookmark(bookmarkList);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(this.Text, result.GetErrorAsString("BookmarkBus.InsertBookmark"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("BookmarkBus.InsertBookmark"));
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            }
                        }
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

        private List<Bookmark> GetBoommarkTai()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiTai.OngTaiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiOngTai;
                bookmark.Value = _uKetQuaNoiSoiTai.OngTaiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.OngTaiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTai.OngTaiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTai.OngTaiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiOngTai;
                bookmark.Value = _uKetQuaNoiSoiTai.OngTaiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.MangNhiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMangNhi;
                bookmark.Value = _uKetQuaNoiSoiTai.MangNhiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.MangNhiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTai.MangNhiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTai.MangNhiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMangNhi;
                bookmark.Value = _uKetQuaNoiSoiTai.MangNhiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.CanBuaPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCanBua;
                bookmark.Value = _uKetQuaNoiSoiTai.CanBuaPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.CanBuaTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTai.CanBuaTrai.Trim().ToUpper() != _uKetQuaNoiSoiTai.CanBuaPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCanBua;
                bookmark.Value = _uKetQuaNoiSoiTai.CanBuaTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.HomNhiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHomNhi;
                bookmark.Value = _uKetQuaNoiSoiTai.HomNhiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.HomNhiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTai.HomNhiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTai.HomNhiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHomNhi;
                bookmark.Value = _uKetQuaNoiSoiTai.HomNhiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.ValsavaPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiValsava;
                bookmark.Value = _uKetQuaNoiSoiTai.ValsavaPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTai.ValsavaTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTai.ValsavaTrai.Trim().ToUpper() != _uKetQuaNoiSoiTai.ValsavaPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiValsava;
                bookmark.Value = _uKetQuaNoiSoiTai.ValsavaTrai;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiTai;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiTai;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkTongQuat()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiTongQuat.OngTaiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiOngTai;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.OngTaiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.OngTaiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTongQuat.OngTaiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTongQuat.OngTaiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiOngTai;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.OngTaiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.MangNhiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMangNhi;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.MangNhiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.MangNhiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTongQuat.MangNhiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTongQuat.MangNhiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMangNhi;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.MangNhiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.CanBuaPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCanBua;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.CanBuaPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.CanBuaTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTongQuat.CanBuaTrai.Trim().ToUpper() != _uKetQuaNoiSoiTongQuat.CanBuaPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCanBua;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.CanBuaTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.HomNhiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHomNhi;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.HomNhiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTongQuat.HomNhiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiTongQuat.HomNhiTrai.Trim().ToUpper() != _uKetQuaNoiSoiTongQuat.HomNhiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHomNhi;
                bookmark.Value = _uKetQuaNoiSoiTongQuat.HomNhiTrai;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiTongQuat;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiTongQuat;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkMui()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiMui.NiemMacPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiNiemMac;
                bookmark.Value = _uKetQuaNoiSoiMui.NiemMacPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.NiemMacTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.NiemMacTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.NiemMacPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiNiemMac;
                bookmark.Value = _uKetQuaNoiSoiMui.NiemMacTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.VachNganPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVachNgan;
                bookmark.Value = _uKetQuaNoiSoiMui.VachNganPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.VachNganTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.VachNganTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.VachNganPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVachNgan;
                bookmark.Value = _uKetQuaNoiSoiMui.VachNganTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.KheTrenPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheTren;
                bookmark.Value = _uKetQuaNoiSoiMui.KheTrenPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.KheTrenTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.KheTrenTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.KheTrenPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheTren;
                bookmark.Value = _uKetQuaNoiSoiMui.KheTrenTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.KheGiuaPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheGiua;
                bookmark.Value = _uKetQuaNoiSoiMui.KheGiuaPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.KheGiuaTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.KheGiuaTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.KheGiuaPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheGiua;
                bookmark.Value = _uKetQuaNoiSoiMui.KheGiuaTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.CuonGiuaPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCuonGiua;
                bookmark.Value = _uKetQuaNoiSoiMui.CuonGiuaPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.CuonGiuaTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.CuonGiuaTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.CuonGiuaPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCuonGiua;
                bookmark.Value = _uKetQuaNoiSoiMui.CuonGiuaTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.CuonDuoiPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCuonDuoi;
                bookmark.Value = _uKetQuaNoiSoiMui.CuonDuoiPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.CuonDuoiTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.CuonDuoiTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.CuonDuoiPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiCuonDuoi;
                bookmark.Value = _uKetQuaNoiSoiMui.CuonDuoiTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.MomMocPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMomMoc;
                bookmark.Value = _uKetQuaNoiSoiMui.MomMocPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.MomMocTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.MomMocTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.MomMocPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMomMoc;
                bookmark.Value = _uKetQuaNoiSoiMui.MomMocTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.BongSangPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiBongSang;
                bookmark.Value = _uKetQuaNoiSoiMui.BongSangPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.BongSangTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.BongSangTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.BongSangPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiBongSang;
                bookmark.Value = _uKetQuaNoiSoiMui.BongSangTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.VomPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVom;
                bookmark.Value = _uKetQuaNoiSoiMui.VomPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiMui.VomTrai.Trim() != string.Empty &&
                _uKetQuaNoiSoiMui.VomTrai.Trim().ToUpper() != _uKetQuaNoiSoiMui.VomPhai.Trim().ToUpper())
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVom;
                bookmark.Value = _uKetQuaNoiSoiMui.VomTrai;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiMui;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiMui;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkHongThanhQuan()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiHongThanhQuan.Amydale.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiAmydale;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.Amydale;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiHongThanhQuan.XoangLe.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiXoangLe;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.XoangLe;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiHongThanhQuan.MiengThucQuan.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMiengThucQuan;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.MiengThucQuan;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiHongThanhQuan.SunPheu.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiSunPheu;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.SunPheu;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiHongThanhQuan.DayThanh.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDayThanh;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.DayThanh;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiHongThanhQuan.BangThanhThat.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiBangThanhThat;
                bookmark.Value = _uKetQuaNoiSoiHongThanhQuan.BangThanhThat;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiHongThanhQuan;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiHongThanhQuan;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkTaiMuiHong()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiTaiMuiHong.OngTaiNgoai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiOngTai;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.OngTaiNgoai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.MangNhi.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMangNhi;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.MangNhi;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.NiemMacMui.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiNiemMac;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.NiemMacMui;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.VachNgan.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVachNgan;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.VachNgan;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.KheTren.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheTren;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.KheTren;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.KheGiua.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiKheGiua;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.KheGiua;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.MomMocBongSang.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMomMoc_BongSang;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.MomMocBongSang;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.Vom.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiVom;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.Vom;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.Amydale.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiAmydale;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.Amydale;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTaiMuiHong.ThanhQuan.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiThanhQuan;
                bookmark.Value = _uKetQuaNoiSoiTaiMuiHong.ThanhQuan;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiTaiMuiHong;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiTaiMuiHong;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkDaDay()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiDaDay.ThucQuan.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiThanhQuan;
                bookmark.Value = _uKetQuaNoiSoiDaDay.ThucQuan;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiDaDay.DaDay.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaDay;
                bookmark.Value = _uKetQuaNoiSoiDaDay.DaDay;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiDaDay.HangVi.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHangVi;
                bookmark.Value = _uKetQuaNoiSoiDaDay.HangVi;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiDaDay.MonVi.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiMonVi;
                bookmark.Value = _uKetQuaNoiSoiDaDay.MonVi;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiDaDay.HanhTaTrang.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiHanhTaTrang;
                bookmark.Value = _uKetQuaNoiSoiDaDay.HanhTaTrang;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiDaDay.Clotest.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiClotest;
                bookmark.Value = _uKetQuaNoiSoiDaDay.Clotest;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiDaDay;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiDaDay;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private List<Bookmark> GetBoommarkTrucTrang()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaNoiSoiTrucTrang.TrucTrang.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiTrucTrang;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.TrucTrang;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.DaiTrangTrai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaiTrangTrai;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.DaiTrangTrai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.DaiTrangGocLach.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaiTrangGocLach;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.DaiTrangGocLach;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.DaiTrangNgang.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaiTrangNgang;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.DaiTrangNgang;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.DaiTrangGocGan.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaiTrangGocGan;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.DaiTrangGocGan;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.DaiTrangPhai.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiDaiTrangPhai;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.DaiTrangPhai;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaNoiSoiTrucTrang.ManhTrang.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaNoiSoiManhTrang;
                bookmark.Value = _uKetQuaNoiSoiTrucTrang.ManhTrang;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanNoiSoiTrucTrang;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.DeNghiNoiSoiTrucTrang;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private void OnPlayWebCam()
        {
            try
            {
                btnStop.Enabled = true;
                btnCapture.Enabled = true;
                btnPlay.Enabled = false;

                if (!_isContinue)
                {
                    _webCam.Start();
                    _isContinue = true;
                }
                else
                    _webCam.Continue();

            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnStopWebCam()
        {
            try
            {
                btnStop.Enabled = false;
                btnCapture.Enabled = false;
                btnPlay.Enabled = true;

                _webCam.Stop();
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void DisplayBookmarkKetLuanDeNghi(LoaiNoiSoi type)
        {
            Result result = null;
            DataTable dtKetLuan = null;
            DataTable dtDeNghi = null;

            switch (type)
            {
                case LoaiNoiSoi.Tai:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiTai);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiTai);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.Mui:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiMui);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiMui);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.Hong_ThanhQuan:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiHongThanhQuan);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiHongThanhQuan);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.TaiMuiHong:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiTaiMuiHong);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiTaiMuiHong);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.TongQuat:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiTongQuat);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiTongQuat);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.DaDay:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiDaDay);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiDaDay);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
                case LoaiNoiSoi.TrucTrang:
                    result = BookmarkBus.GetBookmark(BookMarkType.KetLuanNoiSoiTrucTrang);
                    if (result.IsOK) dtKetLuan = result.QueryResult as DataTable;

                    result = BookmarkBus.GetBookmark(BookMarkType.DeNghiNoiSoiTrucTrang);
                    if (result.IsOK) dtDeNghi = result.QueryResult as DataTable;
                    break;
            }

            if (dtKetLuan != null && dtKetLuan.Rows.Count > 0)
            {
                cboKetLuan.Items.Clear();
                foreach (DataRow row in dtKetLuan.Rows)
                    cboKetLuan.Items.Add(row["Value"].ToString());
            }

            if (dtDeNghi != null && dtDeNghi.Rows.Count > 0)
            {
                cboDeNghi.Items.Clear();
                foreach (DataRow row in dtDeNghi.Rows)
                    cboDeNghi.Items.Add(row["Value"].ToString());
            }
        }

        private void ChonHinhTuBenNgoai(PictureBox picBox)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp; *gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp; *gif";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(dlg.FileName);
                picBox.Image = bmp;
            }
        }

        private void CacheImage(Image image)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Cache\\KetQuaNoiSoi",
                DateTime.Now.ToString("yyyy-MM-dd"), string.Format("{0}-{1}", MaBenhNhan, Utility.ConvertToUnSign2(TenBenhNhan)));

                Utility.CreateFolder(path);

                path = Path.Combine(path, string.Format("{0}.png", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ms")));
                image.Save(path, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
            
        }

        private void CleanCache()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Cache\\KetQuaNoiSoi");
                Utility.CreateFolder(path);
                string[] dirs = Directory.GetDirectories(path);

                DateTime minDate = DateTime.Now.AddDays(-7);
                minDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 0, 0, 0);
                foreach (var dir in dirs)
                {

                    string strDate = dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, "");
                    DateTime date = DateTime.ParseExact(strDate, "yyyy-MM-dd", null);
                    if (date < minDate)
                        Directory.Delete(dir, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
            
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaNoiSoi_Load(object sender, EventArgs e)
        {
            while (!this.IsHandleCreated)
                Thread.Sleep(1000);

            InitData();

            if (!_isNew)
                DisplayInfo(_drKetQuaNoiSoi);
            else
            {
                _uKetQuaNoiSoiTai.SetDefault();
                _uKetQuaNoiSoiMui.SetDefault();
                _uKetQuaNoiSoiHongThanhQuan.SetDefault();
                _uKetQuaNoiSoiTaiMuiHong.SetDefault();
                _uKetQuaNoiSoiTongQuat.SetDefault();
                _uKetQuaNoiSoiDaDay.SetDefault();
                _uKetQuaNoiSoiTrucTrang.SetDefault();
            }
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
                case LoaiNoiSoi.DaDay:
                    ViewControl(_uKetQuaNoiSoiDaDay);
                    break;
                case LoaiNoiSoi.TrucTrang:
                    ViewControl(_uKetQuaNoiSoiTrucTrang);
                    break;
            }

            DisplayBookmarkKetLuanDeNghi(type);
        }

        private void dlgAddKetQuaNoiSoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    SaveInfoAsThread();
                    //OnStopWebCam();
                }
                else
                    e.Cancel = true;
            }
            else 
            {
                if (_allowEdit && MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin khám nội soi ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SaveInfoAsThread();
                        //OnStopWebCam();
                    }
                    else
                        e.Cancel = true;
                }
                //else
                //    OnStopWebCam();
            } 
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //OnPlayWebCam();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //OnStopWebCam();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //if (picWebCam.Image == null) return;

            //Image img = picWebCam.Image;
            //CacheImage(img);
            //imgListCapture.Images.Add(img);

            //_imgCount++;
            //ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
            //item.Tag = picWebCam.Image;
            //lvCapture.Items.Add(item);

            //if (lvCapture.Items.Count <= 4)
            //{
            //    switch (lvCapture.Items.Count)
            //    {
            //        case 1:
            //            picHinh1.Image = (Image)lvCapture.Items[0].Tag;
            //            break;
            //        case 2:
            //            picHinh2.Image = (Image)lvCapture.Items[1].Tag;
            //            break;
            //        case 3:
            //            picHinh3.Image = (Image)lvCapture.Items[2].Tag;
            //            break;
            //        case 4:
            //            picHinh4.Image = (Image)lvCapture.Items[3].Tag;
            //            break;
            //    }
            //}
        }

        private void _watchingFolder_OnCreatedFileEvent(FileSystemEventArgs e)
        {
            try
            {
                string ext = Path.GetExtension(e.FullPath);
                int count = 0;
                Image bmp = null;
                bool isRenameOK = false;
                string fileName = string.Format("{0}\\KQNS-{1}-{2}{3}", Global.HinhChupPath, MaBenhNhan, 
                    DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ms"), ext);
                while (File.Exists(fileName))
                {
                    fileName = string.Format("{0}\\KQNS-{1}-{2}{3}", Global.HinhChupPath, MaBenhNhan, 
                        DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ms"), ext);
                    Thread.Sleep(5);
                }

                while ((bmp == null || !isRenameOK) && count <= Const.DeplayCount)
                {
                    try
                    {
                        if (bmp == null)
                            bmp = Utility.LoadImageFromFile(e.FullPath);
                    }
                    catch (Exception ex)
                    {
                        bmp = null;
                    }

                    if (bmp != null)
                    {
                        try
                        {
                            isRenameOK = Utility.RenameFileName(e.FullPath, fileName);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    count++;
                    Thread.Sleep(10);
                }

                if (bmp == null) return;

                this.Invoke(new MethodInvoker(delegate()
                {
                    imgListCapture.Images.Add(bmp);

                    _imgCount++;
                    ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
                    item.Tag = bmp;
                    lvCapture.Items.Add(item);

                    if (lvCapture.Items.Count <= 4)
                    {
                        switch (lvCapture.Items.Count)
                        {
                            case 1:
                                picHinh1.Image = (Image)lvCapture.Items[0].Tag;
                                break;
                            case 2:
                                picHinh2.Image = (Image)lvCapture.Items[1].Tag;
                                break;
                            case 3:
                                picHinh3.Image = (Image)lvCapture.Items[2].Tag;
                                break;
                            case 4:
                                picHinh4.Image = (Image)lvCapture.Items[3].Tag;
                                break;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.WriteToTraceLog(ex.Message);
            }

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            if (MsgBox.Question(this.Text, "Bạn có muốn xóa hình bạn đang chọn ?") == System.Windows.Forms.DialogResult.No) return;

            foreach (ListViewItem item in lvCapture.SelectedItems)
            {
                int imgIndex = item.ImageIndex;
                lvCapture.Items.Remove(item);
                imgListCapture.Images.RemoveAt(imgIndex);
            }
        }

        private void xóaTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MsgBox.Question(this.Text, "Bạn có muốn xóa tất cả hình ?") == System.Windows.Forms.DialogResult.No) return;

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

        private void tabKhamNoiSoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_allowEdit && tabKhamNoiSoi.SelectedIndex == 1)
            //{
            //    if (btnPlay.Enabled)
            //        OnPlayWebCam();
            //}
        }

        private void dlgAddKetQuaNoiSoi_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopWatchingFolder();
        }

        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (picHinh1.Image == null) return;
            picHinh1.Image = null;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (picHinh2.Image == null) return;
            picHinh2.Image = null;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (picHinh3.Image == null) return;
            picHinh3.Image = null;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (picHinh4.Image == null) return;
            picHinh4.Image = null;
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            _isPrint = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh1);
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh2);
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh3);
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh4);
        }

        private void picHinh1_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh1);
        }

        private void picHinh2_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh2);
        }

        private void picHinh3_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh3);
        }

        private void picHinh4_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh4);
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
