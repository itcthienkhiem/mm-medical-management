using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgPortConfig : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgPortConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void DisplayPortConfigList()
        {
            foreach (PortConfig p in Global.PortConfigCollection.PortConfigList)
            {
                dgConfig.Rows.Add(p.Id, p.TenMayXetNghiem, p.PortName);
            }
        }

        private void OnAdd()
        {
            dlgAddPortConfig dlg = new dlgAddPortConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Global.PortConfigCollection.Add(dlg.PortConfig);
                dgConfig.Rows.Add(dlg.PortConfig.Id, dlg.PortConfig.TenMayXetNghiem, dlg.PortConfig.PortName);
                Global.PortConfigCollection.Serialize(Global.PortConfigPath);
            }
        }

        private void OnEdit()
        {
            if (dgConfig.SelectedRows == null || dgConfig.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 cấu hình kết nối đề sửa.", IconType.Information);
                return;
            }

            DataGridViewRow row = dgConfig.SelectedRows[0];
            string id = row.Cells[0].Value.ToString();
            PortConfig portConfig = Global.PortConfigCollection.GetPortConfigById(id);
            if (portConfig == null) return;
            dlgAddPortConfig dlg = new dlgAddPortConfig(portConfig);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                row.Cells[1].Value = dlg.PortConfig.TenMayXetNghiem;
                row.Cells[2].Value = dlg.PortConfig.PortName;
                Global.PortConfigCollection.Serialize(Global.PortConfigPath);
            }
        }

        private void OnDelete()
        {
            if (dgConfig.SelectedRows == null || dgConfig.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 cấu hình kết nối đề xóa.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa cấu hình kết nối mà bạn đã chọn ?") == DialogResult.Yes)
            {
                DataGridViewRow row = dgConfig.SelectedRows[0];
                string id = row.Cells[0].Value.ToString();
                Global.PortConfigCollection.Remove(id);
                Global.PortConfigCollection.Serialize(Global.PortConfigPath);
                dgConfig.Rows.Remove(row);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgPortConfig_Load(object sender, EventArgs e)
        {
            DisplayPortConfigList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgConfig_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion
    }
}
