using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.DataModels
{

    public class Period
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DayCount { get; set; }
    }

    public class Progressrate
    {
        public string RemainingDays { get; set; }
        public string ProgressRate { get; set; }
    }

    public class GPlanInfo
    {
        public string PlanNo { get; set; }
        public string PatientId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Module { get; set; }
        public string Status { get; set; }
        public string DoctorId { get; set; }
    }

    public class PatientPlan
    {
        public string PatientId { get; set; }
        public string PlanNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TotalDays { get; set; }
        public string RemainingDays { get; set; }
        public string Status { get; set; }
    }

    public class TasksComList
    {
        public string Date { get; set; }
        public string ComplianceValue { get; set; }
        public string TaskType { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string TaskCode { get; set; }
        public string Type { get; set; }
    }

    public class TasksComByPeriodDT
    {
        public string Date { get; set; }
        public string ComplianceValue { get; set; }
        public string TaskType { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    public class TasksByDate
    {
        public string TaskID { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
    }

    public class TasksByStatus
    {
         public string Id { get; set; }
        public string Status { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Instruction { get; set; }
    }

    public class SignDetailByPeriod
    {
         public string RecordDate { get; set; }
        public string RecordTime { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class ComplianceListByPeriod
    {
         public string Date { get; set; }
        public string Compliance { get; set; }
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