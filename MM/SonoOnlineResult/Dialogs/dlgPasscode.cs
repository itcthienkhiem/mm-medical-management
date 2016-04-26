/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
