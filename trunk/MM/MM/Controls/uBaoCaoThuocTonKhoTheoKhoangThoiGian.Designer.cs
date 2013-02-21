namespace MM.Controls
{
    partial class uBaoCaoThuocTonKhoTheoKhoangThoiGian
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabReport = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.dgThuocTonKho = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tenThuocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLTonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spThuocTonKhoResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pageBaoCao = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this._uThuocList = new MM.Controls.uThuocList();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.pageFilter = new DevComponents.DotNetBar.TabItem(this.components);
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tabReport)).BeginInit();
            this.tabReport.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgThuocTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spThuocTonKhoResultBindingSource)).BeginInit();
            this.tabControlPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabReport
            // 
            this.tabReport.CanReorderTabs = true;
            this.tabReport.Controls.Add(this.tabControlPanel1);
            this.tabReport.Controls.Add(this.tabControlPanel2);
            this.tabReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReport.Location = new System.Drawing.Point(0, 0);
            this.tabReport.Name = "tabReport";
            this.tabReport.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabReport.SelectedTabIndex = 0;
            this.tabReport.Size = new System.Drawing.Size(768, 485);
            this.tabReport.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabReport.TabIndex = 0;
            this.tabReport.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabReport.Tabs.Add(this.pageFilter);
            this.tabReport.Tabs.Add(this.pageBaoCao);
            this.tabReport.Text = "tabControl1";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.dgThuocTonKho);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(768, 460);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.pageBaoCao;
            // 
            // dgThuocTonKho
            // 
            this.dgThuocTonKho.AllowUserToAddRows = false;
            this.dgThuocTonKho.AllowUserToDeleteRows = false;
            this.dgThuocTonKho.AllowUserToOrderColumns = true;
            this.dgThuocTonKho.AutoGenerateColumns = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgThuocTonKho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgThuocTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgThuocTonKho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tenThuocDataGridViewTextBoxColumn,
            this.donViTinhDataGridViewTextBoxColumn,
            this.soDuDataGridViewTextBoxColumn,
            this.sLNhapDataGridViewTextBoxColumn,
            this.sLXuatDataGridViewTextBoxColumn,
            this.sLTonDataGridViewTextBoxColumn});
            this.dgThuocTonKho.DataSource = this.spThuocTonKhoResultBindingSource;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgThuocTonKho.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgThuocTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgThuocTonKho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgThuocTonKho.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgThuocTonKho.HighlightSelectedColumnHeaders = false;
            this.dgThuocTonKho.Location = new System.Drawing.Point(1, 1);
            this.dgThuocTonKho.MultiSelect = false;
            this.dgThuocTonKho.Name = "dgThuocTonKho";
            this.dgThuocTonKho.ReadOnly = true;
            this.dgThuocTonKho.RowHeadersWidth = 30;
            this.dgThuocTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgThuocTonKho.Size = new System.Drawing.Size(766, 458);
            this.dgThuocTonKho.TabIndex = 4;
            // 
            // tenThuocDataGridViewTextBoxColumn
            // 
            this.tenThuocDataGridViewTextBoxColumn.DataPropertyName = "TenThuoc";
            this.tenThuocDataGridViewTextBoxColumn.HeaderText = "Tên thuốc";
            this.tenThuocDataGridViewTextBoxColumn.Name = "tenThuocDataGridViewTextBoxColumn";
            this.tenThuocDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenThuocDataGridViewTextBoxColumn.Width = 200;
            // 
            // donViTinhDataGridViewTextBoxColumn
            // 
            this.donViTinhDataGridViewTextBoxColumn.DataPropertyName = "DonViTinh";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.donViTinhDataGridViewTextBoxColumn.HeaderText = "ĐVT";
            this.donViTinhDataGridViewTextBoxColumn.Name = "donViTinhDataGridViewTextBoxColumn";
            this.donViTinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soDuDataGridViewTextBoxColumn
            // 
            this.soDuDataGridViewTextBoxColumn.DataPropertyName = "SoDu";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.soDuDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.soDuDataGridViewTextBoxColumn.HeaderText = "Sô dư";
            this.soDuDataGridViewTextBoxColumn.Name = "soDuDataGridViewTextBoxColumn";
            this.soDuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sLNhapDataGridViewTextBoxColumn
            // 
            this.sLNhapDataGridViewTextBoxColumn.DataPropertyName = "SLNhap";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.sLNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.sLNhapDataGridViewTextBoxColumn.HeaderText = "Số lượng nhập";
            this.sLNhapDataGridViewTextBoxColumn.Name = "sLNhapDataGridViewTextBoxColumn";
            this.sLNhapDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sLXuatDataGridViewTextBoxColumn
            // 
            this.sLXuatDataGridViewTextBoxColumn.DataPropertyName = "SLXuat";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            this.sLXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.sLXuatDataGridViewTextBoxColumn.HeaderText = "Số lượng xuất";
            this.sLXuatDataGridViewTextBoxColumn.Name = "sLXuatDataGridViewTextBoxColumn";
            this.sLXuatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sLTonDataGridViewTextBoxColumn
            // 
            this.sLTonDataGridViewTextBoxColumn.DataPropertyName = "SLTon";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = null;
            this.sLTonDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.sLTonDataGridViewTextBoxColumn.HeaderText = "Số lượng tồn";
            this.sLTonDataGridViewTextBoxColumn.Name = "sLTonDataGridViewTextBoxColumn";
            this.sLTonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // spThuocTonKhoResultBindingSource
            // 
            this.spThuocTonKhoResultBindingSource.DataSource = typeof(MM.Databasae.spThuocTonKhoResult);
            // 
            // pageBaoCao
            // 
            this.pageBaoCao.AttachedControl = this.tabControlPanel2;
            this.pageBaoCao.Image = global::MM.Properties.Resources.product_sales_report_icon;
            this.pageBaoCao.Name = "pageBaoCao";
            this.pageBaoCao.Text = "Báo cáo";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.ContextMenuStrip = this.ctmAction;
            this.tabControlPanel1.Controls.Add(this.panel3);
            this.tabControlPanel1.Controls.Add(this.panel2);
            this.tabControlPanel1.Controls.Add(this.panel1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(768, 460);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.pageFilter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._uThuocList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(766, 386);
            this.panel3.TabIndex = 3;
            // 
            // _uThuocList
            // 
            this._uThuocList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uThuocList.IsReport = true;
            this._uThuocList.Location = new System.Drawing.Point(0, 0);
            this._uThuocList.Name = "_uThuocList";
            this._uThuocList.Size = new System.Drawing.Size(766, 386);
            this._uThuocList.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(1, 423);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 36);
            this.panel2.TabIndex = 2;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(85, 5);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 76;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(6, 5);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 25);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 36);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Từ ngày:";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(73, 9);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(106, 20);
            this.dtpkTuNgay.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Đến ngày:";
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(247, 9);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(106, 20);
            this.dtpkDenNgay.TabIndex = 28;
            // 
            // pageFilter
            // 
            this.pageFilter.AttachedControl = this.tabControlPanel1;
            this.pageFilter.Image = global::MM.Properties.Resources.filter_icon;
            this.pageFilter.Name = "pageFilter";
            this.pageFilter.Text = "Điều kiện xem báo cáo";
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportExcelToolStripMenuItem});
            this.ctmAction.Name = "ctmAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 76);
            // 
            // xemToolStripMenuItem
            // 
            this.xemToolStripMenuItem.Image = global::MM.Properties.Resources.views_icon;
            this.xemToolStripMenuItem.Name = "xemToolStripMenuItem";
            this.xemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xemToolStripMenuItem.Text = "Xem";
            this.xemToolStripMenuItem.Click += new System.EventHandler(this.xemToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Image = global::MM.Properties.Resources.page_excel_icon;
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportExcelToolStripMenuItem.Text = "Xuất Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // uBaoCaoThuocTonKhoTheoKhoangThoiGian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabReport);
            this.Name = "uBaoCaoThuocTonKhoTheoKhoangThoiGian";
            this.Size = new System.Drawing.Size(768, 485);
            ((System.ComponentModel.ISupportInitialize)(this.tabReport)).EndInit();
            this.tabReport.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgThuocTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spThuocTonKhoResultBindingSource)).EndInit();
            this.tabControlPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabReport;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem pageFilter;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pageBaoCao;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private uThuocList _uThuocList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgThuocTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenThuocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLTonDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource spThuocTonKhoResultBindingSource;
        private System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem xemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
    }
}
