namespace MM.Controls
{
    partial class uPatient
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uPatient));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvService = new System.Windows.Forms.ListView();
            this.colChecked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtThuocDiUng = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFullAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHomePhone = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.txtIdentityCard = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDOB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabServiceHistory = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this._uDailyServiceHistory = new MM.Controls.uServiceHistory();
            this.pageDailyService = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this._uServiceHistory = new MM.Controls.uServiceHistory();
            this.pageServiceHistory = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabServiceHistory)).BeginInit();
            this.tabServiceHistory.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1177, 165);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvService);
            this.groupBox1.Controls.Add(this.txtAge);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtThuocDiUng);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFullAddress);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtHomePhone);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtWorkPhone);
            this.groupBox1.Controls.Add(this.txtIdentityCard);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDOB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGender);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1167, 155);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin bệnh nhân";
            // 
            // lvService
            // 
            this.lvService.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colChecked,
            this.colCode,
            this.colName});
            this.lvService.FullRowSelect = true;
            this.lvService.GridLines = true;
            this.lvService.LargeImageList = this.imgList;
            this.lvService.Location = new System.Drawing.Point(710, 19);
            this.lvService.MultiSelect = false;
            this.lvService.Name = "lvService";
            this.lvService.Size = new System.Drawing.Size(368, 126);
            this.lvService.SmallImageList = this.imgList;
            this.lvService.TabIndex = 77;
            this.lvService.UseCompatibleStateImageBehavior = false;
            this.lvService.View = System.Windows.Forms.View.Details;
            this.lvService.Visible = false;
            // 
            // colChecked
            // 
            this.colChecked.Text = "";
            this.colChecked.Width = 22;
            // 
            // colCode
            // 
            this.colCode.Text = "Mã DV";
            this.colCode.Width = 86;
            // 
            // colName
            // 
            this.colName.Text = "Tên DV";
            this.colName.Width = 235;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "check.gif");
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(604, 19);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(100, 20);
            this.txtAge.TabIndex = 76;
            this.txtAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(561, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Tuổi:";
            // 
            // txtThuocDiUng
            // 
            this.txtThuocDiUng.ForeColor = System.Drawing.Color.Red;
            this.txtThuocDiUng.Location = new System.Drawing.Point(83, 91);
            this.txtThuocDiUng.Name = "txtThuocDiUng";
            this.txtThuocDiUng.ReadOnly = true;
            this.txtThuocDiUng.Size = new System.Drawing.Size(621, 54);
            this.txtThuocDiUng.TabIndex = 74;
            this.txtThuocDiUng.Text = "";
            this.txtThuocDiUng.DoubleClick += new System.EventHandler(this.txtThuocDiUng_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Dị ứng thuốc:";
            // 
            // txtFullAddress
            // 
            this.txtFullAddress.Location = new System.Drawing.Point(355, 68);
            this.txtFullAddress.Name = "txtFullAddress";
            this.txtFullAddress.ReadOnly = true;
            this.txtFullAddress.Size = new System.Drawing.Size(349, 20);
            this.txtFullAddress.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Địa chỉ:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(83, 68);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(225, 20);
            this.txtEmail.TabIndex = 32;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(45, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Email:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(391, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Số ĐTDĐ:";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(449, 44);
            this.txtMobile.MaxLength = 50;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(107, 20);
            this.txtMobile.TabIndex = 31;
            this.txtMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(197, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Số ĐT làm việc:";
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Location = new System.Drawing.Point(83, 44);
            this.txtHomePhone.MaxLength = 50;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.ReadOnly = true;
            this.txtHomePhone.Size = new System.Drawing.Size(100, 20);
            this.txtHomePhone.TabIndex = 29;
            this.txtHomePhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Số ĐT nhà:";
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(283, 44);
            this.txtWorkPhone.MaxLength = 50;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.ReadOnly = true;
            this.txtWorkPhone.Size = new System.Drawing.Size(100, 20);
            this.txtWorkPhone.TabIndex = 30;
            this.txtWorkPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIdentityCard
            // 
            this.txtIdentityCard.Location = new System.Drawing.Point(604, 44);
            this.txtIdentityCard.Name = "txtIdentityCard";
            this.txtIdentityCard.ReadOnly = true;
            this.txtIdentityCard.Size = new System.Drawing.Size(100, 20);
            this.txtIdentityCard.TabIndex = 9;
            this.txtIdentityCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(561, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "CMND:";
            // 
            // txtDOB
            // 
            this.txtDOB.Location = new System.Drawing.Point(480, 19);
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.ReadOnly = true;
            this.txtDOB.Size = new System.Drawing.Size(76, 20);
            this.txtDOB.TabIndex = 7;
            this.txtDOB.Tag = "";
            this.txtDOB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ngày sinh:";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(369, 19);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(43, 20);
            this.txtGender.TabIndex = 5;
            this.txtGender.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giới tính:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(83, 19);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(225, 20);
            this.txtFullName.TabIndex = 3;
            this.txtFullName.Text = "Tran Nguyen Thien Phuc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ Tên:";
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1172, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(5, 155);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 155);
            this.panel5.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 160);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1177, 5);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1177, 5);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabServiceHistory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1177, 473);
            this.panel2.TabIndex = 1;
            // 
            // tabServiceHistory
            // 
            this.tabServiceHistory.CanReorderTabs = true;
            this.tabServiceHistory.Controls.Add(this.tabControlPanel2);
            this.tabServiceHistory.Controls.Add(this.tabControlPanel1);
            this.tabServiceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabServiceHistory.Location = new System.Drawing.Point(0, 0);
            this.tabServiceHistory.Name = "tabServiceHistory";
            this.tabServiceHistory.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabServiceHistory.SelectedTabIndex = 0;
            this.tabServiceHistory.Size = new System.Drawing.Size(1177, 473);
            this.tabServiceHistory.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabServiceHistory.TabIndex = 0;
            this.tabServiceHistory.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabServiceHistory.Tabs.Add(this.pageDailyService);
            this.tabServiceHistory.Tabs.Add(this.pageServiceHistory);
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this._uDailyServiceHistory);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1177, 448);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.pageDailyService;
            // 
            // _uDailyServiceHistory
            // 
            this._uDailyServiceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uDailyServiceHistory.IsDailyService = true;
            this._uDailyServiceHistory.Location = new System.Drawing.Point(1, 1);
            this._uDailyServiceHistory.Name = "_uDailyServiceHistory";
            this._uDailyServiceHistory.PatientRow = null;
            this._uDailyServiceHistory.Size = new System.Drawing.Size(1175, 446);
            this._uDailyServiceHistory.TabIndex = 0;
            // 
            // pageDailyService
            // 
            this.pageDailyService.AttachedControl = this.tabControlPanel2;
            this.pageDailyService.Name = "pageDailyService";
            this.pageDailyService.Text = "Dịch vụ trong ngày";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this._uServiceHistory);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1177, 448);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.pageServiceHistory;
            // 
            // _uServiceHistory
            // 
            this._uServiceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uServiceHistory.IsDailyService = false;
            this._uServiceHistory.Location = new System.Drawing.Point(1, 1);
            this._uServiceHistory.Name = "_uServiceHistory";
            this._uServiceHistory.PatientRow = null;
            this._uServiceHistory.Size = new System.Drawing.Size(1175, 446);
            this._uServiceHistory.TabIndex = 0;
            // 
            // pageServiceHistory
            // 
            this.pageServiceHistory.AttachedControl = this.tabControlPanel1;
            this.pageServiceHistory.Name = "pageServiceHistory";
            this.pageServiceHistory.Text = "Dịch vụ đã sử dụng";
            // 
            // uPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uPatient";
            this.Size = new System.Drawing.Size(1177, 638);
            this.Load += new System.EventHandler(this.uPatient_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabServiceHistory)).EndInit();
            this.tabServiceHistory.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.TabControl tabServiceHistory;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem pageServiceHistory;
        private Controls.uServiceHistory _uServiceHistory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtIdentityCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFullAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtHomePhone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWorkPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtThuocDiUng;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pageDailyService;
        private uServiceHistory _uDailyServiceHistory;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvService;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ColumnHeader colChecked;
    }
}
