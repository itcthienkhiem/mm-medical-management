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
using SourceGrid2.Cells;

namespace SourceGrid2.BehaviorModels
{
	/// <summary>
	/// Allow to customize the tooltiptext of a cell. This class read the tooltiptext from the ICellToolTipText.GetToolTipText.  This behavior can be shared between multiple cells.
	/// </summary>
	public class ToolTipText : BehaviorModelGroup
	{
		/// <summary>
		/// Default tooltiptext
		/// </summary>
		public readonly static ToolTipText Default = new ToolTipText();

		#region IBehaviorModel Members
		public override void OnMouseEnter(PositionEventArgs e)
		{
			base.OnMouseEnter(e);

			ApplyToolTipText(e);
		}

		public override void OnMouseLeave(PositionEventArgs e)
		{
			base.OnMouseLeave(e);

			ResetToolTipText(e);
		}
		#endregion

		/// <summary>
		/// Change the cursor with the cursor of the cell
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ApplyToolTipText(PositionEventArgs e)
		{
			if (e.Cell is ICellToolTipText)
			{
				ICellToolTipText l_CellToolTip = (ICellToolTipText)e.Cell;
				string l_ToolTipText = l_CellToolTip.GetToolTipText(e.Position);
				if (l_ToolTipText != null && l_ToolTipText.Length > 0)
					e.Grid.GridToolTipText = l_ToolTipText;
			}
		}

		/// <summary>
		/// Reset the original cursor
		/// </summary>
		/// <param name="e"></param>
		protected virtual void ResetToolTipText(PositionEventArgs e)
		{
			e.Grid.GridToolTipText = null;
		}
	}
}
