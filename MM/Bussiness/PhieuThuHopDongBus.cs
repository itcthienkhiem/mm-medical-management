using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class PhieuThuHopDongBus : BusBase
    {
        public static Result GetPhieuThuHopDongList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenKhacHang, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE NgayThu BETWEEN '{0}' AND '{1}' ORDER BY NgayThu DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE TenNguoiNop LIKE N'%{0}%' ORDER BY NgayThu DESC", tenKhacHang);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE Status={0} AND TenNguoiNop LIKE N'%{1}%' ORDER BY NgayThu DESC",
                        (byte)Status.Actived, tenKhacHang);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDongView WITH(NOLOCK) WHERE Status={0} AND TenNguoiNop LIKE N'%{1}%' ORDER BY NgayThu DESC",
                        (byte)Status.Deactived, tenKhacHang);
                    }

                }

                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetChiTietPhieuThuHopDong(string phieuThuHopDongGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM ChiTietPhieuThuHopDong WITH(NOLOCK) WHERE Status={0} AND PhieuThuHopDongGUID='{1}' ORDER BY DichVu",
                    (byte)Status.Actived, phieuThuHopDongGUID);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetPhieuThuHopDongCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM PhieuThuHopDong WITH(NOLOCK)";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
                }
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetPhieuThuHopDong(string phieuThuHopDongGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuHopDong ptthd = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(p => p.PhieuThuHopDongGUID.ToString() == phieuThuHopDongGUID);
                result.QueryResult = ptthd;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result DeletePhieuThuHopDong(List<string> phieuThuHopDongKeys, List<string> noteList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DateTime dt = DateTime.Now;
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in phieuThuHopDongKeys)
                    {
                        PhieuThuHopDong ptthd = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(p => p.PhieuThuHopDongGUID.ToString() == key);
                        if (ptthd != null)
                        {
                            ptthd.DeletedDate = DateTime.Now;
                            ptthd.DeletedBy = Guid.Parse(Global.UserGUID);
                            ptthd.Status = (byte)Status.Deactived;
                            ptthd.Notes = noteList[index];

                            desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Tên khách hàng: '{4}', Công ty: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}'\n",
                                ptthd.PhieuThuHopDongGUID.ToString(), ptthd.CompanyContract.CompanyContractGUID.ToString(),
                                ptthd.MaPhieuThuHopDong, ptthd.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                                ptthd.TenNguoiNop, ptthd.TenCongTy, ptthd.DiaChi, noteList[index], !ptthd.ChuaThuTien);
                        }

                        index++;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin phiếu thu hợp đồng";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result CheckPhieuThuHopDongExistCode(string phieuThuHopDongGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuHopDong ptthd = null;
                if (phieuThuHopDongGUID == null || phieuThuHopDongGUID == string.Empty)
                    ptthd = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(p => p.MaPhieuThuHopDong.ToLower() == code.ToLower());
                else
                    ptthd = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(p => p.MaPhieuThuHopDong.ToLower() == code.ToLower() &&
                                                                p.PhieuThuHopDongGUID.ToString() != phieuThuHopDongGUID);

                if (ptthd == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result InsertPhieuThuHopDong(PhieuThuHopDong ptthd, List<ChiTietPhieuThuHopDong> addedList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (ptthd.PhieuThuHopDongGUID == null || ptthd.PhieuThuHopDongGUID == Guid.Empty)
                    {
                        ptthd.PhieuThuHopDongGUID = Guid.NewGuid();
                        db.PhieuThuHopDongs.InsertOnSubmit(ptthd);
                        db.SubmitChanges();

                        desc += string.Format("- Phiếu thu hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Tên khách hàng: '{4}', Công ty: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}', Đã xuất HĐ: '{9}'\n",
                            ptthd.PhieuThuHopDongGUID.ToString(), ptthd.CompanyContract.CompanyContractGUID, ptthd.MaPhieuThuHopDong, ptthd.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptthd.TenNguoiNop, ptthd.TenCongTy, ptthd.DiaChi, ptthd.Notes, !ptthd.ChuaThuTien, ptthd.IsExported);

                        desc += "- Chi tiết phiếu thu hợp đồng được thêm:\n";

                        //Chi tiet phieu thu
                        DateTime dt = DateTime.Now;
                        foreach (ChiTietPhieuThuHopDong ctpthd in addedList)
                        {
                            ctpthd.PhieuThuHopDongGUID = ptthd.PhieuThuHopDongGUID;
                            ctpthd.ChiTietPhieuThuHopDongGUID = Guid.NewGuid();
                            db.ChiTietPhieuThuHopDongs.InsertOnSubmit(ctpthd);
                            db.SubmitChanges();

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', Đơn giá: '{2}', Số lượng: '{3}', Giảm: '{4}', Thành tiền: '{5}'\n",
                                ctpthd.ChiTietPhieuThuHopDongGUID.ToString(), ctpthd.DichVu, ctpthd.DonGia, ctpthd.SoLuong, ctpthd.Giam, ctpthd.ThanhTien);
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin phiếu thu hợp đồng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }

                    tnx.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetDichVuLamThem(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CM.FullName, CM.DobStr, CM.GenderAsStr, V.[Name], V.FixedPrice, V.Discount, CAST((V.FixedPrice - (V.FixedPrice * V.Discount)/100) AS float) AS ThanhTien FROM dbo.CompanyContract C WITH(NOLOCK), dbo.ContractMember M WITH(NOLOCK), dbo.DichVuLamThemView V WITH(NOLOCK),ContractMemberView CM WITH(NOLOCK) WHERE C.CompanyContractGUID = M.CompanyContractGUID AND V.ContractMemberGUID = M.ContractMemberGUID AND M.Status = 0 AND V.Status = 0 AND V.ServiceStatus = 0 AND C.CompanyContractGUID = '{0}' AND V.ContractMemberGUID = CM.ContractMemberGUID AND CM.Archived = 'False' ORDER BY CM.FullName, V.[Name]", hopDongGUID);
                result = ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetTongTienDichVuLamThem(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM( CAST((V.FixedPrice - (V.FixedPrice * V.Discount)/100) AS float)) AS TongTien FROM dbo.CompanyContract C WITH(NOLOCK), dbo.ContractMember M WITH(NOLOCK), dbo.DichVuLamThemView V WITH(NOLOCK), ContractMemberView CM WITH(NOLOCK) WHERE C.CompanyContractGUID = M.CompanyContractGUID AND V.ContractMemberGUID = M.ContractMemberGUID AND M.Status = 0 AND V.Status = 0 AND V.ServiceStatus = 0 AND C.CompanyContractGUID = '{0}' AND V.ContractMemberGUID = CM.ContractMemberGUID AND CM.Archived = 'False'", hopDongGUID);
                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToDouble(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetDichVuKhamTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT PV.FullName, PV.DobStr, PV.GenderAsStr, SUM(SH.Price) AS TongTien FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK) WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND NV.PatientGUID = SH.PatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'False' AND ((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR (HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) GROUP BY PV.FullName, PV.DobStr, PV.GenderAsStr ORDER BY PV.FullName", hopDongGUID);
                result = ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetTongTienDichVuKhamTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM(SH.Price) AS TongTien FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK) WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.PatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'False' AND ((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR (HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate))", hopDongGUID);
                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToDouble(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetDichVuKhamChuyenNhuong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT PV.FullName AS NguoiChuyenNhuong, PV2.FullName AS NguoiNhanChuyenNhuong, S.[Name], SH.Price, SH.Discount, CAST((SH.Price - (SH.Price * SH.Discount)/100) AS float) AS ThanhTien FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK), PatientView PV2 WITH(NOLOCK) WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.RootPatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'True' AND ((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR (HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) AND SH.PatientGUID = PV2.PatientGUID ORDER BY PV.FullName", hopDongGUID);
                result = ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetTongTienKhamChuyenNhuong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM(CAST((SH.Price - (SH.Price * SH.Discount)/100) AS float)) AS TongTien FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK) WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.RootPatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'True' AND ((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR (HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate))", hopDongGUID);
                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToDouble(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetTongTienThuTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM(CT.ThanhTien) AS TongTien FROM PhieuThuHopDong PT WITH(NOLOCK), ChiTietPhieuThuHopDong CT WITH(NOLOCK) WHERE PT.PhieuThuHopDongGUID = CT.PhieuThuHopDongGUID AND PT.HopDongGUID = '{0}' AND PT.Status = 0 AND CT.Status = 0 AND PT.ChuaThuTien = 'False'", hopDongGUID);
                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToDouble(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetPhieuThuTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT PT.*, CT.* FROM PhieuThuHopDong PT WITH(NOLOCK), ChiTietPhieuThuHopDong CT WITH(NOLOCK) WHERE PT.PhieuThuHopDongGUID = CT.PhieuThuHopDongGUID AND PT.HopDongGUID = '{0}' AND PT.Status = 0 AND CT.Status = 0 AND PT.ChuaThuTien = 'False' ORDER BY NgayThu", hopDongGUID);
                result = ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetCongNoTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                result = GetTongTienThuTheoHopDong(hopDongGUID);
                if (!result.IsOK) return result;
                double tongTienThu = Convert.ToDouble(result.QueryResult);

                result = GetTongTienDichVuKhamTheoHopDong(hopDongGUID);
                if (!result.IsOK) return result;
                double tongTien = Convert.ToDouble(result.QueryResult);

                result = GetTongTienKhamChuyenNhuong(hopDongGUID);
                if (!result.IsOK) return result;
                tongTien += Convert.ToDouble(result.QueryResult);

                result = GetTongTienDichVuLamThem(hopDongGUID);
                if (!result.IsOK) return result;
                tongTien += Convert.ToDouble(result.QueryResult);

                result.QueryResult = tongTien - tongTienThu;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result CapNhatTrangThaiPhieuThu(string phieuThuHopDongGUID, bool daXuatHD, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    PhieuThuHopDong pthd = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(p => p.PhieuThuHopDongGUID.ToString() == phieuThuHopDongGUID);
                    if (pthd != null)
                    {
                        pthd.UpdatedDate = DateTime.Now;
                        pthd.UpdatedBy = Guid.Parse(Global.UserGUID);
                        pthd.ChuaThuTien = !daThuTien;
                        pthd.IsExported = daXuatHD;

                        string desc = string.Format("Phiếu thu hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Tên khách hàng: '{4}', Công ty: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}', Đã xuất HĐ: '{9}'\n",
                            pthd.PhieuThuHopDongGUID.ToString(), pthd.CompanyContract.CompanyContractGUID, pthd.MaPhieuThuHopDong, pthd.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            pthd.TenNguoiNop, pthd.TenCongTy, pthd.DiaChi, pthd.Notes, !pthd.ChuaThuTien, pthd.IsExported);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa trạng thái phiếu thu hợp đồng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }

                    tnx.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }
    }
}
