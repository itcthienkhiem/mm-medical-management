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

namespace SourceGrid2.Cells
{
	/// <summary>
	/// Interface for informations about a cechkbox
	/// </summary>
	public interface ICellCheckBox
	{
		/// <summary>
		/// Checked status (equal to the Value property but returns a bool). Call the GetValue
		/// </summary>
		/// <param name="p_Position"></param>
		bool GetCheckedValue(Position p_Position);

		/// <summary>
		/// Set checked value, call the Model.SetCellValue. Can be called only if EnableEdit is true
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_bChecked"></param>
		void SetCheckedValue(Position p_Position, bool p_bChecked);

		/// <summary>
		/// Get the status of the checkbox at the current position
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		CheckBoxStatus GetCheckBoxStatus(Position p_Position);
	}
}

namespace SourceGrid2
{
	public struct CheckBoxStatus
	{
		public CheckBoxStatus(bool p_CheckEnable, bool p_bChecked, string p_Caption)
		{
			CheckEnable = p_CheckEnable;
			Checked = p_bChecked;
			Caption = p_Caption;
		}
		/// <summary>
		/// Enable or disable the checkbox
		/// </summary>
		public bool CheckEnable;

		/// <summary>
		/// Checked status
		/// </summary>
		public bool Checked;

		/// <summary>
		/// Caption of the CheckBox
		/// </summary>
		public string Caption;
	}
}
