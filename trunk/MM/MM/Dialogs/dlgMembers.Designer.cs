namespace MM.Dialogs
{
    partial class dlgMembers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgMembers));
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pService = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chkServiceChecked = new System.Windows.Forms.CheckBox();
            this.dgService = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewCheckBoxXColumn2 = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtSearchService = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgMembers = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderAsStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdentityCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            this.pService.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgService)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // btnOK
            // 
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(256, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(335, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pService);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(666, 534);
            this.panel4.TabIndex = 8;
            // 
            // pService
            // 
            this.pService.Controls.Add(this.panel8);
            this.pService.Controls.Add(this.panel7);
            this.pService.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pService.Location = new System.Drawing.Point(0, 303);
            this.pService.Name = "pService";
            this.pService.Size = new System.Drawing.Size(666, 231);
            this.pService.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.chkServiceChecked);
            this.panel8.Controls.Add(this.dgService);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 42);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(666, 189);
            this.panel8.TabIndex = 3;
            // 
            // chkServiceChecked
            // 
            this.chkServiceChecked.AutoSize = true;
            this.chkServiceChecked.Location = new System.Drawing.Point(45, 4);
            this.chkServiceChecked.Name = "chkServiceChecked";
            this.chkServiceChecked.Size = new System.Drawing.Size(15, 14);
            this.chkServiceChecked.TabIndex = 5;
            this.chkServiceChecked.UseVisualStyleBackColor = true;
            this.chkServiceChecked.CheckedChanged += new System.EventHandler(this.chkServiceChecked_CheckedChanged);
            // 
            // dgService
            // 
            this.dgService.AllowUserToAddRows = false;
            this.dgService.AllowUserToDeleteRows = false;
            this.dgService.AllowUserToOrderColumns = true;
            this.dgService.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgService.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgService.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxXColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgService.DataSource = this.serviceBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgService.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgService.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgService.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgService.HighlightSelectedColumnHeaders = false;
            this.dgService.Location = new System.Drawing.Point(0, 0);
            this.dgService.MultiSelect = false;
            this.dgService.Name = "dgService";
            this.dgService.ReadOnly = true;
            this.dgService.RowHeadersWidth = 30;
            this.dgService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgService.Size = new System.Drawing.Size(666, 189);
            this.dgService.TabIndex = 4;
            // 
            // dataGridViewCheckBoxXColumn2
            // 
            this.dataGridViewCheckBoxXColumn2.Checked = true;
            this.dataGridViewCheckBoxXColumn2.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.dataGridViewCheckBoxXColumn2.CheckValue = "N";
            this.dataGridViewCheckBoxXColumn2.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewCheckBoxXColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCheckBoxXColumn2.Frozen = true;
            this.dataGridViewCheckBoxXColumn2.HeaderText = "";
            this.dataGridViewCheckBoxXColumn2.Name = "dataGridViewCheckBoxXColumn2";
            this.dataGridViewCheckBoxXColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxXColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxXColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxXColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Code";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã dịch vụ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên dịch vụ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 420;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtSearchService);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(666, 42);
            this.panel7.TabIndex = 2;
            // 
            // txtSearchService
            // 
            this.txtSearchService.Location = new System.Drawing.Point(78, 12);
            this.txtSearchService.Name = "txtSearchService";
            this.txtSearchService.Size = new System.Drawing.Size(306, 20);
            this.txtSearchService.TabIndex = 0;
            this.txtSearchService.TextChanged += new System.EventHandler(this.txtSearchService_TextChanged);
            this.txtSearchService.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchService_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tìm dịch vụ:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgMembers);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(666, 496);
            this.panel3.TabIndex = 1;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 9;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgMembers
            // 
            this.dgMembers.AllowUserToAddRows = false;
            this.dgMembers.AllowUserToDeleteRows = false;
            this.dgMembers.AllowUserToOrderColumns = true;
            this.dgMembers.AutoGenerateColumns = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.fileNumDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.dobStrDataGridViewTextBoxColumn,
            this.genderAsStrDataGridViewTextBoxColumn,
            this.IdentityCard});
            this.dgMembers.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMembers.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMembers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgMembers.HighlightSelectedColumnHeaders = false;
            this.dgMembers.Location = new System.Drawing.Point(0, 0);
            this.dgMembers.MultiSelect = false;
            this.dgMembers.Name = "dgMembers";
            this.dgMembers.ReadOnly = true;
            this.dgMembers.RowHeadersWidth = 30;
            this.dgMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMembers.Size = new System.Drawing.Size(666, 496);
            this.dgMembers.TabIndex = 8;
            this.dgMembers.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMembers_ColumnHeaderMouseClick);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle6;
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // fileNumDataGridViewTextBoxColumn
            // 
            this.fileNumDataGridViewTextBoxColumn.DataPropertyName = "FileNum";
            this.fileNumDataGridViewTextBoxColumn.HeaderText = "Mã bệnh nhân";
            this.fileNumDataGridViewTextBoxColumn.Name = "fileNumDataGridViewTextBoxColumn";
            this.fileNumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Họ tên";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 180;
            // 
            // dobStrDataGridViewTextBoxColumn
            // 
            this.dobStrDataGridViewTextBoxColumn.DataPropertyName = "DobStr";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dobStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.dobStrDataGridViewTextBoxColumn.HeaderText = "Ngày sinh";
            this.dobStrDataGridViewTextBoxColumn.Name = "dobStrDataGridViewTextBoxColumn";
            this.dobStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.dobStrDataGridViewTextBoxColumn.Width = 90;
            // 
            // genderAsStrDataGridViewTextBoxColumn
            // 
            this.genderAsStrDataGridViewTextBoxColumn.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.genderAsStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.genderAsStrDataGridViewTextBoxColumn.HeaderText = "Giới tính";
            this.genderAsStrDataGridViewTextBoxColumn.Name = "genderAsStrDataGridViewTextBoxColumn";
            this.genderAsStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.genderAsStrDataGridViewTextBoxColumn.Width = 80;
            // 
            // IdentityCard
            // 
            this.IdentityCard.DataPropertyName = "IdentityCard";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IdentityCard.DefaultCellStyle = dataGridViewCellStyle9;
            this.IdentityCard.HeaderText = "CMND";
            this.IdentityCard.Name = "IdentityCard";
            this.IdentityCard.ReadOnly = true;
            this.IdentityCard.Width = 120;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSearchPatient);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(666, 38);
            this.panel2.TabIndex = 0;
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(93, 10);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(291, 20);
            this.txtSearchPatient.TabIndex = 0;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            this.txtSearchPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPatient_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tìm bệnh nhân:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 534);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 38);
            this.panel1.TabIndex = 4;
            // 
            // dlgMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(666, 572);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgMembers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them nhan vien";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgMembers_FormClosing);
            this.Load += new System.EventHandler(this.dlgMembers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            this.pService.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgService)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgMembers;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn genderAsStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdentityCard;
        private System.Windows.Forms.Panel pService;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtSearchService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox chkServiceChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgService;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn dataGridViewCheckBoxXColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}