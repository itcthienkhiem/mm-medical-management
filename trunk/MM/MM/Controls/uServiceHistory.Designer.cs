namespace MM.Controls
{
    partial class uServiceHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgServiceHistory = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixedPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsExported = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocStaffFullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceHistoryViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pTotal = new System.Windows.Forms.Panel();
            this.lbTotalReceipt = new System.Windows.Forms.Label();
            this.lbPay = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.raAmount = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numPercentage = new System.Windows.Forms.NumericUpDown();
            this.raPercentage = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTotalPrice = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportReceipt = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pFilter = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.raFromDateToDate = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceHistoryViewBindingSource)).BeginInit();
            this.pTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage)).BeginInit();
            this.panel2.SuspendLayout();
            this.pFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgServiceHistory);
            this.panel3.Controls.Add(this.pTotal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(814, 371);
            this.panel3.TabIndex = 2;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 2;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgServiceHistory
            // 
            this.dgServiceHistory.AllowUserToAddRows = false;
            this.dgServiceHistory.AllowUserToDeleteRows = false;
            this.dgServiceHistory.AllowUserToOrderColumns = true;
            this.dgServiceHistory.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgServiceHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgServiceHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServiceHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.codeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.fixedPriceDataGridViewTextBoxColumn,
            this.ActivedDate,
            this.IsExported,
            this.noteDataGridViewTextBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.DocStaffFullname});
            this.dgServiceHistory.DataSource = this.serviceHistoryViewBindingSource;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgServiceHistory.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgServiceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgServiceHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgServiceHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgServiceHistory.HighlightSelectedColumnHeaders = false;
            this.dgServiceHistory.Location = new System.Drawing.Point(0, 0);
            this.dgServiceHistory.MultiSelect = false;
            this.dgServiceHistory.Name = "dgServiceHistory";
            this.dgServiceHistory.ReadOnly = true;
            this.dgServiceHistory.RowHeadersWidth = 30;
            this.dgServiceHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgServiceHistory.Size = new System.Drawing.Size(810, 267);
            this.dgServiceHistory.TabIndex = 1;
            this.dgServiceHistory.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgServiceHistory_CellMouseDown);
            this.dgServiceHistory.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgServiceHistory_CellMouseUp);
            this.dgServiceHistory.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgServiceHistory_ColumnHeaderMouseClick);
            this.dgServiceHistory.DoubleClick += new System.EventHandler(this.dgServiceHistory_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã DV";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên DV";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 180;
            // 
            // fixedPriceDataGridViewTextBoxColumn
            // 
            this.fixedPriceDataGridViewTextBoxColumn.DataPropertyName = "FixedPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.fixedPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.fixedPriceDataGridViewTextBoxColumn.HeaderText = "Giá";
            this.fixedPriceDataGridViewTextBoxColumn.Name = "fixedPriceDataGridViewTextBoxColumn";
            this.fixedPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ActivedDate
            // 
            this.ActivedDate.DataPropertyName = "ActivedDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            dataGridViewCellStyle5.NullValue = null;
            this.ActivedDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.ActivedDate.HeaderText = "Ngày sử dụng";
            this.ActivedDate.Name = "ActivedDate";
            this.ActivedDate.ReadOnly = true;
            // 
            // IsExported
            // 
            this.IsExported.DataPropertyName = "IsExported";
            this.IsExported.HeaderText = "Đã trả tiền";
            this.IsExported.Name = "IsExported";
            this.IsExported.ReadOnly = true;
            this.IsExported.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsExported.Width = 80;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Nhận xét";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // createdDateDataGridViewTextBoxColumn
            // 
            this.createdDateDataGridViewTextBoxColumn.DataPropertyName = "CreatedDate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle6.NullValue = null;
            this.createdDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.createdDateDataGridViewTextBoxColumn.HeaderText = "Ngày tạo";
            this.createdDateDataGridViewTextBoxColumn.Name = "createdDateDataGridViewTextBoxColumn";
            this.createdDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDateDataGridViewTextBoxColumn.Visible = false;
            this.createdDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // DocStaffFullname
            // 
            this.DocStaffFullname.DataPropertyName = "CreatedName";
            this.DocStaffFullname.HeaderText = "Người tạo";
            this.DocStaffFullname.Name = "DocStaffFullname";
            this.DocStaffFullname.ReadOnly = true;
            this.DocStaffFullname.Width = 150;
            // 
            // serviceHistoryViewBindingSource
            // 
            this.serviceHistoryViewBindingSource.DataSource = typeof(MM.Databasae.ServiceHistoryView);
            // 
            // pTotal
            // 
            this.pTotal.Controls.Add(this.lbTotalReceipt);
            this.pTotal.Controls.Add(this.lbPay);
            this.pTotal.Controls.Add(this.label3);
            this.pTotal.Controls.Add(this.numAmount);
            this.pTotal.Controls.Add(this.raAmount);
            this.pTotal.Controls.Add(this.label2);
            this.pTotal.Controls.Add(this.numPercentage);
            this.pTotal.Controls.Add(this.raPercentage);
            this.pTotal.Controls.Add(this.label1);
            this.pTotal.Controls.Add(this.lbTotalPrice);
            this.pTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pTotal.Location = new System.Drawing.Point(0, 267);
            this.pTotal.Name = "pTotal";
            this.pTotal.Size = new System.Drawing.Size(810, 100);
            this.pTotal.TabIndex = 7;
            // 
            // lbTotalReceipt
            // 
            this.lbTotalReceipt.AutoSize = true;
            this.lbTotalReceipt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalReceipt.ForeColor = System.Drawing.Color.Red;
            this.lbTotalReceipt.Location = new System.Drawing.Point(17, 30);
            this.lbTotalReceipt.Name = "lbTotalReceipt";
            this.lbTotalReceipt.Size = new System.Drawing.Size(73, 13);
            this.lbTotalReceipt.TabIndex = 9;
            this.lbTotalReceipt.Text = "Tổng tiền thu:";
            // 
            // lbPay
            // 
            this.lbPay.AutoSize = true;
            this.lbPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPay.ForeColor = System.Drawing.Color.Red;
            this.lbPay.Location = new System.Drawing.Point(17, 77);
            this.lbPay.Name = "lbPay";
            this.lbPay.Size = new System.Drawing.Size(42, 13);
            this.lbPay.TabIndex = 8;
            this.lbPay.Text = "Còn lại:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "(VNĐ)";
            // 
            // numAmount
            // 
            this.numAmount.Enabled = false;
            this.numAmount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAmount.Location = new System.Drawing.Point(206, 51);
            this.numAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(113, 20);
            this.numAmount.TabIndex = 6;
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);
            this.numAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numAmount_KeyUp);
            // 
            // raAmount
            // 
            this.raAmount.AutoSize = true;
            this.raAmount.Location = new System.Drawing.Point(187, 54);
            this.raAmount.Name = "raAmount";
            this.raAmount.Size = new System.Drawing.Size(14, 13);
            this.raAmount.TabIndex = 5;
            this.raAmount.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "(%)";
            // 
            // numPercentage
            // 
            this.numPercentage.Location = new System.Drawing.Point(97, 51);
            this.numPercentage.Name = "numPercentage";
            this.numPercentage.Size = new System.Drawing.Size(58, 20);
            this.numPercentage.TabIndex = 3;
            this.numPercentage.ValueChanged += new System.EventHandler(this.numPercentage_ValueChanged);
            this.numPercentage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numPercentage_KeyUp);
            // 
            // raPercentage
            // 
            this.raPercentage.AutoSize = true;
            this.raPercentage.Checked = true;
            this.raPercentage.Location = new System.Drawing.Point(78, 54);
            this.raPercentage.Name = "raPercentage";
            this.raPercentage.Size = new System.Drawing.Size(14, 13);
            this.raPercentage.TabIndex = 2;
            this.raPercentage.TabStop = true;
            this.raPercentage.UseVisualStyleBackColor = true;
            this.raPercentage.CheckedChanged += new System.EventHandler(this.raPercentage_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Giảm giá:";
            // 
            // lbTotalPrice
            // 
            this.lbTotalPrice.AutoSize = true;
            this.lbTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalPrice.ForeColor = System.Drawing.Color.Red;
            this.lbTotalPrice.Location = new System.Drawing.Point(17, 7);
            this.lbTotalPrice.Name = "lbTotalPrice";
            this.lbTotalPrice.Size = new System.Drawing.Size(85, 13);
            this.lbTotalPrice.TabIndex = 0;
            this.lbTotalPrice.Text = "Tổng tiền tất cả:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportReceipt);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 37);
            this.panel2.TabIndex = 10;
            // 
            // btnExportReceipt
            // 
            this.btnExportReceipt.Image = global::MM.Properties.Resources.export_icon;
            this.btnExportReceipt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportReceipt.Location = new System.Drawing.Point(243, 6);
            this.btnExportReceipt.Name = "btnExportReceipt";
            this.btnExportReceipt.Size = new System.Drawing.Size(105, 25);
            this.btnExportReceipt.TabIndex = 6;
            this.btnExportReceipt.Text = "      &Xuất phiếu thu";
            this.btnExportReceipt.UseVisualStyleBackColor = true;
            this.btnExportReceipt.Click += new System.EventHandler(this.btnExportReceipt_Click);
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
            // pFilter
            // 
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.dtpkToDate);
            this.pFilter.Controls.Add(this.lbToDate);
            this.pFilter.Controls.Add(this.dtpkFromDate);
            this.pFilter.Controls.Add(this.raFromDateToDate);
            this.pFilter.Controls.Add(this.raAll);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(814, 60);
            this.pFilter.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(340, 29);
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
            this.dtpkToDate.Location = new System.Drawing.Point(238, 29);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            this.dtpkToDate.ValueChanged += new System.EventHandler(this.dtpk_ValueChanged);
            this.dtpkToDate.Leave += new System.EventHandler(this.dtpk_Leave);
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(180, 33);
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
            this.dtpkFromDate.Location = new System.Drawing.Point(80, 29);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
            this.dtpkFromDate.ValueChanged += new System.EventHandler(this.dtpk_ValueChanged);
            this.dtpkFromDate.Leave += new System.EventHandler(this.dtpk_Leave);
            // 
            // raFromDateToDate
            // 
            this.raFromDateToDate.AutoSize = true;
            this.raFromDateToDate.Location = new System.Drawing.Point(13, 31);
            this.raFromDateToDate.Name = "raFromDateToDate";
            this.raFromDateToDate.Size = new System.Drawing.Size(64, 17);
            this.raFromDateToDate.TabIndex = 1;
            this.raFromDateToDate.Text = "Từ ngày";
            this.raFromDateToDate.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(13, 9);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 0;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // uServiceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uServiceHistory";
            this.Size = new System.Drawing.Size(814, 468);
            this.Load += new System.EventHandler(this.uServiceHistory_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceHistoryViewBindingSource)).EndInit();
            this.pTotal.ResumeLayout(false);
            this.pTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.RadioButton raFromDateToDate;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.Panel pTotal;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lbTotalPrice;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgServiceHistory;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.BindingSource serviceHistoryViewBindingSource;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdbyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnExportReceipt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.RadioButton raAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPercentage;
        private System.Windows.Forms.RadioButton raPercentage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPay;
        private System.Windows.Forms.Label lbTotalReceipt;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixedPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivedDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExported;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocStaffFullname;
    }
}
