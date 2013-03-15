namespace MM.Controls
{
    partial class uTinNhanMauList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uTinNhanMauList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgTinNhanMau = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tinNhanMauBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TieuDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noiDungDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsDuyet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnDuyet = new System.Windows.Forms.Button();
            this.btnBoDuyet = new System.Windows.Forms.Button();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.duyetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.boDuyetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTinNhanMau)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tinNhanMauBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBoDuyet);
            this.panel1.Controls.Add(this.btnDuyet);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 38);
            this.panel1.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(85, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "    &Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkChecked);
            this.panel2.Controls.Add(this.dgTinNhanMau);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(854, 430);
            this.panel2.TabIndex = 4;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 1;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgTinNhanMau
            // 
            this.dgTinNhanMau.AllowUserToAddRows = false;
            this.dgTinNhanMau.AllowUserToDeleteRows = false;
            this.dgTinNhanMau.AllowUserToOrderColumns = true;
            this.dgTinNhanMau.AutoGenerateColumns = false;
            this.dgTinNhanMau.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTinNhanMau.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTinNhanMau.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTinNhanMau.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.TieuDe,
            this.noiDungDataGridViewTextBoxColumn,
            this.IsDuyet});
            this.dgTinNhanMau.ContextMenuStrip = this.ctmAction;
            this.dgTinNhanMau.DataSource = this.tinNhanMauBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTinNhanMau.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgTinNhanMau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTinNhanMau.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTinNhanMau.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgTinNhanMau.HighlightSelectedColumnHeaders = false;
            this.dgTinNhanMau.Location = new System.Drawing.Point(0, 0);
            this.dgTinNhanMau.MultiSelect = false;
            this.dgTinNhanMau.Name = "dgTinNhanMau";
            this.dgTinNhanMau.RowHeadersWidth = 30;
            this.dgTinNhanMau.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTinNhanMau.Size = new System.Drawing.Size(854, 430);
            this.dgTinNhanMau.TabIndex = 0;
            this.dgTinNhanMau.DoubleClick += new System.EventHandler(this.dgTinNhanMau_DoubleClick);
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.duyetToolStripMenuItem,
            this.toolStripSeparator4,
            this.boDuyetToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(129, 138);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addToolStripMenuItem.Text = "Thêm";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::MM.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.editToolStripMenuItem.Text = "Sửa";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(125, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // tinNhanMauBindingSource
            // 
            this.tinNhanMauBindingSource.DataSource = typeof(MM.Databasae.TinNhanMau);
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // TieuDe
            // 
            this.TieuDe.DataPropertyName = "TieuDe";
            this.TieuDe.HeaderText = "Tiêu đề";
            this.TieuDe.Name = "TieuDe";
            this.TieuDe.ReadOnly = true;
            this.TieuDe.Width = 250;
            // 
            // noiDungDataGridViewTextBoxColumn
            // 
            this.noiDungDataGridViewTextBoxColumn.DataPropertyName = "NoiDung";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.noiDungDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.noiDungDataGridViewTextBoxColumn.HeaderText = "Nội dung tin nhắn mẫu";
            this.noiDungDataGridViewTextBoxColumn.Name = "noiDungDataGridViewTextBoxColumn";
            this.noiDungDataGridViewTextBoxColumn.ReadOnly = true;
            this.noiDungDataGridViewTextBoxColumn.Width = 450;
            // 
            // IsDuyet
            // 
            this.IsDuyet.DataPropertyName = "IsDuyet";
            this.IsDuyet.HeaderText = "Duyệt";
            this.IsDuyet.Name = "IsDuyet";
            this.IsDuyet.ReadOnly = true;
            this.IsDuyet.Width = 60;
            // 
            // btnDuyet
            // 
            this.btnDuyet.Image = global::MM.Properties.Resources.check;
            this.btnDuyet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDuyet.Location = new System.Drawing.Point(243, 6);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(75, 25);
            this.btnDuyet.TabIndex = 3;
            this.btnDuyet.Text = "    &Duyệt";
            this.btnDuyet.UseVisualStyleBackColor = true;
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // btnBoDuyet
            // 
            this.btnBoDuyet.Image = ((System.Drawing.Image)(resources.GetObject("btnBoDuyet.Image")));
            this.btnBoDuyet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBoDuyet.Location = new System.Drawing.Point(322, 6);
            this.btnBoDuyet.Name = "btnBoDuyet";
            this.btnBoDuyet.Size = new System.Drawing.Size(81, 25);
            this.btnBoDuyet.TabIndex = 4;
            this.btnBoDuyet.Text = "     &Bỏ duyệt";
            this.btnBoDuyet.UseVisualStyleBackColor = true;
            this.btnBoDuyet.Click += new System.EventHandler(this.btnBoDuyet_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(125, 6);
            // 
            // duyetToolStripMenuItem
            // 
            this.duyetToolStripMenuItem.Image = global::MM.Properties.Resources.check;
            this.duyetToolStripMenuItem.Name = "duyetToolStripMenuItem";
            this.duyetToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.duyetToolStripMenuItem.Text = "Duyệt";
            this.duyetToolStripMenuItem.Click += new System.EventHandler(this.duyetToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(125, 6);
            // 
            // boDuyetToolStripMenuItem
            // 
            this.boDuyetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("boDuyetToolStripMenuItem.Image")));
            this.boDuyetToolStripMenuItem.Name = "boDuyetToolStripMenuItem";
            this.boDuyetToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.boDuyetToolStripMenuItem.Text = "Bỏ duyệt";
            this.boDuyetToolStripMenuItem.Click += new System.EventHandler(this.boDuyetToolStripMenuItem_Click);
            // 
            // uTinNhanMauList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uTinNhanMauList";
            this.Size = new System.Drawing.Size(854, 468);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTinNhanMau)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tinNhanMauBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgTinNhanMau;
        private System.Windows.Forms.BindingSource tinNhanMauBindingSource;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn TieuDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn noiDungDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDuyet;
        private System.Windows.Forms.Button btnBoDuyet;
        private System.Windows.Forms.Button btnDuyet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem duyetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem boDuyetToolStripMenuItem;
    }
}
