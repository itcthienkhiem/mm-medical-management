namespace SonoOnlineResult
{
    partial class UploadFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadFile));
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvFile = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.picViewer = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripImage = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRotateLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRotateRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddText = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChangePassword = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonMySQLConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFTPConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMailConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMailTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLogoConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdver = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResendMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteUploadFiles = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonBranch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTracking = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxTemplates = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxLogo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonApply = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxAds = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonAddAds = new System.Windows.Forms.ToolStripButton();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picViewer)).BeginInit();
            this.toolStripImage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvFile);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(373, 599);
            this.panel3.TabIndex = 4;
            // 
            // lvFile
            // 
            this.lvFile.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvFile.AllowDrop = true;
            this.lvFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName});
            this.lvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFile.FullRowSelect = true;
            this.lvFile.GridLines = true;
            this.lvFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFile.HideSelection = false;
            this.lvFile.Location = new System.Drawing.Point(0, 0);
            this.lvFile.Name = "lvFile";
            this.lvFile.Size = new System.Drawing.Size(373, 599);
            this.lvFile.TabIndex = 1;
            this.lvFile.UseCompatibleStateImageBehavior = false;
            this.lvFile.View = System.Windows.Forms.View.Details;
            this.lvFile.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvFile_ItemSelectionChanged);
            this.lvFile.Click += new System.EventHandler(this.lvFile_Click);
            this.lvFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFile_DragDrop);
            this.lvFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvFile_DragEnter);
            this.lvFile.DoubleClick += new System.EventHandler(this.lvFile_DoubleClick);
            // 
            // colFileName
            // 
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 388;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picViewer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.toolStripImage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(373, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 599);
            this.panel2.TabIndex = 3;
            // 
            // picViewer
            // 
            this.picViewer.BackColor = System.Drawing.Color.White;
            this.picViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picViewer.Location = new System.Drawing.Point(0, 49);
            this.picViewer.Name = "picViewer";
            this.picViewer.Size = new System.Drawing.Size(567, 550);
            this.picViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picViewer.TabIndex = 4;
            this.picViewer.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(567, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Image Viewer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripImage
            // 
            this.toolStripImage.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRotateLeft,
            this.toolStripButtonRotateRight,
            this.toolStripSeparator3,
            this.toolStripButtonAddText});
            this.toolStripImage.Location = new System.Drawing.Point(0, 0);
            this.toolStripImage.Name = "toolStripImage";
            this.toolStripImage.Size = new System.Drawing.Size(567, 31);
            this.toolStripImage.TabIndex = 2;
            this.toolStripImage.Text = "toolStrip1";
            this.toolStripImage.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripImage_ItemClicked);
            // 
            // toolStripButtonRotateLeft
            // 
            this.toolStripButtonRotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRotateLeft.Enabled = false;
            this.toolStripButtonRotateLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRotateLeft.Image")));
            this.toolStripButtonRotateLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRotateLeft.Name = "toolStripButtonRotateLeft";
            this.toolStripButtonRotateLeft.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRotateLeft.Text = "toolStripButton1";
            this.toolStripButtonRotateLeft.ToolTipText = "Rotate counterclockwise";
            // 
            // toolStripButtonRotateRight
            // 
            this.toolStripButtonRotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRotateRight.Enabled = false;
            this.toolStripButtonRotateRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRotateRight.Image")));
            this.toolStripButtonRotateRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRotateRight.Name = "toolStripButtonRotateRight";
            this.toolStripButtonRotateRight.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRotateRight.Text = "toolStripButton2";
            this.toolStripButtonRotateRight.ToolTipText = "Rotate clockwise";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonAddText
            // 
            this.toolStripButtonAddText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddText.Enabled = false;
            this.toolStripButtonAddText.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddText.Image")));
            this.toolStripButtonAddText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddText.Name = "toolStripButtonAddText";
            this.toolStripButtonAddText.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddText.ToolTipText = "Add Text";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveAll);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnUpload);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(0, 630);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 30);
            this.panel1.TabIndex = 2;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(162, 3);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAll.TabIndex = 2;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(321, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(82, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(242, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLogin,
            this.toolStripButtonChangePassword,
            this.toolStripSeparator6,
            this.toolStripButtonMySQLConfig,
            this.toolStripButtonFTPConfig,
            this.toolStripButtonMailConfig,
            this.toolStripButtonMailTemplate,
            this.toolStripButtonLogoConfig,
            this.toolStripButtonAdver,
            this.toolStripButtonResendMail,
            this.toolStripButtonDeleteUploadFiles,
            this.toolStripSeparator1,
            this.toolStripButtonBranch,
            this.toolStripButtonUsers,
            this.toolStripButtonTracking,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.toolStripComboBoxTemplates,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripComboBoxLogo,
            this.toolStripButtonApply,
            this.toolStripSeparator4,
            this.toolStripLabel3,
            this.toolStripComboBoxAds,
            this.toolStripButtonAddAds});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(940, 31);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
            // 
            // toolStripButtonLogin
            // 
            this.toolStripButtonLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLogin.Image = global::SonoOnlineResult.Properties.Resources.Login;
            this.toolStripButtonLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogin.Name = "toolStripButtonLogin";
            this.toolStripButtonLogin.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLogin.Text = "toolStripButton1";
            this.toolStripButtonLogin.ToolTipText = "Login";
            // 
            // toolStripButtonChangePassword
            // 
            this.toolStripButtonChangePassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChangePassword.Enabled = false;
            this.toolStripButtonChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChangePassword.Image")));
            this.toolStripButtonChangePassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChangePassword.Name = "toolStripButtonChangePassword";
            this.toolStripButtonChangePassword.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonChangePassword.Text = "toolStripButton2";
            this.toolStripButtonChangePassword.ToolTipText = "Change Password";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonMySQLConfig
            // 
            this.toolStripButtonMySQLConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMySQLConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMySQLConfig.Image")));
            this.toolStripButtonMySQLConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMySQLConfig.Name = "toolStripButtonMySQLConfig";
            this.toolStripButtonMySQLConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonMySQLConfig.Text = "toolStripButton1";
            this.toolStripButtonMySQLConfig.ToolTipText = "MySQL Configuration";
            // 
            // toolStripButtonFTPConfig
            // 
            this.toolStripButtonFTPConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFTPConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFTPConfig.Image")));
            this.toolStripButtonFTPConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFTPConfig.Name = "toolStripButtonFTPConfig";
            this.toolStripButtonFTPConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonFTPConfig.Text = "toolStripButton1";
            this.toolStripButtonFTPConfig.ToolTipText = "FTP Configuration";
            // 
            // toolStripButtonMailConfig
            // 
            this.toolStripButtonMailConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMailConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMailConfig.Image")));
            this.toolStripButtonMailConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMailConfig.Name = "toolStripButtonMailConfig";
            this.toolStripButtonMailConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonMailConfig.Text = "toolStripButton2";
            this.toolStripButtonMailConfig.ToolTipText = "Mail Configuration";
            // 
            // toolStripButtonMailTemplate
            // 
            this.toolStripButtonMailTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMailTemplate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMailTemplate.Image")));
            this.toolStripButtonMailTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMailTemplate.Name = "toolStripButtonMailTemplate";
            this.toolStripButtonMailTemplate.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonMailTemplate.Text = "toolStripButton1";
            this.toolStripButtonMailTemplate.ToolTipText = "Mail Templates";
            // 
            // toolStripButtonLogoConfig
            // 
            this.toolStripButtonLogoConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLogoConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLogoConfig.Image")));
            this.toolStripButtonLogoConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogoConfig.Name = "toolStripButtonLogoConfig";
            this.toolStripButtonLogoConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLogoConfig.Text = "toolStripButton1";
            this.toolStripButtonLogoConfig.ToolTipText = "Logo Configuration";
            // 
            // toolStripButtonAdver
            // 
            this.toolStripButtonAdver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAdver.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdver.Image")));
            this.toolStripButtonAdver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdver.Name = "toolStripButtonAdver";
            this.toolStripButtonAdver.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAdver.Text = "toolStripButton1";
            this.toolStripButtonAdver.ToolTipText = "Ads Configuration";
            // 
            // toolStripButtonResendMail
            // 
            this.toolStripButtonResendMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResendMail.Enabled = false;
            this.toolStripButtonResendMail.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResendMail.Image")));
            this.toolStripButtonResendMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResendMail.Name = "toolStripButtonResendMail";
            this.toolStripButtonResendMail.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonResendMail.Text = "toolStripButton1";
            this.toolStripButtonResendMail.ToolTipText = "Resend Mail";
            // 
            // toolStripButtonDeleteUploadFiles
            // 
            this.toolStripButtonDeleteUploadFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteUploadFiles.Enabled = false;
            this.toolStripButtonDeleteUploadFiles.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteUploadFiles.Image")));
            this.toolStripButtonDeleteUploadFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteUploadFiles.Name = "toolStripButtonDeleteUploadFiles";
            this.toolStripButtonDeleteUploadFiles.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonDeleteUploadFiles.Text = "toolStripButton1";
            this.toolStripButtonDeleteUploadFiles.ToolTipText = "Delete Upload File";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripButtonBranch
            // 
            this.toolStripButtonBranch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBranch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBranch.Image")));
            this.toolStripButtonBranch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBranch.Name = "toolStripButtonBranch";
            this.toolStripButtonBranch.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonBranch.Text = "toolStripButton1";
            this.toolStripButtonBranch.ToolTipText = "Branch List";
            this.toolStripButtonBranch.Visible = false;
            // 
            // toolStripButtonUsers
            // 
            this.toolStripButtonUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUsers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUsers.Image")));
            this.toolStripButtonUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUsers.Name = "toolStripButtonUsers";
            this.toolStripButtonUsers.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonUsers.Text = "toolStripButton2";
            this.toolStripButtonUsers.ToolTipText = "User List";
            this.toolStripButtonUsers.Visible = false;
            // 
            // toolStripButtonTracking
            // 
            this.toolStripButtonTracking.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTracking.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTracking.Image")));
            this.toolStripButtonTracking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTracking.Name = "toolStripButtonTracking";
            this.toolStripButtonTracking.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonTracking.Text = "toolStripButton3";
            this.toolStripButtonTracking.ToolTipText = "Tracking";
            this.toolStripButtonTracking.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 28);
            this.toolStripLabel1.Text = "Image Templates:";
            // 
            // toolStripComboBoxTemplates
            // 
            this.toolStripComboBoxTemplates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBoxTemplates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.toolStripComboBoxTemplates.Enabled = false;
            this.toolStripComboBoxTemplates.Name = "toolStripComboBoxTemplates";
            this.toolStripComboBoxTemplates.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTemplates_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Blue;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(37, 28);
            this.toolStripLabel2.Text = "Logo:";
            // 
            // toolStripComboBoxLogo
            // 
            this.toolStripComboBoxLogo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBoxLogo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.toolStripComboBoxLogo.Enabled = false;
            this.toolStripComboBoxLogo.Name = "toolStripComboBoxLogo";
            this.toolStripComboBoxLogo.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxLogo.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLogo_SelectedIndexChanged);
            // 
            // toolStripButtonApply
            // 
            this.toolStripButtonApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonApply.ForeColor = System.Drawing.Color.Red;
            this.toolStripButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonApply.Image")));
            this.toolStripButtonApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonApply.Name = "toolStripButtonApply";
            this.toolStripButtonApply.Size = new System.Drawing.Size(42, 28);
            this.toolStripButtonApply.Text = "Apply";
            this.toolStripButtonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel3.ForeColor = System.Drawing.Color.Blue;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(30, 28);
            this.toolStripLabel3.Text = "Ads:";
            // 
            // toolStripComboBoxAds
            // 
            this.toolStripComboBoxAds.Enabled = false;
            this.toolStripComboBoxAds.Name = "toolStripComboBoxAds";
            this.toolStripComboBoxAds.Size = new System.Drawing.Size(121, 31);
            // 
            // toolStripButtonAddAds
            // 
            this.toolStripButtonAddAds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddAds.Enabled = false;
            this.toolStripButtonAddAds.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddAds.Image")));
            this.toolStripButtonAddAds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddAds.Name = "toolStripButtonAddAds";
            this.toolStripButtonAddAds.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddAds.Text = "toolStripButton1";
            this.toolStripButtonAddAds.ToolTipText = "Add Ads";
            // 
            // UploadFile
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 660);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UploadFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sono Online Result";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadFile_FormClosing);
            this.Load += new System.EventHandler(this.UploadFile_Load);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picViewer)).EndInit();
            this.toolStripImage.ResumeLayout(false);
            this.toolStripImage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonFTPConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonMailConfig;
        private System.Windows.Forms.ListView lvFile;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripButton toolStripButtonMySQLConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonMailTemplate;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogoConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTemplates;
        private System.Windows.Forms.PictureBox picViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripImage;
        private System.Windows.Forms.ToolStripButton toolStripButtonRotateLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonRotateRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLogo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddText;
        private System.Windows.Forms.ToolStripButton toolStripButtonApply;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdver;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxAds;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddAds;
        private System.Windows.Forms.ToolStripButton toolStripButtonResendMail;
        private System.Windows.Forms.ToolStripButton toolStripButtonBranch;
        private System.Windows.Forms.ToolStripButton toolStripButtonUsers;
        private System.Windows.Forms.ToolStripButton toolStripButtonTracking;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogin;
        private System.Windows.Forms.ToolStripButton toolStripButtonChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteUploadFiles;
    }
}