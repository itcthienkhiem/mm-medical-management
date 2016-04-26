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

namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// A cell with a combobox for editor. Use a model with a ICollection for standard values.
	/// </summary>
	public abstract class ComboBox : CellVirtual
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_CellType"></param>
		/// <param name="p_StandardValues"></param>
		/// <param name="p_StandardValuesExclusive"></param>
		public ComboBox(Type p_CellType, System.Collections.ICollection p_StandardValues, bool p_StandardValuesExclusive)
		{
			DataModel = new DataModels.EditorComboBox(p_CellType, p_StandardValues, p_StandardValuesExclusive);
		}
	}
}

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A cell with a combobox for editor. Use a model with a ICollection for standard values.
	/// </summary>
	public class ComboBox : Cell
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		/// <param name="p_CellType"></param>
		/// <param name="p_StandardValues"></param>
		/// <param name="p_StandardValuesExclusive"></param>
		public ComboBox(object p_Value, Type p_CellType, System.Collections.ICollection p_StandardValues, bool p_StandardValuesExclusive):base(p_Value)
		{
			DataModel = new DataModels.EditorComboBox(p_CellType, p_StandardValues, p_StandardValuesExclusive);
		}
	}
}
