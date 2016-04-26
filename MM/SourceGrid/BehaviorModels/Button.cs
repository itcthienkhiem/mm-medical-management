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
	/// Summary description for BehaviorModelButton. This behavior can be shared between multiple cells.
	/// </summary>
	public class Button : BehaviorModelGroup
	{
		public readonly static Button Default = new Button();

		public override void OnMouseDown(PositionMouseEventArgs e)
		{
			base.OnMouseDown (e);

			e.Grid.InvalidateCell(e.Position);
		}

		public override void OnMouseUp(PositionMouseEventArgs e)
		{
			base.OnMouseUp (e);

			e.Grid.InvalidateCell(e.Position);
		}
	}
}
