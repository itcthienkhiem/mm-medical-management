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
    public partial class dlgAddThuocKeToa : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drThuoc = null;
        private LoaiToaThuoc _type = LoaiToaThuoc.Chung;
        private ChiTietToaThuoc _chiTietToaThuoc = new ChiTietToaThuoc();
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgAddThuocKeToa(LoaiToaThuoc type)
        {
            InitializeComponent();
            _type = type;
        }

        public dlgAddThuocKeToa(DataRow drThuoc, LoaiToaThuoc type)
        {
            InitializeComponent();
            _type = type;
            _isNew = false;
            this.Text = "Sua thuoc ke toa";
            _drThuoc = drThuoc;
        }
        #endregion
        
        #region Properties
        public string TenThuoc
        {
            get { return cboThuoc.Text; }
        }

        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public ChiTietToaThuoc ChiTietToaThuoc
        {
            get { return _chiTietToaThuoc; }
        }
        #endregion

        #region UI Command
        private void InitData()
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

            if (_type == LoaiToaThuoc.Chung)
            {
                gbToaChung.Enabled = true;
                gbToaSanKhoa.Enabled = false;
            }
            else
            {
                gbToaChung.Enabled = false;
                gbToaSanKhoa.Enabled = true;
            }
        }

        private void DisplayInfo(DataRow drThuoc)
        {
            try
            {
                _chiTietToaThuoc.ChiTietToaThuocGUID = Guid.Parse(drThuoc["ChiTietToaThuocGUID"].ToString());
                cboThuoc.SelectedValue = drThuoc["ThuocGUID"].ToString();
                numSoLuong.Value = (Decimal)Convert.ToInt32(drThuoc["SoLuong"]);
                if (_type == LoaiToaThuoc.Chung)
                {
                    chkSang.Checked = Convert.ToBoolean(drThuoc["Sang"]);
                    txtSangNote.Text = drThuoc["SangNote"].ToString();
                    chkTrua.Checked = Convert.ToBoolean(drThuoc["Trua"]);
                    txtTruaNote.Text = drThuoc["TruaNote"].ToString();
                    chkChieu.Checked = Convert.ToBoolean(drThuoc["Chieu"]);
                    txtChieuNote.Text = drThuoc["ChieuNote"].ToString();
                    chkToi.Checked = Convert.ToBoolean(drThuoc["Toi"]);
                    txtToiNote.Text = drThuoc["ToiNote"].ToString();
                    chkTruocAn.Checked = Convert.ToBoolean(drThuoc["TruocAn"]);
                    txtTruocAnNote.Text = drThuoc["TruocAnNote"].ToString();
                    chkSauAn.Checked = Convert.ToBoolean(drThuoc["SauAn"]);
                    txtSauAnNote.Text = drThuoc["SauAnNote"].ToString();
                    chkKhac_TruocSauAn.Checked = Convert.ToBoolean(drThuoc["Khac_TruocSauAn"]);
                    txtKhac_TruocSauAnNote.Text = drThuoc["Khac_TruocSauAnNote"].ToString();
                    chkUong.Checked = Convert.ToBoolean(drThuoc["Uong"]);
                    txtUongNote.Text = drThuoc["UongNote"].ToString();
                    chkBoi.Checked = Convert.ToBoolean(drThuoc["Boi"]);
                    txtBoiNote.Text = drThuoc["BoiNote"].ToString();
                    chkDatAD.Checked = Convert.ToBoolean(drThuoc["Dat"]);
                    txtDatADNote.Text = drThuoc["DatNote"].ToString();
                    chkKhac_CachDung.Checked = Convert.ToBoolean(drThuoc["Khac_CachDung"]);
                    txtKhac_CachDungNote.Text = drThuoc["Khac_CachDungNote"].ToString();
                }
                else
                {
                    txtLieuDung.Text = drThuoc["LieuDung"].ToString();
                    txtGhiChu.Text = drThuoc["Note"].ToString();
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
            if (cboThuoc.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 thuốc.", IconType.Information);
                cboThuoc.Focus();
                return false;
            }

            if (_dataSource != null && _dataSource.Rows.Count > 0)
            {
                foreach (DataRow row in _dataSource.Rows)
                {
                    string thuocGUID = row["ThuocGUID"].ToString();
                    if (_isNew)
                    {
                        if (thuocGUID == cboThuoc.SelectedValue.ToString())
                        {
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã được kê toa rồi. Vui lòng chọn thuốc khác.", cboThuoc.Text), IconType.Information);
                            cboThuoc.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        string chiTietToaThuocGUID = string.Empty;
                        if (row["ChiTietToaThuocGUID"] != null && row["ChiTietToaThuocGUID"] != DBNull.Value)
                            chiTietToaThuocGUID = row["ChiTietToaThuocGUID"].ToString();

                        if (thuocGUID == cboThuoc.SelectedValue.ToString() && 
                            _chiTietToaThuoc.ChiTietToaThuocGUID.ToString() != chiTietToaThuocGUID)
                        {
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã được kê toa rồi. Vui lòng chọn thuốc khác.", cboThuoc.Text), IconType.Information);
                            cboThuoc.Focus();
                            return false;
                        }
                    }
                }
            }

            string maThuocGUID = cboThuoc.SelectedValue.ToString();
            Result result = LoThuocBus.CheckThuocHetHan(maThuocGUID);
            if (result.IsOK)
            {
                if (Convert.ToBoolean(result.QueryResult))
                {
                    MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã hết hạn sử dụng. Vui lòng chọn thuốc khác.", cboThuoc.Text), IconType.Information);
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.CheckThuocHetHan"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.CheckThuocHetHan"));
                return false;
            }

            int soLuong = (int)numSoLuong.Value;
            result = LoThuocBus.CheckThuocTonKho(maThuocGUID, soLuong);
            if (result.IsOK)
            {
                if (!Convert.ToBoolean(result.QueryResult))
                {
                    MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã hết hoặc không đủ số lượng để bán. Vui lòng chọn thuốc khác.", cboThuoc.Text), IconType.Information);
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.CheckThuocTonKho"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.CheckThuocTonKho"));
                return false;
            }

            return true;
        }

        private void SetInfo()
        {
            try
            {
                _chiTietToaThuoc.ThuocGUID = Guid.Parse(cboThuoc.SelectedValue.ToString());
                _chiTietToaThuoc.SoLuong = (int)numSoLuong.Value;
                if (_isNew)
                {
                    _chiTietToaThuoc.CreatedDate = DateTime.Now;
                    _chiTietToaThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _chiTietToaThuoc.UpdatedDate = DateTime.Now;
                    _chiTietToaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _chiTietToaThuoc.Status = (byte)Status.Actived;

                _chiTietToaThuoc.Sang = chkSang.Checked;
                _chiTietToaThuoc.SangNote = txtSangNote.Text;
                _chiTietToaThuoc.Trua = chkTrua.Checked;
                _chiTietToaThuoc.TruaNote = txtTruaNote.Text;
                _chiTietToaThuoc.Chieu = chkChieu.Checked;
                _chiTietToaThuoc.ChieuNote = txtChieuNote.Text;
                _chiTietToaThuoc.Toi = chkToi.Checked;
                _chiTietToaThuoc.ToiNote = txtToiNote.Text;
                _chiTietToaThuoc.TruocAn = chkTruocAn.Checked;
                _chiTietToaThuoc.TruocAnNote = txtTruocAnNote.Text;
                _chiTietToaThuoc.SauAn = chkSauAn.Checked;
                _chiTietToaThuoc.SauAnNote = txtSauAnNote.Text;
                _chiTietToaThuoc.Khac_TruocSauAn = chkKhac_TruocSauAn.Checked;
                _chiTietToaThuoc.Khac_TruocSauAnNote = txtKhac_TruocSauAnNote.Text;
                _chiTietToaThuoc.Uong = chkUong.Checked;
                _chiTietToaThuoc.UongNote = txtUongNote.Text;
                _chiTietToaThuoc.Boi = chkBoi.Checked;
                _chiTietToaThuoc.BoiNote = txtBoiNote.Text;
                _chiTietToaThuoc.Dat = chkDatAD.Checked;
                _chiTietToaThuoc.DatNote = txtDatADNote.Text;
                _chiTietToaThuoc.Khac_CachDung = chkKhac_CachDung.Checked;
                _chiTietToaThuoc.Khac_CachDungNote = txtKhac_CachDungNote.Text;
                _chiTietToaThuoc.LieuDung = txtLieuDung.Text;
                _chiTietToaThuoc.Note = txtGhiChu.Text;
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddThuocKeToa_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drThuoc);
        }

        private void dlgAddThuocKeToa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SetInfo();
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin kê toa thuốc ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!CheckInfo()) e.Cancel = true;
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SetInfo();
                    }
                }
            }
        }

        private void chkSang_CheckedChanged(object sender, EventArgs e)
        {
            txtSangNote.ReadOnly = !chkSang.Checked;
            if (!chkSang.Checked) txtSangNote.Text = string.Empty;
        }

        private void chkTrua_CheckedChanged(object sender, EventArgs e)
        {
            txtTruaNote.ReadOnly = !chkTrua.Checked;
            if (!chkTrua.Checked) txtTruaNote.Text = string.Empty;
        }

        private void chkChieu_CheckedChanged(object sender, EventArgs e)
        {
            txtChieuNote.ReadOnly = !chkChieu.Checked;
            if (!chkChieu.Checked) txtChieuNote.Text = string.Empty;
        }

        private void chkToi_CheckedChanged(object sender, EventArgs e)
        {
            txtToiNote.ReadOnly = !chkToi.Checked;
            if (!chkToi.Checked) txtToiNote.Text = string.Empty;
        }

        private void chkTruocAn_CheckedChanged(object sender, EventArgs e)
        {
            txtTruocAnNote.ReadOnly = !chkTruocAn.Checked;
            if (!chkTruocAn.Checked) txtTruocAnNote.Text = string.Empty;
        }

        private void chkSauAn_CheckedChanged(object sender, EventArgs e)
        {
            txtSauAnNote.ReadOnly = !chkSauAn.Checked;
            if (!chkSauAn.Checked) txtSauAnNote.Text = string.Empty;
        }

        private void chkUong_CheckedChanged(object sender, EventArgs e)
        {
            txtUongNote.ReadOnly = !chkUong.Checked;
            if (!chkUong.Checked) txtUongNote.Text = string.Empty;
        }

        private void chkBoi_CheckedChanged(object sender, EventArgs e)
        {
            txtBoiNote.ReadOnly = !chkBoi.Checked;
            if (!chkBoi.Checked) txtBoiNote.Text = string.Empty;
        }

        private void chkDatAD_CheckedChanged(object sender, EventArgs e)
        {
            txtDatADNote.ReadOnly = !chkDatAD.Checked;
            if (!chkDatAD.Checked) txtDatADNote.Text = string.Empty;
        }

        private void chkKhac_TruocSauAn_CheckedChanged(object sender, EventArgs e)
        {
            txtKhac_TruocSauAnNote.ReadOnly = !chkKhac_TruocSauAn.Checked;
            if (!chkKhac_TruocSauAn.Checked) txtKhac_TruocSauAnNote.Text = string.Empty;
        }

        private void chkKhac_CachDung_CheckedChanged(object sender, EventArgs e)
        {
            txtKhac_CachDungNote.ReadOnly = !chkKhac_CachDung.Checked;
            if (!chkKhac_CachDung.Checked) txtKhac_CachDungNote.Text = string.Empty;
        }

        private void btnThuocThayThe_Click(object sender, EventArgs e)
        {
            if (cboThuoc.Text == string.Empty) return;
            string thuocGUID = cboThuoc.SelectedValue.ToString();
            dlgThuocThayThe dlg = new dlgThuocThayThe(thuocGUID);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboThuoc.SelectedValue = dlg.ThuocThayThe;
            }
        }

        private void btnChonThuoc_Click(object sender, EventArgs e)
        {
            DataTable dtThuoc = cboThuoc.DataSource as DataTable;
            dlgSelectSingleThuoc dlg = new dlgSelectSingleThuoc(dtThuoc);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboThuoc.SelectedValue = dlg.MaThuocGUID;
            }
        }
        #endregion
    }
}
