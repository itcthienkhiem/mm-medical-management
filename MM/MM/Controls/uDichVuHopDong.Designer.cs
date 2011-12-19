namespace MM.Controls
{
    partial class uDichVuHopDong
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.cboHopDong = new System.Windows.Forms.ComboBox();
            this.companyContractBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._ucReportViewer = new MM.Controls.ucReportViewer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.cboHopDong);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 70);
            this.panel1.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(77, 38);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 7;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cboHopDong
            // 
            this.cboHopDong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboHopDong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboHopDong.DataSource = this.companyContractBindingSource;
            this.cboHopDong.DisplayMember = "ContractName";
            this.cboHopDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHopDong.FormattingEnabled = true;
            this.cboHopDong.Location = new System.Drawing.Point(77, 11);
            this.cboHopDong.Name = "cboHopDong";
            this.cboHopDong.Size = new System.Drawing.Size(300, 21);
            this.cboHopDong.TabIndex = 1;
            this.cboHopDong.ValueMember = "CompanyContractGUID";
            // 
            // companyContractBindingSource
            // 
            this.companyContractBindingSource.DataSource = typeof(MM.Databasae.CompanyContract);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hợp đồng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._ucReportViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(758, 364);
            this.panel2.TabIndex = 1;
            // 
            // _ucReportViewer
            // 
            this._ucReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ucReportViewer.Location = new System.Drawing.Point(0, 0);
            this._ucReportViewer.Name = "_ucReportViewer";
            this._ucReportViewer.Size = new System.Drawing.Size(758, 364);
            this._ucReportViewer.TabIndex = 1;
            // 
            // uDichVuHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uDichVuHopDong";
            this.Size = new System.Drawing.Size(758, 434);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboHopDong;
        private System.Windows.Forms.BindingSource companyContractBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnView;
        private ucReportViewer _ucReportViewer;
    }
}
