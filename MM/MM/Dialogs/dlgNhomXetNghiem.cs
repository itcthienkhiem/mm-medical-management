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
    public partial class dlgNhomXetNghiem : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgNhomXetNghiem()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedRows
        {
            get
            {
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgNhomXetNghiem.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return checkedRows;

                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToBoolean(row["Checked"]))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Commnad
        private void DisplayInfo()
        {
            Result result = KetQuaXetNghiemTongHopBus.GetNhomXetNghiemList();
            if (result.IsOK)
                dgNhomXetNghiem.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.GetNhomXetNghiemList"), IconType.Information);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.GetNhomXetNghiemList"));
            }
        }

        private bool CheckInfo()
        {
            List<DataRow> checkedRows = this.CheckedRows;
            if (checkedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 nhóm xét nghiệm.", IconType.Information);
                dgNhomXetNghiem.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhomXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dlgNhomXetNghiem_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgNhomXetNghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
