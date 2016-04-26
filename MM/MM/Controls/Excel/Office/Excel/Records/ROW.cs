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
	public partial class ROW : Record
	{
		public ROW(Record record) : base(record) { }

		/// <summary>
		/// Index of this row
		/// </summary>
		public UInt16 RowIndex;

		/// <summary>
		/// Index to column of the first cell which is described by a cell record
		/// </summary>
		public UInt16 FirstColIndex;

		/// <summary>
		/// Index to column of the last cell which is described by a cell record, increased by 1
		/// </summary>
		public Int16 LastColIndex;

		public UInt16 RowHeight;

		public UInt16 UnUsed;

		public UInt16 UnUsed2;

		/// <summary>
		/// Option flags and default row formatting
		/// </summary>
		public UInt32 Flags;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.RowIndex = reader.ReadUInt16();
			this.FirstColIndex = reader.ReadUInt16();
			this.LastColIndex = reader.ReadInt16();
			this.RowHeight = reader.ReadUInt16();
			this.UnUsed = reader.ReadUInt16();
			this.UnUsed2 = reader.ReadUInt16();
			this.Flags = reader.ReadUInt32();
		}

	}
}
