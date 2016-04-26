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
namespace MMConfigServices
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnConfigDatabase = new System.Windows.Forms.Button();
            this.btnConfigFTP = new System.Windows.Forms.Button();
            this.btnCauHinhMayXetNghiem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConfigDatabase
            // 
            this.btnConfigDatabase.Location = new System.Drawing.Point(10, 12);
            this.btnConfigDatabase.Name = "btnConfigDatabase";
            this.btnConfigDatabase.Size = new System.Drawing.Size(117, 39);
            this.btnConfigDatabase.TabIndex = 0;
            this.btnConfigDatabase.Text = "Cấu hình CSDL";
            this.btnConfigDatabase.UseVisualStyleBackColor = true;
            this.btnConfigDatabase.Click += new System.EventHandler(this.btnConfigDatabase_Click);
            // 
            // btnConfigFTP
            // 
            this.btnConfigFTP.Location = new System.Drawing.Point(133, 12);
            this.btnConfigFTP.Name = "btnConfigFTP";
            this.btnConfigFTP.Size = new System.Drawing.Size(117, 39);
            this.btnConfigFTP.TabIndex = 1;
            this.btnConfigFTP.Text = "Cấu hình FTP";
            this.btnConfigFTP.UseVisualStyleBackColor = true;
            this.btnConfigFTP.Click += new System.EventHandler(this.btnConfigFTP_Click);
            // 
            // btnCauHinhMayXetNghiem
            // 
            this.btnCauHinhMayXetNghiem.Location = new System.Drawing.Point(10, 57);
            this.btnCauHinhMayXetNghiem.Name = "btnCauHinhMayXetNghiem";
            this.btnCauHinhMayXetNghiem.Size = new System.Drawing.Size(240, 39);
            this.btnCauHinhMayXetNghiem.TabIndex = 2;
            this.btnCauHinhMayXetNghiem.Text = "Cấu hình máy xét nghiệm";
            this.btnCauHinhMayXetNghiem.UseVisualStyleBackColor = true;
            this.btnCauHinhMayXetNghiem.Click += new System.EventHandler(this.btnCauHinhMayXetNghiem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 107);
            this.Controls.Add(this.btnCauHinhMayXetNghiem);
            this.Controls.Add(this.btnConfigFTP);
            this.Controls.Add(this.btnConfigDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MM Config Services";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfigDatabase;
        private System.Windows.Forms.Button btnConfigFTP;
        private System.Windows.Forms.Button btnCauHinhMayXetNghiem;
    }
}

