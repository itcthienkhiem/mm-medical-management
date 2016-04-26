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
	/// Summary description for BehaviorModelCheckBox. This behavior can be shared between multiple cells.
	/// </summary>
	public class CheckBox : BehaviorModelGroup
	{
		/// <summary>
		/// Default behavior checkbox
		/// </summary>
		public readonly static CheckBox Default = new CheckBox();

		/// <summary>
		/// Constructor
		/// </summary>
		public CheckBox()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_bAutoChangeValueOfSelectedCells"></param>
		public CheckBox(bool p_bAutoChangeValueOfSelectedCells)
		{
			m_bAutoChangeValueOfSelectedCells = p_bAutoChangeValueOfSelectedCells;
		}

		#region IBehaviorModel Members

		public override void OnMouseEnter(PositionEventArgs e)
		{
			base.OnMouseEnter(e);

			e.Cell.Invalidate(e.Position);//Invalidate the cell to refresh the checkbox
		}

		public override void OnMouseLeave(PositionEventArgs e)
		{
			base.OnMouseLeave(e);

			e.Cell.Invalidate(e.Position);//Invalidate the cell to refresh the checkbox
		}

		public override void OnKeyPress(PositionKeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			if (e.KeyPressEventArgs.KeyChar == ' ')
				UIChangeChecked(e);
		}

		public override void OnClick(PositionEventArgs e)
		{
			base.OnClick(e);

			UIChangeChecked(e);
		}

		#endregion

		private bool m_bAutoChangeValueOfSelectedCells = false;
		/// <summary>
		/// Indicates if this cells when checked or uncheck must change also the value of the selected cells of type CellCheckBox
		/// </summary>
		public bool AutoChangeValueOfSelectedCells
		{
			get{return m_bAutoChangeValueOfSelectedCells;}
		}

		/// <summary>
		/// Toggle the value of the current cell and if AutoChangeValueOfSelectedCells is true of all the selected cells
		/// </summary>
		/// <param name="e"></param>
		private void UIChangeChecked(PositionEventArgs e)
		{
			ICellCheckBox l_Check = (ICellCheckBox)e.Cell;
			if (l_Check.GetCheckBoxStatus(e.Position).CheckEnable)
			{
				bool l_NewVal = !l_Check.GetCheckedValue(e.Position);
				l_Check.SetCheckedValue(e.Position, l_NewVal);

				//change the status of all selected control
				if (AutoChangeValueOfSelectedCells)
				{
					foreach(Position pos in e.Cell.Grid.Selection.GetCellsPositions())
					{
						Cells.ICellVirtual c = e.Grid.GetCell(pos);
						if (c != this && c != null && c is ICellCheckBox)
						{
							ICellCheckBox cCh = (ICellCheckBox)c;
							cCh.SetCheckedValue(pos, l_NewVal);
						}
					}
				}
			}
		}

	}
}
