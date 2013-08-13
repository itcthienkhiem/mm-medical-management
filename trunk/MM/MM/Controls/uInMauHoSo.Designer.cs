namespace MM.Controls
{
    partial class uInMauHoSo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uInMauHoSo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbThongBao = new System.Windows.Forms.Label();
            this.txtTenHopDong = new System.Windows.Forms.TextBox();
            this.cboMaHopDong = new System.Windows.Forms.ComboBox();
            this.companyContractViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgPatient = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inHoSoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnExportWord = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.raNuTren40 = new System.Windows.Forms.RadioButton();
            this.raNamTren40 = new System.Windows.Forms.RadioButton();
            this.raNuCoGiaDinh = new System.Windows.Forms.RadioButton();
            this.raNu = new System.Windows.Forms.RadioButton();
            this.raNam = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.chkMaBenhNhan = new System.Windows.Forms.CheckBox();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serviceHistoryViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xuatHoSoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).BeginInit();
            this.ctmAction3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceHistoryViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbThongBao);
            this.panel1.Controls.Add(this.txtTenHopDong);
            this.panel1.Controls.Add(this.cboMaHopDong);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1121, 36);
            this.panel1.TabIndex = 0;
            // 
            // lbThongBao
            // 
            this.lbThongBao.AutoSize = true;
            this.lbThongBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbThongBao.ForeColor = System.Drawing.Color.Red;
            this.lbThongBao.Location = new System.Drawing.Point(569, 13);
            this.lbThongBao.Name = "lbThongBao";
            this.lbThongBao.Size = new System.Drawing.Size(0, 13);
            this.lbThongBao.TabIndex = 10;
            // 
            // txtTenHopDong
            // 
            this.txtTenHopDong.Location = new System.Drawing.Point(255, 9);
            this.txtTenHopDong.Name = "txtTenHopDong";
            this.txtTenHopDong.ReadOnly = true;
            this.txtTenHopDong.Size = new System.Drawing.Size(310, 20);
            this.txtTenHopDong.TabIndex = 9;
            // 
            // cboMaHopDong
            // 
            this.cboMaHopDong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboMaHopDong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMaHopDong.DataSource = this.companyContractViewBindingSource;
            this.cboMaHopDong.DisplayMember = "ContractCode";
            this.cboMaHopDong.FormattingEnabled = true;
            this.cboMaHopDong.Location = new System.Drawing.Point(73, 8);
            this.cboMaHopDong.Name = "cboMaHopDong";
            this.cboMaHopDong.Size = new System.Drawing.Size(179, 21);
            this.cboMaHopDong.TabIndex = 8;
            this.cboMaHopDong.ValueMember = "CompanyContractGUID";
            this.cboMaHopDong.SelectedIndexChanged += new System.EventHandler(this.cboMaHopDong_SelectedIndexChanged);
            // 
            // companyContractViewBindingSource
            // 
            this.companyContractViewBindingSource.DataSource = typeof(MM.Databasae.CompanyContractView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hợp đồng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1121, 564);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel14);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1121, 564);
            this.panel3.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.chkChecked);
            this.panel14.Controls.Add(this.dgPatient);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 57);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1121, 469);
            this.panel14.TabIndex = 2;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Checked = true;
            this.chkChecked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 17;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgPatient
            // 
            this.dgPatient.AllowUserToAddRows = false;
            this.dgPatient.AllowUserToDeleteRows = false;
            this.dgPatient.AllowUserToOrderColumns = true;
            this.dgPatient.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.fileNumDataGridViewTextBoxColumn,
            this.Fullname,
            this.dobDataGridViewTextBoxColumn,
            this.GenderAsStr});
            this.dgPatient.ContextMenuStrip = this.ctmAction3;
            this.dgPatient.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatient.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPatient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPatient.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPatient.HighlightSelectedColumnHeaders = false;
            this.dgPatient.Location = new System.Drawing.Point(0, 0);
            this.dgPatient.MultiSelect = false;
            this.dgPatient.Name = "dgPatient";
            this.dgPatient.RowHeadersWidth = 30;
            this.dgPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatient.Size = new System.Drawing.Size(1121, 469);
            this.dgPatient.TabIndex = 4;
            this.dgPatient.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPatient_CellMouseUp);
            this.dgPatient.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPatient_ColumnHeaderMouseClick);
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
            // fileNumDataGridViewTextBoxColumn
            // 
            this.fileNumDataGridViewTextBoxColumn.DataPropertyName = "FileNum";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fileNumDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.fileNumDataGridViewTextBoxColumn.HeaderText = "Mã bệnh nhân";
            this.fileNumDataGridViewTextBoxColumn.Name = "fileNumDataGridViewTextBoxColumn";
            this.fileNumDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNumDataGridViewTextBoxColumn.Width = 110;
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "FullName";
            this.Fullname.HeaderText = "Họ Tên";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            this.Fullname.Width = 250;
            // 
            // dobDataGridViewTextBoxColumn
            // 
            this.dobDataGridViewTextBoxColumn.DataPropertyName = "DobStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.dobDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dobDataGridViewTextBoxColumn.HeaderText = "Ngày sinh";
            this.dobDataGridViewTextBoxColumn.Name = "dobDataGridViewTextBoxColumn";
            this.dobDataGridViewTextBoxColumn.ReadOnly = true;
            this.dobDataGridViewTextBoxColumn.Width = 80;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle4;
            this.GenderAsStr.HeaderText = "Giới tính";
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            this.GenderAsStr.Width = 70;
            // 
            // ctmAction3
            // 
            this.ctmAction3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inHoSoToolStripMenuItem,
            this.toolStripSeparator1,
            this.xuatHoSoToolStripMenuItem});
            this.ctmAction3.Name = "ctmAction";
            this.ctmAction3.Size = new System.Drawing.Size(153, 76);
            // 
            // inHoSoToolStripMenuItem
            // 
            this.inHoSoToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.inHoSoToolStripMenuItem.Name = "inHoSoToolStripMenuItem";
            this.inHoSoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inHoSoToolStripMenuItem.Text = "In hồ sơ";
            this.inHoSoToolStripMenuItem.Click += new System.EventHandler(this.inHoSoToolStripMenuItem_Click);
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.btnExportWord);
            this.panel8.Controls.Add(this.btnPrint);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 526);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1121, 38);
            this.panel8.TabIndex = 1;
            // 
            // btnExportWord
            // 
            this.btnExportWord.Image = ((System.Drawing.Image)(resources.GetObject("btnExportWord.Image")));
            this.btnExportWord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportWord.Location = new System.Drawing.Point(97, 4);
            this.btnExportWord.Name = "btnExportWord";
            this.btnExportWord.Size = new System.Drawing.Size(93, 25);
            this.btnExportWord.TabIndex = 80;
            this.btnExportWord.Text = "      &Xuất hồ sơ";
            this.btnExportWord.UseVisualStyleBackColor = true;
            this.btnExportWord.Click += new System.EventHandler(this.btnExportWord_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(6, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 25);
            this.btnPrint.TabIndex = 79;
            this.btnPrint.Text = "     &In hồ sơ";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.raNuTren40);
            this.panel7.Controls.Add(this.raNamTren40);
            this.panel7.Controls.Add(this.raNuCoGiaDinh);
            this.panel7.Controls.Add(this.raNu);
            this.panel7.Controls.Add(this.raNam);
            this.panel7.Controls.Add(this.raAll);
            this.panel7.Controls.Add(this.chkMaBenhNhan);
            this.panel7.Controls.Add(this.txtSearchPatient);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1121, 57);
            this.panel7.TabIndex = 0;
            // 
            // raNuTren40
            // 
            this.raNuTren40.AutoSize = true;
            this.raNuTren40.Location = new System.Drawing.Point(532, 32);
            this.raNuTren40.Name = "raNuTren40";
            this.raNuTren40.Size = new System.Drawing.Size(95, 17);
            this.raNuTren40.TabIndex = 26;
            this.raNuTren40.Text = "Nữ trên 40 tuổi";
            this.raNuTren40.UseVisualStyleBackColor = true;
            this.raNuTren40.CheckedChanged += new System.EventHandler(this.raNuTren40_CheckedChanged);
            // 
            // raNamTren40
            // 
            this.raNamTren40.AutoSize = true;
            this.raNamTren40.Location = new System.Drawing.Point(417, 32);
            this.raNamTren40.Name = "raNamTren40";
            this.raNamTren40.Size = new System.Drawing.Size(103, 17);
            this.raNamTren40.TabIndex = 24;
            this.raNamTren40.Text = "Nam trên 40 tuổi";
            this.raNamTren40.UseVisualStyleBackColor = true;
            this.raNamTren40.CheckedChanged += new System.EventHandler(this.raNamTren40_CheckedChanged);
            // 
            // raNuCoGiaDinh
            // 
            this.raNuCoGiaDinh.AutoSize = true;
            this.raNuCoGiaDinh.Location = new System.Drawing.Point(313, 32);
            this.raNuCoGiaDinh.Name = "raNuCoGiaDinh";
            this.raNuCoGiaDinh.Size = new System.Drawing.Size(95, 17);
            this.raNuCoGiaDinh.TabIndex = 23;
            this.raNuCoGiaDinh.Text = "Nữ có gia đình";
            this.raNuCoGiaDinh.UseVisualStyleBackColor = true;
            this.raNuCoGiaDinh.CheckedChanged += new System.EventHandler(this.raNuCoGiaDinh_CheckedChanged);
            // 
            // raNu
            // 
            this.raNu.AutoSize = true;
            this.raNu.Location = new System.Drawing.Point(215, 32);
            this.raNu.Name = "raNu";
            this.raNu.Size = new System.Drawing.Size(85, 17);
            this.raNu.TabIndex = 22;
            this.raNu.Text = "Nữ độc thân";
            this.raNu.UseVisualStyleBackColor = true;
            this.raNu.CheckedChanged += new System.EventHandler(this.raNu_CheckedChanged);
            // 
            // raNam
            // 
            this.raNam.AutoSize = true;
            this.raNam.Location = new System.Drawing.Point(155, 32);
            this.raNam.Name = "raNam";
            this.raNam.Size = new System.Drawing.Size(47, 17);
            this.raNam.TabIndex = 21;
            this.raNam.Text = "Nam";
            this.raNam.UseVisualStyleBackColor = true;
            this.raNam.CheckedChanged += new System.EventHandler(this.raNam_CheckedChanged);
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(89, 32);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 20;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // chkMaBenhNhan
            // 
            this.chkMaBenhNhan.AutoSize = true;
            this.chkMaBenhNhan.Checked = true;
            this.chkMaBenhNhan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaBenhNhan.Location = new System.Drawing.Point(386, 8);
            this.chkMaBenhNhan.Name = "chkMaBenhNhan";
            this.chkMaBenhNhan.Size = new System.Drawing.Size(118, 17);
            this.chkMaBenhNhan.TabIndex = 7;
            this.chkMaBenhNhan.Text = "Theo mã nhân viên";
            this.chkMaBenhNhan.UseVisualStyleBackColor = true;
            this.chkMaBenhNhan.CheckedChanged += new System.EventHandler(this.chkMaBenhNhan_CheckedChanged);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(89, 6);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(291, 20);
            this.txtSearchPatient.TabIndex = 6;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            this.txtSearchPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPatient_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tìm nhân viên:";
            // 
            // serviceHistoryViewBindingSource
            // 
            this.serviceHistoryViewBindingSource.DataSource = typeof(MM.Databasae.ServiceHistoryView);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // xuatHoSoToolStripMenuItem
            // 
            this.xuatHoSoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("xuatHoSoToolStripMenuItem.Image")));
            this.xuatHoSoToolStripMenuItem.Name = "xuatHoSoToolStripMenuItem";
            this.xuatHoSoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xuatHoSoToolStripMenuItem.Text = "Xuất hồ sơ";
            this.xuatHoSoToolStripMenuItem.Click += new System.EventHandler(this.xuatHoSoToolStripMenuItem_Click);
            // 
            // uInMauHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uInMauHoSo";
            this.Size = new System.Drawing.Size(1121, 600);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).EndInit();
            this.ctmAction3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceHistoryViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.BindingSource companyContractViewBindingSource;
        private System.Windows.Forms.TextBox txtTenHopDong;
        private System.Windows.Forms.ComboBox cboMaHopDong;
        private System.Windows.Forms.CheckBox chkMaBenhNhan;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPatient;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.RadioButton raNuCoGiaDinh;
        private System.Windows.Forms.RadioButton raNu;
        private System.Windows.Forms.RadioButton raNam;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.Label lbThongBao;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ContextMenuStrip ctmAction3;
        private System.Windows.Forms.BindingSource serviceHistoryViewBindingSource;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.RadioButton raNamTren40;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolStripMenuItem inHoSoToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.RadioButton raNuTren40;
        private System.Windows.Forms.Button btnExportWord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem xuatHoSoToolStripMenuItem;
    }
}
