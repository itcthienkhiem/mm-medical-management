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
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddPatient : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Contact _contact = new Contact();
        private Patient _patient = new Patient();
        private PatientHistory _patientHistory = new PatientHistory();
        #endregion

        #region Constructor
        public dlgAddPatient()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddPatient(DataRow drPatient)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            this.Text = "Sua benh nhan";
            DisplayInfo(drPatient);
        }
        #endregion

        #region Properties
        public Contact Contact
        {
            get { return _contact; }
        }

        public Patient Patient
        {
            get { return _patient; }
        }

        public PatientHistory PatientHistory
        {
            get { return _patientHistory; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            cboGender.SelectedIndex = 0;
            tabPatient.SelectedTabIndex = 0;
        }

        private bool CheckInfo()
        {
            if (txtFileNum.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã bệnh nhân.");
                txtFileNum.Focus();
                return false;
            }

            if (txtFullName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập họ.");
                txtFullName.Focus();
                return false;
            }

            if (txtDOB.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng ngày sinh hoặc năm sinh.");
                txtDOB.Focus();
                return false;
            }

            if (!Utility.isValidDOB(txtDOB.Text))
            {
                MsgBox.Show(this.Text, "Ngày sinh hoặc năm sinh chưa đúng. Vui lòng nhập lại");
                txtDOB.Focus();
                return false;
            }

            string patientGUID = _isNew ? string.Empty : _patient.PatientGUID.ToString();
            Result result = PatientBus.CheckPatientExistFileNum(patientGUID, txtFileNum.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã bệnh nhân này đã tồn tại rồi. Vui lòng nhập mã khác.");
                    txtFileNum.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.CheckPatientExistFileNum"));
                return false;
            }

            if (chkDiUngThuoc.Checked && txtThuocDiUng.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập thuốc dị ứng.");
                tabPatient.SelectedTabIndex = 1;
                txtThuocDiUng.Focus();
                return false;
            }

            if (chkUngThu.Checked && txtCoQuanUngThu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập cơ quan ung thư.");
                tabPatient.SelectedTabIndex = 1;
                txtCoQuanUngThu.Focus();
                return false;
            }

            if (chkBenhKhac.Checked)
            {
                if (txtBenhGi.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập tên bệnh.");
                    tabPatient.SelectedTabIndex = 1;
                    txtBenhGi.Focus();
                    return false;
                }

                if (txtThuocDangDung.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập thuốc đang dùng.");
                    tabPatient.SelectedTabIndex = 1;
                    txtThuocDangDung.Focus();
                    return false;
                }
            }

            return true;
        }

        private void DisplayInfo(DataRow drPatient)
        {
            try
            {
                txtFileNum.Text = drPatient["FileNum"] as string;
                txtFullName.Text = drPatient["FullName"] as string;
                txtKnownAs.Text = drPatient["KnownAs"] as string;
                txtPreferredName.Text = drPatient["PreferredName"] as string;
                cboGender.SelectedIndex = Convert.ToInt32(drPatient["Gender"]);
                txtDOB.Text = drPatient["DobStr"] as string;
                txtIdentityCard.Text = drPatient["IdentityCard"] as string;
                txtOccupation.Text = drPatient["Occupation"] as string;
                txtHomePhone.Text = drPatient["HomePhone"] as string;
                txtWorkPhone.Text = drPatient["WorkPhone"] as string;
                txtMobile.Text = drPatient["Mobile"] as string;
                txtEmail.Text = drPatient["Email"] as string;
                txtFax.Text = drPatient["Fax"] as string;
                txtAddress.Text = drPatient["Address"] as string;

                _contact.ContactGUID = Guid.Parse(drPatient["ContactGUID"].ToString());
                _patient.PatientGUID = Guid.Parse(drPatient["PatientGUID"].ToString());
                _patient.ContactGUID = _contact.ContactGUID;

                if (drPatient["CreatedDate"] != null && drPatient["CreatedDate"] != DBNull.Value)
                    _contact.CreatedDate = Convert.ToDateTime(drPatient["CreatedDate"]);

                if (drPatient["CreatedBy"] != null && drPatient["CreatedBy"] != DBNull.Value)
                    _contact.CreatedBy = Guid.Parse(drPatient["CreatedBy"].ToString());

                if (drPatient["UpdatedDate"] != null && drPatient["UpdatedDate"] != DBNull.Value)
                    _contact.UpdatedDate = Convert.ToDateTime(drPatient["UpdatedDate"]);

                if (drPatient["UpdatedBy"] != null && drPatient["UpdatedBy"] != DBNull.Value)
                    _contact.UpdatedBy = Guid.Parse(drPatient["UpdatedBy"].ToString());

                if (drPatient["DeletedDate"] != null && drPatient["DeletedDate"] != DBNull.Value)
                    _contact.DeletedDate = Convert.ToDateTime(drPatient["DeletedDate"]);

                if (drPatient["DeletedBy"] != null && drPatient["DeletedBy"] != DBNull.Value)
                    _contact.DeletedBy = Guid.Parse(drPatient["DeletedBy"].ToString());

                //Patient History
                if (drPatient["PatientHistoryGUID"] != null && drPatient["PatientHistoryGUID"] != DBNull.Value)
                    _patientHistory.PatientHistoryGUID = Guid.Parse(drPatient["PatientHistoryGUID"].ToString());

                if (drPatient["Di_Ung_Thuoc"] != null && drPatient["Di_Ung_Thuoc"] != DBNull.Value)
                    chkDiUngThuoc.Checked = Convert.ToBoolean(drPatient["Di_Ung_Thuoc"]);

                if (drPatient["Thuoc_Di_Ung"] != null && drPatient["Thuoc_Di_Ung"] != DBNull.Value)
                    txtThuocDiUng.Text = drPatient["Thuoc_Di_Ung"].ToString();

                if (drPatient["Dot_Quy"] != null && drPatient["Dot_Quy"] != DBNull.Value)
                    chkDotQuy.Checked = Convert.ToBoolean(drPatient["Dot_Quy"]);

                if (drPatient["Benh_Tim_Mach"] != null && drPatient["Benh_Tim_Mach"] != DBNull.Value)
                    chkBenhTimMach.Checked = Convert.ToBoolean(drPatient["Benh_Tim_Mach"]);

                if (drPatient["Benh_Lao"] != null && drPatient["Benh_Lao"] != DBNull.Value)
                    chkBenhLao.Checked = Convert.ToBoolean(drPatient["Benh_Lao"]);

                if (drPatient["Dai_Thao_Duong"] != null && drPatient["Dai_Thao_Duong"] != DBNull.Value)
                    chkDaiThaoDuong.Checked = Convert.ToBoolean(drPatient["Dai_Thao_Duong"]);

                if (drPatient["Dai_Duong_Dang_Dieu_Tri"] != null && drPatient["Dai_Duong_Dang_Dieu_Tri"] != DBNull.Value)
                    chkDaiDuongDangDieuTri.Checked = Convert.ToBoolean(drPatient["Dai_Duong_Dang_Dieu_Tri"]);

                if (drPatient["Viem_Gan_B"] != null && drPatient["Viem_Gan_B"] != DBNull.Value)
                    chkViemGanB.Checked = Convert.ToBoolean(drPatient["Viem_Gan_B"]);

                if (drPatient["Viem_Gan_C"] != null && drPatient["Viem_Gan_C"] != DBNull.Value)
                    chkViemGanC.Checked = Convert.ToBoolean(drPatient["Viem_Gan_C"]);

                if (drPatient["Viem_Gan_Dang_Dieu_Tri"] != null && drPatient["Viem_Gan_Dang_Dieu_Tri"] != DBNull.Value)
                    chkViemGanDangDieuTri.Checked = Convert.ToBoolean(drPatient["Viem_Gan_Dang_Dieu_Tri"]);

                if (drPatient["Ung_Thu"] != null && drPatient["Ung_Thu"] != DBNull.Value)
                    chkUngThu.Checked = Convert.ToBoolean(drPatient["Ung_Thu"]);

                if (drPatient["Co_Quan_Ung_Thu"] != null && drPatient["Co_Quan_Ung_Thu"] != DBNull.Value)
                    txtCoQuanUngThu.Text = drPatient["Co_Quan_Ung_Thu"].ToString();

                if (drPatient["Dong_Kinh"] != null && drPatient["Dong_Kinh"] != DBNull.Value)
                    chkDongKinh.Checked = Convert.ToBoolean(drPatient["Dong_Kinh"]);

                if (drPatient["Hen_Suyen"] != null && drPatient["Hen_Suyen"] != DBNull.Value)
                    chkHenSuyen.Checked = Convert.ToBoolean(drPatient["Hen_Suyen"]);

                if (drPatient["Benh_Khac"] != null && drPatient["Benh_Khac"] != DBNull.Value)
                    chkBenhKhac.Checked = Convert.ToBoolean(drPatient["Benh_Khac"]);

                if (drPatient["Benh_Gi"] != null && drPatient["Benh_Gi"] != DBNull.Value)
                    txtBenhGi.Text = drPatient["Benh_Gi"].ToString();

                if (drPatient["Thuoc_Dang_Dung"] != null && drPatient["Thuoc_Dang_Dung"] != DBNull.Value)
                    txtThuocDangDung.Text = drPatient["Thuoc_Dang_Dung"].ToString();

                if (drPatient["Hut_Thuoc"] != null && drPatient["Hut_Thuoc"] != DBNull.Value)
                    chkHutThuoc.Checked = Convert.ToBoolean(drPatient["Hut_Thuoc"]);

                if (drPatient["Uong_Ruou"] != null && drPatient["Uong_Ruou"] != DBNull.Value)
                    chkUongRuou.Checked = Convert.ToBoolean(drPatient["Uong_Ruou"]);

                if (drPatient["Tinh_Trang_Gia_Dinh"] != null && drPatient["Tinh_Trang_Gia_Dinh"] != DBNull.Value)
                    txtTinhTrangGiaDinh.Text = drPatient["Tinh_Trang_Gia_Dinh"].ToString();

                if (drPatient["Chich_Ngua_Viem_Gan_B"] != null && drPatient["Chich_Ngua_Viem_Gan_B"] != DBNull.Value)
                    chkChichNguaViemGanB.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Viem_Gan_B"]);

                if (drPatient["Chich_Ngua_Uon_Van"] != null && drPatient["Chich_Ngua_Uon_Van"] != DBNull.Value)
                    chkChichNguaUonVan.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Uon_Van"]);

                if (drPatient["Chich_Ngua_Cum"] != null && drPatient["Chich_Ngua_Cum"] != DBNull.Value)
                    chkChichNguaCum.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Cum"]);

                if (drPatient["Dang_Co_Thai"] != null && drPatient["Dang_Co_Thai"] != DBNull.Value)
                    chkDangCoThai.Checked = Convert.ToBoolean(drPatient["Dang_Co_Thai"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
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
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void SetPatientInfo()
        {
            try
            {
                _contact.FullName = txtFullName.Text;
                string surName = string.Empty;
                string firstName = string.Empty;
                Utility.GetSurNameFirstNameFromFullName(txtFullName.Text, ref surName, ref firstName);
                _contact.SurName = surName;
                _contact.FirstName = firstName;

                _contact.KnownAs = txtKnownAs.Text;
                _contact.PreferredName = txtPreferredName.Text;
                _contact.Archived = true;
                _contact.DobStr = txtDOB.Text;
                _contact.IdentityCard = txtIdentityCard.Text;
                _contact.Occupation = txtOccupation.Text;
                _contact.HomePhone = txtHomePhone.Text;
                _contact.WorkPhone = txtWorkPhone.Text;
                _contact.Mobile = txtMobile.Text;
                _contact.Email = txtEmail.Text;
                _contact.FAX = txtFax.Text;
                _contact.Address = txtAddress.Text;

                _patient.FileNum = txtFileNum.Text;

                if (_isNew)
                {
                    _contact.CreatedBy = Guid.Parse(Global.UserGUID);
                    _contact.CreatedDate = DateTime.Now;
                }
                else
                {
                    _contact.UpdatedBy = Guid.Parse(Global.UserGUID);
                    _contact.UpdatedDate = DateTime.Now;
                }

                MethodInvoker method = delegate
                {
                    _contact.Gender = (byte)cboGender.SelectedIndex;

                    //Patient History
                    _patientHistory.Dot_Quy = chkDotQuy.Checked;
                    _patientHistory.Benh_Tim_Mach = chkBenhTimMach.Checked;
                    _patientHistory.Benh_Lao = chkBenhLao.Checked;
                    _patientHistory.Dai_Thao_Duong = chkDaiThaoDuong.Checked;
                    _patientHistory.Dai_Duong_Dang_Dieu_Tri = chkDaiDuongDangDieuTri.Checked;
                    _patientHistory.Viem_Gan_B = chkViemGanB.Checked;
                    _patientHistory.Viem_Gan_C = chkViemGanC.Checked;
                    _patientHistory.Viem_Gan_Dang_Dieu_Tri = chkViemGanDangDieuTri.Checked;
                    _patientHistory.Dong_Kinh = chkDongKinh.Checked;
                    _patientHistory.Hen_Suyen = chkHenSuyen.Checked;
                    _patientHistory.Hut_Thuoc = chkHutThuoc.Checked;
                    _patientHistory.Uong_Ruou = chkUongRuou.Checked;
                    _patientHistory.Chich_Ngua_Viem_Gan_B = chkChichNguaViemGanB.Checked;
                    _patientHistory.Chich_Ngua_Uon_Van = chkChichNguaUonVan.Checked;
                    _patientHistory.Chich_Ngua_Cum = chkChichNguaCum.Checked;
                    _patientHistory.Dang_Co_Thai = chkDangCoThai.Checked;
                    _patientHistory.Di_Ung_Thuoc = chkDiUngThuoc.Checked;
                    if (chkDiUngThuoc.Checked) 
                        _patientHistory.Thuoc_Di_Ung = txtThuocDiUng.Text;
                    else
                        _patientHistory.Thuoc_Di_Ung = string.Empty;

                    _patientHistory.Ung_Thu = chkUngThu.Checked;
                    if (chkUngThu.Checked) 
                        _patientHistory.Co_Quan_Ung_Thu = txtCoQuanUngThu.Text;
                    else
                        _patientHistory.Co_Quan_Ung_Thu = string.Empty;

                    _patientHistory.Benh_Khac = chkBenhKhac.Checked;
                    if (chkBenhKhac.Checked)
                    {
                        _patientHistory.Benh_Gi = txtBenhGi.Text;
                        _patientHistory.Thuoc_Dang_Dung = txtThuocDangDung.Text;
                    }
                    else
                    {
                        _patientHistory.Benh_Gi = string.Empty;
                        _patientHistory.Thuoc_Dang_Dung = string.Empty;
                    }


                    _patientHistory.Tinh_Trang_Gia_Dinh = txtTinhTrangGiaDinh.Text;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnSaveInfo()
        {
            SetPatientInfo();
            Result result = PatientBus.InsertPatient(_contact, _patient, _patientHistory);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.InsertPatient"));
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.InsertPatient"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void txtIdentityCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void chkDiUngThuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocDiUng.ReadOnly = !chkDiUngThuoc.Checked;
            if (chkDiUngThuoc.Checked)
                txtThuocDiUng.Focus();
        }

        private void chkUngThu_CheckedChanged(object sender, EventArgs e)
        {
            txtCoQuanUngThu.ReadOnly = !chkUngThu.Checked;
            if (chkUngThu.Checked)
                txtCoQuanUngThu.Focus();
        }

        private void chkBenhKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtBenhGi.ReadOnly = !chkBenhKhac.Checked;
            txtThuocDangDung.ReadOnly = !chkBenhKhac.Checked;
            if (chkBenhKhac.Checked)
                txtBenhGi.Focus();
        }

        private void txtFileNum_TextChanged(object sender, EventArgs e)
        {
            barCode.BarCode = txtFileNum.Text;
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
