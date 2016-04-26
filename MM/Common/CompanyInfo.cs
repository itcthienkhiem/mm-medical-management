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
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class CompanyInfo
    {
        public string CompanyGUID = string.Empty;
        public DataTable DataSource = null;
        public Hashtable AddedMembers = new Hashtable();
        public List<string> DeletedMembers = new List<string>();
        public List<DataRow> DeletedMemberRows = new List<DataRow>();
        public DataTable GiaDichVuDataSource = null;
        public List<string> DeletedGiaDichVus = new List<string>();
        public Dictionary<string, DataTable> DictDichVuCon = null;
        public Dictionary<string, List<string>> DictDeletedDichVuCons = null;

        public List<string> AddedMemberKeys
        {
            get
            {
                List<string> keys = new List<string>();
                foreach (string key in AddedMembers.Keys)
                {
                    keys.Add(key);
                }

                return keys;
            }
        }
    }
}
