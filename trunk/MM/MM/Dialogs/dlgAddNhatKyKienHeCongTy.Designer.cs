﻿namespace MM.Dialogs
{
    partial class dlgAddNhatKyKienHeCongTy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddNhatKyKienHeCongTy));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoiDungLienHe = new System.Windows.Forms.TextBox();
            this.cboCongTyLienHe = new System.Windows.Forms.ComboBox();
            this.dtpkNgayGioLienHe = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNguoiLienHe = new System.Windows.Forms.TextBox();
            this.txtSoDienThoaiLienHe = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSoDienThoaiLienHe);
            this.groupBox1.Controls.Add(this.txtNguoiLienHe);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNoiDungLienHe);
            this.groupBox1.Controls.Add(this.cboCongTyLienHe);
            this.groupBox1.Controls.Add(this.dtpkNgayGioLienHe);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtNoiDungLienHe
            // 
            this.txtNoiDungLienHe.Location = new System.Drawing.Point(109, 106);
            this.txtNoiDungLienHe.Multiline = true;
            this.txtNoiDungLienHe.Name = "txtNoiDungLienHe";
            this.txtNoiDungLienHe.Size = new System.Drawing.Size(284, 75);
            this.txtNoiDungLienHe.TabIndex = 7;
            // 
            // cboCongTyLienHe
            // 
            this.cboCongTyLienHe.FormattingEnabled = true;
            this.cboCongTyLienHe.Location = new System.Drawing.Point(109, 39);
            this.cboCongTyLienHe.MaxLength = 255;
            this.cboCongTyLienHe.Name = "cboCongTyLienHe";
            this.cboCongTyLienHe.Size = new System.Drawing.Size(284, 21);
            this.cboCongTyLienHe.TabIndex = 4;
            // 
            // dtpkNgayGioLienHe
            // 
            this.dtpkNgayGioLienHe.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkNgayGioLienHe.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayGioLienHe.Location = new System.Drawing.Point(109, 17);
            this.dtpkNgayGioLienHe.Name = "dtpkNgayGioLienHe";
            this.dtpkNgayGioLienHe.Size = new System.Drawing.Size(144, 20);
            this.dtpkNgayGioLienHe.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nội dung liên hệ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Công ty liên hệ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày giờ liên hệ:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(208, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(129, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Người liên hệ:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Số ĐT liên hệ:";
            // 
            // txtNguoiLienHe
            // 
            this.txtNguoiLienHe.Location = new System.Drawing.Point(109, 62);
            this.txtNguoiLienHe.Name = "txtNguoiLienHe";
            this.txtNguoiLienHe.Size = new System.Drawing.Size(284, 20);
            this.txtNguoiLienHe.TabIndex = 5;
            // 
            // txtSoDienThoaiLienHe
            // 
            this.txtSoDienThoaiLienHe.Location = new System.Drawing.Point(109, 84);
            this.txtSoDienThoaiLienHe.Name = "txtSoDienThoaiLienHe";
            this.txtSoDienThoaiLienHe.Size = new System.Drawing.Size(154, 20);
            this.txtSoDienThoaiLienHe.TabIndex = 6;
            // 
            // dlgAddNhatKyKienHeCongTy
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 228);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddNhatKyKienHeCongTy";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them nhat ky lien he cong ty";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddNhatKyKienHeCongTy_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddNhatKyKienHeCongTy_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNoiDungLienHe;
        private System.Windows.Forms.ComboBox cboCongTyLienHe;
        private System.Windows.Forms.DateTimePicker dtpkNgayGioLienHe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSoDienThoaiLienHe;
        private System.Windows.Forms.TextBox txtNguoiLienHe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}