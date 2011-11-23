namespace MM.Controls
{
    partial class uPrintLabel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uPrintLabel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgMembers = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderAsStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdentityCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this._printDocument = new System.Drawing.Printing.PrintDocument();
            this._pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 38);
            this.panel1.TabIndex = 4;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(8, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numCount);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtSearchPatient);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(723, 60);
            this.panel2.TabIndex = 5;
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(93, 9);
            this.numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(66, 20);
            this.numCount.TabIndex = 6;
            this.numCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Số lượng:";
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(93, 33);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(291, 20);
            this.txtSearchPatient.TabIndex = 3;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tìm bệnh nhân:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgMembers);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(723, 317);
            this.panel3.TabIndex = 6;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 11;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgMembers
            // 
            this.dgMembers.AllowUserToAddRows = false;
            this.dgMembers.AllowUserToDeleteRows = false;
            this.dgMembers.AllowUserToOrderColumns = true;
            this.dgMembers.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.fileNumDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.dobStrDataGridViewTextBoxColumn,
            this.genderAsStrDataGridViewTextBoxColumn,
            this.IdentityCard});
            this.dgMembers.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMembers.DefaultCellStyle = dataGridViewCellStyle6;
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
            this.dgMembers.Size = new System.Drawing.Size(723, 317);
            this.dgMembers.TabIndex = 10;
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dobStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.dobStrDataGridViewTextBoxColumn.HeaderText = "Ngày sinh";
            this.dobStrDataGridViewTextBoxColumn.Name = "dobStrDataGridViewTextBoxColumn";
            this.dobStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.dobStrDataGridViewTextBoxColumn.Width = 90;
            // 
            // genderAsStrDataGridViewTextBoxColumn
            // 
            this.genderAsStrDataGridViewTextBoxColumn.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.genderAsStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.genderAsStrDataGridViewTextBoxColumn.HeaderText = "Giới tính";
            this.genderAsStrDataGridViewTextBoxColumn.Name = "genderAsStrDataGridViewTextBoxColumn";
            this.genderAsStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.genderAsStrDataGridViewTextBoxColumn.Width = 80;
            // 
            // IdentityCard
            // 
            this.IdentityCard.DataPropertyName = "IdentityCard";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IdentityCard.DefaultCellStyle = dataGridViewCellStyle5;
            this.IdentityCard.HeaderText = "CMND";
            this.IdentityCard.Name = "IdentityCard";
            this.IdentityCard.ReadOnly = true;
            this.IdentityCard.Width = 120;
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // _printPreviewDialog
            // 
            this._printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this._printPreviewDialog.Document = this._printDocument;
            this._printPreviewDialog.Enabled = true;
            this._printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDialog.Icon")));
            this._printPreviewDialog.Name = "_printPreviewDialog";
            this._printPreviewDialog.ShowIcon = false;
            this._printPreviewDialog.Visible = false;
            // 
            // _printDocument
            // 
            this._printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this._printDocument_PrintPage);
            // 
            // _pageSetupDialog
            // 
            this._pageSetupDialog.Document = this._printDocument;
            // 
            // uPrintLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uPrintLabel";
            this.Size = new System.Drawing.Size(723, 415);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgMembers;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn genderAsStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdentityCard;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.PrintPreviewDialog _printPreviewDialog;
        private System.Drawing.Printing.PrintDocument _printDocument;
        private System.Windows.Forms.PageSetupDialog _pageSetupDialog;
    }
}
