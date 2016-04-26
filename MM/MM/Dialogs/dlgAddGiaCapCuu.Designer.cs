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
    partial class dlgAddGiaCapCuu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddGiaCapCuu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpkNgayApDung = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDonViTinh = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.numGiaBan = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cboCapCuu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAppDungQuiTacTinhThuocVien = new System.Windows.Forms.Button();
            this.btnQuiTacTinhVacxinDichTruyen = new System.Windows.Forms.Button();
            this.khoCapCuuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoCapCuuBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpkNgayApDung);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDonViTinh);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.numGiaBan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.cboCapCuu);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin giá cấp cứu";
            // 
            // dtpkNgayApDung
            // 
            this.dtpkNgayApDung.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayApDung.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayApDung.Location = new System.Drawing.Point(90, 70);
            this.dtpkNgayApDung.Name = "dtpkNgayApDung";
            this.dtpkNgayApDung.Size = new System.Drawing.Size(112, 20);
            this.dtpkNgayApDung.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Ngày áp dụng:";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.Location = new System.Drawing.Point(90, 95);
            this.txtDonViTinh.MaxLength = 50;
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.ReadOnly = true;
            this.txtDonViTinh.Size = new System.Drawing.Size(112, 20);
            this.txtDonViTinh.TabIndex = 79;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(23, 98);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(63, 13);
            this.label30.TabIndex = 75;
            this.label30.Text = "Đơn vị tính:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(206, 50);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(36, 13);
            this.label27.TabIndex = 74;
            this.label27.Text = "(VNĐ)";
            // 
            // numGiaBan
            // 
            this.numGiaBan.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numGiaBan.Location = new System.Drawing.Point(90, 46);
            this.numGiaBan.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numGiaBan.Name = "numGiaBan";
            this.numGiaBan.Size = new System.Drawing.Size(112, 20);
            this.numGiaBan.TabIndex = 73;
            this.numGiaBan.ThousandsSeparator = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Giá bán:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(471, 24);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 13);
            this.label28.TabIndex = 71;
            this.label28.Text = "[*]";
            // 
            // cboCapCuu
            // 
            this.cboCapCuu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCapCuu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCapCuu.DataSource = this.khoCapCuuBindingSource;
            this.cboCapCuu.DisplayMember = "TenCapCuu";
            this.cboCapCuu.FormattingEnabled = true;
            this.cboCapCuu.Location = new System.Drawing.Point(90, 21);
            this.cboCapCuu.Name = "cboCapCuu";
            this.cboCapCuu.Size = new System.Drawing.Size(377, 21);
            this.cboCapCuu.TabIndex = 70;
            this.cboCapCuu.ValueMember = "KhoCapCuuGUID";
            this.cboCapCuu.SelectedIndexChanged += new System.EventHandler(this.cboThuoc_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Tên cấp cứu:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(431, 140);
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
            this.btnOK.Location = new System.Drawing.Point(352, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnAppDungQuiTacTinhThuocVien
            // 
            this.btnAppDungQuiTacTinhThuocVien.Image = global::MM.Properties.Resources.pills_5_icon;
            this.btnAppDungQuiTacTinhThuocVien.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppDungQuiTacTinhThuocVien.Location = new System.Drawing.Point(7, 140);
            this.btnAppDungQuiTacTinhThuocVien.Name = "btnAppDungQuiTacTinhThuocVien";
            this.btnAppDungQuiTacTinhThuocVien.Size = new System.Drawing.Size(155, 25);
            this.btnAppDungQuiTacTinhThuocVien.TabIndex = 16;
            this.btnAppDungQuiTacTinhThuocVien.Text = "    Qui tắc tính thuốc viên";
            this.btnAppDungQuiTacTinhThuocVien.UseVisualStyleBackColor = true;
            this.btnAppDungQuiTacTinhThuocVien.Click += new System.EventHandler(this.btnAppDungQuiTacTinhThuocVien_Click);
            // 
            // btnQuiTacTinhVacxinDichTruyen
            // 
            this.btnQuiTacTinhVacxinDichTruyen.Image = global::MM.Properties.Resources.Medicine_icon;
            this.btnQuiTacTinhVacxinDichTruyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuiTacTinhVacxinDichTruyen.Location = new System.Drawing.Point(166, 140);
            this.btnQuiTacTinhVacxinDichTruyen.Name = "btnQuiTacTinhVacxinDichTruyen";
            this.btnQuiTacTinhVacxinDichTruyen.Size = new System.Drawing.Size(182, 25);
            this.btnQuiTacTinhVacxinDichTruyen.TabIndex = 17;
            this.btnQuiTacTinhVacxinDichTruyen.Text = "      Qui tắc tính vắcxin-dịch truyền";
            this.btnQuiTacTinhVacxinDichTruyen.UseVisualStyleBackColor = true;
            this.btnQuiTacTinhVacxinDichTruyen.Click += new System.EventHandler(this.btnQuiTacTinhVacxinDichTruyen_Click);
            // 
            // khoCapCuuBindingSource
            // 
            this.khoCapCuuBindingSource.DataSource = typeof(MM.Databasae.KhoCapCuu);
            // 
            // dlgAddGiaCapCuu
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(515, 171);
            this.Controls.Add(this.btnQuiTacTinhVacxinDichTruyen);
            this.Controls.Add(this.btnAppDungQuiTacTinhThuocVien);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddGiaCapCuu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them gia cap cuu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddGiaThuoc_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddGiaThuoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoCapCuuBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cboCapCuu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numGiaBan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.DateTimePicker dtpkNgayApDung;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAppDungQuiTacTinhThuocVien;
        private System.Windows.Forms.Button btnQuiTacTinhVacxinDichTruyen;
        private System.Windows.Forms.BindingSource khoCapCuuBindingSource;
    }
}
