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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SourceGrid2.Cells
{
	/// <summary>
	/// Represents a Cell to use with Grid control.
	/// </summary>
	public interface ICell : ICellVirtual
	{
		#region Value, DisplayText, ToolTipText

		/// <summary>
		/// The string representation of the Cell.Value property (default Value.ToString())
		/// </summary>
		string DisplayText
		{
			get;
		}

		/// <summary>
		/// Value of the cell 
		/// </summary>
		object Value
		{
			get;
			set;
		}

		/// <summary>
		/// Object to put additional info for this cell
		/// </summary>
		object Tag
		{
			get;
			set;
		}

		/// <summary>
		/// ToolTipText
		/// </summary>
		string ToolTipText
		{
			get;
			set;
		}
		#endregion

		#region LinkToGrid
		/// <summary>
		/// Link the cell at the specified grid.
		/// REMARKS: To insert a cell in a grid use Grid.InsertCell, this methos is for internal use only
		/// </summary>
		/// <param name="p_grid"></param>
		/// <param name="p_Position"></param>
		void BindToGrid(Grid p_grid, Position p_Position);
		#endregion

		#region Range and Position
		/// <summary>
		/// Returns the current Row and Col position. If this cell is not attached to the grid returns Position.Empty. And the range occupied by the current cell.
		/// Returns the Range of the cells occupied by the current cell. If RowSpan and ColSpan = 1 then this method returns a single cell.
		/// </summary>
		Range Range
		{
			get;
		}

		/// <summary>
		/// Current Row
		/// </summary>
		int Row
		{
			get;
		}

		/// <summary>
		/// Current Column
		/// </summary>
		int Column
		{
			get;
		}
		#endregion

		#region Row/Col Span
		/// <summary>
		/// ColSpan for merge operation, calculated using the current range.
		/// </summary>
		int ColumnSpan
		{
			get;
			set;
		}
		/// <summary>
		/// RowSpan for merge operation, calculated using the current range.
		/// </summary>
		int RowSpan
		{
			get;
			set;
		}

		/// <summary>
		/// Returns true if the position specified is inside the current cell range (use Range.Contains).
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		bool ContainsPosition(Position p_Position);
		#endregion

		#region Selection

		/// <summary>
		/// Gets or Sets if the current cell is selected
		/// </summary>
		bool Select
		{
			get;
			set;
		}

		#endregion

		#region Focus
		/// <summary>
		/// True if the has the focus
		/// </summary>
		bool Focused
		{
			get;
		}

		/// <summary>
		/// Give the focus at the cell
		/// </summary>
		/// <returns>Returns if the cell can receive the focus</returns>
		bool Focus();
		
		/// <summary>
		/// Remove the focus from the cell
		/// </summary>
		/// <returns>Returns true if the cell can leave the focus otherwise false</returns>
		bool LeaveFocus();
		#endregion

		#region Editing
		/// <summary>
		/// True if this cell is currently in edit state, otherwise false.
		/// </summary>
		bool IsEditing();
		#endregion

		#region Invalidate
		/// <summary>
		/// Invalidate this cell
		/// </summary>
		void Invalidate();
		#endregion
	}
}
