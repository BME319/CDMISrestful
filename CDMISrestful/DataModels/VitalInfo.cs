using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class VitalInfo
    {   
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RecordDate{ get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecordTime { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public string ItemType { get; set; }
        /// <summary>
        /// 记录项目
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 项目数据
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 项目单位
        /// </summary>
        public string  Unit{ get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string  StartDate{ get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string  EndDate{ get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 以下为更新信息
        /// </summary>
        public string revUserId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalIP { get; set; }
        public int DeviceType { get; set; }
    }

    /// <summary>
    /// 高血压体征详细-日期段
    /// </summary>
    public class SignDetailByP  
    {
        /// <summary>
        /// 需要查询起始的时间
        /// </summary>
        public int NextStartDate { get; set; } 
        public List<SignDetailByD> SignDetailByDs { get; set; }
        public SignDetailByP()
        {
            SignDetailByDs = new List<SignDetailByD>();
        }
    }

    /// <summary>
    /// 高血压体征详细-某天
    /// </summary>
    public class SignDetailByD  
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }        
        /// <summary>
        /// 星期几
        /// </summary>
        public string WeekDay { get; set; }        
        public List<SignDetail> SignDetailList { get; set; }
        public SignDetailByD()
        {
            SignDetailList = new List<SignDetail>();
        }
    }

    /// <summary>
    /// 高血压体征详细-到分 
    /// </summary>
    public class SignDetail   
    {
        /// <summary>
        /// 详细时间 到时分
        /// </summary>
        public string DetailTime { get; set; }           
        /// <summary>
        /// 收缩压
        /// </summary>
        public string SBPValue { get; set; }        
        /// <summary>
        /// 舒张压
        /// </summary>
        public string DBPValue { get; set; }        
        /// <summary>
        /// 脉率  "78"
        /// </summary>
        public string PulseValue { get; set; }        
        public SignDetail()
        {
            SBPValue = "";
            DBPValue = "";
            PulseValue = "";
        }
    }

    public class MstBloodPressure
    {
        public string Code { get; set; }                //编码  
        public string Name { get; set; }               //名称：很高、偏高、警戒、正常
        public string Description { get; set; }       //描述
        public int SBP { get; set; }              //收缩压
        public int DBP { get; set; }             //舒张压
        public string PatientClass { get; set; }       //患者类型
        public string Redundance { get; set; }        //冗余
    }
}