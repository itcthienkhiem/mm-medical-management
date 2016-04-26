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
namespace MM.Dialogs
{
    partial class dlgAddChiTietKetQuaXetNghiemTay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddChiTietKetQuaXetNghiemTay));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHutThuoc = new System.Windows.Forms.CheckBox();
            this.txtXetNghiem = new System.Windows.Forms.TextBox();
            this.chkLamThem = new System.Windows.Forms.CheckBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnChonXetNghiem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xetNghiemManualBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dtpkNgayXetNghiem = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpkNgayXetNghiem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkHutThuoc);
            this.groupBox1.Controls.Add(this.txtXetNghiem);
            this.groupBox1.Controls.Add(this.chkLamThem);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.btnChonXetNghiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkHutThuoc
            // 
            this.chkHutThuoc.AutoSize = true;
            this.chkHutThuoc.Location = new System.Drawing.Point(102, 92);
            this.chkHutThuoc.Name = "chkHutThuoc";
            this.chkHutThuoc.Size = new System.Drawing.Size(73, 17);
            this.chkHutThuoc.TabIndex = 6;
            this.chkHutThuoc.Text = "Hút thuốc";
            this.chkHutThuoc.UseVisualStyleBackColor = true;
            // 
            // txtXetNghiem
            // 
            this.txtXetNghiem.Location = new System.Drawing.Point(102, 42);
            this.txtXetNghiem.Name = "txtXetNghiem";
            this.txtXetNghiem.ReadOnly = true;
            this.txtXetNghiem.Size = new System.Drawing.Size(311, 20);
            this.txtXetNghiem.TabIndex = 7;
            // 
            // chkLamThem
            // 
            this.chkLamThem.AutoSize = true;
            this.chkLamThem.Location = new System.Drawing.Point(102, 113);
            this.chkLamThem.Name = "chkLamThem";
            this.chkLamThem.Size = new System.Drawing.Size(72, 17);
            this.chkLamThem.TabIndex = 7;
            this.chkLamThem.Text = "Làm thêm";
            this.chkLamThem.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(102, 66);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(122, 20);
            this.txtResult.TabIndex = 5;
            // 
            // btnChonXetNghiem
            // 
            this.btnChonXetNghiem.Location = new System.Drawing.Point(419, 40);
            this.btnChonXetNghiem.Name = "btnChonXetNghiem";
            this.btnChonXetNghiem.Size = new System.Drawing.Size(106, 23);
            this.btnChonXetNghiem.TabIndex = 4;
            this.btnChonXetNghiem.Text = "Chọn xét nghiệm...";
            this.btnChonXetNghiem.UseVisualStyleBackColor = true;
            this.btnChonXetNghiem.Click += new System.EventHandler(this.btnChonXetNghiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kết quả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xét nghiệm:";
            // 
            // xetNghiemManualBindingSource
            // 
            this.xetNghiemManualBindingSource.DataSource = typeof(MM.Databasae.XetNghiem_Manual);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(275, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(196, 148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dtpkNgayXetNghiem
            // 
            this.dtpkNgayXetNghiem.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkNgayXetNghiem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayXetNghiem.Location = new System.Drawing.Point(102, 18);
            this.dtpkNgayXetNghiem.Name = "dtpkNgayXetNghiem";
            this.dtpkNgayXetNghiem.Size = new System.Drawing.Size(143, 20);
            this.dtpkNgayXetNghiem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ngày xét nghiệm:";
            // 
            // dlgAddChiTietKetQuaXetNghiemTay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(546, 179);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddChiTietKetQuaXetNghiemTay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them chi tiet ket qua xet nghiem tay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource xetNghiemManualBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChonXetNghiem;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.CheckBox chkLamThem;
        private System.Windows.Forms.TextBox txtXetNghiem;
        private System.Windows.Forms.CheckBox chkHutThuoc;
        private System.Windows.Forms.DateTimePicker dtpkNgayXetNghiem;
        private System.Windows.Forms.Label label3;
    }
}
