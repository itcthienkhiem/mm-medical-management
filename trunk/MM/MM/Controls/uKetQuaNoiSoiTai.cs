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

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiTai : uBase
    {
        #region Members
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTai()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string OngTaiTrai
        {
            get { return GetValue(0, 0); }
            set { SetValue(0, 0, value); }
        }

        public string OngTaiPhai
        {
            get { return GetValue(0, 2); }
            set { SetValue(0, 2, value); }
        }

        public string MangNhiTrai
        {
            get { return GetValue(1, 0); }
            set { SetValue(1, 0, value); }
        }

        public string MangNhiPhai
        {
            get { return GetValue(1, 2); }
            set { SetValue(1, 2, value); }
        }

        public string CanBuaTrai
        {
            get { return GetValue(2, 0); }
            set { SetValue(2, 0, value); }
        }

        public string CanBuaPhai
        {
            get { return GetValue(2, 2); }
            set { SetValue(2, 2, value); }
        }

        public string HomNhiTrai
        {
            get { return GetValue(3, 0); }
            set { SetValue(3, 0, value); }
        }

        public string HomNhiPhai
        {
            get { return GetValue(3, 2); }
            set { SetValue(3, 2, value); }
        }

        public string ValsavaTrai
        {
            get { return GetValue(4, 0); }
            set { SetValue(4, 0, value); }
        }

        public string ValsavaPhai
        {
            get { return GetValue(4, 2); }
            set { SetValue(4, 2, value); }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("", "ỐNG TAI", "");
            dgKetQuaNoiSoi.Rows.Add("", "MÀNG NHĨ", "");
            dgKetQuaNoiSoi.Rows.Add("", "CÁN BÚA", "");
            dgKetQuaNoiSoi.Rows.Add("", "HÒM NHĨ", "");
            dgKetQuaNoiSoi.Rows.Add("", "VALSAVA", "");
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
