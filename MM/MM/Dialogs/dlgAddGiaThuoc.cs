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
    public partial class dlgAddGiaThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private GiaThuoc _giaThuoc = new GiaThuoc();
        private DataRow _drGiaThuoc = null;
        #endregion

        #region Constructor
        public dlgAddGiaThuoc()
        {
            InitializeComponent();
        }

        public dlgAddGiaThuoc(DataRow drGiaThuoc)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua gia thuoc";
            _drGiaThuoc = drGiaThuoc;
        }
        #endregion

        #region Properties
        public GiaThuoc GiaThuoc
        {
            get { return _giaThuoc; }
        }

        public string TenThuoc
        {
            get { return cboThuoc.Text; }
        }

        public string DonViTinh
        {
            get { return txtDonViTinh.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayApDung.Value = DateTime.Now;
            OnDisplayThuocList();
        }

        private void OnDisplayThuocList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
                cboThuoc.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private void RefreshDonViTinh()
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = cboThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string thuocGUID = cboThuoc.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows == null || rows.Length <= 0) return;

            string donViTinh = rows[0]["DonViTinh"].ToString();
            txtDonViTinh.Text = donViTinh;
        }

        private void DisplayInfo(DataRow drGiaThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cboThuoc.SelectedValue = drGiaThuoc["ThuocGUID"].ToString();
                dtpkNgayApDung.Value = Convert.ToDateTime(drGiaThuoc["NgayApDung"]);
                numGiaBan.Value = (Decimal)Convert.ToDouble(drGiaThuoc["GiaBan"]);

                _giaThuoc.GiaThuocGUID = Guid.Parse(drGiaThuoc["GiaThuocGUID"].ToString());

                if (drGiaThuoc["CreatedDate"] != null && drGiaThuoc["CreatedDate"] != DBNull.Value)
                    _giaThuoc.CreatedDate = Convert.ToDateTime(drGiaThuoc["CreatedDate"]);

                if (drGiaThuoc["CreatedBy"] != null && drGiaThuoc["CreatedBy"] != DBNull.Value)
                    _giaThuoc.CreatedBy = Guid.Parse(drGiaThuoc["CreatedBy"].ToString());

                if (drGiaThuoc["UpdatedDate"] != null && drGiaThuoc["UpdatedDate"] != DBNull.Value)
                    _giaThuoc.UpdatedDate = Convert.ToDateTime(drGiaThuoc["UpdatedDate"]);

                if (drGiaThuoc["UpdatedBy"] != null && drGiaThuoc["UpdatedBy"] != DBNull.Value)
                    _giaThuoc.UpdatedBy = Guid.Parse(drGiaThuoc["UpdatedBy"].ToString());

                if (drGiaThuoc["DeletedDate"] != null && drGiaThuoc["DeletedDate"] != DBNull.Value)
                    _giaThuoc.DeletedDate = Convert.ToDateTime(drGiaThuoc["DeletedDate"]);

                if (drGiaThuoc["DeletedBy"] != null && drGiaThuoc["DeletedBy"] != DBNull.Value)
                    _giaThuoc.DeletedBy = Guid.Parse(drGiaThuoc["DeletedBy"].ToString());

                _giaThuoc.Status = Convert.ToByte(drGiaThuoc["GiaThuocStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn thuốc.", IconType.Information);
                cboThuoc.Focus();
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
                MethodInvoker method = delegate
                {
                    _giaThuoc.ThuocGUID = Guid.Parse(cboThuoc.SelectedValue.ToString());
                    _giaThuoc.GiaBan = (double)numGiaBan.Value;
                    _giaThuoc.NgayApDung = dtpkNgayApDung.Value;
                    _giaThuoc.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _giaThuoc.CreatedDate = DateTime.Now;
                        _giaThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _giaThuoc.UpdatedDate = DateTime.Now;
                        _giaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = GiaThuocBus.InsertGiaThuoc(_giaThuoc);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GiaThuocBus.InsertGiaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.InsertGiaThuoc"));
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
        private void dlgAddGiaThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew)
                DisplayInfo(_drGiaThuoc);
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDonViTinh();
        }

        private void dlgAddGiaThuoc_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin giá thuốc ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
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
