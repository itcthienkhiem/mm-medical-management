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
	/// A behavior model with a collection of children model (SubModels). Can be used to nest a list of model.
	/// </summary>
	public class BehaviorModelGroup : IBehaviorModel
	{
		#region IBehaviorModel Members
		public virtual void OnContextMenuPopUp(PositionContextMenuEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnContextMenuPopUp(e);
		}

		public virtual void OnMouseDown(PositionMouseEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnMouseDown(e);
		}

		public virtual void OnMouseUp(PositionMouseEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnMouseUp(e);
		}

		public virtual void OnMouseMove(PositionMouseEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnMouseMove(e);
		}

		public virtual void OnMouseEnter(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnMouseEnter(e);
		}

		public virtual void OnMouseLeave(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnMouseLeave(e);
		}

		public virtual void OnKeyUp(PositionKeyEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnKeyUp(e);
		}

		public virtual void OnKeyDown(PositionKeyEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnKeyDown(e);
		}

		public virtual void OnKeyPress(PositionKeyPressEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnKeyPress(e);
		}

		public virtual void OnDoubleClick(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnDoubleClick(e);
		}

		public virtual void OnClick(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnClick(e);
		}

		public virtual void OnFocusLeaving(PositionCancelEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnFocusLeaving(e);
		}
		public virtual void OnFocusLeft(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnFocusLeft(e);
		}

		public virtual void OnFocusEntering(PositionCancelEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnFocusEntering(e);
		}
		public virtual void OnFocusEntered(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnFocusEntered(e);
		}

		public virtual void OnValueChanged(PositionEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnValueChanged(e);
		}
		public virtual void OnEditStarting(PositionCancelEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnEditStarting(e);
		}
		public virtual void OnEditEnded(PositionCancelEventArgs e)
		{
			for (int i = 0; i < m_SubModels.Count; i++)
				m_SubModels[i].OnEditEnded(e);
		}

		#endregion

		private BehaviorModelCollection m_SubModels = new BehaviorModelCollection();
		public BehaviorModelCollection SubModels
		{
			get{return m_SubModels;}
		}

		/// <summary>
		/// True if the cell can have the focus otherwise false. This method simply call BehaviorModel.CanReceiveFocus.
		/// </summary>
		public virtual bool CanReceiveFocus
		{
			get
			{
				bool ret = true;
				for (int i = 0; i < m_SubModels.Count; i++)
					ret = ret && m_SubModels[i].CanReceiveFocus;

				return ret;
			}
		}
	}
}
