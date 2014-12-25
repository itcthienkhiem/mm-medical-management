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
    public partial class dlgWaiting : Form
    {
        #region Constructor
        public dlgWaiting()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        #endregion

        private void dlgWaiting_Load(object sender, EventArgs e)
        {
            
        }
    }
}
