using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgSelectXetNghiemTay : dlgBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgSelectXetNghiemTay(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }
        #endregion

        #region Properties
        public string XetNghiem_ManualGUID
        {
            get
            {
                DataRow drThuoc = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (drThuoc == null) return string.Empty;
                return drThuoc["XetNghiem_ManualGUID"].ToString();
            }
        }

        public DataRow XetNghiemRow
        {
            get { return (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row; }
        } 
        #endregion

        #region UI Command
        private void OnSearch()
        {
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtXetNghiem.Text.Trim() == string.Empty)
            {
                dgXetNghiem.DataSource = null;
                return;
            }

            if (txtXetNghiem.Text.Trim() == "*")
            {
                DataTable dtSource = _dataSource as DataTable;
                results = (from p in dtSource.AsEnumerable()
                           orderby p.Field<string>("Fullname")
                           select p).ToList<DataRow>();

                newDataSource = dtSource.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgXetNghiem.DataSource = newDataSource;
                return;
            }

            string str = txtXetNghiem.Text.ToLower();
            DataTable dt = _dataSource as DataTable;
            newDataSource = dt.Clone();

            //Ten Thuoc
            results = (from p in dt.AsEnumerable()
                       where p.Field<string>("Fullname") != null &&
                       p.Field<string>("Fullname").Trim() != string.Empty &&
                       p.Field<string>("Fullname").ToLower().IndexOf(str) == 0
                       orderby p.Field<string>("Fullname")
                       select p).ToList<DataRow>();


            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgXetNghiem.DataSource = newDataSource;
                return;
            }

            dgXetNghiem.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void dlgSelectXetNghiemTay_Load(object sender, EventArgs e)
        {

        }

        private void txtXetNghiem_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void dlgSelectXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 xét nghiệm.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 xét nghiệm.", IconType.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtXetNghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgXetNghiem.Focus();

                if (dgXetNghiem.SelectedRows != null && dgXetNghiem.SelectedRows.Count > 0)
                {
                    int index = dgXetNghiem.SelectedRows[0].Index;
                    if (index < dgXetNghiem.RowCount - 1)
                    {
                        index++;
                        dgXetNghiem.CurrentCell = dgXetNghiem[1, index];
                        dgXetNghiem.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgXetNghiem.Focus();

                if (dgXetNghiem.SelectedRows != null && dgXetNghiem.SelectedRows.Count > 0)
                {
                    int index = dgXetNghiem.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgXetNghiem.CurrentCell = dgXetNghiem[1, index];
                        dgXetNghiem.Rows[index].Selected = true;
                    }
                }
            }
        }

        #endregion
    }
}
