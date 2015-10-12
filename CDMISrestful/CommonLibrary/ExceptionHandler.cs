using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace CDMISrestful.CommonLibrary
{
    public class ExceptionHandler
    {
        public HttpResponseMessage SetData(bool operationResult)
        {
            if (operationResult)
            {
                //var response = Request.CreateResponse<bool>(HttpStatusCode.Created, operationResult);
                //string uri = Url.Link("DefaultApi", new { id = item });
                //response.Headers.Location = new Uri(uri);
                //return response;
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage DeleteData(int operationResult)
        {

            //3 数据库连接失败  //0 数据删除失败  
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("数据删除失败"));
            switch (operationResult)
            {
                case 1:
                    //数据删除成功
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("数据删除成功"));
                    break;
                case 2:
                    //数据未找到
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage LogOn(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("登录失败"));
            switch (operationResult)
            {
                case 1:
                    //"已注册激活且有权限，登陆成功，跳转到主页";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("登陆成功"));
                    break;
                case 2:
                    //"已注册激活 但没有权限";
                    resp = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    resp.Content = new StringContent(string.Format("没有权限"));
                    break;
                case 3:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    resp.Content = new StringContent(string.Format("暂未激活"));
                    break;
                case 4:
                    //"用户不存在";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("用户不存在"));
                    break;
                case 5:
                    //"密码错误";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("密码错误"));
                    break;                    
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage Register(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("注册失败"));
            switch (operationResult)
            {
                case 1:
                    //"已注册激活且有权限，登陆成功，跳转到主页";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("注册成功，即将返回登录页面"));
                    break;
                case 2:
                    //"已注册激活 但没有权限";
                    resp = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    resp.Content = new StringContent(string.Format("注册失败"));
                    break;
                case 3:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("用户名重复"));
                    break;
                case 4:
                    //"用户不存在";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("注册成功，密码与您的医生账号一致，即将返回登录页面"));
                    break;
                case 5:
                    //"密码错误";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("用户名不能为空"));
                    break;
                case 6:
                    //"密码错误";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("密码不能为空"));
                    break;
                case 7:
                    //"密码错误";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("真实姓名不能为空"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage Activition(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("激活失败"));
            switch (operationResult)
            {
                case 1:
                    //"已注册激活且有权限，登陆成功，跳转到主页";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("激活成功，首次使用请重置密码"));
                    break;
                case 2:
                    //"已注册激活 但没有权限";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("邀请码错误，请确认后重新输入邀请码"));
                    break;
                case 3:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("邀请码不能为空"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage ChangePassword(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("修改密码失败"));
            switch (operationResult)
            {
                case 1:
                    //"已注册激活且有权限，登陆成功，跳转到主页";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("location.href = HomePage.html"));
                    break;
                case 2:
                    //"已注册激活 但没有权限";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("location.href = TaskMenu.html"));
                    break;
                case 3:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("新密码设置失败，请联系管理员重置密码"));
                    break;
                case 4:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("旧密码错误，请输入正确的旧密码"));
                    break;
                case 5:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("密码已过期，请联系管理员重置密码"));
                    break;
                case 6:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("旧密码不能为空"));
                    break;
                case 7:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("新密码不能为空"));
                    break;
                case 8:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("请再次输入新密码"));
                    break;
                case 9:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("两次输入的密码不同，请再次确认新密码"));
                    break;
                default:
                    break;
            }
            return resp;
        }

    }
}
                       
        