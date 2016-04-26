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
    partial class uKetQuaSoiCTCList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uKetQuaSoiCTCList));
            this.pFilter = new System.Windows.Forms.Panel();
            this.raFromDateToDate = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgSoCTC = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ngayKhamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketLuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deNghiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amHoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amDaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bieuMoLatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moDemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ranhGioiLatTruDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sauAcidAceticDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sauLugolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ketQuaSoiCTCViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.ctmAction2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chuyenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.pFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSoCTC)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaSoiCTCViewBindingSource)).BeginInit();
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
            this.pFilter.Size = new System.Drawing.Size(816, 60);
            this.pFilter.TabIndex = 5;
            // 
            // raFromDateToDate
            // 
            this.raFromDateToDate.AutoSize = true;
            this.raFromDateToDate.Location = new System.Drawing.Point(18, 31);
            this.raFromDateToDate.Name = "raFromDateToDate";
            this.raFromDateToDate.Size = new System.Drawing.Size(64, 17);
            this.raFromDateToDate.TabIndex = 17;
            this.raFromDateToDate.Text = "Từ ngày";
            this.raFromDateToDate.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(18, 9);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 16;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(346, 31);
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
            this.dtpkToDate.Location = new System.Drawing.Point(244, 31);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(187, 34);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(53, 13);
            this.lbToDate.TabIndex = 3;
            this.lbToDate.Text = "Đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Enabled = false;
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(86, 31);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChuyen);
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.btnPrintPreview);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 455);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 37);
            this.panel2.TabIndex = 15;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(410, 6);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 76;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(244, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 7;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(341, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.panel1.Controls.Add(this.dgSoCTC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 395);
            this.panel1.TabIndex = 16;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 12;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgSoCTC
            // 
            this.dgSoCTC.AllowUserToAddRows = false;
            this.dgSoCTC.AllowUserToDeleteRows = false;
            this.dgSoCTC.AllowUserToOrderColumns = true;
            this.dgSoCTC.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSoCTC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSoCTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSoCTC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayKhamDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.ketLuanDataGridViewTextBoxColumn,
            this.deNghiDataGridViewTextBoxColumn,
            this.amHoDataGridViewTextBoxColumn,
            this.amDaoDataGridViewTextBoxColumn,
            this.cTCDataGridViewTextBoxColumn,
            this.bieuMoLatDataGridViewTextBoxColumn,
            this.moDemDataGridViewTextBoxColumn,
            this.ranhGioiLatTruDataGridViewTextBoxColumn,
            this.sauAcidAceticDataGridViewTextBoxColumn,
            this.sauLugolDataGridViewTextBoxColumn});
            this.dgSoCTC.ContextMenuStrip = this.ctmAction;
            this.dgSoCTC.DataSource = this.ketQuaSoiCTCViewBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSoCTC.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgSoCTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSoCTC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgSoCTC.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSoCTC.HighlightSelectedColumnHeaders = false;
            this.dgSoCTC.Location = new System.Drawing.Point(0, 0);
            this.dgSoCTC.MultiSelect = false;
            this.dgSoCTC.Name = "dgSoCTC";
            this.dgSoCTC.RowHeadersWidth = 30;
            this.dgSoCTC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSoCTC.Size = new System.Drawing.Size(816, 395);
            this.dgSoCTC.TabIndex = 11;
            this.dgSoCTC.DoubleClick += new System.EventHandler(this.dgSoCTC_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // ngayKhamDataGridViewTextBoxColumn
            // 
            this.ngayKhamDataGridViewTextBoxColumn.DataPropertyName = "NgayKham";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            dataGridViewCellStyle5.NullValue = null;
            this.ngayKhamDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.ngayKhamDataGridViewTextBoxColumn.HeaderText = "Ngày khám";
            this.ngayKhamDataGridViewTextBoxColumn.Name = "ngayKhamDataGridViewTextBoxColumn";
            this.ngayKhamDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Bác sĩ soi";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // ketLuanDataGridViewTextBoxColumn
            // 
            this.ketLuanDataGridViewTextBoxColumn.DataPropertyName = "KetLuan";
            this.ketLuanDataGridViewTextBoxColumn.HeaderText = "Kết luận";
            this.ketLuanDataGridViewTextBoxColumn.Name = "ketLuanDataGridViewTextBoxColumn";
            this.ketLuanDataGridViewTextBoxColumn.ReadOnly = true;
            this.ketLuanDataGridViewTextBoxColumn.Width = 200;
            // 
            // deNghiDataGridViewTextBoxColumn
            // 
            this.deNghiDataGridViewTextBoxColumn.DataPropertyName = "DeNghi";
            this.deNghiDataGridViewTextBoxColumn.HeaderText = "Đề nghị";
            this.deNghiDataGridViewTextBoxColumn.Name = "deNghiDataGridViewTextBoxColumn";
            this.deNghiDataGridViewTextBoxColumn.ReadOnly = true;
            this.deNghiDataGridViewTextBoxColumn.Width = 200;
            // 
            // amHoDataGridViewTextBoxColumn
            // 
            this.amHoDataGridViewTextBoxColumn.DataPropertyName = "AmHo";
            this.amHoDataGridViewTextBoxColumn.HeaderText = "Âm hộ";
            this.amHoDataGridViewTextBoxColumn.Name = "amHoDataGridViewTextBoxColumn";
            this.amHoDataGridViewTextBoxColumn.ReadOnly = true;
            this.amHoDataGridViewTextBoxColumn.Width = 150;
            // 
            // amDaoDataGridViewTextBoxColumn
            // 
            this.amDaoDataGridViewTextBoxColumn.DataPropertyName = "AmDao";
            this.amDaoDataGridViewTextBoxColumn.HeaderText = "Âm đạo";
            this.amDaoDataGridViewTextBoxColumn.Name = "amDaoDataGridViewTextBoxColumn";
            this.amDaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.amDaoDataGridViewTextBoxColumn.Width = 150;
            // 
            // cTCDataGridViewTextBoxColumn
            // 
            this.cTCDataGridViewTextBoxColumn.DataPropertyName = "CTC";
            this.cTCDataGridViewTextBoxColumn.HeaderText = "CTC";
            this.cTCDataGridViewTextBoxColumn.Name = "cTCDataGridViewTextBoxColumn";
            this.cTCDataGridViewTextBoxColumn.ReadOnly = true;
            this.cTCDataGridViewTextBoxColumn.Width = 150;
            // 
            // bieuMoLatDataGridViewTextBoxColumn
            // 
            this.bieuMoLatDataGridViewTextBoxColumn.DataPropertyName = "BieuMoLat";
            this.bieuMoLatDataGridViewTextBoxColumn.HeaderText = "Biểu mô lát";
            this.bieuMoLatDataGridViewTextBoxColumn.Name = "bieuMoLatDataGridViewTextBoxColumn";
            this.bieuMoLatDataGridViewTextBoxColumn.ReadOnly = true;
            this.bieuMoLatDataGridViewTextBoxColumn.Width = 150;
            // 
            // moDemDataGridViewTextBoxColumn
            // 
            this.moDemDataGridViewTextBoxColumn.DataPropertyName = "MoDem";
            this.moDemDataGridViewTextBoxColumn.HeaderText = "Mô đệm";
            this.moDemDataGridViewTextBoxColumn.Name = "moDemDataGridViewTextBoxColumn";
            this.moDemDataGridViewTextBoxColumn.ReadOnly = true;
            this.moDemDataGridViewTextBoxColumn.Width = 150;
            // 
            // ranhGioiLatTruDataGridViewTextBoxColumn
            // 
            this.ranhGioiLatTruDataGridViewTextBoxColumn.DataPropertyName = "RanhGioiLatTru";
            this.ranhGioiLatTruDataGridViewTextBoxColumn.HeaderText = "Ranh giới lát trụ";
            this.ranhGioiLatTruDataGridViewTextBoxColumn.Name = "ranhGioiLatTruDataGridViewTextBoxColumn";
            this.ranhGioiLatTruDataGridViewTextBoxColumn.ReadOnly = true;
            this.ranhGioiLatTruDataGridViewTextBoxColumn.Width = 150;
            // 
            // sauAcidAceticDataGridViewTextBoxColumn
            // 
            this.sauAcidAceticDataGridViewTextBoxColumn.DataPropertyName = "SauAcidAcetic";
            this.sauAcidAceticDataGridViewTextBoxColumn.HeaderText = "Sau Acid Acetic";
            this.sauAcidAceticDataGridViewTextBoxColumn.Name = "sauAcidAceticDataGridViewTextBoxColumn";
            this.sauAcidAceticDataGridViewTextBoxColumn.ReadOnly = true;
            this.sauAcidAceticDataGridViewTextBoxColumn.Width = 150;
            // 
            // sauLugolDataGridViewTextBoxColumn
            // 
            this.sauLugolDataGridViewTextBoxColumn.DataPropertyName = "SauLugol";
            this.sauLugolDataGridViewTextBoxColumn.HeaderText = "Sau Lugol";
            this.sauLugolDataGridViewTextBoxColumn.Name = "sauLugolDataGridViewTextBoxColumn";
            this.sauLugolDataGridViewTextBoxColumn.ReadOnly = true;
            this.sauLugolDataGridViewTextBoxColumn.Width = 150;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.toolStripSeparator5,
            this.exportExcelToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 188);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printPreviewToolStripMenuItem.Text = "Xem bản in";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printToolStripMenuItem.Text = "In";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Image = global::MM.Properties.Resources.page_excel_icon;
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportExcelToolStripMenuItem.Text = "Xuất Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // ketQuaSoiCTCViewBindingSource
            // 
            this.ketQuaSoiCTCViewBindingSource.DataSource = typeof(MM.Databasae.KetQuaSoiCTCView);
            // 
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
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
            this.btnChuyen.Location = new System.Drawing.Point(7, 6);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(75, 25);
            this.btnChuyen.TabIndex = 78;
            this.btnChuyen.Text = "      &Chuyển";
            this.btnChuyen.UseVisualStyleBackColor = true;
            this.btnChuyen.Visible = false;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // uKetQuaSoiCTCList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uKetQuaSoiCTCList";
            this.Size = new System.Drawing.Size(816, 492);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSoCTC)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaSoiCTCViewBindingSource)).EndInit();
            this.ctmAction2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.RadioButton raFromDateToDate;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSoCTC;
        private System.Windows.Forms.BindingSource ketQuaSoiCTCViewBindingSource;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKhamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ketLuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deNghiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amHoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amDaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bieuMoLatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moDemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ranhGioiLatTruDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sauAcidAceticDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sauLugolDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        protected System.Windows.Forms.ContextMenuStrip ctmAction2;
        private System.Windows.Forms.ToolStripMenuItem chuyenToolStripMenuItem;
        private System.Windows.Forms.Button btnChuyen;
    }
}
