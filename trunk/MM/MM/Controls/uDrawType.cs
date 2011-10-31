using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Controls
{
    public partial class uDrawType : uBase
    {
        #region Members
        private DrawType _type = DrawType.None;
        private int _width = 1;
        private ToolTip _toolTip = new ToolTip();
        #endregion

        #region Constructor
        public uDrawType()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public Image Image
        {
            get { return picDrawType.Image; }
            set { picDrawType.Image = value; }
        }

        public DrawType DrawType
        {
            get { return _type; }
            set { _type = value; }
        }

        public int BrushWidth
        {
            get { return _width; }
            set { _width = value; }
        }
        #endregion

        #region Window Event Handlers
        private void uDrawType_Load(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(picDrawType, _type.ToString());
        }

        private void picDrawType_MouseDown(object sender, MouseEventArgs e)
        {
            RaiseDrawTypeClicked(_type, _width);
        }

        private void picDrawType_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            _toolTip.Active = true;
        }

        private void picDrawType_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            _toolTip.Active = false;
            _toolTip.Hide(picDrawType);
        }
        #endregion
    }
}
