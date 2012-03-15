using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgSelectSingleHopDong : dlgBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgSelectSingleHopDong(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }
        #endregion

        #region Properties
        public string HopDongGUID
        {
            get
            {
                DataRow drHopDong = (dgContract.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (drHopDong == null) return string.Empty;
                return drHopDong["CompanyContractGUID"].ToString();
            }
        }
        #endregion
        
        #region UI Command
        private void OnSearchHopDong()
        {
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtHopDong.Text.Trim() == string.Empty)
            {
                dgContract.DataSource = null;
                return;
            }

            if (txtHopDong.Text.Trim() == "*")
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgContract.DataSource = newDataSource;
                if (dgContract.RowCount > 0) dgContract.Rows[0].Selected = true;
                return;
            }

            string str = txtHopDong.Text.ToLower();

            newDataSource = _dataSource.Clone();

            if (chkMaHopDong.Checked)
            {
                //Ma hop dong
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("ContractCode") != null &&
                             p.Field<string>("ContractCode").Trim() != string.Empty &&
                             p.Field<string>("ContractCode").ToLower().IndexOf(str) >= 0
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgContract.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //Ten hop dong
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("ContractName").ToLower().IndexOf(str) >= 0 &&
                           p.Field<string>("ContractName") != null &&
                           p.Field<string>("ContractName").Trim() != string.Empty
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgContract.DataSource = newDataSource;
                    return;
                }
            }

            dgContract.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void dgContract_DoubleClick(object sender, EventArgs e)
        {
            if (dgContract.SelectedRows == null || dgContract.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 hợp đồng.", IconType.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dlgSelectSingleHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgContract.SelectedRows == null || dgContract.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn chọn 1 hợp đồng.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void txtHopDong_TextChanged(object sender, EventArgs e)
        {
            OnSearchHopDong();
        }

        private void chkMaHopDong_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchHopDong();
        }

        private void txtHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgContract.Focus();

                if (dgContract.SelectedRows != null && dgContract.SelectedRows.Count > 0)
                {
                    int index = dgContract.SelectedRows[0].Index;
                    if (index < dgContract.RowCount - 1)
                    {
                        index++;
                        dgContract.CurrentCell = dgContract[1, index];
                        dgContract.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgContract.Focus();

                if (dgContract.SelectedRows != null && dgContract.SelectedRows.Count > 0)
                {
                    int index = dgContract.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgContract.CurrentCell = dgContract[1, index];
                        dgContract.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        
    }
}
