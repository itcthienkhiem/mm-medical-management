namespace MM.Dialogs
{
    partial class dlgAddServiceHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddServiceHistory));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChonBenhNhan = new System.Windows.Forms.Button();
            this.cboChuyenNhuong = new System.Windows.Forms.ComboBox();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.btnChonDichVu = new System.Windows.Forms.Button();
            this.chkBSCD = new System.Windows.Forms.CheckBox();
            this.cboBacSiChiDinh = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.raNegative = new System.Windows.Forms.RadioButton();
            this.raNormal = new System.Windows.Forms.RadioButton();
            this.gbNegative = new System.Windows.Forms.GroupBox();
            this.chkPositive = new System.Windows.Forms.CheckBox();
            this.chkNegative = new System.Windows.Forms.CheckBox();
            this.gbNormal = new System.Windows.Forms.GroupBox();
            this.chkAbnormal = new System.Windows.Forms.CheckBox();
            this.chkNormal = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkActiveDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.cboService = new System.Windows.Forms.ComboBox();
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbUnit = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.gbNegative.SuspendLayout();
            this.gbNormal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChonBenhNhan);
            this.groupBox1.Controls.Add(this.cboChuyenNhuong);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnChonDichVu);
            this.groupBox1.Controls.Add(this.chkBSCD);
            this.groupBox1.Controls.Add(this.cboBacSiChiDinh);
            this.groupBox1.Controls.Add(this.raNegative);
            this.groupBox1.Controls.Add(this.raNormal);
            this.groupBox1.Controls.Add(this.gbNegative);
            this.groupBox1.Controls.Add(this.gbNormal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numDiscount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpkActiveDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.cboService);
            this.groupBox1.Controls.Add(this.lbUnit);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbPrice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 411);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin dịch vụ";
            // 
            // btnChonBenhNhan
            // 
            this.btnChonBenhNhan.Location = new System.Drawing.Point(386, 92);
            this.btnChonBenhNhan.Name = "btnChonBenhNhan";
            this.btnChonBenhNhan.Size = new System.Drawing.Size(110, 23);
            this.btnChonBenhNhan.TabIndex = 53;
            this.btnChonBenhNhan.Text = "Chọn bệnh nhân...";
            this.btnChonBenhNhan.UseVisualStyleBackColor = true;
            this.btnChonBenhNhan.Click += new System.EventHandler(this.btnChonBenhNhan_Click);
            // 
            // cboChuyenNhuong
            // 
            this.cboChuyenNhuong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboChuyenNhuong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboChuyenNhuong.DataSource = this.patientViewBindingSource;
            this.cboChuyenNhuong.DisplayMember = "FullName";
            this.cboChuyenNhuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChuyenNhuong.FormattingEnabled = true;
            this.cboChuyenNhuong.Location = new System.Drawing.Point(113, 93);
            this.cboChuyenNhuong.Name = "cboChuyenNhuong";
            this.cboChuyenNhuong.Size = new System.Drawing.Size(269, 21);
            this.cboChuyenNhuong.TabIndex = 5;
            this.cboChuyenNhuong.ValueMember = "PatientGUID";
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Chuyển nhượng từ:";
            // 
            // btnChonDichVu
            // 
            this.btnChonDichVu.Location = new System.Drawing.Point(386, 20);
            this.btnChonDichVu.Name = "btnChonDichVu";
            this.btnChonDichVu.Size = new System.Drawing.Size(110, 23);
            this.btnChonDichVu.TabIndex = 1;
            this.btnChonDichVu.Text = "Chọn dịch vụ...";
            this.btnChonDichVu.UseVisualStyleBackColor = true;
            this.btnChonDichVu.Click += new System.EventHandler(this.btnChonDichVu_Click);
            // 
            // chkBSCD
            // 
            this.chkBSCD.AutoSize = true;
            this.chkBSCD.Location = new System.Drawing.Point(49, 71);
            this.chkBSCD.Name = "chkBSCD";
            this.chkBSCD.Size = new System.Drawing.Size(58, 17);
            this.chkBSCD.TabIndex = 3;
            this.chkBSCD.Text = "BSCĐ:";
            this.chkBSCD.UseVisualStyleBackColor = true;
            this.chkBSCD.CheckedChanged += new System.EventHandler(this.chkBSCD_CheckedChanged);
            // 
            // cboBacSiChiDinh
            // 
            this.cboBacSiChiDinh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBacSiChiDinh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBacSiChiDinh.DataSource = this.docStaffViewBindingSource;
            this.cboBacSiChiDinh.DisplayMember = "Fullname";
            this.cboBacSiChiDinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBacSiChiDinh.Enabled = false;
            this.cboBacSiChiDinh.FormattingEnabled = true;
            this.cboBacSiChiDinh.Location = new System.Drawing.Point(113, 69);
            this.cboBacSiChiDinh.Name = "cboBacSiChiDinh";
            this.cboBacSiChiDinh.Size = new System.Drawing.Size(269, 21);
            this.cboBacSiChiDinh.TabIndex = 4;
            this.cboBacSiChiDinh.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // raNegative
            // 
            this.raNegative.AutoSize = true;
            this.raNegative.Location = new System.Drawing.Point(108, 243);
            this.raNegative.Name = "raNegative";
            this.raNegative.Size = new System.Drawing.Size(14, 13);
            this.raNegative.TabIndex = 10;
            this.raNegative.UseVisualStyleBackColor = true;
            this.raNegative.CheckedChanged += new System.EventHandler(this.raNegative_CheckedChanged);
            // 
            // raNormal
            // 
            this.raNormal.AutoSize = true;
            this.raNormal.Checked = true;
            this.raNormal.Location = new System.Drawing.Point(108, 191);
            this.raNormal.Name = "raNormal";
            this.raNormal.Size = new System.Drawing.Size(14, 13);
            this.raNormal.TabIndex = 9;
            this.raNormal.TabStop = true;
            this.raNormal.UseVisualStyleBackColor = true;
            this.raNormal.CheckedChanged += new System.EventHandler(this.raNormal_CheckedChanged);
            // 
            // gbNegative
            // 
            this.gbNegative.Controls.Add(this.chkPositive);
            this.gbNegative.Controls.Add(this.chkNegative);
            this.gbNegative.Enabled = false;
            this.gbNegative.Location = new System.Drawing.Point(113, 243);
            this.gbNegative.Name = "gbNegative";
            this.gbNegative.Size = new System.Drawing.Size(269, 47);
            this.gbNegative.TabIndex = 12;
            this.gbNegative.TabStop = false;
            // 
            // chkPositive
            // 
            this.chkPositive.AutoSize = true;
            this.chkPositive.Location = new System.Drawing.Point(128, 19);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(80, 17);
            this.chkPositive.TabIndex = 3;
            this.chkPositive.Text = "Dương tính";
            this.chkPositive.UseVisualStyleBackColor = true;
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_CheckedChanged);
            // 
            // chkNegative
            // 
            this.chkNegative.AutoSize = true;
            this.chkNegative.Checked = true;
            this.chkNegative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNegative.Location = new System.Drawing.Point(20, 19);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(63, 17);
            this.chkNegative.TabIndex = 2;
            this.chkNegative.Text = "Âm tính";
            this.chkNegative.UseVisualStyleBackColor = true;
            this.chkNegative.CheckedChanged += new System.EventHandler(this.chkNegative_CheckedChanged);
            // 
            // gbNormal
            // 
            this.gbNormal.Controls.Add(this.chkAbnormal);
            this.gbNormal.Controls.Add(this.chkNormal);
            this.gbNormal.Location = new System.Drawing.Point(113, 189);
            this.gbNormal.Name = "gbNormal";
            this.gbNormal.Size = new System.Drawing.Size(269, 47);
            this.gbNormal.TabIndex = 11;
            this.gbNormal.TabStop = false;
            // 
            // chkAbnormal
            // 
            this.chkAbnormal.AutoSize = true;
            this.chkAbnormal.Location = new System.Drawing.Point(128, 19);
            this.chkAbnormal.Name = "chkAbnormal";
            this.chkAbnormal.Size = new System.Drawing.Size(78, 17);
            this.chkAbnormal.TabIndex = 1;
            this.chkAbnormal.Text = "Bất thường";
            this.chkAbnormal.UseVisualStyleBackColor = true;
            this.chkAbnormal.CheckedChanged += new System.EventHandler(this.chkAbnormal_CheckedChanged);
            // 
            // chkNormal
            // 
            this.chkNormal.AutoSize = true;
            this.chkNormal.Checked = true;
            this.chkNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNormal.Location = new System.Drawing.Point(20, 19);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(83, 17);
            this.chkNormal.TabIndex = 0;
            this.chkNormal.Text = "Bình thường";
            this.chkNormal.UseVisualStyleBackColor = true;
            this.chkNormal.CheckedChanged += new System.EventHandler(this.chkNormal_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(%)";
            // 
            // numDiscount
            // 
            this.numDiscount.DecimalPlaces = 1;
            this.numDiscount.Location = new System.Drawing.Point(113, 140);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(69, 20);
            this.numDiscount.TabIndex = 7;
            this.numDiscount.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Giảm:";
            // 
            // dtpkActiveDate
            // 
            this.dtpkActiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkActiveDate.Location = new System.Drawing.Point(113, 163);
            this.dtpkActiveDate.Name = "dtpkActiveDate";
            this.dtpkActiveDate.Size = new System.Drawing.Size(122, 20);
            this.dtpkActiveDate.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ngày sử dụng:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "Fullname";
            this.cboDocStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(113, 45);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(269, 21);
            this.cboDocStaff.TabIndex = 2;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // cboService
            // 
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.DataSource = this.serviceBindingSource;
            this.cboService.DisplayMember = "Name";
            this.cboService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(113, 21);
            this.cboService.Name = "cboService";
            this.cboService.Size = new System.Drawing.Size(269, 21);
            this.cboService.TabIndex = 0;
            this.cboService.ValueMember = "ServiceGUID";
            this.cboService.SelectedValueChanged += new System.EventHandler(this.cboService_SelectedValueChanged);
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(238, 120);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(36, 13);
            this.lbUnit.TabIndex = 8;
            this.lbUnit.Text = "(VNĐ)";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(113, 301);
            this.txtDescription.MaxLength = 4000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(269, 96);
            this.txtDescription.TabIndex = 13;
            // 
            // numPrice
            // 
            this.numPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(113, 117);
            this.numPrice.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(121, 20);
            this.numPrice.TabIndex = 6;
            this.numPrice.ThousandsSeparator = true;
            this.numPrice.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nhận xét:";
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Location = new System.Drawing.Point(81, 120);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(26, 13);
            this.lbPrice.TabIndex = 2;
            this.lbPrice.Text = "Giá:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bác sĩ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dịch vụ:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(261, 423);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(182, 423);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddServiceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(519, 453);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddServiceHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them su dung dich vu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddServiceHistory_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddServiceHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.gbNegative.ResumeLayout(false);
            this.gbNegative.PerformLayout();
            this.gbNormal.ResumeLayout(false);
            this.gbNormal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private System.Windows.Forms.DateTimePicker dtpkActiveDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton raNegative;
        private System.Windows.Forms.RadioButton raNormal;
        private System.Windows.Forms.GroupBox gbNegative;
        private System.Windows.Forms.CheckBox chkPositive;
        private System.Windows.Forms.CheckBox chkNegative;
        private System.Windows.Forms.GroupBox gbNormal;
        private System.Windows.Forms.CheckBox chkAbnormal;
        private System.Windows.Forms.CheckBox chkNormal;
        private System.Windows.Forms.ComboBox cboBacSiChiDinh;
        private System.Windows.Forms.CheckBox chkBSCD;
        private System.Windows.Forms.Button btnChonDichVu;
        private System.Windows.Forms.ComboBox cboChuyenNhuong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.Button btnChonBenhNhan;
    }
}