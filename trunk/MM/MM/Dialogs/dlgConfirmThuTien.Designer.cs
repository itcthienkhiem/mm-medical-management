namespace MM.Dialogs
{
    partial class dlgConfirmThuTien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgConfirmThuTien));
            this.raDaThuTien = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raChuaThuTien = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // raDaThuTien
            // 
            this.raDaThuTien.AutoSize = true;
            this.raDaThuTien.Checked = true;
            this.raDaThuTien.Location = new System.Drawing.Point(17, 19);
            this.raDaThuTien.Name = "raDaThuTien";
            this.raDaThuTien.Size = new System.Drawing.Size(77, 17);
            this.raDaThuTien.TabIndex = 0;
            this.raDaThuTien.TabStop = true;
            this.raDaThuTien.Text = "Đã thu tiền";
            this.raDaThuTien.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raChuaThuTien);
            this.groupBox1.Controls.Add(this.raDaThuTien);
            this.groupBox1.Location = new System.Drawing.Point(5, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // raChuaThuTien
            // 
            this.raChuaThuTien.AutoSize = true;
            this.raChuaThuTien.Location = new System.Drawing.Point(100, 19);
            this.raChuaThuTien.Name = "raChuaThuTien";
            this.raChuaThuTien.Size = new System.Drawing.Size(88, 17);
            this.raChuaThuTien.TabIndex = 1;
            this.raChuaThuTien.Text = "Chưa thu tiền";
            this.raChuaThuTien.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(70, 55);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgConfirmThuTien
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 86);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgConfirmThuTien";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xac nhan thu tien";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgConfirmThuTien_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton raDaThuTien;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raChuaThuTien;
        private System.Windows.Forms.Button btnOK;
    }
}