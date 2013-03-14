namespace MM.Dialogs
{
    partial class dlgAddThuocKeToa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddThuocKeToa));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChonThuoc = new System.Windows.Forms.Button();
            this.btnThuocThayThe = new System.Windows.Forms.Button();
            this.gbToaSanKhoa = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLieuDung = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbToaChung = new System.Windows.Forms.GroupBox();
            this.txtKhac_CachDungNote = new System.Windows.Forms.TextBox();
            this.txtKhac_TruocSauAnNote = new System.Windows.Forms.TextBox();
            this.chkUong = new System.Windows.Forms.CheckBox();
            this.chkTruocAn = new System.Windows.Forms.CheckBox();
            this.txtBoiNote = new System.Windows.Forms.TextBox();
            this.chkKhac_CachDung = new System.Windows.Forms.CheckBox();
            this.txtUongNote = new System.Windows.Forms.TextBox();
            this.chkBoi = new System.Windows.Forms.CheckBox();
            this.txtTruocAnNote = new System.Windows.Forms.TextBox();
            this.txtDatADNote = new System.Windows.Forms.TextBox();
            this.chkDatAD = new System.Windows.Forms.CheckBox();
            this.chkKhac_TruocSauAn = new System.Windows.Forms.CheckBox();
            this.txtSauAnNote = new System.Windows.Forms.TextBox();
            this.chkSauAn = new System.Windows.Forms.CheckBox();
            this.txtToiNote = new System.Windows.Forms.TextBox();
            this.chkToi = new System.Windows.Forms.CheckBox();
            this.txtChieuNote = new System.Windows.Forms.TextBox();
            this.chkChieu = new System.Windows.Forms.CheckBox();
            this.txtTruaNote = new System.Windows.Forms.TextBox();
            this.chkTrua = new System.Windows.Forms.CheckBox();
            this.txtSangNote = new System.Windows.Forms.TextBox();
            this.chkSang = new System.Windows.Forms.CheckBox();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cboThuoc = new System.Windows.Forms.ComboBox();
            this.thuocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).BeginInit();
            this.gbToaSanKhoa.SuspendLayout();
            this.gbToaChung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numSoLuongTon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnChonThuoc);
            this.groupBox1.Controls.Add(this.btnThuocThayThe);
            this.groupBox1.Controls.Add(this.gbToaSanKhoa);
            this.groupBox1.Controls.Add(this.gbToaChung);
            this.groupBox1.Controls.Add(this.numSoLuong);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboThuoc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin thuốc kê toa";
            // 
            // numSoLuongTon
            // 
            this.numSoLuongTon.Enabled = false;
            this.numSoLuongTon.Location = new System.Drawing.Point(289, 47);
            this.numSoLuongTon.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numSoLuongTon.Name = "numSoLuongTon";
            this.numSoLuongTon.Size = new System.Drawing.Size(86, 20);
            this.numSoLuongTon.TabIndex = 9;
            this.numSoLuongTon.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Số lượng tồn:";
            // 
            // btnChonThuoc
            // 
            this.btnChonThuoc.Location = new System.Drawing.Point(381, 21);
            this.btnChonThuoc.Name = "btnChonThuoc";
            this.btnChonThuoc.Size = new System.Drawing.Size(75, 23);
            this.btnChonThuoc.TabIndex = 7;
            this.btnChonThuoc.Text = "&Chọn thuốc";
            this.btnChonThuoc.UseVisualStyleBackColor = true;
            this.btnChonThuoc.Click += new System.EventHandler(this.btnChonThuoc_Click);
            // 
            // btnThuocThayThe
            // 
            this.btnThuocThayThe.Location = new System.Drawing.Point(460, 21);
            this.btnThuocThayThe.Name = "btnThuocThayThe";
            this.btnThuocThayThe.Size = new System.Drawing.Size(97, 23);
            this.btnThuocThayThe.TabIndex = 6;
            this.btnThuocThayThe.Text = "&Thuốc thay thế";
            this.btnThuocThayThe.UseVisualStyleBackColor = true;
            this.btnThuocThayThe.Click += new System.EventHandler(this.btnThuocThayThe_Click);
            // 
            // gbToaSanKhoa
            // 
            this.gbToaSanKhoa.Controls.Add(this.txtGhiChu);
            this.gbToaSanKhoa.Controls.Add(this.label4);
            this.gbToaSanKhoa.Controls.Add(this.txtLieuDung);
            this.gbToaSanKhoa.Controls.Add(this.label3);
            this.gbToaSanKhoa.Enabled = false;
            this.gbToaSanKhoa.Location = new System.Drawing.Point(20, 223);
            this.gbToaSanKhoa.Name = "gbToaSanKhoa";
            this.gbToaSanKhoa.Size = new System.Drawing.Size(585, 82);
            this.gbToaSanKhoa.TabIndex = 5;
            this.gbToaSanKhoa.TabStop = false;
            this.gbToaSanKhoa.Text = "Toa sản khoa";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(75, 47);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(494, 20);
            this.txtGhiChu.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ghi chú";
            // 
            // txtLieuDung
            // 
            this.txtLieuDung.Location = new System.Drawing.Point(75, 23);
            this.txtLieuDung.Name = "txtLieuDung";
            this.txtLieuDung.Size = new System.Drawing.Size(494, 20);
            this.txtLieuDung.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Liều dùng:";
            // 
            // gbToaChung
            // 
            this.gbToaChung.Controls.Add(this.txtKhac_CachDungNote);
            this.gbToaChung.Controls.Add(this.txtKhac_TruocSauAnNote);
            this.gbToaChung.Controls.Add(this.chkUong);
            this.gbToaChung.Controls.Add(this.chkTruocAn);
            this.gbToaChung.Controls.Add(this.txtBoiNote);
            this.gbToaChung.Controls.Add(this.chkKhac_CachDung);
            this.gbToaChung.Controls.Add(this.txtUongNote);
            this.gbToaChung.Controls.Add(this.chkBoi);
            this.gbToaChung.Controls.Add(this.txtTruocAnNote);
            this.gbToaChung.Controls.Add(this.txtDatADNote);
            this.gbToaChung.Controls.Add(this.chkDatAD);
            this.gbToaChung.Controls.Add(this.chkKhac_TruocSauAn);
            this.gbToaChung.Controls.Add(this.txtSauAnNote);
            this.gbToaChung.Controls.Add(this.chkSauAn);
            this.gbToaChung.Controls.Add(this.txtToiNote);
            this.gbToaChung.Controls.Add(this.chkToi);
            this.gbToaChung.Controls.Add(this.txtChieuNote);
            this.gbToaChung.Controls.Add(this.chkChieu);
            this.gbToaChung.Controls.Add(this.txtTruaNote);
            this.gbToaChung.Controls.Add(this.chkTrua);
            this.gbToaChung.Controls.Add(this.txtSangNote);
            this.gbToaChung.Controls.Add(this.chkSang);
            this.gbToaChung.Location = new System.Drawing.Point(20, 79);
            this.gbToaChung.Name = "gbToaChung";
            this.gbToaChung.Size = new System.Drawing.Size(585, 135);
            this.gbToaChung.TabIndex = 4;
            this.gbToaChung.TabStop = false;
            this.gbToaChung.Text = "Toa chung";
            // 
            // txtKhac_CachDungNote
            // 
            this.txtKhac_CachDungNote.Location = new System.Drawing.Point(464, 96);
            this.txtKhac_CachDungNote.MaxLength = 100;
            this.txtKhac_CachDungNote.Name = "txtKhac_CachDungNote";
            this.txtKhac_CachDungNote.ReadOnly = true;
            this.txtKhac_CachDungNote.Size = new System.Drawing.Size(105, 20);
            this.txtKhac_CachDungNote.TabIndex = 21;
            // 
            // txtKhac_TruocSauAnNote
            // 
            this.txtKhac_TruocSauAnNote.Location = new System.Drawing.Point(273, 72);
            this.txtKhac_TruocSauAnNote.MaxLength = 100;
            this.txtKhac_TruocSauAnNote.Name = "txtKhac_TruocSauAnNote";
            this.txtKhac_TruocSauAnNote.ReadOnly = true;
            this.txtKhac_TruocSauAnNote.Size = new System.Drawing.Size(105, 20);
            this.txtKhac_TruocSauAnNote.TabIndex = 13;
            // 
            // chkUong
            // 
            this.chkUong.AutoSize = true;
            this.chkUong.Location = new System.Drawing.Point(402, 26);
            this.chkUong.Name = "chkUong";
            this.chkUong.Size = new System.Drawing.Size(52, 17);
            this.chkUong.TabIndex = 14;
            this.chkUong.Text = "Uống";
            this.chkUong.UseVisualStyleBackColor = true;
            this.chkUong.CheckedChanged += new System.EventHandler(this.chkUong_CheckedChanged);
            // 
            // chkTruocAn
            // 
            this.chkTruocAn.AutoSize = true;
            this.chkTruocAn.Location = new System.Drawing.Point(201, 26);
            this.chkTruocAn.Name = "chkTruocAn";
            this.chkTruocAn.Size = new System.Drawing.Size(69, 17);
            this.chkTruocAn.TabIndex = 8;
            this.chkTruocAn.Text = "Trước ăn";
            this.chkTruocAn.UseVisualStyleBackColor = true;
            this.chkTruocAn.CheckedChanged += new System.EventHandler(this.chkTruocAn_CheckedChanged);
            // 
            // txtBoiNote
            // 
            this.txtBoiNote.Location = new System.Drawing.Point(464, 48);
            this.txtBoiNote.MaxLength = 100;
            this.txtBoiNote.Name = "txtBoiNote";
            this.txtBoiNote.ReadOnly = true;
            this.txtBoiNote.Size = new System.Drawing.Size(105, 20);
            this.txtBoiNote.TabIndex = 17;
            // 
            // chkKhac_CachDung
            // 
            this.chkKhac_CachDung.AutoSize = true;
            this.chkKhac_CachDung.Location = new System.Drawing.Point(402, 98);
            this.chkKhac_CachDung.Name = "chkKhac_CachDung";
            this.chkKhac_CachDung.Size = new System.Drawing.Size(51, 17);
            this.chkKhac_CachDung.TabIndex = 20;
            this.chkKhac_CachDung.Text = "Khác";
            this.chkKhac_CachDung.UseVisualStyleBackColor = true;
            this.chkKhac_CachDung.CheckedChanged += new System.EventHandler(this.chkKhac_CachDung_CheckedChanged);
            // 
            // txtUongNote
            // 
            this.txtUongNote.Location = new System.Drawing.Point(464, 24);
            this.txtUongNote.MaxLength = 100;
            this.txtUongNote.Name = "txtUongNote";
            this.txtUongNote.ReadOnly = true;
            this.txtUongNote.Size = new System.Drawing.Size(105, 20);
            this.txtUongNote.TabIndex = 15;
            // 
            // chkBoi
            // 
            this.chkBoi.AutoSize = true;
            this.chkBoi.Location = new System.Drawing.Point(402, 50);
            this.chkBoi.Name = "chkBoi";
            this.chkBoi.Size = new System.Drawing.Size(41, 17);
            this.chkBoi.TabIndex = 16;
            this.chkBoi.Text = "Bôi";
            this.chkBoi.UseVisualStyleBackColor = true;
            this.chkBoi.CheckedChanged += new System.EventHandler(this.chkBoi_CheckedChanged);
            // 
            // txtTruocAnNote
            // 
            this.txtTruocAnNote.Location = new System.Drawing.Point(273, 24);
            this.txtTruocAnNote.MaxLength = 100;
            this.txtTruocAnNote.Name = "txtTruocAnNote";
            this.txtTruocAnNote.ReadOnly = true;
            this.txtTruocAnNote.Size = new System.Drawing.Size(105, 20);
            this.txtTruocAnNote.TabIndex = 9;
            // 
            // txtDatADNote
            // 
            this.txtDatADNote.Location = new System.Drawing.Point(464, 72);
            this.txtDatADNote.MaxLength = 100;
            this.txtDatADNote.Name = "txtDatADNote";
            this.txtDatADNote.ReadOnly = true;
            this.txtDatADNote.Size = new System.Drawing.Size(105, 20);
            this.txtDatADNote.TabIndex = 19;
            // 
            // chkDatAD
            // 
            this.chkDatAD.AutoSize = true;
            this.chkDatAD.Location = new System.Drawing.Point(402, 74);
            this.chkDatAD.Name = "chkDatAD";
            this.chkDatAD.Size = new System.Drawing.Size(61, 17);
            this.chkDatAD.TabIndex = 18;
            this.chkDatAD.Text = "Đặt AĐ";
            this.chkDatAD.UseVisualStyleBackColor = true;
            this.chkDatAD.CheckedChanged += new System.EventHandler(this.chkDatAD_CheckedChanged);
            // 
            // chkKhac_TruocSauAn
            // 
            this.chkKhac_TruocSauAn.AutoSize = true;
            this.chkKhac_TruocSauAn.Location = new System.Drawing.Point(201, 73);
            this.chkKhac_TruocSauAn.Name = "chkKhac_TruocSauAn";
            this.chkKhac_TruocSauAn.Size = new System.Drawing.Size(51, 17);
            this.chkKhac_TruocSauAn.TabIndex = 12;
            this.chkKhac_TruocSauAn.Text = "Khác";
            this.chkKhac_TruocSauAn.UseVisualStyleBackColor = true;
            this.chkKhac_TruocSauAn.CheckedChanged += new System.EventHandler(this.chkKhac_TruocSauAn_CheckedChanged);
            // 
            // txtSauAnNote
            // 
            this.txtSauAnNote.Location = new System.Drawing.Point(273, 48);
            this.txtSauAnNote.MaxLength = 100;
            this.txtSauAnNote.Name = "txtSauAnNote";
            this.txtSauAnNote.ReadOnly = true;
            this.txtSauAnNote.Size = new System.Drawing.Size(105, 20);
            this.txtSauAnNote.TabIndex = 11;
            // 
            // chkSauAn
            // 
            this.chkSauAn.AutoSize = true;
            this.chkSauAn.Location = new System.Drawing.Point(201, 50);
            this.chkSauAn.Name = "chkSauAn";
            this.chkSauAn.Size = new System.Drawing.Size(60, 17);
            this.chkSauAn.TabIndex = 10;
            this.chkSauAn.Text = "Sau ăn";
            this.chkSauAn.UseVisualStyleBackColor = true;
            this.chkSauAn.CheckedChanged += new System.EventHandler(this.chkSauAn_CheckedChanged);
            // 
            // txtToiNote
            // 
            this.txtToiNote.Location = new System.Drawing.Point(75, 96);
            this.txtToiNote.MaxLength = 100;
            this.txtToiNote.Name = "txtToiNote";
            this.txtToiNote.ReadOnly = true;
            this.txtToiNote.Size = new System.Drawing.Size(105, 20);
            this.txtToiNote.TabIndex = 7;
            // 
            // chkToi
            // 
            this.chkToi.AutoSize = true;
            this.chkToi.Location = new System.Drawing.Point(20, 98);
            this.chkToi.Name = "chkToi";
            this.chkToi.Size = new System.Drawing.Size(41, 17);
            this.chkToi.TabIndex = 6;
            this.chkToi.Text = "Tối";
            this.chkToi.UseVisualStyleBackColor = true;
            this.chkToi.CheckedChanged += new System.EventHandler(this.chkToi_CheckedChanged);
            // 
            // txtChieuNote
            // 
            this.txtChieuNote.Location = new System.Drawing.Point(75, 72);
            this.txtChieuNote.MaxLength = 100;
            this.txtChieuNote.Name = "txtChieuNote";
            this.txtChieuNote.ReadOnly = true;
            this.txtChieuNote.Size = new System.Drawing.Size(105, 20);
            this.txtChieuNote.TabIndex = 5;
            // 
            // chkChieu
            // 
            this.chkChieu.AutoSize = true;
            this.chkChieu.Location = new System.Drawing.Point(20, 74);
            this.chkChieu.Name = "chkChieu";
            this.chkChieu.Size = new System.Drawing.Size(53, 17);
            this.chkChieu.TabIndex = 4;
            this.chkChieu.Text = "Chiều";
            this.chkChieu.UseVisualStyleBackColor = true;
            this.chkChieu.CheckedChanged += new System.EventHandler(this.chkChieu_CheckedChanged);
            // 
            // txtTruaNote
            // 
            this.txtTruaNote.Location = new System.Drawing.Point(75, 48);
            this.txtTruaNote.MaxLength = 100;
            this.txtTruaNote.Name = "txtTruaNote";
            this.txtTruaNote.ReadOnly = true;
            this.txtTruaNote.Size = new System.Drawing.Size(105, 20);
            this.txtTruaNote.TabIndex = 3;
            // 
            // chkTrua
            // 
            this.chkTrua.AutoSize = true;
            this.chkTrua.Location = new System.Drawing.Point(20, 50);
            this.chkTrua.Name = "chkTrua";
            this.chkTrua.Size = new System.Drawing.Size(48, 17);
            this.chkTrua.TabIndex = 2;
            this.chkTrua.Text = "Trưa";
            this.chkTrua.UseVisualStyleBackColor = true;
            this.chkTrua.CheckedChanged += new System.EventHandler(this.chkTrua_CheckedChanged);
            // 
            // txtSangNote
            // 
            this.txtSangNote.Location = new System.Drawing.Point(75, 24);
            this.txtSangNote.MaxLength = 100;
            this.txtSangNote.Name = "txtSangNote";
            this.txtSangNote.ReadOnly = true;
            this.txtSangNote.Size = new System.Drawing.Size(105, 20);
            this.txtSangNote.TabIndex = 1;
            // 
            // chkSang
            // 
            this.chkSang.AutoSize = true;
            this.chkSang.Location = new System.Drawing.Point(20, 26);
            this.chkSang.Name = "chkSang";
            this.chkSang.Size = new System.Drawing.Size(51, 17);
            this.chkSang.TabIndex = 0;
            this.chkSang.Text = "Sáng";
            this.chkSang.UseVisualStyleBackColor = true;
            this.chkSang.CheckedChanged += new System.EventHandler(this.chkSang_CheckedChanged);
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(80, 47);
            this.numSoLuong.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(105, 20);
            this.numSoLuong.TabIndex = 3;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số lượng:";
            // 
            // cboThuoc
            // 
            this.cboThuoc.DataSource = this.thuocBindingSource;
            this.cboThuoc.DisplayMember = "TenThuoc";
            this.cboThuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new System.Drawing.Point(80, 22);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new System.Drawing.Size(295, 21);
            this.cboThuoc.TabIndex = 1;
            this.cboThuoc.ValueMember = "ThuocGUID";
            this.cboThuoc.SelectedIndexChanged += new System.EventHandler(this.cboThuoc_SelectedIndexChanged);
            // 
            // thuocBindingSource
            // 
            this.thuocBindingSource.DataSource = typeof(MM.Databasae.Thuoc);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên thuốc:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(317, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(238, 332);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddThuocKeToa
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(636, 362);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddThuocKeToa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them thuoc ke toa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddThuocKeToa_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddThuocKeToa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).EndInit();
            this.gbToaSanKhoa.ResumeLayout(false);
            this.gbToaSanKhoa.PerformLayout();
            this.gbToaChung.ResumeLayout(false);
            this.gbToaChung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboThuoc;
        private System.Windows.Forms.BindingSource thuocBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbToaSanKhoa;
        private System.Windows.Forms.GroupBox gbToaChung;
        private System.Windows.Forms.CheckBox chkKhac_CachDung;
        private System.Windows.Forms.TextBox txtDatADNote;
        private System.Windows.Forms.CheckBox chkDatAD;
        private System.Windows.Forms.TextBox txtBoiNote;
        private System.Windows.Forms.CheckBox chkBoi;
        private System.Windows.Forms.TextBox txtUongNote;
        private System.Windows.Forms.CheckBox chkUong;
        private System.Windows.Forms.CheckBox chkKhac_TruocSauAn;
        private System.Windows.Forms.TextBox txtSauAnNote;
        private System.Windows.Forms.CheckBox chkSauAn;
        private System.Windows.Forms.TextBox txtTruocAnNote;
        private System.Windows.Forms.CheckBox chkTruocAn;
        private System.Windows.Forms.TextBox txtToiNote;
        private System.Windows.Forms.CheckBox chkToi;
        private System.Windows.Forms.TextBox txtChieuNote;
        private System.Windows.Forms.CheckBox chkChieu;
        private System.Windows.Forms.TextBox txtTruaNote;
        private System.Windows.Forms.CheckBox chkTrua;
        private System.Windows.Forms.TextBox txtSangNote;
        private System.Windows.Forms.CheckBox chkSang;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLieuDung;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnThuocThayThe;
        private System.Windows.Forms.TextBox txtKhac_CachDungNote;
        private System.Windows.Forms.TextBox txtKhac_TruocSauAnNote;
        private System.Windows.Forms.Button btnChonThuoc;
        private System.Windows.Forms.NumericUpDown numSoLuongTon;
        private System.Windows.Forms.Label label5;
    }
}