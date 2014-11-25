using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SonoOnlineResult.Dialogs
{
    public partial class TestMailForm : Form
    {
        private string recipient;
        public string Recipient
        {
            get { return recipient; }
        }

        private string subject;
        public string Subject
        {
            get { return subject; }
        }

        private string body;
        public string Body
        {
            get { return body; }
        }

        public TestMailForm()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(txtRecipient.Text))
            {
                errorProvider1.SetError(txtRecipient, "Please enter recipient email.");
                return false;
            }

            return true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                recipient = txtRecipient.Text;
                subject = txtSubject.Text;
                body = txtBody.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
