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
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QiHe.Office.Excel
{
	public partial class MULBLANK : Record
	{
		public MULBLANK(Record record) : base(record) { }

		/// <summary>
		/// Index to row
		/// </summary>
		public UInt16 RowIndex;

		/// <summary>
		/// Index to first column (fc)
		/// </summary>
		public UInt16 FirstColIndex;

		/// <summary>
		/// List of nc=lc-fc+1 16-bit indexes to XF records
		/// </summary>
		public UInt16 XFIndice;

		/// <summary>
		/// Index to last column (lc)
		/// </summary>
		public Int16 LastColIndex;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.RowIndex = reader.ReadUInt16();
			this.FirstColIndex = reader.ReadUInt16();
			this.XFIndice = reader.ReadUInt16();
			this.LastColIndex = reader.ReadInt16();
		}

	}
}
