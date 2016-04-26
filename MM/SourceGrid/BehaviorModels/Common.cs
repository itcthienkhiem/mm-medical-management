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
using System.Windows.Forms;
using SourceGrid2.Cells;

namespace SourceGrid2.BehaviorModels
{
	/// <summary>
	/// Common behavior of the cell. This behavior can be shared between multiple cells.
	/// </summary>
	public class Common : BehaviorModelGroup
	{
		/// <summary>
		/// The default behavior of a cell.
		/// </summary>
		public readonly static Common Default = new Common();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnKeyDown ( PositionKeyEventArgs e)
		{
			base.OnKeyDown(e);


			if (e.KeyEventArgs.KeyCode == Keys.F2 &&  e.Cell.DataModel != null && ((e.Cell.DataModel.EditableMode & EditableMode.F2Key) == EditableMode.F2Key))
				e.Cell.StartEdit(e.Position, null);

			//viene gi� gestito nella selection
//			//on cancel
//			if (e.KeyEventArgs.KeyCode == Keys.Delete && e.Cell.IsEditing(e.Position) == false && e.Cell.DataModel != null)
//			{
//				e.Cell.DataModel.ClearCell(e.Cell, e.Position);
//			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnKeyPress ( PositionKeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			if ( e.Cell.DataModel != null &&  (e.Cell.DataModel.EditableMode & EditableMode.AnyKey) == EditableMode.AnyKey && e.Cell.IsEditing(e.Position) == false)
			{
				e.Cell.StartEdit(e.Position, e.KeyPressEventArgs.KeyChar.ToString());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnDoubleClick (PositionEventArgs e)
		{
			base.OnDoubleClick(e);

			if ( e.Cell.DataModel != null && (e.Cell.DataModel.EditableMode & EditableMode.DoubleClick) == EditableMode.DoubleClick)
				e.Cell.StartEdit(e.Position, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnClick (PositionEventArgs e)
		{
			base.OnClick(e);

			if ( e.Cell.DataModel != null && (e.Cell.DataModel.EditableMode & EditableMode.SingleClick) == EditableMode.SingleClick)
				e.Cell.StartEdit(e.Position, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnFocusEntered(PositionEventArgs e)
		{
			try
			{
				base.OnFocusEntered(e);
				e.Grid.Selection.Add(e.Position);

				//seleziono questa cella e automaticamente sposto la visuale su questa cella
				e.Grid.ShowCell(e.Position);

				//Getsione dell'edit sul focus, non lo metto all'interno della cella perch� un utente potrebbe chiamare direttamente il metodo SetFocusCell senza passare dalla cella
				if ( e.Cell.DataModel != null && (e.Cell.DataModel.EditableMode & EditableMode.Focus) == EditableMode.Focus)
					e.Cell.StartEdit(e.Position, null);
			}
			catch
			{

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnFocusLeft(PositionEventArgs e)
		{
			base.OnFocusLeft (e);

			if (e.Grid!=null)
				e.Grid.InvalidateCell(e.Position);
		}


		/// <summary>
		/// Fired when the SetValue method is called.
		/// </summary>
		/// <param name="e"></param>
		public override void OnValueChanged(PositionEventArgs e)
		{
			base.OnValueChanged(e);

			if (e.Grid!=null)
				e.Grid.InvalidateCell(e.Position);
		}
	}
}
