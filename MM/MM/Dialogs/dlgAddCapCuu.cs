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
    public partial class dlgAddCapCuu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private KhoCapCuu _khoCapCuu = new KhoCapCuu();
        private DataRow _drCapCuu = null;
        #endregion

        #region Constructor
        public dlgAddCapCuu()
        {
            InitializeComponent();
            cboDonViTinh.SelectedIndex = 0;
        }

        public dlgAddCapCuu(DataRow drCapCuu)
        {
            InitializeComponent();
            cboDonViTinh.SelectedIndex = 0;
            _isNew = false;
            this.Text = "Sua thong tin cap cuu";
            _drCapCuu = drCapCuu;
        }
        #endregion

        #region Properties
        public KhoCapCuu KhoCapCuu
        {
            get { return _khoCapCuu; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtTenCapCuu.Text = _drCapCuu["TenCapCuu"] as string;
                cboDonViTinh.Text = _drCapCuu["DonViTinh"] as string;

                txtNote.Text = _drCapCuu["Note"] as string;

                _khoCapCuu.KhoCapCuuGUID = Guid.Parse(_drCapCuu["KhoCapCuuGUID"].ToString());

                if (_drCapCuu["CreatedDate"] != null && _drCapCuu["CreatedDate"] != DBNull.Value)
                    _khoCapCuu.CreatedDate = Convert.ToDateTime(_drCapCuu["CreatedDate"]);

                if (_drCapCuu["CreatedBy"] != null && _drCapCuu["CreatedBy"] != DBNull.Value)
                    _khoCapCuu.CreatedBy = Guid.Parse(_drCapCuu["CreatedBy"].ToString());

                if (_drCapCuu["UpdatedDate"] != null && _drCapCuu["UpdatedDate"] != DBNull.Value)
                    _khoCapCuu.UpdatedDate = Convert.ToDateTime(_drCapCuu["UpdatedDate"]);

                if (_drCapCuu["UpdatedBy"] != null && _drCapCuu["UpdatedBy"] != DBNull.Value)
                    _khoCapCuu.UpdatedBy = Guid.Parse(_drCapCuu["UpdatedBy"].ToString());

                if (_drCapCuu["DeletedDate"] != null && _drCapCuu["DeletedDate"] != DBNull.Value)
                    _khoCapCuu.DeletedDate = Convert.ToDateTime(_drCapCuu["DeletedDate"]);

                if (_drCapCuu["DeletedBy"] != null && _drCapCuu["DeletedBy"] != DBNull.Value)
                    _khoCapCuu.DeletedBy = Guid.Parse(_drCapCuu["DeletedBy"].ToString());

                _khoCapCuu.Status = Convert.ToByte(_drCapCuu["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtTenCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên cấp cứu.", IconType.Information);
                txtTenCapCuu.Focus();
                return false;
            }

            string khoCapCuuGUID = _isNew ? string.Empty : _khoCapCuu.KhoCapCuuGUID.ToString();
            Result result = KhoCapCuuBus.CheckTenCapCuuExist(khoCapCuuGUID, txtTenCapCuu.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Tên cấp cứu này đã tồn tại rồi. Vui lòng nhập tên khác.", IconType.Information);
                    txtTenCapCuu.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.CheckTenCapCuuExist"), IconType.Error);
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
                _khoCapCuu.TenCapCuu = txtTenCapCuu.Text;
                _khoCapCuu.Note = txtNote.Text;
                _khoCapCuu.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _khoCapCuu.CreatedDate = DateTime.Now;
                    _khoCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _khoCapCuu.UpdatedDate = DateTime.Now;
                    _khoCapCuu.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _khoCapCuu.DonViTinh = cboDonViTinh.Text;
                    Result result = KhoCapCuuBus.InsertThongTinCapCuu(_khoCapCuu);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.InsertThongTinCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.InsertThongTinCapCuu"));
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
        private void dlgAddCapCuu_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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
