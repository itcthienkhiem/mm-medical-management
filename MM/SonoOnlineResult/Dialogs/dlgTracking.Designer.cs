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
namespace SonoOnlineResult.Dialogs
{
    partial class dlgTracking
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTracking));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboUserLogon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTo = new System.Windows.Forms.DateTimePicker();
            this.dtpkFrom = new System.Windows.Forms.DateTimePicker();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.chkFrom = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgTracking = new System.Windows.Forms.DataGridView();
            this.lbCount = new System.Windows.Forms.Label();
            this.colTrackingKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrackingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCcEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbCount);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cboUserLogon);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpkTo);
            this.panel1.Controls.Add(this.dtpkFrom);
            this.panel1.Controls.Add(this.chkTo);
            this.panel1.Controls.Add(this.chkFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 72);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(422, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboUserLogon
            // 
            this.cboUserLogon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboUserLogon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboUserLogon.DisplayMember = "Username";
            this.cboUserLogon.FormattingEnabled = true;
            this.cboUserLogon.Location = new System.Drawing.Point(73, 38);
            this.cboUserLogon.Name = "cboUserLogon";
            this.cboUserLogon.Size = new System.Drawing.Size(342, 21);
            this.cboUserLogon.TabIndex = 5;
            this.cboUserLogon.ValueMember = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username:";
            // 
            // dtpkTo
            // 
            this.dtpkTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTo.Location = new System.Drawing.Point(263, 11);
            this.dtpkTo.Name = "dtpkTo";
            this.dtpkTo.Size = new System.Drawing.Size(152, 20);
            this.dtpkTo.TabIndex = 3;
            // 
            // dtpkFrom
            // 
            this.dtpkFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFrom.Location = new System.Drawing.Point(58, 11);
            this.dtpkFrom.Name = "dtpkFrom";
            this.dtpkFrom.Size = new System.Drawing.Size(152, 20);
            this.dtpkFrom.TabIndex = 1;
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Checked = true;
            this.chkTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTo.Location = new System.Drawing.Point(223, 13);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(39, 17);
            this.chkTo.TabIndex = 2;
            this.chkTo.Text = "To";
            this.chkTo.UseVisualStyleBackColor = true;
            this.chkTo.CheckedChanged += new System.EventHandler(this.chkTo_CheckedChanged);
            // 
            // chkFrom
            // 
            this.chkFrom.AutoSize = true;
            this.chkFrom.Checked = true;
            this.chkFrom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFrom.Location = new System.Drawing.Point(11, 13);
            this.chkFrom.Name = "chkFrom";
            this.chkFrom.Size = new System.Drawing.Size(49, 17);
            this.chkFrom.TabIndex = 0;
            this.chkFrom.Text = "From";
            this.chkFrom.UseVisualStyleBackColor = true;
            this.chkFrom.CheckedChanged += new System.EventHandler(this.chkFrom_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnExportToExcel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(712, 35);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(106, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(7, 6);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(95, 23);
            this.btnExportToExcel.TabIndex = 0;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgTracking);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(712, 404);
            this.panel3.TabIndex = 2;
            // 
            // dgTracking
            // 
            this.dgTracking.AllowUserToAddRows = false;
            this.dgTracking.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTracking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTracking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrackingKey,
            this.colTrackingDate,
            this.colUsername,
            this.colBranch,
            this.colToEmail,
            this.colCcEmail,
            this.colNotes});
            this.dgTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTracking.Location = new System.Drawing.Point(0, 0);
            this.dgTracking.Name = "dgTracking";
            this.dgTracking.ReadOnly = true;
            this.dgTracking.RowHeadersVisible = false;
            this.dgTracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTracking.Size = new System.Drawing.Size(712, 404);
            this.dgTracking.TabIndex = 0;
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCount.ForeColor = System.Drawing.Color.Blue;
            this.lbCount.Location = new System.Drawing.Point(430, 41);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(59, 15);
            this.lbCount.TabIndex = 7;
            this.lbCount.Text = "0 row(s)";
            // 
            // colTrackingKey
            // 
            this.colTrackingKey.DataPropertyName = "TrackingKey";
            this.colTrackingKey.HeaderText = "TrackingKey";
            this.colTrackingKey.Name = "colTrackingKey";
            this.colTrackingKey.ReadOnly = true;
            this.colTrackingKey.Visible = false;
            // 
            // colTrackingDate
            // 
            this.colTrackingDate.DataPropertyName = "TrackingDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            this.colTrackingDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTrackingDate.HeaderText = "Tracking Date";
            this.colTrackingDate.Name = "colTrackingDate";
            this.colTrackingDate.ReadOnly = true;
            this.colTrackingDate.Width = 120;
            // 
            // colUsername
            // 
            this.colUsername.DataPropertyName = "Username";
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.ReadOnly = true;
            this.colUsername.Width = 150;
            // 
            // colBranch
            // 
            this.colBranch.DataPropertyName = "BranchName";
            this.colBranch.HeaderText = "Branch";
            this.colBranch.Name = "colBranch";
            this.colBranch.ReadOnly = true;
            this.colBranch.Width = 150;
            // 
            // colToEmail
            // 
            this.colToEmail.DataPropertyName = "ToEmail";
            this.colToEmail.HeaderText = "To Email";
            this.colToEmail.Name = "colToEmail";
            this.colToEmail.ReadOnly = true;
            this.colToEmail.Width = 120;
            // 
            // colCcEmail
            // 
            this.colCcEmail.DataPropertyName = "CcEmail";
            this.colCcEmail.HeaderText = "Cc Email";
            this.colCcEmail.Name = "colCcEmail";
            this.colCcEmail.ReadOnly = true;
            this.colCcEmail.Width = 150;
            // 
            // colNotes
            // 
            this.colNotes.DataPropertyName = "Note";
            this.colNotes.HeaderText = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            this.colNotes.Visible = false;
            // 
            // dlgTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(712, 511);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tracking";
            this.Load += new System.EventHandler(this.dlgTracking_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.ComboBox cboUserLogon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTo;
        private System.Windows.Forms.DateTimePicker dtpkFrom;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.CheckBox chkFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgTracking;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrackingKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrackingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCcEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
    }
}
