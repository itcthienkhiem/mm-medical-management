using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgEditChiTietKetQuaXetNghiemTay : dlgBase
    {
        #region Members
        private DataRow _drChiTietKQXN = null;
        private ChiTietKetQuaXetNghiem_Manual _chiTietKQXN = new ChiTietKetQuaXetNghiem_Manual();
        #endregion

        #region Constructor
        public dlgEditChiTietKetQuaXetNghiemTay(DataRow drChiTietKQXN)
        {
            InitializeComponent();
            _drChiTietKQXN = drChiTietKQXN;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            txtTenXetNghiem.Text = _drChiTietKQXN["Fullname"].ToString();
            txtResult.Text = _drChiTietKQXN["TestResult"].ToString();
            txtDonVi.Text = _drChiTietKQXN["DonVi"].ToString();

            if (_drChiTietKQXN["FromValue"] != null && _drChiTietKQXN["FromValue"] != DBNull.Value)
            {
                numFromValue.Value = (Decimal)Convert.ToDouble(_drChiTietKQXN["FromValue"]);
                chkFromValue.Checked = true;
            }

            if (_drChiTietKQXN["ToValue"] != null && _drChiTietKQXN["ToValue"] != DBNull.Value)
            {
                numToValue.Value = (Decimal)Convert.ToDouble(_drChiTietKQXN["ToValue"]);
                chkToValue.Checked = true;
            }

            _chiTietKQXN.ChiTietKetQuaXetNghiem_ManualGUID = Guid.Parse(_drChiTietKQXN["ChiTietKetQuaXetNghiem_ManualGUID"].ToString());
        }

        private bool CheckInfo()
        {
            if (txtResult.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập kết quả.", IconType.Information);
                txtResult.Focus();
                return false;
            }

            return true;
        }

        private bool SaveInfo()
        {
            _chiTietKQXN.TestResult = txtResult.Text;

            if (chkFromValue.Checked)
                _chiTietKQXN.FromValue = (double)numFromValue.Value;

            if (chkToValue.Checked)
                _chiTietKQXN.ToValue = (double)numToValue.Value;

            _chiTietKQXN.DonVi = txtDonVi.Text;

            Result result = KetQuaXetNghiemTayBus.UpdateChiTietKQXN(_chiTietKQXN);
            if (!result.IsOK)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateChiTietKQXN"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateChiTietKQXN"));
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgEditChiTietKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgEditChiTietKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else if (!SaveInfo()) e.Cancel = true;
            }
        }

        private void chkFromValue_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromValue.Checked;
        }

        private void chkToValue_CheckedChanged(object sender, EventArgs e)
        {
            numToValue.Enabled = chkToValue.Checked;
        }
        #endregion
    }
}
