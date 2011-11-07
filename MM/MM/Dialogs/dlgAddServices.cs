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
    public partial class dlgAddServices : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Service _service = new Service();
        #endregion

        #region Constructor
        public dlgAddServices()
        {
            InitializeComponent();
        }

        public dlgAddServices(DataRow drService)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sửa dịch vụ";
            DisplayInfo(drService);
        }
        #endregion

        #region Properties
        public Service Service
        {
            get { return _service; }
            set { _service = value; }
        }

        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drService)
        {
            txtCode.Text = drService["Code"] as string;
            txtName.Text = drService["Name"] as string;
            numPrice.Value = (decimal)Double.Parse(drService["Price"].ToString());
            txtDescription.Text = drService["Description"] as string;

            _service.ServiceGUID = Guid.Parse(drService["ServiceGUID"].ToString());
            /*_service.Code = txtCode.Text;
            _service.Name = txtName.Text;
            _service.Price = (double)numPrice.Value;
            _service.Description = txtDescription.Text;*/
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã dịch vụ.");
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên dịch vụ.");
                txtName.Focus();
                return false;
            }

            string serviceGUID = _isNew ? string.Empty : _service.ServiceGUID.ToString();
            Result result = ServicesBus.CheckServicesExistCode(serviceGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã dịch vụ này đã tồn tại rồi. Vui lòng nhập mã khác.");
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.CheckServicesExistCode"));
                return false;
            }

            return true;
        }

        private void SetServiceInfo()
        {
            _service.Code = txtCode.Text;
            _service.Name = txtName.Text;
            _service.Price = (double)numPrice.Value;
            _service.Description = txtDescription.Text;
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
            SetServiceInfo();
            Result result = ServicesBus.InsertService(_service);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.InsertService"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.InsertService"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddServices_FormClosing(object sender, FormClosingEventArgs e)
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
