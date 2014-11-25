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
            this.btnCc = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtCc = new SonoOnlineResult.Dialogs.uAutoComplete();
            this.txtTo = new SonoOnlineResult.Dialogs.uAutoComplete();
            this.SuspendLayout();
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(7, 9);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(56, 23);
            this.btnTo.TabIndex = 0;
            this.btnTo.TabStop = false;
            this.btnTo.Text = "To...";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnCc
            // 
            this.btnCc.Location = new System.Drawing.Point(7, 38);
            this.btnCc.Name = "btnCc";
            this.btnCc.Size = new System.Drawing.Size(56, 23);
            this.btnCc.TabIndex = 2;
            this.btnCc.TabStop = false;
            this.btnCc.Text = "Cc...";
            this.btnCc.UseVisualStyleBackColor = true;
            this.btnCc.Click += new System.EventHandler(this.btnCc_Click);
            // 
            // btnSend
            // 
            this.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSend.Location = new System.Drawing.Point(163, 67);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(243, 67);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtCc
            // 
            this.txtCc.Location = new System.Drawing.Point(69, 38);
            this.txtCc.Name = "txtCc";
            this.txtCc.Size = new System.Drawing.Size(400, 23);
            this.txtCc.TabIndex = 3;
            this.txtCc.Value = "";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(69, 9);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(400, 23);
            this.txtTo.TabIndex = 1;
            this.txtTo.Value = "";
            // 
            // dlgSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 96);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtCc);
            this.Controls.Add(this.btnCc);
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
            this.Move += new System.EventHandler(this.dlgSendMail_Move);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTo;
        private uAutoComplete txtTo;
        private uAutoComplete txtCc;
        private System.Windows.Forms.Button btnCc;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
    }
}