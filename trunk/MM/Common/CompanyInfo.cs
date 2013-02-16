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
