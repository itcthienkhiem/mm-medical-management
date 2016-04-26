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

namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
	/// </summary>
	public abstract class Header : CellVirtual
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Header():this(VisualModels.Header.Default, BehaviorModels.Header.Default)
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Header(VisualModels.IVisualModel p_VisualModel, BehaviorModels.IBehaviorModel p_HeaderBehavior)
		{
			VisualModel = p_VisualModel;
			if (p_HeaderBehavior!=null)
				Behaviors.Add(p_HeaderBehavior);
		}
	}

}

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
	/// </summary>
	public class Header : Cell
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Header():this(null)
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		public Header(object p_Value):this(p_Value, VisualModels.Header.Default, BehaviorModels.Header.Default)
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Header(object p_Value, VisualModels.IVisualModel p_VisualModel, BehaviorModels.IBehaviorModel p_HeaderBehavior):base(p_Value)
		{
			VisualModel = p_VisualModel;
			if (p_HeaderBehavior!=null)
				Behaviors.Add(p_HeaderBehavior);
		}
	}

}
