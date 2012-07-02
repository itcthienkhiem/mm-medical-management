namespace MM.Controls
{
    partial class uNormal_Estradiol
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
            this.uNormal_Midcycle = new MM.Controls.uNormal_Chung();
            this.uNormal_FollicularPhase = new MM.Controls.uNormal_Chung();
            this.chkMidcycle = new System.Windows.Forms.CheckBox();
            this.chkFollicularPhase = new System.Windows.Forms.CheckBox();
            this.uNormal_LutelPhase = new MM.Controls.uNormal_Chung();
            this.chkLutelPhase = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // uNormal_Midcycle
            // 
            this.uNormal_Midcycle.DonVi = "";
            this.uNormal_Midcycle.Enabled = false;
            this.uNormal_Midcycle.FromOperator = "<=";
            this.uNormal_Midcycle.FromValue = 0F;
            this.uNormal_Midcycle.FromValueChecked = false;
            this.uNormal_Midcycle.Location = new System.Drawing.Point(118, 29);
            this.uNormal_Midcycle.Name = "uNormal_Midcycle";
            this.uNormal_Midcycle.Size = new System.Drawing.Size(452, 22);
            this.uNormal_Midcycle.TabIndex = 32;
            this.uNormal_Midcycle.ToOperator = "<=";
            this.uNormal_Midcycle.ToValue = 0F;
            this.uNormal_Midcycle.ToValueChecked = false;
            // 
            // uNormal_FollicularPhase
            // 
            this.uNormal_FollicularPhase.DonVi = "";
            this.uNormal_FollicularPhase.Enabled = false;
            this.uNormal_FollicularPhase.FromOperator = "<=";
            this.uNormal_FollicularPhase.FromValue = 0F;
            this.uNormal_FollicularPhase.FromValueChecked = false;
            this.uNormal_FollicularPhase.Location = new System.Drawing.Point(118, 0);
            this.uNormal_FollicularPhase.Name = "uNormal_FollicularPhase";
            this.uNormal_FollicularPhase.Size = new System.Drawing.Size(452, 22);
            this.uNormal_FollicularPhase.TabIndex = 30;
            this.uNormal_FollicularPhase.ToOperator = "<=";
            this.uNormal_FollicularPhase.ToValue = 0F;
            this.uNormal_FollicularPhase.ToValueChecked = false;
            // 
            // chkMidcycle
            // 
            this.chkMidcycle.AutoSize = true;
            this.chkMidcycle.Location = new System.Drawing.Point(0, 33);
            this.chkMidcycle.Name = "chkMidcycle";
            this.chkMidcycle.Size = new System.Drawing.Size(68, 17);
            this.chkMidcycle.TabIndex = 31;
            this.chkMidcycle.Text = "Midcycle";
            this.chkMidcycle.UseVisualStyleBackColor = true;
            this.chkMidcycle.CheckedChanged += new System.EventHandler(this.chkMidcycle_CheckedChanged);
            // 
            // chkFollicularPhase
            // 
            this.chkFollicularPhase.AutoSize = true;
            this.chkFollicularPhase.Location = new System.Drawing.Point(0, 4);
            this.chkFollicularPhase.Name = "chkFollicularPhase";
            this.chkFollicularPhase.Size = new System.Drawing.Size(99, 17);
            this.chkFollicularPhase.TabIndex = 28;
            this.chkFollicularPhase.Text = "Follicular phase";
            this.chkFollicularPhase.UseVisualStyleBackColor = true;
            this.chkFollicularPhase.CheckedChanged += new System.EventHandler(this.chkFollicularPhase_CheckedChanged);
            // 
            // uNormal_LutelPhase
            // 
            this.uNormal_LutelPhase.DonVi = "";
            this.uNormal_LutelPhase.Enabled = false;
            this.uNormal_LutelPhase.FromOperator = "<=";
            this.uNormal_LutelPhase.FromValue = 0F;
            this.uNormal_LutelPhase.FromValueChecked = false;
            this.uNormal_LutelPhase.Location = new System.Drawing.Point(118, 58);
            this.uNormal_LutelPhase.Name = "uNormal_LutelPhase";
            this.uNormal_LutelPhase.Size = new System.Drawing.Size(452, 22);
            this.uNormal_LutelPhase.TabIndex = 34;
            this.uNormal_LutelPhase.ToOperator = "<=";
            this.uNormal_LutelPhase.ToValue = 0F;
            this.uNormal_LutelPhase.ToValueChecked = false;
            // 
            // chkLutelPhase
            // 
            this.chkLutelPhase.AutoSize = true;
            this.chkLutelPhase.Location = new System.Drawing.Point(0, 62);
            this.chkLutelPhase.Name = "chkLutelPhase";
            this.chkLutelPhase.Size = new System.Drawing.Size(81, 17);
            this.chkLutelPhase.TabIndex = 33;
            this.chkLutelPhase.Text = "Lutel phase";
            this.chkLutelPhase.UseVisualStyleBackColor = true;
            this.chkLutelPhase.CheckedChanged += new System.EventHandler(this.chkLutelPhase_CheckedChanged);
            // 
            // uNormal_Estradiol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uNormal_LutelPhase);
            this.Controls.Add(this.chkLutelPhase);
            this.Controls.Add(this.uNormal_Midcycle);
            this.Controls.Add(this.uNormal_FollicularPhase);
            this.Controls.Add(this.chkMidcycle);
            this.Controls.Add(this.chkFollicularPhase);
            this.Name = "uNormal_Estradiol";
            this.Size = new System.Drawing.Size(569, 82);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private uNormal_Chung uNormal_Midcycle;
        private uNormal_Chung uNormal_FollicularPhase;
        private System.Windows.Forms.CheckBox chkMidcycle;
        private System.Windows.Forms.CheckBox chkFollicularPhase;
        private uNormal_Chung uNormal_LutelPhase;
        private System.Windows.Forms.CheckBox chkLutelPhase;
    }
}
