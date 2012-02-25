using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using DevComponents.DotNetBar.Controls;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiTongQuat : UserControl
    {
        #region Members
        private DataTable _dtKetQuaOngTai = null;
        private DataTable _dtKetQuaMangNhi = null;
        private DataTable _dtKetQuaCanBua = null;
        private DataTable _dtKetQuaHomNhi = null;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTongQuat()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string OngTaiPhai
        {
            get { return GetValue(0, 0); }
            set { SetValue(0, 0, value); }
        }

        public string OngTaiTrai
        {
            get { return GetValue(0, 2); }
            set { SetValue(0, 2, value); }
        }

        public string MangNhiPhai
        {
            get { return GetValue(1, 0); }
            set { SetValue(1, 0, value); }
        }

        public string MangNhiTrai
        {
            get { return GetValue(1, 2); }
            set { SetValue(1, 2, value); }
        }

        public string CanBuaPhai
        {
            get { return GetValue(2, 0); }
            set { SetValue(2, 0, value); }
        }

        public string CanBuaTrai
        {
            get { return GetValue(2, 2); }
            set { SetValue(2, 2, value); }
        }

        public string HomNhiPhai
        {
            get { return GetValue(3, 0); }
            set { SetValue(3, 0, value); }
        }

        public string HomNhiTrai
        {
            get { return GetValue(3, 2); }
            set { SetValue(3, 2, value); }
        }
        #endregion

        #region UI Command
        public void SetDefault()
        {
            OngTaiPhai = "Khô";
            OngTaiTrai = "Khô";
            MangNhiPhai = "Còn";
            MangNhiTrai = "Còn";
            CanBuaPhai = "Đúng Vị Trí";
            CanBuaTrai = "Đúng Vị Trí";
            HomNhiPhai = "Khô";
            HomNhiTrai = "Khô";
        }

        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("Khô", "ỐNG TAI", "Khô");
            dgKetQuaNoiSoi.Rows.Add("Còn", "MÀNG NHĨ", "Còn");
            dgKetQuaNoiSoi.Rows.Add("Đúng Vị Trí", "CÁN BÚA", "Đúng Vị Trí");
            dgKetQuaNoiSoi.Rows.Add("Khô", "HÒM NHĨ", "Khô");

            Result result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiOngTai);
            if (result.IsOK) _dtKetQuaOngTai = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiMangNhi);
            if (result.IsOK) _dtKetQuaMangNhi = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiCanBua);
            if (result.IsOK) _dtKetQuaCanBua = result.QueryResult as DataTable;

            result = BookmarkBus.GetBookmark(BookMarkType.KetQuaNoiSoiHomNhi);
            if (result.IsOK) _dtKetQuaHomNhi = result.QueryResult as DataTable;
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
                if (_dtKetQuaOngTai != null && _dtKetQuaMangNhi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaMangNhi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 2)
            {
                if (_dtKetQuaOngTai != null && _dtKetQuaCanBua.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaCanBua.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
            else if (rowIndex == 3)
            {
                if (_dtKetQuaOngTai != null && _dtKetQuaHomNhi.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtKetQuaHomNhi.Rows)
                        cbo.Items.Add(row["Value"].ToString());
                }
            }
        }
        #endregion

        private void uKetQuaNoiSoiTongQuat_Load(object sender, EventArgs e)
        {
        }
    }
}
