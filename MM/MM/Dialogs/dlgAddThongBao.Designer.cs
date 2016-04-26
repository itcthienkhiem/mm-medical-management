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
    partial class dlgAddThongBao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddThongBao));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenThongBao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTapTinThongBao = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.chkDuyetLan1 = new System.Windows.Forms.CheckBox();
            this.chkDuyetLan2 = new System.Windows.Forms.CheckBox();
            this.chkDuyetLan3 = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNgayDuyet1 = new System.Windows.Forms.TextBox();
            this.txtNgayDuyet2 = new System.Windows.Forms.TextBox();
            this.txtNgayDuyet3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên thông báo:";
            // 
            // txtTenThongBao
            // 
            this.txtTenThongBao.Location = new System.Drawing.Point(109, 17);
            this.txtTenThongBao.Name = "txtTenThongBao";
            this.txtTenThongBao.Size = new System.Drawing.Size(348, 20);
            this.txtTenThongBao.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tập tin thông báo:";
            // 
            // txtTapTinThongBao
            // 
            this.txtTapTinThongBao.Location = new System.Drawing.Point(109, 41);
            this.txtTapTinThongBao.Name = "txtTapTinThongBao";
            this.txtTapTinThongBao.Size = new System.Drawing.Size(348, 20);
            this.txtTapTinThongBao.TabIndex = 3;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.Location = new System.Drawing.Point(461, 40);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(30, 22);
            this.btnBrowser.TabIndex = 4;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // chkDuyetLan1
            // 
            this.chkDuyetLan1.AutoSize = true;
            this.chkDuyetLan1.Location = new System.Drawing.Point(12, 67);
            this.chkDuyetLan1.Name = "chkDuyetLan1";
            this.chkDuyetLan1.Size = new System.Drawing.Size(80, 17);
            this.chkDuyetLan1.TabIndex = 5;
            this.chkDuyetLan1.Text = "Duyệt lần 1";
            this.chkDuyetLan1.UseVisualStyleBackColor = true;
            // 
            // chkDuyetLan2
            // 
            this.chkDuyetLan2.AutoSize = true;
            this.chkDuyetLan2.Enabled = false;
            this.chkDuyetLan2.Location = new System.Drawing.Point(12, 91);
            this.chkDuyetLan2.Name = "chkDuyetLan2";
            this.chkDuyetLan2.Size = new System.Drawing.Size(80, 17);
            this.chkDuyetLan2.TabIndex = 6;
            this.chkDuyetLan2.Text = "Duyệt lần 2";
            this.chkDuyetLan2.UseVisualStyleBackColor = true;
            // 
            // chkDuyetLan3
            // 
            this.chkDuyetLan3.AutoSize = true;
            this.chkDuyetLan3.Enabled = false;
            this.chkDuyetLan3.Location = new System.Drawing.Point(12, 115);
            this.chkDuyetLan3.Name = "chkDuyetLan3";
            this.chkDuyetLan3.Size = new System.Drawing.Size(80, 17);
            this.chkDuyetLan3.TabIndex = 7;
            this.chkDuyetLan3.Text = "Duyệt lần 3";
            this.chkDuyetLan3.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(261, 151);
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
            this.btnOK.Location = new System.Drawing.Point(182, 151);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNgayDuyet3);
            this.groupBox1.Controls.Add(this.txtNgayDuyet2);
            this.groupBox1.Controls.Add(this.txtNgayDuyet1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTenThongBao);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkDuyetLan3);
            this.groupBox1.Controls.Add(this.txtTapTinThongBao);
            this.groupBox1.Controls.Add(this.chkDuyetLan2);
            this.groupBox1.Controls.Add(this.btnBrowser);
            this.groupBox1.Controls.Add(this.chkDuyetLan1);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtNgayDuyet1
            // 
            this.txtNgayDuyet1.Enabled = false;
            this.txtNgayDuyet1.Location = new System.Drawing.Point(109, 65);
            this.txtNgayDuyet1.Name = "txtNgayDuyet1";
            this.txtNgayDuyet1.Size = new System.Drawing.Size(165, 20);
            this.txtNgayDuyet1.TabIndex = 8;
            // 
            // txtNgayDuyet2
            // 
            this.txtNgayDuyet2.Enabled = false;
            this.txtNgayDuyet2.Location = new System.Drawing.Point(109, 89);
            this.txtNgayDuyet2.Name = "txtNgayDuyet2";
            this.txtNgayDuyet2.Size = new System.Drawing.Size(165, 20);
            this.txtNgayDuyet2.TabIndex = 9;
            // 
            // txtNgayDuyet3
            // 
            this.txtNgayDuyet3.Enabled = false;
            this.txtNgayDuyet3.Location = new System.Drawing.Point(109, 113);
            this.txtNgayDuyet3.Name = "txtNgayDuyet3";
            this.txtNgayDuyet3.Size = new System.Drawing.Size(165, 20);
            this.txtNgayDuyet3.TabIndex = 16;
            // 
            // dlgAddThongBao
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(518, 181);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddThongBao";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them thong bao";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddThongBao_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddThongBao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenThongBao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTapTinThongBao;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.CheckBox chkDuyetLan1;
        private System.Windows.Forms.CheckBox chkDuyetLan2;
        private System.Windows.Forms.CheckBox chkDuyetLan3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNgayDuyet3;
        private System.Windows.Forms.TextBox txtNgayDuyet2;
        private System.Windows.Forms.TextBox txtNgayDuyet1;
    }
}
