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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uColor : uBase
    {
        #region Members
        private ToolTip _toolTip = new ToolTip();
        #endregion

        #region Constructor
        public uColor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public Color Color
        {
            get { return picColor.BackColor; }
            set { picColor.BackColor = value; }
        }
        #endregion

        #region Window Event Handlers
        private void uColor_Load(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(picColor, picColor.BackColor.Name);

        }

        private void picColor_MouseDown(object sender, MouseEventArgs e)
        {
            RaiseColorClicked(this.Color);
        }

        private void picColor_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            _toolTip.Active = true;
        }

        private void picColor_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            _toolTip.Active = false;
            _toolTip.Hide(picColor);
        }
        #endregion
    }
}
