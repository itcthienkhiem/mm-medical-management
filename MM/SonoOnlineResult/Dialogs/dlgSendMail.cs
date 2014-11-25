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


            txtTo.AutoCompleteCustomSource.AddRange(_values.ToArray());
        }
        #endregion
    }
}
