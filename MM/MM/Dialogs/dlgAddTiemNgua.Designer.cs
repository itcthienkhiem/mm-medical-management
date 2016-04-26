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
    partial class dlgAddTiemNgua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddTiemNgua));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpkLan3 = new System.Windows.Forms.DateTimePicker();
            this.dtpkLan2 = new System.Windows.Forms.DateTimePicker();
            this.dtpkLan1 = new System.Windows.Forms.DateTimePicker();
            this.chkLan3 = new System.Windows.Forms.CheckBox();
            this.chkLan2 = new System.Windows.Forms.CheckBox();
            this.chkLan1 = new System.Windows.Forms.CheckBox();
            this.btnChonBenhNhan = new System.Windows.Forms.Button();
            this.txtBenhNhan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkDaChich1 = new System.Windows.Forms.CheckBox();
            this.chkDaChich2 = new System.Windows.Forms.CheckBox();
            this.chkDaChich3 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDaChich3);
            this.groupBox1.Controls.Add(this.chkDaChich2);
            this.groupBox1.Controls.Add(this.chkDaChich1);
            this.groupBox1.Controls.Add(this.dtpkLan3);
            this.groupBox1.Controls.Add(this.dtpkLan2);
            this.groupBox1.Controls.Add(this.dtpkLan1);
            this.groupBox1.Controls.Add(this.chkLan3);
            this.groupBox1.Controls.Add(this.chkLan2);
            this.groupBox1.Controls.Add(this.chkLan1);
            this.groupBox1.Controls.Add(this.btnChonBenhNhan);
            this.groupBox1.Controls.Add(this.txtBenhNhan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpkLan3
            // 
            this.dtpkLan3.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpkLan3.Enabled = false;
            this.dtpkLan3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkLan3.Location = new System.Drawing.Point(135, 95);
            this.dtpkLan3.Name = "dtpkLan3";
            this.dtpkLan3.Size = new System.Drawing.Size(146, 20);
            this.dtpkLan3.TabIndex = 16;
            // 
            // dtpkLan2
            // 
            this.dtpkLan2.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpkLan2.Enabled = false;
            this.dtpkLan2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkLan2.Location = new System.Drawing.Point(135, 71);
            this.dtpkLan2.Name = "dtpkLan2";
            this.dtpkLan2.Size = new System.Drawing.Size(146, 20);
            this.dtpkLan2.TabIndex = 15;
            // 
            // dtpkLan1
            // 
            this.dtpkLan1.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpkLan1.Enabled = false;
            this.dtpkLan1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkLan1.Location = new System.Drawing.Point(135, 47);
            this.dtpkLan1.Name = "dtpkLan1";
            this.dtpkLan1.Size = new System.Drawing.Size(146, 20);
            this.dtpkLan1.TabIndex = 14;
            // 
            // chkLan3
            // 
            this.chkLan3.AutoSize = true;
            this.chkLan3.Enabled = false;
            this.chkLan3.Location = new System.Drawing.Point(78, 98);
            this.chkLan3.Name = "chkLan3";
            this.chkLan3.Size = new System.Drawing.Size(56, 17);
            this.chkLan3.TabIndex = 13;
            this.chkLan3.Text = "Lần 3:";
            this.chkLan3.UseVisualStyleBackColor = true;
            this.chkLan3.CheckedChanged += new System.EventHandler(this.chkLan3_CheckedChanged);
            // 
            // chkLan2
            // 
            this.chkLan2.AutoSize = true;
            this.chkLan2.Enabled = false;
            this.chkLan2.Location = new System.Drawing.Point(78, 74);
            this.chkLan2.Name = "chkLan2";
            this.chkLan2.Size = new System.Drawing.Size(56, 17);
            this.chkLan2.TabIndex = 12;
            this.chkLan2.Text = "Lần 2:";
            this.chkLan2.UseVisualStyleBackColor = true;
            this.chkLan2.CheckedChanged += new System.EventHandler(this.chkLan2_CheckedChanged);
            // 
            // chkLan1
            // 
            this.chkLan1.AutoSize = true;
            this.chkLan1.Location = new System.Drawing.Point(78, 50);
            this.chkLan1.Name = "chkLan1";
            this.chkLan1.Size = new System.Drawing.Size(56, 17);
            this.chkLan1.TabIndex = 11;
            this.chkLan1.Text = "Lần 1:";
            this.chkLan1.UseVisualStyleBackColor = true;
            this.chkLan1.CheckedChanged += new System.EventHandler(this.chkLan1_CheckedChanged);
            // 
            // btnChonBenhNhan
            // 
            this.btnChonBenhNhan.Location = new System.Drawing.Point(350, 16);
            this.btnChonBenhNhan.Name = "btnChonBenhNhan";
            this.btnChonBenhNhan.Size = new System.Drawing.Size(105, 22);
            this.btnChonBenhNhan.TabIndex = 10;
            this.btnChonBenhNhan.Text = "Chọn bệnh nhân...";
            this.btnChonBenhNhan.UseVisualStyleBackColor = true;
            this.btnChonBenhNhan.Click += new System.EventHandler(this.btnChonBenhNhan_Click);
            // 
            // txtBenhNhan
            // 
            this.txtBenhNhan.Location = new System.Drawing.Point(78, 17);
            this.txtBenhNhan.Name = "txtBenhNhan";
            this.txtBenhNhan.ReadOnly = true;
            this.txtBenhNhan.Size = new System.Drawing.Size(267, 20);
            this.txtBenhNhan.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bệnh nhân:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(243, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(164, 139);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // chkDaChich1
            // 
            this.chkDaChich1.AutoSize = true;
            this.chkDaChich1.Enabled = false;
            this.chkDaChich1.Location = new System.Drawing.Point(287, 50);
            this.chkDaChich1.Name = "chkDaChich1";
            this.chkDaChich1.Size = new System.Drawing.Size(71, 17);
            this.chkDaChich1.TabIndex = 17;
            this.chkDaChich1.Text = "Đã chích";
            this.chkDaChich1.UseVisualStyleBackColor = true;
            // 
            // chkDaChich2
            // 
            this.chkDaChich2.AutoSize = true;
            this.chkDaChich2.Enabled = false;
            this.chkDaChich2.Location = new System.Drawing.Point(287, 74);
            this.chkDaChich2.Name = "chkDaChich2";
            this.chkDaChich2.Size = new System.Drawing.Size(71, 17);
            this.chkDaChich2.TabIndex = 18;
            this.chkDaChich2.Text = "Đã chích";
            this.chkDaChich2.UseVisualStyleBackColor = true;
            // 
            // chkDaChich3
            // 
            this.chkDaChich3.AutoSize = true;
            this.chkDaChich3.Enabled = false;
            this.chkDaChich3.Location = new System.Drawing.Point(287, 98);
            this.chkDaChich3.Name = "chkDaChich3";
            this.chkDaChich3.Size = new System.Drawing.Size(71, 17);
            this.chkDaChich3.TabIndex = 19;
            this.chkDaChich3.Text = "Đã chích";
            this.chkDaChich3.UseVisualStyleBackColor = true;
            // 
            // dlgAddTiemNgua
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(483, 169);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddTiemNgua";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them tiem ngua";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddTiemNgua_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddTiemNgua_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkLan3;
        private System.Windows.Forms.DateTimePicker dtpkLan2;
        private System.Windows.Forms.DateTimePicker dtpkLan1;
        private System.Windows.Forms.CheckBox chkLan3;
        private System.Windows.Forms.CheckBox chkLan2;
        private System.Windows.Forms.CheckBox chkLan1;
        private System.Windows.Forms.Button btnChonBenhNhan;
        private System.Windows.Forms.TextBox txtBenhNhan;
        private System.Windows.Forms.CheckBox chkDaChich3;
        private System.Windows.Forms.CheckBox chkDaChich2;
        private System.Windows.Forms.CheckBox chkDaChich1;
    }
}
