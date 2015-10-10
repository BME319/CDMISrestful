using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataBaseMethod;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.Models
{
    public class UsersRepository : IUsersRepository
    {
         //DataConnection
        DataConnection pclsCache = new DataConnection();
        UsersMethod usersMethod = new UsersMethod();
        public int LogOn(string PwType, string userId, string password,string role)
        {
            int result = usersMethod.GetCheckPasswordByInput(pclsCache, PwType, userId, password);
            if (result == 1)
            {
                //密码验证成功
                string UId = usersMethod.GetIDByInput(pclsCache, PwType, userId); //输入手机号获取用户ID
                if (UId != "")
                {
                    string Class = usersMethod.GetActivatedState(pclsCache, UId, role); 
                    if (Class == "0")
                    {
                        int flag = 0;
                        List<string> AllRoleMatch = usersMethod.GetAllRoleMatch(pclsCache, UId);
                        for (int i = 0; i < AllRoleMatch.Count; i++)
                        {
                            if (AllRoleMatch[i].ToString() == role)//查询条件
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            return 1; //"已注册激活且有权限，登陆成功，跳转到主页";
                        }
                        else
                        {
                            return 2; //"已注册激活 但没有权限";
                        }
                    }
                    else      //Class == "1" or Class == ""
                    {
                        return 3;            //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    }                 
                }
                else
                {
                    return 4; //"用户不存在";
                }
            }
            else if (result == 0)
            {
                return 5; //"密码错误";
            }
            else
            {
                return 4;   //"用户不存在"
            }
        }     
    }
    
}