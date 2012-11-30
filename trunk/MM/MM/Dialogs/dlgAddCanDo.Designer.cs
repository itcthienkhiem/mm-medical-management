namespace MM.Dialogs
{
    partial class dlgAddCanDo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddCanDo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.raHieuChinh = new System.Windows.Forms.RadioButton();
            this.raKhongHieuChinh = new System.Windows.Forms.RadioButton();
            this.txtMatTrai = new System.Windows.Forms.TextBox();
            this.txtMatPhai = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMuMau = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBMI = new System.Windows.Forms.TextBox();
            this.txtCanNang = new System.Windows.Forms.TextBox();
            this.txtChieuCao = new System.Windows.Forms.TextBox();
            this.txtHoHap = new System.Windows.Forms.TextBox();
            this.txtHuyetAp = new System.Windows.Forms.TextBox();
            this.txtTimMach = new System.Windows.Forms.TextBox();
            this.dtpkNgayCanDo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.raHieuChinh);
            this.groupBox1.Controls.Add(this.raKhongHieuChinh);
            this.groupBox1.Controls.Add(this.txtMatTrai);
            this.groupBox1.Controls.Add(this.txtMatPhai);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMuMau);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtBMI);
            this.groupBox1.Controls.Add(this.txtCanNang);
            this.groupBox1.Controls.Add(this.txtChieuCao);
            this.groupBox1.Controls.Add(this.txtHoHap);
            this.groupBox1.Controls.Add(this.txtHuyetAp);
            this.groupBox1.Controls.Add(this.txtTimMach);
            this.groupBox1.Controls.Add(this.dtpkNgayCanDo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin cân đo";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "FullName";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(96, 46);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(242, 21);
            this.cboDocStaff.TabIndex = 2;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Người khám:";
            // 
            // raHieuChinh
            // 
            this.raHieuChinh.AutoSize = true;
            this.raHieuChinh.Location = new System.Drawing.Point(101, 312);
            this.raHieuChinh.Name = "raHieuChinh";
            this.raHieuChinh.Size = new System.Drawing.Size(76, 17);
            this.raHieuChinh.TabIndex = 24;
            this.raHieuChinh.Text = "Hiệu chỉnh";
            this.raHieuChinh.UseVisualStyleBackColor = true;
            // 
            // raKhongHieuChinh
            // 
            this.raKhongHieuChinh.AutoSize = true;
            this.raKhongHieuChinh.Checked = true;
            this.raKhongHieuChinh.Location = new System.Drawing.Point(101, 289);
            this.raKhongHieuChinh.Name = "raKhongHieuChinh";
            this.raKhongHieuChinh.Size = new System.Drawing.Size(108, 17);
            this.raKhongHieuChinh.TabIndex = 23;
            this.raKhongHieuChinh.TabStop = true;
            this.raKhongHieuChinh.Text = "Không hiệu chỉnh";
            this.raKhongHieuChinh.UseVisualStyleBackColor = true;
            // 
            // txtMatTrai
            // 
            this.txtMatTrai.Location = new System.Drawing.Point(133, 263);
            this.txtMatTrai.MaxLength = 50;
            this.txtMatTrai.Name = "txtMatTrai";
            this.txtMatTrai.Size = new System.Drawing.Size(205, 20);
            this.txtMatTrai.TabIndex = 22;
            // 
            // txtMatPhai
            // 
            this.txtMatPhai.Location = new System.Drawing.Point(133, 239);
            this.txtMatPhai.MaxLength = 50;
            this.txtMatPhai.Name = "txtMatPhai";
            this.txtMatPhai.Size = new System.Drawing.Size(205, 20);
            this.txtMatPhai.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(98, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "L(T):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "R(P):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(46, 241);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Thị lực:";
            // 
            // txtMuMau
            // 
            this.txtMuMau.Location = new System.Drawing.Point(96, 215);
            this.txtMuMau.MaxLength = 50;
            this.txtMuMau.Name = "txtMuMau";
            this.txtMuMau.Size = new System.Drawing.Size(242, 20);
            this.txtMuMau.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Mù màu:";
            // 
            // txtBMI
            // 
            this.txtBMI.Location = new System.Drawing.Point(96, 191);
            this.txtBMI.MaxLength = 50;
            this.txtBMI.Name = "txtBMI";
            this.txtBMI.Size = new System.Drawing.Size(242, 20);
            this.txtBMI.TabIndex = 14;
            // 
            // txtCanNang
            // 
            this.txtCanNang.Location = new System.Drawing.Point(96, 167);
            this.txtCanNang.MaxLength = 50;
            this.txtCanNang.Name = "txtCanNang";
            this.txtCanNang.Size = new System.Drawing.Size(242, 20);
            this.txtCanNang.TabIndex = 13;
            this.txtCanNang.TextChanged += new System.EventHandler(this.txtCanNang_TextChanged);
            // 
            // txtChieuCao
            // 
            this.txtChieuCao.Location = new System.Drawing.Point(96, 143);
            this.txtChieuCao.MaxLength = 50;
            this.txtChieuCao.Name = "txtChieuCao";
            this.txtChieuCao.Size = new System.Drawing.Size(242, 20);
            this.txtChieuCao.TabIndex = 12;
            this.txtChieuCao.TextChanged += new System.EventHandler(this.txtChieuCao_TextChanged);
            // 
            // txtHoHap
            // 
            this.txtHoHap.Location = new System.Drawing.Point(96, 119);
            this.txtHoHap.MaxLength = 50;
            this.txtHoHap.Name = "txtHoHap";
            this.txtHoHap.Size = new System.Drawing.Size(242, 20);
            this.txtHoHap.TabIndex = 11;
            // 
            // txtHuyetAp
            // 
            this.txtHuyetAp.Location = new System.Drawing.Point(96, 95);
            this.txtHuyetAp.MaxLength = 50;
            this.txtHuyetAp.Name = "txtHuyetAp";
            this.txtHuyetAp.Size = new System.Drawing.Size(242, 20);
            this.txtHuyetAp.TabIndex = 10;
            // 
            // txtTimMach
            // 
            this.txtTimMach.Location = new System.Drawing.Point(96, 71);
            this.txtTimMach.MaxLength = 50;
            this.txtTimMach.Name = "txtTimMach";
            this.txtTimMach.Size = new System.Drawing.Size(242, 20);
            this.txtTimMach.TabIndex = 9;
            // 
            // dtpkNgayCanDo
            // 
            this.dtpkNgayCanDo.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayCanDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayCanDo.Location = new System.Drawing.Point(96, 22);
            this.dtpkNgayCanDo.Name = "dtpkNgayCanDo";
            this.dtpkNgayCanDo.Size = new System.Drawing.Size(106, 20);
            this.dtpkNgayCanDo.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "BMI:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Cân nặng:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Chiều cao:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hô hấp:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Huyết áp:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mạch:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày cân đo:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(185, 350);
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
            this.btnOK.Location = new System.Drawing.Point(106, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddCanDo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(366, 380);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddCanDo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them can do";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddCanDo_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddCanDo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBMI;
        private System.Windows.Forms.TextBox txtCanNang;
        private System.Windows.Forms.TextBox txtChieuCao;
        private System.Windows.Forms.TextBox txtHoHap;
        private System.Windows.Forms.TextBox txtHuyetAp;
        private System.Windows.Forms.TextBox txtTimMach;
        private System.Windows.Forms.DateTimePicker dtpkNgayCanDo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton raHieuChinh;
        private System.Windows.Forms.RadioButton raKhongHieuChinh;
        private System.Windows.Forms.TextBox txtMatTrai;
        private System.Windows.Forms.TextBox txtMatPhai;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMuMau;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
    }
}