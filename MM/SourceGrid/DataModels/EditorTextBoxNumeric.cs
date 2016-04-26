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
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace SourceGrid2.DataModels
{
	/// <summary>
	/// A DataModel that use a TextBoxTypedNumeric for editing support. You can customize the NumericCharStyle property to enable char validation.
	/// This class automatically set the ValidCharacters and InvalidCharacters using SourceLibrary.Windows.Forms.TextBoxTypedNumeric.CreateNumericValidChars method.
	/// </summary>
	public class EditorTextBoxNumeric : EditorTextBox
	{
		#region Constructor
		/// <summary>
		/// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
		/// </summary>
		/// <param name="p_Type">The type of this model</param>
		public EditorTextBoxNumeric(Type p_Type):base(p_Type)
		{
		}
		#endregion

		#region Edit Control
		public override Control CreateEditorControl()
		{
			SourceLibrary.Windows.Forms.TextBoxTypedNumeric l_Control = new SourceLibrary.Windows.Forms.TextBoxTypedNumeric();
			l_Control.BorderStyle = BorderStyle.None;
			l_Control.AutoSize = false;
			return l_Control;
		}
		public virtual SourceLibrary.Windows.Forms.TextBoxTypedNumeric GetEditorTextBoxNumeric(GridVirtual p_Grid)
		{
			return (SourceLibrary.Windows.Forms.TextBoxTypedNumeric)GetEditorControl(p_Grid);
		}
		#endregion

		public override void InternalStartEdit(SourceGrid2.Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
		{
			ValidCharacters = SourceLibrary.Windows.Forms.TextBoxTypedNumeric.CreateNumericValidChars(CultureInfo, m_NumericCharStyle);
			InvalidCharacters = null;
			base.InternalStartEdit (p_Cell, p_Position, p_StartEditValue);
		}

		private SourceLibrary.Windows.Forms.NumericCharStyle m_NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;

		/// <summary>
		/// This property automatically set the ValidCharacters and InvalidCharacters using SourceLibrary.Windows.Forms.TextBoxTypedNumeric.CreateNumericValidChars method.
		/// </summary>
		public SourceLibrary.Windows.Forms.NumericCharStyle NumericCharStyle
		{
			get{return m_NumericCharStyle;}
			set{m_NumericCharStyle = value;}
		}
	}
}
