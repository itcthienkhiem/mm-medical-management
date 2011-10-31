using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uColor : uBase
    {
        #region Members
        private ToolTip _toolTip = new ToolTip();
        #endregion

        #region Constructor
        public uColor()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public Color Color
        {
            get { return picColor.BackColor; }
            set { picColor.BackColor = value; }
        }
        #endregion

        #region Window Event Handlers
        private void uColor_Load(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(picColor, picColor.BackColor.Name);

        }

        private void picColor_MouseDown(object sender, MouseEventArgs e)
        {
            RaiseColorClicked(this.Color);
        }

        private void picColor_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            _toolTip.Active = true;
        }

        private void picColor_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            _toolTip.Active = false;
            _toolTip.Hide(picColor);
        }
        #endregion
    }
}
