namespace MM.Dialogs
{
    partial class dlgAddPhieuThuHopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddPhieuThuHopDong));
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkDaThuTien = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numThu = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDichVu = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numCongNo = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnChonHopDong = new System.Windows.Forms.Button();
            this.txtTenCongTy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkNgayThu = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMaHopDong = new System.Windows.Forms.ComboBox();
            this.companyContractViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMaPhieuThu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Image = global::MM.Properties.Resources.invoice_icon;
            this.btnExportInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportInvoice.Location = new System.Drawing.Point(195, 298);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(106, 25);
            this.btnExportInvoice.TabIndex = 18;
            this.btnExportInvoice.Text = "      &Xuất hóa đơn";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            this.btnExportInvoice.Click += new System.EventHandler(this.btnExportInvoice_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(305, 298);
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
            this.btnOK.Location = new System.Drawing.Point(116, 298);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.chkDaThuTien);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.numThu);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtDichVu);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.numCongNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnChonHopDong);
            this.groupBox1.Controls.Add(this.txtTenCongTy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDiaChi);
            this.groupBox1.Controls.Add(this.txtTenKhachHang);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpkNgayThu);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboMaHopDong);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtMaPhieuThu);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 287);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu thu";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(103, 237);
            this.txtGhiChu.MaxLength = 500;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(346, 20);
            this.txtGhiChu.TabIndex = 74;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(52, 240);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 73;
            this.label15.Text = "Ghi chú:";
            // 
            // chkDaThuTien
            // 
            this.chkDaThuTien.AutoSize = true;
            this.chkDaThuTien.Checked = true;
            this.chkDaThuTien.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDaThuTien.Location = new System.Drawing.Point(103, 262);
            this.chkDaThuTien.Name = "chkDaThuTien";
            this.chkDaThuTien.Size = new System.Drawing.Size(78, 17);
            this.chkDaThuTien.TabIndex = 75;
            this.chkDaThuTien.Text = "Đã thu tiền";
            this.chkDaThuTien.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(228, 217);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 71;
            this.label14.Text = "(VNĐ)";
            // 
            // numThu
            // 
            this.numThu.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numThu.Location = new System.Drawing.Point(103, 213);
            this.numThu.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numThu.Name = "numThu";
            this.numThu.Size = new System.Drawing.Size(120, 20);
            this.numThu.TabIndex = 70;
            this.numThu.ThousandsSeparator = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 216);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 69;
            this.label13.Text = "Thu:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(455, 168);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "[*]";
            // 
            // txtDichVu
            // 
            this.txtDichVu.Location = new System.Drawing.Point(103, 165);
            this.txtDichVu.MaxLength = 500;
            this.txtDichVu.Name = "txtDichVu";
            this.txtDichVu.Size = new System.Drawing.Size(346, 20);
            this.txtDichVu.TabIndex = 67;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(52, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Dịch vụ:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 65;
            this.label10.Text = "(VNĐ)";
            // 
            // numCongNo
            // 
            this.numCongNo.Enabled = false;
            this.numCongNo.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCongNo.Location = new System.Drawing.Point(103, 189);
            this.numCongNo.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numCongNo.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.numCongNo.Name = "numCongNo";
            this.numCongNo.Size = new System.Drawing.Size(120, 20);
            this.numCongNo.TabIndex = 64;
            this.numCongNo.ThousandsSeparator = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Công nợ:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(455, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "[*]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(455, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 61;
            this.label7.Text = "[*]";
            // 
            // btnChonHopDong
            // 
            this.btnChonHopDong.Location = new System.Drawing.Point(346, 44);
            this.btnChonHopDong.Name = "btnChonHopDong";
            this.btnChonHopDong.Size = new System.Drawing.Size(105, 22);
            this.btnChonHopDong.TabIndex = 2;
            this.btnChonHopDong.Text = "Chọn hợp đồng...";
            this.btnChonHopDong.UseVisualStyleBackColor = true;
            this.btnChonHopDong.Click += new System.EventHandler(this.btnChonHopDong_Click);
            // 
            // txtTenCongTy
            // 
            this.txtTenCongTy.Location = new System.Drawing.Point(103, 117);
            this.txtTenCongTy.MaxLength = 255;
            this.txtTenCongTy.Name = "txtTenCongTy";
            this.txtTenCongTy.Size = new System.Drawing.Size(346, 20);
            this.txtTenCongTy.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Tên công ty:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(103, 141);
            this.txtDiaChi.MaxLength = 500;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(346, 20);
            this.txtDiaChi.TabIndex = 6;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(103, 93);
            this.txtTenKhachHang.MaxLength = 255;
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(346, 20);
            this.txtTenKhachHang.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Tên khách hàng:";
            // 
            // dtpkNgayThu
            // 
            this.dtpkNgayThu.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayThu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayThu.Location = new System.Drawing.Point(103, 69);
            this.dtpkNgayThu.Name = "dtpkNgayThu";
            this.dtpkNgayThu.Size = new System.Drawing.Size(107, 20);
            this.dtpkNgayThu.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Ngày thu:";
            // 
            // cboMaHopDong
            // 
            this.cboMaHopDong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboMaHopDong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMaHopDong.DataSource = this.companyContractViewBindingSource;
            this.cboMaHopDong.DisplayMember = "ContractName";
            this.cboMaHopDong.FormattingEnabled = true;
            this.cboMaHopDong.Location = new System.Drawing.Point(103, 44);
            this.cboMaHopDong.Name = "cboMaHopDong";
            this.cboMaHopDong.Size = new System.Drawing.Size(237, 21);
            this.cboMaHopDong.TabIndex = 1;
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
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Tên hợp đồng:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(272, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 13);
            this.label22.TabIndex = 49;
            this.label22.Text = "[*]";
            // 
            // txtMaPhieuThu
            // 
            this.txtMaPhieuThu.Location = new System.Drawing.Point(103, 20);
            this.txtMaPhieuThu.MaxLength = 50;
            this.txtMaPhieuThu.Name = "txtMaPhieuThu";
            this.txtMaPhieuThu.Size = new System.Drawing.Size(165, 20);
            this.txtMaPhieuThu.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Mã phiếu thu:";
            // 
            // dlgAddPhieuThuHopDong
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 328);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExportInvoice);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddPhieuThuHopDong";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them phieu thu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddPhieuThuHopDong_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddPhieuThuHopDong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExportInvoice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtMaPhieuThu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboMaHopDong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkNgayThu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenCongTy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnChonHopDong;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource companyContractViewBindingSource;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numCongNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numThu;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDichVu;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkDaThuTien;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label15;
    }
}