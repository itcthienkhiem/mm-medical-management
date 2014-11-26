using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using System.IO;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgEmailList : Form
    {
        #region Constructor
        public dlgEmailList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<string> SelectedEmails
        {
            get
            {
                List<string> selectedEmails = new List<string>();
                foreach (ListViewItem item in lvEmail.SelectedItems)
                    selectedEmails.Add(item.Text);

                return selectedEmails;
            }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            EmailList emailList = new EmailList();
            if (File.Exists(Global.EmailListPath))
            {
                if (emailList.Deserialize(Global.EmailListPath))
                {
                    foreach (var email in emailList.Emails)
                    {
                        ListViewItem item = new ListViewItem(email.EmailAddress);
                        lvEmail.Items.Add(item);
                    }
                }
            }
        }

        private bool CheckInfo()
        {
            if (lvEmail.SelectedItems == null || lvEmail.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select at least one email address.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgEmailList_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgEmailList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }

        private void lvEmail_DoubleClick(object sender, EventArgs e)
        {
            if (lvEmail.SelectedItems == null || lvEmail.SelectedItems.Count <= 0) return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
