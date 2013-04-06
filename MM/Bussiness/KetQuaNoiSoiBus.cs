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
    public class KetQuaNoiSoiBus : BusBase
    {
        public static Result GetKetQuaNoiSoiList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                //    Global.StaffType != StaffType.BacSiPhuKhoa)
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaNoiSoiView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND BSCDArchived = 'False' AND BSNSArchived = 'False' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                //else
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaNoiSoiView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND BSCDArchived = 'False' AND BSNSArchived = 'False' AND BacSiSoi = '{4}' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

                query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaNoiSoiView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND BSCDArchived = 'False' AND BSNSArchived = 'False' ORDER BY NgayKham DESC",
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

        public static Result GetKetQuaNoiSoiList2(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM KetQuaNoiSoiView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3}",
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

        public static Result GetKetQuaNoiSoiCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM KetQuaNoiSoi WITH(NOLOCK)";
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

        public static Result CheckSoPhieuExistCode(string ketQuaNoiSoiGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                KetQuaNoiSoi kqns = null;
                if (ketQuaNoiSoiGUID == null || ketQuaNoiSoiGUID == string.Empty)
                    kqns = db.KetQuaNoiSois.SingleOrDefault<KetQuaNoiSoi>(k => k.SoPhieu.ToLower() == code.ToLower());
                else
                    kqns = db.KetQuaNoiSois.SingleOrDefault<KetQuaNoiSoi>(k => k.SoPhieu.ToLower() == code.ToLower() &&
                                                                k.KetQuaNoiSoiGUID.ToString() != ketQuaNoiSoiGUID);

                if (kqns == null)
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

        public static Result DeleteKetQuaNoiSoi(List<String> ketQuaNoiSoiKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in ketQuaNoiSoiKeys)
                    {
                        KetQuaNoiSoi kqns = db.KetQuaNoiSois.SingleOrDefault<KetQuaNoiSoi>(k => k.KetQuaNoiSoiGUID.ToString() == key);
                        if (kqns != null)
                        {
                            kqns.DeletedDate = DateTime.Now;
                            kqns.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqns.Status = (byte)Status.Deactived;

                            string tenBSCD = string.Empty;
                            if (kqns.BacSiChiDinh.HasValue)
                            {
                                var bscd = db.DocStaffViews.SingleOrDefault(d => d.DocStaffGUID == kqns.BacSiChiDinh.Value);
                                if (bscd != null) tenBSCD = bscd.FullName;
                            }

                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}'\n",
                                kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"), kqns.Patient.Contact.FullName,
                                kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName, kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin khám nội soi";
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

        public static Result InsertKetQuaNoiSoi(KetQuaNoiSoi ketQuaNoiSoi)
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
                    if (ketQuaNoiSoi.KetQuaNoiSoiGUID == null || ketQuaNoiSoi.KetQuaNoiSoiGUID == Guid.Empty)
                    {
                        ketQuaNoiSoi.KetQuaNoiSoiGUID = Guid.NewGuid();
                        db.KetQuaNoiSois.InsertOnSubmit(ketQuaNoiSoi);
                        db.SubmitChanges();

                        //Tracking
                        string tenBSCD = string.Empty;
                        if (ketQuaNoiSoi.BacSiChiDinh.HasValue)
                        {
                            var bscd = db.DocStaffViews.SingleOrDefault(d => d.DocStaffGUID == ketQuaNoiSoi.BacSiChiDinh.Value);
                            if (bscd != null) tenBSCD = bscd.FullName;
                        }

                        if ((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi == LoaiNoiSoi.Tai)
                        {
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai trái: '{9}', Ống tai phải: '{10}', Màng nhĩ trái: '{11}', Màng nhĩ phải: '{12}', Cán búa trái: '{13}', Cán búa phải: '{14}', Hòm nhĩ trái: '{15}', Hòm nhĩ phải: '{16}', Valsava trái: '{17}', Valsava phải: '{18}'\n",
                                ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString(), ketQuaNoiSoi.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"), 
                                ketQuaNoiSoi.Patient.Contact.FullName, ketQuaNoiSoi.LyDoKham, tenBSCD, ketQuaNoiSoi.DocStaff.Contact.FullName, 
                                ketQuaNoiSoi.KetLuan, ketQuaNoiSoi.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi), ketQuaNoiSoi.OngTaiTrai,
                                ketQuaNoiSoi.OngTaiPhai, ketQuaNoiSoi.MangNhiTrai, ketQuaNoiSoi.MangNhiPhai, ketQuaNoiSoi.CanBuaTrai, ketQuaNoiSoi.CanBuaPhai,
                                ketQuaNoiSoi.HomNhiTrai, ketQuaNoiSoi.HomNhiPhai, ketQuaNoiSoi.ValsavaTrai, ketQuaNoiSoi.ValsavaPhai);
                        }
                        else if ((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi == LoaiNoiSoi.Mui)
                        {
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Niêm mạc trái: '{9}', Niêm mạc phải: '{10}', Vách ngăn trái: '{11}', Vách ngăn phải: '{12}', Khe trên trái: '{13}', Khe trên phải: '{14}', Khe giữa trái: '{15}', Khe giữa phải: '{16}', Cuốn giữa trái: '{17}', Cuốn giữa phải: '{18}', Cuốn dưới trái: '{19}', Cuốn dưới phải: '{20}', Mõm móc trái: '{21}', Mõm móc phải: '{22}', Bóng sàng trái: '{23}', Bóng sàng phải: '{24}', Vòm trái: '{25}', Vòm phải: '{26}'\n",
                               ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString(), ketQuaNoiSoi.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                               ketQuaNoiSoi.Patient.Contact.FullName, ketQuaNoiSoi.LyDoKham, tenBSCD, ketQuaNoiSoi.DocStaff.Contact.FullName,
                               ketQuaNoiSoi.KetLuan, ketQuaNoiSoi.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi), ketQuaNoiSoi.NiemMacTrai,
                               ketQuaNoiSoi.NiemMacPhai, ketQuaNoiSoi.VachNganTrai, ketQuaNoiSoi.VachNganPhai, ketQuaNoiSoi.KheTrenTrai, ketQuaNoiSoi.KheTrenPhai,
                               ketQuaNoiSoi.KheGiuaTrai, ketQuaNoiSoi.KheGiuaPhai, ketQuaNoiSoi.CuonGiuaTrai, ketQuaNoiSoi.CuonGiuaPhai, ketQuaNoiSoi.CuonDuoiTrai,
                               ketQuaNoiSoi.CuonDuoiPhai, ketQuaNoiSoi.MomMocTrai, ketQuaNoiSoi.MomMocPhai, ketQuaNoiSoi.BongSangTrai, ketQuaNoiSoi.BongSangPhai,
                               ketQuaNoiSoi.VomTrai, ketQuaNoiSoi.VomPhai);
                        }
                        else if ((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi == LoaiNoiSoi.Hong_ThanhQuan)
                        {
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Amydale: '{9}', Xoang lê: '{10}', Miệng thực quản: '{11}', Sụn phểu: '{12}', Dây thanh: '{13}', Băng thanh thất: '{14}'\n",
                               ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString(), ketQuaNoiSoi.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                               ketQuaNoiSoi.Patient.Contact.FullName, ketQuaNoiSoi.LyDoKham, tenBSCD, ketQuaNoiSoi.DocStaff.Contact.FullName,
                               ketQuaNoiSoi.KetLuan, ketQuaNoiSoi.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi), 
                               ketQuaNoiSoi.Amydale, ketQuaNoiSoi.XoangLe, ketQuaNoiSoi.MiengThucQuan, ketQuaNoiSoi.SunPheu, ketQuaNoiSoi.DayThanh, ketQuaNoiSoi.BangThanhThat);
                        }
                        else if ((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi == LoaiNoiSoi.TaiMuiHong)
                        {
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai ngoài: '{9}', Màng nhĩ: '{10}', Niêm mạc mũi: '{11}', Vách ngăn: '{12}', Khe trên: '{13}', Khe giữa: '{14}', Mõm móc - Bóng sàng: '{15}', Vòm: '{16}', Amydale: '{17}', Thanh quản: '{18}'\n",
                               ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString(), ketQuaNoiSoi.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                               ketQuaNoiSoi.Patient.Contact.FullName, ketQuaNoiSoi.LyDoKham, tenBSCD, ketQuaNoiSoi.DocStaff.Contact.FullName,
                               ketQuaNoiSoi.KetLuan, ketQuaNoiSoi.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi),
                               ketQuaNoiSoi.OngTaiNgoai, ketQuaNoiSoi.MangNhi, ketQuaNoiSoi.NiemMac, ketQuaNoiSoi.VachNgan, ketQuaNoiSoi.KheTren,
                               ketQuaNoiSoi.KheGiua, ketQuaNoiSoi.MomMoc_BongSang, ketQuaNoiSoi.Vom, ketQuaNoiSoi.Amydale, ketQuaNoiSoi.ThanhQuan);
                        }
                        else if ((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi == LoaiNoiSoi.TongQuat)
                        {
                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai trái: '{9}', Ống tai phải: '{10}', Màng nhĩ trái: '{11}', Màng nhĩ phải: '{12}', Cán búa trái: '{13}', Cán búa phải: '{14}', Hòm nhĩ trái: '{15}', Hòm nhĩ phải: '{16}'\n",
                               ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString(), ketQuaNoiSoi.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                               ketQuaNoiSoi.Patient.Contact.FullName, ketQuaNoiSoi.LyDoKham, tenBSCD, ketQuaNoiSoi.DocStaff.Contact.FullName,
                               ketQuaNoiSoi.KetLuan, ketQuaNoiSoi.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)ketQuaNoiSoi.LoaiNoiSoi), ketQuaNoiSoi.OngTaiTrai,
                               ketQuaNoiSoi.OngTaiPhai, ketQuaNoiSoi.MangNhiTrai, ketQuaNoiSoi.MangNhiPhai, ketQuaNoiSoi.CanBuaTrai, ketQuaNoiSoi.CanBuaPhai,
                               ketQuaNoiSoi.HomNhiTrai, ketQuaNoiSoi.HomNhiPhai);
                        }

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin khám nội soi";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetQuaNoiSoi kqns = db.KetQuaNoiSois.SingleOrDefault<KetQuaNoiSoi>(k => k.KetQuaNoiSoiGUID == ketQuaNoiSoi.KetQuaNoiSoiGUID);
                        if (kqns != null)
                        {
                            kqns.NgayKham = ketQuaNoiSoi.NgayKham;
                            kqns.PatientGUID = ketQuaNoiSoi.PatientGUID;
                            kqns.SoPhieu = ketQuaNoiSoi.SoPhieu;
                            kqns.LyDoKham = ketQuaNoiSoi.LyDoKham;
                            kqns.BacSiChiDinh = ketQuaNoiSoi.BacSiChiDinh;
                            kqns.BacSiSoi = ketQuaNoiSoi.BacSiSoi;
                            kqns.KetLuan = ketQuaNoiSoi.KetLuan;
                            kqns.DeNghi = ketQuaNoiSoi.DeNghi;
                            kqns.LoaiNoiSoi = ketQuaNoiSoi.LoaiNoiSoi;
                            kqns.OngTaiTrai = ketQuaNoiSoi.OngTaiTrai;
                            kqns.OngTaiPhai = ketQuaNoiSoi.OngTaiPhai;
                            kqns.MangNhiTrai = ketQuaNoiSoi.MangNhiTrai;
                            kqns.MangNhiPhai = ketQuaNoiSoi.MangNhiPhai;
                            kqns.CanBuaTrai = ketQuaNoiSoi.CanBuaTrai;
                            kqns.CanBuaPhai = ketQuaNoiSoi.CanBuaPhai;
                            kqns.HomNhiTrai = ketQuaNoiSoi.HomNhiTrai;
                            kqns.HomNhiPhai = ketQuaNoiSoi.HomNhiPhai;
                            kqns.ValsavaTrai = ketQuaNoiSoi.ValsavaTrai;
                            kqns.ValsavaPhai = ketQuaNoiSoi.ValsavaPhai;
                            kqns.NiemMacTrai = ketQuaNoiSoi.NiemMacTrai;
                            kqns.NiemMacPhai = ketQuaNoiSoi.NiemMacPhai;
                            kqns.VachNganTrai = ketQuaNoiSoi.VachNganTrai;
                            kqns.VachNganPhai = ketQuaNoiSoi.VachNganPhai;
                            kqns.KheTrenTrai = ketQuaNoiSoi.KheTrenTrai;
                            kqns.KheTrenPhai = ketQuaNoiSoi.KheTrenPhai;
                            kqns.KheGiuaTrai = ketQuaNoiSoi.KheGiuaTrai;
                            kqns.KheGiuaPhai = ketQuaNoiSoi.KheGiuaPhai;
                            kqns.CuonDuoiTrai = ketQuaNoiSoi.CuonDuoiTrai;
                            kqns.CuonDuoiPhai = ketQuaNoiSoi.CuonDuoiPhai;
                            kqns.CuonGiuaTrai = ketQuaNoiSoi.CuonGiuaTrai;
                            kqns.CuonGiuaPhai = ketQuaNoiSoi.CuonGiuaPhai;
                            kqns.MomMocTrai = ketQuaNoiSoi.MomMocTrai;
                            kqns.MomMocPhai = ketQuaNoiSoi.MomMocPhai;
                            kqns.BongSangTrai = ketQuaNoiSoi.BongSangTrai;
                            kqns.BongSangPhai = ketQuaNoiSoi.BongSangPhai;
                            kqns.VomTrai = ketQuaNoiSoi.VomTrai;
                            kqns.VomPhai = ketQuaNoiSoi.VomPhai;
                            kqns.Amydale = ketQuaNoiSoi.Amydale;
                            kqns.XoangLe = ketQuaNoiSoi.XoangLe;
                            kqns.MiengThucQuan = ketQuaNoiSoi.MiengThucQuan;
                            kqns.SunPheu = ketQuaNoiSoi.SunPheu;
                            kqns.DayThanh = ketQuaNoiSoi.DayThanh;
                            kqns.BangThanhThat = ketQuaNoiSoi.BangThanhThat;
                            kqns.OngTaiNgoai = ketQuaNoiSoi.OngTaiNgoai;
                            kqns.MangNhi = ketQuaNoiSoi.MangNhi;
                            kqns.NiemMac = ketQuaNoiSoi.NiemMac;
                            kqns.VachNgan = ketQuaNoiSoi.VachNgan;
                            kqns.KheTren = ketQuaNoiSoi.KheTren;
                            kqns.KheGiua = ketQuaNoiSoi.KheGiua;
                            kqns.MomMoc_BongSang = ketQuaNoiSoi.MomMoc_BongSang;
                            kqns.Vom = ketQuaNoiSoi.Vom;
                            kqns.ThanhQuan = ketQuaNoiSoi.ThanhQuan;
                            kqns.Hinh1 = ketQuaNoiSoi.Hinh1;
                            kqns.Hinh2 = ketQuaNoiSoi.Hinh2;
                            kqns.Hinh3 = ketQuaNoiSoi.Hinh3;
                            kqns.Hinh4 = ketQuaNoiSoi.Hinh4;
                            kqns.CreatedBy = ketQuaNoiSoi.CreatedBy;
                            kqns.CreatedDate = ketQuaNoiSoi.CreatedDate;
                            kqns.DeletedBy = ketQuaNoiSoi.DeletedBy;
                            kqns.DeletedDate = ketQuaNoiSoi.DeletedDate;
                            kqns.UpdatedBy = ketQuaNoiSoi.UpdatedBy;
                            kqns.UpdatedDate = ketQuaNoiSoi.UpdatedDate;
                            kqns.Status = ketQuaNoiSoi.Status;
                            db.SubmitChanges();

                            //Tracking
                            string tenBSCD = string.Empty;
                            if (kqns.BacSiChiDinh.HasValue)
                            {
                                var bscd = db.DocStaffViews.SingleOrDefault(d => d.DocStaffGUID == kqns.BacSiChiDinh.Value);
                                if (bscd != null) tenBSCD = bscd.FullName;
                            }

                            if ((LoaiNoiSoi)kqns.LoaiNoiSoi == LoaiNoiSoi.Tai)
                            {
                                desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai trái: '{9}', Ống tai phải: '{10}', Màng nhĩ trái: '{11}', Màng nhĩ phải: '{12}', Cán búa trái: '{13}', Cán búa phải: '{14}', Hòm nhĩ trái: '{15}', Hòm nhĩ phải: '{16}', Valsava trái: '{17}', Valsava phải: '{18}'\n",
                                    kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                    kqns.Patient.Contact.FullName, kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName,
                                    kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi), kqns.OngTaiTrai,
                                    kqns.OngTaiPhai, kqns.MangNhiTrai, kqns.MangNhiPhai, kqns.CanBuaTrai, kqns.CanBuaPhai,
                                    kqns.HomNhiTrai, kqns.HomNhiPhai, kqns.ValsavaTrai, kqns.ValsavaPhai);
                            }
                            else if ((LoaiNoiSoi)kqns.LoaiNoiSoi == LoaiNoiSoi.Mui)
                            {
                                desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Niêm mạc trái: '{9}', Niêm mạc phải: '{10}', Vách ngăn trái: '{11}', Vách ngăn phải: '{12}', Khe trên trái: '{13}', Khe trên phải: '{14}', Khe giữa trái: '{15}', Khe giữa phải: '{16}', Cuốn giữa trái: '{17}', Cuốn giữa phải: '{18}', Cuốn dưới trái: '{19}', Cuốn dưới phải: '{20}', Mõm móc trái: '{21}', Mõm móc phải: '{22}', Bóng sàng trái: '{23}', Bóng sàng phải: '{24}', Vòm trái: '{25}', Vòm phải: '{26}'\n",
                                   kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                   kqns.Patient.Contact.FullName, kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName,
                                   kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi), kqns.NiemMacTrai,
                                   kqns.NiemMacPhai, kqns.VachNganTrai, kqns.VachNganPhai, kqns.KheTrenTrai, kqns.KheTrenPhai,
                                   kqns.KheGiuaTrai, kqns.KheGiuaPhai, kqns.CuonGiuaTrai, kqns.CuonGiuaPhai, kqns.CuonDuoiTrai,
                                   kqns.CuonDuoiPhai, kqns.MomMocTrai, kqns.MomMocPhai, kqns.BongSangTrai, kqns.BongSangPhai, kqns.VomTrai, kqns.VomPhai);
                            }
                            else if ((LoaiNoiSoi)kqns.LoaiNoiSoi == LoaiNoiSoi.Hong_ThanhQuan)
                            {
                                desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Amydale: '{9}', Xoang lê: '{10}', Miệng thực quản: '{11}', Sụn phểu: '{12}', Dây thanh: '{13}', Băng thanh thất: '{14}'\n",
                                   kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                   kqns.Patient.Contact.FullName, kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName,
                                   kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi),
                                   kqns.Amydale, kqns.XoangLe, kqns.MiengThucQuan, kqns.SunPheu, kqns.DayThanh, kqns.BangThanhThat);
                            }
                            else if ((LoaiNoiSoi)kqns.LoaiNoiSoi == LoaiNoiSoi.TaiMuiHong)
                            {
                                desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai ngoài: '{9}', Màng nhĩ: '{10}', Niêm mạc mũi: '{11}', Vách ngăn: '{12}', Khe trên: '{13}', Khe giữa: '{14}', Mõm móc - Bóng sàng: '{15}', Vòm: '{16}', Amydale: '{17}', Thanh quản: '{18}'\n",
                                   kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                   kqns.Patient.Contact.FullName, kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName,
                                   kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi),
                                   kqns.OngTaiNgoai, kqns.MangNhi, kqns.NiemMac, kqns.VachNgan, kqns.KheTren,
                                   kqns.KheGiua, kqns.MomMoc_BongSang, kqns.Vom, kqns.Amydale, kqns.ThanhQuan);
                            }
                            else if ((LoaiNoiSoi)kqns.LoaiNoiSoi == LoaiNoiSoi.TongQuat)
                            {
                                desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Lý do khám: '{3}', BSCD: '{4}', Bác sĩ soi: '{5}', Kết luận: '{6}', Đề nghị: '{7}', Loại nội soi: '{8}', Ống tai trái: '{9}', Ống tai phải: '{10}', Màng nhĩ trái: '{11}', Màng nhĩ phải: '{12}', Cán búa trái: '{13}', Cán búa phải: '{14}', Hòm nhĩ trái: '{15}', Hòm nhĩ phải: '{16}'\n",
                                   kqns.KetQuaNoiSoiGUID.ToString(), kqns.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                   kqns.Patient.Contact.FullName, kqns.LyDoKham, tenBSCD, kqns.DocStaff.Contact.FullName,
                                   kqns.KetLuan, kqns.DeNghi, Utility.ParseLoaiNoiSoiEnumToName((LoaiNoiSoi)kqns.LoaiNoiSoi), kqns.OngTaiTrai,
                                   kqns.OngTaiPhai, kqns.MangNhiTrai, kqns.MangNhiPhai, kqns.CanBuaTrai, kqns.CanBuaPhai,
                                   kqns.HomNhiTrai, kqns.HomNhiPhai);
                            }

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin khám nội soi";
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
                        string ketQuaNoiSoiGUID = row["KetQuaNoiSoiGUID"].ToString();
                        KetQuaNoiSoi ketQuaNoiSoi = (from s in db.KetQuaNoiSois
                                                     where s.KetQuaNoiSoiGUID.ToString() == ketQuaNoiSoiGUID
                                                     select s).FirstOrDefault();

                        if (ketQuaNoiSoi == null) continue;

                        //Tracking
                        string desc = string.Format("- KetQuaNoiSoiGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (KetQuaNoiSoi)",
                            ketQuaNoiSoiGUID, ketQuaNoiSoi.PatientGUID.ToString(), patientGUID);

                        ketQuaNoiSoi.PatientGUID = Guid.Parse(patientGUID);

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
