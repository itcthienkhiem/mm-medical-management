namespace MM
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._mainStatus = new System.Windows.Forms.StatusStrip();
            this._mainToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._mainPanel = new System.Windows.Forms.Panel();
            this.tbServiceList = new System.Windows.Forms.ToolStripButton();
            this.tbServicePriceList = new System.Windows.Forms.ToolStripButton();
            this.tbOpenPatient = new System.Windows.Forms.ToolStripButton();
            this.tbPatientList = new System.Windows.Forms.ToolStripButton();
            this.tbHelp = new System.Windows.Forms.ToolStripButton();
            this.tbAbout = new System.Windows.Forms.ToolStripButton();
            this.databaseConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicePriceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalManagementHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMedicalManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainMenu.SuspendLayout();
            this._mainToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.serviceToolStripMenuItem,
            this.patientToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._mainMenu.Location = new System.Drawing.Point(0, 0);
            this._mainMenu.Name = "_mainMenu";
            this._mainMenu.Size = new System.Drawing.Size(928, 24);
            this._mainMenu.TabIndex = 0;
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseConfigurationToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.systemToolStripMenuItem.Text = "&System";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceListToolStripMenuItem,
            this.toolStripSeparator4,
            this.servicePriceListToolStripMenuItem});
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.serviceToolStripMenuItem.Text = "&Service";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPatientToolStripMenuItem,
            this.toolStripSeparator3,
            this.patientListToolStripMenuItem});
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            this.patientToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.patientToolStripMenuItem.Text = "&Patient";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medicalManagementHelpToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutMedicalManagementToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // _mainStatus
            // 
            this._mainStatus.Location = new System.Drawing.Point(0, 538);
            this._mainStatus.Name = "_mainStatus";
            this._mainStatus.Size = new System.Drawing.Size(928, 22);
            this._mainStatus.TabIndex = 1;
            this._mainStatus.Text = "statusStrip1";
            // 
            // _mainToolbar
            // 
            this._mainToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbServiceList,
            this.tbServicePriceList,
            this.toolStripSeparator6,
            this.tbOpenPatient,
            this.tbPatientList,
            this.toolStripSeparator5,
            this.tbHelp,
            this.tbAbout});
            this._mainToolbar.Location = new System.Drawing.Point(0, 24);
            this._mainToolbar.Name = "_mainToolbar";
            this._mainToolbar.Size = new System.Drawing.Size(928, 31);
            this._mainToolbar.TabIndex = 2;
            this._mainToolbar.Text = "toolStrip1";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // _mainPanel
            // 
            this._mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 55);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(928, 483);
            this._mainPanel.TabIndex = 3;
            // 
            // tbServiceList
            // 
            this.tbServiceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbServiceList.Image = global::MM.Properties.Resources.accept_icon;
            this.tbServiceList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbServiceList.Name = "tbServiceList";
            this.tbServiceList.Size = new System.Drawing.Size(28, 28);
            this.tbServiceList.ToolTipText = "Service List";
            // 
            // tbServicePriceList
            // 
            this.tbServicePriceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbServicePriceList.Image = global::MM.Properties.Resources.currency_dollar_yellow;
            this.tbServicePriceList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbServicePriceList.Name = "tbServicePriceList";
            this.tbServicePriceList.Size = new System.Drawing.Size(28, 28);
            this.tbServicePriceList.ToolTipText = "Service Price List";
            // 
            // tbOpenPatient
            // 
            this.tbOpenPatient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOpenPatient.Image = global::MM.Properties.Resources.open;
            this.tbOpenPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOpenPatient.Name = "tbOpenPatient";
            this.tbOpenPatient.Size = new System.Drawing.Size(28, 28);
            this.tbOpenPatient.ToolTipText = "Open Patient";
            // 
            // tbPatientList
            // 
            this.tbPatientList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPatientList.Image = global::MM.Properties.Resources._1320161545_people;
            this.tbPatientList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPatientList.Name = "tbPatientList";
            this.tbPatientList.Size = new System.Drawing.Size(28, 28);
            this.tbPatientList.ToolTipText = "Patient List";
            // 
            // tbHelp
            // 
            this.tbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbHelp.Image = global::MM.Properties.Resources.help;
            this.tbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.Size = new System.Drawing.Size(28, 28);
            this.tbHelp.ToolTipText = "Medicel Management Help";
            // 
            // tbAbout
            // 
            this.tbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAbout.Image = global::MM.Properties.Resources.about;
            this.tbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.Size = new System.Drawing.Size(28, 28);
            this.tbAbout.ToolTipText = "About Medicel Management";
            // 
            // databaseConfigurationToolStripMenuItem
            // 
            this.databaseConfigurationToolStripMenuItem.Image = global::MM.Properties.Resources.connect_info;
            this.databaseConfigurationToolStripMenuItem.Name = "databaseConfigurationToolStripMenuItem";
            this.databaseConfigurationToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.databaseConfigurationToolStripMenuItem.Text = "&Database Configuration";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MM.Properties.Resources.exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // serviceListToolStripMenuItem
            // 
            this.serviceListToolStripMenuItem.Image = global::MM.Properties.Resources.accept_icon;
            this.serviceListToolStripMenuItem.Name = "serviceListToolStripMenuItem";
            this.serviceListToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.serviceListToolStripMenuItem.Text = "Service List";
            // 
            // servicePriceListToolStripMenuItem
            // 
            this.servicePriceListToolStripMenuItem.Image = global::MM.Properties.Resources.currency_dollar_yellow;
            this.servicePriceListToolStripMenuItem.Name = "servicePriceListToolStripMenuItem";
            this.servicePriceListToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.servicePriceListToolStripMenuItem.Text = "Service Price List";
            // 
            // openPatientToolStripMenuItem
            // 
            this.openPatientToolStripMenuItem.Image = global::MM.Properties.Resources.open;
            this.openPatientToolStripMenuItem.Name = "openPatientToolStripMenuItem";
            this.openPatientToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openPatientToolStripMenuItem.Text = "Open Patient";
            // 
            // patientListToolStripMenuItem
            // 
            this.patientListToolStripMenuItem.Image = global::MM.Properties.Resources._1320161545_people;
            this.patientListToolStripMenuItem.Name = "patientListToolStripMenuItem";
            this.patientListToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.patientListToolStripMenuItem.Text = "Patient List";
            // 
            // medicalManagementHelpToolStripMenuItem
            // 
            this.medicalManagementHelpToolStripMenuItem.Image = global::MM.Properties.Resources.help;
            this.medicalManagementHelpToolStripMenuItem.Name = "medicalManagementHelpToolStripMenuItem";
            this.medicalManagementHelpToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.medicalManagementHelpToolStripMenuItem.Text = "Medical Management Help";
            // 
            // aboutMedicalManagementToolStripMenuItem
            // 
            this.aboutMedicalManagementToolStripMenuItem.Image = global::MM.Properties.Resources.about;
            this.aboutMedicalManagementToolStripMenuItem.Name = "aboutMedicalManagementToolStripMenuItem";
            this.aboutMedicalManagementToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.aboutMedicalManagementToolStripMenuItem.Text = "About Medical Management";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 560);
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this._mainToolbar);
            this.Controls.Add(this._mainStatus);
            this.Controls.Add(this._mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._mainMenu;
            this.Name = "MainForm";
            this.Text = "Medical Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this._mainToolbar.ResumeLayout(false);
            this._mainToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.StatusStrip _mainStatus;
        private System.Windows.Forms.ToolStrip _mainToolbar;
        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicePriceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicalManagementHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutMedicalManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbOpenPatient;
        private System.Windows.Forms.ToolStripButton tbPatientList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbHelp;
        private System.Windows.Forms.ToolStripButton tbAbout;
        private System.Windows.Forms.ToolStripButton tbServiceList;
        private System.Windows.Forms.ToolStripButton tbServicePriceList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;



    }
}

