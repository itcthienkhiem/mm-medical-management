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
using System.Threading;
using SonoOnlineResult.Dialogs;

namespace SonoOnlineResult
{
    public partial class UploadFile : MM.Dialogs.dlgBase
    {
        #region Members
        private List<string> _fileNames = new List<string>();
        private List<string> _emailList = new List<string>();
        private bool _isUploadSuccess = true;
        #endregion

        #region Constructor
        public UploadFile()
        {
            InitializeComponent();
            InitConfig();
        }
        #endregion

        #region UI Command
        private void InitConfig()
        {
            if (File.Exists(Global.AppConfig))
            {
                Configuration.LoadData(Global.AppConfig);

                Object obj = Configuration.GetValues(Const.FTPServerNameKey);
                if (obj != null) Global.FTPConnectionInfo.ServerName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.FTPUserNameKey);
                if (obj != null) Global.FTPConnectionInfo.Username = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.FTPPasswordKey);
                if (obj != null)
                {
                    string password = Convert.ToString(obj);
                    RijndaelCrypto crypto = new RijndaelCrypto();
                    Global.FTPConnectionInfo.Password = crypto.Decrypt(password);
                }
            }
            else
            {
                Global.FTPConnectionInfo.ServerName = "ftp.ris.com.au";
                Global.FTPConnectionInfo.Username = "onlineresult@ris.com.au";
                Global.FTPConnectionInfo.Password = "Password001qazxdr5";
            }

            if (File.Exists(Global.MailConfigPath))
                Global.MailConfig.Deserialize(Global.MailConfigPath);

            if (File.Exists(Global.MySQLConnectionInfoPath))
                Global.MySQLConnectionInfo.Deserialize(Global.MySQLConnectionInfoPath);

            if (File.Exists(Global.MailTemplatePath))
                Global.MailTemplateList.Deserialize(Global.MailTemplatePath);

            Global.FTPFolder = "Results";
        }

        private void Execute(string cmd)
        {
            switch (cmd)
            {
                case "MySQL Configuration":
                    OnMySQLConfig();
                    break;

                case "FTP Configuration":
                    OnFTPConfig();
                    break;

                case "Mail Configuration":
                    OnMailConfig();
                    break;

                case "Mail Templates":
                    OnMailTemplates();
                    break;
                    
            }
        }

        private void OnMailTemplates()
        {

        }

        private void OnMySQLConfig()
        {
            dlgMySQLConfig dlg = new dlgMySQLConfig();
            dlg.ShowDialog(this);
        }

        private void OnMailConfig()
        {
            dlgMailConfig dlg = new dlgMailConfig();
            dlg.ShowDialog(this);
        }

        private void OnFTPConfig()
        {
            dlgFTPConfig dlg = new dlgFTPConfig();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.IsChangeConnectionInfo)
                {
                    dlg.SetAppConfig();
                    SaveAppConfig();
                }
            }
        }

        private void SaveAppConfig()
        {
            RijndaelCrypto crypto = new RijndaelCrypto();
            Configuration.SetValues(Const.FTPServerNameKey, Global.FTPConnectionInfo.ServerName);
            Configuration.SetValues(Const.FTPUserNameKey, Global.FTPConnectionInfo.Username);
            string password = crypto.Encrypt(Global.FTPConnectionInfo.Password);
            Configuration.SetValues(Const.FTPPasswordKey, password);
            Configuration.SaveData(Global.AppConfig);
        }

        private bool CheckFileExist(string fileName)
        {
            foreach (ListViewItem item in lvFile.Items)
            {
                if (item.Text.Trim().ToUpper() == fileName.Trim().ToUpper())
                    return true;
            }

            return false;
        }

        private void OnAdd()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files: (*.*)|*.*";

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckFileExist(dlg.FileName))
                {
                    ListViewItem item = new ListViewItem(dlg.FileName);
                    lvFile.Items.Add(item);
                }
            }
        }

        private void OnRemove()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select at least one file.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Do you want to remove selected files ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in lvFile.SelectedItems)
                {
                    lvFile.Items.Remove(item);
                }
            }
        }

        private void OnRemoveAll()
        {
            if (lvFile.Items.Count <= 0) return;

            if (MessageBox.Show("Do you want to remove all files ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                lvFile.Items.Clear();
            }
        }

        private void OnUploadAsThread()
        {
            try
            {
                if (lvFile.Items.Count <= 0) return;

                if (MessageBox.Show("Do you want to upload files ?", 
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                _fileNames.Clear();
                _isUploadSuccess = false;

                foreach (ListViewItem item in lvFile.Items)
                {
                    _fileNames.Add(item.Text);
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnUploadProc));
                base.ShowWaiting();

                if (_isUploadSuccess)
                {
                    dlgSendMail dlg = new dlgSendMail();
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        _emailList = dlg.Emails;
                        OnSendMailAsThread();            
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSendMailAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSendMailProc));
                base.ShowWaiting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSendMail()
        {
            foreach (var email in _emailList)
            {
                string pass = Utility.GeneratePassword(5);
                Result result = MySQL.AddUser(email, pass, _fileNames);
                if (!result.IsOK)
                {
                    MessageBox.Show(result.GetErrorAsString("OnSendMail"), 
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
            }
        }

        private void OnUpload()
        {
            foreach (var fileName in _fileNames)
            {
                string remoteFileName = string.Format("{0}/{1}", Global.FTPFolder, Path.GetFileName(fileName));
                Result result = FTP.UploadFile(Global.FTPConnectionInfo, fileName, remoteFileName);
                if (!result.IsOK)
                {
                    MessageBox.Show(result.GetErrorAsString("FTP.UploadFile"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _isUploadSuccess = true;
        }
        #endregion

        #region Window Event Handlers
        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Execute(e.ClickedItem.ToolTipText);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemove();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            OnRemoveAll();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OnUploadAsThread();
        }

        private void lvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effect = DragDropEffects.All;
        }

        private void lvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (!CheckFileExist(file))
                {
                    ListViewItem item = new ListViewItem(file);
                    lvFile.Items.Add(item);
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnUploadProc(object state)
        {
            try
            {
                base.SetTitleWaiting("Uploading files...");
                Thread.Sleep(500);
                OnUpload();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSendMailProc(object state)
        {
            try
            {
                base.SetTitleWaiting("Sending mail...");
                Thread.Sleep(500);
                OnSendMail();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
