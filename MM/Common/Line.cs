/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
