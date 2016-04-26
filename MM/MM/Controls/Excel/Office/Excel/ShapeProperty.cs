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

namespace QiHe.Office.Excel
{
    /// <summary>
    /// Shape Properties msofbtOPT
    /// The first part of an OPT record is an array of FOPTEs,
    /// consisting of ID-value pairs 
    /// </summary>
    public class ShapeProperty
    {
        public UInt16 PropertyID;

        /// <summary>
        /// BLIP properties just store a BLIP ID (basically an index into an array in the BLIP Store).
        /// only valid if IsComplex is false
        /// </summary>
        public bool IsBlipID;

        /// <summary>
        /// if true, PropertyValue is the length of the data.
        /// The data of the complex properties follows the FOPTE array in the file record.
        /// </summary>
        public bool IsComplex;

        public UInt32 PropertyValue;

        public const int Size = 6;

        public static ShapeProperty Decode(byte[] data, int index)
        {
            ShapeProperty property = new ShapeProperty();
            UInt16 num = BitConverter.ToUInt16(data, index);
            property.PropertyID = (UInt16)(num & 0x3FFF);
            property.IsBlipID = (num - 0x4000) == num;
            property.IsComplex = (num - 0x8000) == num;
            property.PropertyValue = BitConverter.ToUInt32(data, index + 2);
            return property;
        }
    }

}
