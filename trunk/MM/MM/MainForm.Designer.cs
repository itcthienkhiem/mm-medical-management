﻿namespace MM
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
            this._mainPanel = new System.Windows.Forms.Panel();
            this._uSymptomList = new MM.Controls.uSymptomList();
            this._uSpecialityList = new MM.Controls.uSpecialityList();
            this._uPatientHistory = new MM.Controls.uPatientHistory();
            this._uPatientList = new MM.Controls.uPatientList();
            this._uDocStaffList = new MM.Controls.uDocStaffList();
            this._uServicesList = new MM.Controls.uServicesList();
            this._mainToolbar = new System.Windows.Forms.ToolStrip();
            this.tbLogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tbServiceList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSpecialityList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tbDoctorList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbOpenPatient = new System.Windows.Forms.ToolStripButton();
            this.tbPatientList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSympton = new System.Windows.Forms.ToolStripButton();
            this._mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialityListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.patientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symptomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symptomListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalManagementHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMedicalManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainPanel.SuspendLayout();
            this._mainToolbar.SuspendLayout();
            this._mainStatus.SuspendLayout();
            this._mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this._mainPanel, "_mainPanel");
            this._mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainPanel.Controls.Add(this._uSymptomList);
            this._mainPanel.Controls.Add(this._uSpecialityList);
            this._mainPanel.Controls.Add(this._uPatientHistory);
            this._mainPanel.Controls.Add(this._uPatientList);
            this._mainPanel.Controls.Add(this._uDocStaffList);
            this._mainPanel.Controls.Add(this._uServicesList);
            this._mainPanel.Name = "_mainPanel";
            // 
            // _uSymptomList
            // 
            resources.ApplyResources(this._uSymptomList, "_uSymptomList");
            this._uSymptomList.Name = "_uSymptomList";
            // 
            // _uSpecialityList
            // 
            resources.ApplyResources(this._uSpecialityList, "_uSpecialityList");
            this._uSpecialityList.Name = "_uSpecialityList";
            // 
            // _uPatientHistory
            // 
            resources.ApplyResources(this._uPatientHistory, "_uPatientHistory");
            this._uPatientHistory.Name = "_uPatientHistory";
            this._uPatientHistory.PatientRow = null;
            // 
            // _uPatientList
            // 
            resources.ApplyResources(this._uPatientList, "_uPatientList");
            this._uPatientList.Name = "_uPatientList";
            // 
            // _uDocStaffList
            // 
            resources.ApplyResources(this._uDocStaffList, "_uDocStaffList");
            this._uDocStaffList.Name = "_uDocStaffList";
            // 
            // _uServicesList
            // 
            resources.ApplyResources(this._uServicesList, "_uServicesList");
            this._uServicesList.Name = "_uServicesList";
            // 
            // _mainToolbar
            // 
            this._mainToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLogin,
            this.toolStripSeparator8,
            this.tbServiceList,
            this.toolStripSeparator6,
            this.tbSpecialityList,
            this.toolStripSeparator9,
            this.tbDoctorList,
            this.toolStripSeparator1,
            this.tbOpenPatient,
            this.tbPatientList,
            this.toolStripSeparator5,
            this.tbSympton});
            resources.ApplyResources(this._mainToolbar, "_mainToolbar");
            this._mainToolbar.Name = "_mainToolbar";
            this._mainToolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._mainToolbar_ItemClicked);
            // 
            // tbLogin
            // 
            this.tbLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbLogin.Image = global::MM.Properties.Resources.Login_icon;
            resources.ApplyResources(this.tbLogin, "tbLogin");
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Tag = "Login";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // tbServiceList
            // 
            this.tbServiceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbServiceList, "tbServiceList");
            this.tbServiceList.Image = global::MM.Properties.Resources.accept_icon;
            this.tbServiceList.Name = "tbServiceList";
            this.tbServiceList.Tag = "Services List";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tbSpecialityList
            // 
            this.tbSpecialityList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbSpecialityList, "tbSpecialityList");
            this.tbSpecialityList.Image = global::MM.Properties.Resources.stethoscope_icon;
            this.tbSpecialityList.Name = "tbSpecialityList";
            this.tbSpecialityList.Tag = "Speciality List";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // tbDoctorList
            // 
            this.tbDoctorList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbDoctorList, "tbDoctorList");
            this.tbDoctorList.Image = global::MM.Properties.Resources.Doctor_32;
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
            resources.ApplyResources(this.tbOpenPatient, "tbOpenPatient");
            this.tbOpenPatient.Image = global::MM.Properties.Resources.folder_customer_icon;
            this.tbOpenPatient.Name = "tbOpenPatient";
            this.tbOpenPatient.Tag = "Open Patient";
            // 
            // tbPatientList
            // 
            this.tbPatientList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbPatientList, "tbPatientList");
            this.tbPatientList.Image = global::MM.Properties.Resources._1320161545_people;
            this.tbPatientList.Name = "tbPatientList";
            this.tbPatientList.Tag = "Patient List";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tbSympton
            // 
            this.tbSympton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbSympton, "tbSympton");
            this.tbSympton.Image = global::MM.Properties.Resources.research;
            this.tbSympton.Name = "tbSympton";
            this.tbSympton.Tag = "Symptom List";
            // 
            // _mainStatus
            // 
            this._mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            resources.ApplyResources(this._mainStatus, "_mainStatus");
            this._mainStatus.Name = "_mainStatus";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Spring = true;
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.servicesToolStripMenuItem,
            this.specialityToolStripMenuItem,
            this.doctorToolStripMenuItem,
            this.patientToolStripMenuItem,
            this.symptomToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this._mainMenu, "_mainMenu");
            this._mainMenu.Name = "_mainMenu";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseConfigurationToolStripMenuItem,
            this.toolStripSeparator4,
            this.loginToolStripMenuItem,
            this.toolStripSeparator7,
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
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Image = global::MM.Properties.Resources.Login_icon;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            resources.ApplyResources(this.loginToolStripMenuItem, "loginToolStripMenuItem");
            this.loginToolStripMenuItem.Tag = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MM.Properties.Resources.Log_Out_icon;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Tag = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // servicesToolStripMenuItem
            // 
            this.servicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceListToolStripMenuItem});
            resources.ApplyResources(this.servicesToolStripMenuItem, "servicesToolStripMenuItem");
            this.servicesToolStripMenuItem.Name = "servicesToolStripMenuItem";
            // 
            // serviceListToolStripMenuItem
            // 
            this.serviceListToolStripMenuItem.Image = global::MM.Properties.Resources.accept_icon;
            this.serviceListToolStripMenuItem.Name = "serviceListToolStripMenuItem";
            resources.ApplyResources(this.serviceListToolStripMenuItem, "serviceListToolStripMenuItem");
            this.serviceListToolStripMenuItem.Tag = "Services List";
            this.serviceListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // specialityToolStripMenuItem
            // 
            this.specialityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.specialityListToolStripMenuItem});
            resources.ApplyResources(this.specialityToolStripMenuItem, "specialityToolStripMenuItem");
            this.specialityToolStripMenuItem.Name = "specialityToolStripMenuItem";
            // 
            // specialityListToolStripMenuItem
            // 
            this.specialityListToolStripMenuItem.Image = global::MM.Properties.Resources.stethoscope_icon;
            this.specialityListToolStripMenuItem.Name = "specialityListToolStripMenuItem";
            resources.ApplyResources(this.specialityListToolStripMenuItem, "specialityListToolStripMenuItem");
            this.specialityListToolStripMenuItem.Tag = "Speciality List";
            this.specialityListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // doctorToolStripMenuItem
            // 
            this.doctorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doctorListToolStripMenuItem});
            resources.ApplyResources(this.doctorToolStripMenuItem, "doctorToolStripMenuItem");
            this.doctorToolStripMenuItem.Name = "doctorToolStripMenuItem";
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
            resources.ApplyResources(this.patientToolStripMenuItem, "patientToolStripMenuItem");
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            // 
            // openPatientToolStripMenuItem
            // 
            this.openPatientToolStripMenuItem.Image = global::MM.Properties.Resources.folder_customer_icon;
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
            // symptomToolStripMenuItem
            // 
            this.symptomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.symptomListToolStripMenuItem});
            resources.ApplyResources(this.symptomToolStripMenuItem, "symptomToolStripMenuItem");
            this.symptomToolStripMenuItem.Name = "symptomToolStripMenuItem";
            // 
            // symptomListToolStripMenuItem
            // 
            this.symptomListToolStripMenuItem.Image = global::MM.Properties.Resources.research;
            this.symptomListToolStripMenuItem.Name = "symptomListToolStripMenuItem";
            resources.ApplyResources(this.symptomListToolStripMenuItem, "symptomListToolStripMenuItem");
            this.symptomListToolStripMenuItem.Tag = "Symptom List";
            this.symptomListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
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
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this._mainToolbar);
            this.Controls.Add(this._mainStatus);
            this.Controls.Add(this._mainMenu);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._mainPanel.ResumeLayout(false);
            this._mainToolbar.ResumeLayout(false);
            this._mainToolbar.PerformLayout();
            this._mainStatus.ResumeLayout(false);
            this._mainStatus.PerformLayout();
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
        private Controls.uServicesList _uServicesList;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tbLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Controls.uDocStaffList _uDocStaffList;
        private Controls.uPatientList _uPatientList;
        private Controls.uPatientHistory _uPatientHistory;
        private System.Windows.Forms.ToolStripMenuItem specialityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialityListToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbSpecialityList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private Controls.uSpecialityList _uSpecialityList;
        private System.Windows.Forms.ToolStripMenuItem symptomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symptomListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbSympton;
        private Controls.uSymptomList _uSymptomList;




    }
}

