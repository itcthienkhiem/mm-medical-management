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
    public partial class dlgAddMailTemplate : Form
    {
        #region Members
        private MailTemplate _template = null;
        private bool _isNew = true;
        #endregion

        #region Constructor
        public dlgAddMailTemplate()
        {
            InitializeComponent();
        }

        public dlgAddMailTemplate(MailTemplate template)
        {
            InitializeComponent();
            _template = template;
            this.Text = "Edit Mail Template";
            _isNew = false;
        }
        #endregion

        #region Properties
        public MailTemplate Template
        {
            get { return _template; }
        }
        #endregion

        #region UI Command
        private void InitBookmarks()
        {
            List<string> bookMarks = new List<string>();
            bookMarks.Add("#Email#");
            bookMarks.Add("#Link#");
            //bookMarks.Add("#Account#");
            bookMarks.Add("#Signature#");

            foreach (string bookMark in bookMarks)
            {
                ListViewItem item = new ListViewItem(bookMark);
                lvBookmarks.Items.Add(item);
            }

            txtBody.DragEnter += new DragEventHandler(txtBody_DragEnter);
        }

        private bool CheckInfo()
        {
            if (txtTemplateName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter template name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplateName.Focus();
                return false;
            }

            if (txtSubject.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter subject.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubject.Focus();
                return false;
            }

            if (txtBody.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter body.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBody.Focus();
                return false;
            }

            int templateKey = _isNew ? 0 : _template.TemplateKey;
            if (Global.MailTemplateList.CheckTemplateNameExist(txtTemplateName.Text, templateKey))
            {
                MessageBox.Show(string.Format("The template name: '{0}' is exist.", txtTemplateName.Text), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplateName.Focus();
                return false;
            }

            return true;
        }

        private void SaveTemplate()
        {
            if (_isNew)
            {
                _template = new MailTemplate();
                _template.TemplateKey = Global.MailTemplateList.GetNextTemplateKey();
                _template.TemplateName = txtTemplateName.Text;
                _template.Subject = txtSubject.Text;
                _template.Body = txtBody.Text;
                Global.MailTemplateList.TemplateList.Add(_template);
            }
            else
            {
                _template.TemplateName = txtTemplateName.Text;
                _template.Subject = txtSubject.Text;
                _template.Body = txtBody.Text;
            }

            Global.MailTemplateList.Serialize(Global.MailTemplatePath);
        }

        private void DisplayInfo()
        {
            txtTemplateName.Text = _template.TemplateName;
            txtSubject.Text = _template.Subject;
            txtBody.Text = _template.Body;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddMailTemplate_Load(object sender, EventArgs e)
        {
            InitBookmarks();
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddMailTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else
                    SaveTemplate();
            }
        }

        private void txtBody_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvBookmarks_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null) return;

            ListViewItem item = e.Item as ListViewItem;
            string bookMark = item.Text;

            lvBookmarks.DoDragDrop(bookMark, DragDropEffects.Move);
        }

        private void lvBookmarks_DoubleClick(object sender, EventArgs e)
        {
            if (lvBookmarks.SelectedItems == null || lvBookmarks.SelectedItems.Count <= 0) return;
            ListViewItem item = lvBookmarks.SelectedItems[0];
            string bookMark = item.Text;
            int index = txtBody.SelectionStart;
            string noiDung = txtBody.Text;
            noiDung = noiDung.Insert(index, bookMark);
            txtBody.Text = noiDung;
            txtBody.SelectionStart = index + bookMark.Length;
            txtBody.Focus();
        }
        #endregion
    }
}
