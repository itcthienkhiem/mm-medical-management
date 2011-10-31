using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MM.Common
{
    public class Pencil
    {
        #region Members
        public List<Point> Points = new List<Point>();
        #endregion

        #region Constructor
        public Pencil()
        {

        }
        #endregion

        #region Public Methods
        public void Draw(Pen pen, Graphics g)
        {
            g.DrawLines(pen, Points.ToArray());
        }
        #endregion
    }
}
