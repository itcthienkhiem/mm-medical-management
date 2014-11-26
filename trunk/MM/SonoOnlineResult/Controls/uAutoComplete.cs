using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using MM.Common;

namespace SonoOnlineResult.Dialogs
{
	public class uAutoComplete : System.Windows.Forms.UserControl
	{
		#region Members
		private System.Windows.Forms.RichTextBox txtTextBox;
		private System.ComponentModel.Container components = null;
        private DlgListBoxData _dlg = new DlgListBoxData();
		private bool _flag = true;
		private char[] _char = {';', ','};
        public List<string> Values = new List<string>();
		#endregion

		#region Contructor & Destructor
        public uAutoComplete()
		{
			InitializeComponent();

            _dlg.ListBoxData.Click += new EventHandler(ListBoxData_Click);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtTextBox
            // 
            this.txtTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTextBox.HideSelection = false;
            this.txtTextBox.Location = new System.Drawing.Point(0, 0);
            this.txtTextBox.Multiline = false;
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtTextBox.Size = new System.Drawing.Size(296, 20);
            this.txtTextBox.TabIndex = 0;
            this.txtTextBox.Text = "";
            this.txtTextBox.TextChanged += new System.EventHandler(this.txtTextBox_TextChanged);
            this.txtTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTextBox_KeyDown);
            this.txtTextBox.Leave += new System.EventHandler(this.txtTextBox_Leave);
            this.txtTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtTextBox_PreviewKeyDown);
            // 
            // uAutoComplete
            // 
            this.Controls.Add(this.txtTextBox);
            this.Name = "uAutoComplete";
            this.Size = new System.Drawing.Size(296, 20);
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		public string Value
		{
			get { return txtTextBox.Text; }
			set { txtTextBox.Text = value; }
		}

		public RichTextBox TxtBox
		{
			get { return txtTextBox; }
		}
		#endregion

		#region UI Command
        public List<string> GetEmailList()
        {
            List<string> emailList = new List<string>();
            string emailStr = txtTextBox.Text.Trim().Replace(",", ";");
            string[] emails = emailStr.Split(";".ToCharArray());
            foreach (var email in emails)
            {
                if (email.Trim() == string.Empty) continue;
                emailList.Add(email.Trim().ToLower());
            }

            return emailList;
        }

		public bool CheckInfo()
		{
            if (txtTextBox.Text.Trim() != string.Empty)
            {
                string emailStr = txtTextBox.Text.Trim().Replace(",", ";");
                string[] emails = emailStr.Split(";".ToCharArray());
                foreach (var email in emails)
                {
                    if (email.Trim() == string.Empty) continue;

                    if (!Utility.IsValidEmail(email.Trim().ToLower()))
                    {
                        MessageBox.Show(string.Format("The email address: '{0}' is invalid.", email.Trim().ToLower()),
                            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTextBox.Focus();
                        return false;
                    }
                }
            }

            return true;
		}

		private bool RefreshListBoxData(string value)
		{
			bool bResult = true;

            _dlg.ListBoxData.Items.Clear();

            foreach (var val in Values)
            {
                if (val.ToLower().IndexOf(value.ToLower()) >= 0)
                    _dlg.ListBoxData.Items.Add(val);
            }

            if (_dlg.ListBoxData.Items.Count <= 0)
                bResult = false;
            else
            {
                _dlg.ListBoxData.SelectedIndex = 0;

                Size size = _dlg.Size;
                Size newSize = new System.Drawing.Size(size.Width, (15 * _dlg.ListBoxData.Items.Count) + 2);
                _dlg.Size = newSize;
            }

			return bResult;
		}

		private string ProcessText()
		{
			string[] s = txtTextBox.Text.Split(_char);
			string ss = "";
			s[s.Length - 1] = _dlg.ListBoxData.SelectedItem.ToString();

            foreach (string str in s)
            {
                if (str.Trim() == string.Empty) continue;
                ss += str.Trim() + "; ";
            }

			return ss;
		}

		public void Clear()
		{
			if (_dlg != null)
			{
				_dlg.Close();
				_dlg = null;
			}
		}

        public void Hide()
        {
            if (_dlg != null)
            {
                _dlg.Hide();
                _dlg.WindowState = FormWindowState.Minimized;
            }
        }

        public void RecalLocation()
        {
            
            if (_dlg != null)
            {
                Point point = this.txtTextBox.GetPositionFromCharIndex(txtTextBox.Text.Length);
                point = txtTextBox.PointToScreen(point);
                point.Y += txtTextBox.Height - 2;
                _dlg.Location = point;
            }
        }
		#endregion

		#region Windows Events Handles
		private void txtTextBox_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (_flag)
				{
					string[] s = txtTextBox.Text.Split(_char);
					string str = s[s.Length - 1];

					str = str.TrimStart(" ".ToCharArray());

                    if (str != string.Empty)
                    {
                        if (RefreshListBoxData(str.Trim()))
                        {
                            _dlg.TopMost = true;
                            _dlg.WindowState = FormWindowState.Normal;
                            _dlg.Show();
                            RecalLocation();
                            txtTextBox.Focus();
                        }
                        else
                        {
                            _dlg.Hide();
                            _dlg.WindowState = FormWindowState.Minimized;
                        }
                    }
                    else
                    {
                        _dlg.Hide();
                        _dlg.WindowState = FormWindowState.Minimized;
                    }
				}

				_flag = true;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtTextBox_Leave(object sender, System.EventArgs e)
		{
            this.Hide();
		}

		private void txtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            if (_dlg.WindowState != FormWindowState.Normal) return;

			if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				if (_dlg.ListBoxData.Items.Count > 0)
				{
                    if (e.KeyCode == Keys.Down)
                    {
                        if (_dlg.ListBoxData.SelectedIndex < _dlg.ListBoxData.Items.Count - 1)
                            _dlg.ListBoxData.SelectedIndex = _dlg.ListBoxData.SelectedIndex + 1;
                    }
                    else
                    {
                        if (_dlg.ListBoxData.SelectedIndex > 0)
                            _dlg.ListBoxData.SelectedIndex = _dlg.ListBoxData.SelectedIndex - 1;
                    }
				}
			}
			
			if(e.KeyCode == Keys.Escape)
			{
				_dlg.Hide();
                _dlg.WindowState = FormWindowState.Minimized;
				txtTextBox.Focus();
			}

            if (e.KeyCode == Keys.Enter)
            {
                string ss = ProcessText();

                _flag = false;
                txtTextBox.Text = "";
                txtTextBox.AppendText(ss);
                _flag = true;

                _dlg.Hide();
                _dlg.WindowState = FormWindowState.Minimized;
                txtTextBox.Focus();
            }
		}

        private void txtTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && _dlg.WindowState == FormWindowState.Normal)
            {
                string ss = ProcessText();

                _flag = false;
                txtTextBox.Text = "";
                txtTextBox.AppendText(ss);
                _flag = true;

                _dlg.Hide();
                _dlg.WindowState = FormWindowState.Minimized;
                txtTextBox.Focus();
            }
        }

        private void ListBoxData_Click(object sender, EventArgs e)
        {
            if (_dlg.ListBoxData.SelectedItem == null)
                return;

            string ss = ProcessText();

            _flag = false;
            txtTextBox.Text = "";
            txtTextBox.AppendText(ss);
            _flag = true;

            _dlg.Hide();
            _dlg.WindowState = FormWindowState.Minimized;
            txtTextBox.Focus();
        }
		#endregion
	}
}
