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
    public partial class uNormal_Estradiol : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Estradiol()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool FollicularPhaseChecked
        {
            get { return chkFollicularPhase.Checked; }
            set { chkFollicularPhase.Checked = value; }
        }

        public bool MidcycleChecked
        {
            get { return chkMidcycle.Checked; }
            set { chkMidcycle.Checked = value; }
        }

        public bool LutelPhaseChecked
        {
            get { return chkLutelPhase.Checked; }
            set { chkLutelPhase.Checked = value; }
        }

        public uNormal_Chung Normal_FollicularPhase
        {
            get { return uNormal_FollicularPhase; }
        }

        public uNormal_Chung Normal_Midcycle
        {
            get { return uNormal_Midcycle; }
        }

        public uNormal_Chung Normal_LutelPhase
        {
            get { return uNormal_LutelPhase; }
        }
        #endregion

        #region UI Command
        public bool CheckInfo()
        {
            if (!chkFollicularPhase.Checked && !chkMidcycle.Checked && !chkLutelPhase.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho Follicular phase hoặc Midcycle hoặc Lutel phase.", Common.IconType.Information);
                chkFollicularPhase.Focus();
                return false;
            }

            if (chkFollicularPhase.Checked && !Normal_FollicularPhase.CheckInfo())
            {
                uNormal_FollicularPhase.Focus();
                return false;
            }

            if (chkMidcycle.Checked && !Normal_Midcycle.CheckInfo())
            {
                uNormal_Midcycle.Focus();
                return false;
            }

            if (chkLutelPhase.Checked && !Normal_LutelPhase.CheckInfo())
            {
                uNormal_LutelPhase.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFollicularPhase_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_FollicularPhase.Enabled = chkFollicularPhase.Checked;
        }

        private void chkMidcycle_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Midcycle.Enabled = chkMidcycle.Checked;
        }

        private void chkLutelPhase_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_LutelPhase.Enabled = chkLutelPhase.Checked;
        }        
        #endregion
    }
}
