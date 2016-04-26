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
    partial class uNormal_HutThuoc_KhongHutThuoc
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
            this.chkKhongHutThuoc = new System.Windows.Forms.CheckBox();
            this.chkHutThuoc = new System.Windows.Forms.CheckBox();
            this.uNormal_KhongHutThuoc = new MM.Controls.uNormal_Chung();
            this.uNormal_HutThuoc = new MM.Controls.uNormal_Chung();
            this.SuspendLayout();
            // 
            // chkKhongHutThuoc
            // 
            this.chkKhongHutThuoc.AutoSize = true;
            this.chkKhongHutThuoc.Location = new System.Drawing.Point(0, 33);
            this.chkKhongHutThuoc.Name = "chkKhongHutThuoc";
            this.chkKhongHutThuoc.Size = new System.Drawing.Size(105, 17);
            this.chkKhongHutThuoc.TabIndex = 25;
            this.chkKhongHutThuoc.Text = "Không hút thuốc";
            this.chkKhongHutThuoc.UseVisualStyleBackColor = true;
            this.chkKhongHutThuoc.CheckedChanged += new System.EventHandler(this.chkKhongHutThuoc_CheckedChanged);
            // 
            // chkHutThuoc
            // 
            this.chkHutThuoc.AutoSize = true;
            this.chkHutThuoc.Location = new System.Drawing.Point(0, 4);
            this.chkHutThuoc.Name = "chkHutThuoc";
            this.chkHutThuoc.Size = new System.Drawing.Size(73, 17);
            this.chkHutThuoc.TabIndex = 19;
            this.chkHutThuoc.Text = "Hút thuốc";
            this.chkHutThuoc.UseVisualStyleBackColor = true;
            this.chkHutThuoc.CheckedChanged += new System.EventHandler(this.chkHutThuoc_CheckedChanged);
            // 
            // uNormal_KhongHutThuoc
            // 
            this.uNormal_KhongHutThuoc.DonVi = "";
            this.uNormal_KhongHutThuoc.Enabled = false;
            this.uNormal_KhongHutThuoc.FromOperator = "<=";
            this.uNormal_KhongHutThuoc.FromValue = 0F;
            this.uNormal_KhongHutThuoc.FromValueChecked = false;
            this.uNormal_KhongHutThuoc.Location = new System.Drawing.Point(118, 29);
            this.uNormal_KhongHutThuoc.Name = "uNormal_KhongHutThuoc";
            this.uNormal_KhongHutThuoc.Size = new System.Drawing.Size(452, 22);
            this.uNormal_KhongHutThuoc.TabIndex = 27;
            this.uNormal_KhongHutThuoc.ToOperator = "<=";
            this.uNormal_KhongHutThuoc.ToValue = 0F;
            this.uNormal_KhongHutThuoc.ToValueChecked = false;
            // 
            // uNormal_HutThuoc
            // 
            this.uNormal_HutThuoc.DonVi = "";
            this.uNormal_HutThuoc.Enabled = false;
            this.uNormal_HutThuoc.FromOperator = "<=";
            this.uNormal_HutThuoc.FromValue = 0F;
            this.uNormal_HutThuoc.FromValueChecked = false;
            this.uNormal_HutThuoc.Location = new System.Drawing.Point(118, 0);
            this.uNormal_HutThuoc.Name = "uNormal_HutThuoc";
            this.uNormal_HutThuoc.Size = new System.Drawing.Size(452, 22);
            this.uNormal_HutThuoc.TabIndex = 26;
            this.uNormal_HutThuoc.ToOperator = "<=";
            this.uNormal_HutThuoc.ToValue = 0F;
            this.uNormal_HutThuoc.ToValueChecked = false;
            // 
            // uNormal_HutThuoc_KhongHutThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uNormal_KhongHutThuoc);
            this.Controls.Add(this.uNormal_HutThuoc);
            this.Controls.Add(this.chkKhongHutThuoc);
            this.Controls.Add(this.chkHutThuoc);
            this.Name = "uNormal_HutThuoc_KhongHutThuoc";
            this.Size = new System.Drawing.Size(569, 54);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkKhongHutThuoc;
        private System.Windows.Forms.CheckBox chkHutThuoc;
        private uNormal_Chung uNormal_KhongHutThuoc;
        private uNormal_Chung uNormal_HutThuoc;
    }
}
