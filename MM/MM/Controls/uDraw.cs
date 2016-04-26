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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Controls
{
    public partial class uDraw : UserControl
    {
        #region Members
        private DrawType _type = DrawType.None;
        private Color _color = Color.Black;
        private int _width = 1;
        private Pen _pen = null;
        private Line _line = null;
        private Pencil _pencil = null;
        private List<Line> _lineList = new List<Line>();
        private List<Pencil> _pencilList = new List<Pencil>();
        Bitmap bufferBmp = null;
        Graphics bufferGraphics = null;

        Bitmap canvasBmp = null;
        Graphics canvasGraphics = null;

        Graphics panelGraphics = null;
        #endregion

        #region Constructor
        public uDraw()
        {
            InitializeComponent();

            bufferBmp = new Bitmap(pDraw.Width, pDraw.Height);
            bufferGraphics = Graphics.FromImage(bufferBmp);

            canvasBmp = new Bitmap(pDraw.Width, pDraw.Height); ;
            canvasGraphics = Graphics.FromImage(canvasBmp);
            canvasGraphics.Clear(Color.White);

            panelGraphics = pDraw.CreateGraphics(); 

            _pen = new Pen(_color);
            _pen.Width = _width;
        }
        #endregion

        #region Properties
        public DrawType DrawType
        {
            get { return _type; }
            set { _type = value; }
        }

        public Color Color
        {
            get { return _color; }
            set 
            { 
                _color = value;
                _pen.Color = _color;
            }
        }

        public int BrushWidth
        {
            get { return _width; }
            set 
            { 
                _width = value;
                _pen.Width = _width;
            }
        }
        #endregion

        #region UI Command
        private void DrawAll()
        {
            if (_lineList.Count > 0)
            {
                foreach (Line line in _lineList)
                {
                    line.Draw(_pen, canvasGraphics);
                }
            }

            if (_pencilList.Count > 0)
            {
                foreach (Pencil pencil in _pencilList)
                {
                    pencil.Draw(_pen, canvasGraphics);
                }
            }

            bufferGraphics.DrawImage(canvasBmp, 0, 0);
        }

        private void InvalidateEx()
        {
            panelGraphics.DrawImageUnscaled(bufferBmp, 0, 0);     
        }
        #endregion

        #region Window Event Handler
        private void pDraw_Paint(object sender, PaintEventArgs e)
        {
            if (_type == Common.DrawType.None) return;
            e.Graphics.DrawImageUnscaled(bufferBmp, 0, 0);            
        }        
        #endregion

        private void pDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (_type == Common.DrawType.None) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                switch (_type)
                {
                    case DrawType.Line:
                        _line = new Line();
                        _line.P1.X = e.X;
                        _line.P1.Y = e.Y;
                        break;
                    case DrawType.Pencil:
                        _pencil = new Pencil();
                        _pencil.Points.Add(new Point(e.X, e.Y));
                        break;
                }
            }
        }

        private void pDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (_type == Common.DrawType.None) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                switch (_type)
                {
                    case DrawType.Line:
                        _line.P2.X = e.X;
                        _line.P2.Y = e.Y;
                        if (_line.P1 != _line.P2) _lineList.Add(_line);
                        else _line = null;
                        break;
                    case DrawType.Pencil:
                        _pencilList.Add(_pencil);
                        break;
                }

                DrawAll();
            }
            else
            {
                _line = null;

                if (_pencil != null)
                {
                    _pencil.Points.Clear();
                    _pencil = null;
                }
            }
        }

        private void pDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (_type == Common.DrawType.None) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bufferGraphics.DrawImage(canvasBmp, 0, 0);

                switch (_type)
                {
                    case DrawType.Line:
                        _line.P2.X = e.X;
                        _line.P2.Y = e.Y;
                        _line.Draw(_pen, bufferGraphics);
                        break;
                    case DrawType.Pencil:
                        _pencil.Points.Add(new Point(e.X, e.Y));
                        _pencil.Draw(_pen, bufferGraphics);
                        break;
                }
                
                InvalidateEx();
            }
        }
    }
}
