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
	/// Summary description for BindProperty.
	/// </summary>
	public class BindProperty : BehaviorModelGroup
	{
		public BindProperty(System.Reflection.PropertyInfo p_Property, object p_LinkObject)
		{
			BindValueAtProperty(p_Property, p_LinkObject);
		}

		public override void OnValueChanged(PositionEventArgs e)
		{
			base.OnValueChanged (e);

			//gestione eventuale link ad una property
			if (m_LinkPropertyInfo != null)
				m_LinkPropertyInfo.SetValue(m_LinkObject,e.Cell.GetValue(e.Position),null);
		}


		#region Bind Property
		//propriet� per gestire il link di una cella ad una property
		private System.Reflection.PropertyInfo m_LinkPropertyInfo = null;
		private object m_LinkObject = null;

		/// <summary>
		/// Bind the cell's value with the property p_Property of the object p_LinkObject
		/// when the cell's value change also the property change
		/// </summary>
		/// <param name="p_Property">linked property</param>
		/// <param name="p_LinkObject">Can be null to call static property</param>
		protected virtual void BindValueAtProperty(System.Reflection.PropertyInfo p_Property, object p_LinkObject)
		{
			m_LinkPropertyInfo = p_Property;
			m_LinkObject = p_LinkObject;
		}

		/// <summary>
		/// UnBind the cell with the property
		/// </summary>
		protected virtual void UnBindValueAtProperty()
		{
			m_LinkPropertyInfo = null;
			m_LinkObject = null;
		}

//		/// <summary>
//		/// Method that can be used to refresh the cell value
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		public void BindObject_ValueChanged(object sender, EventArgs e)
//		{
//			if (m_LinkPropertyInfo!=null)
//			{
//				object tmp = m_LinkPropertyInfo.GetValue(m_LinkObject,null);
//				if (tmp!=Value)
//					Value = tmp;
//			}
//		}
		#endregion

	}
}
