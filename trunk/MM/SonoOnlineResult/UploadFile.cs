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
using MailBee.Mime;
using MailBee.SmtpMail;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace SonoOnlineResult
{
    public partial class UploadFile : MM.Dialogs.dlgBase
    {
        #region Members
        private List<string> _fileNames = new List<string>();
        private List<string> _toEmailList = new List<string>();
        private List<string> _ccEmailList = new List<string>();
        private bool _usingMailTemplate = false;
        private bool _isUploadSuccess = true;
        private string _subject = string.Empty;
        private string _body = string.Empty;
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

            Global.FTPFolder = "results";
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
            dlgMailTemplates dlg = new dlgMailTemplates();
            dlg.ShowDialog(this);
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
            dlg.Multiselect = true;

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var fileName in dlg.FileNames)
                {
                    if (!CheckFileExist(fileName))
                    {
                        ListViewItem item = new ListViewItem(fileName);
                        lvFile.Items.Add(item);
                    }
                }

                lvFile.SelectedItems.Clear();
                lvFile.Items[lvFile.Items.Count - 1].Selected = true;
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
                int index = lvFile.SelectedItems[0].Index;
                foreach (ListViewItem item in lvFile.SelectedItems)
                {
                    lvFile.Items.Remove(item);
                }

                if (lvFile.Items.Count > 0)
                {
                    if (index < lvFile.Items.Count)
                        lvFile.Items[index].Selected = true;
                    else
                        lvFile.Items[0].Selected = true;
                }
            }
        }

        private void OnRemoveAll()
        {
            if (lvFile.Items.Count <= 0) return;

            if (MessageBox.Show("Do you want to remove all files ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                lvFile.Items.Clear();
                picViewer.Image = null;
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
                        _toEmailList = dlg.ToEmailList;
                        _ccEmailList = dlg.CcEmailList;
                        _usingMailTemplate = dlg.UsingMailTemplate;
                        _subject = dlg.Subject;
                        _body = dlg.Body;
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
            string toEmail = _toEmailList[0];
            string pass = Utility.GeneratePassword(8);
            string code = Guid.NewGuid().ToString();
            Result result = MySQL.AddUser(toEmail, pass, code, _fileNames);
            if (!result.IsOK)
            {
                MessageBox.Show(result.GetErrorAsString("OnSendMail"), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MailMessage msg = new MailMessage();
            msg.From = new EmailAddress(Global.MailConfig.SenderMail);
            msg.To.Add(new EmailAddress(toEmail));
            if (_ccEmailList.Count > 0)
            {
                foreach (var email in _ccEmailList)
                    msg.Cc.Add(new EmailAddress(email));
            }

            msg.Subject = _subject;
            string link = string.Format("http://result.ris.com.au?code={0}", code);
            string account = string.Format("Username: {0}\nPassword: {1}", toEmail, pass);

            if (_usingMailTemplate)
            {
                _body = _body.Replace("#Email#", toEmail);
                _body = _body.Replace("#Link#", link);
                _body = _body.Replace("#Account#", account);
                _body = _body.Replace("#Signature#", Global.MailConfig.Signature);
            }
            else
            {
                _body += string.Format("\n\nPlease follow this link to view your result:\n{0}\n\nThe username and password to login are:\n{1}\n\n{2}",
                    link, account, Global.MailConfig.Signature);
            }

            msg.BodyPlainText = _body;

            Smtp.LicenseKey = "MN200-B47C7EFF7C257CFF7C2E34E777B5-D2BD";
            Smtp smtp = new Smtp();
            if (Global.MailConfig.UseSMTPServer)
            {
                SmtpServer server = new SmtpServer();
                server.Timeout = 60000;
                server.SslMode = MailBee.Security.SslStartupMode.OnConnect;
                server.Name = Global.MailConfig.Server;
                server.Port = Global.MailConfig.Port;
                server.AccountName = Global.MailConfig.Username;
                server.Password = Global.MailConfig.Password;
                server.AuthMethods = MailBee.AuthenticationMethods.SaslLogin | MailBee.AuthenticationMethods.SaslPlain;
                smtp.SmtpServers.Add(server);
            }
            else
                smtp.DnsServers.Autodetect();

            try
            {
                smtp.Message = msg;
                if (smtp.Send())
                    MessageBox.Show("Mail has been sent.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    string error = string.Format("Cannot send mail!\r\nError: {0}", smtp.GetErrorDescription());
                    MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("Cannot send mail!\r\nError: {0}", ex.Message);
                MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string ext = Path.GetExtension(fileName).ToLower();
                if (ext == ".bmp" || ext == ".png" || ext == ".jpg" ||
                    ext == ".jpeg" || ext == ".jpe" || ext == ".gif")
                {
                    Image thumbnail = Utility.LoadImageFromFile(fileName);
                    thumbnail = Utility.FixedSize(thumbnail, 320, 320);
                    string thumbnailFileName = string.Format("{0}\\{1}_thumb{2}", 
                        Application.StartupPath, Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));

                    thumbnail.Save(thumbnailFileName);
                    thumbnail.Dispose();
                    thumbnail = null;

                    remoteFileName = string.Format("{0}/{1}", Global.FTPFolder, Path.GetFileName(thumbnailFileName));
                    result = FTP.UploadFile(Global.FTPConnectionInfo, thumbnailFileName, remoteFileName);

                    File.Delete(thumbnailFileName);

                    if (!result.IsOK)
                    {
                        MessageBox.Show(result.GetErrorAsString("FTP.UploadFile"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            _isUploadSuccess = true;
        }

        private void OnRunFile()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0)
                return;

            Process.Start(lvFile.SelectedItems[0].Text);
        }

        private void OnViewImage()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0)
                return;

            string ext = Path.GetExtension(lvFile.SelectedItems[0].Text).ToLower();
            if (ext == ".bmp" || ext == ".png" || ext == ".jpg" ||
                ext == ".jpeg" || ext == ".jpe" || ext == ".gif")
            {
                Image img = Utility.LoadImageFromFile(lvFile.SelectedItems[0].Text);
                img = Utility.FixedSize(img, 320, 320);
                img.Save(string.Format("{0}\\{1}", Application.StartupPath, Path.GetFileName(lvFile.SelectedItems[0].Text)));
                picViewer.Image = img;
            }
            else
                picViewer.Image = null;
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

            lvFile.SelectedItems.Clear();
            lvFile.Items[lvFile.Items.Count - 1].Selected = true;
        }

        private void lvFile_DoubleClick(object sender, EventArgs e)
        {
            OnRunFile();
        }

        private void lvFile_Click(object sender, EventArgs e)
        {
            
        }

        private void lvFile_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OnViewImage();
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
