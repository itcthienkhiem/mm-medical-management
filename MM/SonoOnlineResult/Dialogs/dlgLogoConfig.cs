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
        private string _logoFolder = string.Format("{0}\\Logo", Application.StartupPath);
        public bool IsChanged = false;
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
            

            return true;
        }

        private void DisplayInfo()
        {
            if (!Directory.Exists(_logoFolder)) return;
            string[] fileNames = Directory.GetFiles(_logoFolder);
            if (fileNames == null || fileNames.Length <= 0) return;
            foreach (var fn in fileNames)
            {
                Add2DataGridView(fn);
            }
        }

        private void Add2DataGridView(string fn)
        {
            Image logo = Utility.LoadImageFromFile(fn);
            logo = Utility.FixedSizeAndCrop(logo, 250, 100);

            MemoryStream ms = new MemoryStream();
            logo.Save(ms, ImageFormat.Jpeg);

            int rowIndex = dgLogo.Rows.Add();
            DataGridViewRow row = dgLogo.Rows[rowIndex];
            row.Cells[0].Value = ms.GetBuffer();
            row.Cells[1].Value = Path.GetFileName(fn);
            row.Height = 100;
            row.Tag = fn;
            ms.Close();
        }

        private void OnAdd()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string destFileName = Path.Combine(_logoFolder, Path.GetFileName(dlg.FileName));

                    if (File.Exists(destFileName))
                    {
                        MessageBox.Show(string.Format("The logo name: '{0}' does exist.", Path.GetFileName(dlg.FileName)),
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    File.Copy(dlg.FileName, destFileName);
                    Add2DataGridView(destFileName);
                    IsChanged = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnEdit()
        {
            if (dgLogo.RowCount <= 0) return;

            if (dgLogo.SelectedRows == null || dgLogo.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select one logo ?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string destFileName = Path.Combine(_logoFolder, Path.GetFileName(dlg.FileName));

                    if (File.Exists(destFileName))
                    {
                        MessageBox.Show(string.Format("The logo name: '{0}' does exist.", Path.GetFileName(dlg.FileName)),
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataGridViewRow row = dgLogo.SelectedRows[0];

                    File.Delete(row.Tag.ToString());
                    File.Copy(dlg.FileName, destFileName);

                    Image logo = Utility.LoadImageFromFile(destFileName);
                    logo = Utility.FixedSizeAndCrop(logo, 250, 100);

                    MemoryStream ms = new MemoryStream();
                    logo.Save(ms, ImageFormat.Jpeg);
                    row.Cells[0].Value = ms.GetBuffer();
                    row.Cells[1].Value = Path.GetFileName(destFileName);
                    row.Tag = destFileName;
                    IsChanged = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgLogoConfig_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgLogoConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (!CheckInfo()) e.Cancel = true;
                
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgLogo.RowCount <= 0) return;

            if (dgLogo.SelectedRows == null || dgLogo.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select at least one logo ?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to remove the selected logos ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgLogo.SelectedRows)
                {
                    string fn = row.Tag.ToString();
                    File.Delete(fn);
                    dgLogo.Rows.Remove(row);
                }

                IsChanged = true;
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (dgLogo.RowCount <= 0) return;

            if (MessageBox.Show("Do you want to remove all logos ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dgLogo.Rows)
                    {
                        string fn = row.Tag.ToString();
                        File.Delete(fn);
                    }

                    dgLogo.Rows.Clear();

                    IsChanged = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgLogo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnEdit();
        }
        #endregion
    }
}
