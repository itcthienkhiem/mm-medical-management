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
    public partial class dlgMergePatient : Form
    {
        public dlgMergePatient()
        {
            InitializeComponent();
        }

        public void SetDataSource(DataTable dt)
        {
            this.uMergePatient1.DataSource = dt;
            this.uMergePatient1.BindData();
        }
    }
}
