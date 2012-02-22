using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiHongThanhQuan : UserControl
    {
        #region Members
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
        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("AMYDALE", "");
            dgKetQuaNoiSoi.Rows.Add("XOANG LÊ", "");
            dgKetQuaNoiSoi.Rows.Add("MIỆNG THỰC QUẢN", "");
            dgKetQuaNoiSoi.Rows.Add("SỤN PHỂU", "");
            dgKetQuaNoiSoi.Rows.Add("DÂY THANH", "");
            dgKetQuaNoiSoi.Rows.Add("BĂNG THANH THẤT", "");
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

        #endregion
    }
}
