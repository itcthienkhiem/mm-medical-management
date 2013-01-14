namespace MM.Dialogs
{
    partial class dlgEditBenhNhanNgoaiGoiKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEditBenhNhanNgoaiGoiKham));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboLanDauTaiKham = new System.Windows.Forms.ComboBox();
            this.cboService = new System.Windows.Forms.ComboBox();
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboBenhNhan = new System.Windows.Forms.ComboBox();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtpkNgayKham = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboHopDong = new System.Windows.Forms.ComboBox();
            this.companyContractViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboHopDong);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboLanDauTaiKham);
            this.groupBox1.Controls.Add(this.cboService);
            this.groupBox1.Controls.Add(this.cboBenhNhan);
            this.groupBox1.Controls.Add(this.dtpkNgayKham);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cboLanDauTaiKham
            // 
            this.cboLanDauTaiKham.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLanDauTaiKham.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLanDauTaiKham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanDauTaiKham.FormattingEnabled = true;
            this.cboLanDauTaiKham.Items.AddRange(new object[] {
            "Lần đầu",
            "Tái khám"});
            this.cboLanDauTaiKham.Location = new System.Drawing.Point(111, 118);
            this.cboLanDauTaiKham.Name = "cboLanDauTaiKham";
            this.cboLanDauTaiKham.Size = new System.Drawing.Size(151, 21);
            this.cboLanDauTaiKham.TabIndex = 21;
            // 
            // cboService
            // 
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.DataSource = this.serviceBindingSource;
            this.cboService.DisplayMember = "Name";
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(111, 68);
            this.cboService.Name = "cboService";
            this.cboService.Size = new System.Drawing.Size(269, 21);
            this.cboService.TabIndex = 20;
            this.cboService.ValueMember = "ServiceGUID";
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // cboBenhNhan
            // 
            this.cboBenhNhan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBenhNhan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBenhNhan.DataSource = this.patientViewBindingSource;
            this.cboBenhNhan.DisplayMember = "Fullname";
            this.cboBenhNhan.FormattingEnabled = true;
            this.cboBenhNhan.Location = new System.Drawing.Point(111, 43);
            this.cboBenhNhan.Name = "cboBenhNhan";
            this.cboBenhNhan.Size = new System.Drawing.Size(269, 21);
            this.cboBenhNhan.TabIndex = 6;
            this.cboBenhNhan.ValueMember = "PatientGUID";
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // dtpkNgayKham
            // 
            this.dtpkNgayKham.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayKham.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayKham.Location = new System.Drawing.Point(111, 19);
            this.dtpkNgayKham.Name = "dtpkNgayKham";
            this.dtpkNgayKham.Size = new System.Drawing.Size(112, 20);
            this.dtpkNgayKham.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Lần đầu/Tái khám:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dịch vụ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bệnh nhân:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày khám:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(206, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(127, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Hợp đồng:";
            // 
            // cboHopDong
            // 
            this.cboHopDong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboHopDong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboHopDong.DataSource = this.companyContractViewBindingSource;
            this.cboHopDong.DisplayMember = "ContractName";
            this.cboHopDong.FormattingEnabled = true;
            this.cboHopDong.Location = new System.Drawing.Point(111, 93);
            this.cboHopDong.Name = "cboHopDong";
            this.cboHopDong.Size = new System.Drawing.Size(269, 21);
            this.cboHopDong.TabIndex = 23;
            this.cboHopDong.ValueMember = "CompanyContractGUID";
            // 
            // companyContractViewBindingSource
            // 
            this.companyContractViewBindingSource.DataSource = typeof(MM.Databasae.CompanyContractView);
            // 
            // dlgEditBenhNhanNgoaiGoiKham
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 192);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgEditBenhNhanNgoaiGoiKham";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua benh nhan ngoai goi kham";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgEditBenhNhanNgoaiGoiKham_FormClosing);
            this.Load += new System.EventHandler(this.dlgEditBenhNhanNgoaiGoiKham_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpkNgayKham;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBenhNhan;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private System.Windows.Forms.ComboBox cboLanDauTaiKham;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.ComboBox cboHopDong;
        private System.Windows.Forms.BindingSource companyContractViewBindingSource;
        private System.Windows.Forms.Label label5;
    }
}