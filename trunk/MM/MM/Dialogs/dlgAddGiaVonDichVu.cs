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
    public partial class dlgAddGiaVonDichVu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private GiaVonDichVu _giaVonDichVu = new GiaVonDichVu();
        private DataRow _drGiaVonDichVu = null;
        #endregion

        #region Constructor
        public dlgAddGiaVonDichVu()
        {
            InitializeComponent();
        }

        public dlgAddGiaVonDichVu(DataRow drGiaVonDichVu)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua gia von dich vu";
            _drGiaVonDichVu = drGiaVonDichVu;
        }
        #endregion

        #region Properties
        public GiaVonDichVu GiaVonDichVu
        {
            get { return _giaVonDichVu; }
        }

        public string TenDichVu
        {
            get { return cboService.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayApDung.Value = DateTime.Now;
            OnDisplayServiceList();
        }

        private void OnDisplayServiceList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ServicesBus.GetServicesList();
            if (result.IsOK)
                cboService.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }
        }

        private void DisplayInfo(DataRow drGiaVonDichVu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cboService.SelectedValue = drGiaVonDichVu["ServiceGUID"].ToString();
                dtpkNgayApDung.Value = Convert.ToDateTime(drGiaVonDichVu["NgayApDung"]);
                numGiaBan.Value = (Decimal)Convert.ToDouble(drGiaVonDichVu["GiaVon"]);

                _giaVonDichVu.GiaVonDichVuGUID = Guid.Parse(drGiaVonDichVu["GiaVonDichVuGUID"].ToString());

                if (drGiaVonDichVu["CreatedDate"] != null && drGiaVonDichVu["CreatedDate"] != DBNull.Value)
                    _giaVonDichVu.CreatedDate = Convert.ToDateTime(drGiaVonDichVu["CreatedDate"]);

                if (drGiaVonDichVu["CreatedBy"] != null && drGiaVonDichVu["CreatedBy"] != DBNull.Value)
                    _giaVonDichVu.CreatedBy = Guid.Parse(drGiaVonDichVu["CreatedBy"].ToString());

                if (drGiaVonDichVu["UpdatedDate"] != null && drGiaVonDichVu["UpdatedDate"] != DBNull.Value)
                    _giaVonDichVu.UpdatedDate = Convert.ToDateTime(drGiaVonDichVu["UpdatedDate"]);

                if (drGiaVonDichVu["UpdatedBy"] != null && drGiaVonDichVu["UpdatedBy"] != DBNull.Value)
                    _giaVonDichVu.UpdatedBy = Guid.Parse(drGiaVonDichVu["UpdatedBy"].ToString());

                if (drGiaVonDichVu["DeletedDate"] != null && drGiaVonDichVu["DeletedDate"] != DBNull.Value)
                    _giaVonDichVu.DeletedDate = Convert.ToDateTime(drGiaVonDichVu["DeletedDate"]);

                if (drGiaVonDichVu["DeletedBy"] != null && drGiaVonDichVu["DeletedBy"] != DBNull.Value)
                    _giaVonDichVu.DeletedBy = Guid.Parse(drGiaVonDichVu["DeletedBy"].ToString());

                _giaVonDichVu.Status = Convert.ToByte(drGiaVonDichVu["GiaVonDichVuStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboService.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
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
                    _giaVonDichVu.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());
                    _giaVonDichVu.GiaVon = (double)numGiaBan.Value;
                    _giaVonDichVu.NgayApDung = dtpkNgayApDung.Value;
                    _giaVonDichVu.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _giaVonDichVu.CreatedDate = DateTime.Now;
                        _giaVonDichVu.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _giaVonDichVu.UpdatedDate = DateTime.Now;
                        _giaVonDichVu.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = GiaVonDichVuBus.InsertGiaVonDichVu(_giaVonDichVu);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GiaVonDichVuBus.InsertGiaVonDichVu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.InsertGiaVonDichVu"));
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
                DisplayInfo(_drGiaVonDichVu);
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin giá vốn dịch vụ ?") == System.Windows.Forms.DialogResult.Yes)
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
