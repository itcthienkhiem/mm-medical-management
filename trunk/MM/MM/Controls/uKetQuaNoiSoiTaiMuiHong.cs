using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiTaiMuiHong : UserControl
    {
        #region Members
        private DataTable _dtKetQuaOngTai = null;
        private DataTable _dtKetQuaMangNhi = null;
        private DataTable _dtKetQuaNiemMac = null;
        private DataTable _dtKetQuaVachNgan = null;
        private DataTable _dtKetQuaKheTren = null;
        private DataTable _dtKetQuaKheGiua = null;
        private DataTable _dtKetQuaMomMocBongSang = null;
        private DataTable _dtKetQuaVom = null;
        private DataTable _dtKetQuaAmydale = null;
        private DataTable _dtKetQuaThanhQuan = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTaiMuiHong()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string OngTaiNgoai
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string MangNhi
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string NiemMacMui
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string VachNgan
        {
            get { return GetValue(3, 1); }
            set { SetValue(3, 1, value); }
        }

        public string KheTren
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string KheGiua
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }

        public string MomMocBongSang
        {
            get { return GetValue(6, 1); }
            set { SetValue(6, 1, value); }
        }

        public string Vom
        {
            get { return GetValue(7, 1); }
            set { SetValue(7, 1, value); }
        }

        public string Amydale
        {
            get { return GetValue(8, 1); }
            set { SetValue(8, 1, value); }
        }

        public string ThanhQuan
        {
            get { return GetValue(9, 1); }
            set { SetValue(9, 1, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            OngTaiNgoai = "Khô";
            MangNhi = "Còn";
            NiemMacMui = "Hồng";
            VachNgan = "Thẳng";
            KheTren = "Thông Thoáng";
            KheGiua = "Thông Thoáng";
            MomMocBongSang = "Chưa Thoái Hóa";
            Vom = "Thoáng";
            Amydale = "Teo hốc";
            ThanhQuan = "Không phát hiện bất thường";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("ỐNG TAI NGOÀI", "Khô");
            dgKetQuaNoiSoi.Rows.Add("MÀNG NHĨ", "Còn");
            dgKetQuaNoiSoi.Rows.Add("NIÊM MẠC MŨI", "Hồng");
            dgKetQuaNoiSoi.Rows.Add("VÁCH NGĂN", "Thẳng");
            dgKetQuaNoiSoi.Rows.Add("KHE TRÊN", "Thông Thoáng");
            dgKetQuaNoiSoi.Rows.Add("KHE GIỮA", "Thông Thoáng");
            dgKetQuaNoiSoi.Rows.Add("MÕM MÓC-BÓNG SÀNG", "Chưa Thoái Hóa");
            dgKetQuaNoiSoi.Rows.Add("VÒM", "Thoáng");
            dgKetQuaNoiSoi.Rows.Add("AMYDALE", "Teo hốc");
            dgKetQuaNoiSoi.Rows.Add("THANH QUẢN", "Không phát hiện bất thường");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiOngTai);
            if (result.IsOK) _dtKetQuaOngTai = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMangNhi);
            if (result.IsOK) _dtKetQuaMangNhi = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiNiemMac);
            if (result.IsOK) _dtKetQuaNiemMac = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiVachNgan);
            if (result.IsOK) _dtKetQuaVachNgan = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiKheTren);
            if (result.IsOK) _dtKetQuaKheTren = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiKheGiua);
            if (result.IsOK) _dtKetQuaKheGiua = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMomMoc_BongSang);
            if (result.IsOK) _dtKetQuaMomMocBongSang = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiVom);
            if (result.IsOK) _dtKetQuaVom = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiAmydale);
            if (result.IsOK) _dtKetQuaAmydale = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiThanhQuan);
            if (result.IsOK) _dtKetQuaThanhQuan = result.QueryResult as DataTable;
        }

        private string GetValue(int rowIndex, int colIndex)
        {
            if (dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value != null && dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value != DBNull.Value)
                return dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value.ToString();
            else
                return string.Empty;
        }

        private void SetValue(int rowIndex, int colIndex, string value)
        {
            dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value = value;
        }
        #endregion

        #region Window Event Handlers
        private void dgKetQuaNoiSoi_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cbo = e.Control as ComboBox;
            if (cbo == null) return;

            int rowIndex = dgKetQuaNoiSoi.CurrentRow.Index;
            if (rowIndex < 0) return;

            
            if (rowIndex == 0)
            {
                if (_dtKetQuaOngTai != null && _dtKetQuaOngTai.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaOngTai.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaMangNhi != null && _dtKetQuaMangNhi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMangNhi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaNiemMac != null && _dtKetQuaNiemMac.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaNiemMac.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaVachNgan != null && _dtKetQuaVachNgan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaVachNgan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaKheTren != null && _dtKetQuaKheTren.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaKheTren.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaKheGiua != null && _dtKetQuaKheGiua.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaKheGiua.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 6)
            {
                if (_dtKetQuaMomMocBongSang != null && _dtKetQuaMomMocBongSang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMomMocBongSang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 7)
            {
                if (_dtKetQuaVom != null && _dtKetQuaVom.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaVom.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 8)
            {
                if (_dtKetQuaAmydale != null && _dtKetQuaAmydale.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaAmydale.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 9)
            {
                if (_dtKetQuaThanhQuan != null && _dtKetQuaThanhQuan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaThanhQuan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
