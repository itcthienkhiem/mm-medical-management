using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;

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

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (dgMergePatient.SelectedRows == null || dgMergePatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân cần giữ lại.", IconType.Information);
                return;
            }

            string keepPatientGUID = (dgMergePatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            foreach(DataGridViewRow row in dgMergePatient.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                if (dr["PatientGUID"].ToString() != keepPatientGUID)
                {
                    string mergePatientGUID = dr["PatientGUID"].ToString();
                    PatientBus.Merge2Patients(keepPatientGUID, mergePatientGUID, Global.UserGUID);
                }
            }

            MsgBox.Show("Merge benh nhan", "Quá trình Merge kết thúc", IconType.Information);
        }
    }
}
