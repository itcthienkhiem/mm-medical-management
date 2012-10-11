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
    public partial class dlgAddCongTacNgoaiGio : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drCongTacNgoaiGio;
        private CongTacNgoaiGio _congTacNgoaiGio = new CongTacNgoaiGio();
        #endregion

        #region Constructor
        public dlgAddCongTacNgoaiGio()
        {
            InitializeComponent();
        }

        public dlgAddCongTacNgoaiGio(DataRow drCongTacNgoaiGio)
        {
            InitializeComponent();
            _drCongTacNgoaiGio = drCongTacNgoaiGio;
            this.Text = "Sua cong tac ngoai gio";
            _isNew = false;
        }
        #endregion

        #region Properties
        public CongTacNgoaiGio CongTacNgoaiGio
        {
            get { return _congTacNgoaiGio; }
            set { _congTacNgoaiGio = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgay.Value = DateTime.Now;
            dtpkGioVao.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            dtpkGioRa.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

            Result result = DocStaffBus.GetDocStaffList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                cboNguoiDeXuat.DataSource = dt.Copy();
            }
        }

        private bool CheckInfo()
        {
            if (txtTenNguoiLam.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên người làm.", IconType.Information);
                return false;
            }

            if (cboNguoiDeXuat.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn người đề xuất.", IconType.Information);
                return false;
            }

            return true;
        }

        private void DisplayInfo()
        {
            try
            {
                dtpkNgay.Value = Convert.ToDateTime(_drCongTacNgoaiGio["Ngay"]);
                txtTenNguoiLam.Text = _drCongTacNgoaiGio["TenNguoiLam"] as string;
                txtMucDich.Text = _drCongTacNgoaiGio["MucDich"] as string;
                dtpkGioVao.Value = Convert.ToDateTime(_drCongTacNgoaiGio["GioVao"]);
                dtpkGioRa.Value = Convert.ToDateTime(_drCongTacNgoaiGio["GioRa"]);
                txtKetQuaDanhGia.Text = _drCongTacNgoaiGio["KetQuaDanhGia"] as string;
                cboNguoiDeXuat.SelectedValue = _drCongTacNgoaiGio["NguoiDeXuatGUID"].ToString();
                txtGhiChu.Text = _drCongTacNgoaiGio["GhiChu"] as string;

                if (_drCongTacNgoaiGio["CreatedDate"] != null && _drCongTacNgoaiGio["CreatedDate"] != DBNull.Value)
                    _congTacNgoaiGio.CreatedDate = Convert.ToDateTime(_drCongTacNgoaiGio["CreatedDate"]);

                if (_drCongTacNgoaiGio["CreatedBy"] != null && _drCongTacNgoaiGio["CreatedBy"] != DBNull.Value)
                    _congTacNgoaiGio.CreatedBy = Guid.Parse(_drCongTacNgoaiGio["CreatedBy"].ToString());

                if (_drCongTacNgoaiGio["UpdatedDate"] != null && _drCongTacNgoaiGio["UpdatedDate"] != DBNull.Value)
                    _congTacNgoaiGio.UpdatedDate = Convert.ToDateTime(_drCongTacNgoaiGio["UpdatedDate"]);

                if (_drCongTacNgoaiGio["UpdatedBy"] != null && _drCongTacNgoaiGio["UpdatedBy"] != DBNull.Value)
                    _congTacNgoaiGio.UpdatedBy = Guid.Parse(_drCongTacNgoaiGio["UpdatedBy"].ToString());

                if (_drCongTacNgoaiGio["DeletedDate"] != null && _drCongTacNgoaiGio["DeletedDate"] != DBNull.Value)
                    _congTacNgoaiGio.DeletedDate = Convert.ToDateTime(_drCongTacNgoaiGio["DeletedDate"]);

                if (_drCongTacNgoaiGio["DeletedBy"] != null && _drCongTacNgoaiGio["DeletedBy"] != DBNull.Value)
                    _congTacNgoaiGio.DeletedBy = Guid.Parse(_drCongTacNgoaiGio["DeletedBy"].ToString());

                _congTacNgoaiGio.Status = Convert.ToByte(_drCongTacNgoaiGio["Status"]);
                _congTacNgoaiGio.CongTacNgoaiGioGUID = Guid.Parse(_drCongTacNgoaiGio["CongTacNgoaiGioGUID"].ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
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
                    _congTacNgoaiGio.Ngay = dtpkNgay.Value;
                    _congTacNgoaiGio.TenNguoiLam = txtTenNguoiLam.Text;
                    _congTacNgoaiGio.MucDich = txtMucDich.Text;
                    _congTacNgoaiGio.GioVao = dtpkGioVao.Value;
                    _congTacNgoaiGio.GioRa = dtpkGioRa.Value;
                    _congTacNgoaiGio.KetQuaDanhGia = txtKetQuaDanhGia.Text;
                    _congTacNgoaiGio.NguoiDeXuatGUID = Guid.Parse(cboNguoiDeXuat.SelectedValue.ToString());
                    _congTacNgoaiGio.GhiChu = txtGhiChu.Text;
                    _congTacNgoaiGio.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _congTacNgoaiGio.CreatedDate = DateTime.Now;
                        _congTacNgoaiGio.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _congTacNgoaiGio.UpdatedDate = DateTime.Now;
                        _congTacNgoaiGio.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = CongTacNgoaiGioBus.InsertCongTacNgoaiGio(_congTacNgoaiGio);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CongTacNgoaiGioBus.InsertCongTacNgoaiGio"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CongTacNgoaiGioBus.InsertCongTacNgoaiGio"));
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
        private void dlgAddCongTacNgoaiGio_Load(object sender, EventArgs e)
        {
            InitData();

            if (!_isNew) DisplayInfo();
        }

        private void dlgAddCongTacNgoaiGio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo()) SaveInfoAsThread();
                else e.Cancel = true;
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
