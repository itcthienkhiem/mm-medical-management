namespace MM.Controls
{
    partial class uDanhSachXetNghiem_CellDyn3200List
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtXetNghiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgXetNghiem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.xetNghiemCellDyn3200BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numToValue_NormalPercent = new System.Windows.Forms.NumericUpDown();
            this.chkToValue_NormalPercent = new System.Windows.Forms.CheckBox();
            this.numFromValue_NormalPercent = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue_NormalPercent = new System.Windows.Forms.CheckBox();
            this.numToValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkToValue_Normal = new System.Windows.Forms.CheckBox();
            this.numFromValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue_Normal = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemCellDyn3200BindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_NormalPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_NormalPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtXetNghiem);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 37);
            this.panel2.TabIndex = 2;
            // 
            // txtXetNghiem
            // 
            this.txtXetNghiem.Location = new System.Drawing.Point(89, 8);
            this.txtXetNghiem.Name = "txtXetNghiem";
            this.txtXetNghiem.Size = new System.Drawing.Size(202, 20);
            this.txtXetNghiem.TabIndex = 3;
            this.txtXetNghiem.TextChanged += new System.EventHandler(this.txtXetNghiem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tìm xét nghiệm:";
            // 
            // dgXetNghiem
            // 
            this.dgXetNghiem.AllowUserToAddRows = false;
            this.dgXetNghiem.AllowUserToDeleteRows = false;
            this.dgXetNghiem.AllowUserToOrderColumns = true;
            this.dgXetNghiem.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgXetNghiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgXetNghiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgXetNghiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullNameDataGridViewTextBoxColumn});
            this.dgXetNghiem.DataSource = this.xetNghiemCellDyn3200BindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgXetNghiem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgXetNghiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgXetNghiem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgXetNghiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgXetNghiem.HighlightSelectedColumnHeaders = false;
            this.dgXetNghiem.Location = new System.Drawing.Point(0, 0);
            this.dgXetNghiem.MultiSelect = false;
            this.dgXetNghiem.Name = "dgXetNghiem";
            this.dgXetNghiem.ReadOnly = true;
            this.dgXetNghiem.RowHeadersWidth = 30;
            this.dgXetNghiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgXetNghiem.Size = new System.Drawing.Size(301, 307);
            this.dgXetNghiem.TabIndex = 4;
            this.dgXetNghiem.SelectionChanged += new System.EventHandler(this.dgXetNghiem_SelectionChanged);
            // 
            // xetNghiemCellDyn3200BindingSource
            // 
            this.xetNghiemCellDyn3200BindingSource.DataSource = typeof(MM.Databasae.XetNghiem_CellDyn3200);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(301, 344);
            this.panel4.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgXetNghiem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 37);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(301, 307);
            this.panel5.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(301, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(514, 344);
            this.panel3.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(514, 20);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(514, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thông số chỉ số xét nghiệm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numToValue_NormalPercent);
            this.groupBox1.Controls.Add(this.chkToValue_NormalPercent);
            this.groupBox1.Controls.Add(this.numFromValue_NormalPercent);
            this.groupBox1.Controls.Add(this.chkFromValue_NormalPercent);
            this.groupBox1.Controls.Add(this.numToValue_Normal);
            this.groupBox1.Controls.Add(this.chkToValue_Normal);
            this.groupBox1.Controls.Add(this.numFromValue_Normal);
            this.groupBox1.Controls.Add(this.chkFromValue_Normal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 74);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // numToValue_NormalPercent
            // 
            this.numToValue_NormalPercent.DecimalPlaces = 2;
            this.numToValue_NormalPercent.Enabled = false;
            this.numToValue_NormalPercent.Location = new System.Drawing.Point(284, 43);
            this.numToValue_NormalPercent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue_NormalPercent.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue_NormalPercent.Name = "numToValue_NormalPercent";
            this.numToValue_NormalPercent.Size = new System.Drawing.Size(74, 20);
            this.numToValue_NormalPercent.TabIndex = 11;
            // 
            // chkToValue_NormalPercent
            // 
            this.chkToValue_NormalPercent.AutoSize = true;
            this.chkToValue_NormalPercent.Location = new System.Drawing.Point(241, 45);
            this.chkToValue_NormalPercent.Name = "chkToValue_NormalPercent";
            this.chkToValue_NormalPercent.Size = new System.Drawing.Size(45, 17);
            this.chkToValue_NormalPercent.TabIndex = 10;
            this.chkToValue_NormalPercent.Text = "đến";
            this.chkToValue_NormalPercent.UseVisualStyleBackColor = true;
            this.chkToValue_NormalPercent.CheckedChanged += new System.EventHandler(this.chkToValue_NormalPercent_CheckedChanged);
            // 
            // numFromValue_NormalPercent
            // 
            this.numFromValue_NormalPercent.DecimalPlaces = 2;
            this.numFromValue_NormalPercent.Enabled = false;
            this.numFromValue_NormalPercent.Location = new System.Drawing.Point(161, 43);
            this.numFromValue_NormalPercent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue_NormalPercent.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue_NormalPercent.Name = "numFromValue_NormalPercent";
            this.numFromValue_NormalPercent.Size = new System.Drawing.Size(74, 20);
            this.numFromValue_NormalPercent.TabIndex = 9;
            // 
            // chkFromValue_NormalPercent
            // 
            this.chkFromValue_NormalPercent.AutoSize = true;
            this.chkFromValue_NormalPercent.Location = new System.Drawing.Point(94, 45);
            this.chkFromValue_NormalPercent.Name = "chkFromValue_NormalPercent";
            this.chkFromValue_NormalPercent.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue_NormalPercent.TabIndex = 8;
            this.chkFromValue_NormalPercent.Text = "Chỉ số từ:";
            this.chkFromValue_NormalPercent.UseVisualStyleBackColor = true;
            this.chkFromValue_NormalPercent.CheckedChanged += new System.EventHandler(this.chkFromValue_NormalPercent_CheckedChanged);
            // 
            // numToValue_Normal
            // 
            this.numToValue_Normal.DecimalPlaces = 2;
            this.numToValue_Normal.Enabled = false;
            this.numToValue_Normal.Location = new System.Drawing.Point(284, 18);
            this.numToValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue_Normal.Name = "numToValue_Normal";
            this.numToValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numToValue_Normal.TabIndex = 7;
            // 
            // chkToValue_Normal
            // 
            this.chkToValue_Normal.AutoSize = true;
            this.chkToValue_Normal.Location = new System.Drawing.Point(241, 20);
            this.chkToValue_Normal.Name = "chkToValue_Normal";
            this.chkToValue_Normal.Size = new System.Drawing.Size(45, 17);
            this.chkToValue_Normal.TabIndex = 6;
            this.chkToValue_Normal.Text = "đến";
            this.chkToValue_Normal.UseVisualStyleBackColor = true;
            this.chkToValue_Normal.CheckedChanged += new System.EventHandler(this.chkToValue_Normal_CheckedChanged);
            // 
            // numFromValue_Normal
            // 
            this.numFromValue_Normal.DecimalPlaces = 2;
            this.numFromValue_Normal.Enabled = false;
            this.numFromValue_Normal.Location = new System.Drawing.Point(161, 18);
            this.numFromValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue_Normal.Name = "numFromValue_Normal";
            this.numFromValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numFromValue_Normal.TabIndex = 5;
            // 
            // chkFromValue_Normal
            // 
            this.chkFromValue_Normal.AutoSize = true;
            this.chkFromValue_Normal.Location = new System.Drawing.Point(94, 20);
            this.chkFromValue_Normal.Name = "chkFromValue_Normal";
            this.chkFromValue_Normal.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue_Normal.TabIndex = 4;
            this.chkFromValue_Normal.Text = "Chỉ số từ:";
            this.chkFromValue_Normal.UseVisualStyleBackColor = true;
            this.chkFromValue_Normal.CheckedChanged += new System.EventHandler(this.chkFromValue_Normal_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "% Bình thường:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Bình thường:";
            // 
            // btnOK
            // 
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(8, 101);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FullName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tên xét nghiệm";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Tên xét nghiệm";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // uDanhSachXetNghiem_CellDyn3200List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Name = "uDanhSachXetNghiem_CellDyn3200List";
            this.Size = new System.Drawing.Size(815, 344);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemCellDyn3200BindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_NormalPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_NormalPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtXetNghiem;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgXetNghiem;
        private System.Windows.Forms.BindingSource xetNghiemCellDyn3200BindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numToValue_NormalPercent;
        private System.Windows.Forms.CheckBox chkToValue_NormalPercent;
        private System.Windows.Forms.NumericUpDown numFromValue_NormalPercent;
        private System.Windows.Forms.CheckBox chkFromValue_NormalPercent;
        private System.Windows.Forms.NumericUpDown numToValue_Normal;
        private System.Windows.Forms.CheckBox chkToValue_Normal;
        private System.Windows.Forms.NumericUpDown numFromValue_Normal;
        private System.Windows.Forms.CheckBox chkFromValue_Normal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}
