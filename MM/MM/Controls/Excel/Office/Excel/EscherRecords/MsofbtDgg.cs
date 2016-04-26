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
	public partial class MsofbtDgg : EscherRecord
	{
		public MsofbtDgg(EscherRecord record) : base(record) { }

		public Int32 MaxShapeID;

		public Int32 NumIDClusters;

		public Int32 NumSavedShapes;

		public Int32 NumSavedDrawings;

		public List<Int64> IDClusters;

		public void decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.MaxShapeID = reader.ReadInt32();
			this.NumIDClusters = reader.ReadInt32();
			this.NumSavedShapes = reader.ReadInt32();
			this.NumSavedDrawings = reader.ReadInt32();
			reader.ReadInt64();
		}

	}
}
