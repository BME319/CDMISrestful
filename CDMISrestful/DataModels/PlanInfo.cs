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
}