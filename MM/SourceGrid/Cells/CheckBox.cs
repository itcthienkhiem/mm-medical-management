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
	/// A Cell with a CheckBox. This Cell is of type bool. Abstract, you must override GetValue and SetValue.
	/// </summary>
	public abstract class CheckBox : CellVirtual, ICellCheckBox
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CheckBox()
		{
			DataModel = new SourceGrid2.DataModels.DataModelBase(typeof(bool));
			VisualModel = SourceGrid2.VisualModels.CheckBox.Default;
			Behaviors.Add(BehaviorModels.CheckBox.Default);
		}

		/// <summary>
		/// Checked status (equal to the Value property but returns a bool). Call the GetValue
		/// </summary>
		/// <param name="p_Position"></param>
		public virtual bool GetCheckedValue(Position p_Position)
		{
			return (bool)GetValue(p_Position);
		}

		/// <summary>
		/// Set checked value, call the Model.SetCellValue. Can be called only if EnableEdit is true
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_bChecked"></param>
		public virtual void SetCheckedValue(Position p_Position, bool p_bChecked)
		{
			if (DataModel!=null && DataModel.EnableEdit)
				DataModel.SetCellValue(this, p_Position, p_bChecked);
		}

		/// <summary>
		/// Get the status of the checkbox at the current position
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public virtual CheckBoxStatus GetCheckBoxStatus(Position p_Position)
		{
			return new CheckBoxStatus(DataModel.EnableEdit, GetCheckedValue(p_Position), null);
		}
	}

}

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A Cell with a CheckBox. This Cell is of type bool.
	/// </summary>
	public class CheckBox : Cell, ICellCheckBox
	{
		#region Constructor
		/// <summary>
		/// Construct a CellCheckBox class with no caption, and allign the checkbox in MiddleCenter position
		/// </summary>
		/// <param name="p_InitialValue"></param>
		public CheckBox(bool p_InitialValue):this(null, p_InitialValue)
		{
		}

		/// <summary>
		/// Construct a CellCheckBox class with caption and align checkbox in the MiddleLeft
		/// </summary>
		/// <param name="p_Caption"></param>
		/// <param name="p_InitialValue"></param>
		public CheckBox(string p_Caption, bool p_InitialValue)
		{
			m_Caption = p_Caption;

			DataModel = new SourceGrid2.DataModels.DataModelBase(typeof(bool));

			if (p_Caption == null || p_Caption.Length <= 0)
				VisualModel = SourceGrid2.VisualModels.CheckBox.Default;
			else
				VisualModel = SourceGrid2.VisualModels.CheckBox.MiddleLeftAlign;

			Behaviors.Add(BehaviorModels.CheckBox.Default);
			Value = p_InitialValue;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Checked status (equal to the Value property but returns a bool)
		/// </summary>
		public bool Checked
		{
			get{return GetCheckedValue(Range.Start);}
			set{SetCheckedValue(Range.Start, value);}
		}

		private string m_Caption;
		/// <summary>
		/// Caption of the cell
		/// </summary>
		public string Caption
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}
		#endregion

		/// <summary>
		/// Checked status (equal to the Value property but returns a bool). Call the GetValue
		/// </summary>
		/// <param name="p_Position"></param>
		public virtual bool GetCheckedValue(Position p_Position)
		{
			return (bool)GetValue(p_Position);
		}

		/// <summary>
		/// Set checked value, call the Model.SetCellValue. Can be called only if EnableEdit is true
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_bChecked"></param>
		public virtual void SetCheckedValue(Position p_Position, bool p_bChecked)
		{
			if (DataModel!=null && DataModel.EnableEdit)
				DataModel.SetCellValue(this, p_Position, p_bChecked);
		}

		/// <summary>
		/// Get the status of the checkbox at the current position
		/// </summary>
		/// <param name="p_Position"></param>
		/// <returns></returns>
		public virtual CheckBoxStatus GetCheckBoxStatus(Position p_Position)
		{
			return new CheckBoxStatus(DataModel.EnableEdit, GetCheckedValue(p_Position), m_Caption);
		}
	}
}
