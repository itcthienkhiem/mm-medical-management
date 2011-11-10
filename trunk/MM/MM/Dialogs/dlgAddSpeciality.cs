using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddSpeciality : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Speciality _speciality = new Speciality();
        #endregion

        #region Constructor
        public dlgAddSpeciality()
        {
            InitializeComponent();

        }

        public dlgAddSpeciality(DataRow drSpec)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua chuyen khoa";
            DisplayInfo(drSpec);
        }
        #endregion

        #region Properties
        public Speciality Speciality
        {
            get { return _speciality; }
            set { _speciality = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drSpec)
        {
            try
            {
                txtCode.Text = drSpec["Code"] as string;
                txtName.Text = drSpec["Name"] as string;
                txtDescription.Text = drSpec["Description"] as string;

                _speciality.SpecialityGUID = Guid.Parse(drSpec["SpecialityGUID"].ToString());

                if (drSpec["CreatedDate"] != null && drSpec["CreatedDate"] != DBNull.Value)
                    _speciality.CreatedDate = Convert.ToDateTime(drSpec["CreatedDate"]);

                if (drSpec["CreatedBy"] != null && drSpec["CreatedBy"] != DBNull.Value)
                    _speciality.CreatedBy = Guid.Parse(drSpec["CreatedBy"].ToString());

                if (drSpec["UpdatedDate"] != null && drSpec["UpdatedDate"] != DBNull.Value)
                    _speciality.UpdatedDate = Convert.ToDateTime(drSpec["UpdatedDate"]);

                if (drSpec["UpdatedBy"] != null && drSpec["UpdatedBy"] != DBNull.Value)
                    _speciality.UpdatedBy = Guid.Parse(drSpec["UpdatedBy"].ToString());

                if (drSpec["DeletedDate"] != null && drSpec["DeletedDate"] != DBNull.Value)
                    _speciality.DeletedDate = Convert.ToDateTime(drSpec["DeletedDate"]);

                if (drSpec["DeletedBy"] != null && drSpec["DeletedBy"] != DBNull.Value)
                    _speciality.DeletedBy = Guid.Parse(drSpec["DeletedBy"].ToString());

                _speciality.Status = Convert.ToByte(drSpec["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã chuyên khoa.");
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên chuyên khoa.");
                txtName.Focus();
                return false;
            }

            string specGUID = _isNew ? string.Empty : _speciality.SpecialityGUID.ToString();
            Result result = SpecialityBus.CheckSpecialityExistCode(specGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã chuyên khoa này đã tồn tại rồi. Vui lòng nhập mã khác.");
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SpecialityBus.CheckSpecialityExistCode"));
                return false;
            }

            return true;
        }

        private void SetSpecialityInfo()
        {
            try
            {
                _speciality.Code = txtCode.Text;
                _speciality.Name = txtName.Text;
                _speciality.Description = txtDescription.Text;
                _speciality.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _speciality.CreatedDate = DateTime.Now;
                    _speciality.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _speciality.UpdatedDate = DateTime.Now;
                    _speciality.UpdatedBy = Guid.Parse(Global.UserGUID);
                }
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
            SetSpecialityInfo();
            Result result = SpecialityBus.InsertSpeciality(_speciality);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SpecialityBus.InsertSpeciality"));
                Utility.WriteToTraceLog(result.GetErrorAsString("SpecialityBus.InsertSpeciality"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddSpeciality_FormClosing(object sender, FormClosingEventArgs e)
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
