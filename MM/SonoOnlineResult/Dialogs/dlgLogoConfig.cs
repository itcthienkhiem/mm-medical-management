using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using System.Drawing.Imaging;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgLogoConfig : Form
    {
        #region Members
        private string _logoFileName = string.Format("{0}\\Logo\\Logo.jpg", Application.StartupPath);
        #endregion

        #region Constructor
        public dlgLogoConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (picLogo.Image == null)
            {
                MessageBox.Show("Please select logo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void SaveLogo()
        {
            try
            {
                if (picLogo.Tag == null) return;
                Image image = picLogo.Tag as Image;
                image.Save(_logoFileName, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Window Event Handlers
        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files: (*.*)|*.*";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Image image = Utility.LoadImageFromFile(dlg.FileName);
                picLogo.Image = Utility.FixedSizeAndCrop(image, picLogo.Width, picLogo.Height);
                picLogo.Tag = image;
            }
        }

        private void dlgLogoConfig_Load(object sender, EventArgs e)
        {
            if (File.Exists(_logoFileName))
            {
                Image image = Utility.LoadImageFromFile(_logoFileName);
                image = Utility.FixedSizeAndCrop(image, picLogo.Width, picLogo.Height);
                picLogo.Image = image;
            }
        }

        private void dlgLogoConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveLogo();
            }
        }
        #endregion
    }
}
