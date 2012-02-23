namespace MM.Controls
{
    partial class uKetQuaNoiSoiList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
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
            this.dgKhamNoiSoi = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.soPhieuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayKhamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenBacSiChiDinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lyDoKhamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketLuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deNghiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loaiNoiSoiStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketQuaNoiSoiViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.pFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKhamNoiSoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaNoiSoiViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pFilter
            // 
            this.pFilter.Controls.Add(this.label1);
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.dtpkToDate);
            this.pFilter.Controls.Add(this.lbToDate);
            this.pFilter.Controls.Add(this.dtpkFromDate);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(722, 42);
            this.pFilter.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Từ ngày";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(325, 10);
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
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(223, 10);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(166, 13);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(53, 13);
            this.lbToDate.TabIndex = 3;
            this.lbToDate.Text = "Đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(65, 10);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
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
            this.panel2.Location = new System.Drawing.Point(0, 423);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(722, 37);
            this.panel2.TabIndex = 14;
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
            this.panel1.Controls.Add(this.dgKhamNoiSoi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 381);
            this.panel1.TabIndex = 15;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 10;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgKhamNoiSoi
            // 
            this.dgKhamNoiSoi.AllowUserToAddRows = false;
            this.dgKhamNoiSoi.AllowUserToDeleteRows = false;
            this.dgKhamNoiSoi.AllowUserToOrderColumns = true;
            this.dgKhamNoiSoi.AutoGenerateColumns = false;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgKhamNoiSoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dgKhamNoiSoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKhamNoiSoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.soPhieuDataGridViewTextBoxColumn,
            this.ngayKhamDataGridViewTextBoxColumn,
            this.tenBacSiChiDinhDataGridViewTextBoxColumn,
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn,
            this.lyDoKhamDataGridViewTextBoxColumn,
            this.ketLuanDataGridViewTextBoxColumn,
            this.deNghiDataGridViewTextBoxColumn,
            this.loaiNoiSoiStrDataGridViewTextBoxColumn});
            this.dgKhamNoiSoi.DataSource = this.ketQuaNoiSoiViewBindingSource;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgKhamNoiSoi.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgKhamNoiSoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKhamNoiSoi.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgKhamNoiSoi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgKhamNoiSoi.HighlightSelectedColumnHeaders = false;
            this.dgKhamNoiSoi.Location = new System.Drawing.Point(0, 0);
            this.dgKhamNoiSoi.MultiSelect = false;
            this.dgKhamNoiSoi.Name = "dgKhamNoiSoi";
            this.dgKhamNoiSoi.ReadOnly = true;
            this.dgKhamNoiSoi.RowHeadersWidth = 30;
            this.dgKhamNoiSoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgKhamNoiSoi.Size = new System.Drawing.Size(722, 381);
            this.dgKhamNoiSoi.TabIndex = 9;
            this.dgKhamNoiSoi.DoubleClick += new System.EventHandler(this.dgKhamNoiSoi_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle26;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // soPhieuDataGridViewTextBoxColumn
            // 
            this.soPhieuDataGridViewTextBoxColumn.DataPropertyName = "SoPhieu";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.soPhieuDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle27;
            this.soPhieuDataGridViewTextBoxColumn.HeaderText = "Số phiếu";
            this.soPhieuDataGridViewTextBoxColumn.Name = "soPhieuDataGridViewTextBoxColumn";
            this.soPhieuDataGridViewTextBoxColumn.ReadOnly = true;
            this.soPhieuDataGridViewTextBoxColumn.Width = 120;
            // 
            // ngayKhamDataGridViewTextBoxColumn
            // 
            this.ngayKhamDataGridViewTextBoxColumn.DataPropertyName = "NgayKham";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.Format = "d";
            dataGridViewCellStyle28.NullValue = "dd/MM/yyyy";
            this.ngayKhamDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle28;
            this.ngayKhamDataGridViewTextBoxColumn.HeaderText = "Ngày khám";
            this.ngayKhamDataGridViewTextBoxColumn.Name = "ngayKhamDataGridViewTextBoxColumn";
            this.ngayKhamDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenBacSiChiDinhDataGridViewTextBoxColumn
            // 
            this.tenBacSiChiDinhDataGridViewTextBoxColumn.DataPropertyName = "TenBacSiChiDinh";
            this.tenBacSiChiDinhDataGridViewTextBoxColumn.HeaderText = "Bác sĩ chỉ định";
            this.tenBacSiChiDinhDataGridViewTextBoxColumn.Name = "tenBacSiChiDinhDataGridViewTextBoxColumn";
            this.tenBacSiChiDinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenBacSiChiDinhDataGridViewTextBoxColumn.Width = 200;
            // 
            // tenBacSiNoiSoiDataGridViewTextBoxColumn
            // 
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn.DataPropertyName = "TenBacSiNoiSoi";
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn.HeaderText = "Bác sĩ soi";
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn.Name = "tenBacSiNoiSoiDataGridViewTextBoxColumn";
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenBacSiNoiSoiDataGridViewTextBoxColumn.Width = 200;
            // 
            // lyDoKhamDataGridViewTextBoxColumn
            // 
            this.lyDoKhamDataGridViewTextBoxColumn.DataPropertyName = "LyDoKham";
            this.lyDoKhamDataGridViewTextBoxColumn.HeaderText = "Lý do khám";
            this.lyDoKhamDataGridViewTextBoxColumn.Name = "lyDoKhamDataGridViewTextBoxColumn";
            this.lyDoKhamDataGridViewTextBoxColumn.ReadOnly = true;
            this.lyDoKhamDataGridViewTextBoxColumn.Width = 200;
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
            // loaiNoiSoiStrDataGridViewTextBoxColumn
            // 
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.DataPropertyName = "LoaiNoiSoiStr";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle29;
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.HeaderText = "Loại nội soi";
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.Name = "loaiNoiSoiStrDataGridViewTextBoxColumn";
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.loaiNoiSoiStrDataGridViewTextBoxColumn.Width = 120;
            // 
            // ketQuaNoiSoiViewBindingSource
            // 
            this.ketQuaNoiSoiViewBindingSource.DataSource = typeof(MM.Databasae.KetQuaNoiSoiView);
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
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
            // 
            // uKetQuaNoiSoiList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uKetQuaNoiSoiList";
            this.Size = new System.Drawing.Size(722, 460);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKhamNoiSoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaNoiSoiViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Label label1;
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
        private DevComponents.DotNetBar.Controls.DataGridViewX dgKhamNoiSoi;
        private System.Windows.Forms.BindingSource ketQuaNoiSoiViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn soPhieuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKhamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenBacSiChiDinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenBacSiNoiSoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lyDoKhamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ketLuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deNghiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loaiNoiSoiStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.PrintDialog _printDialog;
    }
}
