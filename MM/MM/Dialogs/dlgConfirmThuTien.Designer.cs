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
            this.dtpkNgayXuat = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.raChuaThuTien = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // raDaThuTien
            // 
            this.raDaThuTien.AutoSize = true;
            this.raDaThuTien.Checked = true;
            this.raDaThuTien.Location = new System.Drawing.Point(14, 49);
            this.raDaThuTien.Name = "raDaThuTien";
            this.raDaThuTien.Size = new System.Drawing.Size(77, 17);
            this.raDaThuTien.TabIndex = 0;
            this.raDaThuTien.TabStop = true;
            this.raDaThuTien.Text = "Đã thu tiền";
            this.raDaThuTien.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpkNgayXuat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.raChuaThuTien);
            this.groupBox1.Controls.Add(this.raDaThuTien);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 108);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtpkNgayXuat
            // 
            this.dtpkNgayXuat.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayXuat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayXuat.Location = new System.Drawing.Point(76, 20);
            this.dtpkNgayXuat.Name = "dtpkNgayXuat";
            this.dtpkNgayXuat.Size = new System.Drawing.Size(98, 20);
            this.dtpkNgayXuat.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Ngày xuất:";
            // 
            // raChuaThuTien
            // 
            this.raChuaThuTien.AutoSize = true;
            this.raChuaThuTien.Location = new System.Drawing.Point(97, 49);
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
            this.btnOK.Location = new System.Drawing.Point(169, 115);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Ghi  chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(69, 73);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(316, 20);
            this.txtGhiChu.TabIndex = 19;
            // 
            // dlgConfirmThuTien
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 145);
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
        private System.Windows.Forms.DateTimePicker dtpkNgayXuat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label2;
    }
}