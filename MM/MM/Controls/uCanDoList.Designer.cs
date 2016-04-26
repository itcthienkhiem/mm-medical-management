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
    partial class uCanDoList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uCanDoList));
            this.pFilter = new System.Windows.Forms.Panel();
            this.raFromDateToDate = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgCanDo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ngayCanDoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timMachDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.huyetApDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoHapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chieuCaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canNangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bMIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MuMau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThiLuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ctmAction2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chuyenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.pFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanDo)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canDoBindingSource)).BeginInit();
            this.ctmAction2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pFilter
            // 
            this.pFilter.Controls.Add(this.raFromDateToDate);
            this.pFilter.Controls.Add(this.raAll);
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.dtpkToDate);
            this.pFilter.Controls.Add(this.lbToDate);
            this.pFilter.Controls.Add(this.dtpkFromDate);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(1195, 60);
            this.pFilter.TabIndex = 1;
            // 
            // raFromDateToDate
            // 
            this.raFromDateToDate.AutoSize = true;
            this.raFromDateToDate.Location = new System.Drawing.Point(12, 31);
            this.raFromDateToDate.Name = "raFromDateToDate";
            this.raFromDateToDate.Size = new System.Drawing.Size(64, 17);
            this.raFromDateToDate.TabIndex = 9;
            this.raFromDateToDate.Text = "Từ ngày";
            this.raFromDateToDate.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(12, 9);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 8;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(338, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 21);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "    &Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpkToDate
            // 
            this.dtpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkToDate.Enabled = false;
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(236, 30);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(178, 34);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(52, 13);
            this.lbToDate.TabIndex = 3;
            this.lbToDate.Text = "đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Enabled = false;
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(78, 30);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChuyen);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1195, 37);
            this.panel2.TabIndex = 11;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(85, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "    &Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkChecked);
            this.panel1.Controls.Add(this.dgCanDo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1195, 307);
            this.panel1.TabIndex = 12;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 4;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgCanDo
            // 
            this.dgCanDo.AllowUserToAddRows = false;
            this.dgCanDo.AllowUserToDeleteRows = false;
            this.dgCanDo.AllowUserToOrderColumns = true;
            this.dgCanDo.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCanDo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgCanDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCanDo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayCanDoDataGridViewTextBoxColumn,
            this.FullName,
            this.timMachDataGridViewTextBoxColumn,
            this.huyetApDataGridViewTextBoxColumn,
            this.hoHapDataGridViewTextBoxColumn,
            this.chieuCaoDataGridViewTextBoxColumn,
            this.canNangDataGridViewTextBoxColumn,
            this.bMIDataGridViewTextBoxColumn,
            this.MuMau,
            this.ThiLuc});
            this.dgCanDo.ContextMenuStrip = this.ctmAction;
            this.dgCanDo.DataSource = this.canDoBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCanDo.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgCanDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCanDo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCanDo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCanDo.HighlightSelectedColumnHeaders = false;
            this.dgCanDo.Location = new System.Drawing.Point(0, 0);
            this.dgCanDo.MultiSelect = false;
            this.dgCanDo.Name = "dgCanDo";
            this.dgCanDo.RowHeadersWidth = 30;
            this.dgCanDo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCanDo.Size = new System.Drawing.Size(1195, 307);
            this.dgCanDo.TabIndex = 3;
            this.dgCanDo.DoubleClick += new System.EventHandler(this.dgCanDo_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // ngayCanDoDataGridViewTextBoxColumn
            // 
            this.ngayCanDoDataGridViewTextBoxColumn.DataPropertyName = "NgayCanDo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy HH:mm:ss";
            this.ngayCanDoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.ngayCanDoDataGridViewTextBoxColumn.HeaderText = "Ngày cân đo";
            this.ngayCanDoDataGridViewTextBoxColumn.Name = "ngayCanDoDataGridViewTextBoxColumn";
            this.ngayCanDoDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayCanDoDataGridViewTextBoxColumn.Width = 120;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Người khám";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 150;
            // 
            // timMachDataGridViewTextBoxColumn
            // 
            this.timMachDataGridViewTextBoxColumn.DataPropertyName = "TimMach";
            this.timMachDataGridViewTextBoxColumn.HeaderText = "Mạch";
            this.timMachDataGridViewTextBoxColumn.Name = "timMachDataGridViewTextBoxColumn";
            this.timMachDataGridViewTextBoxColumn.ReadOnly = true;
            this.timMachDataGridViewTextBoxColumn.Width = 120;
            // 
            // huyetApDataGridViewTextBoxColumn
            // 
            this.huyetApDataGridViewTextBoxColumn.DataPropertyName = "HuyetAp";
            this.huyetApDataGridViewTextBoxColumn.HeaderText = "Huyết áp";
            this.huyetApDataGridViewTextBoxColumn.Name = "huyetApDataGridViewTextBoxColumn";
            this.huyetApDataGridViewTextBoxColumn.ReadOnly = true;
            this.huyetApDataGridViewTextBoxColumn.Width = 120;
            // 
            // hoHapDataGridViewTextBoxColumn
            // 
            this.hoHapDataGridViewTextBoxColumn.DataPropertyName = "HoHap";
            this.hoHapDataGridViewTextBoxColumn.HeaderText = "Hô hấp";
            this.hoHapDataGridViewTextBoxColumn.Name = "hoHapDataGridViewTextBoxColumn";
            this.hoHapDataGridViewTextBoxColumn.ReadOnly = true;
            this.hoHapDataGridViewTextBoxColumn.Width = 120;
            // 
            // chieuCaoDataGridViewTextBoxColumn
            // 
            this.chieuCaoDataGridViewTextBoxColumn.DataPropertyName = "ChieuCao";
            this.chieuCaoDataGridViewTextBoxColumn.HeaderText = "Chiều cao";
            this.chieuCaoDataGridViewTextBoxColumn.Name = "chieuCaoDataGridViewTextBoxColumn";
            this.chieuCaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.chieuCaoDataGridViewTextBoxColumn.Width = 120;
            // 
            // canNangDataGridViewTextBoxColumn
            // 
            this.canNangDataGridViewTextBoxColumn.DataPropertyName = "CanNang";
            this.canNangDataGridViewTextBoxColumn.HeaderText = "Cân nặng";
            this.canNangDataGridViewTextBoxColumn.Name = "canNangDataGridViewTextBoxColumn";
            this.canNangDataGridViewTextBoxColumn.ReadOnly = true;
            this.canNangDataGridViewTextBoxColumn.Width = 120;
            // 
            // bMIDataGridViewTextBoxColumn
            // 
            this.bMIDataGridViewTextBoxColumn.DataPropertyName = "BMI";
            this.bMIDataGridViewTextBoxColumn.HeaderText = "BMI";
            this.bMIDataGridViewTextBoxColumn.Name = "bMIDataGridViewTextBoxColumn";
            this.bMIDataGridViewTextBoxColumn.ReadOnly = true;
            this.bMIDataGridViewTextBoxColumn.Width = 120;
            // 
            // MuMau
            // 
            this.MuMau.DataPropertyName = "MuMau";
            this.MuMau.HeaderText = "Mù màu";
            this.MuMau.Name = "MuMau";
            this.MuMau.ReadOnly = true;
            this.MuMau.Width = 120;
            // 
            // ThiLuc
            // 
            this.ThiLuc.DataPropertyName = "ThiLuc";
            this.ThiLuc.HeaderText = "Thị lực";
            this.ThiLuc.Name = "ThiLuc";
            this.ThiLuc.ReadOnly = true;
            this.ThiLuc.Width = 200;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 104);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Thêm";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::MM.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Sửa";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // canDoBindingSource
            // 
            this.canDoBindingSource.DataSource = typeof(MM.Databasae.CanDo);
            // 
            // ctmAction2
            // 
            this.ctmAction2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chuyenToolStripMenuItem});
            this.ctmAction2.Name = "cmtAction";
            this.ctmAction2.Size = new System.Drawing.Size(153, 48);
            // 
            // chuyenToolStripMenuItem
            // 
            this.chuyenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chuyenToolStripMenuItem.Image")));
            this.chuyenToolStripMenuItem.Name = "chuyenToolStripMenuItem";
            this.chuyenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chuyenToolStripMenuItem.Text = "Chuyển";
            this.chuyenToolStripMenuItem.Click += new System.EventHandler(this.chuyenToolStripMenuItem_Click);
            // 
            // btnChuyen
            // 
            this.btnChuyen.Image = ((System.Drawing.Image)(resources.GetObject("btnChuyen.Image")));
            this.btnChuyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChuyen.Location = new System.Drawing.Point(6, 6);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(75, 25);
            this.btnChuyen.TabIndex = 8;
            this.btnChuyen.Text = "      &Chuyển";
            this.btnChuyen.UseVisualStyleBackColor = true;
            this.btnChuyen.Visible = false;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // uCanDoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uCanDoList";
            this.Size = new System.Drawing.Size(1195, 404);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanDo)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canDoBindingSource)).EndInit();
            this.ctmAction2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCanDo;
        private System.Windows.Forms.BindingSource canDoBindingSource;
        private System.Windows.Forms.RadioButton raFromDateToDate;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayCanDoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn timMachDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn huyetApDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoHapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chieuCaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn canNangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bMIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MuMau;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThiLuc;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        protected System.Windows.Forms.ContextMenuStrip ctmAction2;
        private System.Windows.Forms.ToolStripMenuItem chuyenToolStripMenuItem;
        private System.Windows.Forms.Button btnChuyen;
    }
}
