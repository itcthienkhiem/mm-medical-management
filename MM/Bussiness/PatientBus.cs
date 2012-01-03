﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;
using System.Data.SqlClient;

namespace MM.Bussiness
{
    public class PatientBus : BusBase
    {
        public static Result GetPatientList()
        {
            Result result = null;

            try
            {
                string query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM PatientView WHERE Archived = 'False' ORDER BY FirstName, FullName";
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
        public static Result GetDuplicatePatientList()
        {
            Result result = null;

            try
            {
                string query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM PatientView pv1 WHERE pv1.Archived = 'False' And pv1.FullName in (select pv2.FullName from PatientView pv2 where pv2.FullName =pv1.FullName and pv2.Gender = pv1.Gender and pv2.Archived='False' and pv2.ContactGUID != pv1.ContactGUID) ORDER BY FirstName, FullName";
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
        public static Result Merge2Patients(string keepPatientGuid, string mergePatientGuid, string doneByGuid)
        {
            Result result = null;

            try
            {
                string spName = "spMerge2Patients";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@KeepGUID", keepPatientGuid);
                sqlParams.Add(param);
                SqlParameter param2 = new SqlParameter("@MergedGUID", mergePatientGuid);
                sqlParams.Add(param2);
                SqlParameter param3 = new SqlParameter("@DoneByGUID", doneByGuid);
                sqlParams.Add(param3);
                return ExcuteQuery(spName, sqlParams);
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
        public static Result GetPatientCount()
        {
            Result result = new Result();
            try
            {
                string query = "SELECT Count(*) FROM Patient";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
                }
            }
            catch(System.Data.SqlClient.SqlException se)
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

        public static Result GetPatientListNotInCompany()
        {
            Result result = null;

            try
            {
                string query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM PatientView WHERE Archived = 'False' AND PatientGUID NOT IN (SELECT M.PatientGUID FROM CompanyMember M, Company C WHERE M.Status = 0 AND M.CompanyGUID = C.CompanyGUID AND C.Status = 0) ORDER BY FirstName, FullName";
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

        public static Result DeletePatient(List<String> patientKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in patientKeys)
                    {
                        Patient patient = db.Patients.SingleOrDefault<Patient>(p => p.PatientGUID.ToString() == key);
                        if (patient != null)
                        {
                            Contact contact = patient.Contact;
                            if (contact != null)
                            {
                                contact.Archived = true;
                                contact.DateArchived = DateTime.Now;
                                contact.DeletedBy = Guid.Parse(Global.UserGUID);
                                contact.DeletedDate = DateTime.Now;
                            }
                        }

                        db.SubmitChanges();
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

        public static Result InsertPatient(Contact contact, Patient patient, PatientHistory patientHistory)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (contact.ContactGUID == null || contact.ContactGUID == Guid.Empty)
                    {
                        contact.ContactGUID = Guid.NewGuid();
                        contact.FullName = contact.FullName.ToUpper();
                        db.Contacts.InsertOnSubmit(contact);
                        db.SubmitChanges();

                        patient.PatientGUID = Guid.NewGuid();
                        patient.ContactGUID = contact.ContactGUID;
                        db.Patients.InsertOnSubmit(patient);
                        db.SubmitChanges();

                        patientHistory.PatientHistoryGUID = Guid.NewGuid();
                        patientHistory.PatientGUID = patient.PatientGUID;
                        db.PatientHistories.InsertOnSubmit(patientHistory);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Contact ct = db.Contacts.SingleOrDefault<Contact>(cc => cc.ContactGUID.ToString() == contact.ContactGUID.ToString());
                        if (ct != null)
                        {
                            ct.Address = contact.Address;
                            ct.AliasFirstName = contact.AliasFirstName;
                            ct.AliasSurName = contact.AliasSurName;
                            ct.Archived = contact.Archived;
                            ct.City = contact.City;
                            ct.CreatedBy = contact.CreatedBy;
                            ct.CreatedDate = contact.CreatedDate;
                            ct.DateArchived = contact.DateArchived;
                            ct.DeletedBy = contact.DeletedBy;
                            ct.DeletedDate = contact.DeletedDate;
                            ct.District = contact.District;
                            ct.Dob = contact.Dob;
                            ct.DobStr = contact.DobStr;
                            ct.Email = contact.Email;
                            ct.FAX = contact.FAX;
                            ct.FullName = contact.FullName.ToUpper();
                            ct.FirstName = contact.FirstName;
                            ct.Gender = contact.Gender;
                            ct.HomePhone = contact.HomePhone;
                            ct.IdentityCard = contact.IdentityCard;
                            ct.KnownAs = contact.KnownAs;
                            ct.MiddleName = contact.MiddleName;
                            ct.Mobile = contact.Mobile;
                            ct.Note = contact.Note;
                            ct.Occupation = contact.Occupation;
                            ct.CompanyName = contact.CompanyName;
                            ct.PreferredName = contact.PreferredName;
                            ct.SurName = contact.SurName;
                            ct.Title = contact.Title;
                            ct.UpdatedBy = contact.UpdatedBy;
                            ct.UpdatedDate = contact.UpdatedDate;
                            ct.Ward = contact.Ward;
                            ct.WorkPhone = contact.WorkPhone;

                            Patient p = db.Patients.SingleOrDefault<Patient>(pp => pp.PatientGUID.ToString() == patient.PatientGUID.ToString());
                            if (p != null)
                            {
                                p.FileNum = patient.FileNum;
                                p.BarCode = patient.BarCode;
                                p.Picture = patient.Picture;
                                p.HearFrom = patient.HearFrom;
                                p.Salutation = patient.Salutation;
                                p.LastSeenDate = patient.LastSeenDate;
                                p.LastSeenDocGUID = patient.LastSeenDocGUID;
                                p.DateDeceased = patient.DateDeceased;
                                p.LastVisitGUID = patient.LastVisitGUID;
                            }

                            PatientHistory pHistory = db.PatientHistories.SingleOrDefault<PatientHistory>(pp => pp.PatientHistoryGUID.ToString() == patientHistory.PatientHistoryGUID.ToString());
                            if (pHistory != null)
                            {
                                pHistory.Benh_Gi = patientHistory.Benh_Gi;
                                pHistory.Benh_Khac = patientHistory.Benh_Khac;
                                pHistory.Benh_Lao = patientHistory.Benh_Lao;
                                pHistory.Benh_Tim_Mach = patientHistory.Benh_Tim_Mach;
                                pHistory.Chich_Ngua_Cum = patientHistory.Chich_Ngua_Cum;
                                pHistory.Chich_Ngua_Uon_Van = patientHistory.Chich_Ngua_Uon_Van;
                                pHistory.Chich_Ngua_Viem_Gan_B = patientHistory.Chich_Ngua_Viem_Gan_B;
                                pHistory.Co_Quan_Ung_Thu = patientHistory.Co_Quan_Ung_Thu;
                                pHistory.Dai_Duong_Dang_Dieu_Tri = patientHistory.Dai_Duong_Dang_Dieu_Tri;
                                pHistory.Dai_Thao_Duong = patientHistory.Dai_Thao_Duong;
                                pHistory.Dang_Co_Thai = patientHistory.Dang_Co_Thai;
                                pHistory.Di_Ung_Thuoc = patientHistory.Di_Ung_Thuoc;
                                pHistory.Dong_Kinh = patientHistory.Dong_Kinh;
                                pHistory.Dot_Quy = patientHistory.Dot_Quy;
                                pHistory.Hen_Suyen = patientHistory.Hen_Suyen;
                                pHistory.Hut_Thuoc = patientHistory.Hut_Thuoc;
                                pHistory.Thuoc_Dang_Dung = patientHistory.Thuoc_Dang_Dung;
                                pHistory.Thuoc_Di_Ung = patientHistory.Thuoc_Di_Ung;
                                pHistory.Tinh_Trang_Gia_Dinh = patientHistory.Tinh_Trang_Gia_Dinh;
                                pHistory.Ung_Thu = patientHistory.Ung_Thu;
                                pHistory.Uong_Ruou = patientHistory.Uong_Ruou;
                                pHistory.Viem_Gan_B = patientHistory.Viem_Gan_B;
                                pHistory.Viem_Gan_C = patientHistory.Viem_Gan_C;
                                pHistory.Viem_Gan_Dang_Dieu_Tri = patientHistory.Viem_Gan_Dang_Dieu_Tri;
                            }

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
        public static Result CheckPatientExist(string fullname, string dobStr, byte gender, string source)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Contact ct = null;
                ct = db.Contacts.SingleOrDefault<Contact>(c => c.FullName.Trim().ToLower() == fullname.Trim().ToLower() &&
                                                                c.DobStr.Trim().ToLower() == dobStr.Trim().ToLower() && 
                                                                c.Gender == gender && c.Source == source);
                    

                if (ct == null)
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
        public static Result CheckPatientExistFileNum(string patientGUID, string fileNum)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Patient patient = null;
                if (patientGUID == null || patientGUID == string.Empty)
                    patient = db.Patients.SingleOrDefault<Patient>(p => p.FileNum.Trim().ToLower() == fileNum.Trim().ToLower() &&
                                                                    p.Contact.Archived == false);
                else
                    patient = db.Patients.SingleOrDefault<Patient>(p => p.FileNum.Trim().ToLower() == fileNum.Trim().ToLower() &&
                                                                p.PatientGUID.ToString() != patientGUID &&
                                                                    p.Contact.Archived == false);

                if (patient == null)
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
    }
}
