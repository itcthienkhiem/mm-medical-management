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
    public partial class uKetQuaNoiSoiTrucTrang : UserControl
    {
        #region Members
        private DataTable _dtKetQuaTrucTrang = null;
        private DataTable _dtKetQuaDaiTrangTrai = null;
        private DataTable _dtKetQuaDaiTrangGocLach = null;
        private DataTable _dtKetQuaDaiTrangNgang = null;
        private DataTable _dtKetQuaDaiTrangGocGan = null;
        private DataTable _dtKetQuaDaiTrangPhai = null;
        private DataTable _dtKetQuaManhTrang = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTrucTrang()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string TrucTrang
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string DaiTrangTrai
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string DaiTrangGocLach
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string DaiTrangNgang
        {
            get { return GetValue(3, 1); }
            set { SetValue(3, 1, value); }
        }

        public string DaiTrangGocGan
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string DaiTrangPhai
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }

        public string ManhTrang
        {
            get { return GetValue(6, 1); }
            set { SetValue(6, 1, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            TrucTrang = "Bình thường";
            DaiTrangTrai = "Bình thường";
            DaiTrangGocLach = "Bình thường";
            DaiTrangNgang = "Bình thường";
            DaiTrangGocGan = "Bình thường";
            DaiTrangPhai = "Bình thường";
            ManhTrang = "Bình thường";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("TRỰC TRÀNG", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ĐẠI TRÀNG TRÁI", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ĐẠI TRANG GÓC LÁCH", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ĐẠI TRÀNG NGANG", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ĐẠI TRÀNG GÓC GAN", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("ĐẠI TRÀNG PHẢI", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("MANH TRÀNG", "Bình thường");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiTrucTrang);
            if (result.IsOK) _dtKetQuaTrucTrang = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaiTrangTrai);
            if (result.IsOK) _dtKetQuaDaiTrangTrai= result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaiTrangGocLach);
            if (result.IsOK) _dtKetQuaDaiTrangGocLach = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaiTrangNgang);
            if (result.IsOK) _dtKetQuaDaiTrangNgang = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaiTrangGocGan);
            if (result.IsOK) _dtKetQuaDaiTrangGocGan = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaiTrangPhai);
            if (result.IsOK) _dtKetQuaDaiTrangPhai = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiManhTrang);
            if (result.IsOK) _dtKetQuaManhTrang = result.QueryResult as DataTable;
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
                if (_dtKetQuaTrucTrang != null && _dtKetQuaTrucTrang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaTrucTrang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaDaiTrangTrai != null && _dtKetQuaDaiTrangTrai.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaiTrangTrai.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaDaiTrangGocLach != null && _dtKetQuaDaiTrangGocLach.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaiTrangGocLach.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaDaiTrangNgang != null && _dtKetQuaDaiTrangNgang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaiTrangNgang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaDaiTrangGocGan != null && _dtKetQuaDaiTrangGocGan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaiTrangGocGan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaDaiTrangPhai != null && _dtKetQuaDaiTrangPhai.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaiTrangPhai.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 6)
            {
                if (_dtKetQuaManhTrang != null && _dtKetQuaManhTrang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaManhTrang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
