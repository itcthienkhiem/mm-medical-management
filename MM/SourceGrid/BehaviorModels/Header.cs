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
	/// Summary description for Header.
	/// </summary>
	public class Header : BehaviorModelGroup
	{
		/// <summary>
		/// Default column header behavior
		/// </summary>
		public readonly static Header Default = new Header();

		private Resize m_Resize;
		/// <summary>
		/// Constructor
		/// </summary>
		public Header():this(Resize.ResizeBoth, Button.Default)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_BehaviorResize"></param>
		/// <param name="p_BehaviorButton"></param>
		public Header(Resize p_BehaviorResize, Button p_BehaviorButton)
		{
			m_Resize = p_BehaviorResize;

			SubModels.Add(m_Resize);
			SubModels.Add(p_BehaviorButton);
		}
	}
}
