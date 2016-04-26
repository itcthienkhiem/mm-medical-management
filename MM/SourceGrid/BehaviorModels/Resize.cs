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
using System.Drawing;
using SourceGrid2.Cells;

namespace SourceGrid2.BehaviorModels
{
	/// <summary>
	/// Implement the mouse resize features of a cell. This behavior can be shared between multiple cells.
	/// </summary>
	public class Resize : BehaviorModelGroup
	{
		/// <summary>
		/// Resize both width nd height behavior
		/// </summary>
		public readonly static Resize ResizeBoth = new Resize(CellResizeMode.Both);
		/// <summary>
		/// Resize width behavior
		/// </summary>
		public readonly static Resize ResizeWidth = new Resize(CellResizeMode.Width);
		/// <summary>
		/// Resize height behavior
		/// </summary>
		public readonly static Resize ResizeHeight = new Resize(CellResizeMode.Height);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Mode"></param>
		public Resize(CellResizeMode p_Mode)
		{
			m_ResizeMode = p_Mode;
			SubModels.Add(m_ModelCursor);
		}

		#region IBehaviorModel Members

		public override void OnMouseDown(PositionMouseEventArgs e)
		{
			base.OnMouseDown (e);

			m_IsHeightResize = false;
			m_IsWidthResize = false;

			Rectangle l_CellRect = e.Grid.PositionToDisplayRect(e.Position);
			Point l_MousePoint = new Point(e.MouseEventArgs.X, e.MouseEventArgs.Y);

			if (  ( (ResizeMode & CellResizeMode.Width) == CellResizeMode.Width) &&
						IsInResizeHorRegion(l_CellRect, l_MousePoint) )
				m_IsWidthResize = true;
			else if (  ( (ResizeMode & CellResizeMode.Height) == CellResizeMode.Height) &&
						IsInResizeVerRegion(l_CellRect, l_MousePoint) )
				m_IsHeightResize = true;
		}

		public override void OnMouseUp(PositionMouseEventArgs e)
		{
			base.OnMouseUp (e);

			m_IsWidthResize = false;
			m_IsHeightResize = false;
		}

		public override void OnMouseMove(PositionMouseEventArgs e)
		{
			base.OnMouseMove (e);

			Rectangle l_CellRect = e.Grid.PositionToDisplayRect(e.Position);
			Point l_MousePoint = new Point(e.MouseEventArgs.X, e.MouseEventArgs.Y);

			Point l_AbsPoint = e.Grid.PointRelativeToAbsolute(l_MousePoint);
			if (e.Position.Row < e.Grid.FixedRows)
				l_AbsPoint.Y = l_MousePoint.Y;
			if (e.Position.Column < e.Grid.FixedColumns)
				l_AbsPoint.X = l_MousePoint.X;

			//sono gi� in fase di resizing
			if (e.Grid.MouseDownPosition == e.Position)
			{
				if (m_IsWidthResize)
				{
					int l_NewWidth = l_AbsPoint.X - e.Grid.Columns[e.Position.Column].Left;

					if (l_NewWidth>0)
						e.Grid.Columns[e.Position.Column].Width = l_NewWidth+c_MouseDelta;
				}
				else if (m_IsHeightResize)
				{
					int l_NewHeight = l_AbsPoint.Y - e.Grid.Rows[e.Position.Row].Top;

					if (l_NewHeight>0)
						e.Grid.Rows[e.Position.Row].Height = l_NewHeight+c_MouseDelta;
				}
			}

			//comunque aggiorno il cursore
			if ( IsInResizeHorRegion(l_CellRect, l_MousePoint) && (ResizeMode & CellResizeMode.Width) == CellResizeMode.Width )
				e.Cell.Grid.GridCursor = System.Windows.Forms.Cursors.VSplit;
			else if ( IsInResizeVerRegion(l_CellRect, l_MousePoint) && (ResizeMode & CellResizeMode.Height) == CellResizeMode.Height )
				e.Cell.Grid.GridCursor = System.Windows.Forms.Cursors.HSplit;
			else
				e.Cell.Grid.GridCursor = null;
		}

		public override void OnMouseLeave(PositionEventArgs e)
		{
			base.OnMouseLeave(e);

			e.Cell.Grid.GridCursor = null;
			m_IsWidthResize = false;
			m_IsHeightResize = false;
		}

		public override void OnDoubleClick(PositionEventArgs e)
		{
			base.OnDoubleClick(e);

			Point l_Current = e.Grid.PointToClient(System.Windows.Forms.Control.MousePosition);
			Rectangle l_CellRect = e.Grid.PositionToDisplayRect(e.Position);

			if ( (ResizeMode & CellResizeMode.Width) == CellResizeMode.Width &&
				IsInResizeHorRegion(l_CellRect, l_Current))
			{
				e.Grid.AutoSizeColumn(e.Position.Column, e.Grid.AutoSizeMinWidth);
			}
			else if ( (ResizeMode & CellResizeMode.Height) == CellResizeMode.Height &&
				IsInResizeVerRegion(l_CellRect, l_Current))
			{
				e.Grid.AutoSizeRow(e.Position.Row, e.Grid.AutoSizeMinHeight);
			}
		}

		#endregion

		private CellResizeMode m_ResizeMode = CellResizeMode.Both;

		/// <summary>
		/// Resize mode of the cell
		/// </summary>
		public CellResizeMode ResizeMode
		{
			get{return m_ResizeMode;}
		}

		private Cursor m_ModelCursor = new Cursor();

		//Queste variabili indoco lo stato del resize (essendo usate per� in un contesto di MouseEnter e MouseLeave possono essere tranquillamente condivise tra pi� cello o griglie, visto che il mouse in un dato momento sar� solo in una cella particolare, di un thread particolare, ...). Questo � un motivo in pi� per non poter usare questo controllo in multi thread (cosa che nessun controllo windows form pu� fare ...)
		private bool m_IsWidthResize = false;
		private bool m_IsHeightResize = false;

		/// <summary>
		/// Indicates if the behavior is currently resizing a cell width
		/// </summary>
		public bool IsWidthResizing
		{
			get{return m_IsWidthResize;}
		}

		/// <summary>
		/// Indicates if the behavior is currently resizing a cell height
		/// </summary>
		public bool IsHeightResizing
		{
			get{return m_IsHeightResize;}
		}

		#region Support Functions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_CellRectangle">A grid relative rectangle</param>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool IsInResizeHorRegion(Rectangle p_CellRectangle, Point p)
		{
			if (p.X >= p_CellRectangle.Right-c_MouseDelta && p.X <= p_CellRectangle.Right)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_CellRectangle">A grid relative rectangle</param>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool IsInResizeVerRegion(Rectangle p_CellRectangle, Point p)
		{
			if (p.Y >= p_CellRectangle.Bottom-c_MouseDelta && p.Y <= p_CellRectangle.Bottom)
				return true;
			else
				return false;
		}

		private const int c_MouseDelta = 4;

		#endregion
	}
}
