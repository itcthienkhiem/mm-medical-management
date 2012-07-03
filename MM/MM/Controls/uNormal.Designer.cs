namespace MM.Controls
{
    partial class uNormal
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._uNormal_SoiCanLangNuocTieu = new MM.Controls.uNormal_SoiCanLangNuocTieu();
            this._uNormal_Estradiol = new MM.Controls.uNormal_Estradiol();
            this._uNormal_HutThuoc_KhongHutThuoc = new MM.Controls.uNormal_HutThuoc_KhongHutThuoc();
            this._uNormal_Sang_Chieu = new MM.Controls.uNormal_Sang_Chieu();
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi = new MM.Controls.uNormal_TreEm_NguoiLon_NguoiCaoTuoi();
            this._uNormal_Nam_Nu = new MM.Controls.uNormal_Nam_Nu();
            this._uNormal_Chung = new MM.Controls.uNormal_Chung();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDoiTuong);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._uNormal_SoiCanLangNuocTieu);
            this.groupBox1.Controls.Add(this._uNormal_Estradiol);
            this.groupBox1.Controls.Add(this._uNormal_HutThuoc_KhongHutThuoc);
            this.groupBox1.Controls.Add(this._uNormal_Sang_Chieu);
            this.groupBox1.Controls.Add(this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi);
            this.groupBox1.Controls.Add(this._uNormal_Nam_Nu);
            this.groupBox1.Controls.Add(this._uNormal_Chung);
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bình thường";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDoiTuong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDoiTuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTuong.FormattingEnabled = true;
            this.cboDoiTuong.Items.AddRange(new object[] {
            "",
            "Chung",
            "Nam - Nữ",
            "Trẻ em - Người lớn - Người cao tuổi",
            "Sáng - Chiều",
            "Hút thuốc - Không hút thuốc",
            "Âm tính - Dương tính",
            "Estradiol",
            "Khác"});
            this.cboDoiTuong.Location = new System.Drawing.Point(77, 22);
            this.cboDoiTuong.MaxDropDownItems = 10;
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Size = new System.Drawing.Size(297, 21);
            this.cboDoiTuong.TabIndex = 1;
            this.cboDoiTuong.SelectedIndexChanged += new System.EventHandler(this.cboDoiTuong_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đối tượng:";
            // 
            // _uNormal_SoiCanLangNuocTieu
            // 
            this._uNormal_SoiCanLangNuocTieu.FromToChecked = true;
            this._uNormal_SoiCanLangNuocTieu.FromValue = 0D;
            this._uNormal_SoiCanLangNuocTieu.Location = new System.Drawing.Point(14, 56);
            this._uNormal_SoiCanLangNuocTieu.Name = "_uNormal_SoiCanLangNuocTieu";
            this._uNormal_SoiCanLangNuocTieu.Size = new System.Drawing.Size(242, 22);
            this._uNormal_SoiCanLangNuocTieu.TabIndex = 8;
            this._uNormal_SoiCanLangNuocTieu.ToValue = 10D;
            this._uNormal_SoiCanLangNuocTieu.Visible = false;
            this._uNormal_SoiCanLangNuocTieu.XValue = 40D;
            // 
            // _uNormal_Estradiol
            // 
            this._uNormal_Estradiol.FollicularPhaseChecked = false;
            this._uNormal_Estradiol.Location = new System.Drawing.Point(14, 55);
            this._uNormal_Estradiol.LutelPhaseChecked = false;
            this._uNormal_Estradiol.MidcycleChecked = false;
            this._uNormal_Estradiol.Name = "_uNormal_Estradiol";
            this._uNormal_Estradiol.Size = new System.Drawing.Size(569, 82);
            this._uNormal_Estradiol.TabIndex = 7;
            this._uNormal_Estradiol.Visible = false;
            // 
            // _uNormal_HutThuoc_KhongHutThuoc
            // 
            this._uNormal_HutThuoc_KhongHutThuoc.HutThuocChecked = false;
            this._uNormal_HutThuoc_KhongHutThuoc.KhongHutThuocChecked = false;
            this._uNormal_HutThuoc_KhongHutThuoc.Location = new System.Drawing.Point(14, 54);
            this._uNormal_HutThuoc_KhongHutThuoc.Name = "_uNormal_HutThuoc_KhongHutThuoc";
            this._uNormal_HutThuoc_KhongHutThuoc.Size = new System.Drawing.Size(569, 54);
            this._uNormal_HutThuoc_KhongHutThuoc.TabIndex = 6;
            this._uNormal_HutThuoc_KhongHutThuoc.Visible = false;
            // 
            // _uNormal_Sang_Chieu
            // 
            this._uNormal_Sang_Chieu.ChieuChecked = false;
            this._uNormal_Sang_Chieu.FromTime_Chieu = 4;
            this._uNormal_Sang_Chieu.FromTime_Sang = 7;
            this._uNormal_Sang_Chieu.Location = new System.Drawing.Point(14, 54);
            this._uNormal_Sang_Chieu.Name = "_uNormal_Sang_Chieu";
            this._uNormal_Sang_Chieu.SangChecked = false;
            this._uNormal_Sang_Chieu.Size = new System.Drawing.Size(515, 109);
            this._uNormal_Sang_Chieu.TabIndex = 5;
            this._uNormal_Sang_Chieu.ToTime_Chieu = 8;
            this._uNormal_Sang_Chieu.ToTime_Sang = 10;
            this._uNormal_Sang_Chieu.Visible = false;
            // 
            // _uNormal_TreEm_NguoiLon_NguoiCaoTuoi
            // 
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.Location = new System.Drawing.Point(14, 53);
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.Name = "_uNormal_TreEm_NguoiLon_NguoiCaoTuoi";
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.NguoiCaoTuoiChecked = false;
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.NguoiLonChecked = false;
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.Size = new System.Drawing.Size(552, 83);
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.TabIndex = 4;
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.TreEmChecked = false;
            this._uNormal_TreEm_NguoiLon_NguoiCaoTuoi.Visible = false;
            // 
            // _uNormal_Nam_Nu
            // 
            this._uNormal_Nam_Nu.FromAge_Nam = 20;
            this._uNormal_Nam_Nu.FromAge_NamChecked = false;
            this._uNormal_Nam_Nu.FromAge_Nu = 17;
            this._uNormal_Nam_Nu.FromAge_NuChecked = false;
            this._uNormal_Nam_Nu.Location = new System.Drawing.Point(14, 54);
            this._uNormal_Nam_Nu.NamChecked = false;
            this._uNormal_Nam_Nu.Name = "_uNormal_Nam_Nu";
            this._uNormal_Nam_Nu.NuChecked = false;
            this._uNormal_Nam_Nu.Size = new System.Drawing.Size(506, 106);
            this._uNormal_Nam_Nu.TabIndex = 3;
            this._uNormal_Nam_Nu.ToAge_Nam = 60;
            this._uNormal_Nam_Nu.ToAge_NamChecked = false;
            this._uNormal_Nam_Nu.ToAge_Nu = 60;
            this._uNormal_Nam_Nu.ToAge_NuChecked = false;
            this._uNormal_Nam_Nu.Visible = false;
            // 
            // _uNormal_Chung
            // 
            this._uNormal_Chung.DonVi = "";
            this._uNormal_Chung.FromOperator = "<=";
            this._uNormal_Chung.FromValue = 0D;
            this._uNormal_Chung.FromValueChecked = false;
            this._uNormal_Chung.Location = new System.Drawing.Point(14, 53);
            this._uNormal_Chung.Name = "_uNormal_Chung";
            this._uNormal_Chung.Size = new System.Drawing.Size(452, 24);
            this._uNormal_Chung.TabIndex = 2;
            this._uNormal_Chung.ToOperator = "<=";
            this._uNormal_Chung.ToValue = 0D;
            this._uNormal_Chung.ToValueChecked = false;
            // 
            // uNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "uNormal";
            this.Size = new System.Drawing.Size(597, 175);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDoiTuong;
        private System.Windows.Forms.Label label1;
        private uNormal_Estradiol _uNormal_Estradiol;
        private uNormal_HutThuoc_KhongHutThuoc _uNormal_HutThuoc_KhongHutThuoc;
        private uNormal_Sang_Chieu _uNormal_Sang_Chieu;
        private uNormal_TreEm_NguoiLon_NguoiCaoTuoi _uNormal_TreEm_NguoiLon_NguoiCaoTuoi;
        private uNormal_Nam_Nu _uNormal_Nam_Nu;
        private uNormal_Chung _uNormal_Chung;
        private uNormal_SoiCanLangNuocTieu _uNormal_SoiCanLangNuocTieu;
    }
}
