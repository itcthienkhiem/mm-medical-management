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
    partial class uDrawType
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
            this.picDrawType = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawType)).BeginInit();
            this.SuspendLayout();
            // 
            // picDrawType
            // 
            this.picDrawType.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picDrawType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDrawType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDrawType.Location = new System.Drawing.Point(0, 0);
            this.picDrawType.Name = "picDrawType";
            this.picDrawType.Size = new System.Drawing.Size(16, 16);
            this.picDrawType.TabIndex = 0;
            this.picDrawType.TabStop = false;
            this.picDrawType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDrawType_MouseDown);
            this.picDrawType.MouseLeave += new System.EventHandler(this.picDrawType_MouseLeave);
            this.picDrawType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDrawType_MouseMove);
            // 
            // uDrawType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picDrawType);
            this.Name = "uDrawType";
            this.Size = new System.Drawing.Size(16, 16);
            this.Load += new System.EventHandler(this.uDrawType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDrawType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDrawType;
    }
}
