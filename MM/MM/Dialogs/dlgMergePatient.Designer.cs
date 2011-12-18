namespace MM.Dialogs
{
    partial class dlgMergePatient
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
            this.uMergePatient1 = new MM.Controls.uMergePatient();
            this.SuspendLayout();
            // 
            // uMergePatient1
            // 
            this.uMergePatient1.DataSource = null;
            this.uMergePatient1.Location = new System.Drawing.Point(1, 1);
            this.uMergePatient1.Name = "uMergePatient1";
            this.uMergePatient1.Size = new System.Drawing.Size(827, 237);
            this.uMergePatient1.TabIndex = 0;
            // 
            // dlgMergePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 236);
            this.Controls.Add(this.uMergePatient1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgMergePatient";
            this.Text = "Merge Benh Nhan";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uMergePatient uMergePatient1;
    }
}