﻿namespace MM.Controls
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
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this._uServiceHistory = new MM.Controls.uServiceHistory();
            this.pageServiceHistory = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabServiceHistory = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this._uDailyServiceHistory = new MM.Controls.uServiceHistory();
            this.pageDailyService = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this._uToaThuocList = new MM.Controls.uToaThuocList();
            this.pageKeToa = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvService = new System.Windows.Forms.ListView();
            this.colChecked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtThuocDiUng = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFullAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.txtIdentityCard = new System.Windows.Forms.TextBox();
            this.txtDOB = new System.Windows.Forms.TextBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControlPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabServiceHistory)).BeginInit();
            this.tabServiceHistory.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "1321864209_tick.png");
            this.imgList.Images.SetKeyName(1, "1321864188_exclamation.png");
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this._uServiceHistory);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1177, 425);
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
            this._uServiceHistory.Size = new System.Drawing.Size(1175, 423);
            this._uServiceHistory.TabIndex = 0;
            // 
            // pageServiceHistory
            // 
            this.pageServiceHistory.AttachedControl = this.tabControlPanel1;
            this.pageServiceHistory.Image = global::MM.Properties.Resources.Service_icon;
            this.pageServiceHistory.Name = "pageServiceHistory";
            this.pageServiceHistory.Text = "Dịch vụ đã sử dụng";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabServiceHistory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 188);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1177, 450);
            this.panel2.TabIndex = 1;
            // 
            // tabServiceHistory
            // 
            this.tabServiceHistory.CanReorderTabs = true;
            this.tabServiceHistory.Controls.Add(this.tabControlPanel2);
            this.tabServiceHistory.Controls.Add(this.tabControlPanel3);
            this.tabServiceHistory.Controls.Add(this.tabControlPanel1);
            this.tabServiceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabServiceHistory.Location = new System.Drawing.Point(0, 0);
            this.tabServiceHistory.Name = "tabServiceHistory";
            this.tabServiceHistory.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabServiceHistory.SelectedTabIndex = 0;
            this.tabServiceHistory.Size = new System.Drawing.Size(1177, 450);
            this.tabServiceHistory.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabServiceHistory.TabIndex = 0;
            this.tabServiceHistory.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabServiceHistory.Tabs.Add(this.pageDailyService);
            this.tabServiceHistory.Tabs.Add(this.pageServiceHistory);
            this.tabServiceHistory.Tabs.Add(this.pageKeToa);
            this.tabServiceHistory.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabServiceHistory_SelectedTabChanged);
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this._uDailyServiceHistory);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1177, 425);
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
            this._uDailyServiceHistory.Size = new System.Drawing.Size(1175, 423);
            this._uDailyServiceHistory.TabIndex = 0;
            // 
            // pageDailyService
            // 
            this.pageDailyService.AttachedControl = this.tabControlPanel2;
            this.pageDailyService.Image = global::MM.Properties.Resources.Actions_view_calendar_day_icon;
            this.pageDailyService.Name = "pageDailyService";
            this.pageDailyService.Text = "Dịch vụ trong ngày";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this._uToaThuocList);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(1177, 425);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.pageKeToa;
            // 
            // _uToaThuocList
            // 
            this._uToaThuocList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uToaThuocList.Location = new System.Drawing.Point(1, 1);
            this._uToaThuocList.Name = "_uToaThuocList";
            this._uToaThuocList.PatientRow = null;
            this._uToaThuocList.Size = new System.Drawing.Size(1175, 423);
            this._uToaThuocList.TabIndex = 0;
            // 
            // pageKeToa
            // 
            this.pageKeToa.AttachedControl = this.tabControlPanel3;
            this.pageKeToa.Image = global::MM.Properties.Resources.prescription_icon2;
            this.pageKeToa.Name = "pageKeToa";
            this.pageKeToa.Text = "Kê toa";
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
            this.panel1.Size = new System.Drawing.Size(1177, 188);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.lvService);
            this.groupBox1.Controls.Add(this.txtAge);
            this.groupBox1.Controls.Add(this.txtThuocDiUng);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFullAddress);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.txtWorkPhone);
            this.groupBox1.Controls.Add(this.txtIdentityCard);
            this.groupBox1.Controls.Add(this.txtDOB);
            this.groupBox1.Controls.Add(this.txtGender);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1167, 178);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin bệnh nhân";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::MM.Properties.Resources.Refresh_icon;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(6, 151);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 23);
            this.btnRefresh.TabIndex = 78;
            this.btnRefresh.Text = "   &Làm tươi";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.lvService.Location = new System.Drawing.Point(766, 19);
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
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(331, 18);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(115, 20);
            this.txtAge.TabIndex = 76;
            // 
            // txtThuocDiUng
            // 
            this.txtThuocDiUng.ForeColor = System.Drawing.Color.Red;
            this.txtThuocDiUng.Location = new System.Drawing.Point(4, 78);
            this.txtThuocDiUng.Name = "txtThuocDiUng";
            this.txtThuocDiUng.ReadOnly = true;
            this.txtThuocDiUng.Size = new System.Drawing.Size(751, 67);
            this.txtThuocDiUng.TabIndex = 74;
            this.txtThuocDiUng.Text = "";
            this.txtThuocDiUng.DoubleClick += new System.EventHandler(this.txtThuocDiUng_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Dị ứng:";
            // 
            // txtFullAddress
            // 
            this.txtFullAddress.Location = new System.Drawing.Point(4, 39);
            this.txtFullAddress.Name = "txtFullAddress";
            this.txtFullAddress.ReadOnly = true;
            this.txtFullAddress.Size = new System.Drawing.Size(326, 20);
            this.txtFullAddress.TabIndex = 38;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(563, 39);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(192, 20);
            this.txtEmail.TabIndex = 32;
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(447, 39);
            this.txtMobile.MaxLength = 50;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(115, 20);
            this.txtMobile.TabIndex = 31;
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(331, 39);
            this.txtWorkPhone.MaxLength = 50;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.ReadOnly = true;
            this.txtWorkPhone.Size = new System.Drawing.Size(115, 20);
            this.txtWorkPhone.TabIndex = 30;
            // 
            // txtIdentityCard
            // 
            this.txtIdentityCard.Location = new System.Drawing.Point(563, 18);
            this.txtIdentityCard.Name = "txtIdentityCard";
            this.txtIdentityCard.ReadOnly = true;
            this.txtIdentityCard.Size = new System.Drawing.Size(192, 20);
            this.txtIdentityCard.TabIndex = 9;
            // 
            // txtDOB
            // 
            this.txtDOB.Location = new System.Drawing.Point(215, 18);
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.ReadOnly = true;
            this.txtDOB.Size = new System.Drawing.Size(115, 20);
            this.txtDOB.TabIndex = 7;
            this.txtDOB.Tag = "";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(447, 18);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(115, 20);
            this.txtGender.TabIndex = 5;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(4, 18);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(210, 20);
            this.txtFullName.TabIndex = 3;
            this.txtFullName.Text = "Tran Nguyen Thien Phuc";
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1172, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(5, 178);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 178);
            this.panel5.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 183);
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
            // uPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uPatient";
            this.Size = new System.Drawing.Size(1177, 638);
            this.Load += new System.EventHandler(this.uPatient_Load);
            this.tabControlPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabServiceHistory)).EndInit();
            this.tabServiceHistory.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtIdentityCard;
        private System.Windows.Forms.TextBox txtFullAddress;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtWorkPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtThuocDiUng;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pageDailyService;
        private uServiceHistory _uDailyServiceHistory;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.ListView lvService;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ColumnHeader colChecked;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem pageKeToa;
        private uToaThuocList _uToaThuocList;
        private System.Windows.Forms.Button btnRefresh;
    }
}
