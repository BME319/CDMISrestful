using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class Users
    {
    }

    public class LogOn
    {
        
        [Required(ErrorMessage = "请传入Type")]
        public string PwType { get; set; }
        [Required(ErrorMessage = "请输入用户名")]
        //[RegularExpression(@"/^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/" @"/^1[3|4|5|7|8][0-9]\d{4,8}$/)]
        public string username { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string password { get; set; }
        [Required(ErrorMessage = "角色信息必填")]
        public string role { get; set; }
    }
}