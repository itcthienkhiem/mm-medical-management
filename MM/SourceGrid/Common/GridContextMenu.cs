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

namespace SourceGrid2
{
	/// <summary>
	/// A class derived from ContextMenu but that is syncronized with the grid using the ContextMenuStyle property
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridContextMenu : System.Windows.Forms.ContextMenu
	{
		private GridVirtual m_Grid;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Grid">The grid to sync with</param>
		public GridContextMenu(GridVirtual p_Grid)
		{
			m_Grid = p_Grid;
		}

		/// <summary>
		/// Grid to sync
		/// </summary>
		public GridVirtual Grid
		{
			get{return m_Grid;}
		}

		/// <summary>
		/// Fired when the contextmenu is showed
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPopup(EventArgs e)
		{
			this.MenuItems.Clear();

			base.OnPopup (e);

			MenuCollection l_Menus = m_Grid.GetGridContextMenus();
			for (int i = 0; i < l_Menus.Count; i++)
			{
				MenuItems.Add(l_Menus[i]);
			}
		}

	}
}
