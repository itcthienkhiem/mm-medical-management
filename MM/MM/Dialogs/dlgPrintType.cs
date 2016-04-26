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

namespace MM.Dialogs
{
    public partial class dlgPrintType : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgPrintType()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool Lien1
        {
            get { return chkLien1.Checked; }
        }

        public bool Lien2
        {
            get { return chkLien2.Checked; }
        }

        public bool Lien3
        {
            get { return chkLien3.Checked; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgPrintType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!chkLien1.Checked && !chkLien2.Checked && !chkLien3.Checked)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 liên để in.", Common.IconType.Information);
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}
