using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MailBee.Mime;
using MailBee.SmtpMail;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgMailConfig : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgMailConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtSenderMail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter sender mail.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenderMail.Focus();
                return false;
            }

            if (!MM.Common.Utility.IsValidEmail(txtSenderMail.Text))
            {
                MessageBox.Show("Sender mail is invalid.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenderMail.Focus();
                return false;
            }

            if (chbUseSMTPServer.Checked)
            {
                if (txtServer.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please enter server.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtServer.Focus();
                    return false;
                }

                if (txtUserName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please enter username.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    return false;
                }
            }


            return true;
        }

        private void SaveMailConfig()
        {
            Global.MailConfig.SenderMail = txtSenderMail.Text;
            Global.MailConfig.UseSMTPServer = chbUseSMTPServer.Checked;
            if (chbUseSMTPServer.Checked)
            {
                Global.MailConfig.Server = txtServer.Text;
                Global.MailConfig.Port = (int)numPort.Value;
                Global.MailConfig.Username = txtUserName.Text;
                Global.MailConfig.Password = txtPassword.Text;
            }

            Global.MailConfig.Serialize(Global.MailConfigPath);
        }
        #endregion

        #region Window Event Handlers
        private void dlgMailConfig_Load(object sender, EventArgs e)
        {
            txtSenderMail.Text = Global.MailConfig.SenderMail;
            chbUseSMTPServer.Checked = Global.MailConfig.UseSMTPServer;
            if (Global.MailConfig.UseSMTPServer)
            {
                txtServer.Text = Global.MailConfig.Server;
                numPort.Value = (decimal)Global.MailConfig.Port;
                txtUserName.Text = Global.MailConfig.Username;
                txtPassword.Text = Global.MailConfig.Password;
            }
        }

        private void chbUseSMTPServer_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSMTP.Enabled = chbUseSMTPServer.Checked;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestMailForm testMailForm = new TestMailForm();
            if (testMailForm.ShowDialog(this) != DialogResult.OK)
                return;

            MailMessage msg = new MailMessage();
            msg.From = new EmailAddress(txtSenderMail.Text, txtSenderMail.Text);
            msg.To.Add(new EmailAddress(testMailForm.Recipient));
            msg.Subject = testMailForm.Subject; // "[Notification Service] Test mail";
            msg.BodyPlainText = testMailForm.Body; // "This is a test mail.";

            Smtp.LicenseKey = "MN200-B47C7EFF7C257CFF7C2E34E777B5-D2BD";
            Smtp smtp = new Smtp();
            if (chbUseSMTPServer.Checked)
            {
                SmtpServer server = new SmtpServer();
                server.Name = txtServer.Text;
                server.Port = (int)numPort.Value;
                server.AccountName = txtUserName.Text;
                server.Password = txtPassword.Text;
                server.AuthMethods = MailBee.AuthenticationMethods.SaslLogin | MailBee.AuthenticationMethods.SaslPlain;
                smtp.SmtpServers.Add(server);
            }
            else
                smtp.DnsServers.Autodetect();

            try
            {
                smtp.Message = msg;
                if (smtp.Send())
                {
                    MessageBox.Show("Mail has been sent.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void dlgMailConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else
                    SaveMailConfig();
            }
        }
        #endregion

        
    }
}
