namespace MM.Dialogs
{
    partial class dlgAddBooking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddBooking));
            this.gbBookingMonitor = new System.Windows.Forms.GroupBox();
            this.numEvening = new System.Windows.Forms.NumericUpDown();
            this.numAfternoon = new System.Windows.Forms.NumericUpDown();
            this.numMorning = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBookingMonitorCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpkBookingMonitorDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBookingMonitor = new System.Windows.Forms.CheckBox();
            this.chkBloodTaking = new System.Windows.Forms.CheckBox();
            this.gbBloodTaking = new System.Windows.Forms.GroupBox();
            this.numPax = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBloodTakingCompany = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpkBloodTakingDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
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
            this.gbBookingMonitor.Controls.Add(this.numEvening);
            this.gbBookingMonitor.Controls.Add(this.numAfternoon);
            this.gbBookingMonitor.Controls.Add(this.numMorning);
            this.gbBookingMonitor.Controls.Add(this.label5);
            this.gbBookingMonitor.Controls.Add(this.label4);
            this.gbBookingMonitor.Controls.Add(this.label3);
            this.gbBookingMonitor.Controls.Add(this.txtBookingMonitorCompany);
            this.gbBookingMonitor.Controls.Add(this.label2);
            this.gbBookingMonitor.Controls.Add(this.dtpkBookingMonitorDate);
            this.gbBookingMonitor.Controls.Add(this.label1);
            this.gbBookingMonitor.Location = new System.Drawing.Point(11, 6);
            this.gbBookingMonitor.Name = "gbBookingMonitor";
            this.gbBookingMonitor.Size = new System.Drawing.Size(326, 147);
            this.gbBookingMonitor.TabIndex = 1;
            this.gbBookingMonitor.TabStop = false;
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
            // txtBookingMonitorCompany
            // 
            this.txtBookingMonitorCompany.Location = new System.Drawing.Point(70, 45);
            this.txtBookingMonitorCompany.Name = "txtBookingMonitorCompany";
            this.txtBookingMonitorCompany.Size = new System.Drawing.Size(240, 20);
            this.txtBookingMonitorCompany.TabIndex = 3;
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
            this.dtpkBookingMonitorDate.Location = new System.Drawing.Point(70, 22);
            this.dtpkBookingMonitorDate.Name = "dtpkBookingMonitorDate";
            this.dtpkBookingMonitorDate.Size = new System.Drawing.Size(99, 20);
            this.dtpkBookingMonitorDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date:";
            // 
            // chkBookingMonitor
            // 
            this.chkBookingMonitor.AutoSize = true;
            this.chkBookingMonitor.Checked = true;
            this.chkBookingMonitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBookingMonitor.Location = new System.Drawing.Point(7, 7);
            this.chkBookingMonitor.Name = "chkBookingMonitor";
            this.chkBookingMonitor.Size = new System.Drawing.Size(103, 17);
            this.chkBookingMonitor.TabIndex = 0;
            this.chkBookingMonitor.Text = "Booking Monitor";
            this.chkBookingMonitor.UseVisualStyleBackColor = true;
            this.chkBookingMonitor.CheckedChanged += new System.EventHandler(this.chkBookingMonitor_CheckedChanged);
            // 
            // chkBloodTaking
            // 
            this.chkBloodTaking.AutoSize = true;
            this.chkBloodTaking.Location = new System.Drawing.Point(7, 160);
            this.chkBloodTaking.Name = "chkBloodTaking";
            this.chkBloodTaking.Size = new System.Drawing.Size(89, 17);
            this.chkBloodTaking.TabIndex = 2;
            this.chkBloodTaking.Text = "Blood Taking";
            this.chkBloodTaking.UseVisualStyleBackColor = true;
            this.chkBloodTaking.CheckedChanged += new System.EventHandler(this.chkBloodTaking_CheckedChanged);
            // 
            // gbBloodTaking
            // 
            this.gbBloodTaking.Controls.Add(this.numPax);
            this.gbBloodTaking.Controls.Add(this.label6);
            this.gbBloodTaking.Controls.Add(this.txtBloodTakingCompany);
            this.gbBloodTaking.Controls.Add(this.label7);
            this.gbBloodTaking.Controls.Add(this.dtpkBloodTakingDate);
            this.gbBloodTaking.Controls.Add(this.label8);
            this.gbBloodTaking.Enabled = false;
            this.gbBloodTaking.Location = new System.Drawing.Point(11, 159);
            this.gbBloodTaking.Name = "gbBloodTaking";
            this.gbBloodTaking.Size = new System.Drawing.Size(326, 103);
            this.gbBloodTaking.TabIndex = 3;
            this.gbBloodTaking.TabStop = false;
            // 
            // numPax
            // 
            this.numPax.Location = new System.Drawing.Point(70, 69);
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
            this.label6.Location = new System.Drawing.Point(38, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Pax:";
            // 
            // txtBloodTakingCompany
            // 
            this.txtBloodTakingCompany.Location = new System.Drawing.Point(70, 46);
            this.txtBloodTakingCompany.Name = "txtBloodTakingCompany";
            this.txtBloodTakingCompany.Size = new System.Drawing.Size(240, 20);
            this.txtBloodTakingCompany.TabIndex = 11;
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
            this.btnCancel.Location = new System.Drawing.Point(174, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(95, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddBooking
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(345, 298);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkBloodTaking);
            this.Controls.Add(this.gbBloodTaking);
            this.Controls.Add(this.chkBookingMonitor);
            this.Controls.Add(this.gbBookingMonitor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddBooking";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them lich hen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddBooking_FormClosing);
            this.gbBookingMonitor.ResumeLayout(false);
            this.gbBookingMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAfternoon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMorning)).EndInit();
            this.gbBloodTaking.ResumeLayout(false);
            this.gbBloodTaking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBookingMonitor;
        private System.Windows.Forms.CheckBox chkBookingMonitor;
        private System.Windows.Forms.CheckBox chkBloodTaking;
        private System.Windows.Forms.GroupBox gbBloodTaking;
        private System.Windows.Forms.NumericUpDown numEvening;
        private System.Windows.Forms.NumericUpDown numAfternoon;
        private System.Windows.Forms.NumericUpDown numMorning;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBookingMonitorCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpkBookingMonitorDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBloodTakingCompany;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpkBloodTakingDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}