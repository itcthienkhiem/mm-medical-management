/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgMailTemplates : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgMailTemplates()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void OnAdd()
        {
            dlgAddMailTemplate dlg = new dlgAddMailTemplate();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                AddTemplateToDataGrid(dlg.Template);
            }
        }

        private void OnEdit()
        {
            if (dgTemplates.SelectedRows == null || dgTemplates.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select one template.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgTemplates.SelectedRows[0];
            MailTemplate template = row.Tag as MailTemplate;
            dlgAddMailTemplate dlg = new dlgAddMailTemplate(template);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                row.Tag = dlg.Template;
                row.Cells[2].Value = dlg.Template.TemplateName;
            }
        }

        private List<DataGridViewRow> GetCheckedRows()
        {
            List<DataGridViewRow> checkedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgTemplates.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                    checkedRows.Add(row);
            }

            return checkedRows;
        }

        private void OnDelete()
        {
            List<DataGridViewRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count <= 0)
            {
                MessageBox.Show("Please check at least one template.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to delete selected templates ?", 
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (DataGridViewRow row in checkedRows)
                {
                    MailTemplate template = row.Tag as MailTemplate;
                    Global.MailTemplateList.TemplateList.Remove(template);
                    dgTemplates.Rows.Remove(row);
                }

                Global.MailTemplateList.Serialize(Global.MailTemplatePath);
            }
        }

        private void AddTemplateToDataGrid(MailTemplate template)
        {
            int rowIndex = dgTemplates.Rows.Add();
            DataGridViewRow newRow = dgTemplates.Rows[rowIndex];
            newRow.Cells[0].Value = false;
            newRow.Cells[1].Value = newRow.Index + 1;
            newRow.Cells[2].Value = template.TemplateName;
            newRow.Tag = template;
        }

        private void DisplayInfo()
        {
            foreach (var template in Global.MailTemplateList.TemplateList)
            {
                AddTemplateToDataGrid(template);
            }
        }

        private void RefreshNo()
        {
            int no = 1;
            foreach (DataGridViewRow row in dgTemplates.Rows)
            {
                row.Cells[1].Value = no++;
            }
        }
        #endregion

        #region Window Event Handlers
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

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgTemplates.Rows)
            {
                row.Cells[0].Value = chkCheck.Checked;
            }
        }

        private void dlgMailTemplates_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dgTemplates_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }

        private void dgTemplates_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnEdit();
        }
        #endregion
    }
}
