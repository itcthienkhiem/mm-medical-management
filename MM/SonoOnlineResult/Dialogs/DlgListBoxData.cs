using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SonoOnlineResult.Dialogs
{
	public class DlgListBoxData : System.Windows.Forms.Form
	{
		#region Members
		private System.Windows.Forms.ListBox lbData;
		private System.ComponentModel.Container components = null;
		#endregion
		
		#region Constructor & Destructor
		public DlgListBoxData()
		{
			InitializeComponent();

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgListBoxData));
            this.lbData = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbData
            // 
            this.lbData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbData.ItemHeight = 15;
            this.lbData.Location = new System.Drawing.Point(0, 0);
            this.lbData.Name = "lbData";
            this.lbData.Size = new System.Drawing.Size(204, 136);
            this.lbData.TabIndex = 0;
            this.lbData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbData_MouseMove);
            // 
            // DlgListBoxData
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(204, 136);
            this.Controls.Add(this.lbData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgListBoxData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		public ListBox ListBoxData
		{
			get { return lbData; }
			set { lbData = value; }
		}
		#endregion

        #region Window Event Handler
        private void lbData_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            int index = lbData.IndexFromPoint(p);
            if (index >= 0)
                lbData.SelectedIndex = index;
        }
        #endregion

        
    }
}