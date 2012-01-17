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
    public partial class dlgAddDocStaff : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Contact _contact = new Contact();
        private DocStaff _docStaff = new DocStaff();
        #endregion

        #region Constructor
        public dlgAddDocStaff()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddDocStaff(DataRow drDocStaff)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            this.Text = "Sua nhan vien";
            DisplayInfo(drDocStaff);
        }       
        #endregion

        #region Properties
        public Contact Contact
        {
            get { return _contact; }
        }

        public DocStaff DocStaff
        {
            get { return _docStaff; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            cboGender.SelectedIndex = 0;
            cboWorkType.SelectedIndex = 0;
            cboStaffType.SelectedIndex = 0;

            //Load Speciality List
            Result result = SpecialityBus.GetSpecialityList();
            if (result.IsOK)
                cboSpeciality.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SpecialityBus.GetSpecialityList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SpecialityBus.GetSpecialityList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtFullName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập họ.", IconType.Information);
                txtFullName.Focus();
                return false;
            }

            if (txtDOB.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng ngày sinh hoặc năm sinh.", IconType.Information);
                txtDOB.Focus();
                return false;
            }

            if (!Utility.IsValidDOB(txtDOB.Text))
            {
                MsgBox.Show(this.Text, "Ngày sinh hoặc năm sinh chưa đúng. Vui lòng nhập lại", IconType.Information);
                txtDOB.Focus();
                return false;
            }
            
            if (cboSpeciality.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập chuyên khoa.", IconType.Information);
                cboSpeciality.Focus();
                return false;
            }

            if (txtEmail.Text.Trim() != string.Empty && !Utility.IsValidEmail(txtEmail.Text))
            {
                MsgBox.Show(this.Text, "Địa chỉ email không hợp lê.", IconType.Information);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private int GetStaffTypeIndex(StaffType type)
        {
            switch (type)
            {
                case StaffType.BacSi:
                    return 0;
                case StaffType.BacSiSieuAm:
                    return 1;
                case StaffType.BacSiNgoaiTongQuat:
                    return 2;
                case StaffType.BacSiNoiTongQuat:
                    return 3;
                case StaffType.DieuDuong:
                    return 4;
                case StaffType.LeTan:
                    return 5;
                case StaffType.ThuKyYKhoa:
                    return 6;
                case StaffType.XetNghiem:
                    return 7;
                case StaffType.Sale:
                    return 8;
                case StaffType.KeToan:
                    return 9;
                default:
                    return 0;
            }
        }

        private StaffType GetStaffType(int index)
        {
            switch (index)
            {
                case 0:
                    return StaffType.BacSi;
                case 1:
                    return StaffType.BacSiSieuAm;
                case 2:
                    return StaffType.BacSiNgoaiTongQuat;
                case 3:
                    return StaffType.BacSiNoiTongQuat;
                case 4:
                    return StaffType.DieuDuong;
                case 5:
                    return StaffType.LeTan;
                case 6:
                    return StaffType.ThuKyYKhoa;
                case 7:
                    return StaffType.XetNghiem;
                case 8:
                    return StaffType.Sale;
                case 9:
                    return StaffType.KeToan;
                default:
                    return StaffType.BacSi;
            }
        }

        private void DisplayInfo(DataRow drDocStaff)
        {
            try
            {
                txtFullName.Text = drDocStaff["FullName"] as string;
                txtKnownAs.Text = drDocStaff["KnownAs"] as string;
                txtPreferredName.Text = drDocStaff["PreferredName"] as string;
                cboGender.SelectedIndex = Convert.ToInt32(drDocStaff["Gender"]);
                txtDOB.Text = drDocStaff["DobStr"] as string;
                txtIdentityCard.Text = drDocStaff["IdentityCard"] as string;
                txtQualifications.Text = drDocStaff["Qualifications"] as string;
                cboSpeciality.SelectedValue = drDocStaff["SpecialityGUID"];
                cboWorkType.SelectedIndex = Convert.ToInt32(drDocStaff["WorkType"]);
                cboStaffType.SelectedIndex = GetStaffTypeIndex((StaffType)Convert.ToInt32(drDocStaff["StaffType"]));
                txtHomePhone.Text = drDocStaff["HomePhone"] as string;
                txtWorkPhone.Text = drDocStaff["WorkPhone"] as string;
                txtMobile.Text = drDocStaff["Mobile"] as string;
                txtEmail.Text = drDocStaff["Email"] as string;
                txtFax.Text = drDocStaff["Fax"] as string;
                txtAddress.Text = drDocStaff["Address"] as string;

                _contact.ContactGUID = Guid.Parse(drDocStaff["ContactGUID"].ToString());
                _docStaff.DocStaffGUID = Guid.Parse(drDocStaff["DocStaffGUID"].ToString());
                _docStaff.ContactGUID = _contact.ContactGUID;

                if (drDocStaff["CreatedDate"] != null && drDocStaff["CreatedDate"] != DBNull.Value)
                    _contact.CreatedDate = Convert.ToDateTime(drDocStaff["CreatedDate"]);

                if (drDocStaff["CreatedBy"] != null && drDocStaff["CreatedBy"] != DBNull.Value)
                    _contact.CreatedBy = Guid.Parse(drDocStaff["CreatedBy"].ToString());

                if (drDocStaff["UpdatedDate"] != null && drDocStaff["UpdatedDate"] != DBNull.Value)
                    _contact.UpdatedDate = Convert.ToDateTime(drDocStaff["UpdatedDate"]);

                if (drDocStaff["UpdatedBy"] != null && drDocStaff["UpdatedBy"] != DBNull.Value)
                    _contact.UpdatedBy = Guid.Parse(drDocStaff["UpdatedBy"].ToString());

                if (drDocStaff["DeletedDate"] != null && drDocStaff["DeletedDate"] != DBNull.Value)
                    _contact.DeletedDate = Convert.ToDateTime(drDocStaff["DeletedDate"]);

                if (drDocStaff["DeletedBy"] != null && drDocStaff["DeletedBy"] != DBNull.Value)
                    _contact.DeletedBy = Guid.Parse(drDocStaff["DeletedBy"].ToString());
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

        private void OnSaveInfo()
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
                _contact.Archived = false;

                _contact.DobStr = txtDOB.Text;
                _contact.IdentityCard = txtIdentityCard.Text;
                _contact.HomePhone = txtHomePhone.Text;
                _contact.WorkPhone = txtWorkPhone.Text;
                _contact.Mobile = txtMobile.Text;
                _contact.Email = txtEmail.Text;
                _contact.FAX = txtFax.Text;
                _contact.Address = txtAddress.Text;
                _docStaff.Qualifications = txtQualifications.Text;
                _docStaff.AvailableToWork = true;

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
                    _docStaff.SpecialityGUID = Guid.Parse(cboSpeciality.SelectedValue.ToString());
                    _docStaff.WorkType = (byte)cboWorkType.SelectedIndex;
                    _docStaff.StaffType = (byte)GetStaffType(cboStaffType.SelectedIndex);

                    switch (_docStaff.StaffType)
                    {
                        case 0:
                            _contact.Occupation = "Bác sĩ";
                            break;
                        case 1:
                            _contact.Occupation = "Bác sĩ siêu âm";
                            break;
                        case 2:
                            _contact.Occupation = "Bác sĩ ngoại tổng quát";
                            break;
                        case 3:
                            _contact.Occupation = "Bác sĩ nội tổng quát";
                            break;
                        case 4:
                            _contact.Occupation = "Điều dưỡng";
                            break;
                        case 5:
                            _contact.Occupation = "Lễ tân";
                            break;
                        case 6:
                            _contact.Occupation = "Thư ký y khoa";
                            break;
                        case 7:
                            _contact.Occupation = "Xét nghiệm";
                            break;
                        case 8:
                            _contact.Occupation = "Sale";
                            break;
                        case 9:
                            _contact.Occupation = "Kế toán";
                            break;
                    }

                    _contact.Gender = (byte)cboGender.SelectedIndex;

                    Result result = DocStaffBus.InsertDocStaff(_contact, _docStaff);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.InsertDocStaff"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.InsertDocStaff"));
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
        private void dlgAddDocStaff_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhân viên ?") == System.Windows.Forms.DialogResult.Yes)
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
