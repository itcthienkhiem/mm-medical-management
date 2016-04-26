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
    public partial class uKetQuaNoiSoiHongThanhQuan : UserControl
    {
        #region Members
        private DataTable _dtKetQuaAmydale = null;
        private DataTable _dtKetQuaXoangLe = null;
        private DataTable _dtKetQuaMiengThucQuan = null;
        private DataTable _dtKetQuaSunPheu = null;
        private DataTable _dtKetQuaDayThanh = null;
        private DataTable _dtKetQuaBangThanhThat = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiHongThanhQuan()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string Amydale
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string XoangLe
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string MiengThucQuan
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string SunPheu
        {
            get { return GetValue(3, 1); }
            set { SetValue(3, 1, value); }
        }

        public string DayThanh
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string BangThanhThat
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            Amydale = "Teo hốc";
            XoangLe = "Thông Thoáng";
            MiengThucQuan = "Niêm Mạc Hồng";
            SunPheu = "Niêm Mạc Hồng, Di Động Tốt";
            DayThanh = "Trơn láng, Di Động Tốt";
            BangThanhThat = "Thông Thoáng";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("AMYDALE", "Teo hốc");
            dgKetQuaNoiSoi.Rows.Add("XOANG LÊ", "Thông Thoáng");
            dgKetQuaNoiSoi.Rows.Add("MIỆNG THỰC QUẢN", "Niêm Mạc Hồng");
            dgKetQuaNoiSoi.Rows.Add("SỤN PHỂU", "Niêm Mạc Hồng, Di Động Tốt");
            dgKetQuaNoiSoi.Rows.Add("DÂY THANH", "Trơn láng, Di Động Tốt");
            dgKetQuaNoiSoi.Rows.Add("BĂNG THANH THẤT", "Thông Thoáng");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiAmydale);
            if (result.IsOK) _dtKetQuaAmydale = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiXoangLe);
            if (result.IsOK) _dtKetQuaXoangLe = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMiengThucQuan);
            if (result.IsOK) _dtKetQuaMiengThucQuan = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiSunPheu);
            if (result.IsOK) _dtKetQuaSunPheu = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDayThanh);
            if (result.IsOK) _dtKetQuaDayThanh = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiBangThanhThat);
            if (result.IsOK) _dtKetQuaBangThanhThat = result.QueryResult as DataTable;
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
                if (_dtKetQuaAmydale != null && _dtKetQuaAmydale.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaAmydale.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaXoangLe != null && _dtKetQuaXoangLe.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaXoangLe.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaMiengThucQuan != null && _dtKetQuaMiengThucQuan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMiengThucQuan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaSunPheu != null && _dtKetQuaSunPheu.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaSunPheu.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaDayThanh != null && _dtKetQuaDayThanh.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDayThanh.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaBangThanhThat != null && _dtKetQuaBangThanhThat.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaBangThanhThat.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
