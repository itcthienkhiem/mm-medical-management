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

    public partial class CapCuuResult
    {
        #region Members

        private int _SoNgayHetHan = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SoNgayHetHan", DbType = "int")]
        public int SoNgayHetHan
        {
            get { return _SoNgayHetHan; }
            set { _SoNgayHetHan = value; }
        }


        private int _MaCapCuu = 0;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaCapCuu", DbType = "int")]
        public int MaCapCuu
        {
            get { return _MaCapCuu; }
            set { _MaCapCuu = value; }
        }

        private string _TenCapCuu = string.Empty;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TenCapCuu", DbType = "NVarChar(500)")]
        public string TenCapCuu
        {
            get { return _TenCapCuu; }
            set { _TenCapCuu = value; }
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
        public CapCuuResult()
        {

        }

        public CapCuuResult(int soNgayHetHan, int maCapCuu, string tenCapCuu, DateTime ngaySanXuat, DateTime ngayHetHan, 
            int soLuongNhap, int soLuongXuat, int soLuongTon, string donViTinh)
        {
            this.SoNgayHetHan = soNgayHetHan;
            this.MaCapCuu = maCapCuu;
            this.TenCapCuu = tenCapCuu;
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
