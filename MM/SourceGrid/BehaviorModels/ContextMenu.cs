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
	/// Allow to customize the contextmenu of a cell. This class read the contextmenu from the ICellContextMenu.GetContextMenu.  This behavior can be shared between multiple cells.
	/// </summary>
	public class ContextMenu : BehaviorModelGroup
	{
		/// <summary>
		/// Default tooltiptext
		/// </summary>
		public readonly static ContextMenu Default = new ContextMenu();

		#region IBehaviorModel Members
		public override void OnContextMenuPopUp(PositionContextMenuEventArgs e)
		{
			base.OnContextMenuPopUp (e);
			if (e.Cell is ICellContextMenu)
			{
				ICellContextMenu l_ContextMenu = (ICellContextMenu)e.Cell;
				MenuCollection l_Menus = l_ContextMenu.GetContextMenu(e.Position);
				if (l_Menus != null && l_Menus.Count > 0)
				{
					if (e.ContextMenu.Count>0)
					{
						System.Windows.Forms.MenuItem l_menuBreak = new System.Windows.Forms.MenuItem("-");
						e.ContextMenu.Add(l_menuBreak);
					}

					foreach (System.Windows.Forms.MenuItem m in l_Menus)
						e.ContextMenu.Add(m);
				}
			}
		}
		#endregion
	}
}
