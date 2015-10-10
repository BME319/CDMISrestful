using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class PlanInfo
    {
    }
    public class PlanDeatil
    {
        /// <summary>
        /// 计划名称  “计划”+序号+起止时间  用于计划列表的显示
        /// </summary>
        public string PlanName { get; set; }           

        /// <summary>
        /// 计划编码
        /// </summary>
        public string PlanNo { get; set; }            

        /// <summary>
        /// 起始日期
        /// </summary>
        public int StartDate { get; set; }           

        /// <summary>
        /// 截止日期
        /// </summary>
        public int EndDate { get; set; }             
    }

     
    public class PsTaskByType
    { 
        public string Id {get; set;}
        public string Code {get; set;}
        public string CodeName {get; set;}
        public string Instruction {get; set;}
     
    }
    public class PsTask
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string Instruction { get; set; }

    }
    public class TargetByCode
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Origin { get; set; }
        public string Instruction { get; set; }
        public string Unit { get; set; }

    }

}