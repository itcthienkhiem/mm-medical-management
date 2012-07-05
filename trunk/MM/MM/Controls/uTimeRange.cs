using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uTimeRange : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uTimeRange()
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

        public int FromValue
        {
            get { return (int)numFromValue.Value; }
            set { numFromValue.Value = (Decimal)value; }
        }

        public int ToValue
        {
            get { return (int)numToValue.Value; }
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
        #endregion

        #region UI Command
        private void InitData()
        {
            cboFromOperator.SelectedIndex = 1;
            cboToOperator.SelectedIndex = 1;
        }

        public bool CheckInfo()
        {
            if (!chkFromValue.Checked && !chkToValue.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập khoảng thời gian.", IconType.Information);
                chkFromValue.Focus();
                return false;
            }

            if (chkFromValue.Checked && chkToValue.Checked && numFromValue.Value >= numToValue.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập thời gian từ phải nhỏ hơn thời gian đến.", IconType.Information);
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

            if (!chkFromValue.Checked)
            {
                cboFromOperator.Enabled = false;
                cboToOperator.Enabled = chkToValue.Checked;
            }
            else
            {
                if (chkFromValue.Checked && chkToValue.Checked)
                {
                    cboFromOperator.Enabled = false;
                    cboToOperator.Enabled = false;
                    cboFromOperator.SelectedIndex = 1;
                    cboToOperator.SelectedIndex = 1;
                }
                else
                    cboFromOperator.Enabled = true;
            }
        }

        private void chkToValue_CheckedChanged(object sender, EventArgs e)
        {
            numToValue.Enabled = chkToValue.Checked;

            if (!chkToValue.Checked)
            {
                cboToOperator.Enabled = false;
                cboFromOperator.Enabled = chkFromValue.Checked;
            }
            else
            {
                if (chkFromValue.Checked && chkToValue.Checked)
                {
                    cboFromOperator.Enabled = false;
                    cboToOperator.Enabled = false;
                    cboFromOperator.SelectedIndex = 1;
                    cboToOperator.SelectedIndex = 1;
                }
                else
                    cboToOperator.Enabled = true;
            }
        }
        #endregion
    }
}
