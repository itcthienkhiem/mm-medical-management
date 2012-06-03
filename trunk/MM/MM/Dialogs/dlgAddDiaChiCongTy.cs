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
    public partial class dlgAddDiaChiCongTy : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DiaChiCongTy _diaChiCongTy = new DiaChiCongTy();
        private DataRow _drDiaChiCongTy = null;
        #endregion

        #region Constructor
        public dlgAddDiaChiCongTy()
        {
            InitializeComponent();
        }

        public dlgAddDiaChiCongTy(DataRow drDiaChiCongTy)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua dia chi cong ty";
            _drDiaChiCongTy = drDiaChiCongTy;
        }
        #endregion

        #region Properties
        public DiaChiCongTy DiaChiCongTy
        {
            get { return _diaChiCongTy; }
            set { _diaChiCongTy = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drDiaChiCongTy)
        {
            try
            {
                txtMaCongTy.Text = drDiaChiCongTy["MaCongTy"] as string;
                txtDiaChi.Text = drDiaChiCongTy["DiaChi"] as string;

                _diaChiCongTy.DiaChiCongTyGUID = Guid.Parse(drDiaChiCongTy["DiaChiCongTyGUID"].ToString());

                if (drDiaChiCongTy["CreatedDate"] != null && drDiaChiCongTy["CreatedDate"] != DBNull.Value)
                    _diaChiCongTy.CreatedDate = Convert.ToDateTime(drDiaChiCongTy["CreatedDate"]);

                if (drDiaChiCongTy["CreatedBy"] != null && drDiaChiCongTy["CreatedBy"] != DBNull.Value)
                    _diaChiCongTy.CreatedBy = Guid.Parse(drDiaChiCongTy["CreatedBy"].ToString());

                if (drDiaChiCongTy["UpdatedDate"] != null && drDiaChiCongTy["UpdatedDate"] != DBNull.Value)
                    _diaChiCongTy.UpdatedDate = Convert.ToDateTime(drDiaChiCongTy["UpdatedDate"]);

                if (drDiaChiCongTy["UpdatedBy"] != null && drDiaChiCongTy["UpdatedBy"] != DBNull.Value)
                    _diaChiCongTy.UpdatedBy = Guid.Parse(drDiaChiCongTy["UpdatedBy"].ToString());

                if (drDiaChiCongTy["DeletedDate"] != null && drDiaChiCongTy["DeletedDate"] != DBNull.Value)
                    _diaChiCongTy.DeletedDate = Convert.ToDateTime(drDiaChiCongTy["DeletedDate"]);

                if (drDiaChiCongTy["DeletedBy"] != null && drDiaChiCongTy["DeletedBy"] != DBNull.Value)
                    _diaChiCongTy.DeletedBy = Guid.Parse(drDiaChiCongTy["DeletedBy"].ToString());

                _diaChiCongTy.Status = Convert.ToByte(drDiaChiCongTy["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtMaCongTy.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã công ty.", IconType.Information);
                txtMaCongTy.Focus();
                return false;
            }

            if (txtDiaChi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập địa chỉ công ty.", IconType.Information);
                txtDiaChi.Focus();
                return false;
            }

            string diaChiCongTyGUID = _isNew ? string.Empty : _diaChiCongTy.DiaChiCongTyGUID.ToString();
            Result result = DiaChiCongTyBus.CheckMaCongTyExistCode(diaChiCongTyGUID, txtMaCongTy.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã công ty này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaCongTy.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DiaChiCongTyBus.CheckMaCongTyExistCode"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DiaChiCongTyBus.CheckMaCongTyExistCode"));
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
                _diaChiCongTy.MaCongTy = txtMaCongTy.Text;
                _diaChiCongTy.DiaChi = txtDiaChi.Text;
                _diaChiCongTy.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _diaChiCongTy.CreatedDate = DateTime.Now;
                    _diaChiCongTy.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _diaChiCongTy.UpdatedDate = DateTime.Now;
                    _diaChiCongTy.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = DiaChiCongTyBus.InsertDiaChiCongTy(_diaChiCongTy);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("DiaChiCongTyBus.InsertDiaChiCongTy"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("DiaChiCongTyBus.InsertDiaChiCongTy"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }

        }
        #endregion

        #region Window Event Handlers
        private void dlgAddDiaChiCongTy_Load(object sender, EventArgs e)
        {
            if (!_isNew)
            {
                DisplayInfo(_drDiaChiCongTy);
            }
        }

        private void dlgAddDiaChiCongTy_FormClosing(object sender, FormClosingEventArgs e)
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
