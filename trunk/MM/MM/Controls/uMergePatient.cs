using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uMergePatient : UserControl
    {
        private DataTable _dataSource = null;
        public uMergePatient()
        {
            InitializeComponent();
        }

        public DataTable DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }
        public void BindData()
        {
            dgMergePatient.DataSource = DataSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }
    }
}
