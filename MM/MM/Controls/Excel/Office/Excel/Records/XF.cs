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
	public partial class XF : Record
	{
		public XF(Record record) : base(record) { }

		public UInt16 FontIndex;

		public UInt16 FormatIndex;

		public UInt16 CellProtection;

		public Byte Alignment;

		public Byte Rotation;

		public Byte Indent;

		public Byte Attributes;

		public UInt32 LineStyle;

		public UInt32 LineColor;

		public UInt16 Background;

		public override void Decode()
		{
			MemoryStream stream = new MemoryStream(Data);
			BinaryReader reader = new BinaryReader(stream);
			this.FontIndex = reader.ReadUInt16();
			this.FormatIndex = reader.ReadUInt16();
			this.CellProtection = reader.ReadUInt16();
			this.Alignment = reader.ReadByte();
			this.Rotation = reader.ReadByte();
			this.Indent = reader.ReadByte();
			this.Attributes = reader.ReadByte();
			this.LineStyle = reader.ReadUInt32();
			this.LineColor = reader.ReadUInt32();
			this.Background = reader.ReadUInt16();
		}

	}
}
