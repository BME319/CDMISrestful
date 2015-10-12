using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.Models
{
    public class UsersRepository : IUsersRepository
    {
        //DataConnection
        DataConnection pclsCache = new DataConnection();
        UsersMethod usersMethod = new UsersMethod();
        CommonMethod commonMethod = new CommonMethod();
        public int LogOn(string PwType, string userId, string password, string role)
        {
            int result = usersMethod.CheckPasswordByInput(pclsCache, PwType, userId, password);
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

        public int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int isHealthCoach = 0;
            int ret=0;
            int Flag = usersMethod.CheckRepeat(pclsCache, userId, PwType);
            if (Flag == 1)
            {
                int Flag1 = usersMethod.Register(pclsCache, PwType, userId, "", Password, UserName, revUserId, TerminalName, TerminalIP, DeviceType);
                if (Flag1 == 1)
                {
                    string userID = usersMethod.GetIDByInput(pclsCache, PwType, userId);
                    if (userID != "")
                    {
                        string InviteNo = commonMethod.GetNo(pclsCache, 12, "");
                        if (InviteNo != "")
                        {
                            int test = usersMethod.PsRoleMatchSetData(pclsCache, userID, "HealthCoach", InviteNo, "1", "");
                            if (test == 1)
                            {
                                ret = 1;//"注册成功，即将返回登录页面";
                            }
                            else
                            {
                                ret = 2; // "注册失败！";
                            }
                        }
                    }
                }
            }
            else
            {
                string UserId = usersMethod.GetIDByInput(pclsCache, PwType, userId);
                if (UserId != "")
                {
                    List<string> result = usersMethod.GetAllRoleMatch(pclsCache, UserId);
                    if (result != null)
                    {
                        int flag3 = 0;
                        for (int i = 0; i < result.Count(); i++)
                        {
                            string Role = result[i];
                            if (Role == "HealthCoach")
                            {
                                flag3 = 1; 
                                isHealthCoach = 1;
                            }
                            if(flag3==1)
                            {
                                ret = 3; // "用户名重复！";
                            }
                            
                        }

                        if (isHealthCoach == 0)
                        {
                            string InviteNo = commonMethod.GetNo(pclsCache, 12, "");
                            if (InviteNo != "")
                            {
                                int test = usersMethod.PsRoleMatchSetData(pclsCache, UserId, "HealthCoach", InviteNo, "1", "");
                                if (test == 1)
                                {
                                    ret = 4; // "注册成功，密码与您的医生账号一致，即将返回登录页面";
                                }
                                else
                                {
                                    ret = 2; // "注册失败！";
                                }
                            }
                        }
                        else
                        {
                            int Flag1 = usersMethod.Register(pclsCache, PwType, userId, "", Password, UserName, revUserId, TerminalName, TerminalIP, DeviceType);
                            if (Flag1 == 1)
                            {
                                string userID = usersMethod.GetIDByInput(pclsCache, PwType, userId);
                                if (userID != "")
                                {
                                    string InviteNo = commonMethod.GetNo(pclsCache, 12, "");
                                    if (InviteNo != "")
                                    {
                                        int test = usersMethod.PsRoleMatchSetData(pclsCache, userID, "HealthCoach", InviteNo, "1", "");
                                        if (test == 1)
                                        {
                                            ret = 1; // "注册成功，即将返回登录页面";
                                        }
                                        else
                                        {
                                            ret = 2; // "注册失败！";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (userId == "")
                {
                    ret = 5; // "用户名不能为空！";
                }
                else if (Password == "")
                {
                    ret = 6; // "密码不能为空！";
                }
                else if (UserName == "")
                {
                    ret = 7; // "真实姓名不能为空！";
                }
            }
            return ret;
        }
    
        public int Activition(string UserId, string InviteCode) 
        {
            int ret =0;
			if (InviteCode != "")
			{
				int Flag = usersMethod.SetActivition(pclsCache,UserId,  "HealthCoach",InviteCode);
			    if (Flag == 1)
				{
					ret = 1; // "激活成功，首次使用请重置密码";
                }
				else
				{
					ret = 2 ; // "邀请码错误，请确认后重新输入邀请码！";
				}
			}
			else
			{
				ret = 3 ; // "邀请码不能为空！";
			}
            return ret;
		}

        public int ChangePassword(string OldPassword, string NewPassword ,string ConfirmPassword,string UserId,string Device,string revUserId, string TerminalName, string TerminalIP, int DeviceType) 
        {
            int ret = 0 ;
            if (OldPassword != "" && NewPassword != "" && ConfirmPassword != "" && NewPassword == ConfirmPassword)
			{
				int test = usersMethod.ChangePassword(pclsCache, UserId, OldPassword, NewPassword, revUserId,  TerminalName, TerminalIP, DeviceType);
				if (test == 1)
				{
					if (Device == "Pad")
					{
						ret = 1;  //  location.href = "HomePage.html";
					}
					else if (Device == "Phone")
					{
						ret = 2;  //  location.href = "TaskMenu.html";
					} 
				}
				else if (test == 2)
				{
					ret = 3; // "新密码设置失败，请联系管理员重置密码";
							
				}
				else if (test == 3)
				{
					ret = 4; //"旧密码错误，请输入正确的旧密码";
				}
				else if (test == 4)
				{
					ret = 5; //"密码已过期，请联系管理员重置密码";
				}
				
			}
			else if (OldPassword == "")
			{
				ret = 6; // "旧密码不能为空！";
			}
			else if (NewPassword == "")
			{
				ret = 7; // "新密码不能为空！";
			}
			else if (ConfirmPassword == "")
			{
				ret = 8; // "请再次输入新密码！";
			}
			else if (NewPassword != ConfirmPassword)
			{
                ret = 9; // "两次输入的密码不同，请再次确认新密码！";
			}
            return ret;
		}
		



    }
}