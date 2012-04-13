namespace MM.Dialogs
{
    partial class dlgEditBooking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgEditBooking));
            this.gbBookingMonitor = new System.Windows.Forms.GroupBox();
            this.cboBookingMonitorCompany = new System.Windows.Forms.ComboBox();
            this.numEvening = new System.Windows.Forms.NumericUpDown();
            this.numAfternoon = new System.Windows.Forms.NumericUpDown();
            this.numMorning = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpkBookingMonitorDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gbBloodTaking = new System.Windows.Forms.GroupBox();
            this.cboBloodTakingCompany = new System.Windows.Forms.ComboBox();
            this.numPax = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpkBloodTakingDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboBookingMonitorInOut = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboBloodTakingInOut = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gbBookingMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAfternoon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMorning)).BeginInit();
            this.gbBloodTaking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPax)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBookingMonitor
            // 
            this.gbBookingMonitor.Controls.Add(this.cboBookingMonitorInOut);
            this.gbBookingMonitor.Controls.Add(this.label9);
            this.gbBookingMonitor.Controls.Add(this.cboBookingMonitorCompany);
            this.gbBookingMonitor.Controls.Add(this.numEvening);
            this.gbBookingMonitor.Controls.Add(this.numAfternoon);
            this.gbBookingMonitor.Controls.Add(this.numMorning);
            this.gbBookingMonitor.Controls.Add(this.label5);
            this.gbBookingMonitor.Controls.Add(this.label4);
            this.gbBookingMonitor.Controls.Add(this.label3);
            this.gbBookingMonitor.Controls.Add(this.label2);
            this.gbBookingMonitor.Controls.Add(this.dtpkBookingMonitorDate);
            this.gbBookingMonitor.Controls.Add(this.label1);
            this.gbBookingMonitor.Location = new System.Drawing.Point(6, 2);
            this.gbBookingMonitor.Name = "gbBookingMonitor";
            this.gbBookingMonitor.Size = new System.Drawing.Size(326, 173);
            this.gbBookingMonitor.TabIndex = 2;
            this.gbBookingMonitor.TabStop = false;
            // 
            // cboBookingMonitorCompany
            // 
            this.cboBookingMonitorCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBookingMonitorCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBookingMonitorCompany.FormattingEnabled = true;
            this.cboBookingMonitorCompany.Location = new System.Drawing.Point(70, 44);
            this.cboBookingMonitorCompany.Name = "cboBookingMonitorCompany";
            this.cboBookingMonitorCompany.Size = new System.Drawing.Size(240, 21);
            this.cboBookingMonitorCompany.TabIndex = 10;
            // 
            // numEvening
            // 
            this.numEvening.Location = new System.Drawing.Point(70, 114);
            this.numEvening.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numEvening.Name = "numEvening";
            this.numEvening.Size = new System.Drawing.Size(78, 20);
            this.numEvening.TabIndex = 9;
            // 
            // numAfternoon
            // 
            this.numAfternoon.Location = new System.Drawing.Point(70, 91);
            this.numAfternoon.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numAfternoon.Name = "numAfternoon";
            this.numAfternoon.Size = new System.Drawing.Size(78, 20);
            this.numAfternoon.TabIndex = 8;
            // 
            // numMorning
            // 
            this.numMorning.Location = new System.Drawing.Point(70, 68);
            this.numMorning.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numMorning.Name = "numMorning";
            this.numMorning.Size = new System.Drawing.Size(78, 20);
            this.numMorning.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Evening:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Afternoon:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Morning:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Company:";
            // 
            // dtpkBookingMonitorDate
            // 
            this.dtpkBookingMonitorDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkBookingMonitorDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkBookingMonitorDate.Location = new System.Drawing.Point(70, 21);
            this.dtpkBookingMonitorDate.Name = "dtpkBookingMonitorDate";
            this.dtpkBookingMonitorDate.Size = new System.Drawing.Size(99, 20);
            this.dtpkBookingMonitorDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date:";
            // 
            // gbBloodTaking
            // 
            this.gbBloodTaking.Controls.Add(this.cboBloodTakingInOut);
            this.gbBloodTaking.Controls.Add(this.label10);
            this.gbBloodTaking.Controls.Add(this.cboBloodTakingCompany);
            this.gbBloodTaking.Controls.Add(this.numPax);
            this.gbBloodTaking.Controls.Add(this.label6);
            this.gbBloodTaking.Controls.Add(this.label7);
            this.gbBloodTaking.Controls.Add(this.dtpkBloodTakingDate);
            this.gbBloodTaking.Controls.Add(this.label8);
            this.gbBloodTaking.Location = new System.Drawing.Point(6, 2);
            this.gbBloodTaking.Name = "gbBloodTaking";
            this.gbBloodTaking.Size = new System.Drawing.Size(326, 129);
            this.gbBloodTaking.TabIndex = 4;
            this.gbBloodTaking.TabStop = false;
            // 
            // cboBloodTakingCompany
            // 
            this.cboBloodTakingCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBloodTakingCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBloodTakingCompany.FormattingEnabled = true;
            this.cboBloodTakingCompany.Location = new System.Drawing.Point(70, 46);
            this.cboBloodTakingCompany.Name = "cboBloodTakingCompany";
            this.cboBloodTakingCompany.Size = new System.Drawing.Size(240, 21);
            this.cboBloodTakingCompany.TabIndex = 14;
            // 
            // numPax
            // 
            this.numPax.Location = new System.Drawing.Point(70, 70);
            this.numPax.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numPax.Name = "numPax";
            this.numPax.Size = new System.Drawing.Size(78, 20);
            this.numPax.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Pax:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Company:";
            // 
            // dtpkBloodTakingDate
            // 
            this.dtpkBloodTakingDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpkBloodTakingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkBloodTakingDate.Location = new System.Drawing.Point(70, 23);
            this.dtpkBloodTakingDate.Name = "dtpkBloodTakingDate";
            this.dtpkBloodTakingDate.Size = new System.Drawing.Size(146, 20);
            this.dtpkBloodTakingDate.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Date:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(171, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(92, 181);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // cboBookingMonitorInOut
            // 
            this.cboBookingMonitorInOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBookingMonitorInOut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBookingMonitorInOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBookingMonitorInOut.FormattingEnabled = true;
            this.cboBookingMonitorInOut.Items.AddRange(new object[] {
            "IN",
            "OUT",
            "IN AND OUT"});
            this.cboBookingMonitorInOut.Location = new System.Drawing.Point(70, 138);
            this.cboBookingMonitorInOut.Name = "cboBookingMonitorInOut";
            this.cboBookingMonitorInOut.Size = new System.Drawing.Size(99, 21);
            this.cboBookingMonitorInOut.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "In/Out:";
            // 
            // cboBloodTakingInOut
            // 
            this.cboBloodTakingInOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBloodTakingInOut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBloodTakingInOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBloodTakingInOut.FormattingEnabled = true;
            this.cboBloodTakingInOut.Items.AddRange(new object[] {
            "IN",
            "OUT",
            "IN AND OUT"});
            this.cboBloodTakingInOut.Location = new System.Drawing.Point(70, 94);
            this.cboBloodTakingInOut.Name = "cboBloodTakingInOut";
            this.cboBloodTakingInOut.Size = new System.Drawing.Size(99, 21);
            this.cboBloodTakingInOut.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "In/Out:";
            // 
            // dlgEditBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 211);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbBookingMonitor);
            this.Controls.Add(this.gbBloodTaking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgEditBooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua lich hen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgEditBooking_FormClosing);
            this.Load += new System.EventHandler(this.dlgEditBooking_Load);
            this.gbBookingMonitor.ResumeLayout(false);
            this.gbBookingMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAfternoon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMorning)).EndInit();
            this.gbBloodTaking.ResumeLayout(false);
            this.gbBloodTaking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBookingMonitor;
        private System.Windows.Forms.ComboBox cboBookingMonitorCompany;
        private System.Windows.Forms.NumericUpDown numEvening;
        private System.Windows.Forms.NumericUpDown numAfternoon;
        private System.Windows.Forms.NumericUpDown numMorning;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpkBookingMonitorDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbBloodTaking;
        private System.Windows.Forms.ComboBox cboBloodTakingCompany;
        private System.Windows.Forms.NumericUpDown numPax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpkBloodTakingDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboBookingMonitorInOut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboBloodTakingInOut;
        private System.Windows.Forms.Label label10;
    }
}