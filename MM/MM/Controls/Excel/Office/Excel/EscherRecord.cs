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
    public partial class EscherRecord
    {
        public UInt16 Prop;
        public UInt16 Type;
        public UInt32 Size;
        public byte[] Data;

        public EscherRecord() { }

        public EscherRecord(EscherRecord record)
        {
            Prop = record.Prop;
            Type = record.Type;
            Size = record.Size;
            Data = record.Data;
        }

        /// <summary>
        /// Instance ID
        /// </summary>
        public UInt16 ID
        {
            get { return (UInt16)(Prop >> 4); }
        }


        public virtual void Decode()
        {
        }

        public static EscherRecord ReadBase(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            EscherRecord record = new EscherRecord();
            record.Prop = reader.ReadUInt16();
            record.Type = reader.ReadUInt16();
            record.Size = reader.ReadUInt32();
            record.Data = reader.ReadBytes((int)record.Size);
            return record;
        }
    }
}
