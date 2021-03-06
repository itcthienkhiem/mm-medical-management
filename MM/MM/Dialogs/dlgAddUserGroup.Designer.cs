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
    partial class dlgAddUserGroup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddUserGroup));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgPermission = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.FunctionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsView = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsAdd = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsEdit = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsDelete = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsPrint = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsImport = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsExport = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsExportAll = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsConfirm = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsLock = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsCreateReport = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsUpload = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.IsSendSMS = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.userGroupPermissionViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ctmPermission = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userGroupPermissionViewBindingSource)).BeginInit();
            this.ctmPermission.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGroupName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin nhóm người sử dụng";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(155, 20);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(305, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên nhóm người sử dụng:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgPermission);
            this.groupBox2.Location = new System.Drawing.Point(6, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 471);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phân quyền";
            // 
            // dgPermission
            // 
            this.dgPermission.AllowUserToAddRows = false;
            this.dgPermission.AllowUserToDeleteRows = false;
            this.dgPermission.AllowUserToResizeColumns = false;
            this.dgPermission.AllowUserToResizeRows = false;
            this.dgPermission.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPermission.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPermission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FunctionCode,
            this.FunctionName,
            this.IsView,
            this.IsAdd,
            this.IsEdit,
            this.IsDelete,
            this.IsPrint,
            this.IsImport,
            this.IsExport,
            this.IsExportAll,
            this.IsConfirm,
            this.IsLock,
            this.IsCreateReport,
            this.IsUpload,
            this.IsSendSMS});
            this.dgPermission.DataSource = this.userGroupPermissionViewBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPermission.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPermission.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPermission.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPermission.HighlightSelectedColumnHeaders = false;
            this.dgPermission.Location = new System.Drawing.Point(9, 18);
            this.dgPermission.MultiSelect = false;
            this.dgPermission.Name = "dgPermission";
            this.dgPermission.RowHeadersVisible = false;
            this.dgPermission.RowHeadersWidth = 30;
            this.dgPermission.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPermission.Size = new System.Drawing.Size(873, 442);
            this.dgPermission.TabIndex = 4;
            this.dgPermission.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPermission_CellMouseDown);
            // 
            // FunctionCode
            // 
            this.FunctionCode.DataPropertyName = "FunctionCode";
            this.FunctionCode.HeaderText = "FunctionCode";
            this.FunctionCode.Name = "FunctionCode";
            this.FunctionCode.ReadOnly = true;
            this.FunctionCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FunctionCode.Visible = false;
            // 
            // FunctionName
            // 
            this.FunctionName.DataPropertyName = "FunctionName";
            this.FunctionName.HeaderText = "Tên chức năng";
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.ReadOnly = true;
            this.FunctionName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FunctionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FunctionName.Width = 230;
            // 
            // IsView
            // 
            this.IsView.DataPropertyName = "IsView";
            this.IsView.HeaderText = "Xem";
            this.IsView.Name = "IsView";
            this.IsView.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsView.Width = 40;
            // 
            // IsAdd
            // 
            this.IsAdd.DataPropertyName = "IsAdd";
            this.IsAdd.HeaderText = "Thêm";
            this.IsAdd.Name = "IsAdd";
            this.IsAdd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsAdd.Width = 40;
            // 
            // IsEdit
            // 
            this.IsEdit.DataPropertyName = "IsEdit";
            this.IsEdit.HeaderText = "Sửa";
            this.IsEdit.Name = "IsEdit";
            this.IsEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsEdit.Width = 40;
            // 
            // IsDelete
            // 
            this.IsDelete.DataPropertyName = "IsDelete";
            this.IsDelete.HeaderText = "Xóa";
            this.IsDelete.Name = "IsDelete";
            this.IsDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsDelete.Width = 40;
            // 
            // IsPrint
            // 
            this.IsPrint.DataPropertyName = "IsPrint";
            this.IsPrint.HeaderText = "In";
            this.IsPrint.Name = "IsPrint";
            this.IsPrint.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsPrint.Width = 40;
            // 
            // IsImport
            // 
            this.IsImport.DataPropertyName = "IsImport";
            this.IsImport.HeaderText = "Nhập";
            this.IsImport.Name = "IsImport";
            this.IsImport.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsImport.Width = 40;
            // 
            // IsExport
            // 
            this.IsExport.DataPropertyName = "IsExport";
            this.IsExport.HeaderText = "Xuất";
            this.IsExport.Name = "IsExport";
            this.IsExport.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsExport.Width = 40;
            // 
            // IsExportAll
            // 
            this.IsExportAll.DataPropertyName = "IsExportAll";
            this.IsExportAll.HeaderText = "Xuất hết";
            this.IsExportAll.Name = "IsExportAll";
            this.IsExportAll.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsExportAll.Width = 55;
            // 
            // IsConfirm
            // 
            this.IsConfirm.DataPropertyName = "IsConfirm";
            this.IsConfirm.HeaderText = "Duyệt";
            this.IsConfirm.Name = "IsConfirm";
            this.IsConfirm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsConfirm.Width = 40;
            // 
            // IsLock
            // 
            this.IsLock.DataPropertyName = "IsLock";
            this.IsLock.HeaderText = "Khóa";
            this.IsLock.Name = "IsLock";
            this.IsLock.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsLock.Width = 40;
            // 
            // IsCreateReport
            // 
            this.IsCreateReport.DataPropertyName = "IsCreateReport";
            this.IsCreateReport.HeaderText = "Tạo hồ sơ";
            this.IsCreateReport.Name = "IsCreateReport";
            this.IsCreateReport.Width = 65;
            // 
            // IsUpload
            // 
            this.IsUpload.DataPropertyName = "IsUpload";
            this.IsUpload.HeaderText = "Upload hồ sơ";
            this.IsUpload.Name = "IsUpload";
            this.IsUpload.Width = 80;
            // 
            // IsSendSMS
            // 
            this.IsSendSMS.DataPropertyName = "IsSendSMS";
            this.IsSendSMS.HeaderText = "Gửi SMS";
            this.IsSendSMS.Name = "IsSendSMS";
            this.IsSendSMS.Width = 60;
            // 
            // userGroupPermissionViewBindingSource
            // 
            this.userGroupPermissionViewBindingSource.DataSource = typeof(MM.Databasae.UserGroup_PermissionView);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(456, 541);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(377, 541);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ctmPermission
            // 
            this.ctmPermission.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem});
            this.ctmPermission.Name = "ctmPermission";
            this.ctmPermission.Size = new System.Drawing.Size(155, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.selectAllToolStripMenuItem.Text = "&Chọn tất cả";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.unselectAllToolStripMenuItem.Text = "&Bỏ chọn tất cả";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // dlgAddUserGroup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(908, 573);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddUserGroup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them nhom nguoi su dung";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddUserLogon_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddUserLogon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPermission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userGroupPermissionViewBindingSource)).EndInit();
            this.ctmPermission.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPermission;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip ctmPermission;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.BindingSource userGroupPermissionViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionName;
        private Controls.DataGridViewDisableCheckBoxColumn IsView;
        private Controls.DataGridViewDisableCheckBoxColumn IsAdd;
        private Controls.DataGridViewDisableCheckBoxColumn IsEdit;
        private Controls.DataGridViewDisableCheckBoxColumn IsDelete;
        private Controls.DataGridViewDisableCheckBoxColumn IsPrint;
        private Controls.DataGridViewDisableCheckBoxColumn IsImport;
        private Controls.DataGridViewDisableCheckBoxColumn IsExport;
        private Controls.DataGridViewDisableCheckBoxColumn IsExportAll;
        private Controls.DataGridViewDisableCheckBoxColumn IsConfirm;
        private Controls.DataGridViewDisableCheckBoxColumn IsLock;
        private Controls.DataGridViewDisableCheckBoxColumn IsCreateReport;
        private Controls.DataGridViewDisableCheckBoxColumn IsUpload;
        private Controls.DataGridViewDisableCheckBoxColumn IsSendSMS;
    }
}
