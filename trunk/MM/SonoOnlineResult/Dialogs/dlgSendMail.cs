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
    public partial class dlgSendMail : Form
    {
        #region Members
        private List<string> _values = new List<string>();
        #endregion

        #region Constructor
        public dlgSendMail()
        {
            InitializeComponent();

            _values.Add("Apple");
            _values.Add("Banana");
            _values.Add("Orange");
            _values.Add("Lemon");


            //txtTo.Values = _values;
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgSendMail_Move(object sender, EventArgs e)
        {
            //txtTo.RecalLocation();
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            //txtTo.Hide();
        }
        #endregion
    }
}
