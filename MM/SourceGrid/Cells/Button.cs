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


namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// A cell that rappresent a button 
	/// </summary>
	public abstract class Button : CellVirtual
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Button()
		{
			Behaviors.Add(BehaviorModels.Button.Default);
			VisualModel = VisualModels.Header.Default;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Click"></param>
		public Button(PositionEventHandler p_Click):this()
		{
			if (p_Click!=null)
				Click += p_Click;
		}

		public event PositionEventHandler Click;

		public override void OnClick(PositionEventArgs e)
		{
			base.OnClick (e);

			if (Click!=null)
				Click(this, e);
		}
	}
}

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A cell that rappresent a button 
	/// </summary>
	public class Button : Cell
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		public Button(object p_Value):this(p_Value, null)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		/// <param name="p_Click"></param>
		public Button(object p_Value, PositionEventHandler p_Click):base(p_Value)
		{
			Behaviors.Add(BehaviorModels.Button.Default);

			VisualModel = VisualModels.Header.Default;
			if (p_Click!=null)
				Click += p_Click;
		}

		public event PositionEventHandler Click;

		public override void OnClick(PositionEventArgs e)
		{
			base.OnClick (e);

			if (Click!=null)
				Click(this, e);
		}
	}
}
