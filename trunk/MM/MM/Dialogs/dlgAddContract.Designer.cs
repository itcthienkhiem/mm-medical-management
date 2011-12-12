namespace MM.Dialogs
{
    partial class dlgAddContract
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

        #region Windows Form Designer generated code

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddContract));
            this.tabContract = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.chkCompleted = new System.Windows.Forms.CheckBox();
            this.dtpkBeginDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pageContractInfo = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCheckedMember = new System.Windows.Forms.CheckBox();
            this.dgMembers = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderAsStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractMemberViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.pageMemberList = new DevComponents.DotNetBar.TabItem(this.components);
            this.companyCheckListViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDSNVChuaKham = new System.Windows.Forms.Button();
            this.btnDSNVChuaKhamDu = new System.Windows.Forms.Button();
            this.btnDSNVKhamDu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabContract)).BeginInit();
            this.tabContract.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractMemberViewBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyCheckListViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabContract
            // 
            this.tabContract.CanReorderTabs = true;
            this.tabContract.Controls.Add(this.tabControlPanel1);
            this.tabContract.Controls.Add(this.tabControlPanel2);
            this.tabContract.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabContract.Location = new System.Drawing.Point(0, 0);
            this.tabContract.Name = "tabContract";
            this.tabContract.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabContract.SelectedTabIndex = 0;
            this.tabContract.Size = new System.Drawing.Size(604, 374);
            this.tabContract.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabContract.TabIndex = 0;
            this.tabContract.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabContract.Tabs.Add(this.pageContractInfo);
            this.tabContract.Tabs.Add(this.pageMemberList);
            this.tabContract.TabStop = false;
            this.tabContract.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.btnDSNVKhamDu);
            this.tabControlPanel1.Controls.Add(this.btnDSNVChuaKhamDu);
            this.tabControlPanel1.Controls.Add(this.btnDSNVChuaKham);
            this.tabControlPanel1.Controls.Add(this.cboCompany);
            this.tabControlPanel1.Controls.Add(this.label4);
            this.tabControlPanel1.Controls.Add(this.chkCompleted);
            this.tabControlPanel1.Controls.Add(this.dtpkBeginDate);
            this.tabControlPanel1.Controls.Add(this.label3);
            this.tabControlPanel1.Controls.Add(this.label7);
            this.tabControlPanel1.Controls.Add(this.label22);
            this.tabControlPanel1.Controls.Add(this.txtName);
            this.tabControlPanel1.Controls.Add(this.txtCode);
            this.tabControlPanel1.Controls.Add(this.label2);
            this.tabControlPanel1.Controls.Add(this.label1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(604, 349);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.pageContractInfo;
            // 
            // cboCompany
            // 
            this.cboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCompany.DataSource = this.companyBindingSource;
            this.cboCompany.DisplayMember = "TenCty";
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(98, 61);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(300, 21);
            this.cboCompany.TabIndex = 2;
            this.cboCompany.ValueMember = "CompanyGUID";
            this.cboCompany.SelectedValueChanged += new System.EventHandler(this.cboCompany_SelectedValueChanged);
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataSource = typeof(MM.Databasae.Company);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Công ty:";
            // 
            // chkCompleted
            // 
            this.chkCompleted.AutoSize = true;
            this.chkCompleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkCompleted.Location = new System.Drawing.Point(21, 112);
            this.chkCompleted.Name = "chkCompleted";
            this.chkCompleted.Size = new System.Drawing.Size(116, 17);
            this.chkCompleted.TabIndex = 4;
            this.chkCompleted.Text = "Hoàn tất hợp đồng";
            this.chkCompleted.UseVisualStyleBackColor = false;
            // 
            // dtpkBeginDate
            // 
            this.dtpkBeginDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkBeginDate.Location = new System.Drawing.Point(99, 86);
            this.dtpkBeginDate.Name = "dtpkBeginDate";
            this.dtpkBeginDate.Size = new System.Drawing.Size(97, 20);
            this.dtpkBeginDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label3.Location = new System.Drawing.Point(18, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Ngày bắt đầu:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(402, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "[*]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(265, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 13);
            this.label22.TabIndex = 50;
            this.label22.Text = "[*]";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(98, 37);
            this.txtName.MaxLength = 500;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(98, 13);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(163, 20);
            this.txtCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Tên hợp đồng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Mã hợp đồng:";
            // 
            // pageContractInfo
            // 
            this.pageContractInfo.AttachedControl = this.tabControlPanel1;
            this.pageContractInfo.Image = global::MM.Properties.Resources.info_icon;
            this.pageContractInfo.Name = "pageContractInfo";
            this.pageContractInfo.Text = "Thông tin hợp đồng";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.panel2);
            this.tabControlPanel2.Controls.Add(this.panel1);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(604, 349);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.pageMemberList;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkCheckedMember);
            this.panel2.Controls.Add(this.dgMembers);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 347);
            this.panel2.TabIndex = 1;
            // 
            // chkCheckedMember
            // 
            this.chkCheckedMember.AutoSize = true;
            this.chkCheckedMember.Location = new System.Drawing.Point(45, 5);
            this.chkCheckedMember.Name = "chkCheckedMember";
            this.chkCheckedMember.Size = new System.Drawing.Size(15, 14);
            this.chkCheckedMember.TabIndex = 7;
            this.chkCheckedMember.UseVisualStyleBackColor = true;
            this.chkCheckedMember.CheckedChanged += new System.EventHandler(this.chkCheckedMember_CheckedChanged);
            // 
            // dgMembers
            // 
            this.dgMembers.AllowUserToAddRows = false;
            this.dgMembers.AllowUserToDeleteRows = false;
            this.dgMembers.AllowUserToOrderColumns = true;
            this.dgMembers.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.fileNumDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.dobStrDataGridViewTextBoxColumn,
            this.genderAsStrDataGridViewTextBoxColumn});
            this.dgMembers.DataSource = this.contractMemberViewBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMembers.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMembers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgMembers.HighlightSelectedColumnHeaders = false;
            this.dgMembers.Location = new System.Drawing.Point(0, 0);
            this.dgMembers.MultiSelect = false;
            this.dgMembers.Name = "dgMembers";
            this.dgMembers.ReadOnly = true;
            this.dgMembers.RowHeadersWidth = 30;
            this.dgMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMembers.Size = new System.Drawing.Size(515, 347);
            this.dgMembers.TabIndex = 6;
            this.dgMembers.DoubleClick += new System.EventHandler(this.dgMembers_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // fileNumDataGridViewTextBoxColumn
            // 
            this.fileNumDataGridViewTextBoxColumn.DataPropertyName = "FileNum";
            this.fileNumDataGridViewTextBoxColumn.HeaderText = "Mã bệnh nhân";
            this.fileNumDataGridViewTextBoxColumn.Name = "fileNumDataGridViewTextBoxColumn";
            this.fileNumDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNumDataGridViewTextBoxColumn.Width = 110;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Họ tên";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // dobStrDataGridViewTextBoxColumn
            // 
            this.dobStrDataGridViewTextBoxColumn.DataPropertyName = "DobStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dobStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dobStrDataGridViewTextBoxColumn.HeaderText = "Ngày sinh";
            this.dobStrDataGridViewTextBoxColumn.Name = "dobStrDataGridViewTextBoxColumn";
            this.dobStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.dobStrDataGridViewTextBoxColumn.Width = 90;
            // 
            // genderAsStrDataGridViewTextBoxColumn
            // 
            this.genderAsStrDataGridViewTextBoxColumn.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.genderAsStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.genderAsStrDataGridViewTextBoxColumn.HeaderText = "Giới tính";
            this.genderAsStrDataGridViewTextBoxColumn.Name = "genderAsStrDataGridViewTextBoxColumn";
            this.genderAsStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.genderAsStrDataGridViewTextBoxColumn.Width = 70;
            // 
            // contractMemberViewBindingSource
            // 
            this.contractMemberViewBindingSource.DataSource = typeof(MM.Databasae.ContractMemberView);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteMember);
            this.panel1.Controls.Add(this.btnAddMember);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(516, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 347);
            this.panel1.TabIndex = 0;
            // 
            // btnDeleteMember
            // 
            this.btnDeleteMember.Image = global::MM.Properties.Resources.del;
            this.btnDeleteMember.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteMember.Location = new System.Drawing.Point(6, 34);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(75, 25);
            this.btnDeleteMember.TabIndex = 6;
            this.btnDeleteMember.Text = "    &Xóa";
            this.btnDeleteMember.UseVisualStyleBackColor = true;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            // 
            // btnAddMember
            // 
            this.btnAddMember.Image = global::MM.Properties.Resources.add;
            this.btnAddMember.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddMember.Location = new System.Drawing.Point(6, 6);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(75, 25);
            this.btnAddMember.TabIndex = 5;
            this.btnAddMember.Text = "    &Thêm";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // pageMemberList
            // 
            this.pageMemberList.AttachedControl = this.tabControlPanel2;
            this.pageMemberList.Image = global::MM.Properties.Resources.Apps_preferences_contact_list_icon;
            this.pageMemberList.Name = "pageMemberList";
            this.pageMemberList.Text = "Danh sách nhân viên";
            // 
            // companyCheckListViewBindingSource
            // 
            this.companyCheckListViewBindingSource.DataSource = typeof(MM.Databasae.CompanyCheckListView);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(304, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(225, 380);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnDSNVChuaKham
            // 
            this.btnDSNVChuaKham.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDSNVChuaKham.Location = new System.Drawing.Point(106, 157);
            this.btnDSNVChuaKham.Name = "btnDSNVChuaKham";
            this.btnDSNVChuaKham.Size = new System.Drawing.Size(127, 45);
            this.btnDSNVChuaKham.TabIndex = 54;
            this.btnDSNVChuaKham.Text = "Danh sách nhân viên chưa khám";
            this.btnDSNVChuaKham.UseVisualStyleBackColor = true;
            this.btnDSNVChuaKham.Visible = false;
            this.btnDSNVChuaKham.Click += new System.EventHandler(this.btnDSNVChuaKham_Click);
            // 
            // btnDSNVChuaKhamDu
            // 
            this.btnDSNVChuaKhamDu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDSNVChuaKhamDu.Location = new System.Drawing.Point(239, 157);
            this.btnDSNVChuaKhamDu.Name = "btnDSNVChuaKhamDu";
            this.btnDSNVChuaKhamDu.Size = new System.Drawing.Size(127, 45);
            this.btnDSNVChuaKhamDu.TabIndex = 55;
            this.btnDSNVChuaKhamDu.Text = "Danh sách nhân viên chưa khám đủ dịch vụ";
            this.btnDSNVChuaKhamDu.UseVisualStyleBackColor = true;
            this.btnDSNVChuaKhamDu.Visible = false;
            this.btnDSNVChuaKhamDu.Click += new System.EventHandler(this.btnDSNVChuaKhamDu_Click);
            // 
            // btnDSNVKhamDu
            // 
            this.btnDSNVKhamDu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDSNVKhamDu.Location = new System.Drawing.Point(372, 157);
            this.btnDSNVKhamDu.Name = "btnDSNVKhamDu";
            this.btnDSNVKhamDu.Size = new System.Drawing.Size(127, 45);
            this.btnDSNVKhamDu.TabIndex = 56;
            this.btnDSNVKhamDu.Text = "Danh sách nhân viên đã khám đầy đủ";
            this.btnDSNVKhamDu.UseVisualStyleBackColor = true;
            this.btnDSNVKhamDu.Visible = false;
            this.btnDSNVKhamDu.Click += new System.EventHandler(this.btnDSNVKhamDu_Click);
            // 
            // dlgAddContract
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(604, 412);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabContract);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddContract";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them hop dong";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddContract_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tabContract)).EndInit();
            this.tabContract.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractMemberViewBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.companyCheckListViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabContract;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem pageContractInfo;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pageMemberList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkCompleted;
        private System.Windows.Forms.DateTimePicker dtpkBeginDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.CheckBox chkCheckedMember;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgMembers;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn genderAsStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource contractMemberViewBindingSource;
        private System.Windows.Forms.BindingSource companyCheckListViewBindingSource;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDSNVKhamDu;
        private System.Windows.Forms.Button btnDSNVChuaKhamDu;
        private System.Windows.Forms.Button btnDSNVChuaKham;

    }
}