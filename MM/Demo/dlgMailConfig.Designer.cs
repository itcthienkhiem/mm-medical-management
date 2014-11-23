namespace Demo
{
    partial class dlgMailConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMailConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSMTP = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chbUseSMTPServer = new System.Windows.Forms.CheckBox();
            this.txtSenderMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBoxSMTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbUseSMTPServer);
            this.groupBox1.Controls.Add(this.groupBoxSMTP);
            this.groupBox1.Controls.Add(this.txtSenderMail);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBoxSMTP
            // 
            this.groupBoxSMTP.Controls.Add(this.numPort);
            this.groupBoxSMTP.Controls.Add(this.txtPassword);
            this.groupBoxSMTP.Controls.Add(this.label5);
            this.groupBoxSMTP.Controls.Add(this.txtUserName);
            this.groupBoxSMTP.Controls.Add(this.label6);
            this.groupBoxSMTP.Controls.Add(this.label4);
            this.groupBoxSMTP.Controls.Add(this.txtServer);
            this.groupBoxSMTP.Controls.Add(this.label3);
            this.groupBoxSMTP.Enabled = false;
            this.groupBoxSMTP.Location = new System.Drawing.Point(13, 47);
            this.groupBoxSMTP.Name = "groupBoxSMTP";
            this.groupBoxSMTP.Size = new System.Drawing.Size(285, 125);
            this.groupBoxSMTP.TabIndex = 6;
            this.groupBoxSMTP.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(100, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(168, 20);
            this.txtPassword.TabIndex = 15;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Password:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(100, 69);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(168, 20);
            this.txtUserName.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "User name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Port:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(100, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(168, 20);
            this.txtServer.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Server:";
            // 
            // chbUseSMTPServer
            // 
            this.chbUseSMTPServer.AutoSize = true;
            this.chbUseSMTPServer.Location = new System.Drawing.Point(19, 47);
            this.chbUseSMTPServer.Name = "chbUseSMTPServer";
            this.chbUseSMTPServer.Size = new System.Drawing.Size(110, 17);
            this.chbUseSMTPServer.TabIndex = 4;
            this.chbUseSMTPServer.Text = "Use SMTP server";
            this.chbUseSMTPServer.UseVisualStyleBackColor = true;
            this.chbUseSMTPServer.CheckedChanged += new System.EventHandler(this.chbUseSMTPServer_CheckedChanged);
            // 
            // txtSenderMail
            // 
            this.txtSenderMail.Location = new System.Drawing.Point(81, 19);
            this.txtSenderMail.Name = "txtSenderMail";
            this.txtSenderMail.Size = new System.Drawing.Size(217, 20);
            this.txtSenderMail.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sender mail:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(202, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(123, 193);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(44, 193);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "&Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(100, 46);
            this.numPort.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(68, 20);
            this.numPort.TabIndex = 16;
            this.numPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // dlgMailConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(321, 222);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgMailConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mail Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgMailConfig_FormClosing);
            this.Load += new System.EventHandler(this.dlgMailConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSMTP.ResumeLayout(false);
            this.groupBoxSMTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBoxSMTP;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbUseSMTPServer;
        public System.Windows.Forms.TextBox txtSenderMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.NumericUpDown numPort;
    }
}