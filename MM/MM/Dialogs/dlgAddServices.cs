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
            InitData();
            GenerateCode();
        }

        public dlgAddServices(DataRow drService)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            this.Text = "Sua dich vu";
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
        private void InitData()
        {
            cboType.SelectedIndex = 0;
            cboStaffType.SelectedIndex = 0;
            label3.Visible = Global.AllowShowServiePrice;
            label5.Visible = Global.AllowShowServiePrice;
            numPrice.Visible = Global.AllowShowServiePrice;
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ServicesBus.GetServiceCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtCode.Text = Utility.GetCode("DV", count + 1, 3);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServiceCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServiceCount"));
            }
        }

        private void DisplayInfo(DataRow drService)
        {
            try
            {
                txtCode.Text = drService["Code"] as string;
                txtName.Text = drService["Name"] as string;
                numPrice.Value = (decimal)Double.Parse(drService["Price"].ToString());
                numDiscount.Value = (decimal)Double.Parse(drService["Discount"].ToString());
                txtDescription.Text = drService["Description"] as string;

                if (drService["StaffType"] != null && drService["StaffType"] != DBNull.Value)
                {
                    StaffType staffType = (StaffType)Convert.ToByte(drService["StaffType"]);
                    switch (staffType)
                    {
                        case StaffType.BacSi:
                            cboStaffType.SelectedIndex = 1;
                            break;
                        case StaffType.BacSiSieuAm:
                            cboStaffType.SelectedIndex = 2;
                            break;
                        case StaffType.BacSiNgoaiTongQuat:
                            cboStaffType.SelectedIndex = 3;
                            break;
                        case StaffType.BacSiNoiTongQuat:
                            cboStaffType.SelectedIndex = 4;
                            break;
                        case StaffType.BacSiPhuKhoa:
                            cboStaffType.SelectedIndex = 5;
                            break;
                        case StaffType.DieuDuong:
                            cboStaffType.SelectedIndex = 6;
                            break;
                        case StaffType.XetNghiem:
                            cboStaffType.SelectedIndex = 7;
                            break;
                        case StaffType.None:
                            cboStaffType.SelectedIndex = 0;
                            break;
                    }
                }

                if (drService["EnglishName"] != null && drService["EnglishName"] != DBNull.Value)
                    txtEnglishName.Text = drService["EnglishName"].ToString();

                cboType.SelectedIndex = Convert.ToByte(drService["Type"]);

                _service.ServiceGUID = Guid.Parse(drService["ServiceGUID"].ToString());

                if (drService["CreatedDate"] != null && drService["CreatedDate"] != DBNull.Value)
                    _service.CreatedDate = Convert.ToDateTime(drService["CreatedDate"]);

                if (drService["CreatedBy"] != null && drService["CreatedBy"] != DBNull.Value)
                    _service.CreatedBy = Guid.Parse(drService["CreatedBy"].ToString());

                if (drService["UpdatedDate"] != null && drService["UpdatedDate"] != DBNull.Value)
                    _service.UpdatedDate = Convert.ToDateTime(drService["UpdatedDate"]);

                if (drService["UpdatedBy"] != null && drService["UpdatedBy"] != DBNull.Value)
                    _service.UpdatedBy = Guid.Parse(drService["UpdatedBy"].ToString());

                if (drService["DeletedDate"] != null && drService["DeletedDate"] != DBNull.Value)
                    _service.DeletedDate = Convert.ToDateTime(drService["DeletedDate"]);

                if (drService["DeletedBy"] != null && drService["DeletedBy"] != DBNull.Value)
                    _service.DeletedBy = Guid.Parse(drService["DeletedBy"].ToString());

                _service.Status = Convert.ToByte(drService["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã dịch vụ.", IconType.Information);
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên dịch vụ.", IconType.Information);
                txtName.Focus();
                return false;
            }

            string serviceGUID = _isNew ? string.Empty : _service.ServiceGUID.ToString();
            Result result = ServicesBus.CheckServicesExistCode(serviceGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã dịch vụ này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.CheckServicesExistCode"), IconType.Error);
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
                _service.Code = txtCode.Text;
                _service.Name = txtName.Text;
                _service.EnglishName = txtEnglishName.Text;
                _service.Price = (double)numPrice.Value;
                _service.Discount = (double)numDiscount.Value;
                _service.Description = txtDescription.Text;
                _service.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _service.CreatedDate = DateTime.Now;
                    _service.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _service.UpdatedDate = DateTime.Now;
                    _service.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _service.Type = (byte)cboType.SelectedIndex;

                    switch (cboStaffType.SelectedIndex)
                    {
                        case 0:
                            _service.StaffType = (byte)StaffType.None;
                            break;
                        case 1:
                            _service.StaffType = (byte)StaffType.BacSi;
                            break;
                        case 2:
                            _service.StaffType = (byte)StaffType.BacSiSieuAm;
                            break;
                        case 3:
                            _service.StaffType = (byte)StaffType.BacSiNgoaiTongQuat;
                            break;
                        case 4:
                            _service.StaffType = (byte)StaffType.BacSiNoiTongQuat;
                            break;
                        case 5:
                            _service.StaffType = (byte)StaffType.BacSiPhuKhoa;
                            break;
                        case 6:
                            _service.StaffType = (byte)StaffType.DieuDuong;
                            break;
                        case 7:
                            _service.StaffType = (byte)StaffType.XetNghiem;
                            break;
                    }

                    Result result = ServicesBus.InsertService(_service);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.InsertService"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.InsertService"));
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
        private void dlgAddServices_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin dịch vụ ?") == System.Windows.Forms.DialogResult.Yes)
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
