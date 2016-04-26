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
	public partial class MULRK : Record
	{
        public List<UInt32> RKList;
        public List<UInt16> XFList;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			RowIndex = reader.ReadUInt16();
			FirstColIndex = reader.ReadUInt16();

            int count = (Size - 6) / 6;
            RKList = new List<uint>(count);
            XFList = new List<ushort>(count);
            for (int i = 0; i < count; i++)
            {
                UInt16 XFIndex = reader.ReadUInt16();
                UInt32 RKValue = reader.ReadUInt32();
                XFList.Add(XFIndex);
                RKList.Add(RKValue);
            }

			LastColIndex = reader.ReadInt16();
		}

	}
}
