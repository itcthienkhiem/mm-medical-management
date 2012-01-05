namespace MM.Dialogs
{
    partial class dlgAddPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddPatient));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDOB = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFileNum = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPreferredName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtKnownAs = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHomePhone = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIdentityCard = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPatient = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.txtTenCongTy = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.barCode = new DSBarCode.BarCodeCtrl();
            this.label23 = new System.Windows.Forms.Label();
            this.pagePatientInfo = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.chkDangCoThai = new System.Windows.Forms.CheckBox();
            this.chkChichNguaCum = new System.Windows.Forms.CheckBox();
            this.chkChichNguaUonVan = new System.Windows.Forms.CheckBox();
            this.chkChichNguaViemGanB = new System.Windows.Forms.CheckBox();
            this.txtTinhTrangGiaDinh = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.chkUongRuou = new System.Windows.Forms.CheckBox();
            this.chkHutThuoc = new System.Windows.Forms.CheckBox();
            this.txtThuocDangDung = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBenhGi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBenhKhac = new System.Windows.Forms.CheckBox();
            this.chkHenSuyen = new System.Windows.Forms.CheckBox();
            this.chkDongKinh = new System.Windows.Forms.CheckBox();
            this.txtCoQuanUngThu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkUngThu = new System.Windows.Forms.CheckBox();
            this.chkViemGanDangDieuTri = new System.Windows.Forms.CheckBox();
            this.chkViemGanC = new System.Windows.Forms.CheckBox();
            this.chkViemGanB = new System.Windows.Forms.CheckBox();
            this.chkDaiDuongDangDieuTri = new System.Windows.Forms.CheckBox();
            this.chkDaiThaoDuong = new System.Windows.Forms.CheckBox();
            this.chkBenhLao = new System.Windows.Forms.CheckBox();
            this.chkBenhTimMach = new System.Windows.Forms.CheckBox();
            this.chkDotQuy = new System.Windows.Forms.CheckBox();
            this.txtThuocDiUng = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDiUngThuoc = new System.Windows.Forms.CheckBox();
            this.pagePatientHistory = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabPatient)).BeginInit();
            this.tabPatient.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(146, 490);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(225, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(217, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "[*]";
            // 
            // txtDOB
            // 
            this.txtDOB.Location = new System.Drawing.Point(110, 119);
            this.txtDOB.MaxLength = 50;
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(104, 20);
            this.txtDOB.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(278, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 13);
            this.label19.TabIndex = 53;
            this.label19.Text = "[*]";
            // 
            // txtFileNum
            // 
            this.txtFileNum.Location = new System.Drawing.Point(110, 14);
            this.txtFileNum.MaxLength = 50;
            this.txtFileNum.Name = "txtFileNum";
            this.txtFileNum.Size = new System.Drawing.Size(165, 20);
            this.txtFileNum.TabIndex = 0;
            this.txtFileNum.TextChanged += new System.EventHandler(this.txtFileNum_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label20.Location = new System.Drawing.Point(21, 17);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 52;
            this.label20.Text = "Mã bệnh nhân:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(416, 99);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 13);
            this.label22.TabIndex = 42;
            this.label22.Text = "[*]";
            // 
            // txtOccupation
            // 
            this.txtOccupation.Location = new System.Drawing.Point(110, 240);
            this.txtOccupation.MaxLength = 255;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(157, 20);
            this.txtOccupation.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label18.Location = new System.Drawing.Point(29, 243);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Nghề nghiệp:";
            // 
            // txtPreferredName
            // 
            this.txtPreferredName.Location = new System.Drawing.Point(110, 192);
            this.txtPreferredName.MaxLength = 50;
            this.txtPreferredName.Name = "txtPreferredName";
            this.txtPreferredName.Size = new System.Drawing.Size(104, 20);
            this.txtPreferredName.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label17.Location = new System.Drawing.Point(18, 195);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Tên thường gọi:";
            // 
            // txtKnownAs
            // 
            this.txtKnownAs.Location = new System.Drawing.Point(110, 168);
            this.txtKnownAs.MaxLength = 50;
            this.txtKnownAs.Name = "txtKnownAs";
            this.txtKnownAs.Size = new System.Drawing.Size(104, 20);
            this.txtKnownAs.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label16.Location = new System.Drawing.Point(52, 171);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "Bí danh:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(110, 384);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(223, 20);
            this.txtEmail.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label15.Location = new System.Drawing.Point(65, 387);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Email:";
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(110, 360);
            this.txtFax.MaxLength = 50;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(157, 20);
            this.txtFax.TabIndex = 16;
            this.txtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHomePhone_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label14.Location = new System.Drawing.Point(73, 363);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Fax:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label13.Location = new System.Drawing.Point(43, 339);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Số ĐTDĐ:";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(110, 336);
            this.txtMobile.MaxLength = 50;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(157, 20);
            this.txtMobile.TabIndex = 15;
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHomePhone_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label12.Location = new System.Drawing.Point(17, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Số ĐT làm việc:";
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Location = new System.Drawing.Point(110, 288);
            this.txtHomePhone.MaxLength = 50;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(156, 20);
            this.txtHomePhone.TabIndex = 13;
            this.txtHomePhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHomePhone_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label11.Location = new System.Drawing.Point(38, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Số ĐT nhà:";
            // 
            // txtIdentityCard
            // 
            this.txtIdentityCard.Location = new System.Drawing.Point(110, 216);
            this.txtIdentityCard.MaxLength = 15;
            this.txtIdentityCard.Name = "txtIdentityCard";
            this.txtIdentityCard.Size = new System.Drawing.Size(157, 20);
            this.txtIdentityCard.TabIndex = 8;
            this.txtIdentityCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdentityCard_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label10.Location = new System.Drawing.Point(58, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "CMND:";
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(110, 312);
            this.txtWorkPhone.MaxLength = 50;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(156, 20);
            this.txtWorkPhone.TabIndex = 14;
            this.txtWorkPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHomePhone_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label9.Location = new System.Drawing.Point(43, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Ngày sinh:";
            // 
            // cboGender
            // 
            this.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Không xác định"});
            this.cboGender.Location = new System.Drawing.Point(110, 143);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(104, 21);
            this.cboGender.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label8.Location = new System.Drawing.Point(50, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Giới tính:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(110, 408);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(322, 20);
            this.txtAddress.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label4.Location = new System.Drawing.Point(57, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Địa chỉ:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(110, 96);
            this.txtFullName.MaxLength = 255;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(303, 20);
            this.txtFullName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label1.Location = new System.Drawing.Point(54, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ Tên:";
            // 
            // tabPatient
            // 
            this.tabPatient.CanReorderTabs = true;
            this.tabPatient.Controls.Add(this.tabControlPanel1);
            this.tabPatient.Controls.Add(this.tabControlPanel2);
            this.tabPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPatient.Location = new System.Drawing.Point(0, 0);
            this.tabPatient.Name = "tabPatient";
            this.tabPatient.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabPatient.SelectedTabIndex = 0;
            this.tabPatient.Size = new System.Drawing.Size(446, 485);
            this.tabPatient.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabPatient.TabIndex = 0;
            this.tabPatient.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabPatient.Tabs.Add(this.pagePatientInfo);
            this.tabPatient.Tabs.Add(this.pagePatientHistory);
            this.tabPatient.TabStop = false;
            this.tabPatient.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.txtTenCongTy);
            this.tabControlPanel1.Controls.Add(this.label24);
            this.tabControlPanel1.Controls.Add(this.barCode);
            this.tabControlPanel1.Controls.Add(this.label23);
            this.tabControlPanel1.Controls.Add(this.label2);
            this.tabControlPanel1.Controls.Add(this.txtFullName);
            this.tabControlPanel1.Controls.Add(this.txtDOB);
            this.tabControlPanel1.Controls.Add(this.label1);
            this.tabControlPanel1.Controls.Add(this.label19);
            this.tabControlPanel1.Controls.Add(this.label4);
            this.tabControlPanel1.Controls.Add(this.txtFileNum);
            this.tabControlPanel1.Controls.Add(this.txtAddress);
            this.tabControlPanel1.Controls.Add(this.label20);
            this.tabControlPanel1.Controls.Add(this.label8);
            this.tabControlPanel1.Controls.Add(this.label22);
            this.tabControlPanel1.Controls.Add(this.cboGender);
            this.tabControlPanel1.Controls.Add(this.txtOccupation);
            this.tabControlPanel1.Controls.Add(this.label9);
            this.tabControlPanel1.Controls.Add(this.label18);
            this.tabControlPanel1.Controls.Add(this.txtWorkPhone);
            this.tabControlPanel1.Controls.Add(this.txtPreferredName);
            this.tabControlPanel1.Controls.Add(this.label10);
            this.tabControlPanel1.Controls.Add(this.label17);
            this.tabControlPanel1.Controls.Add(this.txtIdentityCard);
            this.tabControlPanel1.Controls.Add(this.txtKnownAs);
            this.tabControlPanel1.Controls.Add(this.label11);
            this.tabControlPanel1.Controls.Add(this.label16);
            this.tabControlPanel1.Controls.Add(this.txtHomePhone);
            this.tabControlPanel1.Controls.Add(this.txtEmail);
            this.tabControlPanel1.Controls.Add(this.label12);
            this.tabControlPanel1.Controls.Add(this.label15);
            this.tabControlPanel1.Controls.Add(this.txtMobile);
            this.tabControlPanel1.Controls.Add(this.txtFax);
            this.tabControlPanel1.Controls.Add(this.label13);
            this.tabControlPanel1.Controls.Add(this.label14);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(446, 460);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.pagePatientInfo;
            // 
            // txtTenCongTy
            // 
            this.txtTenCongTy.Location = new System.Drawing.Point(110, 264);
            this.txtTenCongTy.MaxLength = 255;
            this.txtTenCongTy.Name = "txtTenCongTy";
            this.txtTenCongTy.Size = new System.Drawing.Size(303, 20);
            this.txtTenCongTy.TabIndex = 10;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label24.Location = new System.Drawing.Point(29, 267);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 13);
            this.label24.TabIndex = 59;
            this.label24.Text = "Tên công ty:";
            // 
            // barCode
            // 
            this.barCode.BarCode = "";
            this.barCode.BarCodeHeight = 40;
            this.barCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barCode.FooterFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.barCode.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.barCode.HeaderText = "";
            this.barCode.LeftMargin = 10;
            this.barCode.Location = new System.Drawing.Point(110, 38);
            this.barCode.Name = "barCode";
            this.barCode.ShowFooter = false;
            this.barCode.ShowHeader = false;
            this.barCode.Size = new System.Drawing.Size(324, 53);
            this.barCode.TabIndex = 57;
            this.barCode.TopMargin = 5;
            this.barCode.VertAlign = DSBarCode.BarCodeCtrl.AlignType.Left;
            this.barCode.Weight = DSBarCode.BarCodeCtrl.BarCodeWeight.Small;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label23.Location = new System.Drawing.Point(48, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(52, 13);
            this.label23.TabIndex = 56;
            this.label23.Text = "Mã vạch:";
            // 
            // pagePatientInfo
            // 
            this.pagePatientInfo.AttachedControl = this.tabControlPanel1;
            this.pagePatientInfo.Image = global::MM.Properties.Resources.personal_information_icon;
            this.pagePatientInfo.Name = "pagePatientInfo";
            this.pagePatientInfo.Text = "Thông tin bệnh nhân";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.chkDangCoThai);
            this.tabControlPanel2.Controls.Add(this.chkChichNguaCum);
            this.tabControlPanel2.Controls.Add(this.chkChichNguaUonVan);
            this.tabControlPanel2.Controls.Add(this.chkChichNguaViemGanB);
            this.tabControlPanel2.Controls.Add(this.txtTinhTrangGiaDinh);
            this.tabControlPanel2.Controls.Add(this.label21);
            this.tabControlPanel2.Controls.Add(this.chkUongRuou);
            this.tabControlPanel2.Controls.Add(this.chkHutThuoc);
            this.tabControlPanel2.Controls.Add(this.txtThuocDangDung);
            this.tabControlPanel2.Controls.Add(this.label7);
            this.tabControlPanel2.Controls.Add(this.txtBenhGi);
            this.tabControlPanel2.Controls.Add(this.label6);
            this.tabControlPanel2.Controls.Add(this.chkBenhKhac);
            this.tabControlPanel2.Controls.Add(this.chkHenSuyen);
            this.tabControlPanel2.Controls.Add(this.chkDongKinh);
            this.tabControlPanel2.Controls.Add(this.txtCoQuanUngThu);
            this.tabControlPanel2.Controls.Add(this.label5);
            this.tabControlPanel2.Controls.Add(this.chkUngThu);
            this.tabControlPanel2.Controls.Add(this.chkViemGanDangDieuTri);
            this.tabControlPanel2.Controls.Add(this.chkViemGanC);
            this.tabControlPanel2.Controls.Add(this.chkViemGanB);
            this.tabControlPanel2.Controls.Add(this.chkDaiDuongDangDieuTri);
            this.tabControlPanel2.Controls.Add(this.chkDaiThaoDuong);
            this.tabControlPanel2.Controls.Add(this.chkBenhLao);
            this.tabControlPanel2.Controls.Add(this.chkBenhTimMach);
            this.tabControlPanel2.Controls.Add(this.chkDotQuy);
            this.tabControlPanel2.Controls.Add(this.txtThuocDiUng);
            this.tabControlPanel2.Controls.Add(this.label3);
            this.tabControlPanel2.Controls.Add(this.chkDiUngThuoc);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(446, 460);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.pagePatientHistory;
            // 
            // chkDangCoThai
            // 
            this.chkDangCoThai.AutoSize = true;
            this.chkDangCoThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDangCoThai.Location = new System.Drawing.Point(169, 170);
            this.chkDangCoThai.Name = "chkDangCoThai";
            this.chkDangCoThai.Size = new System.Drawing.Size(87, 17);
            this.chkDangCoThai.TabIndex = 70;
            this.chkDangCoThai.Text = "Đang có thai";
            this.chkDangCoThai.UseVisualStyleBackColor = false;
            // 
            // chkChichNguaCum
            // 
            this.chkChichNguaCum.AutoSize = true;
            this.chkChichNguaCum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkChichNguaCum.Location = new System.Drawing.Point(15, 170);
            this.chkChichNguaCum.Name = "chkChichNguaCum";
            this.chkChichNguaCum.Size = new System.Drawing.Size(105, 17);
            this.chkChichNguaCum.TabIndex = 69;
            this.chkChichNguaCum.Text = "Chích ngừa cúm";
            this.chkChichNguaCum.UseVisualStyleBackColor = false;
            // 
            // chkChichNguaUonVan
            // 
            this.chkChichNguaUonVan.AutoSize = true;
            this.chkChichNguaUonVan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkChichNguaUonVan.Location = new System.Drawing.Point(169, 148);
            this.chkChichNguaUonVan.Name = "chkChichNguaUonVan";
            this.chkChichNguaUonVan.Size = new System.Drawing.Size(124, 17);
            this.chkChichNguaUonVan.TabIndex = 68;
            this.chkChichNguaUonVan.Text = "Chích ngừa uốn ván";
            this.chkChichNguaUonVan.UseVisualStyleBackColor = false;
            // 
            // chkChichNguaViemGanB
            // 
            this.chkChichNguaViemGanB.AutoSize = true;
            this.chkChichNguaViemGanB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkChichNguaViemGanB.Location = new System.Drawing.Point(15, 148);
            this.chkChichNguaViemGanB.Name = "chkChichNguaViemGanB";
            this.chkChichNguaViemGanB.Size = new System.Drawing.Size(138, 17);
            this.chkChichNguaViemGanB.TabIndex = 67;
            this.chkChichNguaViemGanB.Text = "Chích ngừa viêm gan B";
            this.chkChichNguaViemGanB.UseVisualStyleBackColor = false;
            // 
            // txtTinhTrangGiaDinh
            // 
            this.txtTinhTrangGiaDinh.Location = new System.Drawing.Point(125, 426);
            this.txtTinhTrangGiaDinh.MaxLength = 50;
            this.txtTinhTrangGiaDinh.Name = "txtTinhTrangGiaDinh";
            this.txtTinhTrangGiaDinh.Size = new System.Drawing.Size(307, 20);
            this.txtTinhTrangGiaDinh.TabIndex = 78;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label21.Location = new System.Drawing.Point(14, 429);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 13);
            this.label21.TabIndex = 76;
            this.label21.Text = "Tình trạng gia đình:";
            // 
            // chkUongRuou
            // 
            this.chkUongRuou.AutoSize = true;
            this.chkUongRuou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkUongRuou.Location = new System.Drawing.Point(169, 125);
            this.chkUongRuou.Name = "chkUongRuou";
            this.chkUongRuou.Size = new System.Drawing.Size(76, 17);
            this.chkUongRuou.TabIndex = 66;
            this.chkUongRuou.Text = "Uống rượu";
            this.chkUongRuou.UseVisualStyleBackColor = false;
            // 
            // chkHutThuoc
            // 
            this.chkHutThuoc.AutoSize = true;
            this.chkHutThuoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkHutThuoc.Location = new System.Drawing.Point(15, 125);
            this.chkHutThuoc.Name = "chkHutThuoc";
            this.chkHutThuoc.Size = new System.Drawing.Size(73, 17);
            this.chkHutThuoc.TabIndex = 65;
            this.chkHutThuoc.Text = "Hút thuốc";
            this.chkHutThuoc.UseVisualStyleBackColor = false;
            // 
            // txtThuocDangDung
            // 
            this.txtThuocDangDung.Location = new System.Drawing.Point(126, 366);
            this.txtThuocDangDung.MaxLength = 500;
            this.txtThuocDangDung.Multiline = true;
            this.txtThuocDangDung.Name = "txtThuocDangDung";
            this.txtThuocDangDung.ReadOnly = true;
            this.txtThuocDangDung.Size = new System.Drawing.Size(306, 54);
            this.txtThuocDangDung.TabIndex = 77;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label7.Location = new System.Drawing.Point(25, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "Thuốc đang dùng:";
            // 
            // txtBenhGi
            // 
            this.txtBenhGi.Location = new System.Drawing.Point(126, 342);
            this.txtBenhGi.MaxLength = 500;
            this.txtBenhGi.Name = "txtBenhGi";
            this.txtBenhGi.ReadOnly = true;
            this.txtBenhGi.Size = new System.Drawing.Size(306, 20);
            this.txtBenhGi.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label6.Location = new System.Drawing.Point(75, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Bệnh gì:";
            // 
            // chkBenhKhac
            // 
            this.chkBenhKhac.AutoSize = true;
            this.chkBenhKhac.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkBenhKhac.Location = new System.Drawing.Point(15, 321);
            this.chkBenhKhac.Name = "chkBenhKhac";
            this.chkBenhKhac.Size = new System.Drawing.Size(78, 17);
            this.chkBenhKhac.TabIndex = 75;
            this.chkBenhKhac.Text = "Bệnh khác";
            this.chkBenhKhac.UseVisualStyleBackColor = false;
            this.chkBenhKhac.CheckedChanged += new System.EventHandler(this.chkBenhKhac_CheckedChanged);
            // 
            // chkHenSuyen
            // 
            this.chkHenSuyen.AutoSize = true;
            this.chkHenSuyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkHenSuyen.Location = new System.Drawing.Point(169, 102);
            this.chkHenSuyen.Name = "chkHenSuyen";
            this.chkHenSuyen.Size = new System.Drawing.Size(77, 17);
            this.chkHenSuyen.TabIndex = 64;
            this.chkHenSuyen.Text = "Hen suyễn";
            this.chkHenSuyen.UseVisualStyleBackColor = false;
            // 
            // chkDongKinh
            // 
            this.chkDongKinh.AutoSize = true;
            this.chkDongKinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDongKinh.Location = new System.Drawing.Point(15, 102);
            this.chkDongKinh.Name = "chkDongKinh";
            this.chkDongKinh.Size = new System.Drawing.Size(75, 17);
            this.chkDongKinh.TabIndex = 63;
            this.chkDongKinh.Text = "Động kinh";
            this.chkDongKinh.UseVisualStyleBackColor = false;
            // 
            // txtCoQuanUngThu
            // 
            this.txtCoQuanUngThu.Location = new System.Drawing.Point(126, 295);
            this.txtCoQuanUngThu.MaxLength = 500;
            this.txtCoQuanUngThu.Name = "txtCoQuanUngThu";
            this.txtCoQuanUngThu.ReadOnly = true;
            this.txtCoQuanUngThu.Size = new System.Drawing.Size(306, 20);
            this.txtCoQuanUngThu.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label5.Location = new System.Drawing.Point(32, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Cơ quan ung thư:";
            // 
            // chkUngThu
            // 
            this.chkUngThu.AutoSize = true;
            this.chkUngThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkUngThu.Location = new System.Drawing.Point(15, 276);
            this.chkUngThu.Name = "chkUngThu";
            this.chkUngThu.Size = new System.Drawing.Size(64, 17);
            this.chkUngThu.TabIndex = 73;
            this.chkUngThu.Text = "Ung thư";
            this.chkUngThu.UseVisualStyleBackColor = false;
            this.chkUngThu.CheckedChanged += new System.EventHandler(this.chkUngThu_CheckedChanged);
            // 
            // chkViemGanDangDieuTri
            // 
            this.chkViemGanDangDieuTri.AutoSize = true;
            this.chkViemGanDangDieuTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkViemGanDangDieuTri.Location = new System.Drawing.Point(169, 79);
            this.chkViemGanDangDieuTri.Name = "chkViemGanDangDieuTri";
            this.chkViemGanDangDieuTri.Size = new System.Drawing.Size(133, 17);
            this.chkViemGanDangDieuTri.TabIndex = 62;
            this.chkViemGanDangDieuTri.Text = "Viêm gan đang điều trị";
            this.chkViemGanDangDieuTri.UseVisualStyleBackColor = false;
            // 
            // chkViemGanC
            // 
            this.chkViemGanC.AutoSize = true;
            this.chkViemGanC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkViemGanC.Location = new System.Drawing.Point(15, 79);
            this.chkViemGanC.Name = "chkViemGanC";
            this.chkViemGanC.Size = new System.Drawing.Size(80, 17);
            this.chkViemGanC.TabIndex = 61;
            this.chkViemGanC.Text = "Viêm gan C";
            this.chkViemGanC.UseVisualStyleBackColor = false;
            // 
            // chkViemGanB
            // 
            this.chkViemGanB.AutoSize = true;
            this.chkViemGanB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkViemGanB.Location = new System.Drawing.Point(169, 56);
            this.chkViemGanB.Name = "chkViemGanB";
            this.chkViemGanB.Size = new System.Drawing.Size(80, 17);
            this.chkViemGanB.TabIndex = 60;
            this.chkViemGanB.Text = "Viêm gan B";
            this.chkViemGanB.UseVisualStyleBackColor = false;
            // 
            // chkDaiDuongDangDieuTri
            // 
            this.chkDaiDuongDangDieuTri.AutoSize = true;
            this.chkDaiDuongDangDieuTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDaiDuongDangDieuTri.Location = new System.Drawing.Point(15, 56);
            this.chkDaiDuongDangDieuTri.Name = "chkDaiDuongDangDieuTri";
            this.chkDaiDuongDangDieuTri.Size = new System.Drawing.Size(139, 17);
            this.chkDaiDuongDangDieuTri.TabIndex = 59;
            this.chkDaiDuongDangDieuTri.Text = "Đái đường đang điều trị";
            this.chkDaiDuongDangDieuTri.UseVisualStyleBackColor = false;
            // 
            // chkDaiThaoDuong
            // 
            this.chkDaiThaoDuong.AutoSize = true;
            this.chkDaiThaoDuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDaiThaoDuong.Location = new System.Drawing.Point(169, 33);
            this.chkDaiThaoDuong.Name = "chkDaiThaoDuong";
            this.chkDaiThaoDuong.Size = new System.Drawing.Size(100, 17);
            this.chkDaiThaoDuong.TabIndex = 58;
            this.chkDaiThaoDuong.Text = "Đái tháo đường";
            this.chkDaiThaoDuong.UseVisualStyleBackColor = false;
            // 
            // chkBenhLao
            // 
            this.chkBenhLao.AutoSize = true;
            this.chkBenhLao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkBenhLao.Location = new System.Drawing.Point(15, 33);
            this.chkBenhLao.Name = "chkBenhLao";
            this.chkBenhLao.Size = new System.Drawing.Size(68, 17);
            this.chkBenhLao.TabIndex = 57;
            this.chkBenhLao.Text = "Bệnh lao";
            this.chkBenhLao.UseVisualStyleBackColor = false;
            // 
            // chkBenhTimMach
            // 
            this.chkBenhTimMach.AutoSize = true;
            this.chkBenhTimMach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkBenhTimMach.Location = new System.Drawing.Point(169, 10);
            this.chkBenhTimMach.Name = "chkBenhTimMach";
            this.chkBenhTimMach.Size = new System.Drawing.Size(96, 17);
            this.chkBenhTimMach.TabIndex = 56;
            this.chkBenhTimMach.Text = "Bệnh tim mạch";
            this.chkBenhTimMach.UseVisualStyleBackColor = false;
            // 
            // chkDotQuy
            // 
            this.chkDotQuy.AutoSize = true;
            this.chkDotQuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDotQuy.Location = new System.Drawing.Point(15, 10);
            this.chkDotQuy.Name = "chkDotQuy";
            this.chkDotQuy.Size = new System.Drawing.Size(63, 17);
            this.chkDotQuy.TabIndex = 55;
            this.chkDotQuy.Text = "Đột quỵ";
            this.chkDotQuy.UseVisualStyleBackColor = false;
            // 
            // txtThuocDiUng
            // 
            this.txtThuocDiUng.Location = new System.Drawing.Point(125, 215);
            this.txtThuocDiUng.MaxLength = 500;
            this.txtThuocDiUng.Multiline = true;
            this.txtThuocDiUng.Name = "txtThuocDiUng";
            this.txtThuocDiUng.ReadOnly = true;
            this.txtThuocDiUng.Size = new System.Drawing.Size(307, 54);
            this.txtThuocDiUng.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label3.Location = new System.Drawing.Point(48, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Thuốc dị ứng:";
            // 
            // chkDiUngThuoc
            // 
            this.chkDiUngThuoc.AutoSize = true;
            this.chkDiUngThuoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.chkDiUngThuoc.Location = new System.Drawing.Point(15, 194);
            this.chkDiUngThuoc.Name = "chkDiUngThuoc";
            this.chkDiUngThuoc.Size = new System.Drawing.Size(87, 17);
            this.chkDiUngThuoc.TabIndex = 71;
            this.chkDiUngThuoc.Text = "Dị ứng thuốc";
            this.chkDiUngThuoc.UseVisualStyleBackColor = false;
            this.chkDiUngThuoc.CheckedChanged += new System.EventHandler(this.chkDiUngThuoc_CheckedChanged);
            // 
            // pagePatientHistory
            // 
            this.pagePatientHistory.AttachedControl = this.tabControlPanel2;
            this.pagePatientHistory.Image = global::MM.Properties.Resources.File_History_icon;
            this.pagePatientHistory.Name = "pagePatientHistory";
            this.pagePatientHistory.Text = "Bệnh sử";
            // 
            // dlgAddPatient
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(446, 520);
            this.Controls.Add(this.tabPatient);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddPatient";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them benh nhan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddPatient_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tabPatient)).EndInit();
            this.tabPatient.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.tabControlPanel2.ResumeLayout(false);
            this.tabControlPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFileNum;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtPreferredName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtKnownAs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtHomePhone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIdentityCard;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWorkPhone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.TabControl tabPatient;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pagePatientHistory;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem pagePatientInfo;
        private System.Windows.Forms.CheckBox chkDiUngThuoc;
        private System.Windows.Forms.TextBox txtThuocDiUng;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDangCoThai;
        private System.Windows.Forms.CheckBox chkChichNguaCum;
        private System.Windows.Forms.CheckBox chkChichNguaUonVan;
        private System.Windows.Forms.CheckBox chkChichNguaViemGanB;
        private System.Windows.Forms.TextBox txtTinhTrangGiaDinh;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkUongRuou;
        private System.Windows.Forms.CheckBox chkHutThuoc;
        private System.Windows.Forms.TextBox txtThuocDangDung;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBenhGi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBenhKhac;
        private System.Windows.Forms.CheckBox chkHenSuyen;
        private System.Windows.Forms.CheckBox chkDongKinh;
        private System.Windows.Forms.TextBox txtCoQuanUngThu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkUngThu;
        private System.Windows.Forms.CheckBox chkViemGanDangDieuTri;
        private System.Windows.Forms.CheckBox chkViemGanC;
        private System.Windows.Forms.CheckBox chkViemGanB;
        private System.Windows.Forms.CheckBox chkDaiDuongDangDieuTri;
        private System.Windows.Forms.CheckBox chkDaiThaoDuong;
        private System.Windows.Forms.CheckBox chkBenhLao;
        private System.Windows.Forms.CheckBox chkBenhTimMach;
        private System.Windows.Forms.CheckBox chkDotQuy;
        private DSBarCode.BarCodeCtrl barCode;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtTenCongTy;
        private System.Windows.Forms.Label label24;
    }
}