namespace MM.Dialogs
{
    partial class dlgAddXuatKhoCapCuu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddXuatKhoCapCuu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numSoLuongXuat = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.dtpkNgayXuat = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKhoCapCuu = new System.Windows.Forms.ComboBox();
            this.khoCapCuuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoCapCuuBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numSoLuongXuat);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numSoLuongTon);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpkNgayHetHan);
            this.groupBox1.Controls.Add(this.dtpkNgayXuat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboKhoCapCuu);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 220);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin xuất kho cấp cứu";
            // 
            // numSoLuongXuat
            // 
            this.numSoLuongXuat.Enabled = false;
            this.numSoLuongXuat.Location = new System.Drawing.Point(94, 95);
            this.numSoLuongXuat.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numSoLuongXuat.Name = "numSoLuongXuat";
            this.numSoLuongXuat.Size = new System.Drawing.Size(112, 20);
            this.numSoLuongXuat.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Số lượng xuất:";
            // 
            // numSoLuongTon
            // 
            this.numSoLuongTon.Enabled = false;
            this.numSoLuongTon.Location = new System.Drawing.Point(304, 71);
            this.numSoLuongTon.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numSoLuongTon.Name = "numSoLuongTon";
            this.numSoLuongTon.Size = new System.Drawing.Size(112, 20);
            this.numSoLuongTon.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Số lượng tồn:";
            // 
            // dtpkNgayHetHan
            // 
            this.dtpkNgayHetHan.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayHetHan.Enabled = false;
            this.dtpkNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayHetHan.Location = new System.Drawing.Point(94, 71);
            this.dtpkNgayHetHan.Name = "dtpkNgayHetHan";
            this.dtpkNgayHetHan.Size = new System.Drawing.Size(112, 20);
            this.dtpkNgayHetHan.TabIndex = 62;
            // 
            // dtpkNgayXuat
            // 
            this.dtpkNgayXuat.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayXuat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayXuat.Location = new System.Drawing.Point(94, 22);
            this.dtpkNgayXuat.Name = "dtpkNgayXuat";
            this.dtpkNgayXuat.Size = new System.Drawing.Size(112, 20);
            this.dtpkNgayXuat.TabIndex = 61;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 56;
            this.label7.Text = "Ngày hết hạn:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Ngày xuất:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Tên cấp cứu:";
            // 
            // cboKhoCapCuu
            // 
            this.cboKhoCapCuu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboKhoCapCuu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboKhoCapCuu.DataSource = this.khoCapCuuBindingSource;
            this.cboKhoCapCuu.DisplayMember = "TenCapCuu";
            this.cboKhoCapCuu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoCapCuu.FormattingEnabled = true;
            this.cboKhoCapCuu.Location = new System.Drawing.Point(94, 46);
            this.cboKhoCapCuu.Name = "cboKhoCapCuu";
            this.cboKhoCapCuu.Size = new System.Drawing.Size(322, 21);
            this.cboKhoCapCuu.TabIndex = 52;
            this.cboKhoCapCuu.ValueMember = "KhoCapCuuGUID";
            this.cboKhoCapCuu.SelectedIndexChanged += new System.EventHandler(this.cboThuoc_SelectedIndexChanged);
            // 
            // khoCapCuuBindingSource
            // 
            this.khoCapCuuBindingSource.DataSource = typeof(MM.Databasae.KhoCapCuu);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(225, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(146, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(94, 119);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(322, 88);
            this.txtGhiChu.TabIndex = 68;
            // 
            // dlgAddXuatKhoCapCuu
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(446, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddXuatKhoCapCuu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them xuat kho cap cuu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddLoThuoc_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddLoThuoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khoCapCuuBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboKhoCapCuu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpkNgayHetHan;
        private System.Windows.Forms.DateTimePicker dtpkNgayXuat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource khoCapCuuBindingSource;
        private System.Windows.Forms.NumericUpDown numSoLuongTon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSoLuongXuat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label4;
    }
}