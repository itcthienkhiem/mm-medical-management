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

namespace SourceGrid2.VisualModels
{
	/// <summary>
	/// A interface that represents the visual aspect of a cell. Contains the Draw method and the common properties
	/// </summary>
	public interface IVisualModel : ICloneable
	{
		#region Format
		/// <summary>
		/// If null the default font is used
		/// </summary>
		Font Font
		{
			get;
			set;
		}

		/// <summary>
		/// BackColor of the cell
		/// </summary>
		Color BackColor
		{
			get;
			set;
		}

		/// <summary>
		/// ForeColor of the cell
		/// </summary>
		Color ForeColor
		{
			get;
			set;
		}

		/// <summary>
		/// The normal border of a cell
		/// </summary>
		RectangleBorder Border
		{
			get;
			set;
		}

		/// <summary>
		/// Word Wrap.  This property is only a wrapper around StringFormat
		/// </summary>
		bool WordWrap
		{
			get;
			set;
		}

		/// <summary>
		/// Text Alignment. This property is only a wrapper around StringFormat
		/// </summary>
		ContentAlignment TextAlignment
		{
			get;
			set;
		}
		#endregion


		/// <summary>
		/// Draw the cell specified
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		void DrawCell(Cells.ICellVirtual p_Cell,
			Position p_CellPosition,
			System.Windows.Forms.PaintEventArgs e, 
			System.Drawing.Rectangle p_ClientRectangle);


		/// <summary>
		/// Returns the minimum required size of the current cell, calculating using the current DisplayString, Image and Borders informations.
		/// </summary>
		/// <param name="p_Graphics"></param>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <returns></returns>
		SizeF GetRequiredSize(Graphics p_Graphics,
			Cells.ICellVirtual p_Cell,
			Position p_CellPosition);

		/// <summary>
		/// Export the cell contents in html format
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_Position"></param>
		/// <param name="p_Export"></param>
		/// <param name="p_Writer"></param>
		void ExportHTML(Cells.ICellVirtual p_Cell,Position p_Position, IHTMLExport p_Export, System.Xml.XmlTextWriter p_Writer);

		/// <summary>
		/// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_bReadOnly">True if the new object must be read only, otherwise false.</param>
		/// <returns></returns>
		object Clone(bool p_bReadOnly);


		/// <summary>
		/// True if this class is ReadOnly otherwise False.
		/// </summary>
		bool IsReadOnly
		{
			get;
			set;
		}

		/// <summary>
		/// Make the current instance readonly. Use this method to prevent unexpected changes.
		/// </summary>
		void MakeReadOnly();
	}
}
