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
            this.Text = "Sua bac si";
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

            if (!Utility.IsValidDOB(txtDOB.Text))
            {
                MsgBox.Show(this.Text, "Ngày sinh hoặc năm sinh chưa đúng. Vui lòng nhập lại");
                txtDOB.Focus();
                return false;
            }
            
            if (cboSpeciality.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập chuyên khoa.");
                cboSpeciality.Focus();
                return false;
            }

            if (txtEmail.Text.Trim() != string.Empty && !Utility.IsValidEmail(txtEmail.Text))
            {
                MsgBox.Show(this.Text, "Địa chỉ email không hợp lê.");
                txtEmail.Focus();
                return false;
            }

            return true;
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
                cboStaffType.SelectedIndex = Convert.ToInt32(drDocStaff["StaffType"]);
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
                _contact.Archived = true;

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
                    _docStaff.StaffType = (byte)cboStaffType.SelectedIndex;

                    switch (_docStaff.StaffType)
                    {
                        case 0:
                            _contact.Occupation = "Bác sĩ";
                            break;
                        case 1:
                            _contact.Occupation = "Y tá";
                            break;
                        case 2:
                            _contact.Occupation = "Lễ tân";
                            break;
                    }

                    _contact.Gender = (byte)cboGender.SelectedIndex;

                    Result result = DocStaffBus.InsertDocStaff(_contact, _docStaff);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.InsertDocStaff"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.InsertDocStaff"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
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
