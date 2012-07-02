namespace MM.Controls
{
    partial class uNormal_Sang_Chieu
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
            this.numToTime_Sang = new System.Windows.Forms.NumericUpDown();
            this.numFromTime_Sang = new System.Windows.Forms.NumericUpDown();
            this.chkSang = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numToTime_Chieu = new System.Windows.Forms.NumericUpDown();
            this.numFromTime_Chieu = new System.Windows.Forms.NumericUpDown();
            this.chkChieu = new System.Windows.Forms.CheckBox();
            this.uNormal_Sang = new MM.Controls.uNormal_Chung();
            this.uNormal_Chieu = new MM.Controls.uNormal_Chung();
            ((System.ComponentModel.ISupportInitialize)(this.numToTime_Sang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromTime_Sang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToTime_Chieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromTime_Chieu)).BeginInit();
            this.SuspendLayout();
            // 
            // numToTime_Sang
            // 
            this.numToTime_Sang.Enabled = false;
            this.numToTime_Sang.Location = new System.Drawing.Point(167, 0);
            this.numToTime_Sang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numToTime_Sang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToTime_Sang.Name = "numToTime_Sang";
            this.numToTime_Sang.Size = new System.Drawing.Size(45, 20);
            this.numToTime_Sang.TabIndex = 16;
            this.numToTime_Sang.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numFromTime_Sang
            // 
            this.numFromTime_Sang.Enabled = false;
            this.numFromTime_Sang.Location = new System.Drawing.Point(82, 0);
            this.numFromTime_Sang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numFromTime_Sang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromTime_Sang.Name = "numFromTime_Sang";
            this.numFromTime_Sang.Size = new System.Drawing.Size(45, 20);
            this.numFromTime_Sang.TabIndex = 14;
            this.numFromTime_Sang.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkSang
            // 
            this.chkSang.AutoSize = true;
            this.chkSang.Location = new System.Drawing.Point(0, 1);
            this.chkSang.Name = "chkSang";
            this.chkSang.Size = new System.Drawing.Size(51, 17);
            this.chkSang.TabIndex = 12;
            this.chkSang.Text = "Sáng";
            this.chkSang.UseVisualStyleBackColor = true;
            this.chkSang.CheckedChanged += new System.EventHandler(this.chkSang_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Từ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Đến:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Đến:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Từ:";
            // 
            // numToTime_Chieu
            // 
            this.numToTime_Chieu.Enabled = false;
            this.numToTime_Chieu.Location = new System.Drawing.Point(167, 59);
            this.numToTime_Chieu.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numToTime_Chieu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToTime_Chieu.Name = "numToTime_Chieu";
            this.numToTime_Chieu.Size = new System.Drawing.Size(45, 20);
            this.numToTime_Chieu.TabIndex = 27;
            this.numToTime_Chieu.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // numFromTime_Chieu
            // 
            this.numFromTime_Chieu.Enabled = false;
            this.numFromTime_Chieu.Location = new System.Drawing.Point(82, 59);
            this.numFromTime_Chieu.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numFromTime_Chieu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromTime_Chieu.Name = "numFromTime_Chieu";
            this.numFromTime_Chieu.Size = new System.Drawing.Size(45, 20);
            this.numFromTime_Chieu.TabIndex = 26;
            this.numFromTime_Chieu.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // chkChieu
            // 
            this.chkChieu.AutoSize = true;
            this.chkChieu.Location = new System.Drawing.Point(0, 60);
            this.chkChieu.Name = "chkChieu";
            this.chkChieu.Size = new System.Drawing.Size(53, 17);
            this.chkChieu.TabIndex = 25;
            this.chkChieu.Text = "Chiều";
            this.chkChieu.UseVisualStyleBackColor = true;
            this.chkChieu.CheckedChanged += new System.EventHandler(this.chkChieu_CheckedChanged);
            // 
            // uNormal_Sang
            // 
            this.uNormal_Sang.DonVi = "";
            this.uNormal_Sang.Enabled = false;
            this.uNormal_Sang.FromOperator = "<=";
            this.uNormal_Sang.FromValue = 0F;
            this.uNormal_Sang.FromValueChecked = false;
            this.uNormal_Sang.Location = new System.Drawing.Point(64, 24);
            this.uNormal_Sang.Name = "uNormal_Sang";
            this.uNormal_Sang.Size = new System.Drawing.Size(452, 24);
            this.uNormal_Sang.TabIndex = 24;
            this.uNormal_Sang.ToOperator = "<=";
            this.uNormal_Sang.ToValue = 0F;
            this.uNormal_Sang.ToValueChecked = false;
            // 
            // uNormal_Chieu
            // 
            this.uNormal_Chieu.DonVi = "";
            this.uNormal_Chieu.Enabled = false;
            this.uNormal_Chieu.FromOperator = "<=";
            this.uNormal_Chieu.FromValue = 0F;
            this.uNormal_Chieu.FromValueChecked = false;
            this.uNormal_Chieu.Location = new System.Drawing.Point(64, 84);
            this.uNormal_Chieu.Name = "uNormal_Chieu";
            this.uNormal_Chieu.Size = new System.Drawing.Size(452, 24);
            this.uNormal_Chieu.TabIndex = 28;
            this.uNormal_Chieu.ToOperator = "<=";
            this.uNormal_Chieu.ToValue = 0F;
            this.uNormal_Chieu.ToValueChecked = false;
            // 
            // uNormal_Sang_Chieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uNormal_Chieu);
            this.Controls.Add(this.uNormal_Sang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numToTime_Chieu);
            this.Controls.Add(this.numFromTime_Chieu);
            this.Controls.Add(this.chkChieu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numToTime_Sang);
            this.Controls.Add(this.numFromTime_Sang);
            this.Controls.Add(this.chkSang);
            this.Name = "uNormal_Sang_Chieu";
            this.Size = new System.Drawing.Size(515, 109);
            ((System.ComponentModel.ISupportInitialize)(this.numToTime_Sang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromTime_Sang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToTime_Chieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromTime_Chieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numToTime_Sang;
        private System.Windows.Forms.NumericUpDown numFromTime_Sang;
        private System.Windows.Forms.CheckBox chkSang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numToTime_Chieu;
        private System.Windows.Forms.NumericUpDown numFromTime_Chieu;
        private System.Windows.Forms.CheckBox chkChieu;
        private uNormal_Chung uNormal_Sang;
        private uNormal_Chung uNormal_Chieu;
    }
}
