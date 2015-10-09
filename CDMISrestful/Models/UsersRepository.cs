using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.Models
{
    public class UsersRepository : IUsersRepository
    {
         //DataConnection
        DataConnection pclsCache = new DataConnection();

        public int LogOn(string userId, string password)
        {
            
			string myRegEmail = @"/^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/";
			string myRegPhone = @"/^1[3|4|5|7|8][0-9]\d{4,8}$/";
			
            if (userId != "" && password != "")
            {
              
                string PwType = "";
                if (Regex.IsMatch(userId.Trim(), myRegPhone))
                {
                    PwType = "PhoneNo";
                }
                else if (Regex.IsMatch(userId.Trim(), myRegEmail))
                {
                    PwType = "Email";	//暂无此Type
                }
                else
                {
                    PwType = "Else";	//暂无此Type
                }
                int result = GetCheckPasswordByInput(pclsCache, PwType, userId, password);
                if (result == 1)
                {
                    string UId = GetIDByInput(pclsCache, PwType, userId); //输入手机号获取用户ID
                    if (UId != "")
                    {
                        string Class = GetActivatedState(pclsCache, UId, "HealthCoach"); //PAD
                        if (Class == "0")
						{
                            int flag = 0;
                            DataTable AllRoleMatch = GetAllRoleMatch(pclsCache, UId);
                            for (int i = 0; i < AllRoleMatch.Rows.Count; i++)
                            {
                                if (AllRoleMatch.Rows[i]["RoleClass"].ToString() == "HealthCoach")//查询条件
                                { 	
                                    flag = 1;
                                }
                            }
                            if (flag == 1)
                            {                              
                                return 1; //"HomePage.html";
                            }
                            else
                            {
                                return 2; //"已激活 非健康专员";
                            }
						}
						else if (Class == "1")
						{
                            return  3; //"Activition-Pad.html";
						}
						else
					    {
						    return 4; //"抱歉，由于权限问题，您无法使用本系统";
					    }
                    }
                    else
                    {
                        return 5; //"查找不到UserId";
                    }
                }
                else
                {
                    return 6; //"密码错误或用户名不存在";
                }
            }
            else if (userId == "") 
			{
				return 7; //"用户名不能为空！";		
            }
            else 
			{   
				return 8; //"密码不能为空！";
            }
        }

        #region functions
        private int GetCheckPasswordByInput(DataConnection pclsCache, string Type, string Name, string Password)
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

        private string GetIDByInput(DataConnection pclsCache, string Type, string Name)
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

        private string GetActivatedState(DataConnection pclsCache, string UserID, string RoleClass)
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

        private DataTable GetAllRoleMatch(DataConnection pclsCache, string UserID)
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("RoleClass", typeof(string)));

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
                    list.Rows.Add(cdr["RoleClass"].ToString());
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
        #endregion


    }
    
}