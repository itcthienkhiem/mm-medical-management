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
    public partial class uNormal_SoiCanLangNuocTieu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_SoiCanLangNuocTieu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool FromToChecked
        {
            get { return chkFromTo.Checked; }
            set { chkFromTo.Checked = value; }
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

        public float XValue
        {
            get { return (float)numXValue.Value; }
            set { numXValue.Value = (Decimal)value; }
        }
        #endregion

        #region UI Command
        public bool CheckInfo()
        {
            if (chkFromTo.Checked && numFromValue.Value > numToValue.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số từ nhỏ hơn hoặc bằng chỉ số đến.", Common.IconType.Information);
                numFromValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFromTo_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromTo.Checked;
            numToValue.Enabled = chkFromTo.Checked;
        }
        #endregion
    }
}
