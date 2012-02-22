﻿namespace MM.Dialogs
{
    partial class dlgChonHinh
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raHinh4 = new System.Windows.Forms.RadioButton();
            this.raHinh3 = new System.Windows.Forms.RadioButton();
            this.raHinh2 = new System.Windows.Forms.RadioButton();
            this.raHinh1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(92, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(13, 88);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raHinh4);
            this.groupBox1.Controls.Add(this.raHinh3);
            this.groupBox1.Controls.Add(this.raHinh2);
            this.groupBox1.Controls.Add(this.raHinh1);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 81);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // raHinh4
            // 
            this.raHinh4.AutoSize = true;
            this.raHinh4.Location = new System.Drawing.Point(96, 51);
            this.raHinh4.Name = "raHinh4";
            this.raHinh4.Size = new System.Drawing.Size(56, 17);
            this.raHinh4.TabIndex = 23;
            this.raHinh4.TabStop = true;
            this.raHinh4.Text = "Hình 4";
            this.raHinh4.UseVisualStyleBackColor = true;
            // 
            // raHinh3
            // 
            this.raHinh3.AutoSize = true;
            this.raHinh3.Location = new System.Drawing.Point(96, 20);
            this.raHinh3.Name = "raHinh3";
            this.raHinh3.Size = new System.Drawing.Size(56, 17);
            this.raHinh3.TabIndex = 22;
            this.raHinh3.TabStop = true;
            this.raHinh3.Text = "Hình 3";
            this.raHinh3.UseVisualStyleBackColor = true;
            // 
            // raHinh2
            // 
            this.raHinh2.AutoSize = true;
            this.raHinh2.Location = new System.Drawing.Point(17, 51);
            this.raHinh2.Name = "raHinh2";
            this.raHinh2.Size = new System.Drawing.Size(56, 17);
            this.raHinh2.TabIndex = 21;
            this.raHinh2.TabStop = true;
            this.raHinh2.Text = "Hình 2";
            this.raHinh2.UseVisualStyleBackColor = true;
            // 
            // raHinh1
            // 
            this.raHinh1.AutoSize = true;
            this.raHinh1.Checked = true;
            this.raHinh1.Location = new System.Drawing.Point(17, 20);
            this.raHinh1.Name = "raHinh1";
            this.raHinh1.Size = new System.Drawing.Size(56, 17);
            this.raHinh1.TabIndex = 20;
            this.raHinh1.TabStop = true;
            this.raHinh1.Text = "Hình 1";
            this.raHinh1.UseVisualStyleBackColor = true;
            // 
            // dlgChonHinh
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(180, 118);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgChonHinh";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chon hinh";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raHinh4;
        private System.Windows.Forms.RadioButton raHinh3;
        private System.Windows.Forms.RadioButton raHinh2;
        private System.Windows.Forms.RadioButton raHinh1;
    }
}