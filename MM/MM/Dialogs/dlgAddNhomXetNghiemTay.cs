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
    public partial class dlgAddNhomXetNghiemTay : dlgBase
    {
        #region Constructor
        public dlgAddNhomXetNghiemTay()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<string> NhomXetNghiemList
        {
            get
            {
                List<string> nhomXNList = new List<string>();
                foreach (ListViewItem item in lvNhomXN.Items)
                {
                    if (item.Checked)
                        nhomXNList.Add(item.Text);
                }

                return nhomXNList;
            }
        }

        public DateTime NgayXetNghiem
        {
            get { return dtpkNgayXetNghiem.Value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = XetNghiemTayBus.GetNhomXetNghiemList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    lvNhomXN.Items.Add(row[0].ToString());
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"));
            }

            dtpkNgayXetNghiem.Value = DateTime.Now;
        }

        private bool CheckInfo()
        {
            bool result = false;
            foreach (ListViewItem item in lvNhomXN.Items)
            {
                if (item.Checked)
                {
                    result = true;
                    break;
                }
            }

            if (!result)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 nhóm xét nghiệm.", IconType.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddChiTietKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dlgAddChiTietKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
