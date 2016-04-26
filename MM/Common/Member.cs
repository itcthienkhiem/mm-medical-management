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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class Member
    {
        public string ConstractGUID = string.Empty;
        public string CompanyMemberGUID = string.Empty;
        public List<string> AddedServices = new List<string>();
        public List<string> DeletedServices = new List<string>();
        public List<DataRow> DeletedServiceRows = new List<DataRow>();
        public DataTable DataSource = null;

        public void RemoveServiceFromDataSource(string serviceGUID)
        {
            if (DataSource == null) return;

            DataRow[] rows = DataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    DataSource.Rows.Remove(row);
                }
            }
        }
    }
}
