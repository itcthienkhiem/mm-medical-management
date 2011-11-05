using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Dialogs;

namespace MM
{
    public class MsgBox
    {
        private static dlgMessage _dlgMsg = new dlgMessage();

        public static void Show(string title, string message)
        {
            _dlgMsg.Title = title;
            _dlgMsg.Message = message;
            _dlgMsg.MsgBoxType = Common.MsgBoxType.OK;
            _dlgMsg.ShowDialog();
        }

        public static DialogResult Question(string title, string message)
        {
            _dlgMsg.Title = title;
            _dlgMsg.Message = message;
            _dlgMsg.MsgBoxType = Common.MsgBoxType.YesNo;

            if (_dlgMsg.ShowDialog() == DialogResult.OK)
                return DialogResult.Yes;
            else
                return DialogResult.No;
        }
    }
}
