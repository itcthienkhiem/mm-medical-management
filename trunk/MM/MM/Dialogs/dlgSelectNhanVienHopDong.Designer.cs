namespace MM.Dialogs
{
    partial class dlgSelectNhanVienHopDong
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSelectNhanVienHopDong));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTenHopDong = new System.Windows.Forms.TextBox();
            this.cboMaHopDong = new System.Windows.Forms.ComboBox();
            this.companyContractViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this._uSearchPatient = new MM.Controls.uSearchPatient();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTenHopDong);
            this.panel1.Controls.Add(this.cboMaHopDong);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 33);
            this.panel1.TabIndex = 0;
            // 
            // txtTenHopDong
            // 
            this.txtTenHopDong.Location = new System.Drawing.Point(271, 11);
            this.txtTenHopDong.Name = "txtTenHopDong";
            this.txtTenHopDong.ReadOnly = true;
            this.txtTenHopDong.Size = new System.Drawing.Size(310, 20);
            this.txtTenHopDong.TabIndex = 7;
            // 
            // cboMaHopDong
            // 
            this.cboMaHopDong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboMaHopDong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMaHopDong.DataSource = this.companyContractViewBindingSource;
            this.cboMaHopDong.DisplayMember = "ContractCode";
            this.cboMaHopDong.FormattingEnabled = true;
            this.cboMaHopDong.Location = new System.Drawing.Point(89, 10);
            this.cboMaHopDong.Name = "cboMaHopDong";
            this.cboMaHopDong.Size = new System.Drawing.Size(179, 21);
            this.cboMaHopDong.TabIndex = 6;
            this.cboMaHopDong.ValueMember = "CompanyContractGUID";
            this.cboMaHopDong.SelectedIndexChanged += new System.EventHandler(this.cboMaHopDong_SelectedIndexChanged);
            // 
            // companyContractViewBindingSource
            // 
            this.companyContractViewBindingSource.DataSource = typeof(MM.Databasae.CompanyContractView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã hợp đồng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 533);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 37);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(403, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(324, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._uSearchPatient);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(803, 500);
            this.panel3.TabIndex = 2;
            // 
            // _uSearchPatient
            // 
            this._uSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uSearchPatient.HopDongGUID = "";
            this._uSearchPatient.IsMulti = false;
            this._uSearchPatient.Location = new System.Drawing.Point(0, 0);
            this._uSearchPatient.Name = "_uSearchPatient";
            this._uSearchPatient.PatientGUID = "";
            this._uSearchPatient.PatientSearchType = MM.Common.PatientSearchType.BenhNhan;
            this._uSearchPatient.ServiceGUID = "";
            this._uSearchPatient.Size = new System.Drawing.Size(803, 500);
            this._uSearchPatient.TabIndex = 1;
            // 
            // dlgSelectNhanVienHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(803, 570);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSelectNhanVienHopDong";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chon nhan vien hop dong";
            this.Load += new System.EventHandler(this.dlgSelectNhanVienHopDong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyContractViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMaHopDong;
        private System.Windows.Forms.TextBox txtTenHopDong;
        private System.Windows.Forms.BindingSource companyContractViewBindingSource;
        private System.Windows.Forms.Panel panel3;
        private Controls.uSearchPatient _uSearchPatient;
    }
}