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
    public partial class dlgSelectSingleThuoc : Form
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgSelectSingleThuoc(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }
        #endregion

        #region Members
        public string MaThuocGUID
        {
            get
            {
                DataRow drThuoc = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (drThuoc == null) return string.Empty;
                return drThuoc["ThuocGUID"].ToString();
            }
        }
        #endregion

        #region UI Command
        private void OnSearch()
        {
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTimThuoc.Text.Trim() == string.Empty)
            {
                dgThuoc.DataSource = null;
                return;
            }

            if (txtTimThuoc.Text.Trim() == "*")
            {
                DataTable dtSource = _dataSource as DataTable;
                results = (from p in dtSource.AsEnumerable()
                           orderby p.Field<string>("TenThuoc")
                           select p).ToList<DataRow>();

                newDataSource = dtSource.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgThuoc.DataSource = newDataSource;
                return;
            }

            string str = txtTimThuoc.Text.ToLower();
            DataTable dt = _dataSource as DataTable;
            newDataSource = dt.Clone();

            //Ten Thuoc
            results = (from p in dt.AsEnumerable()
                       where p.Field<string>("TenThuoc") != null &&
                       p.Field<string>("TenThuoc").Trim() != string.Empty &&
                       (p.Field<string>("TenThuoc").ToLower().IndexOf(str) == 0 ||
                       str.IndexOf(p.Field<string>("TenThuoc").ToLower()) == 0)
                       orderby p.Field<string>("TenThuoc")
                       select p).ToList<DataRow>();


            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgThuoc.DataSource = newDataSource;
                return;
            }

            dgThuoc.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void dlgSelectSingleThuoc_Load(object sender, EventArgs e)
        {

        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 thuốc.", IconType.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dlgSelectSingleThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 thuốc.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void txtTimThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }
        #endregion
    }
}
