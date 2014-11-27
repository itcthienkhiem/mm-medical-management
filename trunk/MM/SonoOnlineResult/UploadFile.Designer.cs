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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMySQLConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFTPConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMailConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMailTemplate = new System.Windows.Forms.ToolStripButton();
            this.lvFile = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picViewer = new System.Windows.Forms.PictureBox();
            this.toolStripMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMySQLConfig,
            this.toolStripButtonFTPConfig,
            this.toolStripButtonMailConfig,
            this.toolStripButtonMailTemplate});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(858, 31);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
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
            // lvFile
            // 
            this.lvFile.AllowDrop = true;
            this.lvFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName});
            this.lvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFile.FullRowSelect = true;
            this.lvFile.GridLines = true;
            this.lvFile.HideSelection = false;
            this.lvFile.Location = new System.Drawing.Point(0, 0);
            this.lvFile.Name = "lvFile";
            this.lvFile.Size = new System.Drawing.Size(522, 482);
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
            this.colFileName.Width = 502;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRemoveAll);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnUpload);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 30);
            this.panel1.TabIndex = 2;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.picViewer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(522, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(336, 482);
            this.panel2.TabIndex = 3;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.lvFile);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(522, 482);
            this.panel3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image Viewer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picViewer
            // 
            this.picViewer.BackColor = System.Drawing.Color.White;
            this.picViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picViewer.Location = new System.Drawing.Point(0, 18);
            this.picViewer.Name = "picViewer";
            this.picViewer.Size = new System.Drawing.Size(336, 464);
            this.picViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picViewer.TabIndex = 1;
            this.picViewer.TabStop = false;
            // 
            // UploadFile
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 543);
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
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picViewer)).EndInit();
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
        private System.Windows.Forms.PictureBox picViewer;
        private System.Windows.Forms.Label label1;
    }
}