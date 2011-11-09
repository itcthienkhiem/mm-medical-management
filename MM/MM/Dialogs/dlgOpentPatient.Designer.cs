namespace MM.Dialogs
{
    partial class dlgOpentPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgOpentPatient));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenPatient = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._uSearchPatient = new MM.Controls.uSearchPatient();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOpenPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 36);
            this.panel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(120, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpenPatient
            // 
            this.btnOpenPatient.Image = global::MM.Properties.Resources.folder_customer_icon__1_;
            this.btnOpenPatient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenPatient.Location = new System.Drawing.Point(8, 6);
            this.btnOpenPatient.Name = "btnOpenPatient";
            this.btnOpenPatient.Size = new System.Drawing.Size(107, 25);
            this.btnOpenPatient.TabIndex = 10;
            this.btnOpenPatient.Text = "      &Mở bệnh nhân";
            this.btnOpenPatient.UseVisualStyleBackColor = true;
            this.btnOpenPatient.Click += new System.EventHandler(this.btnOpenPatient_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._uSearchPatient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(853, 399);
            this.panel2.TabIndex = 1;
            // 
            // _uSearchPatient
            // 
            this._uSearchPatient.DataSource = null;
            this._uSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uSearchPatient.Location = new System.Drawing.Point(0, 0);
            this._uSearchPatient.Name = "_uSearchPatient";
            this._uSearchPatient.Size = new System.Drawing.Size(853, 399);
            this._uSearchPatient.TabIndex = 0;
            // 
            // dlgOpentPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(853, 435);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgOpentPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mo benh nhan";
            this.Load += new System.EventHandler(this.dlgOpentPatient_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenPatient;
        private System.Windows.Forms.Button btnCancel;
        private Controls.uSearchPatient _uSearchPatient;
    }
}