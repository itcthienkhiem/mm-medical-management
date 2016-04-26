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
using MM.Common;

namespace MM.Controls
{
    public partial class uDrawType : uBase
    {
        #region Members
        private DrawType _type = DrawType.None;
        private int _width = 1;
        private ToolTip _toolTip = new ToolTip();
        #endregion

        #region Constructor
        public uDrawType()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public Image Image
        {
            get { return picDrawType.Image; }
            set { picDrawType.Image = value; }
        }

        public DrawType DrawType
        {
            get { return _type; }
            set { _type = value; }
        }

        public int BrushWidth
        {
            get { return _width; }
            set { _width = value; }
        }
        #endregion

        #region Window Event Handlers
        private void uDrawType_Load(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(picDrawType, _type.ToString());
        }

        private void picDrawType_MouseDown(object sender, MouseEventArgs e)
        {
            RaiseDrawTypeClicked(_type, _width);
        }

        private void picDrawType_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            _toolTip.Active = true;
        }

        private void picDrawType_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            _toolTip.Active = false;
            _toolTip.Hide(picDrawType);
        }
        #endregion
    }
}
