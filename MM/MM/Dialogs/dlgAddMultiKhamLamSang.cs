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
    public partial class dlgAddMultiKhamLamSang : dlgBase
    {
        #region Members
        private string _patientGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddMultiKhamLamSang(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }
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
                DataTable dt = result.QueryResult as DataTable;
                cboDocStaff_TaiMuiHong.DataSource = dt;
                cboDocStaff_Mat.DataSource = dt.Copy();
                cboDocStaff_RangHamMat.DataSource = dt.Copy();
                cboDocStaff_HoHap.DataSource = dt.Copy();
                cboDocStaff_TimMach.DataSource = dt.Copy();
                cboDocStaff_TieuHoa.DataSource = dt.Copy();
                cboDocStaff_TietNieuSinhDuc.DataSource = dt.Copy();
                cboDocStaff_CoXuongKhop.DataSource = dt.Copy();
                cboDocStaff_DaLieu.DataSource = dt.Copy();
                cboDocStaff_ThanKinh.DataSource = dt.Copy();
                cboDocStaff_NoiTiet.DataSource = dt.Copy();
                cboDocStaff_CoQuanKhac.DataSource = dt.Copy();
                cboDocStaff_KhamPhuKhoa.DataSource = dt.Copy();
            }

            if (Global.StaffType == StaffType.BacSi)
            {
                cboDocStaff_TaiMuiHong.SelectedValue = Global.UserGUID;
                cboDocStaff_Mat.SelectedValue = Global.UserGUID;
                cboDocStaff_RangHamMat.SelectedValue = Global.UserGUID;
                cboDocStaff_HoHap.SelectedValue = Global.UserGUID;
                cboDocStaff_TimMach.SelectedValue = Global.UserGUID;
                cboDocStaff_TieuHoa.SelectedValue = Global.UserGUID;
                cboDocStaff_TietNieuSinhDuc.SelectedValue = Global.UserGUID;
                cboDocStaff_CoXuongKhop.SelectedValue = Global.UserGUID;
                cboDocStaff_DaLieu.SelectedValue = Global.UserGUID;
                cboDocStaff_ThanKinh.SelectedValue = Global.UserGUID;
                cboDocStaff_NoiTiet.SelectedValue = Global.UserGUID;
                cboDocStaff_CoQuanKhac.SelectedValue = Global.UserGUID;
                cboDocStaff_KhamPhuKhoa.SelectedValue = Global.UserGUID;
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff_TaiMuiHong.Text.Trim() == string.Empty && chkTaiMuiHong.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_TaiMuiHong.Focus();
                return false;
            }

            if (cboDocStaff_RangHamMat.Text.Trim() == string.Empty && chkRangHamMat.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_RangHamMat.Focus();
                return false;
            }

            if (cboDocStaff_Mat.Text.Trim() == string.Empty && chkMat.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_Mat.Focus();
                return false;
            }

            if (cboDocStaff_HoHap.Text.Trim() == string.Empty && chkHoHap.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_HoHap.Focus();
                return false;
            }

            if (cboDocStaff_TimMach.Text.Trim() == string.Empty && chkTimMach.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_TimMach.Focus();
                return false;
            }

            if (cboDocStaff_TieuHoa.Text.Trim() == string.Empty && chkTieuHoa.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_TieuHoa.Focus();
                return false;
            }

            if (cboDocStaff_TietNieuSinhDuc.Text.Trim() == string.Empty && chkTietNieuSinhDuc.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_TietNieuSinhDuc.Focus();
                return false;
            }

            if (cboDocStaff_CoXuongKhop.Text.Trim() == string.Empty && chkCoXuongKhop.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_CoXuongKhop.Focus();
                return false;
            }

            if (cboDocStaff_DaLieu.Text.Trim() == string.Empty && chkDaLieu.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_DaLieu.Focus();
                return false;
            }

            if (cboDocStaff_ThanKinh.Text.Trim() == string.Empty && chkThanKinh.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_ThanKinh.Focus();
                return false;
            }

            if (cboDocStaff_NoiTiet.Text.Trim() == string.Empty && chkNoiTiet.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_NoiTiet.Focus();
                return false;
            }

            if (cboDocStaff_CoQuanKhac.Text.Trim() == string.Empty && chkCacCoQuanKhac.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_CoQuanKhac.Focus();
                return false;
            }

            if (cboDocStaff_KhamPhuKhoa.Text.Trim() == string.Empty && chkKhamPhuKhoa.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff_KhamPhuKhoa.Focus();
                return false;
            }

            if (!chkMat.Checked && !chkTaiMuiHong.Checked && !chkRangHamMat.Checked && !chkHoHap.Checked && !chkTimMach.Checked && !chkTieuHoa.Checked &&
                !chkTietNieuSinhDuc.Checked && !chkCoXuongKhop.Checked && !chkDaLieu.Checked && !chkThanKinh.Checked && !chkNoiTiet.Checked &&
                !chkCacCoQuanKhac.Checked && !chkKhamPhuKhoa.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 ít nhất cơ quan để khám.", IconType.Information);
                chkTaiMuiHong.Focus();
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
                MethodInvoker method = delegate
                {
                    List<KetQuaLamSang> ketQuaLamSangList = new List<KetQuaLamSang>();

                    if (chkMat.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.Mat;
                        kq.Normal = chkNormal_Mat.Checked;
                        kq.Abnormal = chkAbnormal_Mat.Checked;
                        kq.Note = txtNhanXet_Mat.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_Mat.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkTaiMuiHong.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.TaiMuiHong;
                        kq.Normal = chkNormal_TaiMuiHong.Checked;
                        kq.Abnormal = chkAbnormal_TaiMuiHong.Checked;
                        kq.Note = txtNhanXet_TaiMuiHong.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_TaiMuiHong.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkRangHamMat.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.RangHamMat;
                        kq.Normal = chkNormal_RangHamMat.Checked;
                        kq.Abnormal = chkAbnormal_RangHamMat.Checked;
                        kq.Note = txtNhanXet_RangHamMat.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_RangHamMat.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkHoHap.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.HoHap;
                        kq.Normal = chkNormal_HoHap.Checked;
                        kq.Abnormal = chkAbnormal_HoHap.Checked;
                        kq.Note = txtNhanXet_HoHap.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_HoHap.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkTimMach.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.TimMach;
                        kq.Normal = chkNormal_TimMach.Checked;
                        kq.Abnormal = chkAbnormal_TimMach.Checked;
                        kq.Note = txtNhanXet_TimMach.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_TimMach.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkTieuHoa.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.TieuHoa;
                        kq.Normal = chkNormal_TieuHoa.Checked;
                        kq.Abnormal = chkAbnormal_TieuHoa.Checked;
                        kq.Note = txtNhanXet_TieuHoa.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_TieuHoa.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkTietNieuSinhDuc.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.TietNieuSinhDuc;
                        kq.Normal = chkNormal_TietNieuSinhDuc.Checked;
                        kq.Abnormal = chkAbnormal_TietNieuSinhDuc.Checked;
                        kq.Note = txtNhanXet_TietNieuSinhDuc.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_TietNieuSinhDuc.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkCoXuongKhop.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.CoXuongKhop;
                        kq.Normal = chkNormal_CoXuongKhop.Checked;
                        kq.Abnormal = chkAbnormal_CoXuongKhop.Checked;
                        kq.Note = txtNhanXet_CoXuongKhop.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_CoXuongKhop.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkDaLieu.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.DaLieu;
                        kq.Normal = chkNormal_DaLieu.Checked;
                        kq.Abnormal = chkAbnormal_DaLieu.Checked;
                        kq.Note = txtNhanXet_DaLieu.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_DaLieu.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkThanKinh.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.ThanKinh;
                        kq.Normal = chkNormal_ThanKinh.Checked;
                        kq.Abnormal = chkAbnormal_ThanKinh.Checked;
                        kq.Note = txtNhanXet_ThanKinh.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_ThanKinh.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkNoiTiet.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.NoiTiet;
                        kq.Normal = chkNormal_NoiTiet.Checked;
                        kq.Abnormal = chkAbnormal_NoiTiet.Checked;
                        kq.Note = txtNhanXet_NoiTiet.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_NoiTiet.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkCacCoQuanKhac.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.Khac;
                        kq.Normal = false;
                        kq.Abnormal = false;
                        kq.Note = txtNhanXet_CoQuanKhac.Text;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_CoQuanKhac.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }
                    else if (chkKhamPhuKhoa.Checked)
                    {
                        KetQuaLamSang kq = new KetQuaLamSang();
                        kq.CreatedDate = DateTime.Now;
                        kq.CreatedBy = Guid.Parse(Global.UserGUID);
                        kq.PatientGUID = Guid.Parse(_patientGUID);
                        kq.NgayKham = dtpkNgay.Value;
                        kq.CoQuan = (byte)CoQuan.KhamPhuKhoa;
                        kq.PARA = txtPARA.Text;
                        kq.NgayKinhChot = dtpkNgayKinhChot.Value;
                        kq.Note = txtKetQuaKhamPhuKhoa.Text;
                        kq.Normal = chkNormal_KhamPhuKhoa.Checked;
                        kq.Abnormal = chkAbnormal_KhamPhuKhoa.Checked;
                        kq.DocStaffGUID = Guid.Parse(cboDocStaff_KhamPhuKhoa.SelectedValue.ToString());
                        ketQuaLamSangList.Add(kq);
                    }

                    Result result = KetQuaLamSangBus.InsertKetQuaLamSang(ketQuaLamSangList);
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
            if (!chkNormal_KhamPhuKhoa.Checked && !chkAbnormal_KhamPhuKhoa.Checked)
                chkNormal_KhamPhuKhoa.Checked = true;

            if (chkNormal_KhamPhuKhoa.Checked) chkAbnormal_KhamPhuKhoa.Checked = false;
        }

        private void chkAbnormal_KhamPhuKhoa_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal_KhamPhuKhoa.Checked && !chkNormal_KhamPhuKhoa.Checked)
                chkAbnormal_KhamPhuKhoa.Checked = true;

            if (chkAbnormal_KhamPhuKhoa.Checked) chkNormal_KhamPhuKhoa.Checked = false;
        }

        private void chkTaiMuiHong_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TaiMuiHong.Enabled = chkTaiMuiHong.Checked;
            chkAbnormal_TaiMuiHong.Enabled = chkTaiMuiHong.Checked;
            txtNhanXet_TaiMuiHong.ReadOnly = !chkTaiMuiHong.Checked;
            cboDocStaff_TaiMuiHong.Enabled = chkTaiMuiHong.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkRangHamMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_RangHamMat.Enabled = chkRangHamMat.Checked;
            chkAbnormal_RangHamMat.Enabled = chkRangHamMat.Checked;
            txtNhanXet_RangHamMat.ReadOnly = !chkRangHamMat.Checked;
            cboDocStaff_RangHamMat.Enabled = chkRangHamMat.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkMat_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_Mat.Enabled = chkMat.Checked;
            chkAbnormal_Mat.Enabled = chkMat.Checked;
            txtNhanXet_Mat.ReadOnly = !chkMat.Checked;
            cboDocStaff_Mat.Enabled = chkMat.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkHoHap_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_HoHap.Enabled = chkHoHap.Checked;
            chkAbnormal_HoHap.Enabled = chkHoHap.Checked;
            txtNhanXet_HoHap.ReadOnly = !chkHoHap.Checked;
            cboDocStaff_HoHap.Enabled = chkHoHap.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkTimMach_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TimMach.Enabled = chkTimMach.Checked;
            chkAbnormal_TimMach.Enabled = chkTimMach.Checked;
            txtNhanXet_TimMach.ReadOnly = !chkTimMach.Checked;
            cboDocStaff_TimMach.Enabled = chkTimMach.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkTieuHoa_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TieuHoa.Enabled = chkTieuHoa.Checked;
            chkAbnormal_TieuHoa.Enabled = chkTieuHoa.Checked;
            txtNhanXet_TieuHoa.ReadOnly = !chkTieuHoa.Checked;
            cboDocStaff_TieuHoa.Enabled = chkTieuHoa.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkTietNieuSinhDuc_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_TietNieuSinhDuc.Enabled = chkTietNieuSinhDuc.Checked;
            chkAbnormal_TietNieuSinhDuc.Enabled = chkTietNieuSinhDuc.Checked;
            txtNhanXet_TietNieuSinhDuc.ReadOnly = !chkTietNieuSinhDuc.Checked;
            cboDocStaff_TietNieuSinhDuc.Enabled = chkTietNieuSinhDuc.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkCoXuongKhop_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_CoXuongKhop.Enabled = chkCoXuongKhop.Checked;
            chkAbnormal_CoXuongKhop.Enabled = chkCoXuongKhop.Checked;
            txtNhanXet_CoXuongKhop.ReadOnly = !chkCoXuongKhop.Checked;
            cboDocStaff_CoXuongKhop.Enabled = chkCoXuongKhop.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkDaLieu_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_DaLieu.Enabled = chkDaLieu.Checked;
            chkAbnormal_DaLieu.Enabled = chkDaLieu.Checked;
            txtNhanXet_DaLieu.ReadOnly = !chkDaLieu.Checked;
            cboDocStaff_DaLieu.Enabled = chkDaLieu.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkThanKinh_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_ThanKinh.Enabled = chkThanKinh.Checked;
            chkAbnormal_ThanKinh.Enabled = chkThanKinh.Checked;
            txtNhanXet_ThanKinh.ReadOnly = !chkThanKinh.Checked;
            cboDocStaff_ThanKinh.Enabled = chkThanKinh.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkNoiTiet_CheckedChanged(object sender, EventArgs e)
        {
            chkNormal_NoiTiet.Enabled = chkNoiTiet.Checked;
            chkAbnormal_NoiTiet.Enabled = chkNoiTiet.Checked;
            txtNhanXet_NoiTiet.ReadOnly = !chkNoiTiet.Checked;
            cboDocStaff_NoiTiet.Enabled = chkNoiTiet.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkCacCoQuanKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtNhanXet_CoQuanKhac.ReadOnly = !chkCacCoQuanKhac.Checked;
            cboDocStaff_CoQuanKhac.Enabled = chkCacCoQuanKhac.Checked && Global.StaffType != StaffType.BacSi;
        }

        private void chkKhamPhuKhoa_CheckedChanged(object sender, EventArgs e)
        {
            txtPARA.ReadOnly = !chkKhamPhuKhoa.Checked;
            dtpkNgayKinhChot.Enabled = chkKhamPhuKhoa.Checked;
            txtKetQuaKhamPhuKhoa.ReadOnly = !chkKhamPhuKhoa.Checked;
            txtSoiTuoiHuyetTrang.ReadOnly = !chkKhamPhuKhoa.Checked;
            chkNormal_KhamPhuKhoa.Enabled = chkKhamPhuKhoa.Checked;
            chkAbnormal_KhamPhuKhoa.Enabled = chkKhamPhuKhoa.Checked;
            cboDocStaff_KhamPhuKhoa.Enabled = chkKhamPhuKhoa.Checked && Global.StaffType != StaffType.BacSi;
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
