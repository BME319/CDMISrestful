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

    public class BasicInfo
    {
        public string UserName { get; set; }
        public string Birthday { get; set; }
        public string IDNo { get; set; }
        public string Gender { get; set; }

    }
    public class PatientBasicInfo
    {
        public string UserName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string IDNo { get; set; }
        public string DoctorId { get; set; }
        public string InsuranceType { get; set; }
        public string Birthday { get; set; }
        public string GenderText { get; set; }
        public string BloodTypeText { get; set; }
        public string InsuranceTypeText { get; set; }


    }
    public class UserBasicInfo
    {
        public string UserName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string IDNo { get; set; }
        public string DoctorId { get; set; }
        public string InsuranceType { get; set; }
        public string InvalidFlag { get; set; }
    }
    public class DoctorInfo
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string IDNo { get; set; }
        public string InvalidFlag { get; set; }
    }
}