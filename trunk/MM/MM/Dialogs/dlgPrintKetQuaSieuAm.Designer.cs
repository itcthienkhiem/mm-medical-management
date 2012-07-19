namespace MM.Dialogs
{
    partial class dlgPrintKetQuaSieuAm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgPrintKetQuaSieuAm));
            this._uPrintKetQuaSieuAm = new MM.Controls.uPrintKetQuaSieuAm();
            this.SuspendLayout();
            // 
            // _uPrintKetQuaSieuAm
            // 
            this._uPrintKetQuaSieuAm.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPrintKetQuaSieuAm.Location = new System.Drawing.Point(0, 0);
            this._uPrintKetQuaSieuAm.Name = "_uPrintKetQuaSieuAm";
            this._uPrintKetQuaSieuAm.PatientRow = null;
            this._uPrintKetQuaSieuAm.Size = new System.Drawing.Size(803, 610);
            this._uPrintKetQuaSieuAm.TabIndex = 0;
            // 
            // dlgPrintKetQuaSieuAm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 610);
            this.Controls.Add(this._uPrintKetQuaSieuAm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgPrintKetQuaSieuAm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In ket qua sieu am";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.dlgPrintKetQuaSieuAm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uPrintKetQuaSieuAm _uPrintKetQuaSieuAm;
    }
}