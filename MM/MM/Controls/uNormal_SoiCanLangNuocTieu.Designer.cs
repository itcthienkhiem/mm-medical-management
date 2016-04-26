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
    partial class uNormal_SoiCanLangNuocTieu
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
            this.numFromValue = new System.Windows.Forms.NumericUpDown();
            this.chkFromTo = new System.Windows.Forms.CheckBox();
            this.numToValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numXValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXValue)).BeginInit();
            this.SuspendLayout();
            // 
            // numFromValue
            // 
            this.numFromValue.Location = new System.Drawing.Point(21, 0);
            this.numFromValue.Name = "numFromValue";
            this.numFromValue.Size = new System.Drawing.Size(52, 20);
            this.numFromValue.TabIndex = 13;
            // 
            // chkFromTo
            // 
            this.chkFromTo.AutoSize = true;
            this.chkFromTo.Checked = true;
            this.chkFromTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFromTo.Location = new System.Drawing.Point(0, 2);
            this.chkFromTo.Name = "chkFromTo";
            this.chkFromTo.Size = new System.Drawing.Size(15, 14);
            this.chkFromTo.TabIndex = 12;
            this.chkFromTo.UseVisualStyleBackColor = true;
            this.chkFromTo.CheckedChanged += new System.EventHandler(this.chkFromTo_CheckedChanged);
            // 
            // numToValue
            // 
            this.numToValue.Location = new System.Drawing.Point(89, 0);
            this.numToValue.Name = "numToValue";
            this.numToValue.Size = new System.Drawing.Size(52, 20);
            this.numToValue.TabIndex = 14;
            this.numToValue.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "(+)/";
            // 
            // numXValue
            // 
            this.numXValue.Location = new System.Drawing.Point(171, 0);
            this.numXValue.Name = "numXValue";
            this.numXValue.Size = new System.Drawing.Size(52, 20);
            this.numXValue.TabIndex = 17;
            this.numXValue.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "X";
            // 
            // uNormal_SoiCanLangNuocTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numXValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numToValue);
            this.Controls.Add(this.numFromValue);
            this.Controls.Add(this.chkFromTo);
            this.Name = "uNormal_SoiCanLangNuocTieu";
            this.Size = new System.Drawing.Size(242, 22);
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numFromValue;
        private System.Windows.Forms.CheckBox chkFromTo;
        private System.Windows.Forms.NumericUpDown numToValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numXValue;
        private System.Windows.Forms.Label label3;
    }
}
