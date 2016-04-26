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
    public partial class uKetQuaSoiCTC : UserControl
    {
        #region Members
        private DataTable _dtKetQuaAmHo = null;
        private DataTable _dtKetQuaAmDao = null;
        private DataTable _dtKetQuaCTC = null;
        private DataTable _dtKetQuaBieuMoLat = null;
        private DataTable _dtKetQuaMoDem = null;
        private DataTable _dtKetQuaRanhGioiLatTru = null;
        private DataTable _dtKetQuaSauAcidAcetic = null;
        private DataTable _dtKetQuaSauLugol = null;
        #endregion

        #region Constructor
        public uKetQuaSoiCTC()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string AmHo
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string AmDao
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string CTC
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string BieuMoLat
        {
            get { return GetValue(3, 1); }
            set { SetValue(3, 1, value); }
        }

        public string MoDem
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string RanhGioiLatTru
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }

        public string SauAcidAcetic
        {
            get { return GetValue(6, 1); }
            set { SetValue(6, 1, value); }
        }

        public string SauLugol
        {
            get { return GetValue(7, 1); }
            set { SetValue(7, 1, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            AmHo = "Bình thường";
            AmDao = "Có ít huyết trắng";
            CTC = "Kích thước 2.5 cm";
            BieuMoLat = "Láng";
            MoDem = "Mạch máu bình thường";
            RanhGioiLatTru = "Lỗ ngoài";
            SauAcidAcetic = "Không bất thường";
            SauLugol = "Bắt màu đều";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("ÂM HỘ", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ÂM ĐẠO", "Có ít huyết trắng");
            dgKetQuaNoiSoi.Rows.Add("CTC", "Kích thước 2.5 cm");
            dgKetQuaNoiSoi.Rows.Add("BIỂU MÔ LÁT", "Láng");
            dgKetQuaNoiSoi.Rows.Add("MÔ ĐỆM", "Mạch máu bình thường");
            dgKetQuaNoiSoi.Rows.Add("RANH GIỚI LÁT TRỤ", "Lỗ ngoài");
            dgKetQuaNoiSoi.Rows.Add("SAU ACID ACETIC", "Không bất thường");
            dgKetQuaNoiSoi.Rows.Add("SAU LUGOL", "Bắt màu đều");
            
            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiAmHo);
            if (result.IsOK) _dtKetQuaAmHo = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiAmDao);
            if (result.IsOK) _dtKetQuaAmDao = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiCTC);
            if (result.IsOK) _dtKetQuaCTC = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiBieuMoLat);
            if (result.IsOK) _dtKetQuaBieuMoLat = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiMoDem);
            if (result.IsOK) _dtKetQuaMoDem = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiRanhGioiLatTru);
            if (result.IsOK) _dtKetQuaRanhGioiLatTru = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiSauAcidAcetic);
            if (result.IsOK) _dtKetQuaSauAcidAcetic = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaSoiSauLugol);
            if (result.IsOK) _dtKetQuaSauLugol = result.QueryResult as DataTable;
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
                if (_dtKetQuaAmHo != null && _dtKetQuaAmHo.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaAmHo.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaAmDao != null && _dtKetQuaAmDao.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaAmDao.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaCTC != null && _dtKetQuaCTC.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaCTC.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaBieuMoLat != null && _dtKetQuaBieuMoLat.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaBieuMoLat.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaMoDem != null && _dtKetQuaMoDem.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMoDem.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaRanhGioiLatTru != null && _dtKetQuaRanhGioiLatTru.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaRanhGioiLatTru.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 6)
            {
                if (_dtKetQuaSauAcidAcetic != null && _dtKetQuaSauAcidAcetic.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaSauAcidAcetic.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 7)
            {
                if (_dtKetQuaSauLugol != null && _dtKetQuaSauLugol.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaSauLugol.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
