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
