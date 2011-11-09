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
        #endregion

        #region UI Command
        private void InitData()
        {
            cboGender.SelectedIndex = 0;
        }

        private bool CheckInfo()
        {
            if (txtFileNum.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã bệnh nhân.");
                txtFileNum.Focus();
                return false;
            }

            if (txtSurName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập họ.");
                txtSurName.Focus();
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

            if (txtOccupation.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập nghề nghiệp.");
                txtOccupation.Focus();
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

            return true;
        }

        private void DisplayInfo(DataRow drPatient)
        {
            try
            {
                txtFileNum.Text = drPatient["FileNum"] as string;
                txtSurName.Text = drPatient["SurName"] as string;
                txtMiddleName.Text = drPatient["MiddleName"] as string;
                txtFirstName.Text = drPatient["FirstName"] as string;
                txtKnownAs.Text = drPatient["KnownAs"] as string;
                txtPreferredName.Text = drPatient["PreferredName"] as string;
                cboGender.SelectedIndex = Convert.ToInt32(drPatient["Gender"]);
                dtpkDOB.Value = Convert.ToDateTime(drPatient["Dob"]);
                txtIdentityCard.Text = drPatient["IdentityCard"] as string;
                txtOccupation.Text = drPatient["Occupation"] as string;
                txtHomePhone.Text = drPatient["HomePhone"] as string;
                txtWorkPhone.Text = drPatient["WorkPhone"] as string;
                txtMobile.Text = drPatient["Mobile"] as string;
                txtEmail.Text = drPatient["Email"] as string;
                txtFax.Text = drPatient["Fax"] as string;
                txtAddress.Text = drPatient["Address"] as string;
                txtWard.Text = drPatient["Ward"] as string;
                txtDistrict.Text = drPatient["District"] as string;
                txtCity.Text = drPatient["City"] as string;

                _contact.ContactGUID = Guid.Parse(drPatient["ContactGUID"].ToString());
                _patient.PatientGUID = Guid.Parse(drPatient["PatientGUID"].ToString());
                _patient.ContactGUID = _contact.ContactGUID;
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
                _contact.SurName = txtSurName.Text;
                _contact.MiddleName = txtMiddleName.Text;
                _contact.FirstName = txtFirstName.Text;
                _contact.KnownAs = txtKnownAs.Text;
                _contact.PreferredName = txtPreferredName.Text;
                _contact.Archived = true;
                _contact.Dob = dtpkDOB.Value;
                _contact.IdentityCard = txtIdentityCard.Text;
                _contact.Occupation = txtOccupation.Text;
                _contact.HomePhone = txtHomePhone.Text;
                _contact.WorkPhone = txtWorkPhone.Text;
                _contact.Mobile = txtMobile.Text;
                _contact.Email = txtEmail.Text;
                _contact.FAX = txtFax.Text;
                _contact.Address = txtAddress.Text;
                _contact.Ward = txtWard.Text;
                _contact.District = txtDistrict.Text;
                _contact.City = txtCity.Text;

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
            Result result = PatientBus.InsertPatient(_contact, _patient);
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
