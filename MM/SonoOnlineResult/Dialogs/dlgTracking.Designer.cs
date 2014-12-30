namespace SonoOnlineResult.Dialogs
{
    partial class dlgTracking
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTracking));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboUserLogon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTo = new System.Windows.Forms.DateTimePicker();
            this.dtpkFrom = new System.Windows.Forms.DateTimePicker();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.chkFrom = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgTracking = new System.Windows.Forms.DataGridView();
            this.colTrackingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCcEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cboUserLogon);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpkTo);
            this.panel1.Controls.Add(this.dtpkFrom);
            this.panel1.Controls.Add(this.chkTo);
            this.panel1.Controls.Add(this.chkFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 72);
            this.panel1.TabIndex = 0;
            // 
            // cboUserLogon
            // 
            this.cboUserLogon.DisplayMember = "Username";
            this.cboUserLogon.FormattingEnabled = true;
            this.cboUserLogon.Location = new System.Drawing.Point(73, 38);
            this.cboUserLogon.Name = "cboUserLogon";
            this.cboUserLogon.Size = new System.Drawing.Size(342, 21);
            this.cboUserLogon.TabIndex = 5;
            this.cboUserLogon.ValueMember = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username:";
            // 
            // dtpkTo
            // 
            this.dtpkTo.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtpkTo.Enabled = false;
            this.dtpkTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTo.Location = new System.Drawing.Point(263, 11);
            this.dtpkTo.Name = "dtpkTo";
            this.dtpkTo.Size = new System.Drawing.Size(152, 20);
            this.dtpkTo.TabIndex = 3;
            // 
            // dtpkFrom
            // 
            this.dtpkFrom.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtpkFrom.Enabled = false;
            this.dtpkFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFrom.Location = new System.Drawing.Point(58, 11);
            this.dtpkFrom.Name = "dtpkFrom";
            this.dtpkFrom.Size = new System.Drawing.Size(152, 20);
            this.dtpkFrom.TabIndex = 1;
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Location = new System.Drawing.Point(223, 13);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(39, 17);
            this.chkTo.TabIndex = 2;
            this.chkTo.Text = "To";
            this.chkTo.UseVisualStyleBackColor = true;
            // 
            // chkFrom
            // 
            this.chkFrom.AutoSize = true;
            this.chkFrom.Location = new System.Drawing.Point(11, 13);
            this.chkFrom.Name = "chkFrom";
            this.chkFrom.Size = new System.Drawing.Size(49, 17);
            this.chkFrom.TabIndex = 0;
            this.chkFrom.Text = "From";
            this.chkFrom.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnExportToExcel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(759, 35);
            this.panel2.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(106, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(7, 6);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(95, 23);
            this.btnExportToExcel.TabIndex = 0;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgTracking);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 404);
            this.panel3.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(422, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgTracking
            // 
            this.dgTracking.AllowUserToAddRows = false;
            this.dgTracking.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTracking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTracking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrackingDate,
            this.colUsername,
            this.colBranch,
            this.colToEmail,
            this.colCcEmail,
            this.colNotes});
            this.dgTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTracking.Location = new System.Drawing.Point(0, 0);
            this.dgTracking.Name = "dgTracking";
            this.dgTracking.ReadOnly = true;
            this.dgTracking.RowHeadersVisible = false;
            this.dgTracking.Size = new System.Drawing.Size(759, 404);
            this.dgTracking.TabIndex = 0;
            // 
            // colTrackingDate
            // 
            this.colTrackingDate.HeaderText = "Tracking Date";
            this.colTrackingDate.Name = "colTrackingDate";
            this.colTrackingDate.ReadOnly = true;
            // 
            // colUsername
            // 
            this.colUsername.HeaderText = "Username";
            this.colUsername.Name = "colUsername";
            this.colUsername.ReadOnly = true;
            // 
            // colBranch
            // 
            this.colBranch.HeaderText = "Branch";
            this.colBranch.Name = "colBranch";
            this.colBranch.ReadOnly = true;
            // 
            // colToEmail
            // 
            this.colToEmail.HeaderText = "To Email";
            this.colToEmail.Name = "colToEmail";
            this.colToEmail.ReadOnly = true;
            // 
            // colCcEmail
            // 
            this.colCcEmail.HeaderText = "Cc Email";
            this.colCcEmail.Name = "colCcEmail";
            this.colCcEmail.ReadOnly = true;
            // 
            // colNotes
            // 
            this.colNotes.HeaderText = "Notes";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // dlgTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(759, 511);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tracking";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.ComboBox cboUserLogon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTo;
        private System.Windows.Forms.DateTimePicker dtpkFrom;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.CheckBox chkFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgTracking;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrackingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCcEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
    }
}