using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MM.Common;

namespace MM.Databasae
{
    public class MMOverride : MMDataContext
    {
        #region Constructor
        public MMOverride() : base(Global.ConnectionInfo.ConnectionString)
        {
            this.CommandTimeout = 0;
        }
        #endregion
    }

    public partial class spDoanhThuNhanVienTongHopResult
    {
        private string _FullName;
        private double _Revenue;

        public spDoanhThuNhanVienTongHopResult()
        {

        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FullName", DbType = "NVarChar(255)")]
        public string FullName
        {
            get { return this._FullName; }
            set { this._FullName = value; }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Revenue", DbType = "Float")]
        public double Revenue
        {
            get { return this._Revenue; }
            set { this._Revenue = value; }
        }
    }

    public partial class spDoanhThuNhanVienChiTiet
    {
        private string _FullName;
        private DateTime _ActivedDate;
        private double _Revenue;

        public spDoanhThuNhanVienChiTiet()
        {

        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FullName", DbType = "NVarChar(255)")]
        public string FullName
        {
            get { return this._FullName; }
            set { this._FullName = value; }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActivedDate", DbType = "DateTime")]
        public DateTime ActivedDate
        {
            get { return this._ActivedDate; }
            set { this._ActivedDate = value; }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Revenue", DbType = "Float")]
        public double Revenue
        {
            get { return this._Revenue; }
            set { this._Revenue = value; }
        }
    }
}
