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
	public class MultiImages : Common
	{
		#region Constructors

		/// <summary>
		/// Use default setting and construct a read and write VisualProperties
		/// </summary>
		public MultiImages():this(false)
		{
		}

		/// <summary>
		/// Use default setting
		/// </summary>
		/// <param name="p_bReadOnly"></param>
		public MultiImages(bool p_bReadOnly):base(p_bReadOnly)
		{
		}

		/// <summary>
		/// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_Source"></param>
		/// <param name="p_bReadOnly"></param>
		public MultiImages(MultiImages p_Source, bool p_bReadOnly):base(p_Source, p_bReadOnly)
		{
			m_Images = (PositionedImageCollection)p_Source.m_Images.Clone();
		}
		#endregion

		private PositionedImageCollection m_Images = new PositionedImageCollection();
		/// <summary>
		/// Images of the cells
		/// </summary>
		public PositionedImageCollection SubImages
		{
			//TODO questa reference anche nel caso ReadOnly = true potrebbe essere modificata, come posso fare? applicare il readonly anche alla collection
			get{return m_Images;}
		}

		#region DrawCell
		/// <summary>
		/// Draw the image and the displaystring of the specified cell.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_ImageAndText(Cells.ICellVirtual p_Cell, Position p_CellPosition, System.Windows.Forms.PaintEventArgs e, System.Drawing.Rectangle p_ClientRectangle, DrawCellStatus p_Status)
		{
			base.DrawCell_ImageAndText (p_Cell, p_CellPosition, e, p_ClientRectangle, p_Status);

			RectangleBorder l_Border = Border;
			Color l_ForeColor = ForeColor;
			if (p_Status == DrawCellStatus.Focus)
			{
				l_Border = FocusBorder;
				l_ForeColor = FocusForeColor;
			}
			else if (p_Status == DrawCellStatus.Selected)
			{
				l_Border = SelectionBorder;
				l_ForeColor = SelectionForeColor;
			}

			for (int i = 0; i < m_Images.Count; i++)
			{
				if (m_Images[i]!=null)
				{
					Utility.PaintImageAndText(e.Graphics,
						p_ClientRectangle,
						m_Images[i].Image,
						m_Images[i].Alignment,
						false, 
						null, //not used
						null, //not used
						false, //not used
						l_Border,
						Color.Black, //not used
						null); //not used
				}
			}
		}
		#endregion

		#region Clone
		/// <summary>
		/// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_bReadOnly">True if the new object must be read only, otherwise false.</param>
		/// <returns></returns>
		public override object Clone(bool p_bReadOnly)
		{
			return new MultiImages(this, p_bReadOnly);
		}
		#endregion
	}
}
