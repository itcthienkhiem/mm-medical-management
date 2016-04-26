/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
    public partial class uKetQuaNoiSoiMui : UserControl
    {
        #region Members
        private DataTable _dtKetQuaNiemMac = null;
        private DataTable _dtKetQuaVachNgan = null;
        private DataTable _dtKetQuaKheTren = null;
        private DataTable _dtKetQuaKheGiua = null;
        private DataTable _dtKetQuaCuonGiua = null;
        private DataTable _dtKetQuaCuonDuoi = null;
        private DataTable _dtKetQuaMomMoc = null;
        private DataTable _dtKetQuaBongSang = null;
        private DataTable _dtKetQuaVom = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiMui()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string NiemMacPhai
        {
            get { return GetValue(0, 0); }
            set { SetValue(0, 0, value); }
        }

        public string NiemMacTrai
        {
            get { return GetValue(0, 2); }
            set { SetValue(0, 2, value); }
        }

        public string VachNganPhai
        {
            get { return GetValue(1, 0); }
            set { SetValue(1, 0, value); }
        }

        public string VachNganTrai
        {
            get { return GetValue(1, 2); }
            set { SetValue(1, 2, value); }
        }

        public string KheTrenPhai
        {
            get { return GetValue(2, 0); }
            set { SetValue(2, 0, value); }
        }

        public string KheTrenTrai
        {
            get { return GetValue(2, 2); }
            set { SetValue(2, 2, value); }
        }

        public string KheGiuaPhai
        {
            get { return GetValue(3, 0); }
            set { SetValue(3, 0, value); }
        }

        public string KheGiuaTrai
        {
            get { return GetValue(3, 2); }
            set { SetValue(3, 2, value); }
        }

        public string CuonGiuaPhai
        {
            get { return GetValue(4, 0); }
            set { SetValue(4, 0, value); }
        }

        public string CuonGiuaTrai
        {
            get { return GetValue(4, 2); }
            set { SetValue(4, 2, value); }
        }

        public string CuonDuoiPhai
        {
            get { return GetValue(5, 0); }
            set { SetValue(5, 0, value); }
        }

        public string CuonDuoiTrai
        {
            get { return GetValue(5, 2); }
            set { SetValue(5, 2, value); }
        }

        public string MomMocPhai
        {
            get { return GetValue(6, 0); }
            set { SetValue(6, 0, value); }
        }

        public string MomMocTrai
        {
            get { return GetValue(6, 2); }
            set { SetValue(6, 2, value); }
        }

        public string BongSangPhai
        {
            get { return GetValue(7, 0); }
            set { SetValue(7, 0, value); }
        }

        public string BongSangTrai
        {
            get { return GetValue(7, 2); }
            set { SetValue(7, 2, value); }
        }

        public string VomPhai
        {
            get { return GetValue(8, 0); }
            set { SetValue(8, 0, value); }
        }

        public string VomTrai
        {
            get { return GetValue(8, 2); }
            set { SetValue(8, 2, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            NiemMacPhai = "Hồng";
            NiemMacTrai = "Hồng";
            VachNganPhai = "Thẳng";
            VachNganTrai = "Thẳng";
            KheTrenPhai = "Thông Thoáng";
            KheTrenTrai = "Thông Thoáng";
            KheGiuaPhai = "Thông Thoáng";
            KheGiuaTrai = "Thông Thoáng";
            CuonGiuaPhai = "Không Phì Đại";
            CuonGiuaTrai = "Không Phì Đại";
            CuonDuoiPhai = "Không Phì Đại";
            CuonDuoiTrai = "Không Phì Đại";
            MomMocPhai = "Chưa Thoái Hóa";
            MomMocTrai = "Chưa Thoái Hóa";
            BongSangPhai = "Chưa Thoái Hóa";
            BongSangTrai = "Chưa Thoái Hóa";
            VomPhai = "Thoáng";
            VomTrai = "Thoáng";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("Hồng", "NIÊM MẠC", "Hồng");
            dgKetQuaNoiSoi.Rows.Add("Thẳng", "VÁCH NGĂN", "Thẳng");
            dgKetQuaNoiSoi.Rows.Add("Thông Thoáng", "KHE TRÊN", "Thông Thoáng");
            dgKetQuaNoiSoi.Rows.Add("Thông Thoáng", "KHE GIỮA", "Thông Thoáng");
            dgKetQuaNoiSoi.Rows.Add("Không Phì Đại", "CUỐN GIỮA", "Không Phì Đại");
            dgKetQuaNoiSoi.Rows.Add("Không Phì Đại", "CUỐN DƯỚI", "Không Phì Đại");
            dgKetQuaNoiSoi.Rows.Add("Chưa Thoái Hóa", "MÕM MÓC", "Chưa Thoái Hóa");
            dgKetQuaNoiSoi.Rows.Add("Chưa Thoái Hóa", "BÓNG SÀNG", "Chưa Thoái Hóa");
            dgKetQuaNoiSoi.Rows.Add("Thoáng", "VÒM", "Thoáng");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiNiemMac);
            if (result.IsOK) _dtKetQuaNiemMac = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiVachNgan);
            if (result.IsOK) _dtKetQuaVachNgan = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiKheTren);
            if (result.IsOK) _dtKetQuaKheTren = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiKheGiua);
            if (result.IsOK) _dtKetQuaKheGiua = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiCuonGiua);
            if (result.IsOK) _dtKetQuaCuonGiua = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiCuonDuoi);
            if (result.IsOK) _dtKetQuaCuonDuoi = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMomMoc);
            if (result.IsOK) _dtKetQuaMomMoc = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiBongSang);
            if (result.IsOK) _dtKetQuaBongSang = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiVom);
            if (result.IsOK) _dtKetQuaVom = result.QueryResult as DataTable;
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
                if (_dtKetQuaNiemMac != null && _dtKetQuaNiemMac.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaNiemMac.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaVachNgan != null && _dtKetQuaVachNgan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaVachNgan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaKheTren != null && _dtKetQuaKheTren.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaKheTren.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaKheGiua != null && _dtKetQuaKheGiua.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaKheGiua.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaCuonGiua != null && _dtKetQuaCuonGiua.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaCuonGiua.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaCuonDuoi != null && _dtKetQuaCuonDuoi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaCuonDuoi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 6)
            {
                if (_dtKetQuaMomMoc != null && _dtKetQuaMomMoc.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMomMoc.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 7)
            {
                if (_dtKetQuaBongSang != null && _dtKetQuaBongSang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaBongSang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 8)
            {
                if (_dtKetQuaVom != null && _dtKetQuaVom.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaVom.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
