using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;

namespace MM.Controls
{
    public partial class uPatient : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private bool _isCallDisplayInfo = false;
        #endregion

        #region Constructor
        public uPatient()
        {
            InitializeComponent();
            _uServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
            _uDailyServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
            _uServiceHistory.OnExportReceiptChanged += new ExportReceiptChangedHandler(_uServiceHistory_OnExportReceiptChanged);
            _uDailyServiceHistory.OnExportReceiptChanged += new ExportReceiptChangedHandler(_uServiceHistory_OnExportReceiptChanged);
            _uServiceHistory.OnRefreshCheckList += new RefreshCheckListHandler(_uServiceHistory_OnRefreshCheckList);
            _uDailyServiceHistory.OnRefreshCheckList += new RefreshCheckListHandler(_uServiceHistory_OnRefreshCheckList);
            this.HandleCreated += new EventHandler(uPatient_HandleCreated);
            _uToaThuocList.EnableTextboxBenhNhan = false;
        }

        private void _uServiceHistory_OnRefreshCheckList()
        {
            DisplayCheckListAsThread();
        }

        private void _uServiceHistory_OnExportReceiptChanged()
        {
            DisplayCheckListAsThread();
        }

        private void uPatient_HandleCreated(object sender, EventArgs e)
        {
            DisplayInfo();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                _uServiceHistory.PatientRow = value;
                _uDailyServiceHistory.PatientRow = value;
                _uToaThuocList.PatientRow = value;
                _uChiDinhList.PatientRow = value;
                _uCanDoList.PatientRow = value;
                _uLoiKhuyenList.PatientRow = value;
                _uKetQuaLamSangList.PatientRow = value;
                _uKetQuaCanLamSangList.PatientRow = value;
                _uKetLuanList.PatientRow = value;
                _uKetQuaNoiSoiList.PatientRow = value;
                _uKetQuaSoiCTCList.PatientRow = value;
                _uKetQuaSieuAmList.PatientRow = value;
            }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            if (!this.IsHandleCreated)
            {
                _isCallDisplayInfo = true;
                return;
            }
            else
                _isCallDisplayInfo = false;

            if (_patientRow == null) return;

            DataRow row = _patientRow as DataRow;

            txtFullName.Text = row["FullName"].ToString();
            txtGender.Text = row["GenderAsStr"].ToString();
            txtDOB.Text ="NS: " + row["DobStr"].ToString();
            txtAge.Text = Utility.GetAge(row["DobStr"].ToString()).ToString() + " tuổi";
            txtIdentityCard.Text ="CMND: " +  row["IdentityCard"].ToString();
            txtWorkPhone.Text = "ĐT: "  + row["WorkPhone"].ToString();
            txtMobile.Text = "DĐ: " + row["Mobile"].ToString();
            txtEmail.Text ="Email: " + row["Email"].ToString();
            txtFullAddress.Text ="Địa chỉ: " + row["Address"].ToString();
            txtThuocDiUng.Text = row["Thuoc_Di_Ung"].ToString();

            btnThemYKienKhachHang.Enabled = Global.AllowAddYKienKhachHang;
            
            pageDailyService.Visible = Global.AllowViewDichVuDaSuDung;
            pageServiceHistory.Visible = Global.AllowViewDichVuDaSuDung;
            pageKeToa.Visible = Global.AllowViewKeToa;
            pageChiDinh.Visible = Global.AllowViewChiDinh;
            pageCanDo.Visible = Global.AllowViewCanDo;
            pageKhamLamSang.Visible = Global.AllowViewKhamLamSang;
            pageCanLamSang.Visible = Global.AllowViewCanLamSang;
            pageLoiKhuyen.Visible = Global.AllowViewLoiKhuyen;
            pageKetLuan.Visible = Global.AllowViewKetLuan;
            pageKhamNoiSoi.Visible = Global.AllowViewKhamNoiSoi;
            pageKhamCTC.Visible = Global.AllowViewKhamCTC;
            pageKetQuaSieuAm.Visible = Global.AllowViewSieuAm;

