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
    public partial class dlgAddXuatKhoCapCuu : dlgBase
    {
        #region Members
        private XuatKhoCapCuu _xuatKhoCapCuu = new XuatKhoCapCuu();
        #endregion

        #region Constructor
        public dlgAddXuatKhoCapCuu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public XuatKhoCapCuu XuatKhoCapCuu
        {
            get { return _xuatKhoCapCuu; }
        }

        public string TenCapCuu
        {
            get { return cboKhoCapCuu.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayXuat.Value = DateTime.Now;
            OnDisplayKhoCapCuuList();
        }

        private void OnDisplayKhoCapCuuList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
                cboKhoCapCuu.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private bool CheckInfo()
        {
            if (cboKhoCapCuu.SelectedValue == null || cboKhoCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn thông tin cấp cứu.", IconType.Information);
                cboKhoCapCuu.Focus();
                return false;
            }

            if (numSoLuongXuat.Value <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số lượng xuất.", IconType.Information);
                numSoLuongXuat.Focus();
                return false;
            }

            string khoCapCuuGUID = cboKhoCapCuu.SelectedValue.ToString();
            Result r = NhapKhoCapCuuBus.CheckKhoCapCuuTonKho(khoCapCuuGUID, (int)numSoLuongXuat.Value);
            if (r.IsOK)
            {
                if (!Convert.ToBoolean(r.QueryResult))
                {
                    MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hoặc không đủ số lượng để xuất.", cboKhoCapCuu.Text), IconType.Information);
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"), IconType.Error);
                Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"));
                return false;
            }

            r = NhapKhoCapCuuBus.CheckKhoCapCuuHetHan(khoCapCuuGUID);
            if (r.IsOK)
            {
                if (Convert.ToBoolean(r.QueryResult))
                {
                    MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hạn sử dụng.", cboKhoCapCuu.Text), IconType.Information);
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"), IconType.Error);
                Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"));
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
                    _xuatKhoCapCuu.KhoCapCuuGUID = Guid.Parse(cboKhoCapCuu.SelectedValue.ToString());
                    _xuatKhoCapCuu.NgayXuat = dtpkNgayXuat.Value;
                    _xuatKhoCapCuu.SoLuong = (int)numSoLuongXuat.Value;
                    _xuatKhoCapCuu.GiaXuat = 0;
                    _xuatKhoCapCuu.Note = txtGhiChu.Text;
                    _xuatKhoCapCuu.Status = (byte)Status.Actived;
                    _xuatKhoCapCuu.CreatedDate = DateTime.Now;
                    _xuatKhoCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);

                    Result result = XuatKhoCapCuuBus.InsertXuatKhoCapCuu(_xuatKhoCapCuu);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XuatKhoCapCuuBus.InsertXuatKhoCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XuatKhoCapCuuBus.InsertXuatKhoCapCuu"));
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
        private void dlgAddLoThuoc_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string khoCapCuuGUID = cboKhoCapCuu.SelectedValue.ToString();
            Result result = NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu(khoCapCuuGUID);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"));
                return;
            }

            if (result.QueryResult != null)
                dtpkNgayHetHan.Value = Convert.ToDateTime(result.QueryResult);

            result = NhapKhoCapCuuBus.GetKhoCapCuuTonKho(khoCapCuuGUID);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"));
                return;
            }

            if (result.QueryResult != null)
                numSoLuongTon.Value = Convert.ToInt32(result.QueryResult);
        }

        private void dlgAddLoThuoc_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhập kho cấp cứu ?") == System.Windows.Forms.DialogResult.Yes)
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
