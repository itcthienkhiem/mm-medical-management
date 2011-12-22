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

    public partial class ThuocResult
    {
        #region Members
        
        private int _SoNgayHetHan = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoNgayHetHan", DbType = "int")]
        public int SoNgayHetHan
        {
            get { return _SoNgayHetHan; }
            set { _SoNgayHetHan = value; }
        }

        
        private string _MaThuoc = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaThuoc", DbType = "NVarChar(50)")]
        public string MaThuoc
        {
            get { return _MaThuoc; }
            set { _MaThuoc = value; }
        }

        private string _TenThuoc = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TenThuoc", DbType = "NVarChar(255)")]
        public string TenThuoc
        {
            get { return _TenThuoc; }
            set { _TenThuoc = value; }
        }
        
        private string _MaLoThuoc = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaLoThuoc", DbType = "NVarChar(50)")]
        public string MaLoThuoc
        {
            get { return _MaLoThuoc; }
            set { _MaLoThuoc = value; }
        }

        
        private string _TenLoThuoc = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TenLoThuoc", DbType = "NVarChar(255)")]
        public string TenLoThuoc
        {
            get { return _TenLoThuoc; }
            set { _TenLoThuoc = value; }
        }
        
        private DateTime _NgaySanXuat = DateTime.Now;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NgaySanXuat", DbType = "DateTime")]
        public DateTime NgaySanXuat
        {
            get { return _NgaySanXuat; }
            set { _NgaySanXuat = value; }
        }

        
        private DateTime _NgayHetHan = DateTime.Now;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NgayHetHan", DbType = "DateTime")]
        public DateTime NgayHetHan
        {
            get { return _NgayHetHan; }
            set { _NgayHetHan = value; }
        }


        
        private int _SoLuongNhap = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoLuongNhap", DbType = "int")]
        public int SoLuongNhap
        {
            get { return _SoLuongNhap; }
            set { _SoLuongNhap = value; }
        }

        
        private int _SoLuongXuat = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoLuongXuat", DbType = "int")]
        public int SoLuongXuat
        {
            get { return _SoLuongXuat; }
            set { _SoLuongXuat = value; }
        }

        
        private int _SoLuongTon = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoLuongTon", DbType = "int")]
        public int SoLuongTon
        {
            get { return _SoLuongTon; }
            set { _SoLuongTon = value; }
        }

        
        private string _DonViTinh = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DonViTinh", DbType = "NVarChar(50)")]
        public string DonViTinh
        {
            get { return _DonViTinh; }
            set { _DonViTinh = value; }
        }
        #endregion

        #region Constructor
        public ThuocResult()
        {

        }

        public ThuocResult(int soNgayHetHan, string maThuoc, string tenThuoc, string maLoThuoc, string tenLoThuoc, 
            DateTime ngaySanXuat, DateTime ngayHetHan, int soLuongNhap, int soLuongXuat, int soLuongTon, string donViTinh)
        {
            this.SoNgayHetHan = soNgayHetHan;
            this.MaThuoc = maThuoc;
            this.TenThuoc = tenThuoc;
            this.MaLoThuoc = maLoThuoc;
            this.TenLoThuoc = tenLoThuoc;
            this.NgaySanXuat = ngaySanXuat;
            this.NgayHetHan = ngayHetHan;
            this.SoLuongNhap = soLuongNhap;
            this.SoLuongXuat = soLuongXuat;
            this.SoLuongTon = soLuongTon;
            this.DonViTinh = donViTinh;
        }
        #endregion
    }
}
