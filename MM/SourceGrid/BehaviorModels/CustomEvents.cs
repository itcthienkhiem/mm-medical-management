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
	/// Summary description for BehaviorModelEvent.
	/// </summary>
	public class CustomEvents : IBehaviorModel
	{
		#region IBehaviorModel Members

		public event PositionContextMenuEventHandler ContextMenuPopUp;
		public void OnContextMenuPopUp(PositionContextMenuEventArgs e)
		{
			if (ContextMenuPopUp!=null)
				ContextMenuPopUp(this, e);
		}

		public event PositionMouseEventHandler MouseDown;
		public void OnMouseDown(PositionMouseEventArgs e)
		{
			if (MouseDown!=null)
				MouseDown(this, e);
		}

		public event PositionMouseEventHandler MouseUp;
		public void OnMouseUp(PositionMouseEventArgs e)
		{
			if (MouseUp!=null)
				MouseUp(this, e);
		}

		public event PositionMouseEventHandler MouseMove;
		public void OnMouseMove(PositionMouseEventArgs e)
		{
			if (MouseMove!=null)
				MouseMove(this, e);
		}

		public event PositionEventHandler MouseEnter;
		public void OnMouseEnter(PositionEventArgs e)
		{
			if (MouseEnter!=null)
				MouseEnter(this, e);
		}

		public event PositionEventHandler MouseLeave;
		public void OnMouseLeave(PositionEventArgs e)
		{
			if (MouseLeave!=null)
				MouseLeave(this, e);
		}

		public event PositionKeyEventHandler KeyUp;
		public void OnKeyUp(PositionKeyEventArgs e)
		{
			if (KeyUp!=null)
				KeyUp(this, e);
		}

		public event PositionKeyEventHandler KeyDown;
		public void OnKeyDown(PositionKeyEventArgs e)
		{
			if (KeyDown!=null)
				KeyDown(this, e);
		}

		public event PositionKeyPressEventHandler KeyPress;
		public void OnKeyPress(PositionKeyPressEventArgs e)
		{
			if (KeyPress!=null)
				KeyPress(this, e);
		}

		public event PositionEventHandler DoubleClick;
		public void OnDoubleClick(PositionEventArgs e)
		{
			if (DoubleClick!=null)
				DoubleClick(this, e);
		}

		public event PositionEventHandler Click;
		public void OnClick(PositionEventArgs e)
		{
			if (Click!=null)
				Click(this, e);
		}

		public event PositionCancelEventHandler FocusLeaving;
		public void OnFocusLeaving(PositionCancelEventArgs e)
		{
			if (FocusLeaving!=null)
				FocusLeaving(this, e);
		}
		public event PositionEventHandler FocusLeft;
		public void OnFocusLeft(PositionEventArgs e)
		{
			if (FocusLeft!=null)
				FocusLeft(this, e);
		}

		public event PositionCancelEventHandler FocusEntering;
		public void OnFocusEntering(PositionCancelEventArgs e)
		{
			if (FocusEntering!=null)
				FocusEntering(this, e);
		}
		public event PositionEventHandler FocusEntered;
		public void OnFocusEntered(PositionEventArgs e)
		{
			if (FocusEntered!=null)
				FocusEntered(this, e);
		}

		public event PositionEventHandler ValueChanged;
		public void OnValueChanged(PositionEventArgs e)
		{
			if (ValueChanged!=null)
				ValueChanged(this, e);
		}

		public event PositionCancelEventHandler EditStarting;
		public virtual void OnEditStarting(PositionCancelEventArgs e)
		{
			if (EditStarting!=null)
				EditStarting(this, e);
		}
		public event PositionCancelEventHandler EditEnded;
		public virtual void OnEditEnded(PositionCancelEventArgs e)
		{
			if (EditEnded!=null)
				EditEnded(this, e);
		}

		public bool CanReceiveFocus
		{
			get{return true;}
		}

		#endregion
	}
}
