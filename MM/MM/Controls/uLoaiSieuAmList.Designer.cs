namespace MM.Controls
{
    partial class uLoaiSieuAmList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uLoaiSieuAmList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgLoaiSieuAm = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tenSieuAmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thuTuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loaiSieuAmBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tabMauBaoCao = new System.Windows.Forms.TabControl();
            this.ctmAction2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._page1 = new System.Windows.Forms.TabPage();
            this._textControl1 = new TXTextControl.TextControl();
            this.panel9 = new System.Windows.Forms.Panel();
            this.chkInTrang2 = new System.Windows.Forms.CheckBox();
            this.chkNu = new System.Windows.Forms.CheckBox();
            this.chkNam = new System.Windows.Forms.CheckBox();
            this.btnBrowse_Nu = new System.Windows.Forms.Button();
            this.txtMauBaoCao_Nu = new System.Windows.Forms.TextBox();
            this.btnBrowse_Nam = new System.Windows.Forms.Button();
            this.txtMauBaoCao_Nam = new System.Windows.Forms.TextBox();
            this.btnBrowse_Chung = new System.Windows.Forms.Button();
            this.txtMauBaoCao_Chung = new System.Windows.Forms.TextBox();
            this.raNamNu = new System.Windows.Forms.RadioButton();
            this.raChung = new System.Windows.Forms.RadioButton();
            this.numThuTu = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenSieuAm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExportWord = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoaiSieuAm)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loaiSieuAmBindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabMauBaoCao.SuspendLayout();
            this.ctmAction2.SuspendLayout();
            this._page1.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 620);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(314, 578);
            this.panel5.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chkChecked);
            this.panel7.Controls.Add(this.dgLoaiSieuAm);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 19);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(314, 559);
            this.panel7.TabIndex = 2;
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
            // dgLoaiSieuAm
            // 
            this.dgLoaiSieuAm.AllowUserToAddRows = false;
            this.dgLoaiSieuAm.AllowUserToDeleteRows = false;
            this.dgLoaiSieuAm.AllowUserToOrderColumns = true;
            this.dgLoaiSieuAm.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLoaiSieuAm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLoaiSieuAm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLoaiSieuAm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.tenSieuAmDataGridViewTextBoxColumn,
            this.thuTuDataGridViewTextBoxColumn});
            this.dgLoaiSieuAm.ContextMenuStrip = this.ctmAction;
            this.dgLoaiSieuAm.DataSource = this.loaiSieuAmBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLoaiSieuAm.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgLoaiSieuAm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLoaiSieuAm.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgLoaiSieuAm.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgLoaiSieuAm.HighlightSelectedColumnHeaders = false;
            this.dgLoaiSieuAm.Location = new System.Drawing.Point(0, 0);
            this.dgLoaiSieuAm.MultiSelect = false;
            this.dgLoaiSieuAm.Name = "dgLoaiSieuAm";
            this.dgLoaiSieuAm.RowHeadersWidth = 30;
            this.dgLoaiSieuAm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLoaiSieuAm.Size = new System.Drawing.Size(314, 559);
            this.dgLoaiSieuAm.TabIndex = 2;
            this.dgLoaiSieuAm.SelectionChanged += new System.EventHandler(this.dgLoaiSieuAm_SelectionChanged);
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
            // tenSieuAmDataGridViewTextBoxColumn
            // 
            this.tenSieuAmDataGridViewTextBoxColumn.DataPropertyName = "TenSieuAm";
            this.tenSieuAmDataGridViewTextBoxColumn.HeaderText = "Tên siêu âm";
            this.tenSieuAmDataGridViewTextBoxColumn.Name = "tenSieuAmDataGridViewTextBoxColumn";
            this.tenSieuAmDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenSieuAmDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tenSieuAmDataGridViewTextBoxColumn.Width = 180;
            // 
            // thuTuDataGridViewTextBoxColumn
            // 
            this.thuTuDataGridViewTextBoxColumn.DataPropertyName = "ThuTu";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.thuTuDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.thuTuDataGridViewTextBoxColumn.HeaderText = "Thứ tự";
            this.thuTuDataGridViewTextBoxColumn.Name = "thuTuDataGridViewTextBoxColumn";
            this.thuTuDataGridViewTextBoxColumn.ReadOnly = true;
            this.thuTuDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.thuTuDataGridViewTextBoxColumn.Width = 45;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(104, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // loaiSieuAmBindingSource
            // 
            this.loaiSieuAmBindingSource.DataSource = typeof(MM.Databasae.LoaiSieuAm);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Danh sách loại siêu âm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDelete);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 578);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(314, 38);
            this.panel4.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(5, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(318, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 620);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(321, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 620);
            this.panel2.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(676, 578);
            this.panel6.TabIndex = 5;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 19);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(676, 559);
            this.panel8.TabIndex = 3;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.tabMauBaoCao);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 138);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(676, 421);
            this.panel10.TabIndex = 1;
            // 
            // tabMauBaoCao
            // 
            this.tabMauBaoCao.ContextMenuStrip = this.ctmAction2;
            this.tabMauBaoCao.Controls.Add(this._page1);
            this.tabMauBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMauBaoCao.Location = new System.Drawing.Point(0, 0);
            this.tabMauBaoCao.Name = "tabMauBaoCao";
            this.tabMauBaoCao.SelectedIndex = 0;
            this.tabMauBaoCao.Size = new System.Drawing.Size(676, 421);
            this.tabMauBaoCao.TabIndex = 0;
            // 
            // ctmAction2
            // 
            this.ctmAction2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportWordToolStripMenuItem});
            this.ctmAction2.Name = "cmtAction";
            this.ctmAction2.Size = new System.Drawing.Size(153, 104);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Thêm mới";
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
            this.editToolStripMenuItem.Text = "Cập nhật";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exportWordToolStripMenuItem
            // 
            this.exportWordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportWordToolStripMenuItem.Image")));
            this.exportWordToolStripMenuItem.Name = "exportWordToolStripMenuItem";
            this.exportWordToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportWordToolStripMenuItem.Text = "Xuất Word";
            this.exportWordToolStripMenuItem.Click += new System.EventHandler(this.exportWordToolStripMenuItem_Click);
            // 
            // _page1
            // 
            this._page1.Controls.Add(this._textControl1);
            this._page1.Location = new System.Drawing.Point(4, 22);
            this._page1.Name = "_page1";
            this._page1.Size = new System.Drawing.Size(668, 395);
            this._page1.TabIndex = 0;
            this._page1.Text = "Mẫu báo cáo (Chung)";
            this._page1.UseVisualStyleBackColor = true;
            // 
            // _textControl1
            // 
            this._textControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textControl1.EditMode = TXTextControl.EditMode.ReadAndSelect;
            this._textControl1.Font = new System.Drawing.Font("Arial", 10F);
            this._textControl1.Location = new System.Drawing.Point(0, 0);
            this._textControl1.Name = "_textControl1";
            this._textControl1.PageMargins.Bottom = 79;
            this._textControl1.PageMargins.Left = 79;
            this._textControl1.PageMargins.Right = 79;
            this._textControl1.PageMargins.Top = 79;
            this._textControl1.Size = new System.Drawing.Size(668, 395);
            this._textControl1.TabIndex = 0;
            this._textControl1.ViewMode = TXTextControl.ViewMode.Normal;
            // 
            // panel9
            // 
            this.panel9.ContextMenuStrip = this.ctmAction2;
            this.panel9.Controls.Add(this.chkInTrang2);
            this.panel9.Controls.Add(this.chkNu);
            this.panel9.Controls.Add(this.chkNam);
            this.panel9.Controls.Add(this.btnBrowse_Nu);
            this.panel9.Controls.Add(this.txtMauBaoCao_Nu);
            this.panel9.Controls.Add(this.btnBrowse_Nam);
            this.panel9.Controls.Add(this.txtMauBaoCao_Nam);
            this.panel9.Controls.Add(this.btnBrowse_Chung);
            this.panel9.Controls.Add(this.txtMauBaoCao_Chung);
            this.panel9.Controls.Add(this.raNamNu);
            this.panel9.Controls.Add(this.raChung);
            this.panel9.Controls.Add(this.numThuTu);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.txtTenSieuAm);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(676, 138);
            this.panel9.TabIndex = 0;
            // 
            // chkInTrang2
            // 
            this.chkInTrang2.AutoSize = true;
            this.chkInTrang2.Location = new System.Drawing.Point(435, 12);
            this.chkInTrang2.Name = "chkInTrang2";
            this.chkInTrang2.Size = new System.Drawing.Size(71, 17);
            this.chkInTrang2.TabIndex = 4;
            this.chkInTrang2.Text = "In trang 2";
            this.chkInTrang2.UseVisualStyleBackColor = true;
            // 
            // chkNu
            // 
            this.chkNu.AutoSize = true;
            this.chkNu.Enabled = false;
            this.chkNu.Location = new System.Drawing.Point(95, 111);
            this.chkNu.Name = "chkNu";
            this.chkNu.Size = new System.Drawing.Size(43, 17);
            this.chkNu.TabIndex = 12;
            this.chkNu.Text = "Nữ:";
            this.chkNu.UseVisualStyleBackColor = true;
            this.chkNu.CheckedChanged += new System.EventHandler(this.chkNu_CheckedChanged);
            // 
            // chkNam
            // 
            this.chkNam.AutoSize = true;
            this.chkNam.Enabled = false;
            this.chkNam.Location = new System.Drawing.Point(95, 88);
            this.chkNam.Name = "chkNam";
            this.chkNam.Size = new System.Drawing.Size(51, 17);
            this.chkNam.TabIndex = 9;
            this.chkNam.Text = "Nam:";
            this.chkNam.UseVisualStyleBackColor = true;
            this.chkNam.CheckedChanged += new System.EventHandler(this.chkNam_CheckedChanged);
            // 
            // btnBrowse_Nu
            // 
            this.btnBrowse_Nu.Enabled = false;
            this.btnBrowse_Nu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse_Nu.Location = new System.Drawing.Point(557, 108);
            this.btnBrowse_Nu.Name = "btnBrowse_Nu";
            this.btnBrowse_Nu.Size = new System.Drawing.Size(28, 22);
            this.btnBrowse_Nu.TabIndex = 14;
            this.btnBrowse_Nu.Text = "...";
            this.btnBrowse_Nu.UseVisualStyleBackColor = true;
            this.btnBrowse_Nu.Click += new System.EventHandler(this.btnBrowse_Nu_Click);
            // 
            // txtMauBaoCao_Nu
            // 
            this.txtMauBaoCao_Nu.Location = new System.Drawing.Point(149, 109);
            this.txtMauBaoCao_Nu.MaxLength = 255;
            this.txtMauBaoCao_Nu.Name = "txtMauBaoCao_Nu";
            this.txtMauBaoCao_Nu.ReadOnly = true;
            this.txtMauBaoCao_Nu.Size = new System.Drawing.Size(403, 20);
            this.txtMauBaoCao_Nu.TabIndex = 13;
            // 
            // btnBrowse_Nam
            // 
            this.btnBrowse_Nam.Enabled = false;
            this.btnBrowse_Nam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse_Nam.Location = new System.Drawing.Point(557, 84);
            this.btnBrowse_Nam.Name = "btnBrowse_Nam";
            this.btnBrowse_Nam.Size = new System.Drawing.Size(28, 22);
            this.btnBrowse_Nam.TabIndex = 11;
            this.btnBrowse_Nam.Text = "...";
            this.btnBrowse_Nam.UseVisualStyleBackColor = true;
            this.btnBrowse_Nam.Click += new System.EventHandler(this.btnBrowse_Nam_Click);
            // 
            // txtMauBaoCao_Nam
            // 
            this.txtMauBaoCao_Nam.Location = new System.Drawing.Point(149, 85);
            this.txtMauBaoCao_Nam.MaxLength = 255;
            this.txtMauBaoCao_Nam.Name = "txtMauBaoCao_Nam";
            this.txtMauBaoCao_Nam.ReadOnly = true;
            this.txtMauBaoCao_Nam.Size = new System.Drawing.Size(403, 20);
            this.txtMauBaoCao_Nam.TabIndex = 10;
            // 
            // btnBrowse_Chung
            // 
            this.btnBrowse_Chung.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse_Chung.Location = new System.Drawing.Point(556, 36);
            this.btnBrowse_Chung.Name = "btnBrowse_Chung";
            this.btnBrowse_Chung.Size = new System.Drawing.Size(28, 22);
            this.btnBrowse_Chung.TabIndex = 7;
            this.btnBrowse_Chung.Text = "...";
            this.btnBrowse_Chung.UseVisualStyleBackColor = true;
            this.btnBrowse_Chung.Click += new System.EventHandler(this.btnBrowse_Chung_Click);
            // 
            // txtMauBaoCao_Chung
            // 
            this.txtMauBaoCao_Chung.Location = new System.Drawing.Point(148, 37);
            this.txtMauBaoCao_Chung.MaxLength = 255;
            this.txtMauBaoCao_Chung.Name = "txtMauBaoCao_Chung";
            this.txtMauBaoCao_Chung.ReadOnly = true;
            this.txtMauBaoCao_Chung.Size = new System.Drawing.Size(403, 20);
            this.txtMauBaoCao_Chung.TabIndex = 6;
            // 
            // raNamNu
            // 
            this.raNamNu.AutoSize = true;
            this.raNamNu.Location = new System.Drawing.Point(16, 62);
            this.raNamNu.Name = "raNamNu";
            this.raNamNu.Size = new System.Drawing.Size(145, 17);
            this.raNamNu.TabIndex = 8;
            this.raNamNu.Text = "Mẫu báo cáo (Nam - Nữ):";
            this.raNamNu.UseVisualStyleBackColor = true;
            this.raNamNu.CheckedChanged += new System.EventHandler(this.raNamNu_CheckedChanged);
            // 
            // raChung
            // 
            this.raChung.AutoSize = true;
            this.raChung.Checked = true;
            this.raChung.Location = new System.Drawing.Point(16, 38);
            this.raChung.Name = "raChung";
            this.raChung.Size = new System.Drawing.Size(131, 17);
            this.raChung.TabIndex = 5;
            this.raChung.TabStop = true;
            this.raChung.Text = "Mẫu báo cáo (Chung):";
            this.raChung.UseVisualStyleBackColor = true;
            this.raChung.CheckedChanged += new System.EventHandler(this.raChung_CheckedChanged);
            // 
            // numThuTu
            // 
            this.numThuTu.Location = new System.Drawing.Point(356, 10);
            this.numThuTu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThuTu.Name = "numThuTu";
            this.numThuTu.Size = new System.Drawing.Size(62, 20);
            this.numThuTu.TabIndex = 3;
            this.numThuTu.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Thứ tự:";
            // 
            // txtTenSieuAm
            // 
            this.txtTenSieuAm.Location = new System.Drawing.Point(86, 10);
            this.txtTenSieuAm.MaxLength = 255;
            this.txtTenSieuAm.Name = "txtTenSieuAm";
            this.txtTenSieuAm.Size = new System.Drawing.Size(212, 20);
            this.txtTenSieuAm.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên siêu âm:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(676, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mẫu báo cáo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExportWord);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 578);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(676, 38);
            this.panel3.TabIndex = 3;
            // 
            // btnExportWord
            // 
            this.btnExportWord.Image = ((System.Drawing.Image)(resources.GetObject("btnExportWord.Image")));
            this.btnExportWord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportWord.Location = new System.Drawing.Point(195, 6);
            this.btnExportWord.Name = "btnExportWord";
            this.btnExportWord.Size = new System.Drawing.Size(89, 25);
            this.btnExportWord.TabIndex = 2;
            this.btnExportWord.Text = "      &Xuất Word";
            this.btnExportWord.UseVisualStyleBackColor = true;
            this.btnExportWord.Click += new System.EventHandler(this.btnExportWord_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(101, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(89, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "    &Cập nhật";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(5, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // uLoaiSieuAmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "uLoaiSieuAmList";
            this.Size = new System.Drawing.Size(1001, 620);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoaiSieuAm)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loaiSieuAmBindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tabMauBaoCao.ResumeLayout(false);
            this.ctmAction2.ResumeLayout(false);
            this._page1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgLoaiSieuAm;
        private System.Windows.Forms.BindingSource loaiSieuAmBindingSource;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.NumericUpDown numThuTu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenSieuAm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse_Nu;
        private System.Windows.Forms.TextBox txtMauBaoCao_Nu;
        private System.Windows.Forms.Button btnBrowse_Nam;
        private System.Windows.Forms.TextBox txtMauBaoCao_Nam;
        private System.Windows.Forms.TabControl tabMauBaoCao;
        private System.Windows.Forms.CheckBox chkNu;
        private System.Windows.Forms.CheckBox chkNam;
        private System.Windows.Forms.Button btnBrowse_Chung;
        private System.Windows.Forms.TextBox txtMauBaoCao_Chung;
        private System.Windows.Forms.RadioButton raNamNu;
        private System.Windows.Forms.RadioButton raChung;
        private System.Windows.Forms.TabPage _page1;
        private TXTextControl.TextControl _textControl1;
        private System.Windows.Forms.CheckBox chkInTrang2;
        private System.Windows.Forms.Button btnExportWord;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenSieuAmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thuTuDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        protected System.Windows.Forms.ContextMenuStrip ctmAction2;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportWordToolStripMenuItem;
    }
}
