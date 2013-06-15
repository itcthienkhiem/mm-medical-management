using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgSelectSingleDichVu : Form
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgSelectSingleDichVu(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }
        #endregion

        #region Members
        public string ServiceGUID
        {
            get
            {
                DataRow drService = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (drService == null) return string.Empty;
                return drService["ServiceGUID"].ToString();
            }
        }

        public string ServiceName
        {
            get
            {
                DataRow drService = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (drService == null) return string.Empty;
                return drService["Name"].ToString();
            }
        }
        #endregion

        #region UI Command
        private void OnSearch()
        {
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTimDichVu.Text.Trim() == string.Empty)
            {
                dgService.DataSource = null;
                return;
            }

            if (txtTimDichVu.Text.Trim() == "*")
            {
                DataTable dtSource = _dataSource as DataTable;
                results = (from p in dtSource.AsEnumerable()
                           orderby p.Field<string>("Name")
                           select p).ToList<DataRow>();

                newDataSource = dtSource.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgService.DataSource = newDataSource;
                return;
            }

            string str = txtTimDichVu.Text.ToLower();
            DataTable dt = _dataSource as DataTable;
            newDataSource = dt.Clone();

            //Ten dịch vụ
            results = (from p in dt.AsEnumerable()
                       where p.Field<string>("Name") != null &&
                       p.Field<string>("Name").Trim() != string.Empty &&
                       p.Field<string>("Name").ToLower().IndexOf(str) == 0
                       orderby p.Field<string>("Name")
                       select p).ToList<DataRow>();


            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }

            dgService.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void dlgSelectSingleThuoc_Load(object sender, EventArgs e)
        {

        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (dgService.SelectedRows == null || dgService.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 dịch vụ.", IconType.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dlgSelectSingleThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgService.SelectedRows == null || dgService.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 dịch vụ.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void txtTimThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void txtTimDichVu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

       
    }
}
