using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Dialogs
{
    public partial class DlgWaiting : Form
    {
        #region Constructor
        public DlgWaiting()
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

        public bool IsShowTitle
        {
            get { return label1.Visible; }
            set 
            {
                if (value) this.Size = new Size(209, 48);
                else this.Size = new Size(48, 48);
                
                label1.Visible = value; 
            }
        }
        #endregion
    }
}
