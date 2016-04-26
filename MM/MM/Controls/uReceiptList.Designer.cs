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
    partial class uReceiptList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uReceiptList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkTongTien = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.raChuaThuTien = new System.Windows.Forms.RadioButton();
            this.raDaThuTien = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raDaXoa = new System.Windows.Forms.RadioButton();
            this.raChuaXoa = new System.Windows.Forms.RadioButton();
            this.raTatCa = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.raTenBenhNhan = new System.Windows.Forms.RadioButton();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.raTuNgayToiNgay = new System.Windows.Forms.RadioButton();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgReceipt = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ReceiptCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiptDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LyDoGiam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsExportedInVoice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DaThuTien = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TrongGoiKham = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HinhThucThanhToanStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xuatHoaDonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGhiNhanTraNo = new System.Windows.Forms.Button();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ghiNhanTraNoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipt)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receiptViewBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGhiNhanTraNo);
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnExportInvoice);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1252, 38);
            this.panel1.TabIndex = 1;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(188, 6);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 15;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Image = global::MM.Properties.Resources.invoice_icon;
            this.btnExportInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportInvoice.Location = new System.Drawing.Point(285, 6);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(106, 25);
            this.btnExportInvoice.TabIndex = 16;
            this.btnExportInvoice.Text = "      &Xuất hóa đơn";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            this.btnExportInvoice.Click += new System.EventHandler(this.btnExportInvoice_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(87, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 25);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "      &In phiếu thu";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(8, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkTongTien);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.txtTenBenhNhan);
            this.panel2.Controls.Add(this.raTenBenhNhan);
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.raTuNgayToiNgay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1252, 130);
            this.panel2.TabIndex = 2;
            // 
            // chkTongTien
            // 
            this.chkTongTien.AutoSize = true;
            this.chkTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTongTien.ForeColor = System.Drawing.Color.Red;
            this.chkTongTien.Location = new System.Drawing.Point(609, 99);
            this.chkTongTien.Name = "chkTongTien";
            this.chkTongTien.Size = new System.Drawing.Size(84, 17);
            this.chkTongTien.TabIndex = 17;
            this.chkTongTien.Text = "Tổng tiền:";
            this.chkTongTien.UseVisualStyleBackColor = true;
            this.chkTongTien.CheckedChanged += new System.EventHandler(this.chkTongTien_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.raChuaThuTien);
            this.groupBox2.Controls.Add(this.raDaThuTien);
            this.groupBox2.Controls.Add(this.raAll);
            this.groupBox2.Location = new System.Drawing.Point(16, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 32);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // raChuaThuTien
            // 
            this.raChuaThuTien.AutoSize = true;
            this.raChuaThuTien.Location = new System.Drawing.Point(261, 10);
            this.raChuaThuTien.Name = "raChuaThuTien";
            this.raChuaThuTien.Size = new System.Drawing.Size(88, 17);
            this.raChuaThuTien.TabIndex = 3;
            this.raChuaThuTien.Text = "Chưa thu tiền";
            this.raChuaThuTien.UseVisualStyleBackColor = true;
            this.raChuaThuTien.CheckedChanged += new System.EventHandler(this.raChuaThuTien_CheckedChanged);
            // 
            // raDaThuTien
            // 
            this.raDaThuTien.AutoSize = true;
            this.raDaThuTien.Location = new System.Drawing.Point(129, 10);
            this.raDaThuTien.Name = "raDaThuTien";
            this.raDaThuTien.Size = new System.Drawing.Size(77, 17);
            this.raDaThuTien.TabIndex = 2;
            this.raDaThuTien.Text = "Đã thu tiền";
            this.raDaThuTien.UseVisualStyleBackColor = true;
            this.raDaThuTien.CheckedChanged += new System.EventHandler(this.raDaThuTien_CheckedChanged);
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(20, 10);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 1;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(473, 102);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 15;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raDaXoa);
            this.groupBox1.Controls.Add(this.raChuaXoa);
            this.groupBox1.Controls.Add(this.raTatCa);
            this.groupBox1.Location = new System.Drawing.Point(16, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 32);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // raDaXoa
            // 
            this.raDaXoa.AutoSize = true;
            this.raDaXoa.Location = new System.Drawing.Point(261, 10);
            this.raDaXoa.Name = "raDaXoa";
            this.raDaXoa.Size = new System.Drawing.Size(59, 17);
            this.raDaXoa.TabIndex = 3;
            this.raDaXoa.Text = "Đã xóa";
            this.raDaXoa.UseVisualStyleBackColor = true;
            this.raDaXoa.CheckedChanged += new System.EventHandler(this.raDaXoa_CheckedChanged);
            // 
            // raChuaXoa
            // 
            this.raChuaXoa.AutoSize = true;
            this.raChuaXoa.Checked = true;
            this.raChuaXoa.Location = new System.Drawing.Point(129, 10);
            this.raChuaXoa.Name = "raChuaXoa";
            this.raChuaXoa.Size = new System.Drawing.Size(70, 17);
            this.raChuaXoa.TabIndex = 2;
            this.raChuaXoa.TabStop = true;
            this.raChuaXoa.Text = "Chưa xóa";
            this.raChuaXoa.UseVisualStyleBackColor = true;
            this.raChuaXoa.CheckedChanged += new System.EventHandler(this.raChuaXoa_CheckedChanged);
            // 
            // raTatCa
            // 
            this.raTatCa.AutoSize = true;
            this.raTatCa.Location = new System.Drawing.Point(20, 10);
            this.raTatCa.Name = "raTatCa";
            this.raTatCa.Size = new System.Drawing.Size(56, 17);
            this.raTatCa.TabIndex = 1;
            this.raTatCa.Text = "Tất cả";
            this.raTatCa.UseVisualStyleBackColor = true;
            this.raTatCa.CheckedChanged += new System.EventHandler(this.raTatCa_CheckedChanged);
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(381, 97);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(120, 34);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.Size = new System.Drawing.Size(255, 20);
            this.txtTenBenhNhan.TabIndex = 5;
            this.txtTenBenhNhan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTenBenhNhan_KeyDown);
            // 
            // raTenBenhNhan
            // 
            this.raTenBenhNhan.AutoSize = true;
            this.raTenBenhNhan.Location = new System.Drawing.Point(16, 35);
            this.raTenBenhNhan.Name = "raTenBenhNhan";
            this.raTenBenhNhan.Size = new System.Drawing.Size(98, 17);
            this.raTenBenhNhan.TabIndex = 4;
            this.raTenBenhNhan.Text = "Tên bệnh nhân";
            this.raTenBenhNhan.UseVisualStyleBackColor = true;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(262, 10);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(84, 10);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 1;
            // 
            // raTuNgayToiNgay
            // 
            this.raTuNgayToiNgay.AutoSize = true;
            this.raTuNgayToiNgay.Checked = true;
            this.raTuNgayToiNgay.Location = new System.Drawing.Point(16, 11);
            this.raTuNgayToiNgay.Name = "raTuNgayToiNgay";
            this.raTuNgayToiNgay.Size = new System.Drawing.Size(64, 17);
            this.raTuNgayToiNgay.TabIndex = 0;
            this.raTuNgayToiNgay.TabStop = true;
            this.raTuNgayToiNgay.Text = "Từ ngày";
            this.raTuNgayToiNgay.UseVisualStyleBackColor = true;
            this.raTuNgayToiNgay.CheckedChanged += new System.EventHandler(this.raTuNgayToiNgay_CheckedChanged);
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
            // dgReceipt
            // 
            this.dgReceipt.AllowUserToAddRows = false;
            this.dgReceipt.AllowUserToDeleteRows = false;
            this.dgReceipt.AllowUserToOrderColumns = true;
            this.dgReceipt.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ReceiptCode,
            this.receiptDateDataGridViewTextBoxColumn,
            this.FileNum,
            this.fullNameDataGridViewTextBoxColumn,
            this.Address,
            this.LyDoGiam,
            this.Notes,
            this.IsExportedInVoice,
            this.DaThuTien,
            this.TrongGoiKham,
            this.HinhThucThanhToanStr,
            this.NguoiTao});
            this.dgReceipt.ContextMenuStrip = this.ctmAction;
            this.dgReceipt.DataSource = this.receiptViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgReceipt.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReceipt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgReceipt.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgReceipt.HighlightSelectedColumnHeaders = false;
            this.dgReceipt.Location = new System.Drawing.Point(0, 0);
            this.dgReceipt.MultiSelect = false;
            this.dgReceipt.Name = "dgReceipt";
            this.dgReceipt.RowHeadersWidth = 30;
            this.dgReceipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgReceipt.Size = new System.Drawing.Size(1252, 287);
            this.dgReceipt.TabIndex = 2;
            this.dgReceipt.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgReceipt_ColumnHeaderMouseClick);
            this.dgReceipt.DoubleClick += new System.EventHandler(this.dgReceipt_DoubleClick);
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
            // ReceiptCode
            // 
            this.ReceiptCode.DataPropertyName = "ReceiptCode";
            this.ReceiptCode.HeaderText = "Mã phiếu thu";
            this.ReceiptCode.Name = "ReceiptCode";
            this.ReceiptCode.ReadOnly = true;
            // 
            // receiptDateDataGridViewTextBoxColumn
            // 
            this.receiptDateDataGridViewTextBoxColumn.DataPropertyName = "ReceiptDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.receiptDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.receiptDateDataGridViewTextBoxColumn.HeaderText = "Ngày thu";
            this.receiptDateDataGridViewTextBoxColumn.Name = "receiptDateDataGridViewTextBoxColumn";
            this.receiptDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FileNum
            // 
            this.FileNum.DataPropertyName = "FileNum";
            this.FileNum.HeaderText = "Mã bệnh nhân";
            this.FileNum.Name = "FileNum";
            this.FileNum.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Bệnh nhân";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Địa chỉ";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 250;
            // 
            // LyDoGiam
            // 
            this.LyDoGiam.DataPropertyName = "LyDoGiam";
            this.LyDoGiam.HeaderText = "Lý do giảm";
            this.LyDoGiam.Name = "LyDoGiam";
            this.LyDoGiam.ReadOnly = true;
            this.LyDoGiam.Width = 250;
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "Notes";
            this.Notes.HeaderText = "Ghi chú";
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            this.Notes.Width = 250;
            // 
            // IsExportedInVoice
            // 
            this.IsExportedInVoice.DataPropertyName = "IsExportedInVoice";
            this.IsExportedInVoice.HeaderText = "Đã xuất HĐ";
            this.IsExportedInVoice.Name = "IsExportedInVoice";
            this.IsExportedInVoice.ReadOnly = true;
            this.IsExportedInVoice.Width = 80;
            // 
            // DaThuTien
            // 
            this.DaThuTien.DataPropertyName = "DaThuTien";
            this.DaThuTien.HeaderText = "Đã thu tiền";
            this.DaThuTien.Name = "DaThuTien";
            this.DaThuTien.ReadOnly = true;
            this.DaThuTien.Width = 80;
            // 
            // TrongGoiKham
            // 
            this.TrongGoiKham.DataPropertyName = "TrongGoiKham";
            this.TrongGoiKham.HeaderText = "Trong gói khám";
            this.TrongGoiKham.Name = "TrongGoiKham";
            this.TrongGoiKham.ReadOnly = true;
            // 
            // HinhThucThanhToanStr
            // 
            this.HinhThucThanhToanStr.DataPropertyName = "HinhThucThanhToanStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.HinhThucThanhToanStr.DefaultCellStyle = dataGridViewCellStyle3;
            this.HinhThucThanhToanStr.HeaderText = "Hình thức thanh toán";
            this.HinhThucThanhToanStr.Name = "HinhThucThanhToanStr";
            this.HinhThucThanhToanStr.ReadOnly = true;
            this.HinhThucThanhToanStr.Width = 130;
            // 
            // NguoiTao
            // 
            this.NguoiTao.DataPropertyName = "NguoiTao";
            this.NguoiTao.HeaderText = "Người tạo";
            this.NguoiTao.Name = "NguoiTao";
            this.NguoiTao.ReadOnly = true;
            this.NguoiTao.Width = 200;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.printToolStripMenuItem,
            this.toolStripSeparator5,
            this.exportExcelToolStripMenuItem,
            this.toolStripSeparator1,
            this.xuatHoaDonToolStripMenuItem,
            this.toolStripSeparator2,
            this.ghiNhanTraNoToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(157, 160);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.printToolStripMenuItem.Text = "In phiếu thu";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(153, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Image = global::MM.Properties.Resources.page_excel_icon;
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exportExcelToolStripMenuItem.Text = "Xuất Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // xuatHoaDonToolStripMenuItem
            // 
            this.xuatHoaDonToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon;
            this.xuatHoaDonToolStripMenuItem.Name = "xuatHoaDonToolStripMenuItem";
            this.xuatHoaDonToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.xuatHoaDonToolStripMenuItem.Text = "Xuất hóa đơn";
            this.xuatHoaDonToolStripMenuItem.Click += new System.EventHandler(this.xuatHoaDonToolStripMenuItem_Click);
            // 
            // receiptViewBindingSource
            // 
            this.receiptViewBindingSource.DataSource = typeof(MM.Databasae.ReceiptView);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgReceipt);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 130);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1252, 287);
            this.panel3.TabIndex = 4;
            // 
            // btnGhiNhanTraNo
            // 
            this.btnGhiNhanTraNo.Image = ((System.Drawing.Image)(resources.GetObject("btnGhiNhanTraNo.Image")));
            this.btnGhiNhanTraNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGhiNhanTraNo.Location = new System.Drawing.Point(395, 6);
            this.btnGhiNhanTraNo.Name = "btnGhiNhanTraNo";
            this.btnGhiNhanTraNo.Size = new System.Drawing.Size(106, 25);
            this.btnGhiNhanTraNo.TabIndex = 17;
            this.btnGhiNhanTraNo.Text = "      &Ghi nhận trả nợ";
            this.btnGhiNhanTraNo.UseVisualStyleBackColor = true;
            this.btnGhiNhanTraNo.Click += new System.EventHandler(this.btnGhiNhanTraNo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // ghiNhanTraNoToolStripMenuItem
            // 
            this.ghiNhanTraNoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ghiNhanTraNoToolStripMenuItem.Image")));
            this.ghiNhanTraNoToolStripMenuItem.Name = "ghiNhanTraNoToolStripMenuItem";
            this.ghiNhanTraNoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ghiNhanTraNoToolStripMenuItem.Text = "Ghi nhận trả nợ";
            this.ghiNhanTraNoToolStripMenuItem.Click += new System.EventHandler(this.ghiNhanTraNoToolStripMenuItem_Click);
            // 
            // uReceiptList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uReceiptList";
            this.Size = new System.Drawing.Size(1252, 455);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipt)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.receiptViewBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgReceipt;
        private System.Windows.Forms.BindingSource receiptViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn promotionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paymentDataGridViewTextBoxColumn;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.Button btnExportInvoice;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.RadioButton raTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.RadioButton raTuNgayToiNgay;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raDaXoa;
        private System.Windows.Forms.RadioButton raChuaXoa;
        private System.Windows.Forms.RadioButton raTatCa;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem xuatHoaDonToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton raChuaThuTien;
        private System.Windows.Forms.RadioButton raDaThuTien;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiptDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn LyDoGiam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExportedInVoice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DaThuTien;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TrongGoiKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn HinhThucThanhToanStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiTao;
        private System.Windows.Forms.CheckBox chkTongTien;
        private System.Windows.Forms.Button btnGhiNhanTraNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ghiNhanTraNoToolStripMenuItem;
    }
}
