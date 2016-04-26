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
	public partial class MsofbtClientAnchor : EscherRecord
	{
		public MsofbtClientAnchor(EscherRecord record) : base(record) { }

		public UInt16 Flag;

		public UInt16 Col1;

		public UInt16 DX1;

		public UInt16 Row1;

		public UInt16 DY1;

		public UInt16 Col2;

		public UInt16 DX2;

		public UInt16 Row2;

		public UInt16 DY2;

		public Byte[] ExtraData;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.Flag = reader.ReadUInt16();
			this.Col1 = reader.ReadUInt16();
			this.DX1 = reader.ReadUInt16();
			this.Row1 = reader.ReadUInt16();
			this.DY1 = reader.ReadUInt16();
			this.Col2 = reader.ReadUInt16();
			this.DX2 = reader.ReadUInt16();
			this.Row2 = reader.ReadUInt16();
			this.DY2 = reader.ReadUInt16();
			this.ExtraData = reader.ReadBytes((int)(stream.Length - stream.Position));
		}

	}
}
