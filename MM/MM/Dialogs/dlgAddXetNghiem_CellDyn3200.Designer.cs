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
    partial class dlgAddXetNghiem_CellDyn3200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddXetNghiem_CellDyn3200));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numToValue_NormalPercent = new System.Windows.Forms.NumericUpDown();
            this.chkToValue_NormalPercent = new System.Windows.Forms.CheckBox();
            this.numFromValue_NormalPercent = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue_NormalPercent = new System.Windows.Forms.CheckBox();
            this.numToValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkToValue_Normal = new System.Windows.Forms.CheckBox();
            this.numFromValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue_Normal = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenXetNghiem = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_NormalPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_NormalPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTenXetNghiem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numToValue_NormalPercent);
            this.groupBox1.Controls.Add(this.chkToValue_NormalPercent);
            this.groupBox1.Controls.Add(this.numFromValue_NormalPercent);
            this.groupBox1.Controls.Add(this.chkFromValue_NormalPercent);
            this.groupBox1.Controls.Add(this.numToValue_Normal);
            this.groupBox1.Controls.Add(this.chkToValue_Normal);
            this.groupBox1.Controls.Add(this.numFromValue_Normal);
            this.groupBox1.Controls.Add(this.chkFromValue_Normal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // numToValue_NormalPercent
            // 
            this.numToValue_NormalPercent.DecimalPlaces = 2;
            this.numToValue_NormalPercent.Enabled = false;
            this.numToValue_NormalPercent.Location = new System.Drawing.Point(287, 67);
            this.numToValue_NormalPercent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue_NormalPercent.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue_NormalPercent.Name = "numToValue_NormalPercent";
            this.numToValue_NormalPercent.Size = new System.Drawing.Size(74, 20);
            this.numToValue_NormalPercent.TabIndex = 11;
            // 
            // chkToValue_NormalPercent
            // 
            this.chkToValue_NormalPercent.AutoSize = true;
            this.chkToValue_NormalPercent.Location = new System.Drawing.Point(244, 69);
            this.chkToValue_NormalPercent.Name = "chkToValue_NormalPercent";
            this.chkToValue_NormalPercent.Size = new System.Drawing.Size(45, 17);
            this.chkToValue_NormalPercent.TabIndex = 10;
            this.chkToValue_NormalPercent.Text = "đến";
            this.chkToValue_NormalPercent.UseVisualStyleBackColor = true;
            this.chkToValue_NormalPercent.CheckedChanged += new System.EventHandler(this.chkToValue_NormalPercent_CheckedChanged);
            // 
            // numFromValue_NormalPercent
            // 
            this.numFromValue_NormalPercent.DecimalPlaces = 2;
            this.numFromValue_NormalPercent.Enabled = false;
            this.numFromValue_NormalPercent.Location = new System.Drawing.Point(164, 67);
            this.numFromValue_NormalPercent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue_NormalPercent.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue_NormalPercent.Name = "numFromValue_NormalPercent";
            this.numFromValue_NormalPercent.Size = new System.Drawing.Size(74, 20);
            this.numFromValue_NormalPercent.TabIndex = 9;
            // 
            // chkFromValue_NormalPercent
            // 
            this.chkFromValue_NormalPercent.AutoSize = true;
            this.chkFromValue_NormalPercent.Location = new System.Drawing.Point(97, 69);
            this.chkFromValue_NormalPercent.Name = "chkFromValue_NormalPercent";
            this.chkFromValue_NormalPercent.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue_NormalPercent.TabIndex = 8;
            this.chkFromValue_NormalPercent.Text = "Chỉ số từ:";
            this.chkFromValue_NormalPercent.UseVisualStyleBackColor = true;
            this.chkFromValue_NormalPercent.CheckedChanged += new System.EventHandler(this.chkFromValue_NormalPercent_CheckedChanged);
            // 
            // numToValue_Normal
            // 
            this.numToValue_Normal.DecimalPlaces = 2;
            this.numToValue_Normal.Enabled = false;
            this.numToValue_Normal.Location = new System.Drawing.Point(287, 42);
            this.numToValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue_Normal.Name = "numToValue_Normal";
            this.numToValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numToValue_Normal.TabIndex = 7;
            // 
            // chkToValue_Normal
            // 
            this.chkToValue_Normal.AutoSize = true;
            this.chkToValue_Normal.Location = new System.Drawing.Point(244, 44);
            this.chkToValue_Normal.Name = "chkToValue_Normal";
            this.chkToValue_Normal.Size = new System.Drawing.Size(45, 17);
            this.chkToValue_Normal.TabIndex = 6;
            this.chkToValue_Normal.Text = "đến";
            this.chkToValue_Normal.UseVisualStyleBackColor = true;
            this.chkToValue_Normal.CheckedChanged += new System.EventHandler(this.chkToValue_Normal_CheckedChanged);
            // 
            // numFromValue_Normal
            // 
            this.numFromValue_Normal.DecimalPlaces = 2;
            this.numFromValue_Normal.Enabled = false;
            this.numFromValue_Normal.Location = new System.Drawing.Point(164, 42);
            this.numFromValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue_Normal.Name = "numFromValue_Normal";
            this.numFromValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numFromValue_Normal.TabIndex = 5;
            // 
            // chkFromValue_Normal
            // 
            this.chkFromValue_Normal.AutoSize = true;
            this.chkFromValue_Normal.Location = new System.Drawing.Point(97, 44);
            this.chkFromValue_Normal.Name = "chkFromValue_Normal";
            this.chkFromValue_Normal.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue_Normal.TabIndex = 4;
            this.chkFromValue_Normal.Text = "Chỉ số từ:";
            this.chkFromValue_Normal.UseVisualStyleBackColor = true;
            this.chkFromValue_Normal.CheckedChanged += new System.EventHandler(this.chkFromValue_Normal_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "% Bình thường:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bình thường:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(195, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(116, 107);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tên xét nghiệm:";
            // 
            // txtTenXetNghiem
            // 
            this.txtTenXetNghiem.Location = new System.Drawing.Point(97, 17);
            this.txtTenXetNghiem.Name = "txtTenXetNghiem";
            this.txtTenXetNghiem.ReadOnly = true;
            this.txtTenXetNghiem.Size = new System.Drawing.Size(264, 20);
            this.txtTenXetNghiem.TabIndex = 13;
            // 
            // dlgAddXetNghiem_CellDyn3200
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(386, 137);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddXetNghiem_CellDyn3200";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua xet nghiem CellDyn3200";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddXetNghiem_CellDyn3200_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddXetNghiem_CellDyn3200_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_NormalPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_NormalPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numToValue_NormalPercent;
        private System.Windows.Forms.CheckBox chkToValue_NormalPercent;
        private System.Windows.Forms.NumericUpDown numFromValue_NormalPercent;
        private System.Windows.Forms.CheckBox chkFromValue_NormalPercent;
        private System.Windows.Forms.NumericUpDown numToValue_Normal;
        private System.Windows.Forms.CheckBox chkToValue_Normal;
        private System.Windows.Forms.NumericUpDown numFromValue_Normal;
        private System.Windows.Forms.CheckBox chkFromValue_Normal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtTenXetNghiem;
        private System.Windows.Forms.Label label3;
    }
}
