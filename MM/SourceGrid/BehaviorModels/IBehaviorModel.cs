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
	/// Represents a behavior of a cell.
	/// </summary>
	public interface IBehaviorModel
	{
		/// <summary>
		/// Fired when a context menu is showed
		/// </summary>
		/// <param name="e"></param>
		void OnContextMenuPopUp(PositionContextMenuEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnMouseDown(PositionMouseEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnMouseUp(PositionMouseEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnMouseMove(PositionMouseEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnMouseEnter(PositionEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnMouseLeave(PositionEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnKeyUp ( PositionKeyEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnKeyDown ( PositionKeyEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnKeyPress ( PositionKeyPressEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnDoubleClick (PositionEventArgs e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void OnClick (PositionEventArgs e);

		/// <summary>
		/// Fired before the cell leave the focus, you can put the e.Cancel = true to cancel the leave operation.
		/// </summary>
		/// <param name="e"></param>
		void OnFocusLeaving(PositionCancelEventArgs e);
		/// <summary>
		/// Fired when the cell has left the focus.
		/// </summary>
		/// <param name="e"></param>
		void OnFocusLeft(PositionEventArgs e);
		/// <summary>
		/// Fired when the focus is entering in the specified cell. You can put the e.Cancel = true to cancel the focus operation.
		/// </summary>
		/// <param name="e"></param>
		void OnFocusEntering(PositionCancelEventArgs e);
		/// <summary>
		/// Fired when the focus enter in the specified cell.
		/// </summary>
		/// <param name="e"></param>
		void OnFocusEntered(PositionEventArgs e);


		/// <summary>
		/// Fired when the SetValue method is called.
		/// </summary>
		/// <param name="e"></param>
		void OnValueChanged(PositionEventArgs e);
		/// <summary>
		/// Fired when the StartEdit is called. You can set the Cancel = true to stop editing.
		/// </summary>
		/// <param name="e"></param>
		void OnEditStarting(PositionCancelEventArgs e);
		/// <summary>
		/// Fired when the EndEdit is called. You can read the Cancel property to determine if the edit is completed. If you change the cancel property there is no effect.
		/// </summary>
		/// <param name="e"></param>
		void OnEditEnded(PositionCancelEventArgs e);
		/// <summary>
		/// Returns true if the current cell can receive the focus. If only one behavior return false the return value is false.
		/// </summary>
		bool CanReceiveFocus
		{
			get;
		}
	}
}
