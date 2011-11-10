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
        #endregion

        #region UI Command
        
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
