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
    public class SieuAmBus : BusBase
    {
        public static Result GetLoaiSieuAmList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM LoaiSieuAm WITH(NOLOCK) WHERE Status={0} ORDER BY ThuTu", (byte)Status.Actived);
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

        public static Result GetLoaiSieuAmList(bool isNam)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<LoaiSieuAm> loaiSieuAmList = null;
                if (isNam)
                {
                    loaiSieuAmList = (from s in db.LoaiSieuAms
                                      join m in db.MauBaoCaos on s.LoaiSieuAmGUID equals m.LoaiSieuAmGUID
                                      where (m.DoiTuong == (byte)DoiTuong.Chung || m.DoiTuong == (byte)DoiTuong.Nam) &&
                                      m.Status == (byte)Status.Actived  && s.Status == (byte)Status.Actived
                                      orderby s.ThuTu ascending
                                      select s).ToList();
                }
                else
                {
                    loaiSieuAmList = (from s in db.LoaiSieuAms
                                      join m in db.MauBaoCaos on s.LoaiSieuAmGUID equals m.LoaiSieuAmGUID
                                      where (m.DoiTuong == (byte)DoiTuong.Chung || m.DoiTuong == (byte)DoiTuong.Nu) &&
                                      m.Status == (byte)Status.Actived && s.Status == (byte)Status.Actived
                                      orderby s.ThuTu ascending
                                      select s).ToList();
                }

                result.QueryResult = loaiSieuAmList;                    
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

        public static Result GetMauBaoCaoList(string loaiSieuAmGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<MauBaoCao> mauBaoCaoList = (from m in db.MauBaoCaos
                                                 where m.LoaiSieuAmGUID.ToString() == loaiSieuAmGUID &&
                                                 m.Status == (byte)Status.Actived
                                                 orderby m.DoiTuong ascending
                                                 select m).ToList();

                result.QueryResult = mauBaoCaoList;
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

        public static Result GetMauBaoCao(string loaiSieuAmGUID, bool isNam)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                MauBaoCao mauBaoCao = null;
                if (isNam)
                {
                    mauBaoCao = (from m in db.MauBaoCaos
                                 where m.LoaiSieuAmGUID.ToString() == loaiSieuAmGUID && m.Status == (byte)Status.Actived &&
                                 (m.DoiTuong == (byte)DoiTuong.Chung || m.DoiTuong == (byte)DoiTuong.Nam)
                                 select m).FirstOrDefault();
                }
                else
                {
                    mauBaoCao = (from m in db.MauBaoCaos
                                 where m.LoaiSieuAmGUID.ToString() == loaiSieuAmGUID && m.Status == (byte)Status.Actived &&
                                 (m.DoiTuong == (byte)DoiTuong.Chung || m.DoiTuong == (byte)DoiTuong.Nu)
                                 select m).FirstOrDefault();
                }

                result.QueryResult = mauBaoCao;
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

        public static Result CheckTenSieuAmExist(string loaiSieuAmGUID, string tenSieuAm)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoaiSieuAm loaiSieuAm = null;
                if (loaiSieuAmGUID == null || loaiSieuAmGUID == string.Empty)
                    loaiSieuAm = db.LoaiSieuAms.FirstOrDefault<LoaiSieuAm>(s => s.TenSieuAm.ToLower() == tenSieuAm.ToLower());
                else
                    loaiSieuAm = db.LoaiSieuAms.FirstOrDefault<LoaiSieuAm>(s => s.TenSieuAm.ToLower() == tenSieuAm.ToLower() &&
                                                                s.LoaiSieuAmGUID.ToString() != loaiSieuAmGUID);

                if (loaiSieuAm == null)
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

        public static Result DeleteLoaiSieuAm(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        LoaiSieuAm l = db.LoaiSieuAms.SingleOrDefault<LoaiSieuAm>(ss => ss.LoaiSieuAmGUID.ToString() == key);
                        if (l != null)
                        {
                            l.DeletedDate = DateTime.Now;
                            l.DeletedBy = Guid.Parse(Global.UserGUID);
                            l.Status = (byte)Status.Deactived;
                            
                            desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}', In trang 2: '{3}'\n", 
                                l.LoaiSieuAmGUID.ToString(), l.TenSieuAm, l.ThuTu, l.InTrang2);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin loại siêu âm";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertLoaiSieuAm(LoaiSieuAm loaiSieuAm, List<MauBaoCao> mauBaoCaoList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (loaiSieuAm.LoaiSieuAmGUID == null || loaiSieuAm.LoaiSieuAmGUID == Guid.Empty)
                    {
                        loaiSieuAm.LoaiSieuAmGUID = Guid.NewGuid();
                        db.LoaiSieuAms.InsertOnSubmit(loaiSieuAm);
                        db.SubmitChanges();

                        foreach (var mauBaoCao in mauBaoCaoList)
                        {
                            mauBaoCao.MauBaoCaoGUID = Guid.NewGuid();
                            mauBaoCao.LoaiSieuAmGUID = loaiSieuAm.LoaiSieuAmGUID;
                            mauBaoCao.CreatedDate = DateTime.Now;
                            mauBaoCao.CreatedBy = Guid.Parse(Global.UserGUID);
                            db.MauBaoCaos.InsertOnSubmit(mauBaoCao);
                        }

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}', In trang 2: '{3}'", 
                            loaiSieuAm.LoaiSieuAmGUID.ToString(), loaiSieuAm.TenSieuAm, loaiSieuAm.ThuTu, loaiSieuAm.InTrang2);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin loại siêu âm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LoaiSieuAm lsa = db.LoaiSieuAms.SingleOrDefault<LoaiSieuAm>(s => s.LoaiSieuAmGUID.ToString() == loaiSieuAm.LoaiSieuAmGUID.ToString());
                        if (lsa != null)
                        {
                            lsa.TenSieuAm = loaiSieuAm.TenSieuAm;
                            lsa.ThuTu = loaiSieuAm.ThuTu;
                            lsa.InTrang2 = loaiSieuAm.InTrang2;
                            lsa.Path = loaiSieuAm.Path;
                            lsa.UpdatedDate = loaiSieuAm.UpdatedDate;
                            lsa.UpdatedBy = loaiSieuAm.UpdatedBy;
                            lsa.Status = loaiSieuAm.Status;

                            //Delete mẫu báo cáo
                            var mbcs = lsa.MauBaoCaos;
                            foreach (var mbc in mbcs)
                            {
                                mbc.UpdatedBy = Guid.Parse(Global.UserGUID);
                                mbc.UpdatedDate = DateTime.Now;
                                mbc.Status = (byte)Status.Deactived;
                            }

                            //Update mẫu báo cáo
                            foreach (var mbc in mauBaoCaoList)
                            {
                                MauBaoCao mauBaoCao = lsa.MauBaoCaos.Where(m => m.DoiTuong == mbc.DoiTuong).FirstOrDefault();
                                if (mauBaoCao == null)
                                {
                                    mbc.MauBaoCaoGUID = Guid.NewGuid();
                                    mbc.LoaiSieuAmGUID = loaiSieuAm.LoaiSieuAmGUID;
                                    mbc.CreatedDate = DateTime.Now;
                                    mbc.CreatedBy = Guid.Parse(Global.UserGUID);
                                    db.MauBaoCaos.InsertOnSubmit(mbc);
                                }
                                else
                                {
                                    mauBaoCao.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    mauBaoCao.UpdatedDate = DateTime.Now;
                                    mauBaoCao.Template = mbc.Template;
                                    mauBaoCao.Status = (byte)Status.Actived;
                                }
                            }

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}', In trang 2: '{3}'",
                            lsa.LoaiSieuAmGUID.ToString(), lsa.TenSieuAm, lsa.ThuTu, lsa.InTrang2);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin loại siêu âm";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

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

        public static Result GetKetQuaSieuAmList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                    Global.StaffType != StaffType.BacSiPhuKhoa)
                    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaSieuAmView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgaySieuAm BETWEEN '{1}' AND '{2}' AND Status = {3} AND LoaiSieuAmStatus = {3} AND PatientArchived = 'False' AND BacSiSieuAmArchived = 'False' ORDER BY NgaySieuAm DESC",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                else
                    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaSieuAmView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgaySieuAm BETWEEN '{1}' AND '{2}' AND Status = {3}  AND LoaiSieuAmStatus = {3} AND PatientArchived = 'False' AND BacSiSieuAmArchived = 'False' AND BacSiSieuAmGUID = '{4}' ORDER BY NgaySieuAm DESC",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

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

        public static Result DeleteKetQuaSieuAm(List<String> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        KetQuaSieuAm kqsa = db.KetQuaSieuAms.SingleOrDefault<KetQuaSieuAm>(k => k.KetQuaSieuAmGUID.ToString() == key);
                        if (kqsa != null)
                        {
                            kqsa.DeletedDate = DateTime.Now;
                            kqsa.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqsa.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày siêu âm: '{1}', Bệnh nhân: '{2}', Bác sĩ siêu âm: '{3}', Bác sĩ chỉ định: '{4}', Loại siêu âm: '{5}'\n",
                                kqsa.KetQuaSieuAmGUID.ToString(), kqsa.NgaySieuAm.Value.ToString("dd/MM/yyyy HH:mm:ss"), kqsa.Patient.Contact.FullName,
                                kqsa.BacSiSieuAmGUID.ToString(), kqsa.BacSiChiDinhGUID.ToString(), kqsa.LoaiSieuAm.TenSieuAm);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa kết quả siêu âm";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertKetQuaSieuAm(KetQuaSieuAm ketQuaSieuAm)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (ketQuaSieuAm.KetQuaSieuAmGUID == null || ketQuaSieuAm.KetQuaSieuAmGUID == Guid.Empty)
                    {
                        ketQuaSieuAm.KetQuaSieuAmGUID = Guid.NewGuid();
                        db.KetQuaSieuAms.InsertOnSubmit(ketQuaSieuAm);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày siêu âm: '{1}', Bệnh nhân: '{2}', Bác sĩ siêu âm: '{3}', Bác sĩ chỉ định: '{4}', Loại siêu âm: '{5}'",
                                ketQuaSieuAm.KetQuaSieuAmGUID.ToString(), ketQuaSieuAm.NgaySieuAm.Value.ToString("dd/MM/yyyy HH:mm:ss"), ketQuaSieuAm.Patient.Contact.FullName,
                                ketQuaSieuAm.BacSiSieuAmGUID.ToString(), ketQuaSieuAm.BacSiChiDinhGUID.ToString(), ketQuaSieuAm.LoaiSieuAm.TenSieuAm);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm kết quả siêu âm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetQuaSieuAm kqsa = db.KetQuaSieuAms.SingleOrDefault<KetQuaSieuAm>(k => k.KetQuaSieuAmGUID == ketQuaSieuAm.KetQuaSieuAmGUID);
                        if (kqsa != null)
                        {
                            kqsa.NgaySieuAm = ketQuaSieuAm.NgaySieuAm;
                            kqsa.PatientGUID = ketQuaSieuAm.PatientGUID;
                            kqsa.BacSiSieuAmGUID = ketQuaSieuAm.BacSiSieuAmGUID;
                            kqsa.BacSiChiDinhGUID = ketQuaSieuAm.BacSiChiDinhGUID;
                            kqsa.LoaiSieuAmGUID = ketQuaSieuAm.LoaiSieuAmGUID;
                            kqsa.LamSang = ketQuaSieuAm.LamSang;
                            kqsa.KetQuaSieuAm1 = ketQuaSieuAm.KetQuaSieuAm1;
                            kqsa.Hinh1 = ketQuaSieuAm.Hinh1;
                            kqsa.Hinh2 = ketQuaSieuAm.Hinh2;
                            kqsa.UpdatedBy = ketQuaSieuAm.UpdatedBy;
                            kqsa.UpdatedDate = ketQuaSieuAm.UpdatedDate;
                            kqsa.Status = ketQuaSieuAm.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày siêu âm: '{1}', Bệnh nhân: '{2}', Bác sĩ siêu âm: '{3}', Bác sĩ chỉ định: '{4}', Loại siêu âm: '{5}'",
                                kqsa.KetQuaSieuAmGUID.ToString(), kqsa.NgaySieuAm.Value.ToString("dd/MM/yyyy HH:mm:ss"), kqsa.Patient.Contact.FullName,
                                kqsa.BacSiSieuAmGUID.ToString(), kqsa.BacSiChiDinhGUID.ToString(), kqsa.LoaiSieuAm.TenSieuAm);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa kết quả siêu âm";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

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
    }
}
