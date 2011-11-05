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
            set { this.Text = value; }
        }

        public string Message
        {
            get { return lbMessage.Text; }
            set { lbMessage.Text = value; }
        }

        public MsgBoxType MsgBoxType
        {
            set
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
            }
        }
        #endregion

        #region UI Command
        
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
