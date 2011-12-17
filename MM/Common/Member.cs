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
    }
}
