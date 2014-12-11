using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgPasscode : Form
    {
        #region Constuctor
        public dlgPasscode()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string Passcode
        {
            get { return txtPasscode.Text; }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtPasscode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter passcode.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPasscode.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgPasscode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true; 
            }
        }
        #endregion
    }
}
