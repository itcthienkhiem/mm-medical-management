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
    public partial class uNormal_Nam_Nu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Nam_Nu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool NamChecked
        {
            get { return chkNam.Checked; }
            set { chkNam.Checked = value; }
        }

        public bool NuChecked
        {
            get { return chkNu.Checked; }
            set { chkNu.Checked = value; }
        }

        public int FromAge_Nam
        {
            get { return (int)numFromAge_Nam.Value; }
            set { numFromAge_Nam.Value = value; }
        }

        public int FromAge_Nu
        {
            get { return (int)numFromAge_Nu.Value; }
            set { numFromAge_Nu.Value = value; }
        }

        public int ToAge_Nam
        {
            get { return (int)numToAge_Nam.Value; }
            set { numToAge_Nam.Value = value; }
        }

        public int ToAge_Nu
        {
            get { return (int)numToAge_Nu.Value; }
            set { numToAge_Nu.Value = value; }
        }

        public uNormal_Chung Normal_Nam
        {
            get { return uNormal_Nam; }
        }

        public uNormal_Chung Normal_Nu
        {
            get { return uNormal_Nu; }
        }
        #endregion

        #region UI Command
        public bool CheckInfo()
        {
            if (!chkNam.Checked && !chkNu.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho nam hoặc nữ.", Common.IconType.Information);
                chkNam.Focus();
                return false;
            }

            if (chkFromAge_Nam.Checked && chkToAge_Nam.Checked && numFromAge_Nam.Value > numToAge_Nam.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tuổi từ nhỏ hơn hoặc bằng tuổi đến.", Common.IconType.Information);
                numFromAge_Nam.Focus();
                return false;
            }

            if (chkNam.Checked && !Normal_Nam.CheckInfo())
            {
                uNormal_Nam.Focus();
                return false;
            }

            if (chkFromAge_Nu.Checked && chkToAge_Nu.Checked && numFromAge_Nu.Value > numToAge_Nu.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tuổi từ nhỏ hơn hoặc bằng tuổi đến.", Common.IconType.Information);
                numFromAge_Nu.Focus();
                return false;
            }

            if (chkNu.Checked && !Normal_Nu.CheckInfo())
            {
                uNormal_Nu.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkNam_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nam.Enabled = chkNam.Checked;

            chkFromAge_Nam.Enabled = chkNam.Checked;
            chkToAge_Nam.Enabled = chkNam.Checked;

            numFromAge_Nam.Enabled = chkNam.Checked && chkFromAge_Nam.Checked;
            numToAge_Nam.Enabled = chkNam.Checked && chkToAge_Nam.Checked;
        }

        private void chkNu_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nu.Enabled = chkNu.Checked;

            chkFromAge_Nu.Enabled = chkNu.Checked;
            chkToAge_Nu.Enabled = chkNu.Checked;

            numFromAge_Nu.Enabled = chkNu.Checked && chkFromAge_Nu.Checked;
            numToAge_Nu.Enabled = chkNu.Checked && chkToAge_Nu.Checked;
        }
        #endregion
    }
}
