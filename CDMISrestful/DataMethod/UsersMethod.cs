using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;

namespace CDMISrestful.DataBaseMethod
{
    public class UsersMethod
    {
        public int GetCheckPasswordByInput(DataConnection pclsCache, string Type, string Name, string Password)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUserDetail.CheckPasswordByInput(pclsCache.CacheConnectionObject, Type, Name, Password);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstUserDetail.CheckPasswordByInput", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        #region<CmMstUserDetail>

        public string GetIDByInput(DataConnection pclsCache, string Type, string Name)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = Cm.MstUserDetail.GetIDByInput(pclsCache.CacheConnectionObject, Type, Name);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstUserDetail.GetIDByInput", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        // CheckRepeat LS 2015-03-26  TDY 20150507修改 //WF 20151010
        public int CheckRepeat(DataConnection pclsCache, string Input, string Type)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUserDetail.CheckRepeat(pclsCache.CacheConnectionObject, Input, Type);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstUserDetail.CheckRepeat", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //CheckPassworkByInput LS 2015-03-26  TDY 20150507修改 //WF 20151010
        public int CheckPasswordByInput(DataConnection pclsCache, string Type, string Name, string Password)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUserDetail.CheckPasswordByInput(pclsCache.CacheConnectionObject, Type, Name, Password);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstUserDetail.CheckPasswordByInput", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        #endregion

