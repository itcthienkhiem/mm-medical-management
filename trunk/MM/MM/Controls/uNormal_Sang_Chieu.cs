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
    public partial class uNormal_Sang_Chieu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Sang_Chieu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool SangChecked
        {
            get { return chkSang.Checked; }
            set { chkSang.Checked = value; }
        }

        public bool ChieuChecked
        {
            get { return chkChieu.Checked; }
            set { chkChieu.Checked = value; }
        }

        public int FromTime_Sang
        {
            get { return (int)numFromTime_Sang.Value; }
            set { numFromTime_Sang.Value = value; }
        }

        public int FromTime_Chieu
        {
            get { return (int)numFromTime_Chieu.Value; }
            set { numFromTime_Chieu.Value = value; }
        }

        public int ToTime_Sang
        {
            get { return (int)numToTime_Sang.Value; }
            set { numToTime_Sang.Value = value; }
        }

        public int ToTime_Chieu
        {
            get { return (int)numToTime_Chieu.Value; }
            set { numToTime_Chieu.Value = value; }
        }

        public uNormal_Chung Normal_Sang
        {
            get { return uNormal_Sang; }
        }

        public uNormal_Chung Normal_Chieu
        {
            get { return uNormal_Chieu; }
        }
        #endregion

        #region UI Command
        public bool CheckInfo()
        {
            if (!chkSang.Checked && !chkChieu.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho sáng hoặc chiều.", Common.IconType.Information);
                chkSang.Focus();
                return false;
            }

            if (chkSang.Checked && numFromTime_Sang.Value > numToTime_Sang.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập thời gian từ nhở hơn hoặc bằng thời gian đến.", Common.IconType.Information);
                numFromTime_Sang.Focus();
                return false;
            }

            if (chkSang.Checked && !Normal_Sang.CheckInfo())
            {
                uNormal_Sang.Focus();
                return false;
            }

            if (chkChieu.Checked && numFromTime_Chieu.Value > numToTime_Chieu.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập thời gian từ nhở hơn hoặc bằng thời gian đến.", Common.IconType.Information);
                numFromTime_Chieu.Focus();
                return false;
            }

            if (chkChieu.Checked && !Normal_Chieu.CheckInfo())
            {
                uNormal_Chieu.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkSang_CheckedChanged(object sender, EventArgs e)
        {
            numFromTime_Sang.Enabled = chkSang.Checked;
            numToTime_Sang.Enabled = chkSang.Checked;
            uNormal_Sang.Enabled = chkSang.Checked;
        }

        private void chkChieu_CheckedChanged(object sender, EventArgs e)
        {
            numFromTime_Chieu.Enabled = chkChieu.Checked;
            numToTime_Chieu.Enabled = chkChieu.Checked;
            uNormal_Chieu.Enabled = chkChieu.Checked;
        }
        #endregion
    }
}
