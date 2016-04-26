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
    public partial class uPalette : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPalette()
        {
            InitializeComponent();
            uColor1.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor2.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor3.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor4.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor5.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor6.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor7.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor8.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor9.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor10.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor11.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor12.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor13.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor14.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor15.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
            uColor16.OnColorClicked += new ColorClickedHandler(uColor_OnColorClicked);
        }
        #endregion

        #region Properties
        public Color Color01
        {
            get { return this.uColor1.Color; }
            set { this.uColor1.Color = value; }
        }

        public Color Color02
        {
            get { return this.uColor2.Color; }
            set { this.uColor2.Color = value; }
        }

        public Color Color03
        {
            get { return this.uColor3.Color; }
            set { this.uColor3.Color = value; }
        }

        public Color Color04
        {
            get { return this.uColor4.Color; }
            set { this.uColor4.Color = value; }
        }

        public Color Color05
        {
            get { return this.uColor5.Color; }
            set { this.uColor5.Color = value; }
        }

        public Color Color06
        {
            get { return this.uColor6.Color; }
            set { this.uColor6.Color = value; }
        }

        public Color Color07
        {
            get { return this.uColor7.Color; }
            set { this.uColor7.Color = value; }
        }

        public Color Color08
        {
            get { return this.uColor8.Color; }
            set { this.uColor8.Color = value; }
        }

        public Color Color09
        {
            get { return this.uColor9.Color; }
            set { this.uColor9.Color = value; }
        }

        public Color Color10
        {
            get { return this.uColor10.Color; }
            set { this.uColor10.Color = value; }
        }

        public Color Color11
        {
            get { return this.uColor11.Color; }
            set { this.uColor11.Color = value; }
        }

        public Color Color12
        {
            get { return this.uColor12.Color; }
            set { this.uColor12.Color = value; }
        }

        public Color Color13
        {
            get { return this.uColor13.Color; }
            set { this.uColor13.Color = value; }
        }

        public Color Color14
        {
            get { return this.uColor14.Color; }
            set { this.uColor14.Color = value; }
        }

        public Color Color15
        {
            get { return this.uColor15.Color; }
            set { this.uColor15.Color = value; }
        }

        public Color Color16
        {
            get { return this.uColor16.Color; }
            set { this.uColor16.Color = value; }
        }
        #endregion

        #region Window Event Handlers
        private void uColor_OnColorClicked(Color color)
        {
            base.RaiseColorClicked(color);
        }
        #endregion
    }
}
