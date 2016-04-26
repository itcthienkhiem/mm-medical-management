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
    partial class ucReportViewer
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
            this._reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // _reportViewer
            // 
            this._reportViewer.AutoScroll = true;
            this._reportViewer.AutoSize = true;
            this._reportViewer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportViewer.Location = new System.Drawing.Point(0, 0);
            this._reportViewer.Name = "_reportViewer";
            this._reportViewer.ShowBackButton = false;
            this._reportViewer.ShowDocumentMapButton = false;
            this._reportViewer.ShowExportButton = false;
            this._reportViewer.ShowRefreshButton = false;
            this._reportViewer.ShowStopButton = false;
            this._reportViewer.Size = new System.Drawing.Size(481, 364);
            this._reportViewer.TabIndex = 0;
            this._reportViewer.WaitControlDisplayAfter = 300;
            // 
            // ucReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._reportViewer);
            this.Name = "ucReportViewer";
            this.Size = new System.Drawing.Size(481, 364);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer _reportViewer;
    }
}
