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
    partial class dlgPrintKetQuaSieuAm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgPrintKetQuaSieuAm));
            this._uPrintKetQuaSieuAm = new MM.Controls.uPrintKetQuaSieuAm();
            this.SuspendLayout();
            // 
            // _uPrintKetQuaSieuAm
            // 
            this._uPrintKetQuaSieuAm.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPrintKetQuaSieuAm.Location = new System.Drawing.Point(0, 0);
            this._uPrintKetQuaSieuAm.Name = "_uPrintKetQuaSieuAm";
            this._uPrintKetQuaSieuAm.PatientRow = null;
            this._uPrintKetQuaSieuAm.Size = new System.Drawing.Size(803, 610);
            this._uPrintKetQuaSieuAm.TabIndex = 0;
            // 
            // dlgPrintKetQuaSieuAm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 610);
            this.Controls.Add(this._uPrintKetQuaSieuAm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgPrintKetQuaSieuAm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In ket qua sieu am";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.dlgPrintKetQuaSieuAm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uPrintKetQuaSieuAm _uPrintKetQuaSieuAm;
    }
}
