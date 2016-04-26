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
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgMessage : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgMessage()
        {
            InitializeComponent();
        }

        public dlgMessage(string title, string message, MsgBoxType msgBoxType)
        {
            InitializeComponent();
            
            this.Text = title;
            this.lbMessage.Text = message;

            switch (msgBoxType)
            {
                case MsgBoxType.OK:
                    btnCancel.Visible = true;
                    btnCancel2.Visible = false;
                    btnOK.Visible = false;
                    break;
                case MsgBoxType.YesNo:
                    btnCancel.Visible = false;
                    btnCancel2.Visible = true;
                    btnOK.Visible = true;
                    break;
            }

        }
        #endregion

        #region Properties
        public string Title
        {
            get { return this.Text; }
            set             
            {
                MethodInvoker method = delegate
                {
                    this.Text = value; 
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
        }

        public string Message
        {
            get { return lbMessage.Text; }
            set 
            { 
                MethodInvoker method = delegate
                {
                    lbMessage.Text = value;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
        }

        public MsgBoxType MsgBoxType
        {
            set
            {
                MethodInvoker method = delegate
                {
                    switch (value)
                    {
                        case MsgBoxType.OK:
                            btnCancel.Visible = true;
                            btnCancel2.Visible = false;
                            btnOK.Visible = false;
                            break;
                        case MsgBoxType.YesNo:
                            btnCancel.Visible = false;
                            btnCancel2.Visible = true;
                            btnOK.Visible = true;
                            break;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
        }

        public IconType IconType
        {
            set
            {
                switch (value)
                {
                    case IconType.Information:
                        picIcon.Image = Properties.Resources.Information_icon;
                        break;
                    case IconType.Question:
                        picIcon.Image = Properties.Resources.Help_icon;
                        break;
                    case IconType.Error:
                        picIcon.Image = Properties.Resources.Actions_dialog_close_icon;
                        break;
                }
            }
        }
        #endregion

        #region UI Command
        
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
