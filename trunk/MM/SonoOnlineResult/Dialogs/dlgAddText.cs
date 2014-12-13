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
    public partial class dlgAddText : Form
    {
        #region Constructors
        public dlgAddText()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string Text1
        {
            get { return txtText1.Text.Trim(); }
            set { txtText1.Text = value; }
        }

        public string Text2
        {
            get { return txtText2.Text.Trim(); }
            set { txtText2.Text = value; }
        }

        public string Text3
        {
            get { return txtText3.Text.Trim(); }
            set { txtText3.Text = value; }
        }
        #endregion
    }
}
