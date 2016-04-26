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
	public partial class DIMENSIONS : Record
	{
		public DIMENSIONS(Record record) : base(record) { }

		/// <summary>
		/// Index to first used row
		/// </summary>
		public Int32 FirstRow;

		/// <summary>
		/// Index to last used row, increased by 1
		/// </summary>
		public Int32 LastRow;

		/// <summary>
		/// Index to first used column
		/// </summary>
		public Int16 FirstColumn;

		/// <summary>
		/// Index to last used column, increased by 1
		/// </summary>
		public Int16 LastColumn;

		/// <summary>
		/// Not used
		/// </summary>
		public Int16 UnUsed;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.FirstRow = reader.ReadInt32();
			this.LastRow = reader.ReadInt32();
			this.FirstColumn = reader.ReadInt16();
			this.LastColumn = reader.ReadInt16();
			this.UnUsed = reader.ReadInt16();
		}

	}
}
