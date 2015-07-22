namespace MM.Dialogs
{
    partial class dlgAddKetQuaSoiCTC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddKetQuaSoiCTC));
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this._uKetQuaSoiCTC = new MM.Controls.uKetQuaSoiCTC();
            this.pageKetQuaNoiSoi = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cboKetLuan = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDeNghi = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.picHinh2 = new System.Windows.Forms.PictureBox();
            this.ctmHinh2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.picHinh1 = new System.Windows.Forms.PictureBox();
            this.ctmHinh1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xóaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.cboBSSoi = new System.Windows.Forms.ComboBox();
            this.dtpkNgayKham = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabKhamNoiSoi = new System.Windows.Forms.TabControl();
            this.pageChupHinh = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lvCapture = new System.Windows.Forms.ListView();
            this.ctmCapture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xóaTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.chọnHìnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListCapture = new System.Windows.Forms.ImageList(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel5.SuspendLayout();
            this.pageKetQuaNoiSoi.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh2)).BeginInit();
            this.ctmHinh2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh1)).BeginInit();
            this.ctmHinh1.SuspendLayout();
            this.tabKhamNoiSoi.SuspendLayout();
            this.pageChupHinh.SuspendLayout();
            this.panel7.SuspendLayout();
            this.ctmCapture.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._uKetQuaSoiCTC);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 289);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(834, 201);
            this.panel5.TabIndex = 3;
            // 
            // _uKetQuaSoiCTC
            // 
            this._uKetQuaSoiCTC.AmDao = "Có ít huyết trắng";
            this._uKetQuaSoiCTC.AmHo = "Bình thường";
            this._uKetQuaSoiCTC.BieuMoLat = "Láng";
            this._uKetQuaSoiCTC.CTC = "Kích thước 2.5 cm";
            this._uKetQuaSoiCTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uKetQuaSoiCTC.Location = new System.Drawing.Point(0, 0);
            this._uKetQuaSoiCTC.MoDem = "Mạch máu bình thường";
            this._uKetQuaSoiCTC.Name = "_uKetQuaSoiCTC";
            this._uKetQuaSoiCTC.RanhGioiLatTru = "Lỗ ngoài";
            this._uKetQuaSoiCTC.SauAcidAcetic = "Không bất thường";
            this._uKetQuaSoiCTC.SauLugol = "Bắt màu đều";
            this._uKetQuaSoiCTC.Size = new System.Drawing.Size(834, 201);
            this._uKetQuaSoiCTC.TabIndex = 0;
            // 
            // pageKetQuaNoiSoi
            // 
            this.pageKetQuaNoiSoi.BackColor = System.Drawing.SystemColors.Control;
            this.pageKetQuaNoiSoi.Controls.Add(this.panel5);
            this.pageKetQuaNoiSoi.Controls.Add(this.panel4);
            this.pageKetQuaNoiSoi.Controls.Add(this.panel2);
            this.pageKetQuaNoiSoi.ImageIndex = 1;
            this.pageKetQuaNoiSoi.Location = new System.Drawing.Point(4, 23);
            this.pageKetQuaNoiSoi.Name = "pageKetQuaNoiSoi";
            this.pageKetQuaNoiSoi.Padding = new System.Windows.Forms.Padding(3);
            this.pageKetQuaNoiSoi.Size = new System.Drawing.Size(840, 550);
            this.pageKetQuaNoiSoi.TabIndex = 0;
            this.pageKetQuaNoiSoi.Text = "Kết quả soi CTC";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cboKetLuan);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.cboDeNghi);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 490);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(834, 57);
            this.panel4.TabIndex = 4;
            // 
            // cboKetLuan
            // 
            this.cboKetLuan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboKetLuan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboKetLuan.FormattingEnabled = true;
            this.cboKetLuan.Location = new System.Drawing.Point(60, 7);
            this.cboKetLuan.MaxLength = 500;
            this.cboKetLuan.Name = "cboKetLuan";
            this.cboKetLuan.Size = new System.Drawing.Size(766, 21);
            this.cboKetLuan.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kết luận:";
            // 
            // cboDeNghi
            // 
            this.cboDeNghi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDeNghi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDeNghi.FormattingEnabled = true;
            this.cboDeNghi.Location = new System.Drawing.Point(60, 32);
            this.cboDeNghi.MaxLength = 500;
            this.cboDeNghi.Name = "cboDeNghi";
            this.cboDeNghi.Size = new System.Drawing.Size(766, 21);
            this.cboDeNghi.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Đề nghị:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.picHinh2);
            this.panel2.Controls.Add(this.picHinh1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cboBSSoi);
            this.panel2.Controls.Add(this.dtpkNgayKham);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(834, 286);
            this.panel2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(478, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "(SAU LUGOL)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(256, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "(SAU ACID ACETIC)";
            // 
            // picHinh2
            // 
            this.picHinh2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinh2.ContextMenuStrip = this.ctmHinh2;
            this.picHinh2.Location = new System.Drawing.Point(421, 59);
            this.picHinh2.Name = "picHinh2";
            this.picHinh2.Size = new System.Drawing.Size(200, 200);
            this.picHinh2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinh2.TabIndex = 14;
            this.picHinh2.TabStop = false;
            this.picHinh2.DoubleClick += new System.EventHandler(this.picHinh2_DoubleClick);
            // 
            // ctmHinh2
            // 
            this.ctmHinh2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1,
            this.toolStripSeparator4,
            this.toolStripMenuItem1});
            this.ctmHinh2.Name = "ctmHinh1";
            this.ctmHinh2.Size = new System.Drawing.Size(201, 54);
            // 
            // chọnHìnhTừBênNgoàiToolStripMenuItem1
            // 
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Name = "chọnHìnhTừBênNgoàiToolStripMenuItem1";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Text = "Chọn hình từ bên ngoài";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Click += new System.EventHandler(this.chọnHìnhTừBênNgoàiToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(197, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItem1.Text = "Xóa";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // picHinh1
            // 
            this.picHinh1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinh1.ContextMenuStrip = this.ctmHinh1;
            this.picHinh1.Location = new System.Drawing.Point(213, 59);
            this.picHinh1.Name = "picHinh1";
            this.picHinh1.Size = new System.Drawing.Size(200, 200);
            this.picHinh1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinh1.TabIndex = 13;
            this.picHinh1.TabStop = false;
            this.picHinh1.DoubleClick += new System.EventHandler(this.picHinh1_DoubleClick);
            // 
            // ctmHinh1
            // 
            this.ctmHinh1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnHìnhTừBênNgoàiToolStripMenuItem,
            this.toolStripSeparator3,
            this.xóaToolStripMenuItem1});
            this.ctmHinh1.Name = "ctmHinh1";
            this.ctmHinh1.Size = new System.Drawing.Size(201, 54);
            // 
            // chọnHìnhTừBênNgoàiToolStripMenuItem
            // 
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Name = "chọnHìnhTừBênNgoàiToolStripMenuItem";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Text = "Chọn hình từ bên ngoài";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Click += new System.EventHandler(this.chọnHìnhTừBênNgoàiToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // xóaToolStripMenuItem1
            // 
            this.xóaToolStripMenuItem1.Name = "xóaToolStripMenuItem1";
            this.xóaToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.xóaToolStripMenuItem1.Text = "Xóa";
            this.xóaToolStripMenuItem1.Click += new System.EventHandler(this.xóaToolStripMenuItem1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(346, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "HÌNH ẢNH NỘI SOI";
            // 
            // cboBSSoi
            // 
            this.cboBSSoi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBSSoi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBSSoi.DataSource = this.docStaffViewBindingSource;
            this.cboBSSoi.DisplayMember = "Fullname";
            this.cboBSSoi.FormattingEnabled = true;
            this.cboBSSoi.Location = new System.Drawing.Point(265, 9);
            this.cboBSSoi.Name = "cboBSSoi";
            this.cboBSSoi.Size = new System.Drawing.Size(274, 21);
            this.cboBSSoi.TabIndex = 3;
            this.cboBSSoi.ValueMember = "DocStaffGUID";
            // 
            // dtpkNgayKham
            // 
            this.dtpkNgayKham.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayKham.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayKham.Location = new System.Drawing.Point(82, 10);
            this.dtpkNgayKham.Name = "dtpkNgayKham";
            this.dtpkNgayKham.Size = new System.Drawing.Size(105, 20);
            this.dtpkNgayKham.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bác sĩ soi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày khám:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(466, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabKhamNoiSoi
            // 
            this.tabKhamNoiSoi.Controls.Add(this.pageKetQuaNoiSoi);
            this.tabKhamNoiSoi.Controls.Add(this.pageChupHinh);
            this.tabKhamNoiSoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabKhamNoiSoi.ImageList = this.imgList;
            this.tabKhamNoiSoi.Location = new System.Drawing.Point(0, 0);
            this.tabKhamNoiSoi.Name = "tabKhamNoiSoi";
            this.tabKhamNoiSoi.SelectedIndex = 0;
            this.tabKhamNoiSoi.Size = new System.Drawing.Size(848, 577);
            this.tabKhamNoiSoi.TabIndex = 0;
            this.tabKhamNoiSoi.TabStop = false;
            // 
            // pageChupHinh
            // 
            this.pageChupHinh.BackColor = System.Drawing.SystemColors.Control;
            this.pageChupHinh.Controls.Add(this.panel7);
            this.pageChupHinh.Controls.Add(this.panel6);
            this.pageChupHinh.ImageIndex = 0;
            this.pageChupHinh.Location = new System.Drawing.Point(4, 23);
            this.pageChupHinh.Name = "pageChupHinh";
            this.pageChupHinh.Padding = new System.Windows.Forms.Padding(3);
            this.pageChupHinh.Size = new System.Drawing.Size(840, 550);
            this.pageChupHinh.TabIndex = 1;
            this.pageChupHinh.Text = "Danh sách hình chụp";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lvCapture);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 32);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(834, 515);
            this.panel7.TabIndex = 1;
            // 
            // lvCapture
            // 
            this.lvCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvCapture.ContextMenuStrip = this.ctmCapture;
            this.lvCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCapture.LargeImageList = this.imgListCapture;
            this.lvCapture.Location = new System.Drawing.Point(0, 0);
            this.lvCapture.Name = "lvCapture";
            this.lvCapture.Size = new System.Drawing.Size(834, 515);
            this.lvCapture.SmallImageList = this.imgListCapture;
            this.lvCapture.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCapture.TabIndex = 0;
            this.lvCapture.UseCompatibleStateImageBehavior = false;
            this.lvCapture.DoubleClick += new System.EventHandler(this.lvCapture_DoubleClick);
            // 
            // ctmCapture
            // 
            this.ctmCapture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem,
            this.toolStripSeparator1,
            this.xóaTấtCảToolStripMenuItem,
            this.toolStripSeparator2,
            this.chọnHìnhToolStripMenuItem});
            this.ctmCapture.Name = "ctmCapture";
            this.ctmCapture.Size = new System.Drawing.Size(153, 104);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xóaToolStripMenuItem.Text = "Xóa";
            this.xóaToolStripMenuItem.Click += new System.EventHandler(this.xóaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // xóaTấtCảToolStripMenuItem
            // 
            this.xóaTấtCảToolStripMenuItem.Name = "xóaTấtCảToolStripMenuItem";
            this.xóaTấtCảToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xóaTấtCảToolStripMenuItem.Text = "Xóa tất cả";
            this.xóaTấtCảToolStripMenuItem.Click += new System.EventHandler(this.xóaTấtCảToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // chọnHìnhToolStripMenuItem
            // 
            this.chọnHìnhToolStripMenuItem.Name = "chọnHìnhToolStripMenuItem";
            this.chọnHìnhToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chọnHìnhToolStripMenuItem.Text = "Chọn hình";
            this.chọnHìnhToolStripMenuItem.Click += new System.EventHandler(this.chọnHìnhToolStripMenuItem_Click);
            // 
            // imgListCapture
            // 
            this.imgListCapture.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListCapture.ImageSize = new System.Drawing.Size(200, 200);
            this.imgListCapture.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label14);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(834, 29);
            this.panel6.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(5, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Danh sách hình được chụp";
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "camera-icon (1).png");
            this.imgList.Images.SetKeyName(1, "clipboard-search-result-icon.png");
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabKhamNoiSoi);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(848, 577);
            this.panel3.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveAndPrint);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 577);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 39);
            this.panel1.TabIndex = 7;
            this.panel1.TabStop = true;
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveAndPrint.Image = global::MM.Properties.Resources.save;
            this.btnSaveAndPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAndPrint.Location = new System.Drawing.Point(387, 6);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(75, 25);
            this.btnSaveAndPrint.TabIndex = 18;
            this.btnSaveAndPrint.Text = "     &Lưu && In";
            this.btnSaveAndPrint.UseVisualStyleBackColor = true;
            this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(308, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddKetQuaSoiCTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 616);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddKetQuaSoiCTC";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them ket qua soi CTC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddKetQuaSoiCTC_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddKetQuaSoiCTC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pageKetQuaNoiSoi.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh2)).EndInit();
            this.ctmHinh2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHinh1)).EndInit();
            this.ctmHinh1.ResumeLayout(false);
            this.tabKhamNoiSoi.ResumeLayout(false);
            this.pageChupHinh.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ctmCapture.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TabPage pageKetQuaNoiSoi;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cboKetLuan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDeNghi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox picHinh2;
        private System.Windows.Forms.ContextMenuStrip ctmHinh2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.PictureBox picHinh1;
        private System.Windows.Forms.ContextMenuStrip ctmHinh1;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboBSSoi;
        private System.Windows.Forms.DateTimePicker dtpkNgayKham;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabKhamNoiSoi;
        private System.Windows.Forms.TabPage pageChupHinh;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ListView lvCapture;
        private System.Windows.Forms.ContextMenuStrip ctmCapture;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem xóaTấtCảToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem chọnHìnhToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListCapture;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveAndPrint;
        private System.Windows.Forms.Button btnOK;
        private Controls.uKetQuaSoiCTC _uKetQuaSoiCTC;
        private System.Windows.Forms.ToolStripMenuItem chọnHìnhTừBênNgoàiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem chọnHìnhTừBênNgoàiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}