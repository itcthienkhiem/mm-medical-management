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
        }
        #endregion

        #region UI Command
        private void InitBookmarks()
        {
            List<string> bookMarks = new List<string>();
            bookMarks.Add("#Email#");
            bookMarks.Add("#Link#");
            bookMarks.Add("#Account#");

            foreach (string bookMark in bookMarks)
            {
                ListViewItem item = new ListViewItem(bookMark);
                lvBookmarks.Items.Add(item);
            }

            txtBody.DragEnter += new DragEventHandler(txtBody_DragEnter);
            //txtBody.DragDrop += new DragEventHandler(txtBody_DragDrop);
        }

        private bool CheckInfo()
        {
            if (txtTemplateName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter template name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplateName.Focus();
                return false;
            }



            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddMailTemplate_Load(object sender, EventArgs e)
        {
            InitBookmarks();
        }

        private void dlgAddMailTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }

        //private void txtBody_DragDrop(object sender, DragEventArgs e)
        //{

        //}

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
