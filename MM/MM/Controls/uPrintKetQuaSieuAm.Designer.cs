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
    partial class uPrintKetQuaSieuAm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uPrintKetQuaSieuAm));
            this.toolStripSieuAm = new System.Windows.Forms.ToolStrip();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this._textControl = new TXTextControl.TextControl();
            this.toolStripSieuAm.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSieuAm
            // 
            this.toolStripSieuAm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbPrint});
            this.toolStripSieuAm.Location = new System.Drawing.Point(0, 0);
            this.toolStripSieuAm.Name = "toolStripSieuAm";
            this.toolStripSieuAm.Size = new System.Drawing.Size(746, 25);
            this.toolStripSieuAm.TabIndex = 0;
            this.toolStripSieuAm.Text = "toolStrip1";
            // 
            // tbPrint
            // 
            this.tbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPrint.Image")));
            this.tbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(23, 22);
            this.tbPrint.Text = "toolStripButton1";
            this.tbPrint.ToolTipText = "Print";
            this.tbPrint.Click += new System.EventHandler(this.tbPrint_Click);
            // 
            // _textControl
            // 
            this._textControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textControl.EditMode = TXTextControl.EditMode.ReadAndSelect;
            this._textControl.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textControl.Location = new System.Drawing.Point(0, 25);
            this._textControl.Name = "_textControl";
            this._textControl.PageMargins.Bottom = 79;
            this._textControl.PageMargins.Left = 79;
            this._textControl.PageMargins.Right = 79;
            this._textControl.PageMargins.Top = 79;
            this._textControl.Size = new System.Drawing.Size(746, 532);
            this._textControl.TabIndex = 2;
            this._textControl.ViewMode = TXTextControl.ViewMode.PageView;
            // 
            // uPrintKetQuaSieuAm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._textControl);
            this.Controls.Add(this.toolStripSieuAm);
            this.Name = "uPrintKetQuaSieuAm";
            this.Size = new System.Drawing.Size(746, 557);
            this.toolStripSieuAm.ResumeLayout(false);
            this.toolStripSieuAm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripSieuAm;
        private System.Windows.Forms.ToolStripButton tbPrint;
        private TXTextControl.TextControl _textControl;
    }
}
