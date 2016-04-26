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
        Dictionary<int, int> GroupIdClusters = new Dictionary<int, int>();
		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			MaxShapeID = reader.ReadInt32();
			NumIDClusters = reader.ReadInt32();
			NumSavedShapes = reader.ReadInt32();
			NumSavedDrawings = reader.ReadInt32();
            IDClusters = new List<long>();
            while (stream.Position < stream.Length)
            {
                //IDClusters.Add(reader.ReadInt64());
                int drawingGroupId = reader.ReadInt32();
                int numShapeIdsUsed = reader.ReadInt32();
                GroupIdClusters.Add(drawingGroupId, numShapeIdsUsed);
            }
		}

	}
}
