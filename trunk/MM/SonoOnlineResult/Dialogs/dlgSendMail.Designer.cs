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
            this.txtTo = new SonoOnlineResult.Dialogs.uAutoComplete();
            this.SuspendLayout();
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(7, 8);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(56, 23);
            this.btnTo.TabIndex = 0;
            this.btnTo.Text = "To...";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(69, 8);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(400, 23);
            this.txtTo.TabIndex = 1;
            this.txtTo.Value = "";
            // 
            // dlgSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 169);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.btnTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSendMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Mail";
            this.Move += new System.EventHandler(this.dlgSendMail_Move);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTo;
        private uAutoComplete txtTo;
    }
}