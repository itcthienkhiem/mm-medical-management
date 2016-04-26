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
	/// Summary description for BehaviorModelRowHeader.
	/// </summary>
	public class RowHeader : BehaviorModelGroup
	{
		/// <summary>
		/// Default Row Header
		/// </summary>
		public readonly static RowHeader Default = new RowHeader();

		private Resize m_Resize;

		/// <summary>
		/// Constructor
		/// </summary>
		public RowHeader():this(Resize.ResizeHeight, Button.Default, Unselectable.Default)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_BehaviorResize"></param>
		/// <param name="p_BehaviorButton"></param>
		/// <param name="p_BehaviorUnselectable"></param>
		public RowHeader(Resize p_BehaviorResize, Button p_BehaviorButton, Unselectable p_BehaviorUnselectable)
		{
			m_Resize = p_BehaviorResize;
			SubModels.Add(m_Resize);
			SubModels.Add(p_BehaviorButton);
			SubModels.Add(p_BehaviorUnselectable);
		}

		public override void OnFocusEntering(PositionCancelEventArgs e)
		{
			//check if the user is in a resize region
			if (m_Resize!=null &&
				m_Resize.IsHeightResizing == false &&
				m_Resize.IsWidthResizing == false &&
				e.Grid.Selection.SelectionMode != GridSelectionMode.Col)
			{
				e.Grid.Rows[e.Position.Row].Focus();
				e.Grid.Rows[e.Position.Row].Select = true;
			}

			base.OnFocusEntering (e);
		}

	}
}
