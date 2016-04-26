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

namespace SourceGrid2.DataModels
{
	/// <summary>
	/// Summary description for NumericUpDownEditor.
	/// </summary>
	public class EditorNumericUpDown : EditorControlBase
	{
		private decimal m_Maximum = 100;
		private decimal m_Minimum = 0;
		private decimal m_Increment = 1;

		/// <summary>
		/// Create a model of type Decimal
		/// </summary>
		public EditorNumericUpDown():base(typeof(decimal))
		{
		}
		
		public EditorNumericUpDown(Type p_CellType, decimal p_Maximum, decimal p_Minimum, decimal p_Increment):base(p_CellType)
		{
			if (p_CellType==null || p_CellType == typeof(int) ||
				p_CellType == typeof(long) || p_CellType == typeof(decimal))
			{
				m_Maximum = p_Maximum;
				m_Minimum = p_Minimum;
				m_Increment = p_Increment;
			}
			else
				throw new SourceGridException("Invalid CellType expected long, int or decimal");
		}

		#region Edit Control
		public override Control CreateEditorControl()
		{
			System.Windows.Forms.NumericUpDown l_Control = new System.Windows.Forms.NumericUpDown();
			l_Control.BorderStyle = System.Windows.Forms.BorderStyle.None;

			return l_Control;
		}
		public virtual System.Windows.Forms.NumericUpDown GetEditorNumericUpDown(GridVirtual p_Grid)
		{
			return (System.Windows.Forms.NumericUpDown)GetEditorControl(p_Grid);
		}
		#endregion

		/// <summary>
		/// Start editing the cell passed
		/// </summary>
		/// <param name="p_Cell">Cell to start edit</param>
		/// <param name="p_Position">Editing position(Row/Col)</param>
		/// <param name="p_StartEditValue">Can be null(in this case use the p_cell.Value</param>
		public override void InternalStartEdit(Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
		{
			base.InternalStartEdit(p_Cell, p_Position, p_StartEditValue);

			if (EnableEdit==false)
				return;

			System.Windows.Forms.NumericUpDown l_Control = GetEditorNumericUpDown(p_Cell.Grid);

			l_Control.Maximum = m_Maximum;
			l_Control.Minimum = m_Minimum;
			l_Control.Increment = m_Increment;

			if (p_StartEditValue != null)
			{
				SetValueToControl(p_StartEditValue);
			}
			else
			{
				SetValueToControl(p_Cell.GetValue(p_Position));
			}
		}

		public decimal Maximum
		{
			get{return m_Maximum;}
			set{m_Maximum = value;}
		}
		public decimal Minimum
		{
			get{return m_Minimum;}
			set{m_Minimum = value;}
		}
		public decimal Increment
		{
			get{return m_Increment;}
			set{m_Increment = value;}
		}

		/// <summary>
		/// Returns the value inserted with the current editor control
		/// </summary>
		/// <returns></returns>
		public override object GetEditedValue()
		{
			return GetValueFromControl();
		}

		private object GetValueFromControl()
		{
			if (ValueType == null)
				return GetEditorNumericUpDown(EditCell.Grid).Value;
			if (ValueType == typeof(decimal))
				return GetEditorNumericUpDown(EditCell.Grid).Value;
			if (ValueType == typeof(int))
				return (int)(GetEditorNumericUpDown(EditCell.Grid).Value);
			if (ValueType == typeof(long))
				return (long)(GetEditorNumericUpDown(EditCell.Grid).Value);

			throw new SourceGridException("Invalid type of the cell expected decimal, long or int");
		}

		private void SetValueToControl(object p_Value)
		{
			if (p_Value is decimal)
				GetEditorNumericUpDown(EditCell.Grid).Value = (decimal)p_Value;
			else if (p_Value is long)
				GetEditorNumericUpDown(EditCell.Grid).Value = (decimal)((long)p_Value);
			else if (p_Value is int)
				GetEditorNumericUpDown(EditCell.Grid).Value = (decimal)((int)p_Value);
			else if (p_Value == null)
				GetEditorNumericUpDown(EditCell.Grid).Value = GetEditorNumericUpDown(EditCell.Grid).Minimum;
			else
				throw new SourceGridException("Invalid value, expected Decimal, Int or Long");
		}
	}
}
