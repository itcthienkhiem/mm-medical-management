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
using System.Windows.Forms;
using SourceGrid2.Cells;

namespace SourceGrid2.VisualModels
{
	/// <summary>
	/// Summary description for VisualModelCheckBox.
	/// </summary>
	public class Header : Common
	{
		/// <summary>
		/// Represents a default Header, with a 3D border and a LightGray BackColor
		/// </summary>
		public new readonly static Header Default = new Header(true);
		/// <summary>
		/// Represents a Column Header with the ability to draw an Image in the right to indicates the sort operation. You must use this model with a cell of type ICellSortableHeader.
		/// </summary>
		public readonly static Header ColumnHeader;
		/// <summary>
		/// Represents a Row Header.
		/// </summary>
		public readonly static Header RowHeader;

		#region Constructors

		static Header()
		{
			ColumnHeader = new Header(false);
			ColumnHeader.MakeReadOnly();

			RowHeader = new Header(false);
			RowHeader.TextAlignment = ContentAlignment.MiddleCenter;
			RowHeader.MakeReadOnly();
		}

		/// <summary>
		/// Use default setting and construct a read and write VisualProperties
		/// </summary>
		public Header():this(false)
		{
		}

		/// <summary>
		/// Use default setting
		/// </summary>
		/// <param name="p_bReadOnly"></param>
		public Header(bool p_bReadOnly)
		{
			BackColor = Color.FromKnownColor(KnownColor.Control);
			SyncBorders();
			m_bIsReadOnly = p_bReadOnly;
		}

		/// <summary>
		/// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_Source"></param>
		/// <param name="p_bReadOnly"></param>
		public Header(Header p_Source, bool p_bReadOnly):base(p_Source, p_bReadOnly)
		{
			m_BorderStyle = p_Source.m_BorderStyle;
			m_HeaderTopLeftWidth = p_Source.m_HeaderTopLeftWidth;
			m_HeaderBottomRightWidth = p_Source.m_HeaderBottomRightWidth;
			m_HeaderLightColor = p_Source.m_HeaderLightColor;
			m_HeaderShadowColor = p_Source.m_HeaderShadowColor;
			//SyncBorders(); non serve perch� p_Source dovrebbe essere gi� sincronizzato
		}
		#endregion

		private Color m_HeaderShadowColor = Color.FromKnownColor(KnownColor.ControlDark);
		private Color m_HeaderLightColor = Color.White; //Color.FromKnownColor(KnownColor.ControlLight);
		private int m_HeaderTopLeftWidth = 4;
		private int m_HeaderBottomRightWidth = 3;
		private CommonBorderStyle m_BorderStyle = CommonBorderStyle.Raised;

		/// <summary>
		/// Specifies the dark color of this cell for 3D effects (BorderStyle)
		/// </summary>
		public Color HeaderShadowColor
		{
			get{return m_HeaderShadowColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");

				m_HeaderShadowColor = value;
				SyncBorders();
				OnChange();
			}
		}

		/// <summary>
		/// Cell Border Style
		/// </summary>
		public virtual CommonBorderStyle BorderStyle
		{
			get{return m_BorderStyle;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");

				m_BorderStyle = value;
				SyncBorders();

				OnChange();
			}
		}

		/// <summary>
		/// Specifies the light color of this cell for 3D effects (BorderStyle)
		/// </summary>
		public Color HeaderLightColor
		{
			get{return m_HeaderLightColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");

				m_HeaderLightColor = value;
				SyncBorders();

				OnChange();
			}
		}

		/// <summary>
		/// Specified the width of the border for 3D effects (BorderStyle)
		/// </summary>
		public int HeaderLightBorderWidth
		{
			get{return m_HeaderTopLeftWidth;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");

				m_HeaderTopLeftWidth = value;
				SyncBorders();

				OnChange();
			}
		}

