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
    partial class uNhapKhoCapCuuList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgNhapKhoCapCuu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tenCapCuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHetHanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaNhapQuiDoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDangKyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangSanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nhaPhanPhoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nhapKhoCapCuuViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenCapCuu = new System.Windows.Forms.TextBox();
            this.raTuNgayDenNgay = new System.Windows.Forms.RadioButton();
            this.raTenCapCuu = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhapKhoCapCuuViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 38);
            this.panel1.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
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
            this.btnEdit.TabIndex = 1;
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
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 5;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgNhapKhoCapCuu
            // 
            this.dgNhapKhoCapCuu.AllowUserToAddRows = false;
            this.dgNhapKhoCapCuu.AllowUserToDeleteRows = false;
            this.dgNhapKhoCapCuu.AllowUserToOrderColumns = true;
            this.dgNhapKhoCapCuu.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNhapKhoCapCuu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgNhapKhoCapCuu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNhapKhoCapCuu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.tenCapCuuDataGridViewTextBoxColumn,
            this.ngaySanXuatDataGridViewTextBoxColumn,
            this.ngayHetHanDataGridViewTextBoxColumn,
            this.soLuongNhapDataGridViewTextBoxColumn,
            this.GiaNhap,
            this.donViTinhNhapDataGridViewTextBoxColumn,
            this.donViTinhQuiDoiDataGridViewTextBoxColumn,
            this.soLuongQuiDoiDataGridViewTextBoxColumn,
            this.GiaNhapQuiDoi,
            this.soLuongXuatDataGridViewTextBoxColumn,
            this.soDangKyDataGridViewTextBoxColumn,
            this.hangSanXuatDataGridViewTextBoxColumn,
            this.nhaPhanPhoiDataGridViewTextBoxColumn,
            this.ngayNhapDataGridViewTextBoxColumn});
            this.dgNhapKhoCapCuu.ContextMenuStrip = this.ctmAction;
            this.dgNhapKhoCapCuu.DataSource = this.nhapKhoCapCuuViewBindingSource;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgNhapKhoCapCuu.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgNhapKhoCapCuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNhapKhoCapCuu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgNhapKhoCapCuu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgNhapKhoCapCuu.HighlightSelectedColumnHeaders = false;
            this.dgNhapKhoCapCuu.Location = new System.Drawing.Point(0, 0);
            this.dgNhapKhoCapCuu.MultiSelect = false;
            this.dgNhapKhoCapCuu.Name = "dgNhapKhoCapCuu";
            this.dgNhapKhoCapCuu.RowHeadersWidth = 30;
            this.dgNhapKhoCapCuu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNhapKhoCapCuu.Size = new System.Drawing.Size(973, 336);
            this.dgNhapKhoCapCuu.TabIndex = 4;
            this.dgNhapKhoCapCuu.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgNhapKhoCapCuu_CellMouseUp);
            this.dgNhapKhoCapCuu.DoubleClick += new System.EventHandler(this.dgLoThuoc_DoubleClick);
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
            // tenCapCuuDataGridViewTextBoxColumn
            // 
            this.tenCapCuuDataGridViewTextBoxColumn.DataPropertyName = "TenCapCuu";
            this.tenCapCuuDataGridViewTextBoxColumn.HeaderText = "Tên cấp cứu";
            this.tenCapCuuDataGridViewTextBoxColumn.Name = "tenCapCuuDataGridViewTextBoxColumn";
            this.tenCapCuuDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCapCuuDataGridViewTextBoxColumn.Width = 200;
            // 
            // ngaySanXuatDataGridViewTextBoxColumn
            // 
            this.ngaySanXuatDataGridViewTextBoxColumn.DataPropertyName = "NgaySanXuat";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngaySanXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngaySanXuatDataGridViewTextBoxColumn.HeaderText = "Ngày sản xuất";
            this.ngaySanXuatDataGridViewTextBoxColumn.Name = "ngaySanXuatDataGridViewTextBoxColumn";
            this.ngaySanXuatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayHetHanDataGridViewTextBoxColumn
            // 
            this.ngayHetHanDataGridViewTextBoxColumn.DataPropertyName = "NgayHetHan";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = null;
            this.ngayHetHanDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayHetHanDataGridViewTextBoxColumn.HeaderText = "Ngày hết hạn";
            this.ngayHetHanDataGridViewTextBoxColumn.Name = "ngayHetHanDataGridViewTextBoxColumn";
            this.ngayHetHanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongNhapDataGridViewTextBoxColumn
            // 
            this.soLuongNhapDataGridViewTextBoxColumn.DataPropertyName = "SoLuongNhap";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.soLuongNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.soLuongNhapDataGridViewTextBoxColumn.HeaderText = "Số lượng nhập";
            this.soLuongNhapDataGridViewTextBoxColumn.Name = "soLuongNhapDataGridViewTextBoxColumn";
            this.soLuongNhapDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongNhapDataGridViewTextBoxColumn.Width = 110;
            // 
            // GiaNhap
            // 
            this.GiaNhap.DataPropertyName = "GiaNhap";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.GiaNhap.DefaultCellStyle = dataGridViewCellStyle5;
            this.GiaNhap.HeaderText = "Giá nhập";
            this.GiaNhap.Name = "GiaNhap";
            this.GiaNhap.ReadOnly = true;
            // 
            // donViTinhNhapDataGridViewTextBoxColumn
            // 
            this.donViTinhNhapDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhNhap";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.donViTinhNhapDataGridViewTextBoxColumn.HeaderText = "ĐVT Nhập";
            this.donViTinhNhapDataGridViewTextBoxColumn.Name = "donViTinhNhapDataGridViewTextBoxColumn";
            this.donViTinhNhapDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // donViTinhQuiDoiDataGridViewTextBoxColumn
            // 
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhQuiDoi";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.HeaderText = "ĐVT qui đổi";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.Name = "donViTinhQuiDoiDataGridViewTextBoxColumn";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongQuiDoiDataGridViewTextBoxColumn
            // 
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "SoLuongQuiDoi";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.HeaderText = "Số lượng qui đổi";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Name = "soLuongQuiDoiDataGridViewTextBoxColumn";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Width = 110;
            // 
            // GiaNhapQuiDoi
            // 
            this.GiaNhapQuiDoi.DataPropertyName = "GiaNhapQuiDoi";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            this.GiaNhapQuiDoi.DefaultCellStyle = dataGridViewCellStyle9;
            this.GiaNhapQuiDoi.HeaderText = "Giá nhập qui đổi";
            this.GiaNhapQuiDoi.Name = "GiaNhapQuiDoi";
            this.GiaNhapQuiDoi.ReadOnly = true;
            this.GiaNhapQuiDoi.Width = 120;
            // 
            // soLuongXuatDataGridViewTextBoxColumn
            // 
            this.soLuongXuatDataGridViewTextBoxColumn.DataPropertyName = "SoLuongXuat";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.soLuongXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.soLuongXuatDataGridViewTextBoxColumn.HeaderText = "Số lượng xuất";
            this.soLuongXuatDataGridViewTextBoxColumn.Name = "soLuongXuatDataGridViewTextBoxColumn";
            this.soLuongXuatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soDangKyDataGridViewTextBoxColumn
            // 
            this.soDangKyDataGridViewTextBoxColumn.DataPropertyName = "SoDangKy";
            this.soDangKyDataGridViewTextBoxColumn.HeaderText = "Số đăng ký";
            this.soDangKyDataGridViewTextBoxColumn.Name = "soDangKyDataGridViewTextBoxColumn";
            this.soDangKyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hangSanXuatDataGridViewTextBoxColumn
            // 
            this.hangSanXuatDataGridViewTextBoxColumn.DataPropertyName = "HangSanXuat";
            this.hangSanXuatDataGridViewTextBoxColumn.HeaderText = "Hãng sản xuất";
            this.hangSanXuatDataGridViewTextBoxColumn.Name = "hangSanXuatDataGridViewTextBoxColumn";
            this.hangSanXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.hangSanXuatDataGridViewTextBoxColumn.Width = 150;
            // 
            // nhaPhanPhoiDataGridViewTextBoxColumn
            // 
            this.nhaPhanPhoiDataGridViewTextBoxColumn.DataPropertyName = "NhaPhanPhoi";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.HeaderText = "Nhà phân phối";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.Name = "nhaPhanPhoiDataGridViewTextBoxColumn";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.nhaPhanPhoiDataGridViewTextBoxColumn.Width = 150;
            // 
            // ngayNhapDataGridViewTextBoxColumn
            // 
            this.ngayNhapDataGridViewTextBoxColumn.DataPropertyName = "NgayNhap";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle11.NullValue = null;
            this.ngayNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.ngayNhapDataGridViewTextBoxColumn.HeaderText = "Ngày nhập";
            this.ngayNhapDataGridViewTextBoxColumn.Name = "ngayNhapDataGridViewTextBoxColumn";
            this.ngayNhapDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayNhapDataGridViewTextBoxColumn.Width = 120;
            // 
            // nhapKhoCapCuuViewBindingSource
            // 
            this.nhapKhoCapCuuViewBindingSource.DataSource = typeof(MM.Databasae.NhapKhoCapCuuView);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTenCapCuu);
            this.panel2.Controls.Add(this.raTuNgayDenNgay);
            this.panel2.Controls.Add(this.raTenCapCuu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(973, 64);
            this.panel2.TabIndex = 6;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Enabled = false;
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(279, 34);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(111, 20);
            this.dtpkDenNgay.TabIndex = 6;
            this.dtpkDenNgay.ValueChanged += new System.EventHandler(this.dtpkDenNgay_ValueChanged);
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Enabled = false;
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(103, 34);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(111, 20);
            this.dtpkTuNgay.TabIndex = 5;
            this.dtpkTuNgay.ValueChanged += new System.EventHandler(this.dtpkTuNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // txtTenCapCuu
            // 
            this.txtTenCapCuu.Location = new System.Drawing.Point(103, 10);
            this.txtTenCapCuu.Name = "txtTenCapCuu";
            this.txtTenCapCuu.Size = new System.Drawing.Size(287, 20);
            this.txtTenCapCuu.TabIndex = 2;
            this.txtTenCapCuu.TextChanged += new System.EventHandler(this.txtTenThuoc_TextChanged);
            // 
            // raTuNgayDenNgay
            // 
            this.raTuNgayDenNgay.AutoSize = true;
            this.raTuNgayDenNgay.Location = new System.Drawing.Point(16, 35);
            this.raTuNgayDenNgay.Name = "raTuNgayDenNgay";
            this.raTuNgayDenNgay.Size = new System.Drawing.Size(64, 17);
            this.raTuNgayDenNgay.TabIndex = 1;
            this.raTuNgayDenNgay.Text = "Từ ngày";
            this.raTuNgayDenNgay.UseVisualStyleBackColor = true;
            this.raTuNgayDenNgay.CheckedChanged += new System.EventHandler(this.raTuNgayDenNgay_CheckedChanged);
            // 
            // raTenCapCuu
            // 
            this.raTenCapCuu.AutoSize = true;
            this.raTenCapCuu.Checked = true;
            this.raTenCapCuu.Location = new System.Drawing.Point(16, 11);
            this.raTenCapCuu.Name = "raTenCapCuu";
            this.raTenCapCuu.Size = new System.Drawing.Size(86, 17);
            this.raTenCapCuu.TabIndex = 0;
            this.raTenCapCuu.TabStop = true;
            this.raTenCapCuu.Text = "Tên cấp cứu";
            this.raTenCapCuu.UseVisualStyleBackColor = true;
            this.raTenCapCuu.CheckedChanged += new System.EventHandler(this.raTenThuoc_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgNhapKhoCapCuu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(973, 336);
            this.panel3.TabIndex = 7;
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
            // uNhapKhoCapCuuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uNhapKhoCapCuuList";
            this.Size = new System.Drawing.Size(973, 438);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhapKhoCapCuuViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgNhapKhoCapCuu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenCapCuu;
        private System.Windows.Forms.RadioButton raTuNgayDenNgay;
        private System.Windows.Forms.RadioButton raTenCapCuu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.BindingSource nhapKhoCapCuuViewBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCapCuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHetHanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaNhapQuiDoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDangKyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangSanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nhaPhanPhoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayNhapDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