        #region<CmMstUser>
        //ChangePassword ZAM 2014-12-01 //WF 20151010
        public int ChangePassword(DataConnection pclsCache, string UserId, string OldPassword, string newPassword, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return ret;

                }
                ret = (int)Cm.MstUser.ChangePassword(pclsCache.CacheConnectionObject, UserId, OldPassword, newPassword, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstUser.ChangePassword", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }


        }
        //Register TDY 2015-04-07 专员注册 //TDY 20150419修改 //WF 20151010
        public int Register(DataConnection pclsCache, string Type, string Name, string Value, string Password, string UserName, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUser.Register(pclsCache.CacheConnectionObject, Type, Name, Value, Password, UserName, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstUser.Register", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        // GetNameByUserId ZAM 2014-11-26 //WF 20151010
        public string GetNameByUserId(DataConnection pclsCache, string UserId)
        {
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }
                string Name = Cm.MstUser.GetNameByUserId(pclsCache.CacheConnectionObject, UserId);
                return Name;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstUser.GetNameByUserId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        #endregion

        #region<PsRoleMatch>
        public string GetActivatedState(DataConnection pclsCache, string UserID, string RoleClass)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = Ps.RoleMatch.GetActivatedState(pclsCache.CacheConnectionObject, UserID, RoleClass);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.RoleMatch.GetActivatedState", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        public List<string> GetAllRoleMatch(DataConnection pclsCache, string UserID)
        {
            List<string> list = new List<string>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.RoleMatch.GetAllRoleMatch(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserID", CacheDbType.NVarChar).Value = UserID;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(cdr["RoleClass"].ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsRoleMatch.GetAllRoleMatch", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        //TDY 2015-05-26 //WF 20151010
        public int PsRoleMatchSetData(DataConnection pclsCache, string PatientId, string RoleClass, string ActivationCode, string ActivatedState, string Description)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Ps.RoleMatch.SetData(pclsCache.CacheConnectionObject, PatientId, RoleClass, ActivationCode, ActivatedState, Description);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.RoleMatch.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        // TDY 20150526 SetActivition //WF 20151010
        public int SetActivition(DataConnection pclsCache, string UserID, string RoleClass, string ActivationCode)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Ps.RoleMatch.SetActivition(pclsCache.CacheConnectionObject, UserID, RoleClass, ActivationCode);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.RoleMatch.SetActivition", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        #endregion

        #region<PsBasicInfo>
        //SetData WF 2014-12-2 //WF 20151010
        public int PsBasicInfoSetData(DataConnection pclsCache, string UserId, string UserName, int Birthday, int Gender, int BloodType, string IDNo, string DoctorId, string InsuranceType, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int IsSaved = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return IsSaved;

                }
                IsSaved = (int)Ps.BasicInfo.SetData(pclsCache.CacheConnectionObject, UserId, UserName, Birthday, Gender, BloodType, IDNo, DoctorId, InsuranceType, InvalidFlag, revUserId, TerminalName, TerminalIP, DeviceType);

                return IsSaved;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfo.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return IsSaved;
            }
            finally
            {
                pclsCache.DisConnect();
            }


        }

        //GetUserBasicInfo TDY 2014-12-4  //WF 20151010
        public UserBasicInfo GetUserBasicInfo(DataConnection pclsCache, string UserId)
        {
            UserBasicInfo ret = new UserBasicInfo();
            try
            {

                if (!pclsCache.Connect())
                {
                    return null;
                }
                //Array a = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId);
                ret.UserName = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[0].ToString();
                ret.Birthday = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[1].ToString();
                ret.Gender = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[2].ToString();
                ret.BloodType = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[3].ToString();
                ret.IDNo = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[4].ToString();
                ret.DoctorId = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[5].ToString();
                ret.InsuranceType = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[6].ToString();
                ret.InvalidFlag = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[7].ToString();



                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfo.GetUserBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        //GetBasicInfo WF 2014-12-2  //WF 20151010
        public PatientBasicInfo GetPatientBasicInfo(DataConnection pclsCache, string UserId)
        {
            PatientBasicInfo ret = new PatientBasicInfo();
            try
            {

                if (!pclsCache.Connect())
                {
                    return null;
                }
                ret.UserName = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[0].ToString();
                ret.Age = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[1].ToString();
                ret.Gender = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[2].ToString();
                ret.BloodType = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[3].ToString();
                ret.IDNo = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[4].ToString();
                ret.DoctorId = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[5].ToString();
                ret.InsuranceType = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[6].ToString();
                ret.Birthday = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[7].ToString();
                ret.GenderText = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[8].ToString();
                ret.BloodTypeText = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[9].ToString();
                ret.InsuranceTypeText = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId)[10].ToString();


                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfo.GetPatientBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        //GetAgeByBirthDay SYF 2015-04-23 //WF 20151010
        public int GetAgeByBirthDay(DataConnection pclsCache, int Birthday)
        {
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return 0;
                }
                else
                {
                    int Age = (int)Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Birthday);
                    return Age;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfo.GetUserBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return 0;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }

        //GetBasicInfo WF 2014-12-2  //WF 20151010
        public BasicInfo GetBasicInfo(DataConnection pclsCache, string UserId)
        {
            BasicInfo ret = new BasicInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                ret.UserName = Ps.BasicInfo.GetBasicInfo(pclsCache.CacheConnectionObject, UserId)[0].ToString();
                ret.Birthday = Ps.BasicInfo.GetBasicInfo(pclsCache.CacheConnectionObject, UserId)[1].ToString();
                ret.IDNo = Ps.BasicInfo.GetBasicInfo(pclsCache.CacheConnectionObject, UserId)[2].ToString();
                ret.Gender = Ps.BasicInfo.GetBasicInfo(pclsCache.CacheConnectionObject, UserId)[3].ToString();
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsBasicInfo.GetBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }

        #endregion

        #region<PsDoctorInfo>
        //SetData  LS 2014-12-1
        public int PsDoctorInfoSetData(DataConnection pclsCache, string UserId, string UserName, int Birthday, int Gender, string IDNo, int InvalidFlag, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            int IsSaved = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return IsSaved;

                }
                IsSaved = (int)Ps.DoctorInfo.SetData(pclsCache.CacheConnectionObject, UserId, UserName, Birthday, Gender, IDNo, InvalidFlag, piUserId, piTerminalName, piTerminalIP, piDeviceType);

                return IsSaved;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsDoctorInfo.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return IsSaved;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        // GetDoctorInfo返回医生基本信息 ZYF 2014-12-4
        public DoctorInfo GetDoctorInfo(DataConnection pclsCache, string DoctorId)
        {
            DoctorInfo ret = new DoctorInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                ret.DoctorId = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[0].ToString();
                ret.DoctorName = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[1].ToString();
                ret.Birthday = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[2].ToString();
                ret.Gender = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[3].ToString();
                ret.IDNo = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[4].ToString();
                ret.InvalidFlag = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId)[5].ToString();
                //DataCheck ZAM 2015-1-7

                //
                //list.Rows.Add(CacheList[0].ToString(), CacheList[1].ToString(), CacheList[2].ToString(), CacheList[3].ToString(), CacheList[4], CacheList[5]);
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsDoctorInfo.GetDoctorInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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