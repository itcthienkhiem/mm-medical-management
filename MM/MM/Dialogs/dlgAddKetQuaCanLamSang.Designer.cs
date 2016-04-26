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
    partial class dlgAddKetQuaCanLamSang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddKetQuaCanLamSang));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.raNormal = new System.Windows.Forms.RadioButton();
            this.raNegative = new System.Windows.Forms.RadioButton();
            this.gbNormal = new System.Windows.Forms.GroupBox();
            this.chkAbnormal = new System.Windows.Forms.CheckBox();
            this.chkNormal = new System.Windows.Forms.CheckBox();
            this.gbNegative = new System.Windows.Forms.GroupBox();
            this.chkPositive = new System.Windows.Forms.CheckBox();
            this.chkNegative = new System.Windows.Forms.CheckBox();
            this.btnChonDichVu = new System.Windows.Forms.Button();
            this.dtpkActiveDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboService = new System.Windows.Forms.ComboBox();
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbNormal.SuspendLayout();
            this.gbNegative.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnChonDichVu);
            this.groupBox1.Controls.Add(this.dtpkActiveDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.cboService);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 348);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.raNormal);
            this.panel1.Controls.Add(this.raNegative);
            this.panel1.Controls.Add(this.gbNormal);
            this.panel1.Controls.Add(this.gbNegative);
            this.panel1.Location = new System.Drawing.Point(84, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 108);
            this.panel1.TabIndex = 13;
            // 
            // raNormal
            // 
            this.raNormal.AutoSize = true;
            this.raNormal.Checked = true;
            this.raNormal.Location = new System.Drawing.Point(18, 5);
            this.raNormal.Name = "raNormal";
            this.raNormal.Size = new System.Drawing.Size(14, 13);
            this.raNormal.TabIndex = 13;
            this.raNormal.TabStop = true;
            this.raNormal.UseVisualStyleBackColor = true;
            this.raNormal.CheckedChanged += new System.EventHandler(this.raNormal_CheckedChanged);
            // 
            // raNegative
            // 
            this.raNegative.AutoSize = true;
            this.raNegative.Location = new System.Drawing.Point(18, 57);
            this.raNegative.Name = "raNegative";
            this.raNegative.Size = new System.Drawing.Size(14, 13);
            this.raNegative.TabIndex = 15;
            this.raNegative.UseVisualStyleBackColor = true;
            this.raNegative.CheckedChanged += new System.EventHandler(this.raNegative_CheckedChanged);
            // 
            // gbNormal
            // 
            this.gbNormal.Controls.Add(this.chkAbnormal);
            this.gbNormal.Controls.Add(this.chkNormal);
            this.gbNormal.Location = new System.Drawing.Point(23, 3);
            this.gbNormal.Name = "gbNormal";
            this.gbNormal.Size = new System.Drawing.Size(269, 47);
            this.gbNormal.TabIndex = 14;
            this.gbNormal.TabStop = false;
            // 
            // chkAbnormal
            // 
            this.chkAbnormal.AutoSize = true;
            this.chkAbnormal.Location = new System.Drawing.Point(128, 19);
            this.chkAbnormal.Name = "chkAbnormal";
            this.chkAbnormal.Size = new System.Drawing.Size(78, 17);
            this.chkAbnormal.TabIndex = 1;
            this.chkAbnormal.Text = "Bất thường";
            this.chkAbnormal.UseVisualStyleBackColor = true;
            this.chkAbnormal.CheckedChanged += new System.EventHandler(this.chkAbnormal_CheckedChanged);
            // 
            // chkNormal
            // 
            this.chkNormal.AutoSize = true;
            this.chkNormal.Checked = true;
            this.chkNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNormal.Location = new System.Drawing.Point(20, 19);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(83, 17);
            this.chkNormal.TabIndex = 0;
            this.chkNormal.Text = "Bình thường";
            this.chkNormal.UseVisualStyleBackColor = true;
            this.chkNormal.CheckedChanged += new System.EventHandler(this.chkNormal_CheckedChanged);
            // 
            // gbNegative
            // 
            this.gbNegative.Controls.Add(this.chkPositive);
            this.gbNegative.Controls.Add(this.chkNegative);
            this.gbNegative.Enabled = false;
            this.gbNegative.Location = new System.Drawing.Point(23, 57);
            this.gbNegative.Name = "gbNegative";
            this.gbNegative.Size = new System.Drawing.Size(269, 47);
            this.gbNegative.TabIndex = 16;
            this.gbNegative.TabStop = false;
            // 
            // chkPositive
            // 
            this.chkPositive.AutoSize = true;
            this.chkPositive.Location = new System.Drawing.Point(128, 19);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(80, 17);
            this.chkPositive.TabIndex = 3;
            this.chkPositive.Text = "Dương tính";
            this.chkPositive.UseVisualStyleBackColor = true;
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_CheckedChanged);
            // 
            // chkNegative
            // 
            this.chkNegative.AutoSize = true;
            this.chkNegative.Checked = true;
            this.chkNegative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNegative.Location = new System.Drawing.Point(20, 19);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(63, 17);
            this.chkNegative.TabIndex = 2;
            this.chkNegative.Text = "Âm tính";
            this.chkNegative.UseVisualStyleBackColor = true;
            this.chkNegative.CheckedChanged += new System.EventHandler(this.chkNegative_CheckedChanged);
            // 
            // btnChonDichVu
            // 
            this.btnChonDichVu.Location = new System.Drawing.Point(406, 19);
            this.btnChonDichVu.Name = "btnChonDichVu";
            this.btnChonDichVu.Size = new System.Drawing.Size(110, 23);
            this.btnChonDichVu.TabIndex = 1;
            this.btnChonDichVu.Text = "Chọn dịch vụ...";
            this.btnChonDichVu.UseVisualStyleBackColor = true;
            this.btnChonDichVu.Click += new System.EventHandler(this.btnChonDichVu_Click);
            // 
            // dtpkActiveDate
            // 
            this.dtpkActiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkActiveDate.Location = new System.Drawing.Point(107, 46);
            this.dtpkActiveDate.Name = "dtpkActiveDate";
            this.dtpkActiveDate.Size = new System.Drawing.Size(122, 20);
            this.dtpkActiveDate.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ngày khám:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "Fullname";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(107, 70);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(293, 21);
            this.cboDocStaff.TabIndex = 5;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // cboService
            // 
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.DataSource = this.serviceBindingSource;
            this.cboService.DisplayMember = "Name";
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(107, 21);
            this.cboService.Name = "cboService";
            this.cboService.Size = new System.Drawing.Size(293, 21);
            this.cboService.TabIndex = 0;
            this.cboService.ValueMember = "ServiceGUID";
            this.cboService.SelectedValueChanged += new System.EventHandler(this.cboService_SelectedValueChanged);
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(107, 209);
            this.txtDescription.MaxLength = 4000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(409, 96);
            this.txtDescription.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nhận xét:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bác sĩ thực hiện:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dịch vụ:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(274, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(195, 354);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(389, 311);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(127, 25);
            this.btnAdd.TabIndex = 85;
            this.btnAdd.Text = "    &Thêm lời khuyên";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dlgAddKetQuaCanLamSang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(545, 385);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddKetQuaCanLamSang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them ket qua can lam sang";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddServiceHistory_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddServiceHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbNormal.ResumeLayout(false);
            this.gbNormal.PerformLayout();
            this.gbNegative.ResumeLayout(false);
            this.gbNegative.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private System.Windows.Forms.DateTimePicker dtpkActiveDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton raNegative;
        private System.Windows.Forms.RadioButton raNormal;
        private System.Windows.Forms.GroupBox gbNegative;
        private System.Windows.Forms.CheckBox chkPositive;
        private System.Windows.Forms.CheckBox chkNegative;
        private System.Windows.Forms.GroupBox gbNormal;
        private System.Windows.Forms.CheckBox chkAbnormal;
        private System.Windows.Forms.CheckBox chkNormal;
        private System.Windows.Forms.Button btnChonDichVu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAdd;
    }
}
