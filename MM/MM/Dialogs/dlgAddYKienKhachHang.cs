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
    public partial class dlgAddYKienKhachHang : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private YKienKhachHang _yKienKhachHang = new YKienKhachHang();
        private DataRow _drYKienKhachHang = null;
        private DataTable _dataSourceBenhNhan = null;
        private bool _isView = false;
        #endregion

        #region Constructor
        public dlgAddYKienKhachHang()
        {
            InitializeComponent();
        }

        public dlgAddYKienKhachHang(object drPatient)
        {
            InitializeComponent();

            DataRow patientRow = (DataRow)drPatient;

            txtTenKhachHang.Tag = patientRow["PatientGUID"].ToString();
            txtTenKhachHang.Text = patientRow["FullName"].ToString();
            txtSoDienThoai.Text = patientRow["Mobile"].ToString();
            txtDiaChi.Text = patientRow["Address"].ToString();
        }

        public dlgAddYKienKhachHang(DataRow drYKienKhachHang)
        {
            InitializeComponent();
            _drYKienKhachHang = drYKienKhachHang;
            _isNew = false;
            this.Text = "Sua y kien khach hang";
        }
        #endregion

        #region Properties
        public YKienKhachHang YKienKhachHang
        {
            get { return _yKienKhachHang; }
            set { _yKienKhachHang = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drYKienKhachHang)
        {
            try
            {
                txtTenKhachHang.Text = drYKienKhachHang["TenKhachHang"] as string;
                txtSoDienThoai.Text = drYKienKhachHang["SoDienThoai"] as string;
                txtDiaChi.Text = drYKienKhachHang["DiaChi"] as string;
                txtYeuCau.Text = drYKienKhachHang["YeuCau"] as string;
                txtNguon.Text = drYKienKhachHang["Nguon"] as string;

                _yKienKhachHang.YKienKhachHangGUID = Guid.Parse(drYKienKhachHang["YKienKhachHangGUID"].ToString());

                if (drYKienKhachHang["ContactDate"] != null && drYKienKhachHang["ContactDate"] != DBNull.Value)
                    _yKienKhachHang.ContactDate = Convert.ToDateTime(drYKienKhachHang["ContactDate"]);

                if (drYKienKhachHang["ContactBy"] != null && drYKienKhachHang["ContactBy"] != DBNull.Value)
                    _yKienKhachHang.ContactBy = Guid.Parse(drYKienKhachHang["ContactBy"].ToString());

                if (drYKienKhachHang["UpdatedDate"] != null && drYKienKhachHang["UpdatedDate"] != DBNull.Value)
                    _yKienKhachHang.UpdatedDate = Convert.ToDateTime(drYKienKhachHang["UpdatedDate"]);

                if (drYKienKhachHang["UpdatedBy"] != null && drYKienKhachHang["UpdatedBy"] != DBNull.Value)
                    _yKienKhachHang.UpdatedBy = Guid.Parse(drYKienKhachHang["UpdatedBy"].ToString());

                if (drYKienKhachHang["DeletedDate"] != null && drYKienKhachHang["DeletedDate"] != DBNull.Value)
                    _yKienKhachHang.DeletedDate = Convert.ToDateTime(drYKienKhachHang["DeletedDate"]);

                if (drYKienKhachHang["DeletedBy"] != null && drYKienKhachHang["DeletedBy"] != DBNull.Value)
                    _yKienKhachHang.DeletedBy = Guid.Parse(drYKienKhachHang["DeletedBy"].ToString());

                _yKienKhachHang.Status = Convert.ToByte(drYKienKhachHang["Status"]);

                string userGUID = drYKienKhachHang["ContactBy"].ToString();
                if (userGUID != Global.UserGUID)
                {
                    groupBox1.Enabled = false;
                    btnOK.Enabled = false;
                    _isView = true;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtTenKhachHang.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên khách hàng.", IconType.Information);
                txtTenKhachHang.Focus();
                return false;
            }

            string yKienKhachHangGUID = string.Empty;
            if (!_isNew) yKienKhachHangGUID = _yKienKhachHang.YKienKhachHangGUID.ToString();

            Result result = YKienKhachHangBus.CheckKhachHangExist(txtTenKhachHang.Text, yKienKhachHangGUID);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, string.Format("Khách hàng: '{0}' đã lấy ý kiến trước rồi. Vui lòng cập nhật thông tin cho khách hàng này.", txtTenKhachHang.Text),
                        IconType.Information);

                    _isView = true;
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("YKienKhachHangBus.CheckKhachHangExist"), IconType.Error);
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
                
                _yKienKhachHang.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _yKienKhachHang.ContactDate = DateTime.Now;
                    _yKienKhachHang.ContactBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _yKienKhachHang.UpdatedDate = DateTime.Now;
                    _yKienKhachHang.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    if (txtTenKhachHang.Tag != null)
                        _yKienKhachHang.PatientGUID = Guid.Parse(txtTenKhachHang.Tag.ToString());
                    else
                        _yKienKhachHang.PatientGUID = null;

                    _yKienKhachHang.TenKhachHang = txtTenKhachHang.Text;
                    _yKienKhachHang.SoDienThoai = txtSoDienThoai.Text;
                    _yKienKhachHang.DiaChi = txtDiaChi.Text;
                    _yKienKhachHang.YeuCau = txtYeuCau.Text;
                    _yKienKhachHang.Nguon = txtNguon.Text;
                    _yKienKhachHang.Note = string.Empty;

                    Result result = YKienKhachHangBus.InsertYKienKhachHang(_yKienKhachHang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("YKienKhachHangBus.InsertYKienKhachHang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.InsertYKienKhachHang"));
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

        private void OnDisplayBenhNhan()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
                _dataSourceBenhNhan = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddYKienKhachHang_Load(object sender, EventArgs e)
        {
            OnDisplayBenhNhan();
            if (!_isNew) DisplayInfo(_drYKienKhachHang);
        }

        private void dlgAddYKienKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else if (!_isView)
                    e.Cancel = true;
            }
            else if (!_isView)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu ý kiến khách hàng ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(_dataSourceBenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtTenKhachHang.Tag = patientRow["PatientGUID"].ToString();
                    txtTenKhachHang.Text = patientRow["FullName"].ToString();
                    txtSoDienThoai.Text = patientRow["Mobile"].ToString();
                    txtDiaChi.Text = patientRow["Address"].ToString();
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
