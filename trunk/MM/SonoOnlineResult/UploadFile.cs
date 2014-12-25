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
//using MailBee.Mime;
//using MailBee.SmtpMail;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace SonoOnlineResult
{
    public partial class UploadFile : SonoOnlineResult.Dialogs.dlgBase
    {
        #region Members
        private List<ResultFileInfo> _resultFileInfos = new List<ResultFileInfo>();
        private List<string> _toEmailList = new List<string>();
        private List<string> _ccEmailList = new List<string>();
        private bool _usingMailTemplate = false;
        private bool _isUploadSuccess = true;
        private string _subject = string.Empty;
        private string _body = string.Empty;
        private string _mailTemplate = string.Empty;
        //private string _templateName = string.Empty;
        //private string _logoName = string.Empty;
        private string _passcode = string.Empty;
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
                Global.FTPConnectionInfo.Password = "onlineresult1qazxdr5";
            }

            if (File.Exists(Global.MailConfigPath))
                Global.MailConfig.Deserialize(Global.MailConfigPath);

            if (File.Exists(Global.MySQLConnectionInfoPath))
                Global.MySQLConnectionInfo.Deserialize(Global.MySQLConnectionInfoPath);

            MySQLHelper._connectionString = Global.MySQLConnectionInfo.ConnectionString;

            if (File.Exists(Global.MailTemplatePath))
                Global.MailTemplateList.Deserialize(Global.MailTemplatePath);

            Global.FTPFolder = "results";

            LoadImageTemplates();
            LoadLogos();
            LoadAds();
        }

        private void LoadImageTemplates()
        {
            toolStripComboBoxTemplates.Items.Clear();
            toolStripComboBoxTemplates.Items.Add("[None]");

            string templateFolder = string.Format("{0}\\ImageTemplates", Application.StartupPath);
            if (Directory.Exists(templateFolder))
            {
                string[] fileNames = Directory.GetFiles(templateFolder);
                foreach (var fileName in fileNames)
                    toolStripComboBoxTemplates.Items.Add(Path.GetFileName(fileName));

                toolStripComboBoxTemplates.SelectedIndex = 0;
            }
        }

        private void LoadLogos()
        {

            string oldLogoName = toolStripComboBoxLogo.SelectedItem == null ? string.Empty : toolStripComboBoxLogo.SelectedItem.ToString();
            toolStripComboBoxLogo.Items.Clear();
            toolStripComboBoxLogo.Items.Add("[None]");

            string logoFolder = string.Format("{0}\\Logo", Application.StartupPath);
            if (Directory.Exists(logoFolder))
            {
                string[] fileNames = Directory.GetFiles(logoFolder);
                foreach (var fileName in fileNames)
                    toolStripComboBoxLogo.Items.Add(Path.GetFileName(fileName));

                toolStripComboBoxLogo.SelectedIndex = 0;
            }

            toolStripComboBoxLogo.SelectedItem = oldLogoName;
        }

        private void LoadAds()
        {

            string oldAdsName = toolStripComboBoxAds.SelectedItem == null ? string.Empty : toolStripComboBoxAds.SelectedItem.ToString();
            toolStripComboBoxAds.Items.Clear();
            toolStripComboBoxAds.Items.Add("[None]");

            string adsFolder = string.Format("{0}\\Ads", Application.StartupPath);
            if (Directory.Exists(adsFolder))
            {
                string[] fileNames = Directory.GetFiles(adsFolder);
                foreach (var fileName in fileNames)
                    toolStripComboBoxAds.Items.Add(Path.GetFileName(fileName));

                toolStripComboBoxAds.SelectedIndex = 0;
            }

            toolStripComboBoxAds.SelectedItem = oldAdsName;
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

                case "Logo Configuration":
                    OnLogoConfig();
                    break;

                case "Rotate counterclockwise":
                    OnRotateCounterclockwise();
                    break;

                case "Rotate clockwise":
                    OnRotateClockwise();
                    break;

                case "Add Text":
                    OnAddText();
                    break;

                case "Apply":
                    OnApplyTemplate();
                    break;

                case "Ads Configuration":
                    OnAdsConfig();
                    break;

                case "Add Ads":
                    OnAddAds();
                    break;

                case "Resend Mail":
                    OnResendMail();
                    break;

                case "Branch List":
                    OnBranchList();
                    break;

                case "User List":
                    OnUserList();
                    break;

                case "Tracking":
                    OnTracking();
                    break;

                case "Login":
                    OnLogin();
                    break;

                case "Logout":
                    OnLogout();
                    break;

                case "Change Password":
                    OnChangePassword();
                    break;
            }
        }

        private void OnLogin()
        {
            
        }

        private void OnLogout()
        {
            
        }

        private void OnChangePassword()
        {
            
        }

        private void OnBranchList()
        {
            dlgBranchList dlg = new dlgBranchList();
            dlg.ShowDialog(this);
        }

        private void OnUserList()
        {
            
        }

        private void OnTracking()
        {
            
        }

        private void OnResendMail()
        {
            dlgSendMail dlg = new dlgSendMail();
            dlg.MailTemplate = _mailTemplate;
            dlg.Subject = _subject;
            dlg.Body = _body;

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _toEmailList = dlg.ToEmailList;
                _ccEmailList = dlg.CcEmailList;
                _usingMailTemplate = dlg.UsingMailTemplate;
                _subject = dlg.Subject;
                _body = dlg.Body;
                _passcode = dlg.Passcode;

                OnSendMailAsThread();
            }
        }

        private void OnAdsConfig()
        {
            dlgAdsConfig dlg = new dlgAdsConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                if (dlg.IsChanged)
                {
                    LoadAds();
                }
            }
        }

        private void OnAddAds()
        {
            if (toolStripComboBoxAds.SelectedItem == null ||
                toolStripComboBoxAds.SelectedItem.ToString() == "[None]") return;

            string fileName = toolStripComboBoxAds.SelectedItem.ToString();
            if (!CheckFileExist(fileName))
            {
                ListViewItem item = new ListViewItem(fileName);
                ResultFileInfo info = new ResultFileInfo();
                info.FileName = fileName;
                info.Type = FileType.Ads;
                info.ProcessResultImage();
                item.Tag = info;
                lvFile.Items.Add(item);
            }

            lvFile.SelectedItems.Clear();
            lvFile.Items[lvFile.Items.Count - 1].Selected = true;
        }

        private void OnApplyTemplate()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in lvFile.SelectedItems)
            {
                ResultFileInfo info = item.Tag as ResultFileInfo;
                info.TemplateName = toolStripComboBoxTemplates.SelectedItem == null ? "[None]" : toolStripComboBoxTemplates.SelectedItem.ToString();
                info.LogoName = toolStripComboBoxLogo.SelectedItem == null ? "[None]" : toolStripComboBoxLogo.SelectedItem.ToString();
            }

            OnViewImage();
        }

        private void OnAddText()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0)
                return;
            ResultFileInfo info = lvFile.SelectedItems[0].Tag as ResultFileInfo;

            dlgAddText dlg = new dlgAddText();
            dlg.Text1 = info.Text1;
            dlg.Text2 = info.Text2;
            dlg.Text3 = info.Text3;

            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                info = lvFile.SelectedItems[0].Tag as ResultFileInfo;
                info.Text1 = dlg.Text1;
                info.Text2 = dlg.Text2;
                info.Text3 = dlg.Text3;
                OnViewImage();
            }
        }

        private void OnRotateCounterclockwise()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0) return;
            ResultFileInfo info = lvFile.SelectedItems[0].Tag as ResultFileInfo;
            info.RotateOrgImage(RotateFlipType.Rotate270FlipNone);
            OnViewImage();
            
        }

        private void OnRotateClockwise()
        {
            if (lvFile.SelectedItems == null || lvFile.SelectedItems.Count <= 0) return;
            ResultFileInfo info = lvFile.SelectedItems[0].Tag as ResultFileInfo;
            info.RotateOrgImage(RotateFlipType.Rotate90FlipNone);
            OnViewImage();
        }

        private void OnLogoConfig()
        {
            dlgLogoConfig dlg = new dlgLogoConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                if (dlg.IsChanged)
                {
                    LoadLogos();
                }
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
                        ResultFileInfo info = new ResultFileInfo();
                        info.FileName = fileName;
                        //info.TemplateName = toolStripComboBoxTemplates.SelectedItem == null ? string.Empty : toolStripComboBoxTemplates.SelectedItem.ToString();
                        //info.LogoName = toolStripComboBoxLogo.SelectedItem == null ? string.Empty : toolStripComboBoxLogo.SelectedItem.ToString();
                        info.ProcessResultImage();
                        item.Tag = info;
                        lvFile.Items.Add(item);
                    }
                }

                lvFile.SelectedItems.Clear();
                lvFile.Items[lvFile.Items.Count - 1].Selected = true;

                toolStripButtonResendMail.Enabled = CheckAllowResendMail();
            }
        }

        private bool CheckAllowResendMail()
        {
            if (_resultFileInfos.Count != lvFile.Items.Count)
                return false;

            foreach (ListViewItem item in lvFile.Items)
            {
                bool result = false;
                foreach (var info in _resultFileInfos)
                {
                    if (item.Text == info.FileName)
                    {
                        result = true;
                        break;
                    }
                }

                if (!result) return false;
            }

            return true;
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
                    if (item.Tag != null)
                    {
                        (item.Tag as ResultFileInfo).Clear();
                        item.Tag = null;
                    }

                    lvFile.Items.Remove(item);
                }

                if (lvFile.Items.Count > 0)
                {
                    if (index < lvFile.Items.Count)
                        lvFile.Items[index].Selected = true;
                    else
                        lvFile.Items[0].Selected = true;
                }

                toolStripButtonResendMail.Enabled = CheckAllowResendMail();
            }
        }

        private void OnRemoveAll()
        {
            if (lvFile.Items.Count <= 0) return;

            if (MessageBox.Show("Do you want to remove all files ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in lvFile.Items)
                {
                    if (item.Tag != null)
                    {
                        (item.Tag as ResultFileInfo).Clear();
                        item.Tag = null;
                    }
                }

                lvFile.Items.Clear();
                picViewer.Image = null;

                toolStripButtonResendMail.Enabled = false;
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

                _resultFileInfos.Clear();
                toolStripButtonResendMail.Enabled = false;
                _isUploadSuccess = false;
                //_templateName = toolStripComboBoxTemplates.SelectedItem == null ? string.Empty : toolStripComboBoxTemplates.SelectedItem.ToString();
                //_logoName = toolStripComboBoxLogo.SelectedItem == null ? string.Empty : toolStripComboBoxLogo.SelectedItem.ToString();

                foreach (ListViewItem item in lvFile.Items)
                    _resultFileInfos.Add(item.Tag as ResultFileInfo);

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
                        _passcode = dlg.Passcode;
                        _mailTemplate = dlg.MailTemplate;

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

                toolStripButtonResendMail.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSendMail()
        {
            string toEmail = _toEmailList[0];
            //string pass = Utility.GeneratePassword(8);
            string code = Guid.NewGuid().ToString();
            Result result = MySQL.AddUser(toEmail, _passcode, code, _resultFileInfos);
            if (!result.IsOK)
            {
                MessageBox.Show(result.GetErrorAsString("OnSendMail"), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(Global.MailConfig.SenderMail);
            msg.To.Add(new MailAddress(toEmail));
            if (_ccEmailList.Count > 0)
            {
                foreach (var email in _ccEmailList)
                    msg.CC.Add(new MailAddress(email));
            }

            msg.Subject = _subject;
            string link = string.Format("http://result.ris.com.au?code={0}", code);
            //string account = string.Format("Username: {0}\nPassword: {1}", toEmail, pass);

            string body = _body;
            if (_usingMailTemplate)
            {
                body = body.Replace("#Email#", toEmail);
                body = body.Replace("#Link#", link);
                //_body = _body.Replace("#Account#", account);
                body = body.Replace("#Signature#", Global.MailConfig.Signature);
            }
            else
            {
                body += string.Format("\n\nPlease follow this link to view your result:\n{0}\n\n{1}",
                    link, Global.MailConfig.Signature);
            }

            msg.Body = body;

            SmtpClient client = new SmtpClient();
            client.Port = Global.MailConfig.Port;
            client.Host = Global.MailConfig.Server;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Global.MailConfig.Username, Global.MailConfig.Password);

            try
            {
                client.Send(msg);

                MessageBox.Show("Mail has been sent.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string error = string.Format("Cannot send mail!\r\nError: {0}", ex.Message);
                MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnUpload()
        {
            foreach (var info in _resultFileInfos)
            {
                if (info.IsImageFile)
                {
                    string fn = string.Format("{0}\\{1}", Application.StartupPath, Path.GetFileName(info.FileName));
                    //info.TemplateName = _templateName;
                    //info.LogoName = _logoName;
                    Image img = info.ProcessResultImage();
                    img.Save(fn, ImageFormat.Jpeg);

                    string remoteFileName = string.Format("{0}/{1}", Global.FTPFolder, Path.GetFileName(fn));
                    Result result = FTP.UploadFile(Global.FTPConnectionInfo, fn, remoteFileName);
                    if (!result.IsOK)
                    {
                        MessageBox.Show(result.GetErrorAsString("FTP.UploadFile"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Image thumbnail = Utility.FixedSize(img, 320, 320);
                    string thumbnailFileName = string.Format("{0}\\{1}_thumb{2}",
                        Application.StartupPath, Path.GetFileNameWithoutExtension(fn), Path.GetExtension(fn));

                    thumbnail.Save(thumbnailFileName, ImageFormat.Jpeg);
                    thumbnail.Dispose();
                    thumbnail = null;
                    //img.Dispose();
                    //img = null;

                    remoteFileName = string.Format("{0}/{1}", Global.FTPFolder, Path.GetFileName(thumbnailFileName));
                    result = FTP.UploadFile(Global.FTPConnectionInfo, thumbnailFileName, remoteFileName);

                    File.Delete(thumbnailFileName);
                    File.Delete(fn);

                    if (!result.IsOK)
                    {
                        MessageBox.Show(result.GetErrorAsString("FTP.UploadFile"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    string remoteFileName = string.Format("{0}/{1}", Global.FTPFolder, Path.GetFileName(info.FileName));
                    Result result = FTP.UploadFile(Global.FTPConnectionInfo, info.FileName, remoteFileName);
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
            {
                picViewer.Image = null;
                toolStripButtonRotateLeft.Enabled = false;
                toolStripButtonRotateRight.Enabled = false;
                toolStripButtonAddText.Enabled = false;
                return;
            }

            ResultFileInfo info = lvFile.SelectedItems[0].Tag as ResultFileInfo;
            //info.TemplateName = toolStripComboBoxTemplates.SelectedItem == null ? string.Empty : toolStripComboBoxTemplates.SelectedItem.ToString();
            //info.LogoName = toolStripComboBoxLogo.SelectedItem == null ? string.Empty : toolStripComboBoxLogo.SelectedItem.ToString();

            Image img = info.ProcessResultImage();
            if (img != null)
            {
                //img.Save(string.Format("D:\\test.jpg", Application.StartupPath));
                img = Utility.FixedSizeAndCrop(img, picViewer.Width, picViewer.Height);
                toolStripButtonRotateLeft.Enabled = true;
                toolStripButtonRotateRight.Enabled = true;
                toolStripButtonAddText.Enabled = info.TemplateName != "[None]" ? true : false;
            }
            else
            {
                toolStripButtonRotateLeft.Enabled = false;
                toolStripButtonRotateRight.Enabled = false;
                toolStripButtonAddText.Enabled = false;
            }

            picViewer.Image = img;
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
                    ResultFileInfo info = new ResultFileInfo();
                    info.FileName = file;
                    info.TemplateName = toolStripComboBoxTemplates.SelectedItem == null ? "[None]" : toolStripComboBoxTemplates.SelectedItem.ToString();
                    info.LogoName = toolStripComboBoxLogo.SelectedItem == null ? "[None]" : toolStripComboBoxLogo.SelectedItem.ToString();
                    info.ProcessResultImage();
                    item.Tag = info;
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

        private void toolStripComboBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OnViewImage();
        }

        private void toolStripComboBoxLogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OnViewImage();
        }

        private void toolStripImage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Execute(e.ClickedItem.ToolTipText);
        }

        private void UploadFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to quit this program ?", 
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
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

    public class ResultFileInfo
    {
        #region Members
        public string FileName = string.Empty;
        public string LogoName = "[None]";
        public string TemplateName = "[None]";
        public string Text1 = string.Empty;
        public string Text2 = string.Empty;
        public string Text3 = string.Empty;
        public Image OrgImage = null;
        public FileType Type = FileType.Result;
        #endregion

        #region Constructor
        public ResultFileInfo()
        {

        }
        #endregion

        #region Properties
        public bool IsImageFile
        {
            get
            {
                string ext = Path.GetExtension(FileName).ToLower();
                if (ext == ".bmp" || ext == ".png" || ext == ".jpg" ||
                    ext == ".jpeg" || ext == ".jpe" || ext == ".gif")
                    return true;

                return false;
            }
        }
        #endregion

        #region Methods
        public Image ProcessResultImage()
        {
            if (!IsImageFile) return null;

            Image resultImage = null;

            if (OrgImage == null)
            {
                if (Type == FileType.Result)
                    OrgImage = Utility.LoadImageFromFile(FileName);
                else
                {
                    string AdsFileName = string.Format("{0}\\Ads\\{1}", Application.StartupPath, FileName);
                    OrgImage = Utility.LoadImageFromFile(AdsFileName);
                }
            }

            resultImage = OrgImage;

            if (TemplateName != "[None]")
            {
                string templateFileName = string.Format("{0}\\ImageTemplates\\{1}", Application.StartupPath, TemplateName);
                if (File.Exists(templateFileName))
                {
                    string logoFileName = string.Format("{0}\\Logo\\{1}", Application.StartupPath, LogoName);
                    Image logo = null;
                    if (File.Exists(logoFileName))
                        logo = Utility.LoadImageFromFile(logoFileName);

                    Image imgTemplate = Utility.LoadImageFromFile(templateFileName);
                    Rectangle logoRect = new Rectangle(404, 120, 708, 185);
                    Rectangle contentRect = new Rectangle(97, 480, 1092, 1183);
                    Rectangle textRect = new Rectangle(405, 310, 690, 96);
                    resultImage = Utility.FillData2ImageTemplate(imgTemplate, logo, OrgImage, logoRect, contentRect, textRect, Text1, Text2, Text3);
                }
            }

            return resultImage;
        }

        public void RotateOrgImage(RotateFlipType rotateFlipType)
        {
            if (OrgImage != null)
                OrgImage = Utility.RotateImage(OrgImage, rotateFlipType);
        }

        public void Clear()
        {
            if (OrgImage != null)
            {
                OrgImage.Dispose();
                OrgImage = null;
            }
        }
        #endregion
    }

    public enum FileType : int
    {
        Result = 0,
        Ads
    }
}
