namespace SonoOnlineResult.Dialogs
{
    partial class dlgBranchList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgBranchList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCheck = new System.Windows.Forms.CheckBox();
            this.dgBranch = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colBranchKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWebsite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 444);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(245, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(166, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(87, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkCheck);
            this.panel2.Controls.Add(this.dgBranch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 444);
            this.panel2.TabIndex = 1;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoSize = true;
            this.chkCheck.Location = new System.Drawing.Point(10, 4);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(15, 14);
            this.chkCheck.TabIndex = 2;
            this.chkCheck.UseVisualStyleBackColor = true;
            this.chkCheck.CheckedChanged += new System.EventHandler(this.chkCheck_CheckedChanged);
            // 
            // dgBranch
            // 
            this.dgBranch.AllowUserToAddRows = false;
            this.dgBranch.AllowUserToDeleteRows = false;
            this.dgBranch.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBranch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBranch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colBranchKey,
            this.colBranchName,
            this.colAddress,
            this.colTelephone,
            this.colFax,
            this.colWebsite,
            this.colNote});
            this.dgBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBranch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgBranch.Location = new System.Drawing.Point(0, 0);
            this.dgBranch.MultiSelect = false;
            this.dgBranch.Name = "dgBranch";
            this.dgBranch.RowHeadersVisible = false;
            this.dgBranch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBranch.Size = new System.Drawing.Size(831, 444);
            this.dgBranch.TabIndex = 0;
            this.dgBranch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBranch_CellDoubleClick);
            // 
            // colCheck
            // 
            this.colCheck.DataPropertyName = "Check";
            this.colCheck.Frozen = true;
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 30;
            // 
            // colBranchKey
            // 
            this.colBranchKey.DataPropertyName = "BranchKey";
            this.colBranchKey.HeaderText = "BranchKey";
            this.colBranchKey.Name = "colBranchKey";
            this.colBranchKey.ReadOnly = true;
            this.colBranchKey.Visible = false;
            // 
            // colBranchName
            // 
            this.colBranchName.DataPropertyName = "BranchName";
            this.colBranchName.HeaderText = "Branch Name";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.ReadOnly = true;
            this.colBranchName.Width = 150;
            // 
            // colAddress
            // 
            this.colAddress.DataPropertyName = "Address";
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 150;
            // 
            // colTelephone
            // 
            this.colTelephone.DataPropertyName = "Telephone";
            this.colTelephone.HeaderText = "Telephone";
            this.colTelephone.Name = "colTelephone";
            this.colTelephone.ReadOnly = true;
            this.colTelephone.Width = 80;
            // 
            // colFax
            // 
            this.colFax.DataPropertyName = "Fax";
            this.colFax.HeaderText = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.ReadOnly = true;
            this.colFax.Width = 80;
            // 
            // colWebsite
            // 
            this.colWebsite.DataPropertyName = "Website";
            this.colWebsite.HeaderText = "Website";
            this.colWebsite.Name = "colWebsite";
            this.colWebsite.ReadOnly = true;
            this.colWebsite.Width = 120;
            // 
            // colNote
            // 
            this.colNote.DataPropertyName = "Note";
            this.colNote.HeaderText = "Notes";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            this.colNote.Width = 200;
            // 
            // dlgBranchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(831, 480);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgBranchList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branch List";
            this.Load += new System.EventHandler(this.dlgBranchList_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgBranch;
        private System.Windows.Forms.CheckBox chkCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWebsite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
    }
}