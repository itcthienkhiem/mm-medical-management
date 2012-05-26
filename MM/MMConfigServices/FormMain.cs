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

namespace MMConfigServices
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            OnInitConfig();
        }

        private void OnInitConfig()
        {
            if (File.Exists(Global.AppConfig))
            {
                Configuration.LoadData(Global.AppConfig);

                object obj = Configuration.GetValues(Const.ServerNameKey);
                if (obj != null) Global.ConnectionInfo.ServerName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.DatabaseNameKey);
                if (obj != null) Global.ConnectionInfo.DatabaseName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.AuthenticationKey);
                if (obj != null) Global.ConnectionInfo.Authentication = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.UserNameKey);
                if (obj != null) Global.ConnectionInfo.UserName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.PasswordKey);
                if (obj != null)
                {
                    string password = Convert.ToString(obj);
                    RijndaelCrypto crypto = new RijndaelCrypto();
                    Global.ConnectionInfo.Password = crypto.Decrypt(password);
                }

                obj = Configuration.GetValues(Const.FTPServerNameKey);
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
        }

        private void SaveAppConfig()
        {
            Configuration.SetValues(Const.ServerNameKey, Global.ConnectionInfo.ServerName);
            Configuration.SetValues(Const.DatabaseNameKey, Global.ConnectionInfo.DatabaseName);
            Configuration.SetValues(Const.AuthenticationKey, Global.ConnectionInfo.Authentication);
            Configuration.SetValues(Const.UserNameKey, Global.ConnectionInfo.UserName);
            RijndaelCrypto crypto = new RijndaelCrypto();
            string password = crypto.Encrypt(Global.ConnectionInfo.Password);
            Configuration.SetValues(Const.PasswordKey, password);

            Configuration.SetValues(Const.FTPServerNameKey, Global.FTPConnectionInfo.ServerName);
            Configuration.SetValues(Const.FTPUserNameKey, Global.FTPConnectionInfo.Username);
            password = crypto.Encrypt(Global.FTPConnectionInfo.Password);
            Configuration.SetValues(Const.FTPPasswordKey, password);

            Configuration.SaveData(Global.AppConfig);
        }

        private void btnConfigDatabase_Click(object sender, EventArgs e)
        {
            MM.Dialogs.dlgDatabaseConfig dlg = new MM.Dialogs.dlgDatabaseConfig();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dlg.SetAppConfig();
                SaveAppConfig();
            }

        }

        private void btnConfigFTP_Click(object sender, EventArgs e)
        {
            MM.Dialogs.dlgFTPConfig dlg = new MM.Dialogs.dlgFTPConfig();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.IsChangeConnectionInfo)
                {
                    dlg.SetAppConfig();
                    SaveAppConfig();
                }
            }
        }

        private void btnCauHinhMayXetNghiem_Click(object sender, EventArgs e)
        {
            MM.Dialogs.dlgPortConfig dlg = new MM.Dialogs.dlgPortConfig();
            dlg.ShowDialog(this);
            MM.Common.Utility.ResetMMSerivice();
        }
    }
}
