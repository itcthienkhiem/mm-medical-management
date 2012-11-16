namespace MM.Controls
{
    partial class uPatientList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uPatientList));
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
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._uPrintKetQuaSieuAm = new MM.Controls.uPrintKetQuaSieuAm();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTaoMatKhau = new System.Windows.Forms.Button();
            this.btnXemHoSo = new System.Windows.Forms.Button();
            this.btnUploadHoSo = new System.Windows.Forms.Button();
            this.btnTaoHoSo = new System.Windows.Forms.Button();
            this.btnVaoPhongCho = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnOpenPatient = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.chkTheoSoDienThoai = new System.Windows.Forms.CheckBox();
            this.chkMaBenhNhan = new System.Windows.Forms.CheckBox();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgPatient);
            this.panel3.Controls.Add(this._uPrintKetQuaSieuAm);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1039, 341);
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
            this.dgPatient.Size = new System.Drawing.Size(1039, 341);
            this.dgPatient.TabIndex = 2;
            this.dgPatient.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPatient_CellMouseUp);
            this.dgPatient.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPatient_ColumnHeaderMouseClick);
            this.dgPatient.DoubleClick += new System.EventHandler(this.dgDocStaff_DoubleClick);
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
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
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
            this.panel2.Controls.Add(this.btnTaoMatKhau);
            this.panel2.Controls.Add(this.btnXemHoSo);
            this.panel2.Controls.Add(this.btnUploadHoSo);
            this.panel2.Controls.Add(this.btnTaoHoSo);
            this.panel2.Controls.Add(this.btnVaoPhongCho);
            this.panel2.Controls.Add(this.btnImportExcel);
            this.panel2.Controls.Add(this.btnOpenPatient);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1039, 36);
            this.panel2.TabIndex = 5;
            // 
            // btnTaoMatKhau
            // 
            this.btnTaoMatKhau.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoMatKhau.Image")));
            this.btnTaoMatKhau.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaoMatKhau.Location = new System.Drawing.Point(855, 5);
            this.btnTaoMatKhau.Name = "btnTaoMatKhau";
            this.btnTaoMatKhau.Size = new System.Drawing.Size(101, 25);
            this.btnTaoMatKhau.TabIndex = 15;
            this.btnTaoMatKhau.Text = "      &Tạo mật khẩu";
            this.btnTaoMatKhau.UseVisualStyleBackColor = true;
            this.btnTaoMatKhau.Click += new System.EventHandler(this.btnTaoMatKhau_Click);
            // 
            // btnXemHoSo
            // 
            this.btnXemHoSo.Image = ((System.Drawing.Image)(resources.GetObject("btnXemHoSo.Image")));
            this.btnXemHoSo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemHoSo.Location = new System.Drawing.Point(661, 5);
            this.btnXemHoSo.Name = "btnXemHoSo";
            this.btnXemHoSo.Size = new System.Drawing.Size(86, 25);
            this.btnXemHoSo.TabIndex = 13;
            this.btnXemHoSo.Text = "      &Xem hồ sơ";
            this.btnXemHoSo.UseVisualStyleBackColor = true;
            this.btnXemHoSo.Click += new System.EventHandler(this.btnXemHoSo_Click);
            // 
            // btnUploadHoSo
            // 
            this.btnUploadHoSo.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadHoSo.Image")));
            this.btnUploadHoSo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadHoSo.Location = new System.Drawing.Point(751, 5);
            this.btnUploadHoSo.Name = "btnUploadHoSo";
            this.btnUploadHoSo.Size = new System.Drawing.Size(100, 25);
            this.btnUploadHoSo.TabIndex = 14;
            this.btnUploadHoSo.Text = "      &Upload hồ sơ";
            this.btnUploadHoSo.UseVisualStyleBackColor = true;
            this.btnUploadHoSo.Click += new System.EventHandler(this.btnUploadHoSo_Click);
            // 
            // btnTaoHoSo
            // 
            this.btnTaoHoSo.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoHoSo.Image")));
            this.btnTaoHoSo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaoHoSo.Location = new System.Drawing.Point(571, 5);
            this.btnTaoHoSo.Name = "btnTaoHoSo";
            this.btnTaoHoSo.Size = new System.Drawing.Size(86, 25);
            this.btnTaoHoSo.TabIndex = 12;
            this.btnTaoHoSo.Text = "      &Tạo hồ sơ";
            this.btnTaoHoSo.UseVisualStyleBackColor = true;
            this.btnTaoHoSo.Click += new System.EventHandler(this.btnTaoHoSo_Click);
            // 
            // btnVaoPhongCho
            // 
            this.btnVaoPhongCho.Image = global::MM.Properties.Resources.conference_icon;
            this.btnVaoPhongCho.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVaoPhongCho.Location = new System.Drawing.Point(452, 5);
            this.btnVaoPhongCho.Name = "btnVaoPhongCho";
            this.btnVaoPhongCho.Size = new System.Drawing.Size(115, 25);
            this.btnVaoPhongCho.TabIndex = 11;
            this.btnVaoPhongCho.Text = "      &Vào phòng chờ";
            this.btnVaoPhongCho.UseVisualStyleBackColor = true;
            this.btnVaoPhongCho.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Image = global::MM.Properties.Resources.Excel_icon;
            this.btnImportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportExcel.Location = new System.Drawing.Point(241, 5);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(95, 25);
            this.btnImportExcel.TabIndex = 10;
            this.btnImportExcel.Text = "      &Nhập Excel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnOpenPatient
            // 
            this.btnOpenPatient.Image = global::MM.Properties.Resources.folder_customer_icon__1_;
            this.btnOpenPatient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenPatient.Location = new System.Drawing.Point(340, 5);
            this.btnOpenPatient.Name = "btnOpenPatient";
            this.btnOpenPatient.Size = new System.Drawing.Size(108, 25);
            this.btnOpenPatient.TabIndex = 9;
            this.btnOpenPatient.Text = "      &Mở bệnh nhân";
            this.btnOpenPatient.UseVisualStyleBackColor = true;
            this.btnOpenPatient.Click += new System.EventHandler(this.btnOpenPatient_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(162, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(83, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "    &Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(73, 25);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel1.Controls.Add(this.chkTheoSoDienThoai);
            this.panel1.Controls.Add(this.chkMaBenhNhan);
            this.panel1.Controls.Add(this.txtSearchPatient);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 63);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::MM.Properties.Resources.Refresh_icon;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(94, 33);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "  Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            // uPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uPatientList";
            this.Size = new System.Drawing.Size(1039, 440);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpenPatient;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPatient;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.CheckBox chkMaBenhNhan;
        private System.Windows.Forms.Button btnVaoPhongCho;
        private System.Windows.Forms.CheckBox chkTheoSoDienThoai;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTaoHoSo;
        private uPrintKetQuaSieuAm _uPrintKetQuaSieuAm;
        private System.Windows.Forms.Button btnUploadHoSo;
        private System.Windows.Forms.Button btnXemHoSo;
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
        private System.Windows.Forms.Button btnTaoMatKhau;
    }
}