		/// <summary>
		/// Specified the width of the border for 3D effects (BorderStyle)
		/// </summary>
		public int HeaderShadowBorderWidth
		{
			get{return m_HeaderBottomRightWidth;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");

				m_HeaderBottomRightWidth = value;
				SyncBorders();

				OnChange();
			}
		}

		/// <summary>
		/// Returns the minimum required size of the current cell, calculating using the current DisplayString, Image and Borders informations.
		/// </summary>
		/// <param name="p_Graphics"></param>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <returns></returns>
		public override SizeF GetRequiredSize(Graphics p_Graphics,
			Cells.ICellVirtual p_Cell,
			Position p_CellPosition)
		{
			SizeF s = base.GetRequiredSize(p_Graphics, p_Cell, p_CellPosition);
			s.Width += CommonImages.SortUp.Width; //add the width of the sort image
			return s;
		}

		private void SyncBorders()
		{
			Border = new RectangleBorder(new Border(HeaderLightColor, HeaderLightBorderWidth),
										new Border(HeaderShadowColor, HeaderShadowBorderWidth),
										new Border(HeaderLightColor, HeaderLightBorderWidth),
										new Border(HeaderShadowColor, HeaderShadowBorderWidth));
		}

		#region Clone
		/// <summary>
		/// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_bReadOnly">True if the new object must be read only, otherwise false.</param>
		/// <returns></returns>
		public override object Clone(bool p_bReadOnly)
		{
			return new Header(this, p_bReadOnly);
		}
		#endregion

		/// <summary>
		/// Draw the borders of the specified cell using DrawGradient3DBorder
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_Border(SourceGrid2.Cells.ICellVirtual p_Cell, Position p_CellPosition, PaintEventArgs e, Rectangle p_ClientRectangle, DrawCellStatus p_Status)
		{
			Color l_BackColor;
			if (p_Status == DrawCellStatus.Focus)
				l_BackColor = Color.Black;
			else if (p_Status == DrawCellStatus.Selected)
                l_BackColor = Color.Black;
			else
				l_BackColor = BackColor;

			if (p_CellPosition == p_Cell.Grid.MouseDownPosition)
				SourceLibrary.Drawing.ControlPaint.DrawGradient3DBorder(e.Graphics, p_ClientRectangle, l_BackColor, HeaderShadowColor, HeaderLightColor, HeaderShadowBorderWidth, HeaderLightBorderWidth, SourceLibrary.Drawing.Gradient3DBorderStyle.Sunken);
			else
				SourceLibrary.Drawing.ControlPaint.DrawGradient3DBorder(e.Graphics, p_ClientRectangle, l_BackColor, HeaderShadowColor, HeaderLightColor, HeaderShadowBorderWidth, HeaderLightBorderWidth, SourceLibrary.Drawing.Gradient3DBorderStyle.Raised);
		}

		/// <summary>
		/// Draw the image and the displaystring of the specified cell.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_ImageAndText(SourceGrid2.Cells.ICellVirtual p_Cell, Position p_CellPosition, PaintEventArgs e, Rectangle p_ClientRectangle, DrawCellStatus p_Status)
		{
			base.DrawCell_ImageAndText(p_Cell, p_CellPosition, e, p_ClientRectangle, p_Status);

			if (p_Cell is ICellSortableHeader)
			{
				ICellSortableHeader l_Header = (ICellSortableHeader)p_Cell;
				SortStatus l_Status = l_Header.GetSortStatus(p_CellPosition);
				if (l_Status.Mode==GridSortMode.Ascending)
					Utility.PaintImageAndText(e.Graphics, p_ClientRectangle, CommonImages.SortUp, ContentAlignment.MiddleRight, false, null, null, false, Border, Color.Black, null);
				else if (l_Status.Mode==GridSortMode.Descending)
					Utility.PaintImageAndText(e.Graphics, p_ClientRectangle, CommonImages.SortDown, ContentAlignment.MiddleRight, false, null, null, false, Border, Color.Black, null);
			}
		}
	}
}
