using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Linq;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class KetQuaSoiCTCBus : BusBase
    {
        public static Result GetKetQuaSoiCTCList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                //    Global.StaffType != StaffType.BacSiPhuKhoa)
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaSoiCTCView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                //else
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaSoiCTCView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' AND BacSiSoi = '{4}' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

                query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaSoiCTCView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' ORDER BY NgayKham DESC",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result GetKetQuaSoiCTCList2(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM KetQuaSoiCTCView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3}",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result DeleteKetQuaSoiCTC(List<String> ketQuaSoiCTCKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in ketQuaSoiCTCKeys)
                    {
                        KetQuaSoiCTC kqsctc = db.KetQuaSoiCTCs.SingleOrDefault<KetQuaSoiCTC>(k => k.KetQuaSoiCTCGUID.ToString() == key);
                        if (kqsctc != null)
                        {
                            kqsctc.DeletedDate = DateTime.Now;
                            kqsctc.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqsctc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Bác sĩ soi: '{3}', Kết luận: '{4}', Đề nghị: '{5}'\n",
                                kqsctc.KetQuaSoiCTCGUID.ToString(), kqsctc.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"), kqsctc.Patient.Contact.FullName,
                                kqsctc.DocStaff.Contact.FullName, kqsctc.KetLuan, kqsctc.DeNghi);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin khám CTC";
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

        public static Result InsertKetQuaSoiCTC(KetQuaSoiCTC ketQuaSoiCTC)
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
                    if (ketQuaSoiCTC.KetQuaSoiCTCGUID == null || ketQuaSoiCTC.KetQuaSoiCTCGUID == Guid.Empty)
                    {
                        ketQuaSoiCTC.KetQuaSoiCTCGUID = Guid.NewGuid();
                        db.KetQuaSoiCTCs.InsertOnSubmit(ketQuaSoiCTC);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Bác sĩ soi: '{3}', Kết luận: '{4}', Đề nghị: '{5}', Âm hộ: '{6}', Âm đạo: '{7}', CTC: '{8}', Biểu mô lát: '{9}', Mô đệm: '{10}', Ranh giới lát trụ: '{11}', Sau Acid Acetic: '{12}', Sau Lugol: '{13}'\n",
                                ketQuaSoiCTC.KetQuaSoiCTCGUID.ToString(), ketQuaSoiCTC.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                ketQuaSoiCTC.Patient.Contact.FullName, ketQuaSoiCTC.DocStaff.Contact.FullName,
                                ketQuaSoiCTC.KetLuan, ketQuaSoiCTC.DeNghi, ketQuaSoiCTC.AmHo, ketQuaSoiCTC.AmDao, ketQuaSoiCTC.CTC, ketQuaSoiCTC.BieuMoLat, 
                                ketQuaSoiCTC.MoDem, ketQuaSoiCTC.RanhGioiLatTru, ketQuaSoiCTC.SauAcidAcetic, ketQuaSoiCTC.SauLugol);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin khám CTC";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetQuaSoiCTC kqsctc = db.KetQuaSoiCTCs.SingleOrDefault<KetQuaSoiCTC>(k => k.KetQuaSoiCTCGUID == ketQuaSoiCTC.KetQuaSoiCTCGUID);
                        if (kqsctc != null)
                        {
                            kqsctc.NgayKham = ketQuaSoiCTC.NgayKham;
                            kqsctc.PatientGUID = ketQuaSoiCTC.PatientGUID;
                            kqsctc.BacSiSoi = ketQuaSoiCTC.BacSiSoi;
                            kqsctc.KetLuan = ketQuaSoiCTC.KetLuan;
                            kqsctc.DeNghi = ketQuaSoiCTC.DeNghi;
                            kqsctc.Hinh1 = ketQuaSoiCTC.Hinh1;
                            kqsctc.Hinh2 = ketQuaSoiCTC.Hinh2;
                            kqsctc.AmHo = ketQuaSoiCTC.AmHo;
                            kqsctc.AmDao = ketQuaSoiCTC.AmDao;
                            kqsctc.CTC = ketQuaSoiCTC.CTC;
                            kqsctc.BieuMoLat = ketQuaSoiCTC.BieuMoLat;
                            kqsctc.MoDem = ketQuaSoiCTC.MoDem;
                            kqsctc.RanhGioiLatTru = ketQuaSoiCTC.RanhGioiLatTru;
                            kqsctc.SauAcidAcetic = ketQuaSoiCTC.SauAcidAcetic;
                            kqsctc.SauLugol = ketQuaSoiCTC.SauLugol;
                            kqsctc.CreatedBy = ketQuaSoiCTC.CreatedBy;
                            kqsctc.CreatedDate = ketQuaSoiCTC.CreatedDate;
                            kqsctc.DeletedBy = ketQuaSoiCTC.DeletedBy;
                            kqsctc.DeletedDate = ketQuaSoiCTC.DeletedDate;
                            kqsctc.UpdatedBy = ketQuaSoiCTC.UpdatedBy;
                            kqsctc.UpdatedDate = ketQuaSoiCTC.UpdatedDate;
                            kqsctc.Status = ketQuaSoiCTC.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Bác sĩ soi: '{3}', Kết luận: '{4}', Đề nghị: '{5}', Âm hộ: '{6}', Âm đạo: '{7}', CTC: '{8}', Biểu mô lát: '{9}', Mô đệm: '{10}', Ranh giới lát trụ: '{11}', Sau Acid Acetic: '{12}', Sau Lugol: '{13}'\n",
                                kqsctc.KetQuaSoiCTCGUID.ToString(), kqsctc.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                kqsctc.Patient.Contact.FullName, kqsctc.DocStaff.Contact.FullName,
                                kqsctc.KetLuan, kqsctc.DeNghi, kqsctc.AmHo, kqsctc.AmDao, kqsctc.CTC, kqsctc.BieuMoLat,
                                kqsctc.MoDem, kqsctc.RanhGioiLatTru, kqsctc.SauAcidAcetic, kqsctc.SauLugol);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin khám CTC";
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

        public static Result ChuyenBenhAn(string patientGUID, List<DataRow> rows)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in rows)
                    {
                        string ketQuaSoiCTCGUID = row["KetQuaSoiCTCGUID"].ToString();
                        KetQuaSoiCTC ketQuaSoiCTC = (from s in db.KetQuaSoiCTCs
                                                     where s.KetQuaSoiCTCGUID.ToString() == ketQuaSoiCTCGUID
                                                     select s).FirstOrDefault();

                        if (ketQuaSoiCTC == null) continue;

                        //Tracking
                        string desc = string.Format("- KetQuaSoiCTCGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (KetQuaSoiCTC)",
                            ketQuaSoiCTCGUID, ketQuaSoiCTC.PatientGUID.ToString(), patientGUID);

                        ketQuaSoiCTC.PatientGUID = Guid.Parse(patientGUID);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Chuyển bệnh án";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
                    }

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
    }
}
