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
                    //"HomePage.html";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("主页"));
                    break;
                case 2:
                    //"已激活 非健康专员";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 3:
                    //"Activition-Pad.html";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 4:
                    //"抱歉，由于权限问题，您无法使用本系统";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 5:
                    //"查找不到UserId";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 6:
                    //"密码错误或用户名不存在";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 7:
                    //"用户名不能为空！";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                case 8:
                    //"密码不能为空！";
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                default:
                    break;
            }
            return resp;
        }
    
    }
}
                       
        