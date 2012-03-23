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
    public partial class dlgAddNhatKyKienHeCongTy : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private NhatKyLienHeCongTy _nhatKyLienHeCongTy = new NhatKyLienHeCongTy();
        private DataRow _drNhatKyLienHeCongTy = null;

        #endregion

        #region Constructor
        public dlgAddNhatKyKienHeCongTy()
        {
            InitializeComponent();
        }

        public dlgAddNhatKyKienHeCongTy(DataRow drNhatKyLienHeCongTy)
        {
            InitializeComponent();
            _drNhatKyLienHeCongTy = drNhatKyLienHeCongTy;
            _isNew = false;
            this.Text = "Sua nhat ky lien he cong ty";
        }
        #endregion

        #region Properties
        public NhatKyLienHeCongTy NhatKyLienHeCongTy
        {
            get { return _nhatKyLienHeCongTy; }
            set { _nhatKyLienHeCongTy = value; }
        }
        #endregion

        #region UI Command
        private void DisplayCongTyLienHeList()
        {
            dtpkNgayGioLienHe.Value = DateTime.Now;
            Result result = NhatKyLienHeCongTyBus.GetCongTyLienHeList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cboCongTyLienHe.Items.Add(row["CongTyLienHe"].ToString());
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.GetCongTyLienHeList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.GetCongTyLienHeList"));
            }
        }

        private void DisplayInfo(DataRow drNhatKyLienHeCongTy)
        {
            try
            {
                dtpkNgayGioLienHe.Value = Convert.ToDateTime(drNhatKyLienHeCongTy["NgayGioLienHe"]);
                cboCongTyLienHe.Text = drNhatKyLienHeCongTy["CongTyLienHe"] as string;
                txtNoiDungLienHe.Text = drNhatKyLienHeCongTy["NoiDungLienHe"] as string;

                _nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID = Guid.Parse(drNhatKyLienHeCongTy["NhatKyLienHeCongTyGUID"].ToString());

                if (drNhatKyLienHeCongTy["CreatedDate"] != null && drNhatKyLienHeCongTy["CreatedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.CreatedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["CreatedDate"]);

                if (drNhatKyLienHeCongTy["CreatedBy"] != null && drNhatKyLienHeCongTy["CreatedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.CreatedBy = Guid.Parse(drNhatKyLienHeCongTy["CreatedBy"].ToString());

                if (drNhatKyLienHeCongTy["UpdatedDate"] != null && drNhatKyLienHeCongTy["UpdatedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.UpdatedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["UpdatedDate"]);

                if (drNhatKyLienHeCongTy["UpdatedBy"] != null && drNhatKyLienHeCongTy["UpdatedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.UpdatedBy = Guid.Parse(drNhatKyLienHeCongTy["UpdatedBy"].ToString());

                if (drNhatKyLienHeCongTy["DeletedDate"] != null && drNhatKyLienHeCongTy["DeletedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.DeletedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["DeletedDate"]);

                if (drNhatKyLienHeCongTy["DeletedBy"] != null && drNhatKyLienHeCongTy["DeletedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.DeletedBy = Guid.Parse(drNhatKyLienHeCongTy["DeletedBy"].ToString());

                _nhatKyLienHeCongTy.Status = Convert.ToByte(drNhatKyLienHeCongTy["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboCongTyLienHe.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập công ty liên hệ.", IconType.Information);
                cboCongTyLienHe.Focus();
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

                _nhatKyLienHeCongTy.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _nhatKyLienHeCongTy.CreatedDate = DateTime.Now;
                    _nhatKyLienHeCongTy.DeletedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _nhatKyLienHeCongTy.UpdatedDate = DateTime.Now;
                    _nhatKyLienHeCongTy.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _nhatKyLienHeCongTy.NgayGioLienHe = dtpkNgayGioLienHe.Value;

                    if (Global.StaffType != StaffType.Admin)
                        _nhatKyLienHeCongTy.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    else
                        _nhatKyLienHeCongTy.DocStaffGUID = null;

                    _nhatKyLienHeCongTy.CongTyLienHe = cboCongTyLienHe.Text;
                    _nhatKyLienHeCongTy.NoiDungLienHe = txtNoiDungLienHe.Text;
                    _nhatKyLienHeCongTy.Note = string.Empty;

                    Result result = NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy(_nhatKyLienHeCongTy);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"));
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
        private void dlgAddNhatKyKienHeCongTy_Load(object sender, EventArgs e)
        {
            DisplayCongTyLienHeList();
            if (!_isNew) DisplayInfo(_drNhatKyLienHeCongTy);
        }

        private void dlgAddNhatKyKienHeCongTy_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu nhật ký liên hệ công ty ?") == System.Windows.Forms.DialogResult.Yes)
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
