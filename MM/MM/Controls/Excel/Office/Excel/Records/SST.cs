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
	public partial class SST : Record
	{
		public SST(Record record) : base(record) { }

		/// <summary>
		/// Total number of strings in the workbook
		/// </summary>
		public Int32 TotalOccurance;

		/// <summary>
		/// Number of following strings (nm)
		/// </summary>
		public Int32 NumStrings;

		/// <summary>
		/// List of nm Unicode strings, 16-bit string length
		/// </summary>
		public List<String> StringList;

		public void decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.TotalOccurance = reader.ReadInt32();
			this.NumStrings = reader.ReadInt32();
			reader.ReadString();
		}

	}
}
