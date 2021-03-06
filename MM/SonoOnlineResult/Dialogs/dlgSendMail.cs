/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
        private string _passcode = string.Empty;
        #endregion

        #region Constructor
        public dlgSendMail()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public List<string> ToEmailList
        {
            get { return txtTo.GetEmailList(); }
        }

        public List<string> CcEmailList
        {
            get { return txtCc.GetEmailList(); }
        }

        public bool UsingMailTemplate
        {
            get { return cboMailTemplate.SelectedIndex > 0; }
        }

        public string MailTemplate
        {
            get { return cboMailTemplate.Text; }
            set { cboMailTemplate.Text = value; }
        }

        public string Subject
        {
            get { return txtSubject.Text; }
            set { txtSubject.Text = value; }
        }

        public string Body
        {
            get { return txtBody.Text; }
            set { txtBody.Text = value; }
        }

        public string Passcode
        {
            get { return _passcode; }
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

            if (!txtTo.CheckInfo())
                return false;

            if (txtTo.GetEmailList().Count > 1)
            {
                MessageBox.Show("To email address only one.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTo.TxtBox.Focus();
                return false;
            }

            if (txtCc.Value.Trim() != string.Empty && !txtCc.CheckInfo())
                return false;

            if (txtSubject.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter subject", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubject.Focus();
                return false;
            }

            return true;
        }

        private void InitData()
        {
            MySQLHelper._connectionString = Global.MySQLConnectionInfo.ConnectionString;

            if (File.Exists(Global.EmailListPath))
            {
                if (_emailList.Deserialize(Global.EmailListPath))
                {
                    List<string> emails = _emailList.GetEmails();
                    txtTo.Values = emails;
                    txtCc.Values = emails;
                }
            }

            cboMailTemplate.Items.Add("");
            foreach (var template in Global.MailTemplateList.TemplateList)
            {
                cboMailTemplate.Items.Add(template.TemplateName);
            }

            cboMailTemplate.SelectedIndex = 0;
        }
        #endregion

        #region Window Event Handlers
        private void dlgSendMail_Move(object sender, EventArgs e)
        {
            txtTo.RecalLocation();
            txtCc.RecalLocation();
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            txtTo.Hide();
            txtCc.Hide();

            dlgEmailList dlg = new dlgEmailList();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtTo.Value = dlg.SelectedEmails[0] + "; ";
                txtTo.TxtBox.SelectionStart = txtTo.Value.Length;
                txtTo.Hide();
            }
        }

        private void btnCc_Click(object sender, EventArgs e)
        {
            txtTo.Hide();
            txtCc.Hide();

            dlgEmailList dlg = new dlgEmailList();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<string> selectedEmails = dlg.SelectedEmails;
                foreach (var email in selectedEmails)
                {
                    txtCc.TxtBox.AppendText(string.Format("{0}; ", email));
                }

                txtCc.TxtBox.SelectionStart = txtTo.Value.Length;
                txtCc.Hide();
            }
        }

        private void dlgSendMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySQLHelper._connectionString = Global.MySQLConnectionInfo.ConnectionString;

            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo())
                {
                    txtTo.Hide();
                    txtCc.Hide();
                    e.Cancel = true;
                }
                else
                {
                    dlgPasscode dlg = new dlgPasscode();
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        _passcode = dlg.Passcode;
                        List<string> toEmailList = txtTo.GetEmailList();
                        List<string> ccEmailList = txtCc.GetEmailList();
                        foreach (var mail in toEmailList)
                            _emailList.Add(mail);

                        foreach (var mail in ccEmailList)
                            _emailList.Add(mail);

                        _emailList.Serialize(Global.EmailListPath);

                        txtTo.Clear();
                        txtCc.Clear();
                    }
                    else
                        e.Cancel = true;
                }
            }
            else
            {
                txtTo.Hide();
                txtCc.Hide();
            }
        }

        private void dlgSendMail_Load(object sender, EventArgs e)
        {
            //InitData();   
        }

        private void cboMailTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMailTemplate.SelectedIndex == 0)
            {
                txtSubject.Text = string.Empty;
                txtBody.Text = string.Empty;
            }
            else
            {
                MailTemplate template = Global.MailTemplateList.GetMailTemplate(cboMailTemplate.Text);
                if (template != null)
                {
                    txtSubject.Text = template.Subject;
                    txtBody.Text = template.Body;
                }
            }
        }
        #endregion
    }
}
