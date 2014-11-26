namespace SonoOnlineResult.Dialogs
{
    partial class dlgSendMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSendMail));
            this.btnTo = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMailTemplate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.RichTextBox();
            this.txtCc = new SonoOnlineResult.Dialogs.uAutoComplete();
            this.txtTo = new SonoOnlineResult.Dialogs.uAutoComplete();
            this.SuspendLayout();
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(7, 10);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(73, 23);
            this.btnTo.TabIndex = 0;
            this.btnTo.TabStop = false;
            this.btnTo.Text = "To...";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnSend
            // 
            this.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSend.Location = new System.Drawing.Point(255, 419);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 20;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(333, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnCc
            // 
            this.btnCc.Location = new System.Drawing.Point(7, 36);
            this.btnCc.Name = "btnCc";
            this.btnCc.Size = new System.Drawing.Size(73, 23);
            this.btnCc.TabIndex = 6;
            this.btnCc.TabStop = false;
            this.btnCc.Text = "Cc...";
            this.btnCc.UseVisualStyleBackColor = true;
            this.btnCc.Click += new System.EventHandler(this.btnCc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mail Template:";
            // 
            // cboMailTemplate
            // 
            this.cboMailTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMailTemplate.FormattingEnabled = true;
            this.cboMailTemplate.Location = new System.Drawing.Point(83, 63);
            this.cboMailTemplate.Name = "cboMailTemplate";
            this.cboMailTemplate.Size = new System.Drawing.Size(296, 21);
            this.cboMailTemplate.TabIndex = 10;
            this.cboMailTemplate.SelectedIndexChanged += new System.EventHandler(this.cboMailTemplate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Subject:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Body:";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(83, 88);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(573, 20);
            this.txtSubject.TabIndex = 13;
            // 
            // txtBody
            // 
            this.txtBody.AcceptsTab = true;
            this.txtBody.EnableAutoDragDrop = true;
            this.txtBody.Location = new System.Drawing.Point(83, 112);
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(573, 304);
            this.txtBody.TabIndex = 14;
            this.txtBody.Text = "";
            // 
            // txtCc
            // 
            this.txtCc.Location = new System.Drawing.Point(83, 36);
            this.txtCc.Name = "txtCc";
            this.txtCc.Size = new System.Drawing.Size(573, 23);
            this.txtCc.TabIndex = 2;
            this.txtCc.Value = "";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(83, 10);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(573, 23);
            this.txtTo.TabIndex = 1;
            this.txtTo.Value = "";
            // 
            // dlgSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 446);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMailTemplate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCc);
            this.Controls.Add(this.btnCc);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.btnTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSendMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Mail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgSendMail_FormClosing);
            this.Load += new System.EventHandler(this.dlgSendMail_Load);
            this.Move += new System.EventHandler(this.dlgSendMail_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTo;
        private uAutoComplete txtTo;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private uAutoComplete txtCc;
        private System.Windows.Forms.Button btnCc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMailTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.RichTextBox txtBody;
    }
}