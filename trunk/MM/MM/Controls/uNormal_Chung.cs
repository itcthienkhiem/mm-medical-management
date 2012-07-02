using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Controls
{
    public partial class uNormal_Chung : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Chung()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public bool FromValueChecked
        {
            get { return chkFromValue.Checked; }
            set { chkFromValue.Checked = value; }
        }

        public bool ToValueChecked
        {
            get { return chkToValue.Checked; }
            set { chkToValue.Checked = value; }
        }

        public float FromValue
        {
            get { return (float)numFromValue.Value; }
            set { numFromValue.Value = (Decimal)value; }
        }

        public float ToValue
        {
            get { return (float)numToValue.Value; }
            set { numToValue.Value = (Decimal)value; }
        }

        public string FromOperator
        {
            get { return cboFromOperator.Text; }
            set { cboFromOperator.Text = value; }
        }

        public string ToOperator
        {
            get { return cboToOperator.Text; }
            set { cboToOperator.Text = value; }
        }

        public string DonVi
        {
            get { return txtDonVi.Text; }
            set { txtDonVi.Text = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            cboFromOperator.SelectedIndex = 1;
            cboToOperator.SelectedIndex = 1;
        }

        public bool CheckInfo()
        {
            if (chkFromValue.Checked && chkToValue.Checked && numFromValue.Value >= numToValue.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số từ phải nhỏ hơn chỉ số đến.", IconType.Information);
                numFromValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFromValue_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromValue.Checked;
            cboFromOperator.Enabled = chkFromValue.Checked;

            if (chkFromValue.Checked || chkToValue.Checked)
                txtDonVi.Enabled = true;
            else
                txtDonVi.Enabled = false;
        }

        private void chkToValue_CheckedChanged(object sender, EventArgs e)
        {
            numToValue.Enabled = chkToValue.Checked;
            cboToOperator.Enabled = chkToValue.Checked;

            if (chkToValue.Checked || chkToValue.Checked)
                txtDonVi.Enabled = true;
            else
                txtDonVi.Enabled = false;
        }
        #endregion
    }
}
