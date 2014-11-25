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
    public partial class dlgSendMail : Form
    {
        #region Members
        private List<string> _values = new List<string>();
        private EmailList _emailList = new EmailList();
        #endregion

        #region Constructor
        public dlgSendMail()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<string> Emails
        {
            get
            {
                List<string> emailList = new List<string>();
                string emailStr = txtTo.Value.Trim().Replace(",", ";");
                string[] emails = emailStr.Split(";".ToCharArray());
                foreach (var email in emails)
                {
                    if (email.Trim() == string.Empty) continue;
                    emailList.Add(email.Trim().ToLower());
                }

                return emailList;
            }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtTo.Value.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter recipient email adderss.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTo.TxtBox.Focus();
                return false;
            }

            string emailStr = txtTo.Value.Trim().Replace(",", ";");
            string[] emails = emailStr.Split(";".ToCharArray());
            foreach (var email in emails)
            {
                if (email.Trim() == string.Empty) continue;

                if (!Utility.IsValidEmail(email.Trim().ToLower()))
                {
                    MessageBox.Show(string.Format("The email address: '{0}' is invalid.", email.Trim().ToLower()), 
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTo.TxtBox.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgSendMail_Move(object sender, EventArgs e)
        {
            txtTo.RecalLocation();
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            txtTo.Hide();
        }

        private void btnCc_Click(object sender, EventArgs e)
        {
            txtTo.Hide();
        }

        private void dlgSendMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySQLHelper._connectionString = Global.MySQLConnectionInfo.ConnectionString;

            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo())
                {
                    txtTo.Hide();
                    e.Cancel = true;
                }
                else
                {
                    foreach (var mail in Emails)
                    {
                        _emailList.Add(mail);
                    }

                    _emailList.Serialize(Global.EmailListPath);

                    txtTo.Clear();
                }
            }
            else
                txtTo.Hide();
        }

        private void dlgSendMail_Load(object sender, EventArgs e)
        {
            //MySQLHelper._connectionString = Global.MySQLConnectionInfo.ConnectionString;

            if (File.Exists(Global.EmailListPath))
            {
                if (_emailList.Deserialize(Global.EmailListPath))
                {
                    txtTo.Values = _emailList.GetEmails();
                }
            }
        }
        #endregion

    }
}
