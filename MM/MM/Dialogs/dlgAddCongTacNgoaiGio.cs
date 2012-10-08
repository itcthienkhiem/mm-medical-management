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
        private CongTacNgoaiGio _congTacNgoaiGio;
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
                cboNhanVien.DataSource = dt;
                cboNguoiDeXuat.DataSource = dt.Copy();
            }
        }

        private bool CheckInfo()
        {
            if (cboNhanVien.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn nhân viên.", IconType.Information);
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
                cboNhanVien.SelectedValue = _drCongTacNgoaiGio["DocStaffGUID"].ToString();
                txtMucDich.Text = _drCongTacNgoaiGio["MucDich"] as string;
                dtpkGioVao.Value = Convert.ToDateTime(_drCongTacNgoaiGio["GioVao"]);
                dtpkGioRa.Value = Convert.ToDateTime(_drCongTacNgoaiGio["GioRa"]);
                txtKetQuaDanhGia.Text = _drCongTacNgoaiGio["KetQuaDanhGia"] as string;
                cboNguoiDeXuat.SelectedValue = _drCongTacNgoaiGio["NguoiDeXuatGUID"].ToString();
                txtGhiChu.Text = _drCongTacNgoaiGio["GhiChu"] as string;

                //cboService.SelectedValue = drServiceHistory["ServiceGUID"].ToString();
                //cboDocStaff.SelectedValue = drServiceHistory["DocStaffGUID"].ToString();

                //if (drServiceHistory["RootPatientGUID"] != null && drServiceHistory["RootPatientGUID"] != DBNull.Value)
                //{
                //    txtChuyenNhuong.Tag = drServiceHistory["RootPatientGUID"].ToString();
                //    chkChuyenNhuong.Checked = true;
                //}

                //if (drServiceHistory["TenBenhNhanChuyenNhuong"] != null && drServiceHistory["TenBenhNhanChuyenNhuong"] != DBNull.Value)
                //    txtChuyenNhuong.Text = drServiceHistory["TenBenhNhanChuyenNhuong"].ToString();

                //bool isNormalOrNegative = Convert.ToBoolean(drServiceHistory["IsNormalOrNegative"]);
                //bool normal = Convert.ToBoolean(drServiceHistory["Normal"]);
                //bool abnormal = Convert.ToBoolean(drServiceHistory["Abnormal"]);
                //bool negative = Convert.ToBoolean(drServiceHistory["Negative"]);
                //bool positive = Convert.ToBoolean(drServiceHistory["Positive"]);

                //raNormal.Checked = isNormalOrNegative;
                //raNegative.Checked = !isNormalOrNegative;
                //chkNormal.Checked = normal;
                //chkAbnormal.Checked = abnormal;
                //chkNegative.Checked = negative;
                //chkPositive.Checked = positive;
                //raKhamTuTuc.Checked = Convert.ToBoolean(drServiceHistory["KhamTuTuc"]);
                //raKhamTheoHopDong.Checked = !raKhamTuTuc.Checked;

                //numPrice.Value = (decimal)Double.Parse(drServiceHistory["FixedPrice"].ToString());
                //numDiscount.Value = (decimal)Double.Parse(drServiceHistory["Discount"].ToString());
                //txtDescription.Text = drServiceHistory["Note"] as string;
                //_serviceHistory.ServiceHistoryGUID = Guid.Parse(drServiceHistory["ServiceHistoryGUID"].ToString());

                //if (drServiceHistory["ActivedDate"] != null && drServiceHistory["ActivedDate"] != DBNull.Value)
                //{
                //    _serviceHistory.ActivedDate = Convert.ToDateTime(drServiceHistory["ActivedDate"]);
                //    dtpkActiveDate.Value = _serviceHistory.ActivedDate.Value;
                //}

                //if (drServiceHistory["CreatedDate"] != null && drServiceHistory["CreatedDate"] != DBNull.Value)
                //    _serviceHistory.CreatedDate = Convert.ToDateTime(drServiceHistory["CreatedDate"]);

                //if (drServiceHistory["CreatedBy"] != null && drServiceHistory["CreatedBy"] != DBNull.Value)
                //    _serviceHistory.CreatedBy = Guid.Parse(drServiceHistory["CreatedBy"].ToString());

                //if (drServiceHistory["UpdatedDate"] != null && drServiceHistory["UpdatedDate"] != DBNull.Value)
                //    _serviceHistory.UpdatedDate = Convert.ToDateTime(drServiceHistory["UpdatedDate"]);

                //if (drServiceHistory["UpdatedBy"] != null && drServiceHistory["UpdatedBy"] != DBNull.Value)
                //    _serviceHistory.UpdatedBy = Guid.Parse(drServiceHistory["UpdatedBy"].ToString());

                //if (drServiceHistory["DeletedDate"] != null && drServiceHistory["DeletedDate"] != DBNull.Value)
                //    _serviceHistory.DeletedDate = Convert.ToDateTime(drServiceHistory["DeletedDate"]);

                //if (drServiceHistory["DeletedBy"] != null && drServiceHistory["DeletedBy"] != DBNull.Value)
                //    _serviceHistory.DeletedBy = Guid.Parse(drServiceHistory["DeletedBy"].ToString());

                //_serviceHistory.Status = Convert.ToByte(drServiceHistory["Status"]);

                //Result result = ChiDinhBus.GetChiDinh(_serviceHistory.ServiceHistoryGUID.ToString());
                //if (!result.IsOK)
                //{
                //    MsgBox.Show(this.Text, result.GetErrorAsString("ChiDinhBus.GetBacSiChiDinh"), IconType.Error);
                //    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetBacSiChiDinh"));
                //    return;
                //}
                //else if (result.QueryResult != null)
                //{
                //    _chiDinh = (ChiDinh)result.QueryResult;
                //    cboBacSiChiDinh.SelectedValue = _chiDinh.BacSiChiDinhGUID.ToString();
                //    chkBSCD.Checked = true;
                //}

                //if (!_allowEdit)
                //{
                //    btnOK.Enabled = _allowEdit;
                //    groupBox1.Enabled = _allowEdit;
                //}
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void SaveInfoAsThread()
        {

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
    }
}
