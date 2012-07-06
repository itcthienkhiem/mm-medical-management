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
    public partial class dlgEditChiTietKetQuaXetNghiemTay : dlgBase
    {
        #region Members
        private DataRow _drChiTietKQXN = null;
        private ChiTietKetQuaXetNghiem_Manual _chiTietKQXN = new ChiTietKetQuaXetNghiem_Manual();
        private bool _isTongHop = false;
        #endregion

        #region Constructor
        public dlgEditChiTietKetQuaXetNghiemTay(DataRow drChiTietKQXN)
        {
            InitializeComponent();
            _drChiTietKQXN = drChiTietKQXN;
        }
        #endregion

        #region Properties
        public bool IsTongHop
        {
            get { return _isTongHop; }
            set { _isTongHop = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Result result = XetNghiemTayBus.GetDonViList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                _uNormal_Chung.DonViList = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.GetDonViList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetDonViList"));
            }
        }

        private void DisplayInfo()
        {
            if (!_isTongHop)
            {
                _chiTietKQXN.ChiTietKetQuaXetNghiem_ManualGUID = Guid.Parse(_drChiTietKQXN["ChiTietKetQuaXetNghiem_ManualGUID"].ToString());
                dtpkNgayXetNghiem.Value = Convert.ToDateTime(_drChiTietKQXN["NgayXetNghiem"]);
            }
            else
            {
                _chiTietKQXN.ChiTietKetQuaXetNghiem_ManualGUID = Guid.Parse(_drChiTietKQXN["ChiTietKQXNGUID"].ToString());
                dtpkNgayXetNghiem.Value = Convert.ToDateTime(_drChiTietKQXN["NgayXN"]);
                //dtpkNgayXetNghiem.Value = Convert.ToDateTime(_drChiTietKQXN["NgayXN"]);
                //txtTenXetNghiem.Text = string.Format("{0} ({1})", _drChiTietKQXN["Fullname"].ToString(), _drChiTietKQXN["GroupName"].ToString());
                //txtResult.Text = _drChiTietKQXN["TestResult"].ToString();
                //chkLamThem.Checked = Convert.ToBoolean(_drChiTietKQXN["LamThem"]);
                //chkHutThuoc.Checked = Convert.ToBoolean(_drChiTietKQXN["HasHutThuoc"]);

                //if (_drChiTietKQXN["DoiTuong2"] != null && _drChiTietKQXN["DoiTuong2"] != DBNull.Value)
                //{
                //    DoiTuong doiTuong = (DoiTuong)Convert.ToByte(_drChiTietKQXN["DoiTuong2"]);

                //    if (doiTuong == DoiTuong.Khac)
                //    {
                //        _uNormal_SoiCanLangNuocTieu.Visible = true;

                //        if (_drChiTietKQXN["FromValue2"] != null && _drChiTietKQXN["FromValue2"] != DBNull.Value &&
                //            _drChiTietKQXN["ToValue2"] != null && _drChiTietKQXN["ToValue2"] != DBNull.Value)
                //        {
                //            _uNormal_SoiCanLangNuocTieu.FromToChecked = true;
                //            _uNormal_SoiCanLangNuocTieu.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue2"]);
                //            _uNormal_SoiCanLangNuocTieu.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue2"]);
                //        }
                //        else
                //            _uNormal_SoiCanLangNuocTieu.FromToChecked = false;

                //        _uNormal_SoiCanLangNuocTieu.XValue = Convert.ToDouble(_drChiTietKQXN["XValue2"]);
                //    }
                //    else
                //    {
                //        _uNormal_Chung.Visible = true;
                //        string donVi = string.Empty;
                //        if (_drChiTietKQXN["DonVi2"] != null && _drChiTietKQXN["DonVi2"] != DBNull.Value)
                //            donVi = _drChiTietKQXN["DonVi2"].ToString();
                //        _uNormal_Chung.DonVi = donVi;

                //        if (_drChiTietKQXN["FromValue2"] != null && _drChiTietKQXN["FromValue2"] != DBNull.Value &&
                //            _drChiTietKQXN["ToValue2"] != null && _drChiTietKQXN["ToValue2"] != DBNull.Value)
                //        {
                //            _uNormal_Chung.FromValueChecked = true;
                //            _uNormal_Chung.ToValueChecked = true;
                //            _uNormal_Chung.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue2"]);
                //            _uNormal_Chung.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue2"]);
                //        }
                //        else if (_drChiTietKQXN["FromValue2"] != null && _drChiTietKQXN["FromValue2"] != DBNull.Value)
                //        {
                //            _uNormal_Chung.FromValueChecked = true;
                //            _uNormal_Chung.ToValueChecked = false;
                //            _uNormal_Chung.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue2"]);
                //            _uNormal_Chung.FromOperator = _drChiTietKQXN["FromOperator2"].ToString();
                //        }
                //        else if (_drChiTietKQXN["ToValue2"] != null && _drChiTietKQXN["ToValue2"] != DBNull.Value)
                //        {
                //            _uNormal_Chung.FromValueChecked = false;
                //            _uNormal_Chung.ToValueChecked = true;
                //            _uNormal_Chung.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue2"]);
                //            _uNormal_Chung.ToOperator = _drChiTietKQXN["ToOperator2"].ToString();
                //        }
                //    }
                //}
            }

            
            txtTenXetNghiem.Text = string.Format("{0} ({1})", _drChiTietKQXN["Fullname"].ToString(), _drChiTietKQXN["GroupName"].ToString());
            txtResult.Text = _drChiTietKQXN["TestResult"].ToString();

            if (txtResult.Text.ToLower().Replace("negative", "").Replace("positive", "").Trim() != string.Empty)
                txtResult.Text = txtResult.Text.ToLower().Replace("negative", "").Replace("positive", "").Trim();

            chkLamThem.Checked = Convert.ToBoolean(_drChiTietKQXN["LamThem"]);
            chkHutThuoc.Checked = Convert.ToBoolean(_drChiTietKQXN["HasHutThuoc"]);

            if (_drChiTietKQXN["DoiTuong"] != null && _drChiTietKQXN["DoiTuong"] != DBNull.Value)
            {
                DoiTuong doiTuong = (DoiTuong)Convert.ToByte(_drChiTietKQXN["DoiTuong"]);

                if (doiTuong == DoiTuong.Khac)
                {
                    _uNormal_SoiCanLangNuocTieu.Visible = true;

                    if (_drChiTietKQXN["FromValue"] != null && _drChiTietKQXN["FromValue"] != DBNull.Value &&
                        _drChiTietKQXN["ToValue"] != null && _drChiTietKQXN["ToValue"] != DBNull.Value)
                    {
                        _uNormal_SoiCanLangNuocTieu.FromToChecked = true;
                        _uNormal_SoiCanLangNuocTieu.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue"]);
                        _uNormal_SoiCanLangNuocTieu.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue"]);
                    }
                    else
                        _uNormal_SoiCanLangNuocTieu.FromToChecked = false;

                    _uNormal_SoiCanLangNuocTieu.XValue = Convert.ToDouble(_drChiTietKQXN["XValue"]);
                }
                else
                {
                    _uNormal_Chung.Visible = true;
                    string donVi = string.Empty;
                    if (_drChiTietKQXN["DonVi"] != null && _drChiTietKQXN["DonVi"] != DBNull.Value)
                        donVi = _drChiTietKQXN["DonVi"].ToString();
                    _uNormal_Chung.DonVi = donVi;

                    if (_drChiTietKQXN["FromValue"] != null && _drChiTietKQXN["FromValue"] != DBNull.Value &&
                        _drChiTietKQXN["ToValue"] != null && _drChiTietKQXN["ToValue"] != DBNull.Value)
                    {
                        _uNormal_Chung.FromValueChecked = true;
                        _uNormal_Chung.ToValueChecked = true;
                        _uNormal_Chung.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue"]);
                        _uNormal_Chung.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue"]);
                    }
                    else if (_drChiTietKQXN["FromValue"] != null && _drChiTietKQXN["FromValue"] != DBNull.Value)
                    {
                        _uNormal_Chung.FromValueChecked = true;
                        _uNormal_Chung.ToValueChecked = false;
                        _uNormal_Chung.FromValue = Convert.ToDouble(_drChiTietKQXN["FromValue"]);
                        _uNormal_Chung.FromOperator = _drChiTietKQXN["FromOperator"].ToString();
                    }
                    else if (_drChiTietKQXN["ToValue"] != null && _drChiTietKQXN["ToValue"] != DBNull.Value)
                    {
                        _uNormal_Chung.FromValueChecked = false;
                        _uNormal_Chung.ToValueChecked = true;
                        _uNormal_Chung.ToValue = Convert.ToDouble(_drChiTietKQXN["ToValue"]);
                        _uNormal_Chung.ToOperator = _drChiTietKQXN["ToOperator"].ToString();
                    }
                }
            }
        }

        private bool CheckInfo()
        {
            if (txtResult.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập kết quả.", IconType.Information);
                txtResult.Focus();
                return false;
            }

            if (_uNormal_Chung.Visible && !_uNormal_Chung.CheckInfo())
            {
                _uNormal_Chung.Focus();
                return false;
            }

            if (_uNormal_SoiCanLangNuocTieu.Visible && !_uNormal_SoiCanLangNuocTieu.CheckInfo())
            {
                _uNormal_SoiCanLangNuocTieu.Focus();
                return false;
            }

            return true;
        }

        private bool SaveInfo()
        {
            _chiTietKQXN.NgayXetNghiem = dtpkNgayXetNghiem.Value;
            _chiTietKQXN.TestResult = txtResult.Text;
            _chiTietKQXN.LamThem = chkLamThem.Checked;
            _chiTietKQXN.HasHutThuoc = chkHutThuoc.Checked;

            if (_uNormal_SoiCanLangNuocTieu.Visible)
            {
                _chiTietKQXN.XValue = _uNormal_SoiCanLangNuocTieu.XValue;
                if (_uNormal_SoiCanLangNuocTieu.FromToChecked)
                {
                    _chiTietKQXN.FromValue = _uNormal_SoiCanLangNuocTieu.FromValue;
                    _chiTietKQXN.ToValue = _uNormal_SoiCanLangNuocTieu.ToValue;
                }
            }
            else if (_uNormal_Chung.Visible)
            {
                _chiTietKQXN.DonVi = _uNormal_Chung.DonVi;
                if (_uNormal_Chung.FromValueChecked)
                {
                    _chiTietKQXN.FromValue = _uNormal_Chung.FromValue;
                    _chiTietKQXN.FromOperator = _uNormal_Chung.FromOperator;
                }

                if (_uNormal_Chung.ToValueChecked)
                {
                    _chiTietKQXN.ToValue = _uNormal_Chung.ToValue;
                    _chiTietKQXN.ToOperator = _uNormal_Chung.ToOperator;
                }
            }

            Result result = KetQuaXetNghiemTayBus.UpdateChiTietKQXN(_chiTietKQXN);
            if (!result.IsOK)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateChiTietKQXN"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateChiTietKQXN"));
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgEditChiTietKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            InitData();
            DisplayInfo();
        }

        private void dlgEditChiTietKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else if (!SaveInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
