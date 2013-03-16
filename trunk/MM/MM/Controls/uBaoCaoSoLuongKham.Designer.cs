namespace MM.Controls
{
    partial class uBaoCaoSoLuongKham
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkMaBenhNhan = new System.Windows.Forms.CheckBox();
            this.raChuaDenKham = new System.Windows.Forms.RadioButton();
            this.raDenKham = new System.Windows.Forms.RadioButton();
            this.txtMaBenhNhan = new System.Windows.Forms.TextBox();
            this.lbKetQua = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgBenhNhan = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.PatientGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayKham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DobStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBenhNhan)).BeginInit();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkMaBenhNhan);
            this.panel2.Controls.Add(this.raChuaDenKham);
            this.panel2.Controls.Add(this.raDenKham);
            this.panel2.Controls.Add(this.txtMaBenhNhan);
            this.panel2.Controls.Add(this.lbKetQua);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpkToDate);
            this.panel2.Controls.Add(this.dtpkFromDate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 110);
            this.panel2.TabIndex = 5;
            // 
            // chkMaBenhNhan
            // 
            this.chkMaBenhNhan.AutoSize = true;
            this.chkMaBenhNhan.Checked = true;
            this.chkMaBenhNhan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaBenhNhan.Location = new System.Drawing.Point(384, 35);
            this.chkMaBenhNhan.Name = "chkMaBenhNhan";
            this.chkMaBenhNhan.Size = new System.Drawing.Size(122, 17);
            this.chkMaBenhNhan.TabIndex = 22;
            this.chkMaBenhNhan.Text = "Theo mã bệnh nhân";
            this.chkMaBenhNhan.UseVisualStyleBackColor = true;
            this.chkMaBenhNhan.CheckedChanged += new System.EventHandler(this.chkMaBenhNhan_CheckedChanged);
            // 
            // raChuaDenKham
            // 
            this.raChuaDenKham.AutoSize = true;
            this.raChuaDenKham.Location = new System.Drawing.Point(170, 57);
            this.raChuaDenKham.Name = "raChuaDenKham";
            this.raChuaDenKham.Size = new System.Drawing.Size(101, 17);
            this.raChuaDenKham.TabIndex = 21;
            this.raChuaDenKham.Text = "Chưa đến khám";
            this.raChuaDenKham.UseVisualStyleBackColor = true;
            this.raChuaDenKham.CheckedChanged += new System.EventHandler(this.raChuaDenKham_CheckedChanged);
            // 
            // raDenKham
            // 
            this.raDenKham.AutoSize = true;
            this.raDenKham.Checked = true;
            this.raDenKham.Location = new System.Drawing.Point(77, 57);
            this.raDenKham.Name = "raDenKham";
            this.raDenKham.Size = new System.Drawing.Size(74, 17);
            this.raDenKham.TabIndex = 20;
            this.raDenKham.TabStop = true;
            this.raDenKham.Text = "Đến khám";
            this.raDenKham.UseVisualStyleBackColor = true;
            this.raDenKham.CheckedChanged += new System.EventHandler(this.raDenKham_CheckedChanged);
            // 
            // txtMaBenhNhan
            // 
            this.txtMaBenhNhan.Location = new System.Drawing.Point(77, 33);
            this.txtMaBenhNhan.Name = "txtMaBenhNhan";
            this.txtMaBenhNhan.Size = new System.Drawing.Size(301, 20);
            this.txtMaBenhNhan.TabIndex = 4;
            // 
            // lbKetQua
            // 
            this.lbKetQua.AutoSize = true;
            this.lbKetQua.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQua.Location = new System.Drawing.Point(167, 85);
            this.lbKetQua.Name = "lbKetQua";
            this.lbKetQua.Size = new System.Drawing.Size(123, 13);
            this.lbKetQua.TabIndex = 19;
            this.lbKetQua.Text = "Kết quả được tìm thấy: 0";
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(77, 80);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bệnh nhân:";
            // 
            // dtpkToDate
            // 
            this.dtpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(264, 10);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(114, 20);
            this.dtpkToDate.TabIndex = 3;
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(77, 10);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(114, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnPrintPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 395);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 36);
            this.panel1.TabIndex = 6;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(173, 5);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 81;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(104, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 80;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(7, 5);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 79;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgBenhNhan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 110);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(833, 285);
            this.panel3.TabIndex = 7;
            // 
            // dgBenhNhan
            // 
            this.dgBenhNhan.AllowUserToAddRows = false;
            this.dgBenhNhan.AllowUserToDeleteRows = false;
            this.dgBenhNhan.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBenhNhan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBenhNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBenhNhan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientGUID,
            this.NgayKham,
            this.FileNum,
            this.FullName,
            this.DobStr,
            this.GenderAsStr,
            this.colMobile,
            this.Address});
            this.dgBenhNhan.ContextMenuStrip = this.ctmAction;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBenhNhan.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgBenhNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBenhNhan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgBenhNhan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBenhNhan.HighlightSelectedColumnHeaders = false;
            this.dgBenhNhan.Location = new System.Drawing.Point(0, 0);
            this.dgBenhNhan.MultiSelect = false;
            this.dgBenhNhan.Name = "dgBenhNhan";
            this.dgBenhNhan.ReadOnly = true;
            this.dgBenhNhan.RowHeadersWidth = 30;
            this.dgBenhNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBenhNhan.Size = new System.Drawing.Size(833, 285);
            this.dgBenhNhan.TabIndex = 5;
            // 
            // PatientGUID
            // 
            this.PatientGUID.DataPropertyName = "PatientGUID";
            this.PatientGUID.HeaderText = "PatientGUID";
            this.PatientGUID.Name = "PatientGUID";
            this.PatientGUID.ReadOnly = true;
            this.PatientGUID.Visible = false;
            // 
            // NgayKham
            // 
            this.NgayKham.DataPropertyName = "NgayKham";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.NgayKham.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayKham.HeaderText = "Ngày khám";
            this.NgayKham.Name = "NgayKham";
            this.NgayKham.ReadOnly = true;
            // 
            // FileNum
            // 
            this.FileNum.DataPropertyName = "FileNum";
            this.FileNum.HeaderText = "Mã bệnh nhân";
            this.FileNum.Name = "FileNum";
            this.FileNum.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Tên bệnh nhân";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 200;
            // 
            // DobStr
            // 
            this.DobStr.DataPropertyName = "DobStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DobStr.DefaultCellStyle = dataGridViewCellStyle3;
            this.DobStr.HeaderText = "Ngày sinh";
            this.DobStr.Name = "DobStr";
            this.DobStr.ReadOnly = true;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle4;
            this.GenderAsStr.HeaderText = "Giới tính";
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            this.GenderAsStr.Width = 80;
            // 
            // colMobile
            // 
            this.colMobile.DataPropertyName = "Mobile";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMobile.DefaultCellStyle = dataGridViewCellStyle5;
            this.colMobile.HeaderText = "ĐTDĐ";
            this.colMobile.Name = "colMobile";
            this.colMobile.ReadOnly = true;
            this.colMobile.Width = 120;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Địa chỉ";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 250;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.toolStripSeparator5,
            this.exportExcelToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 104);
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
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
            // 
            // uBaoCaoSoLuongKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "uBaoCaoSoLuongKham";
            this.Size = new System.Drawing.Size(833, 431);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBenhNhan)).EndInit();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtMaBenhNhan;
        private System.Windows.Forms.Label lbKetQua;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgBenhNhan;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.RadioButton raChuaDenKham;
        private System.Windows.Forms.RadioButton raDenKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DobStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkMaBenhNhan;
    }
}
