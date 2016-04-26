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
namespace MM.Controls
{
    partial class uNormal_Sang_Chieu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkSang = new System.Windows.Forms.CheckBox();
            this.chkChieu = new System.Windows.Forms.CheckBox();
            this.uNormal_Sang = new MM.Controls.uNormal_Chung();
            this.uNormal_Chieu = new MM.Controls.uNormal_Chung();
            this._uTimeRange_Sang = new MM.Controls.uTimeRange();
            this._uTimeRange_Chieu = new MM.Controls.uTimeRange();
            this.raChung_Sang = new System.Windows.Forms.RadioButton();
            this.raNamNu_Sang = new System.Windows.Forms.RadioButton();
            this.uNormal_Nam_Nu_Sang = new MM.Controls.uNormal_Nam_Nu();
            this.uNormal_Nam_Nu_Chieu = new MM.Controls.uNormal_Nam_Nu();
            this.raNamNu_Chieu = new System.Windows.Forms.RadioButton();
            this.raChung_Chieu = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSang
            // 
            this.chkSang.AutoSize = true;
            this.chkSang.Location = new System.Drawing.Point(0, 3);
            this.chkSang.Name = "chkSang";
            this.chkSang.Size = new System.Drawing.Size(51, 17);
            this.chkSang.TabIndex = 12;
            this.chkSang.Text = "Sáng";
            this.chkSang.UseVisualStyleBackColor = true;
            this.chkSang.CheckedChanged += new System.EventHandler(this.chkSang_CheckedChanged);
            // 
            // chkChieu
            // 
            this.chkChieu.AutoSize = true;
            this.chkChieu.Location = new System.Drawing.Point(0, 192);
            this.chkChieu.Name = "chkChieu";
            this.chkChieu.Size = new System.Drawing.Size(53, 17);
            this.chkChieu.TabIndex = 25;
            this.chkChieu.Text = "Chiều";
            this.chkChieu.UseVisualStyleBackColor = true;
            this.chkChieu.CheckedChanged += new System.EventHandler(this.chkChieu_CheckedChanged);
            // 
            // uNormal_Sang
            // 
            this.uNormal_Sang.DonVi = "";
            this.uNormal_Sang.Enabled = false;
            this.uNormal_Sang.FromOperator = "<=";
            this.uNormal_Sang.FromValue = 0D;
            this.uNormal_Sang.FromValueChecked = false;
            this.uNormal_Sang.Location = new System.Drawing.Point(63, 1);
            this.uNormal_Sang.Name = "uNormal_Sang";
            this.uNormal_Sang.Size = new System.Drawing.Size(452, 24);
            this.uNormal_Sang.TabIndex = 24;
            this.uNormal_Sang.ToOperator = "<=";
            this.uNormal_Sang.ToValue = 0D;
            this.uNormal_Sang.ToValueChecked = false;
            // 
            // uNormal_Chieu
            // 
            this.uNormal_Chieu.DonVi = "";
            this.uNormal_Chieu.Enabled = false;
            this.uNormal_Chieu.FromOperator = "<=";
            this.uNormal_Chieu.FromValue = 0D;
            this.uNormal_Chieu.FromValueChecked = false;
            this.uNormal_Chieu.Location = new System.Drawing.Point(79, 214);
            this.uNormal_Chieu.Name = "uNormal_Chieu";
            this.uNormal_Chieu.Size = new System.Drawing.Size(452, 24);
            this.uNormal_Chieu.TabIndex = 28;
            this.uNormal_Chieu.ToOperator = "<=";
            this.uNormal_Chieu.ToValue = 0D;
            this.uNormal_Chieu.ToValueChecked = false;
            // 
            // _uTimeRange_Sang
            // 
            this._uTimeRange_Sang.Enabled = false;
            this._uTimeRange_Sang.FromOperator = "<=";
            this._uTimeRange_Sang.FromValue = 0;
            this._uTimeRange_Sang.FromValueChecked = false;
            this._uTimeRange_Sang.Location = new System.Drawing.Point(52, 0);
            this._uTimeRange_Sang.Name = "_uTimeRange_Sang";
            this._uTimeRange_Sang.Size = new System.Drawing.Size(294, 24);
            this._uTimeRange_Sang.TabIndex = 13;
            this._uTimeRange_Sang.ToOperator = "<=";
            this._uTimeRange_Sang.ToValue = 23;
            this._uTimeRange_Sang.ToValueChecked = false;
            // 
            // _uTimeRange_Chieu
            // 
            this._uTimeRange_Chieu.Enabled = false;
            this._uTimeRange_Chieu.FromOperator = "<=";
            this._uTimeRange_Chieu.FromValue = 0;
            this._uTimeRange_Chieu.FromValueChecked = false;
            this._uTimeRange_Chieu.Location = new System.Drawing.Point(64, 189);
            this._uTimeRange_Chieu.Name = "_uTimeRange_Chieu";
            this._uTimeRange_Chieu.Size = new System.Drawing.Size(294, 24);
            this._uTimeRange_Chieu.TabIndex = 26;
            this._uTimeRange_Chieu.ToOperator = "<=";
            this._uTimeRange_Chieu.ToValue = 23;
            this._uTimeRange_Chieu.ToValueChecked = false;
            // 
            // raChung_Sang
            // 
            this.raChung_Sang.AutoSize = true;
            this.raChung_Sang.Checked = true;
            this.raChung_Sang.Enabled = false;
            this.raChung_Sang.Location = new System.Drawing.Point(2, 4);
            this.raChung_Sang.Name = "raChung_Sang";
            this.raChung_Sang.Size = new System.Drawing.Size(59, 17);
            this.raChung_Sang.TabIndex = 29;
            this.raChung_Sang.TabStop = true;
            this.raChung_Sang.Text = "Chung:";
            this.raChung_Sang.UseVisualStyleBackColor = true;
            this.raChung_Sang.CheckedChanged += new System.EventHandler(this.raChung_Sang_CheckedChanged);
            // 
            // raNamNu_Sang
            // 
            this.raNamNu_Sang.AutoSize = true;
            this.raNamNu_Sang.Enabled = false;
            this.raNamNu_Sang.Location = new System.Drawing.Point(2, 30);
            this.raNamNu_Sang.Name = "raNamNu_Sang";
            this.raNamNu_Sang.Size = new System.Drawing.Size(67, 17);
            this.raNamNu_Sang.TabIndex = 30;
            this.raNamNu_Sang.Text = "Nam-Nữ:";
            this.raNamNu_Sang.UseVisualStyleBackColor = true;
            this.raNamNu_Sang.CheckedChanged += new System.EventHandler(this.raNamNu_Sang_CheckedChanged);
            // 
            // uNormal_Nam_Nu_Sang
            // 
            this.uNormal_Nam_Nu_Sang.Enabled = false;
            this.uNormal_Nam_Nu_Sang.FromAge_Nam = 20;
            this.uNormal_Nam_Nu_Sang.FromAge_NamChecked = false;
            this.uNormal_Nam_Nu_Sang.FromAge_Nu = 17;
            this.uNormal_Nam_Nu_Sang.FromAge_NuChecked = false;
            this.uNormal_Nam_Nu_Sang.Location = new System.Drawing.Point(20, 53);
            this.uNormal_Nam_Nu_Sang.NamChecked = false;
            this.uNormal_Nam_Nu_Sang.Name = "uNormal_Nam_Nu_Sang";
            this.uNormal_Nam_Nu_Sang.NuChecked = false;
            this.uNormal_Nam_Nu_Sang.Size = new System.Drawing.Size(506, 106);
            this.uNormal_Nam_Nu_Sang.TabIndex = 31;
            this.uNormal_Nam_Nu_Sang.ToAge_Nam = 60;
            this.uNormal_Nam_Nu_Sang.ToAge_NamChecked = false;
            this.uNormal_Nam_Nu_Sang.ToAge_Nu = 60;
            this.uNormal_Nam_Nu_Sang.ToAge_NuChecked = false;
            // 
            // uNormal_Nam_Nu_Chieu
            // 
            this.uNormal_Nam_Nu_Chieu.Enabled = false;
            this.uNormal_Nam_Nu_Chieu.FromAge_Nam = 20;
            this.uNormal_Nam_Nu_Chieu.FromAge_NamChecked = false;
            this.uNormal_Nam_Nu_Chieu.FromAge_Nu = 17;
            this.uNormal_Nam_Nu_Chieu.FromAge_NuChecked = false;
            this.uNormal_Nam_Nu_Chieu.Location = new System.Drawing.Point(36, 265);
            this.uNormal_Nam_Nu_Chieu.NamChecked = false;
            this.uNormal_Nam_Nu_Chieu.Name = "uNormal_Nam_Nu_Chieu";
            this.uNormal_Nam_Nu_Chieu.NuChecked = false;
            this.uNormal_Nam_Nu_Chieu.Size = new System.Drawing.Size(506, 106);
            this.uNormal_Nam_Nu_Chieu.TabIndex = 34;
            this.uNormal_Nam_Nu_Chieu.ToAge_Nam = 60;
            this.uNormal_Nam_Nu_Chieu.ToAge_NamChecked = false;
            this.uNormal_Nam_Nu_Chieu.ToAge_Nu = 60;
            this.uNormal_Nam_Nu_Chieu.ToAge_NuChecked = false;
            // 
            // raNamNu_Chieu
            // 
            this.raNamNu_Chieu.AutoSize = true;
            this.raNamNu_Chieu.Enabled = false;
            this.raNamNu_Chieu.Location = new System.Drawing.Point(18, 242);
            this.raNamNu_Chieu.Name = "raNamNu_Chieu";
            this.raNamNu_Chieu.Size = new System.Drawing.Size(67, 17);
            this.raNamNu_Chieu.TabIndex = 33;
            this.raNamNu_Chieu.Text = "Nam-Nữ:";
            this.raNamNu_Chieu.UseVisualStyleBackColor = true;
            this.raNamNu_Chieu.CheckedChanged += new System.EventHandler(this.raNamNu_Chieu_CheckedChanged);
            // 
            // raChung_Chieu
            // 
            this.raChung_Chieu.AutoSize = true;
            this.raChung_Chieu.Checked = true;
            this.raChung_Chieu.Enabled = false;
            this.raChung_Chieu.Location = new System.Drawing.Point(18, 216);
            this.raChung_Chieu.Name = "raChung_Chieu";
            this.raChung_Chieu.Size = new System.Drawing.Size(59, 17);
            this.raChung_Chieu.TabIndex = 32;
            this.raChung_Chieu.TabStop = true;
            this.raChung_Chieu.Text = "Chung:";
            this.raChung_Chieu.UseVisualStyleBackColor = true;
            this.raChung_Chieu.CheckedChanged += new System.EventHandler(this.raChung_Chieu_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uNormal_Nam_Nu_Sang);
            this.panel1.Controls.Add(this.uNormal_Sang);
            this.panel1.Controls.Add(this.raChung_Sang);
            this.panel1.Controls.Add(this.raNamNu_Sang);
            this.panel1.Location = new System.Drawing.Point(16, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 160);
            this.panel1.TabIndex = 35;
            // 
            // uNormal_Sang_Chieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uNormal_Nam_Nu_Chieu);
            this.Controls.Add(this.raNamNu_Chieu);
            this.Controls.Add(this.raChung_Chieu);
            this.Controls.Add(this._uTimeRange_Chieu);
            this.Controls.Add(this._uTimeRange_Sang);
            this.Controls.Add(this.uNormal_Chieu);
            this.Controls.Add(this.chkChieu);
            this.Controls.Add(this.chkSang);
            this.Name = "uNormal_Sang_Chieu";
            this.Size = new System.Drawing.Size(544, 370);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSang;
        private System.Windows.Forms.CheckBox chkChieu;
        private uNormal_Chung uNormal_Sang;
        private uNormal_Chung uNormal_Chieu;
        private uTimeRange _uTimeRange_Sang;
        private uTimeRange _uTimeRange_Chieu;
        private System.Windows.Forms.RadioButton raChung_Sang;
        private System.Windows.Forms.RadioButton raNamNu_Sang;
        private uNormal_Nam_Nu uNormal_Nam_Nu_Sang;
        private uNormal_Nam_Nu uNormal_Nam_Nu_Chieu;
        private System.Windows.Forms.RadioButton raNamNu_Chieu;
        private System.Windows.Forms.RadioButton raChung_Chieu;
        private System.Windows.Forms.Panel panel1;
    }
}
