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

namespace MM.Dialogs
{
    public partial class dlgCauHinhShareFolder : Form
    {
        #region Constructor
        public dlgCauHinhShareFolder()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtShareFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input share folder.", ProductName);
                txtShareFolder.Focus();
                return false;
            }

            if (!Directory.Exists(txtShareFolder.Text))
            {
                MessageBox.Show("Share folder is not exists", ProductName);
                txtShareFolder.Focus();
                return false;
            }

            if (!Utility.ValidateNetworkPath(txtShareFolder.Text))
            {
                MessageBox.Show("Share folder must be the network path", ProductName);
                txtShareFolder.Focus();
                return false;
            }

            return true;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (txtShareFolder.Text != string.Empty)
                dlg.SelectedPath = txtShareFolder.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
                txtShareFolder.Text = dlg.SelectedPath;
        }

        private void dlgCauHinhShareFolder_Load(object sender, EventArgs e)
        {
            txtShareFolder.Text = Global.ShareFolder;
        }

        private void dlgCauHinhShareFolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else
                    Global.ShareFolder = txtShareFolder.Text;
            }
        }
        #endregion
    }
}
