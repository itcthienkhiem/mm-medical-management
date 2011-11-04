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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbServiceList = new System.Windows.Forms.ToolStripButton();
            this._mainToolbar = new System.Windows.Forms.ToolStrip();
            this.tbDoctorList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbOpenPatient = new System.Windows.Forms.ToolStripButton();
            this.tbPatientList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbHelp = new System.Windows.Forms.ToolStripButton();
            this.tbAbout = new System.Windows.Forms.ToolStripButton();
            this._mainStatus = new System.Windows.Forms.StatusStrip();
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.patientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalManagementHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMedicalManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._mainToolbar.SuspendLayout();
            this._mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tbServiceList
            // 
            this.tbServiceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbServiceList.Image = global::MM.Properties.Resources.accept_icon;
            resources.ApplyResources(this.tbServiceList, "tbServiceList");
            this.tbServiceList.Name = "tbServiceList";
            this.tbServiceList.Tag = "Services List";
            // 
            // _mainToolbar
            // 
            this._mainToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbServiceList,
            this.toolStripSeparator6,
            this.tbDoctorList,
            this.toolStripSeparator1,
            this.tbOpenPatient,
            this.tbPatientList,
            this.toolStripSeparator5,
            this.tbHelp,
            this.tbAbout});
            resources.ApplyResources(this._mainToolbar, "_mainToolbar");
            this._mainToolbar.Name = "_mainToolbar";
            this._mainToolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._mainToolbar_ItemClicked);
            // 
            // tbDoctorList
            // 
            this.tbDoctorList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbDoctorList.Image = global::MM.Properties.Resources.Doctor_32;
            resources.ApplyResources(this.tbDoctorList, "tbDoctorList");
            this.tbDoctorList.Name = "tbDoctorList";
            this.tbDoctorList.Tag = "Doctor List";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tbOpenPatient
            // 
            this.tbOpenPatient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOpenPatient.Image = global::MM.Properties.Resources.open;
            resources.ApplyResources(this.tbOpenPatient, "tbOpenPatient");
            this.tbOpenPatient.Name = "tbOpenPatient";
            this.tbOpenPatient.Tag = "Open Patient";
            // 
            // tbPatientList
            // 
            this.tbPatientList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPatientList.Image = global::MM.Properties.Resources._1320161545_people;
            resources.ApplyResources(this.tbPatientList, "tbPatientList");
            this.tbPatientList.Name = "tbPatientList";
            this.tbPatientList.Tag = "Patient List";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tbHelp
            // 
            this.tbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbHelp.Image = global::MM.Properties.Resources.help;
            resources.ApplyResources(this.tbHelp, "tbHelp");
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.Tag = "Help";
            // 
            // tbAbout
            // 
            this.tbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAbout.Image = global::MM.Properties.Resources.about;
            resources.ApplyResources(this.tbAbout, "tbAbout");
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.Tag = "About";
            // 
            // _mainStatus
            // 
            resources.ApplyResources(this._mainStatus, "_mainStatus");
            this._mainStatus.Name = "_mainStatus";
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.servicesToolStripMenuItem,
            this.doctorToolStripMenuItem,
            this.patientToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this._mainMenu, "_mainMenu");
            this._mainMenu.Name = "_mainMenu";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseConfigurationToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // databaseConfigurationToolStripMenuItem
            // 
            this.databaseConfigurationToolStripMenuItem.Image = global::MM.Properties.Resources.connect_info;
            this.databaseConfigurationToolStripMenuItem.Name = "databaseConfigurationToolStripMenuItem";
            resources.ApplyResources(this.databaseConfigurationToolStripMenuItem, "databaseConfigurationToolStripMenuItem");
            this.databaseConfigurationToolStripMenuItem.Tag = "Database Configuration";
            this.databaseConfigurationToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MM.Properties.Resources.exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Tag = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // servicesToolStripMenuItem
            // 
            this.servicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceListToolStripMenuItem});
            this.servicesToolStripMenuItem.Name = "servicesToolStripMenuItem";
            resources.ApplyResources(this.servicesToolStripMenuItem, "servicesToolStripMenuItem");
            // 
            // serviceListToolStripMenuItem
            // 
            this.serviceListToolStripMenuItem.Image = global::MM.Properties.Resources.accept_icon;
            this.serviceListToolStripMenuItem.Name = "serviceListToolStripMenuItem";
            resources.ApplyResources(this.serviceListToolStripMenuItem, "serviceListToolStripMenuItem");
            this.serviceListToolStripMenuItem.Tag = "Services List";
            this.serviceListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // doctorToolStripMenuItem
            // 
            this.doctorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doctorListToolStripMenuItem});
            this.doctorToolStripMenuItem.Name = "doctorToolStripMenuItem";
            resources.ApplyResources(this.doctorToolStripMenuItem, "doctorToolStripMenuItem");
            // 
            // doctorListToolStripMenuItem
            // 
            this.doctorListToolStripMenuItem.Image = global::MM.Properties.Resources.Doctor_32;
            this.doctorListToolStripMenuItem.Name = "doctorListToolStripMenuItem";
            resources.ApplyResources(this.doctorListToolStripMenuItem, "doctorListToolStripMenuItem");
            this.doctorListToolStripMenuItem.Tag = "Doctor List";
            this.doctorListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPatientToolStripMenuItem,
            this.toolStripSeparator3,
            this.patientListToolStripMenuItem});
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            resources.ApplyResources(this.patientToolStripMenuItem, "patientToolStripMenuItem");
            // 
            // openPatientToolStripMenuItem
            // 
            this.openPatientToolStripMenuItem.Image = global::MM.Properties.Resources.open;
            this.openPatientToolStripMenuItem.Name = "openPatientToolStripMenuItem";
            resources.ApplyResources(this.openPatientToolStripMenuItem, "openPatientToolStripMenuItem");
            this.openPatientToolStripMenuItem.Tag = "Open Patient";
            this.openPatientToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // patientListToolStripMenuItem
            // 
            this.patientListToolStripMenuItem.Image = global::MM.Properties.Resources._1320161545_people;
            this.patientListToolStripMenuItem.Name = "patientListToolStripMenuItem";
            resources.ApplyResources(this.patientListToolStripMenuItem, "patientListToolStripMenuItem");
            this.patientListToolStripMenuItem.Tag = "Patient List";
            this.patientListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medicalManagementHelpToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutMedicalManagementToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // medicalManagementHelpToolStripMenuItem
            // 
            this.medicalManagementHelpToolStripMenuItem.Image = global::MM.Properties.Resources.help;
            this.medicalManagementHelpToolStripMenuItem.Name = "medicalManagementHelpToolStripMenuItem";
            resources.ApplyResources(this.medicalManagementHelpToolStripMenuItem, "medicalManagementHelpToolStripMenuItem");
            this.medicalManagementHelpToolStripMenuItem.Tag = "Help";
            this.medicalManagementHelpToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // aboutMedicalManagementToolStripMenuItem
            // 
            this.aboutMedicalManagementToolStripMenuItem.Image = global::MM.Properties.Resources.about;
            this.aboutMedicalManagementToolStripMenuItem.Name = "aboutMedicalManagementToolStripMenuItem";
            resources.ApplyResources(this.aboutMedicalManagementToolStripMenuItem, "aboutMedicalManagementToolStripMenuItem");
            this.aboutMedicalManagementToolStripMenuItem.Tag = "About";
            this.aboutMedicalManagementToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // _mainPanel
            // 
            resources.ApplyResources(this._mainPanel, "_mainPanel");
            this._mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainPanel.Name = "_mainPanel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainToolbar);
            this.Controls.Add(this._mainStatus);
            this.Controls.Add(this._mainMenu);
            this.Controls.Add(this._mainPanel);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this._mainToolbar.ResumeLayout(false);
            this._mainToolbar.PerformLayout();
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tbServiceList;
        private System.Windows.Forms.ToolStrip _mainToolbar;
        private System.Windows.Forms.ToolStripButton tbDoctorList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbOpenPatient;
        private System.Windows.Forms.ToolStripButton tbPatientList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbHelp;
        private System.Windows.Forms.ToolStripButton tbAbout;
        private System.Windows.Forms.StatusStrip _mainStatus;
        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doctorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doctorListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem patientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicalManagementHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutMedicalManagementToolStripMenuItem;
        private System.Windows.Forms.Panel _mainPanel;




    }
}

