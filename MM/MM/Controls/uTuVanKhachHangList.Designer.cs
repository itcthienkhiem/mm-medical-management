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
    partial class uTuVanKhachHangList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSoDongChon = new System.Windows.Forms.Label();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.cboINOUT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTuNgay = new System.Windows.Forms.CheckBox();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtTenNguoiTao = new System.Windows.Forms.TextBox();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.chkDenNgay = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgYKienKhachHang = new DevComponents.DotNetBar.Controls.DataGridViewX();
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
            this.yKienKhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contactDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenCongTy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenKhachHangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDienThoaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaChiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MucDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yeuCauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KetLuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BacSiPhuTrach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaXong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nguonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INOUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTongDai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBanThe = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NguoiKetLuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiCapNhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgYKienKhachHang)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yKienKhachHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbSoDongChon);
            this.panel1.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel1.Controls.Add(this.cboINOUT);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkTuNgay);
            this.panel1.Controls.Add(this.cboDocStaff);
            this.panel1.Controls.Add(this.txtTenNguoiTao);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.txtTenBenhNhan);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.chkDenNgay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1188, 131);
            this.panel1.TabIndex = 0;
            // 
            // lbSoDongChon
            // 
            this.lbSoDongChon.AutoSize = true;
            this.lbSoDongChon.ForeColor = System.Drawing.Color.Red;
            this.lbSoDongChon.Location = new System.Drawing.Point(631, 107);
            this.lbSoDongChon.Name = "lbSoDongChon";
            this.lbSoDongChon.Size = new System.Drawing.Size(86, 13);
            this.lbSoDongChon.TabIndex = 20;
            this.lbSoDongChon.Text = "Số dòng chọn: 0";
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(405, 107);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 19;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // cboINOUT
            // 
            this.cboINOUT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboINOUT.FormattingEnabled = true;
            this.cboINOUT.Items.AddRange(new object[] {
            "",
            "IN",
            "OUT"});
            this.cboINOUT.Location = new System.Drawing.Point(104, 102);
            this.cboINOUT.Name = "cboINOUT";
            this.cboINOUT.Size = new System.Drawing.Size(69, 21);
            this.cboINOUT.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "IN/OUT:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Bác sĩ phụ trách:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tên người tạo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Tên khách hàng:";
            // 
            // chkTuNgay
            // 
            this.chkTuNgay.AutoSize = true;
            this.chkTuNgay.Location = new System.Drawing.Point(12, 10);
            this.chkTuNgay.Name = "chkTuNgay";
            this.chkTuNgay.Size = new System.Drawing.Size(65, 17);
            this.chkTuNgay.TabIndex = 1;
            this.chkTuNgay.Text = "Từ ngày";
            this.chkTuNgay.UseVisualStyleBackColor = true;
            this.chkTuNgay.CheckedChanged += new System.EventHandler(this.chkTuNgay_CheckedChanged);
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "Fullname";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(104, 78);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(292, 21);
            this.cboDocStaff.TabIndex = 13;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            this.cboDocStaff.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboDocStaff_KeyUp);
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // txtTenNguoiTao
            // 
            this.txtTenNguoiTao.Location = new System.Drawing.Point(104, 55);
            this.txtTenNguoiTao.Name = "txtTenNguoiTao";
            this.txtTenNguoiTao.Size = new System.Drawing.Size(292, 20);
            this.txtTenNguoiTao.TabIndex = 8;
            this.txtTenNguoiTao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTenNguoiTao_KeyUp);
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(321, 102);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(104, 32);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.Size = new System.Drawing.Size(292, 20);
            this.txtTenBenhNhan.TabIndex = 6;
            this.txtTenBenhNhan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTenBenhNhan_KeyUp);
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Enabled = false;
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(283, 8);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 4;
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Enabled = false;
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(80, 8);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(117, 20);
            this.dtpkTuNgay.TabIndex = 2;
            // 
            // chkDenNgay
            // 
            this.chkDenNgay.AutoSize = true;
            this.chkDenNgay.Location = new System.Drawing.Point(215, 11);
            this.chkDenNgay.Name = "chkDenNgay";
            this.chkDenNgay.Size = new System.Drawing.Size(71, 17);
            this.chkDenNgay.TabIndex = 3;
            this.chkDenNgay.Text = "đến ngày";
            this.chkDenNgay.UseVisualStyleBackColor = true;
            this.chkDenNgay.CheckedChanged += new System.EventHandler(this.chkDenNgay_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.btnPrintPreview);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 575);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1188, 38);
            this.panel2.TabIndex = 1;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(408, 6);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 79;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(243, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 77;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(340, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 78;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgYKienKhachHang);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 131);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1188, 444);
            this.panel3.TabIndex = 2;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 3;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgYKienKhachHang
            // 
            this.dgYKienKhachHang.AllowUserToAddRows = false;
            this.dgYKienKhachHang.AllowUserToDeleteRows = false;
            this.dgYKienKhachHang.AllowUserToOrderColumns = true;
            this.dgYKienKhachHang.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgYKienKhachHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgYKienKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgYKienKhachHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.contactDateDataGridViewTextBoxColumn,
            this.UpdatedDate,
            this.TenCongTy,
            this.MaKhachHang,
            this.tenKhachHangDataGridViewTextBoxColumn,
            this.soDienThoaiDataGridViewTextBoxColumn,
            this.diaChiDataGridViewTextBoxColumn,
            this.MucDich,
            this.yeuCauDataGridViewTextBoxColumn,
            this.KetLuan,
            this.NguoiTao,
            this.BacSiPhuTrach,
            this.DaXong,
            this.nguonDataGridViewTextBoxColumn,
            this.INOUT,
            this.SoTongDai,
            this.colBanThe,
            this.NguoiKetLuan,
            this.NguoiCapNhat});
            this.dgYKienKhachHang.ContextMenuStrip = this.ctmAction;
            this.dgYKienKhachHang.DataSource = this.yKienKhachHangBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgYKienKhachHang.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgYKienKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgYKienKhachHang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgYKienKhachHang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgYKienKhachHang.HighlightSelectedColumnHeaders = false;
            this.dgYKienKhachHang.Location = new System.Drawing.Point(0, 0);
            this.dgYKienKhachHang.Name = "dgYKienKhachHang";
            this.dgYKienKhachHang.RowHeadersWidth = 30;
            this.dgYKienKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgYKienKhachHang.Size = new System.Drawing.Size(1188, 444);
            this.dgYKienKhachHang.TabIndex = 2;
            this.dgYKienKhachHang.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgYKienKhachHang_CellMouseUp);
            this.dgYKienKhachHang.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgYKienKhachHang_ColumnHeaderMouseClick);
            this.dgYKienKhachHang.SelectionChanged += new System.EventHandler(this.dgYKienKhachHang_SelectionChanged);
            this.dgYKienKhachHang.DoubleClick += new System.EventHandler(this.dgYKienKhachHang_DoubleClick);
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
            this.ctmAction.Size = new System.Drawing.Size(135, 166);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.addToolStripMenuItem.Text = "Thêm";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::MM.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.editToolStripMenuItem.Text = "Sửa";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(131, 6);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.printPreviewToolStripMenuItem.Text = "Xem bản in";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(131, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.printToolStripMenuItem.Text = "In";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(131, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Image = global::MM.Properties.Resources.page_excel_icon;
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exportExcelToolStripMenuItem.Text = "Xuất Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // yKienKhachHangBindingSource
            // 
            this.yKienKhachHangBindingSource.DataSource = typeof(MM.Databasae.YKienKhachHang);
            // 
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
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
            // contactDateDataGridViewTextBoxColumn
            // 
            this.contactDateDataGridViewTextBoxColumn.DataPropertyName = "ContactDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.contactDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.contactDateDataGridViewTextBoxColumn.HeaderText = "Ngày liên hệ";
            this.contactDateDataGridViewTextBoxColumn.Name = "contactDateDataGridViewTextBoxColumn";
            this.contactDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.contactDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // UpdatedDate
            // 
            this.UpdatedDate.DataPropertyName = "UpdatedDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            this.UpdatedDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.UpdatedDate.HeaderText = "Ngày cập nhật";
            this.UpdatedDate.Name = "UpdatedDate";
            this.UpdatedDate.ReadOnly = true;
            this.UpdatedDate.Width = 120;
            // 
            // TenCongTy
            // 
            this.TenCongTy.DataPropertyName = "TenCongTy";
            this.TenCongTy.HeaderText = "Tên công ty";
            this.TenCongTy.Name = "TenCongTy";
            this.TenCongTy.ReadOnly = true;
            this.TenCongTy.Width = 150;
            // 
            // MaKhachHang
            // 
            this.MaKhachHang.DataPropertyName = "MaKhachHang";
            this.MaKhachHang.HeaderText = "Mã khách hàng";
            this.MaKhachHang.Name = "MaKhachHang";
            this.MaKhachHang.ReadOnly = true;
            this.MaKhachHang.Width = 120;
            // 
            // tenKhachHangDataGridViewTextBoxColumn
            // 
            this.tenKhachHangDataGridViewTextBoxColumn.DataPropertyName = "TenKhachHang";
            this.tenKhachHangDataGridViewTextBoxColumn.HeaderText = "Tên khách hàng";
            this.tenKhachHangDataGridViewTextBoxColumn.Name = "tenKhachHangDataGridViewTextBoxColumn";
            this.tenKhachHangDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenKhachHangDataGridViewTextBoxColumn.Width = 200;
            // 
            // soDienThoaiDataGridViewTextBoxColumn
            // 
            this.soDienThoaiDataGridViewTextBoxColumn.DataPropertyName = "SoDienThoai";
            this.soDienThoaiDataGridViewTextBoxColumn.HeaderText = "Số điện thoại";
            this.soDienThoaiDataGridViewTextBoxColumn.Name = "soDienThoaiDataGridViewTextBoxColumn";
            this.soDienThoaiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diaChiDataGridViewTextBoxColumn
            // 
            this.diaChiDataGridViewTextBoxColumn.DataPropertyName = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.HeaderText = "Địa chỉ";
            this.diaChiDataGridViewTextBoxColumn.Name = "diaChiDataGridViewTextBoxColumn";
            this.diaChiDataGridViewTextBoxColumn.ReadOnly = true;
            this.diaChiDataGridViewTextBoxColumn.Width = 300;
            // 
            // MucDich
            // 
            this.MucDich.DataPropertyName = "MucDich";
            this.MucDich.HeaderText = "Mục đích";
            this.MucDich.Name = "MucDich";
            this.MucDich.ReadOnly = true;
            this.MucDich.Width = 200;
            // 
            // yeuCauDataGridViewTextBoxColumn
            // 
            this.yeuCauDataGridViewTextBoxColumn.DataPropertyName = "YeuCau";
            this.yeuCauDataGridViewTextBoxColumn.HeaderText = "Phản hồi từ khách hàng";
            this.yeuCauDataGridViewTextBoxColumn.Name = "yeuCauDataGridViewTextBoxColumn";
            this.yeuCauDataGridViewTextBoxColumn.ReadOnly = true;
            this.yeuCauDataGridViewTextBoxColumn.Width = 250;
            // 
            // KetLuan
            // 
            this.KetLuan.DataPropertyName = "KetLuan";
            this.KetLuan.HeaderText = "Hướng giải quyết";
            this.KetLuan.Name = "KetLuan";
            this.KetLuan.ReadOnly = true;
            this.KetLuan.Width = 250;
            // 
            // NguoiTao
            // 
            this.NguoiTao.DataPropertyName = "NguoiTao";
            this.NguoiTao.HeaderText = "Nhân viên phụ trách";
            this.NguoiTao.Name = "NguoiTao";
            this.NguoiTao.ReadOnly = true;
            this.NguoiTao.Width = 200;
            // 
            // BacSiPhuTrach
            // 
            this.BacSiPhuTrach.DataPropertyName = "BacSiPhuTrach";
            this.BacSiPhuTrach.HeaderText = "Bác sĩ phụ trách";
            this.BacSiPhuTrach.Name = "BacSiPhuTrach";
            this.BacSiPhuTrach.ReadOnly = true;
            this.BacSiPhuTrach.Width = 200;
            // 
            // DaXong
            // 
            this.DaXong.DataPropertyName = "DaXongStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DaXong.DefaultCellStyle = dataGridViewCellStyle4;
            this.DaXong.HeaderText = "Trạng thái";
            this.DaXong.Name = "DaXong";
            this.DaXong.ReadOnly = true;
            this.DaXong.Width = 80;
            // 
            // nguonDataGridViewTextBoxColumn
            // 
            this.nguonDataGridViewTextBoxColumn.DataPropertyName = "Nguon";
            this.nguonDataGridViewTextBoxColumn.HeaderText = "Nguồn";
            this.nguonDataGridViewTextBoxColumn.Name = "nguonDataGridViewTextBoxColumn";
            this.nguonDataGridViewTextBoxColumn.ReadOnly = true;
            this.nguonDataGridViewTextBoxColumn.Width = 200;
            // 
            // INOUT
            // 
            this.INOUT.DataPropertyName = "InOut";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.INOUT.DefaultCellStyle = dataGridViewCellStyle5;
            this.INOUT.HeaderText = "IN/OUT";
            this.INOUT.Name = "INOUT";
            this.INOUT.ReadOnly = true;
            this.INOUT.Width = 50;
            // 
            // SoTongDai
            // 
            this.SoTongDai.DataPropertyName = "SoTongDai";
            this.SoTongDai.HeaderText = "Số tổng đài";
            this.SoTongDai.Name = "SoTongDai";
            this.SoTongDai.ReadOnly = true;
            // 
            // colBanThe
            // 
            this.colBanThe.DataPropertyName = "BanThe";
            this.colBanThe.HeaderText = "Bán thẻ";
            this.colBanThe.Name = "colBanThe";
            this.colBanThe.ReadOnly = true;
            this.colBanThe.Width = 80;
            // 
            // NguoiKetLuan
            // 
            this.NguoiKetLuan.DataPropertyName = "TenNguoiKetLuan";
            this.NguoiKetLuan.HeaderText = "Người kết luận";
            this.NguoiKetLuan.Name = "NguoiKetLuan";
            this.NguoiKetLuan.ReadOnly = true;
            this.NguoiKetLuan.Visible = false;
            this.NguoiKetLuan.Width = 200;
            // 
            // NguoiCapNhat
            // 
            this.NguoiCapNhat.DataPropertyName = "NguoiCapNhat";
            this.NguoiCapNhat.HeaderText = "Người cập nhật";
            this.NguoiCapNhat.Name = "NguoiCapNhat";
            this.NguoiCapNhat.ReadOnly = true;
            this.NguoiCapNhat.Visible = false;
            this.NguoiCapNhat.Width = 200;
            // 
            // uTuVanKhachHangList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uTuVanKhachHangList";
            this.Size = new System.Drawing.Size(1188, 613);
            this.Load += new System.EventHandler(this.uYKienKhachHangList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgYKienKhachHang)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.yKienKhachHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgYKienKhachHang;
        private System.Windows.Forms.BindingSource yKienKhachHangBindingSource;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.TextBox txtTenNguoiTao;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.CheckBox chkTuNgay;
        private System.Windows.Forms.CheckBox chkDenNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboINOUT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.Label lbSoDongChon;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenCongTy;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenKhachHangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoaiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MucDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn yeuCauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KetLuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacSiPhuTrach;
        private System.Windows.Forms.DataGridViewTextBoxColumn DaXong;
        private System.Windows.Forms.DataGridViewTextBoxColumn nguonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn INOUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTongDai;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colBanThe;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiKetLuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiCapNhat;
    }
}
