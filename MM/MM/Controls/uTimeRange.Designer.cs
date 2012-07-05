namespace MM.Controls
{
    partial class uTimeRange
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
            this.numToValue = new System.Windows.Forms.NumericUpDown();
            this.chkToValue = new System.Windows.Forms.CheckBox();
            this.numFromValue = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue = new System.Windows.Forms.CheckBox();
            this.cboFromOperator = new System.Windows.Forms.ComboBox();
            this.cboToOperator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).BeginInit();
            this.SuspendLayout();
            // 
            // numToValue
            // 
            this.numToValue.Enabled = false;
            this.numToValue.Location = new System.Drawing.Point(229, 2);
            this.numToValue.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numToValue.Name = "numToValue";
            this.numToValue.Size = new System.Drawing.Size(45, 20);
            this.numToValue.TabIndex = 10;
            this.numToValue.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            // 
            // chkToValue
            // 
            this.chkToValue.AutoSize = true;
            this.chkToValue.Location = new System.Drawing.Point(278, 5);
            this.chkToValue.Name = "chkToValue";
            this.chkToValue.Size = new System.Drawing.Size(15, 14);
            this.chkToValue.TabIndex = 11;
            this.chkToValue.UseVisualStyleBackColor = true;
            this.chkToValue.CheckedChanged += new System.EventHandler(this.chkToValue_CheckedChanged);
            // 
            // numFromValue
            // 
            this.numFromValue.Enabled = false;
            this.numFromValue.Location = new System.Drawing.Point(18, 2);
            this.numFromValue.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numFromValue.Name = "numFromValue";
            this.numFromValue.Size = new System.Drawing.Size(45, 20);
            this.numFromValue.TabIndex = 7;
            // 
            // chkFromValue
            // 
            this.chkFromValue.AutoSize = true;
            this.chkFromValue.Location = new System.Drawing.Point(0, 5);
            this.chkFromValue.Name = "chkFromValue";
            this.chkFromValue.Size = new System.Drawing.Size(15, 14);
            this.chkFromValue.TabIndex = 6;
            this.chkFromValue.UseVisualStyleBackColor = true;
            this.chkFromValue.CheckedChanged += new System.EventHandler(this.chkFromValue_CheckedChanged);
            // 
            // cboFromOperator
            // 
            this.cboFromOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFromOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFromOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromOperator.Enabled = false;
            this.cboFromOperator.FormattingEnabled = true;
            this.cboFromOperator.ItemHeight = 13;
            this.cboFromOperator.Items.AddRange(new object[] {
            "<",
            "<="});
            this.cboFromOperator.Location = new System.Drawing.Point(65, 1);
            this.cboFromOperator.Name = "cboFromOperator";
            this.cboFromOperator.Size = new System.Drawing.Size(48, 21);
            this.cboFromOperator.TabIndex = 8;
            // 
            // cboToOperator
            // 
            this.cboToOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboToOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboToOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToOperator.Enabled = false;
            this.cboToOperator.FormattingEnabled = true;
            this.cboToOperator.Items.AddRange(new object[] {
            "<",
            "<="});
            this.cboToOperator.Location = new System.Drawing.Point(179, 1);
            this.cboToOperator.Name = "cboToOperator";
            this.cboToOperator.Size = new System.Drawing.Size(48, 21);
            this.cboToOperator.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Thời gian";
            // 
            // uNormal_Chung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboToOperator);
            this.Controls.Add(this.cboFromOperator);
            this.Controls.Add(this.numToValue);
            this.Controls.Add(this.chkToValue);
            this.Controls.Add(this.numFromValue);
            this.Controls.Add(this.chkFromValue);
            this.Name = "uNormal_Chung";
            this.Size = new System.Drawing.Size(294, 24);
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numToValue;
        private System.Windows.Forms.CheckBox chkToValue;
        private System.Windows.Forms.NumericUpDown numFromValue;
        private System.Windows.Forms.CheckBox chkFromValue;
        private System.Windows.Forms.ComboBox cboFromOperator;
        private System.Windows.Forms.ComboBox cboToOperator;
        private System.Windows.Forms.Label label1;
    }
}
