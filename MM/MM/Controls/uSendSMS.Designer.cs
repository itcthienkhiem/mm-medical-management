namespace MM.Controls
{
    partial class uSendSMS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uSendSMS));
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgPatient = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identityCardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.homePhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workPhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._uPrintKetQuaSieuAm = new MM.Controls.uPrintKetQuaSieuAm();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.chkTheoSoDienThoai = new System.Windows.Forms.CheckBox();
            this.chkMaBenhNhan = new System.Windows.Forms.CheckBox();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.guiSMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgPatient);
            this.panel3.Controls.Add(this._uPrintKetQuaSieuAm);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1039, 368);
            this.panel3.TabIndex = 1;
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
            this.FullAddress,
            this.GenderAsStr,
            this.dobDataGridViewTextBoxColumn,
            this.identityCardDataGridViewTextBoxColumn,
            this.homePhoneDataGridViewTextBoxColumn,
            this.workPhoneDataGridViewTextBoxColumn,
            this.mobileDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn});
            this.dgPatient.ContextMenuStrip = this.ctmAction;
            this.dgPatient.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatient.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPatient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPatient.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPatient.HighlightSelectedColumnHeaders = false;
            this.dgPatient.Location = new System.Drawing.Point(0, 0);
            this.dgPatient.MultiSelect = false;
            this.dgPatient.Name = "dgPatient";
            this.dgPatient.RowHeadersWidth = 30;
            this.dgPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatient.Size = new System.Drawing.Size(1039, 368);
            this.dgPatient.TabIndex = 2;
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
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "FullName";
            this.Fullname.HeaderText = "Họ Tên";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            this.Fullname.Width = 150;
            // 
            // FullAddress
            // 
            this.FullAddress.DataPropertyName = "Address";
            this.FullAddress.HeaderText = "Địa chỉ";
            this.FullAddress.Name = "FullAddress";
            this.FullAddress.ReadOnly = true;
            this.FullAddress.Width = 250;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle3;
            this.GenderAsStr.HeaderText = "Giới tính";
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            // 
            // dobDataGridViewTextBoxColumn
            // 
            this.dobDataGridViewTextBoxColumn.DataPropertyName = "DobStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = null;
            this.dobDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.dobDataGridViewTextBoxColumn.HeaderText = "Ngày sinh";
            this.dobDataGridViewTextBoxColumn.Name = "dobDataGridViewTextBoxColumn";
            this.dobDataGridViewTextBoxColumn.ReadOnly = true;
            this.dobDataGridViewTextBoxColumn.Width = 80;
            // 
            // identityCardDataGridViewTextBoxColumn
            // 
            this.identityCardDataGridViewTextBoxColumn.DataPropertyName = "IdentityCard";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identityCardDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.identityCardDataGridViewTextBoxColumn.HeaderText = "CMND";
            this.identityCardDataGridViewTextBoxColumn.Name = "identityCardDataGridViewTextBoxColumn";
            this.identityCardDataGridViewTextBoxColumn.ReadOnly = true;
            this.identityCardDataGridViewTextBoxColumn.Width = 90;
            // 
            // homePhoneDataGridViewTextBoxColumn
            // 
            this.homePhoneDataGridViewTextBoxColumn.DataPropertyName = "HomePhone";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.homePhoneDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.homePhoneDataGridViewTextBoxColumn.HeaderText = "SĐT nhà";
            this.homePhoneDataGridViewTextBoxColumn.Name = "homePhoneDataGridViewTextBoxColumn";
            this.homePhoneDataGridViewTextBoxColumn.ReadOnly = true;
            this.homePhoneDataGridViewTextBoxColumn.Width = 80;
            // 
            // workPhoneDataGridViewTextBoxColumn
            // 
            this.workPhoneDataGridViewTextBoxColumn.DataPropertyName = "WorkPhone";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.workPhoneDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.workPhoneDataGridViewTextBoxColumn.HeaderText = "SĐT làm việc";
            this.workPhoneDataGridViewTextBoxColumn.Name = "workPhoneDataGridViewTextBoxColumn";
            this.workPhoneDataGridViewTextBoxColumn.ReadOnly = true;
            this.workPhoneDataGridViewTextBoxColumn.Width = 95;
            // 
            // mobileDataGridViewTextBoxColumn
            // 
            this.mobileDataGridViewTextBoxColumn.DataPropertyName = "Mobile";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mobileDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.mobileDataGridViewTextBoxColumn.HeaderText = "Số DTDĐ";
            this.mobileDataGridViewTextBoxColumn.Name = "mobileDataGridViewTextBoxColumn";
            this.mobileDataGridViewTextBoxColumn.ReadOnly = true;
            this.mobileDataGridViewTextBoxColumn.Width = 80;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            this.emailDataGridViewTextBoxColumn.Width = 150;
            // 
            // _uPrintKetQuaSieuAm
            // 
            this._uPrintKetQuaSieuAm.Location = new System.Drawing.Point(656, 89);
            this._uPrintKetQuaSieuAm.Name = "_uPrintKetQuaSieuAm";
            this._uPrintKetQuaSieuAm.PatientRow = null;
            this._uPrintKetQuaSieuAm.Size = new System.Drawing.Size(241, 184);
            this._uPrintKetQuaSieuAm.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSendSMS);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1039, 36);
            this.panel2.TabIndex = 5;
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Image = ((System.Drawing.Image)(resources.GetObject("btnSendSMS.Image")));
            this.btnSendSMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendSMS.Location = new System.Drawing.Point(6, 5);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(88, 25);
            this.btnSendSMS.TabIndex = 7;
            this.btnSendSMS.Text = "     &Gửi SMS";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel1.Controls.Add(this.chkTheoSoDienThoai);
            this.panel1.Controls.Add(this.chkMaBenhNhan);
            this.panel1.Controls.Add(this.txtSearchPatient);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 36);
            this.panel1.TabIndex = 0;
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(653, 12);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 7;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // chkTheoSoDienThoai
            // 
            this.chkTheoSoDienThoai.AutoSize = true;
            this.chkTheoSoDienThoai.Location = new System.Drawing.Point(521, 12);
            this.chkTheoSoDienThoai.Name = "chkTheoSoDienThoai";
            this.chkTheoSoDienThoai.Size = new System.Drawing.Size(115, 17);
            this.chkTheoSoDienThoai.TabIndex = 6;
            this.chkTheoSoDienThoai.Text = "Theo số điện thoại";
            this.chkTheoSoDienThoai.UseVisualStyleBackColor = true;
            this.chkTheoSoDienThoai.CheckedChanged += new System.EventHandler(this.chkTheoSoDienThoai_CheckedChanged);
            // 
            // chkMaBenhNhan
            // 
            this.chkMaBenhNhan.AutoSize = true;
            this.chkMaBenhNhan.Location = new System.Drawing.Point(393, 12);
            this.chkMaBenhNhan.Name = "chkMaBenhNhan";
            this.chkMaBenhNhan.Size = new System.Drawing.Size(122, 17);
            this.chkMaBenhNhan.TabIndex = 2;
            this.chkMaBenhNhan.Text = "Theo mã bệnh nhân";
            this.chkMaBenhNhan.UseVisualStyleBackColor = true;
            this.chkMaBenhNhan.CheckedChanged += new System.EventHandler(this.chkMaBenhNhan_CheckedChanged);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(95, 9);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(291, 20);
            this.txtSearchPatient.TabIndex = 1;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            this.txtSearchPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPatient_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm bệnh nhân:";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Checked";
            this.dataGridViewCheckBoxColumn1.Frozen = true;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FileNum";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã bệnh nhân";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FullName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Họ Tên";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn3.HeaderText = "Địa chỉ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "Giới tính";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DobStr";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn5.HeaderText = "Ngày sinh";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "IdentityCard";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn6.HeaderText = "CMND";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 90;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "HomePhone";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn7.HeaderText = "SĐT nhà";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "WorkPhone";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn8.HeaderText = "SĐT làm việc";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 95;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Mobile";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn9.HeaderText = "Số DTDĐ";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Email";
            this.dataGridViewTextBoxColumn10.HeaderText = "Email";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guiSMSToolStripMenuItem});
            this.ctmAction.Name = "ctmAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 48);
            // 
            // guiSMSToolStripMenuItem
            // 
            this.guiSMSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guiSMSToolStripMenuItem.Image")));
            this.guiSMSToolStripMenuItem.Name = "guiSMSToolStripMenuItem";
            this.guiSMSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guiSMSToolStripMenuItem.Text = "Gửi SMS";
            this.guiSMSToolStripMenuItem.Click += new System.EventHandler(this.guiSMSToolStripMenuItem_Click);
            // 
            // uSendSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uSendSMS";
            this.Size = new System.Drawing.Size(1039, 440);
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPatient;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkMaBenhNhan;
        private System.Windows.Forms.CheckBox chkTheoSoDienThoai;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private uPrintKetQuaSieuAm _uPrintKetQuaSieuAm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identityCardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn homePhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workPhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem guiSMSToolStripMenuItem;
    }
}