            _uToaThuocList.AllowAdd = Global.AllowAddKeToa;
            _uToaThuocList.AllowEdit = Global.AllowEditKeToa;
            _uToaThuocList.AllowDelete = Global.AllowDeleteKeToa;
            _uToaThuocList.AllowPrint = Global.AllowPrintKeToa;

            btnTaoHoSo.Enabled = Global.AllowTaoHoSo;
            btnUploadHoSo.Enabled = Global.AllowUploadHoSo;
            btnTaoMatKhau.Enabled = Global.AllowAddMatKhauHoSo;

            OnRefreshData();
            
        }

        private void GetNgayLienHeBenhNhanGanNhat(string patientGUID)
        {
            Result result = YKienKhachHangBus.GetNgayLienHeBenhNhanGanNhat(patientGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    DateTime dt = Convert.ToDateTime(result.QueryResult);
                    txtNgayLienHeGanNhat.Text = dt.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                    txtNgayLienHeGanNhat.Text = string.Empty;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("YKienKhachHangBus.GetNgayLienHeBenhNhanGanNhat"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.GetCompanyMemberList"));
            }
        }

        private void OnRefreshData()
        {
            DisplayCheckListAsThread();

            DataRow row = _patientRow as DataRow;
            GetNgayLienHeBenhNhanGanNhat(row["PatientGUID"].ToString());

            if (tabServiceHistory.SelectedTabIndex == 0)
                _uDailyServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 1)
                _uServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 2)
                _uToaThuocList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 3)
                _uChiDinhList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 4)
                _uCanDoList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 5)
                _uKetQuaLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 6)
                _uKetQuaCanLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 7)
                _uLoiKhuyenList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 8)
                _uKetLuanList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 9)
                _uKetQuaNoiSoiList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 10)
                _uKetQuaSoiCTCList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 11)
                _uKetQuaSieuAmList.DisplayAsThread();
        }

        public void DisplayCheckListAsThread()
        {
            try
            {
                lvService.Items.Clear();
                string patientGUID = (_patientRow as DataRow)["PatientGUID"].ToString();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCheckListProc), patientGUID);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayCheckList(string patientGUID)
        {
            Result result = CompanyContractBus.GetCheckListByPatient(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    lvService.Visible = dt.Rows.Count > 0 ? true : false;

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["Code"].ToString();
                        string name = row["Name"].ToString();
                        string nguoiNhanCN = row["NguoiChuyenNhuong"].ToString();
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        int imgIndex = isChecked ? 0 : 1;

                        ListViewItem item = new ListViewItem(string.Empty, imgIndex);
                        item.SubItems.Add(code);
                        item.SubItems.Add(name);
                        item.SubItems.Add(nguoiNhanCN);
                        lvService.Items.Add(item);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                lvService.Visible = false;
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }

        private void OnXemHoSo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataRow row = _patientRow as DataRow;
                string maBenhNhan = row["FileNum"].ToString();
                string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MM.MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnTaoHoSo()
        {
            if (MsgBox.Question(Application.ProductName, "Bạn có muốn tạo hồ sơ bệnh nhân này ?") == DialogResult.Yes)
            {
                DataRow row = _patientRow as DataRow;
                if (OnClearHoSo(row))
                {
                    TaoHoSoAsThread(row);
                }
            }
        }

        private bool OnClearHoSo(DataRow row)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string maBenhNhan = row["FileNum"].ToString();
                string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
                return false;
            }
        }

        private void TaoHoSoAsThread(DataRow row)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnTaoHoSoProc), row);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private bool OnTaoHoSo(DataRow row)
        {
            string patientGUID = row["PatientGUID"].ToString();

            Result result = ReportBus.GetNgayKhamCuoiCung(patientGUID);
            if (!result.IsOK)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetNgayKhamCuoiCung"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetNgayKhamCuoiCung"));
                return false;
            }

            List<DateTime> ngayKhamCuoiCungList = (List<DateTime>)result.QueryResult;

            string maBenhNhan = row["FileNum"].ToString();
            string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
            string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string ketQuaKhamTongQuatFileName = string.Format("{0}\\Temp\\KetQuaKhamSucKhoeTongQuat.xls", Application.StartupPath);//string.Format("{0}\\KetQuaKhamSucKhoeTongQuat_{1}.xls", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
            if (!Exports.ExportExcel.ExportKetQuaKhamTongQuatToExcel(ketQuaKhamTongQuatFileName, row, ngayKhamCuoiCungList))
                return false;

            string pdfFileName = string.Format("{0}\\KetQuaKhamSucKhoeTongQuat_{1}.pdf", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
            if (!Exports.ConvertExcelToPDF.Convert(ketQuaKhamTongQuatFileName, pdfFileName, Utility.GetPageSetup(Const.KhamSucKhoeTongQuatTemplate)))
                return false;

            //Kết quả nội soi
            if (ngayKhamCuoiCungList[5] != Global.MinDateTime)
            {
                DateTime fromDate = new DateTime(ngayKhamCuoiCungList[5].Year, ngayKhamCuoiCungList[5].Month, ngayKhamCuoiCungList[5].Day, 0, 0, 0);
                DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                result = KetQuaNoiSoiBus.GetKetQuaNoiSoiList2(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList2"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList2"));
                    return false;
                }

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LoaiNoiSoi loaiNoiSoi = (LoaiNoiSoi)Convert.ToInt32(dr["LoaiNoiSoi"]);
                        string ketQuaNoiSoiFileName = string.Format("{0}\\Temp\\KetQuaNoiSoi.xls", Application.StartupPath);
                        //string.Format("{0}\\KetQuaNoiSoi_{1}_{2}.xls", path, loaiNoiSoi.ToString(), DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                        PageSetup p = null;

                        switch (loaiNoiSoi)
                        {
                            case LoaiNoiSoi.Tai:
                                if (!Exports.ExportExcel.ExportKetQuaNoiSoiTaiToExcel(ketQuaNoiSoiFileName, row, dr))
                                    return false;
                                p = Utility.GetPageSetup(Const.KetQuaNoiSoiTaiTemplate);
                                break;
                            case LoaiNoiSoi.Mui:
                                if (!Exports.ExportExcel.ExportKetQuaNoiSoiMuiToExcel(ketQuaNoiSoiFileName, row, dr))
                                    return false;
                                p = Utility.GetPageSetup(Const.KetQuaNoiSoiMuiTemplate);
                                break;
                            case LoaiNoiSoi.Hong_ThanhQuan:
                                if (!Exports.ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(ketQuaNoiSoiFileName, row, dr))
                                    return false;
                                p = Utility.GetPageSetup(Const.KetQuaNoiSoiHongThanhQuanTemplate);
                                break;
                            case LoaiNoiSoi.TaiMuiHong:
                                if (!Exports.ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(ketQuaNoiSoiFileName, row, dr))
                                    return false;
                                p = Utility.GetPageSetup(Const.KetQuaNoiSoiTaiMuiHongTemplate);
                                break;
                            case LoaiNoiSoi.TongQuat:
                                if (!Exports.ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(ketQuaNoiSoiFileName, row, dr))
                                    return false;
                                p = Utility.GetPageSetup(Const.KetQuaNoiSoiTongQuatTemplate);
                                break;
                        }

                        pdfFileName = string.Format("{0}\\KetQuaNoiSoi_{1}_{2}.xls", path, loaiNoiSoi.ToString(), DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                        if (!Exports.ConvertExcelToPDF.Convert(ketQuaNoiSoiFileName, pdfFileName, p))
                            return false;
                    }
                }
            }

            //Kết quả soi CTC
            if (ngayKhamCuoiCungList[6] != Global.MinDateTime)
            {
                DateTime fromDate = new DateTime(ngayKhamCuoiCungList[6].Year, ngayKhamCuoiCungList[6].Month, ngayKhamCuoiCungList[6].Day, 0, 0, 0);
                DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                result = KetQuaSoiCTCBus.GetKetQuaSoiCTCList2(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList2"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList2"));
                    return false;
                }

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string ketQuaSoiCTCFileName = string.Format("{0}\\Temp\\KetQuaSoiCTC.xls", Application.StartupPath);
                        //string.Format("{0}\\KetQuaSoiCTC_{1}.xls", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                        if (!Exports.ExportExcel.ExportKetQuaSoiCTCToExcel(ketQuaSoiCTCFileName, row, dr))
                            return false;

                        pdfFileName = string.Format("{0}\\KetQuaSoiCTC_{1}.xls", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                        if (!Exports.ConvertExcelToPDF.Convert(ketQuaSoiCTCFileName, pdfFileName, Utility.GetPageSetup(Const.KetQuaSoiCTCTemplate)))
                            return false;
                    }
                }
            }

            //Kết quả siêu âm
            if (ngayKhamCuoiCungList[7] != Global.MinDateTime)
            {
                DateTime fromDate = new DateTime(ngayKhamCuoiCungList[7].Year, ngayKhamCuoiCungList[7].Month, ngayKhamCuoiCungList[7].Day, 0, 0, 0);
                DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                result = SieuAmBus.GetKetQuaSieuAmList2(patientGUID, fromDate, toDate);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList2"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList2"));
                    return false;
                }

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    MethodInvoker method = delegate
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string tenSieuAm = Utility.ConvertToUnSign(dr["TenSieuAm"].ToString());
                            string ketQuaSieuAmFileName = string.Format("{0}\\KetQuaSieuAm_{1}_{2}.pdf", path, tenSieuAm,
                                DateTime.Now.ToString("ddMMyyyyHHmmssms"));

                            _uPrintKetQuaSieuAm.PatientRow = row;
                            _uPrintKetQuaSieuAm.ExportToPDF(dr, ketQuaSieuAmFileName);

                        }
                    };
                    if (InvokeRequired) BeginInvoke(method);
                    else method.Invoke();
                }
            }

            return true;
        }

        private bool CheckHoSo(DataRow row)
        {
            string maBenhNhan = row["FileNum"].ToString();
            string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
            string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
            if (!Directory.Exists(path))
            {
                MsgBox.Show(Application.ProductName, string.Format("Bệnh nhân: '{0}' chưa có hồ sơ để upload. Vui lòng kiểm tra lại.", row["FullName"].ToString()),
                    IconType.Information);
                return false;
            }

            string[] files = Directory.GetFiles(path);
            if (files == null || files.Length <= 0)
            {
                MsgBox.Show(Application.ProductName, string.Format("Bệnh nhân: '{0}' chưa có hồ sơ để upload. Vui lòng kiểm tra lại.", row["FullName"].ToString()),
                    IconType.Information);
                return false;
            }

            return true;
        }

        private void OnUploadHoSoAsThread(DataRow row)
        {
            try
            {
                if (!CheckHoSo(row)) return;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnUploadHoSoProc), row);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnUploadHoSo(DataRow row)
        {
            string maBenhNhan = row["FileNum"].ToString();
            string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
            string tenBenhNhan2 = Utility.ConvertToUnSign2(row["FullName"].ToString());
            string password = Utility.GeneratePassword(5);
            string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
            if (!Directory.Exists(path)) return;
            string[] files = Directory.GetFiles(path);
            if (files == null || files.Length <= 0) return;

            Result result = UserBus.GetUser(maBenhNhan);
            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetUser"), IconType.Information);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetUser"));
                return;
            }

            if (result.Error.Code == ErrorCode.NOT_EXIST)
            {
                result = UserBus.AddUser(maBenhNhan, password);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                    return;
                }
            }
            else
            {
                password = (result.QueryResult as User).Password;
            }

            result = MySQLHelper.InsertUser(maBenhNhan, password, tenBenhNhan2);
            if (!result.IsOK)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.InsertUser"), IconType.Information);
                Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.InsertUser"));
                return;
            }

            foreach (string file in files)
            {
                string remoteFileName = string.Format("{0}/{1}/{2}", Global.FTPFolder, maBenhNhan, Path.GetFileName(file));
                result = FTP.UploadFile(Global.FTPConnectionInfo, file, remoteFileName);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("FTP.UploadFile"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("FTP.UploadFile"));
                }
            }
        }

        private void OnTaoMatKhauAsThread(DataRow row)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnTaoMatKhauProc), row);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnTaoMatKhau(DataRow row)
        {
            string fileNum = row["FileNum"].ToString();
            Result result = UserBus.GetUser(fileNum);
            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetUser"), IconType.Information);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetUser"));
                return;
            }

            if (result.Error.Code == ErrorCode.NOT_EXIST)
            {
                string password = Utility.GeneratePassword(5);
                result = UserBus.AddUser(fileNum, password);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                    return;
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnThemYKienKhachHang_Click(object sender, EventArgs e)
        {
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang(_patientRow);
            dlg.ShowDialog(this);
        }

        private void _uServiceHistory_OnServiceHistoryChanged()
        {
            _uServiceHistory.DisplayAsThread();
            _uDailyServiceHistory.DisplayAsThread();
            DisplayCheckListAsThread();
        }

        private void txtThuocDiUng_DoubleClick(object sender, EventArgs e)
        {
            dlgPatientHistory dlg = new dlgPatientHistory((DataRow)_patientRow);
            dlg.ShowDialog(this);
        }

        private void tabServiceHistory_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabServiceHistory.SelectedTabIndex == 0)
                _uDailyServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 1)
                _uServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 2)
                _uToaThuocList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 3)
                _uChiDinhList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 4)
                _uCanDoList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 5)
                _uKetQuaLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 6)
                _uKetQuaCanLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 7)
                _uLoiKhuyenList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 8)
                _uKetLuanList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 9)
                _uKetQuaNoiSoiList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 10)
                _uKetQuaSoiCTCList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 11)
                _uKetQuaSieuAmList.DisplayAsThread();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshData();
        }

        private void btnTaoHoSo_Click(object sender, EventArgs e)
        {
            OnTaoHoSo();
        }

        private void btnXemHoSo_Click(object sender, EventArgs e)
        {
            OnXemHoSo();
        }

        private void btnUploadHoSo_Click(object sender, EventArgs e)
        {
            if (MsgBox.Question(Application.ProductName, "Bạn có muốn upload hồ sơ ?") == DialogResult.Yes)
            {
                DataRow row = _patientRow as DataRow;
                OnUploadHoSoAsThread(row);
            }
        }

        private void btnTaoMatKhau_Click(object sender, EventArgs e)
        {
            if (MsgBox.Question(Application.ProductName, "Bạn có muốn tạo mật khẩu hồ sơ cho bệnh nhân này ?") == DialogResult.Yes)
            {
                DataRow row = _patientRow as DataRow;
                OnTaoMatKhauAsThread(row);
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayCheckListProc(object state)
        {
            try
            {
                OnDisplayCheckList(state.ToString());
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnTaoHoSoProc(object state)
        {
            try
            {
                DataRow row = (DataRow)state;
                if (OnTaoHoSo(row))
                {
                    if (MsgBox.Question(Application.ProductName, "Bạn có muốn upload hồ sơ ?") == DialogResult.Yes)
                        OnUploadHoSo(row);
                }
            }
            catch (Exception e)
            {
                base.HideWaiting();
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnUploadHoSoProc(object state)
        {
            try
            {
                DataRow row = (DataRow)state;
                OnUploadHoSo(row);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                this.HideWaiting();
            }
        }

        private void OnTaoMatKhauProc(object state)
        {
            try
            {
                DataRow row = (DataRow)state;
                OnTaoMatKhau(row);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                this.HideWaiting();
            }
        }
        #endregion

        

        
    }
}
