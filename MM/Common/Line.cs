using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MM.Common
{
    public class Line
    {
        #region Members
        public Point P1 = Point.Empty;
        public Point P2 = Point.Empty;
        #endregion

        #region Constructor
        public Line()
        {
        }

        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }
        #endregion

        #region Public Methods 
        public void Draw(Pen pen, Graphics g)
        {
            g.DrawLine(pen, P1, P2);
        }
        #endregion
    }
}
