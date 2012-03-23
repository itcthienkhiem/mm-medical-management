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
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE NgayThu BETWEEN '{0}' AND '{1}' ORDER BY NgayThu DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE TenNguoiNop LIKE N'%{0}%' ORDER BY NgayThu DESC", tenKhacHang);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE Status={0} AND TenNguoiNop LIKE N'%{1}%' ORDER BY NgayThu DESC",
                        (byte)Status.Actived, tenKhacHang);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuHopDong WHERE Status={0} AND TenNguoiNop LIKE N'%{1}%' ORDER BY NgayThu DESC",
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
                string query = string.Format("SELECT * FROM ChiTietPhieuThuHopDong WHERE Status={0} AND PhieuThuHopDongGUID='{1}' ORDER BY DichVu",
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
                string query = "SELECT Count(*) FROM PhieuThuHopDong";
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

                        desc += string.Format("- Phiếu thu hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Tên khách hàng: '{4}', Công ty: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}'\n",
                            ptthd.PhieuThuHopDongGUID.ToString(), ptthd.CompanyContract.CompanyContractGUID, ptthd.MaPhieuThuHopDong, ptthd.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptthd.TenNguoiNop, ptthd.TenCongTy, ptthd.DiaChi, ptthd.Notes, !ptthd.ChuaThuTien);

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

        public static Result GetTongTienDichVuKhamTheoHopDong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM(SH.Price) AS TongTien FROM CompanyContract HD, ContractMember CM, CompanyMember NV, PatientView PV, CompanyCheckList CL, ServiceHistory SH, Services S WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.PatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'False' AND (HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate OR HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)", hopDongGUID);
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

        public static Result GetTongTienKhamChuyenNhuong(string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT SUM(SH.Price) AS TongTien FROM CompanyContract HD, ContractMember CM, CompanyMember NV, PatientView PV, CompanyCheckList CL, ServiceHistory SH, Services S WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.RootPatientGUID AND SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = '{0}' AND SH.IsExported = 'False' AND SH.KhamTuTuc = 'True' AND (HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate OR HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)", hopDongGUID);
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
                string query = string.Format("SELECT SUM(CT.ThanhTien) AS TongTien FROM PhieuThuHopDong PT, ChiTietPhieuThuHopDong CT WHERE PT.PhieuThuHopDongGUID = CT.PhieuThuHopDongGUID AND PT.HopDongGUID = '{0}' AND PT.Status = 0 AND CT.Status = 0", hopDongGUID);
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
    }
}
