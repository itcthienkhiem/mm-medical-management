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
    partial class dlgOpentPatient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgOpentPatient));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVaoPhongCho = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenPatient = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._uSearchPatient = new MM.Controls.uSearchPatient();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.vaoPhongChoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnVaoPhongCho);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOpenPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 36);
            this.panel1.TabIndex = 3;
            // 
            // btnVaoPhongCho
            // 
            this.btnVaoPhongCho.Image = global::MM.Properties.Resources.conference_icon;
            this.btnVaoPhongCho.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVaoPhongCho.Location = new System.Drawing.Point(119, 6);
            this.btnVaoPhongCho.Name = "btnVaoPhongCho";
            this.btnVaoPhongCho.Size = new System.Drawing.Size(115, 25);
            this.btnVaoPhongCho.TabIndex = 24;
            this.btnVaoPhongCho.Text = "      &Vào phòng chờ";
            this.btnVaoPhongCho.UseVisualStyleBackColor = true;
            this.btnVaoPhongCho.Click += new System.EventHandler(this.btnVaoPhongCho_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(238, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpenPatient
            // 
            this.btnOpenPatient.Image = global::MM.Properties.Resources.folder_customer_icon__1_;
            this.btnOpenPatient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenPatient.Location = new System.Drawing.Point(8, 6);
            this.btnOpenPatient.Name = "btnOpenPatient";
            this.btnOpenPatient.Size = new System.Drawing.Size(107, 25);
            this.btnOpenPatient.TabIndex = 10;
            this.btnOpenPatient.Text = "      &Mở bệnh nhân";
            this.btnOpenPatient.UseVisualStyleBackColor = true;
            this.btnOpenPatient.Click += new System.EventHandler(this.btnOpenPatient_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._uSearchPatient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(853, 399);
            this.panel2.TabIndex = 1;
            // 
            // _uSearchPatient
            // 
            this._uSearchPatient.ContextMenuStrip = this.ctmAction;
            this._uSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uSearchPatient.HopDongGUID = "";
            this._uSearchPatient.IsMulti = false;
            this._uSearchPatient.Location = new System.Drawing.Point(0, 0);
            this._uSearchPatient.Name = "_uSearchPatient";
            this._uSearchPatient.PatientGUID = "";
            this._uSearchPatient.PatientSearchType = MM.Common.PatientSearchType.BenhNhan;
            this._uSearchPatient.ServiceGUID = "";
            this._uSearchPatient.Size = new System.Drawing.Size(853, 399);
            this._uSearchPatient.TabIndex = 0;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPatientToolStripMenuItem,
            this.toolStripSeparator1,
            this.vaoPhongChoToolStripMenuItem});
            this.ctmAction.Name = "ctmAction";
            this.ctmAction.Size = new System.Drawing.Size(157, 76);
            // 
            // openPatientToolStripMenuItem
            // 
            this.openPatientToolStripMenuItem.Image = global::MM.Properties.Resources.folder_customer_icon__1_;
            this.openPatientToolStripMenuItem.Name = "openPatientToolStripMenuItem";
            this.openPatientToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openPatientToolStripMenuItem.Text = "Mở bệnh nhân";
            this.openPatientToolStripMenuItem.Click += new System.EventHandler(this.openPatientToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // vaoPhongChoToolStripMenuItem
            // 
            this.vaoPhongChoToolStripMenuItem.Image = global::MM.Properties.Resources.conference_icon;
            this.vaoPhongChoToolStripMenuItem.Name = "vaoPhongChoToolStripMenuItem";
            this.vaoPhongChoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.vaoPhongChoToolStripMenuItem.Text = "Vào phòng chờ";
            this.vaoPhongChoToolStripMenuItem.Click += new System.EventHandler(this.vaoPhongChoToolStripMenuItem_Click);
            // 
            // dlgOpentPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(853, 435);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgOpentPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mo benh nhan";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenPatient;
        private System.Windows.Forms.Button btnCancel;
        private Controls.uSearchPatient _uSearchPatient;
        private System.Windows.Forms.Button btnVaoPhongCho;
        private System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem openPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem vaoPhongChoToolStripMenuItem;
    }
}
