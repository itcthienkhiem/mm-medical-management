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
    partial class uNormal_TreEm_NguoiLon_NguoiCaoTuoi
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
            this.chkTreEm = new System.Windows.Forms.CheckBox();
            this.chkNguoiLon = new System.Windows.Forms.CheckBox();
            this.chkNguoiCaoTuoi = new System.Windows.Forms.CheckBox();
            this.uNormal_TreEm = new MM.Controls.uNormal_Chung();
            this.uNormal_NguoiLon = new MM.Controls.uNormal_Chung();
            this.uNormal_NguoiCaoTuoi = new MM.Controls.uNormal_Chung();
            this.SuspendLayout();
            // 
            // chkTreEm
            // 
            this.chkTreEm.AutoSize = true;
            this.chkTreEm.Location = new System.Drawing.Point(0, 3);
            this.chkTreEm.Name = "chkTreEm";
            this.chkTreEm.Size = new System.Drawing.Size(59, 17);
            this.chkTreEm.TabIndex = 8;
            this.chkTreEm.Text = "Trẻ em";
            this.chkTreEm.UseVisualStyleBackColor = true;
            this.chkTreEm.CheckedChanged += new System.EventHandler(this.chkTreEm_CheckedChanged);
            // 
            // chkNguoiLon
            // 
            this.chkNguoiLon.AutoSize = true;
            this.chkNguoiLon.Location = new System.Drawing.Point(0, 34);
            this.chkNguoiLon.Name = "chkNguoiLon";
            this.chkNguoiLon.Size = new System.Drawing.Size(71, 17);
            this.chkNguoiLon.TabIndex = 10;
            this.chkNguoiLon.Text = "Người lớn";
            this.chkNguoiLon.UseVisualStyleBackColor = true;
            this.chkNguoiLon.CheckedChanged += new System.EventHandler(this.chkNguoiLon_CheckedChanged);
            // 
            // chkNguoiCaoTuoi
            // 
            this.chkNguoiCaoTuoi.AutoSize = true;
            this.chkNguoiCaoTuoi.Location = new System.Drawing.Point(0, 64);
            this.chkNguoiCaoTuoi.Name = "chkNguoiCaoTuoi";
            this.chkNguoiCaoTuoi.Size = new System.Drawing.Size(95, 17);
            this.chkNguoiCaoTuoi.TabIndex = 12;
            this.chkNguoiCaoTuoi.Text = "Người cao tuổi";
            this.chkNguoiCaoTuoi.UseVisualStyleBackColor = true;
            this.chkNguoiCaoTuoi.CheckedChanged += new System.EventHandler(this.chkNguoiCaoTuoi_CheckedChanged);
            // 
            // uNormal_TreEm
            // 
            this.uNormal_TreEm.DonVi = "";
            this.uNormal_TreEm.Enabled = false;
            this.uNormal_TreEm.FromOperator = "<=";
            this.uNormal_TreEm.FromValue = 0F;
            this.uNormal_TreEm.FromValueChecked = false;
            this.uNormal_TreEm.Location = new System.Drawing.Point(101, 0);
            this.uNormal_TreEm.Name = "uNormal_TreEm";
            this.uNormal_TreEm.Size = new System.Drawing.Size(452, 22);
            this.uNormal_TreEm.TabIndex = 9;
            this.uNormal_TreEm.ToOperator = "<=";
            this.uNormal_TreEm.ToValue = 0F;
            this.uNormal_TreEm.ToValueChecked = false;
            // 
            // uNormal_NguoiLon
            // 
            this.uNormal_NguoiLon.DonVi = "";
            this.uNormal_NguoiLon.Enabled = false;
            this.uNormal_NguoiLon.FromOperator = "<=";
            this.uNormal_NguoiLon.FromValue = 0F;
            this.uNormal_NguoiLon.FromValueChecked = false;
            this.uNormal_NguoiLon.Location = new System.Drawing.Point(101, 30);
            this.uNormal_NguoiLon.Name = "uNormal_NguoiLon";
            this.uNormal_NguoiLon.Size = new System.Drawing.Size(452, 22);
            this.uNormal_NguoiLon.TabIndex = 11;
            this.uNormal_NguoiLon.ToOperator = "<=";
            this.uNormal_NguoiLon.ToValue = 0F;
            this.uNormal_NguoiLon.ToValueChecked = false;
            // 
            // uNormal_NguoiCaoTuoi
            // 
            this.uNormal_NguoiCaoTuoi.DonVi = "";
            this.uNormal_NguoiCaoTuoi.Enabled = false;
            this.uNormal_NguoiCaoTuoi.FromOperator = "<=";
            this.uNormal_NguoiCaoTuoi.FromValue = 0F;
            this.uNormal_NguoiCaoTuoi.FromValueChecked = false;
            this.uNormal_NguoiCaoTuoi.Location = new System.Drawing.Point(101, 60);
            this.uNormal_NguoiCaoTuoi.Name = "uNormal_NguoiCaoTuoi";
            this.uNormal_NguoiCaoTuoi.Size = new System.Drawing.Size(452, 22);
            this.uNormal_NguoiCaoTuoi.TabIndex = 13;
            this.uNormal_NguoiCaoTuoi.ToOperator = "<=";
            this.uNormal_NguoiCaoTuoi.ToValue = 0F;
            this.uNormal_NguoiCaoTuoi.ToValueChecked = false;
            // 
            // uNormal_TreEm_NguoiLon_NguoiCaoTuoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uNormal_NguoiCaoTuoi);
            this.Controls.Add(this.uNormal_NguoiLon);
            this.Controls.Add(this.uNormal_TreEm);
            this.Controls.Add(this.chkNguoiCaoTuoi);
            this.Controls.Add(this.chkNguoiLon);
            this.Controls.Add(this.chkTreEm);
            this.Name = "uNormal_TreEm_NguoiLon_NguoiCaoTuoi";
            this.Size = new System.Drawing.Size(552, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTreEm;
        private System.Windows.Forms.CheckBox chkNguoiLon;
        private System.Windows.Forms.CheckBox chkNguoiCaoTuoi;
        private uNormal_Chung uNormal_TreEm;
        private uNormal_Chung uNormal_NguoiLon;
        private uNormal_Chung uNormal_NguoiCaoTuoi;
    }
}
