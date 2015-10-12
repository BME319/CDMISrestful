using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.DataMethod
{
    public class ModuleInfoMethod
    {
        #region <Ps.BasicInfoDetail>
        //SetData LY 2015-10-10 
        public int PsBasicInfoDetailSetData(DataConnection pclsCache, string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.BasicInfoDetail.SetData(pclsCache.CacheConnectionObject, Patient, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        // 获取需要知道的项目的值 LY 2015-10-10
        public string PsBasicInfoDetailGetValue(DataConnection pclsCache, string UserId, string CategoryCode, string ItemCode, int ItemSeq)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (string)Ps.BasicInfoDetail.GetValue(pclsCache.CacheConnectionObject, UserId, CategoryCode, ItemCode, ItemSeq);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.GetValue", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //得到专员信息 LY 2015-10-10
        public TypeAndName PsBasicInfoDetailGetSDoctor(DataConnection pclsCache, string PatientId)
        {
            TypeAndName ret = new TypeAndName();
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret.Type = Ps.BasicInfoDetail.GetSDoctor(pclsCache.CacheConnectionObject, PatientId)[0].ToString();
                ret.Name = Ps.BasicInfoDetail.GetSDoctor(pclsCache.CacheConnectionObject, PatientId)[1].ToString();
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.GetSDoctor", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //得到病人详细信息 LY 2015-10-10
        public PatDetailInfo PsBasicInfoDetailGetPatientDetailInfo(DataConnection pclsCache, string UserId)
        {
            PatDetailInfo ret = new PatDetailInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret.PhoneNumber = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[0].ToString();
                ret.HomeAddress = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[1].ToString();
                ret.Occupation = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[2].ToString();
                ret.Nationality = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[3].ToString();
                ret.EmergencyContact = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[4].ToString();
                ret.EmergencyContactPhoneNumber = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[5].ToString();
                ret.PhotoAddress = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[6].ToString();
                ret.IDNo = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[7].ToString();
                ret.Height = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[8].ToString();
                ret.Weight = Ps.BasicInfoDetail.GetPatientDetailInfo(pclsCache.CacheConnectionObject, UserId)[9].ToString();
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.GetPatientDetailInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //获取用户全部详细信息 LY 2015-10-10 
        public List<PatBasicInfoDetail> PsBasicInfoDetailGetPatientBasicInfoDetail(DataConnection pclsCache, string UserId, string CategoryCode)
        {
            List<PatBasicInfoDetail> list = new List<PatBasicInfoDetail>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.BasicInfoDetail.GetPatientBasicInfoDetail(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;
                cmd.Parameters.Add("CategoryCode", CacheDbType.NVarChar).Value = CategoryCode;
                cdr = cmd.ExecuteReader();
                int ItemSeq;
                int SortNo;
                int ControlType;
                while (cdr.Read())
                {
                    if (cdr["ItemSeq"].ToString() == "")
                    {
                        ItemSeq = 0;
                    }
                    else
                    {
                        ItemSeq = Convert.ToInt32(cdr["ItemSeq"]);
                    }
                    if (cdr["SortNo"].ToString() == "")
                    {
                        SortNo = 0;
                    }
                    else
                    {
                        SortNo = Convert.ToInt32(cdr["SortNo"]);
                    }
                    if (cdr["ControlType"].ToString() == "")
                    {
                        ControlType = 0;
                    }
                    else
                    {
                        ControlType = Convert.ToInt32(cdr["ControlType"]);
                    }
                    PatBasicInfoDetail NewLine = new PatBasicInfoDetail();
                    NewLine.UserId = cdr["UserId"].ToString();
                    NewLine.CategoryCode = cdr["CategoryCode"].ToString();
                    NewLine.CategoryName = cdr["CategoryName"].ToString();
                    NewLine.ItemCode = cdr["ItemCode"].ToString();
                    NewLine.ItemName = cdr["ItemName"].ToString();
                    NewLine.ParentCode = cdr["ParentCode"].ToString();
                    NewLine.ItemSeq = ItemSeq;
                    NewLine.Value = cdr["Value"].ToString();
                    NewLine.Content = cdr["Content"].ToString();
                    NewLine.Description = cdr["Description"].ToString();
                    NewLine.SortNo = SortNo;
                    NewLine.ControlType = ControlType;
                    NewLine.OptionCategory = cdr["OptionCategory"].ToString();
                    list.Add(NewLine);
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.GetPatientBasicInfoDetail", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }

                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }

        //通过PID得到模块 LY 2015-10-10 
        public List<TypeAndName> PsBasicInfoDetailGetModulesByPID(DataConnection pclsCache, string PatientId)
        {
            List<TypeAndName> list = new List<TypeAndName>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.BasicInfoDetail.GetModulesByPID(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    TypeAndName NewLine = new TypeAndName();
                    NewLine.Type = cdr["CategoryCode"].ToString();
                    NewLine.Name = cdr["Modules"].ToString();
                    list.Add(NewLine);
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfoDetail.GetModulesByPID", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }

                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }
        #endregion
        #region <Ps.DoctorInfoDetail>
        //SetData  LY 2015-10-10
        public int PsDoctorInfoDetailSetData(DataConnection pclsCache, string Doctor, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.DoctorInfoDetail.SetData(pclsCache.CacheConnectionObject, Doctor, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, piUserId, piTerminalName, piTerminalIP, piDeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsDoctorInfoDetail.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //得到最大的ItemSeq LY 2015-10-10
        public int PsDoctorInfoDetailGetMaxItemSeq(DataConnection pclsCache, string DoctorId, string CategoryCode, string ItemCode)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.DoctorInfoDetail.GetMaxItemSeq(pclsCache.CacheConnectionObject, DoctorId, CategoryCode, ItemCode);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsDoctorInfoDetail.GetMaxItemSeq", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //得到医生详细信息 LY 2015-10-10
        public DocInfoDetail PsDoctorInfoDetailGetDoctorInfoDetail(DataConnection pclsCache, string UserId)
        {
            try
            {
                DocInfoDetail ret = new DocInfoDetail();
                if (!pclsCache.Connect())
                {
                    return null;
                }
                ret.IDNo = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[0].ToString();
                ret.PhotoAddress = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[1].ToString();
                ret.UnitName = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[2].ToString();
                ret.JobTitle = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[3].ToString();
                ret.Level = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[4].ToString();
                ret.Dept = Ps.DoctorInfoDetail.GetDoctorInfoDetail(pclsCache.CacheConnectionObject, UserId)[5].ToString();
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsDoctorInfoDetail.GetDoctorInfoDetail", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        #endregion
    }
}