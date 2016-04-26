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
    partial class dlgAddKetLuan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddKetLuan));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLyDo_2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkKhong_3 = new System.Windows.Forms.CheckBox();
            this.chkCo_3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLyDo_1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkKhong_2 = new System.Windows.Forms.CheckBox();
            this.chkCo_2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCacXetNghiemLamThem = new System.Windows.Forms.TextBox();
            this.chkKhong_1 = new System.Windows.Forms.CheckBox();
            this.chkCo_1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpkNgayKetLuan = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLyDo_2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkKhong_3);
            this.groupBox1.Controls.Add(this.chkCo_3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLyDo_1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkKhong_2);
            this.groupBox1.Controls.Add(this.chkCo_2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCacXetNghiemLamThem);
            this.groupBox1.Controls.Add(this.chkKhong_1);
            this.groupBox1.Controls.Add(this.chkCo_1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dtpkNgayKetLuan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 246);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin kết luận";
            // 
            // txtLyDo_2
            // 
            this.txtLyDo_2.Location = new System.Drawing.Point(438, 210);
            this.txtLyDo_2.Name = "txtLyDo_2";
            this.txtLyDo_2.Size = new System.Drawing.Size(116, 20);
            this.txtLyDo_2.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Loại:";
            // 
            // chkKhong_3
            // 
            this.chkKhong_3.AutoSize = true;
            this.chkKhong_3.Location = new System.Drawing.Point(335, 213);
            this.chkKhong_3.Name = "chkKhong_3";
            this.chkKhong_3.Size = new System.Drawing.Size(57, 17);
            this.chkKhong_3.TabIndex = 40;
            this.chkKhong_3.Text = "Không";
            this.chkKhong_3.UseVisualStyleBackColor = true;
            this.chkKhong_3.CheckedChanged += new System.EventHandler(this.chkKhong_3_CheckedChanged);
            // 
            // chkCo_3
            // 
            this.chkCo_3.AutoSize = true;
            this.chkCo_3.Checked = true;
            this.chkCo_3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCo_3.Location = new System.Drawing.Point(283, 213);
            this.chkCo_3.Name = "chkCo_3";
            this.chkCo_3.Size = new System.Drawing.Size(39, 17);
            this.chkCo_3.TabIndex = 39;
            this.chkCo_3.Text = "Có";
            this.chkCo_3.UseVisualStyleBackColor = true;
            this.chkCo_3.CheckedChanged += new System.EventHandler(this.chkCo_3_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "3. Đủ sức khỏe làm việc:";
            // 
            // txtLyDo_1
            // 
            this.txtLyDo_1.Location = new System.Drawing.Point(438, 176);
            this.txtLyDo_1.Name = "txtLyDo_1";
            this.txtLyDo_1.Size = new System.Drawing.Size(116, 20);
            this.txtLyDo_1.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Lý do:";
            // 
            // chkKhong_2
            // 
            this.chkKhong_2.AutoSize = true;
            this.chkKhong_2.Location = new System.Drawing.Point(335, 179);
            this.chkKhong_2.Name = "chkKhong_2";
            this.chkKhong_2.Size = new System.Drawing.Size(57, 17);
            this.chkKhong_2.TabIndex = 32;
            this.chkKhong_2.Text = "Không";
            this.chkKhong_2.UseVisualStyleBackColor = true;
            this.chkKhong_2.CheckedChanged += new System.EventHandler(this.chkKhong_2_CheckedChanged);
            // 
            // chkCo_2
            // 
            this.chkCo_2.AutoSize = true;
            this.chkCo_2.Checked = true;
            this.chkCo_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCo_2.Location = new System.Drawing.Point(283, 179);
            this.chkCo_2.Name = "chkCo_2";
            this.chkCo_2.Size = new System.Drawing.Size(39, 17);
            this.chkCo_2.TabIndex = 31;
            this.chkCo_2.Text = "Có";
            this.chkCo_2.UseVisualStyleBackColor = true;
            this.chkCo_2.CheckedChanged += new System.EventHandler(this.chkCo_2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "2. Đã làm đủ cận lâm sàng trong gói khám:";
            // 
            // txtCacXetNghiemLamThem
            // 
            this.txtCacXetNghiemLamThem.Location = new System.Drawing.Point(35, 104);
            this.txtCacXetNghiemLamThem.Multiline = true;
            this.txtCacXetNghiemLamThem.Name = "txtCacXetNghiemLamThem";
            this.txtCacXetNghiemLamThem.Size = new System.Drawing.Size(519, 61);
            this.txtCacXetNghiemLamThem.TabIndex = 29;
            // 
            // chkKhong_1
            // 
            this.chkKhong_1.AutoSize = true;
            this.chkKhong_1.Checked = true;
            this.chkKhong_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKhong_1.Location = new System.Drawing.Point(340, 82);
            this.chkKhong_1.Name = "chkKhong_1";
            this.chkKhong_1.Size = new System.Drawing.Size(57, 17);
            this.chkKhong_1.TabIndex = 28;
            this.chkKhong_1.Text = "Không";
            this.chkKhong_1.UseVisualStyleBackColor = true;
            this.chkKhong_1.CheckedChanged += new System.EventHandler(this.chkKhong_1_CheckedChanged);
            // 
            // chkCo_1
            // 
            this.chkCo_1.AutoSize = true;
            this.chkCo_1.Location = new System.Drawing.Point(283, 82);
            this.chkCo_1.Name = "chkCo_1";
            this.chkCo_1.Size = new System.Drawing.Size(39, 17);
            this.chkCo_1.TabIndex = 27;
            this.chkCo_1.Text = "Có";
            this.chkCo_1.UseVisualStyleBackColor = true;
            this.chkCo_1.CheckedChanged += new System.EventHandler(this.chkCo_1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "1. Các xét nghiệm làm thêm:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DisplayMember = "FullName";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(96, 46);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(296, 21);
            this.cboDocStaff.TabIndex = 2;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Bác sĩ:";
            // 
            // dtpkNgayKetLuan
            // 
            this.dtpkNgayKetLuan.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayKetLuan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayKetLuan.Location = new System.Drawing.Point(96, 22);
            this.dtpkNgayKetLuan.Name = "dtpkNgayKetLuan";
            this.dtpkNgayKetLuan.Size = new System.Drawing.Size(106, 20);
            this.dtpkNgayKetLuan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày kết luận:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(294, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(215, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddKetLuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 287);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddKetLuan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them ket luan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddKetLuan_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddKetLuan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLyDo_2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkKhong_3;
        private System.Windows.Forms.CheckBox chkCo_3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLyDo_1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkKhong_2;
        private System.Windows.Forms.CheckBox chkCo_2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCacXetNghiemLamThem;
        private System.Windows.Forms.CheckBox chkKhong_1;
        private System.Windows.Forms.CheckBox chkCo_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpkNgayKetLuan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}
