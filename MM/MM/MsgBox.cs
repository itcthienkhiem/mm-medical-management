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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Dialogs;
using MM.Common;

namespace MM
{
    public class MsgBox
    {
        private static dlgMessage _dlgMsg = new dlgMessage();

        public static void Show(string title, string message, IconType iconType)
        {
            _dlgMsg.Title = title;
            _dlgMsg.Message = message;
            _dlgMsg.MsgBoxType = Common.MsgBoxType.OK;
            _dlgMsg.IconType = iconType;
            _dlgMsg.ShowDialog();
        }

        public static DialogResult Question(string title, string message)
        {
            _dlgMsg.Title = title;
            _dlgMsg.Message = message;
            _dlgMsg.IconType = IconType.Question;
            _dlgMsg.MsgBoxType = Common.MsgBoxType.YesNo;

            if (_dlgMsg.ShowDialog() == DialogResult.OK)
                return DialogResult.Yes;
            else
                return DialogResult.No;
        }
    }
}
