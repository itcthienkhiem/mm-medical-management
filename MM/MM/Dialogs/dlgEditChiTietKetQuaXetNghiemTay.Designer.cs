namespace MM.Dialogs
{
    partial class dlgEditChiTietKetQuaXetNghiemTay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEditChiTietKetQuaXetNghiemTay));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLamThem = new System.Windows.Forms.CheckBox();
            this.txtTenXetNghiem = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._uNormal_Chung = new MM.Controls.uNormal_Chung();
            this._uNormal_SoiCanLangNuocTieu = new MM.Controls.uNormal_SoiCanLangNuocTieu();
            this.dtpkNgayXetNghiem = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.chkHutThuoc = new System.Windows.Forms.CheckBox();
            this.lbNormal = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbNormal);
            this.groupBox1.Controls.Add(this.chkHutThuoc);
            this.groupBox1.Controls.Add(this.dtpkNgayXetNghiem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._uNormal_SoiCanLangNuocTieu);
            this.groupBox1.Controls.Add(this._uNormal_Chung);
            this.groupBox1.Controls.Add(this.chkLamThem);
            this.groupBox1.Controls.Add(this.txtTenXetNghiem);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 148);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkLamThem
            // 
            this.chkLamThem.AutoSize = true;
            this.chkLamThem.Location = new System.Drawing.Point(101, 90);
            this.chkLamThem.Name = "chkLamThem";
            this.chkLamThem.Size = new System.Drawing.Size(72, 17);
            this.chkLamThem.TabIndex = 4;
            this.chkLamThem.Text = "Làm thêm";
            this.chkLamThem.UseVisualStyleBackColor = true;
            // 
            // txtTenXetNghiem
            // 
            this.txtTenXetNghiem.Location = new System.Drawing.Point(102, 42);
            this.txtTenXetNghiem.Name = "txtTenXetNghiem";
            this.txtTenXetNghiem.ReadOnly = true;
            this.txtTenXetNghiem.Size = new System.Drawing.Size(452, 20);
            this.txtTenXetNghiem.TabIndex = 2;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(102, 66);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(122, 20);
            this.txtResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kết quả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xét nghiệm:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(290, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(211, 157);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // _uNormal_Chung
            // 
            this._uNormal_Chung.DonVi = "";
            this._uNormal_Chung.FromOperator = "<=";
            this._uNormal_Chung.FromValue = 0D;
            this._uNormal_Chung.FromValueChecked = false;
            this._uNormal_Chung.Location = new System.Drawing.Point(102, 112);
            this._uNormal_Chung.Name = "_uNormal_Chung";
            this._uNormal_Chung.Size = new System.Drawing.Size(452, 24);
            this._uNormal_Chung.TabIndex = 6;
            this._uNormal_Chung.ToOperator = "<=";
            this._uNormal_Chung.ToValue = 0D;
            this._uNormal_Chung.ToValueChecked = false;
            this._uNormal_Chung.Visible = false;
            // 
            // _uNormal_SoiCanLangNuocTieu
            // 
            this._uNormal_SoiCanLangNuocTieu.FromToChecked = true;
            this._uNormal_SoiCanLangNuocTieu.FromValue = 0D;
            this._uNormal_SoiCanLangNuocTieu.Location = new System.Drawing.Point(102, 113);
            this._uNormal_SoiCanLangNuocTieu.Name = "_uNormal_SoiCanLangNuocTieu";
            this._uNormal_SoiCanLangNuocTieu.Size = new System.Drawing.Size(242, 22);
            this._uNormal_SoiCanLangNuocTieu.TabIndex = 7;
            this._uNormal_SoiCanLangNuocTieu.ToValue = 10D;
            this._uNormal_SoiCanLangNuocTieu.Visible = false;
            this._uNormal_SoiCanLangNuocTieu.XValue = 40D;
            // 
            // dtpkNgayXetNghiem
            // 
            this.dtpkNgayXetNghiem.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkNgayXetNghiem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayXetNghiem.Location = new System.Drawing.Point(102, 18);
            this.dtpkNgayXetNghiem.Name = "dtpkNgayXetNghiem";
            this.dtpkNgayXetNghiem.Size = new System.Drawing.Size(143, 20);
            this.dtpkNgayXetNghiem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Ngày xét nghiệm:";
            // 
            // chkHutThuoc
            // 
            this.chkHutThuoc.AutoSize = true;
            this.chkHutThuoc.Location = new System.Drawing.Point(179, 90);
            this.chkHutThuoc.Name = "chkHutThuoc";
            this.chkHutThuoc.Size = new System.Drawing.Size(73, 17);
            this.chkHutThuoc.TabIndex = 5;
            this.chkHutThuoc.Text = "Hút thuốc";
            this.chkHutThuoc.UseVisualStyleBackColor = true;
            // 
            // lbNormal
            // 
            this.lbNormal.AutoSize = true;
            this.lbNormal.Location = new System.Drawing.Point(31, 116);
            this.lbNormal.Name = "lbNormal";
            this.lbNormal.Size = new System.Drawing.Size(67, 13);
            this.lbNormal.TabIndex = 18;
            this.lbNormal.Text = "Bình thường:";
            this.lbNormal.Visible = false;
            // 
            // dlgEditChiTietKetQuaXetNghiemTay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(577, 187);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgEditChiTietKetQuaXetNghiemTay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua chi tiet ket qua xet nghiem tay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgEditChiTietKetQuaXetNghiemTay_FormClosing);
            this.Load += new System.EventHandler(this.dlgEditChiTietKetQuaXetNghiemTay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTenXetNghiem;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkLamThem;
        private Controls.uNormal_SoiCanLangNuocTieu _uNormal_SoiCanLangNuocTieu;
        private Controls.uNormal_Chung _uNormal_Chung;
        private System.Windows.Forms.DateTimePicker dtpkNgayXetNghiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbNormal;
        private System.Windows.Forms.CheckBox chkHutThuoc;
    }
}