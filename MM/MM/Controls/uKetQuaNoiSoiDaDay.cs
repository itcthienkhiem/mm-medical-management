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
    public partial class uKetQuaNoiSoiDaDay : UserControl
    {
        #region Members
        private DataTable _dtKetQuaThucQuan = null;
        private DataTable _dtKetQuaDaDay = null;
        private DataTable _dtKetQuaHangVi = null;
        private DataTable _dtKetQuaMonVi = null;
        private DataTable _dtKetQuaHanhTaTrang = null;
        private DataTable _dtKetQuaClotest = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiDaDay()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string ThucQuan
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string DaDay
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string HangVi
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string MonVi
        {
            get { return GetValue(3, 1); }
            set { SetValue(3, 1, value); }
        }

        public string HanhTaTrang
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string Clotest
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            ThucQuan = "Bình thường";
            DaDay = "Dạ dày sạch, không ứ đọng. Tâm vị, thân vị, hang vị bình thường, bờ cong nhỏ không loét, không u. Lỗ môn vị tròn co bóp tốt.";
            HangVi = "Bình thường";
            MonVi = "Lỗ môn vị tròn co bóp tốt.";
            HanhTaTrang = "Không biến dạng";
            Clotest = "Âm tính";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("THỰC QUẢN", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("DẠ DÀY", "Dạ dày sạch, không ứ đọng. Tâm vị, thân vị, hang vị bình thường, bờ cong nhỏ không loét, không u. Lỗ môn vị tròn co bóp tốt.");
            dgKetQuaNoiSoi.Rows.Add("HANG VỊ", "Bình thường");
            dgKetQuaNoiSoi.Rows.Add("MÔN VỊ", "Lỗ môn vị tròn co bóp tốt.");
            dgKetQuaNoiSoi.Rows.Add("HÀNH TÁ TRÀNG", "Không biến dạng");
            dgKetQuaNoiSoi.Rows.Add("CLOTEST", "Âm tính");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiThucQuan);
            if (result.IsOK) _dtKetQuaThucQuan = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiDaDay);
            if (result.IsOK) _dtKetQuaDaDay= result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiHangVi);
            if (result.IsOK) _dtKetQuaHangVi = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMonVi);
            if (result.IsOK) _dtKetQuaMonVi = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiHanhTaTrang);
            if (result.IsOK) _dtKetQuaHanhTaTrang = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiClotest);
            if (result.IsOK) _dtKetQuaClotest = result.QueryResult as DataTable;
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
                if (_dtKetQuaThucQuan != null && _dtKetQuaThucQuan.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaThucQuan.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 1)
            {
                if (_dtKetQuaDaDay != null && _dtKetQuaDaDay.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaDaDay.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaHangVi != null && _dtKetQuaHangVi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaHangVi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaMonVi != null && _dtKetQuaMonVi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMonVi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 4)
            {
                if (_dtKetQuaHanhTaTrang != null && _dtKetQuaHanhTaTrang.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaHanhTaTrang.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 5)
            {
                if (_dtKetQuaClotest != null && _dtKetQuaClotest.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaClotest.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion
    }
}
