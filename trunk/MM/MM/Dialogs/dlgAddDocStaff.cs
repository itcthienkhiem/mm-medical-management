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
            _isNew = false;
            this.Text = "Sửa bác sĩ";
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
                MsgBox.Show(this.Text, result.GetErrorAsString("SpecialityBus.GetSpecialityList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("SpecialityBus.GetSpecialityList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtSurName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập họ.");
                txtSurName.Focus();
                return false;
            }

            if (txtMiddleName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên đệm.");
                txtMiddleName.Focus();
                return false;
            }

            if (txtFirstName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên.");
                txtFirstName.Focus();
                return false;
            }

            if (txtIdentityCard.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập CMND.");
                txtIdentityCard.Focus();
                return false;
            }

            if (txtQualifications.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bằng cấp.");
                txtQualifications.Focus();
                return false;
            }

            if (txtAddress.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập địa chỉ.");
                txtAddress.Focus();
                return false;
            }

            if (txtWard.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập phường/xã.");
                txtWard.Focus();
                return false;
            }

            if (txtDistrict.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập quận/huyện");
                txtDistrict.Focus();
                return false;
            }

            if (txtCity.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tỉnh/thành phố.");
                txtCity.Focus();
                return false;
            }

            return true;
        }

        private void DisplayInfo(DataRow drDocStaff)
        {
            txtSurName.Text = drDocStaff["SurName"] as string;
            txtMiddleName.Text = drDocStaff["MiddleName"] as string;
            txtFirstName.Text = drDocStaff["FirstName"] as string;
            txtKnownAs.Text = drDocStaff["KnownAs"] as string;
            txtPreferredName.Text = drDocStaff["PreferredName"] as string;
            cboGender.SelectedIndex = Convert.ToInt32(drDocStaff["Gender"]);
            dtpkDOB.Value = Convert.ToDateTime(drDocStaff["Dob"]);
            txtIdentityCard.Text = drDocStaff["IdentityCard"] as string;
            txtQualifications.Text = drDocStaff["Qualifications"] as string;
            cboSpeciality.SelectedValue = drDocStaff["SpecialityGUID"] as string;
            cboWorkType.SelectedIndex = Convert.ToInt32(drDocStaff["WorkType"]);
            cboStaffType.SelectedIndex = Convert.ToInt32(drDocStaff["StaffType"]);
            txtHomePhone.Text = drDocStaff["HomePhone"] as string;
            txtWorkPhone.Text = drDocStaff["WorkPhone"] as string;
            txtMobile.Text = drDocStaff["Mobile"] as string;
            txtEmail.Text = drDocStaff["Email"] as string;
            txtFax.Text = drDocStaff["Fax"] as string;
            txtAddress.Text = drDocStaff["Address"] as string;
            txtWard.Text = drDocStaff["Ward"] as string;
            txtDistrict.Text = drDocStaff["District"] as string;
            txtCity.Text = drDocStaff["City"] as string;

            _contact.ContactGUID = Guid.Parse(drDocStaff["ContactGUID"].ToString());
            /*_contact.SurName = txtSurName.Text;
            _contact.MiddleName = txtMiddleName.Text;
            _contact.FirstName = txtFirstName.Text;
            _contact.KnownAs = txtKnownAs.Text;
            _contact.PreferredName = txtPreferredName.Text;
            _contact.Gender = (byte)cboGender.SelectedIndex;
            _contact.Dob = dtpkDOB.Value;
            _contact.IdentityCard = txtIdentityCard.Text;
            _contact.HomePhone = txtHomePhone.Text;
            _contact.WorkPhone = txtWorkPhone.Text;
            _contact.Mobile = txtMobile.Text;
            _contact.Email = txtEmail.Text;
            _contact.FAX = txtFax.Text;
            _contact.Address = txtAddress.Text;
            _contact.Ward = txtWard.Text;
            _contact.District = txtDistrict.Text;
            _contact.City = txtCity.Text;*/

            _docStaff.ContactGUID = _contact.ContactGUID;
            /*_docStaff.Qualifications = txtQualifications.Text;
            _docStaff.SpecialityGUID = Guid.Parse(cboSpeciality.SelectedValue.ToString());
            _docStaff.WorkType = (byte)cboWorkType.SelectedIndex;
            _docStaff.StaffType = (byte)cboStaffType.SelectedIndex;*/
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

        private void SetDocStaffInfo()
        {
            _contact.SurName = txtSurName.Text;
            _contact.MiddleName = txtMiddleName.Text;
            _contact.FirstName = txtFirstName.Text;
            _contact.KnownAs = txtKnownAs.Text;
            _contact.PreferredName = txtPreferredName.Text;
            
            _contact.Dob = dtpkDOB.Value;
            _contact.IdentityCard = txtIdentityCard.Text;
            _contact.HomePhone = txtHomePhone.Text;
            _contact.WorkPhone = txtWorkPhone.Text;
            _contact.Mobile = txtMobile.Text;
            _contact.Email = txtEmail.Text;
            _contact.FAX = txtFax.Text;
            _contact.Address = txtAddress.Text;
            _contact.Ward = txtWard.Text;
            _contact.District = txtDistrict.Text;
            _contact.City = txtCity.Text;
            _docStaff.Qualifications = txtQualifications.Text;

            MethodInvoker method = delegate
            {
                _docStaff.SpecialityGUID = Guid.Parse(cboSpeciality.SelectedValue.ToString());
                _docStaff.WorkType = (byte)cboWorkType.SelectedIndex;
                _docStaff.StaffType = (byte)cboStaffType.SelectedIndex;
                _contact.Gender = (byte)cboGender.SelectedIndex;
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();
            
        }

        private void OnSaveInfo()
        {
            SetDocStaffInfo();
            Result result = DocStaffBus.InsertDocStaff(_contact, _docStaff);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.InsertDocStaff"));
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.InsertDocStaff"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
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
