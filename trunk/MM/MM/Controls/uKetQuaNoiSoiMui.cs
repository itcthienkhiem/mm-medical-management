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

        #endregion

        #region Constructor
        public uKetQuaNoiSoiMui()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string NiemMacTrai
        {
            get { return GetValue(0, 0); }
            set { SetValue(0, 0, value); }
        }

        public string NiemMacPhai
        {
            get { return GetValue(0, 2); }
            set { SetValue(0, 2, value); }
        }

        public string VachNganTrai
        {
            get { return GetValue(1, 0); }
            set { SetValue(1, 0, value); }
        }

        public string VachNganPhai
        {
            get { return GetValue(1, 2); }
            set { SetValue(1, 2, value); }
        }

        public string KheTrenTrai
        {
            get { return GetValue(2, 0); }
            set { SetValue(2, 0, value); }
        }

        public string KheTrenPhai
        {
            get { return GetValue(2, 2); }
            set { SetValue(2, 2, value); }
        }

        public string KheGiuaTrai
        {
            get { return GetValue(3, 0); }
            set { SetValue(3, 0, value); }
        }

        public string KheGiuaPhai
        {
            get { return GetValue(3, 2); }
            set { SetValue(3, 2, value); }
        }

        public string CuonGiuaTrai
        {
            get { return GetValue(4, 0); }
            set { SetValue(4, 0, value); }
        }

        public string CuonGiuaPhai
        {
            get { return GetValue(4, 2); }
            set { SetValue(4, 2, value); }
        }

        public string CuonDuoiTrai
        {
            get { return GetValue(5, 0); }
            set { SetValue(5, 0, value); }
        }

        public string CuonDuoiPhai
        {
            get { return GetValue(5, 2); }
            set { SetValue(5, 2, value); }
        }

        public string MomMocTrai
        {
            get { return GetValue(6, 0); }
            set { SetValue(6, 0, value); }
        }

        public string MomMocPhai
        {
            get { return GetValue(6, 2); }
            set { SetValue(6, 2, value); }
        }

        public string BongSangTrai
        {
            get { return GetValue(7, 0); }
            set { SetValue(7, 0, value); }
        }

        public string BongSangPhai
        {
            get { return GetValue(7, 2); }
            set { SetValue(7, 2, value); }
        }

        public string VomTrai
        {
            get { return GetValue(8, 0); }
            set { SetValue(8, 0, value); }
        }

        public string VomPhai
        {
            get { return GetValue(8, 2); }
            set { SetValue(8, 2, value); }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("", "NIÊM MẠC", "");
            dgKetQuaNoiSoi.Rows.Add("", "VÁCH NGĂN", "");
            dgKetQuaNoiSoi.Rows.Add("", "KHE TRÊN", "");
            dgKetQuaNoiSoi.Rows.Add("", "KHE GIỮA", "");
            dgKetQuaNoiSoi.Rows.Add("", "CUỐN GIỮA", "");
            dgKetQuaNoiSoi.Rows.Add("", "CUỐN DƯỚI", "");
            dgKetQuaNoiSoi.Rows.Add("", "MÕM MÓC", "");
            dgKetQuaNoiSoi.Rows.Add("", "BÓNG SÀNG", "");
            dgKetQuaNoiSoi.Rows.Add("", "VÒM", "");
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
