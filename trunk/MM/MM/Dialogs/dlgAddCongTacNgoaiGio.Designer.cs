namespace MM.Dialogs
{
    partial class dlgAddCongTacNgoaiGio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddCongTacNgoaiGio));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboNguoiDeXuat = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtKetQuaDanhGia = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpkGioRa = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpkGioVao = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMucDich = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpkNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtTenNguoiLam = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTenNguoiLam);
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboNguoiDeXuat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtKetQuaDanhGia);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpkGioRa);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpkGioVao);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMucDich);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpkNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 222);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(107, 189);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(381, 20);
            this.txtGhiChu.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Ghi chú:";
            // 
            // cboNguoiDeXuat
            // 
            this.cboNguoiDeXuat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNguoiDeXuat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNguoiDeXuat.DataSource = this.docStaffViewBindingSource;
            this.cboNguoiDeXuat.DisplayMember = "Fullname";
            this.cboNguoiDeXuat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiDeXuat.FormattingEnabled = true;
            this.cboNguoiDeXuat.Location = new System.Drawing.Point(107, 164);
            this.cboNguoiDeXuat.Name = "cboNguoiDeXuat";
            this.cboNguoiDeXuat.Size = new System.Drawing.Size(238, 21);
            this.cboNguoiDeXuat.TabIndex = 16;
            this.cboNguoiDeXuat.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Người đề xuất:";
            // 
            // txtKetQuaDanhGia
            // 
            this.txtKetQuaDanhGia.Location = new System.Drawing.Point(107, 140);
            this.txtKetQuaDanhGia.Name = "txtKetQuaDanhGia";
            this.txtKetQuaDanhGia.Size = new System.Drawing.Size(381, 20);
            this.txtKetQuaDanhGia.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Kết quả/Đánh giá:";
            // 
            // dtpkGioRa
            // 
            this.dtpkGioRa.CustomFormat = "HH:mm";
            this.dtpkGioRa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkGioRa.Location = new System.Drawing.Point(107, 116);
            this.dtpkGioRa.Name = "dtpkGioRa";
            this.dtpkGioRa.ShowUpDown = true;
            this.dtpkGioRa.Size = new System.Drawing.Size(76, 20);
            this.dtpkGioRa.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Giờ ra:";
            // 
            // dtpkGioVao
            // 
            this.dtpkGioVao.CustomFormat = "HH:mm";
            this.dtpkGioVao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkGioVao.Location = new System.Drawing.Point(107, 92);
            this.dtpkGioVao.Name = "dtpkGioVao";
            this.dtpkGioVao.ShowUpDown = true;
            this.dtpkGioVao.Size = new System.Drawing.Size(76, 20);
            this.dtpkGioVao.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Giờ vào:";
            // 
            // txtMucDich
            // 
            this.txtMucDich.Location = new System.Drawing.Point(107, 68);
            this.txtMucDich.Name = "txtMucDich";
            this.txtMucDich.Size = new System.Drawing.Size(381, 20);
            this.txtMucDich.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mục đích:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên người làm:";
            // 
            // dtpkNgay
            // 
            this.dtpkNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgay.Location = new System.Drawing.Point(107, 19);
            this.dtpkNgay.Name = "dtpkNgay";
            this.dtpkNgay.Size = new System.Drawing.Size(103, 20);
            this.dtpkNgay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(261, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(182, 232);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtTenNguoiLam
            // 
            this.txtTenNguoiLam.Location = new System.Drawing.Point(107, 43);
            this.txtTenNguoiLam.Name = "txtTenNguoiLam";
            this.txtTenNguoiLam.Size = new System.Drawing.Size(238, 20);
            this.txtTenNguoiLam.TabIndex = 6;
            // 
            // dlgAddCongTacNgoaiGio
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(518, 264);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddCongTacNgoaiGio";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them cong tac ngoai gio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddCongTacNgoaiGio_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddCongTacNgoaiGio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpkNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkGioRa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpkGioVao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMucDich;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.TextBox txtKetQuaDanhGia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboNguoiDeXuat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtTenNguoiLam;
    }
}