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
    public partial class dlgAddThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Thuoc _thuoc = new Thuoc();
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddThuoc()
        {
            InitializeComponent();
            cboDonViTinh.SelectedIndex = 0;
            GenerateCode();
        }

        public dlgAddThuoc(DataRow drThuoc, bool allowEdit)
        {
            InitializeComponent();
            cboDonViTinh.SelectedIndex = 0;
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua thuoc";
            DisplayInfo(drThuoc);
        }
        #endregion

        #region Properties
        public Thuoc Thuoc
        {
            get { return _thuoc; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaThuoc.Text = Utility.GetCode("TH", count + 1, 5);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocCount"));
            }
        }

        private void DisplayInfo(DataRow drThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaThuoc.Text = drThuoc["MaThuoc"] as string;
                txtTenThuoc.Text = drThuoc["TenThuoc"] as string;
                txtBietDuoc.Text = drThuoc["BietDuoc"] as string;
                cboDonViTinh.Text = drThuoc["DonViTinh"] as string;

                if (drThuoc["HamLuong"] != null && drThuoc["HamLuong"] != DBNull.Value)
                    txtHamLuong.Text = drThuoc["HamLuong"] as string;

                if (drThuoc["HoatChat"] != null && drThuoc["HoatChat"] != DBNull.Value)
                    txtHoatChat.Text = drThuoc["HoatChat"] as string;

                txtNote.Text = drThuoc["Note"] as string;

                _thuoc.ThuocGUID = Guid.Parse(drThuoc["ThuocGUID"].ToString());

                if (drThuoc["CreatedDate"] != null && drThuoc["CreatedDate"] != DBNull.Value)
                    _thuoc.CreatedDate = Convert.ToDateTime(drThuoc["CreatedDate"]);

                if (drThuoc["CreatedBy"] != null && drThuoc["CreatedBy"] != DBNull.Value)
                    _thuoc.CreatedBy = Guid.Parse(drThuoc["CreatedBy"].ToString());

                if (drThuoc["UpdatedDate"] != null && drThuoc["UpdatedDate"] != DBNull.Value)
                    _thuoc.UpdatedDate = Convert.ToDateTime(drThuoc["UpdatedDate"]);

                if (drThuoc["UpdatedBy"] != null && drThuoc["UpdatedBy"] != DBNull.Value)
                    _thuoc.UpdatedBy = Guid.Parse(drThuoc["UpdatedBy"].ToString());

                if (drThuoc["DeletedDate"] != null && drThuoc["DeletedDate"] != DBNull.Value)
                    _thuoc.DeletedDate = Convert.ToDateTime(drThuoc["DeletedDate"]);

                if (drThuoc["DeletedBy"] != null && drThuoc["DeletedBy"] != DBNull.Value)
                    _thuoc.DeletedBy = Guid.Parse(drThuoc["DeletedBy"].ToString());

                _thuoc.Status = Convert.ToByte(drThuoc["Status"]);

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    groupBox1.Enabled = _allowEdit;
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
            if (txtMaThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã thuốc.", IconType.Information);
                txtMaThuoc.Focus();
                return false;
            }

            if (txtTenThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên thuốc.", IconType.Information);
                txtTenThuoc.Focus();
                return false;
            }

            string thuocGUID = _isNew ? string.Empty : _thuoc.ThuocGUID.ToString();
            Result result = ThuocBus.CheckThuocExistCode(thuocGUID, txtMaThuoc.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.CheckThuocExistCode"), IconType.Error);
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
                _thuoc.MaThuoc = txtMaThuoc.Text;
                _thuoc.TenThuoc = txtTenThuoc.Text;
                _thuoc.BietDuoc = txtBietDuoc.Text;
                _thuoc.HamLuong = txtHamLuong.Text;
                _thuoc.HoatChat = txtHoatChat.Text;
                _thuoc.Note = txtNote.Text;
                _thuoc.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _thuoc.CreatedDate = DateTime.Now;
                    _thuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _thuoc.UpdatedDate = DateTime.Now;
                    _thuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _thuoc.DonViTinh = cboDonViTinh.Text;
                    Result result = ThuocBus.InsertThuoc(_thuoc);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.InsertThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.InsertThuoc"));
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
        private void dlgAddThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin thuốc ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void raNhom6_CheckedChanged(object sender, EventArgs e)
        {
            numGoi_Nhom6.Enabled = raNhom6.Checked;
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
