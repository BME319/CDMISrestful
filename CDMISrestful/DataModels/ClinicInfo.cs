using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class ClinicInfo
    {
    }
    public class ClinicalTrans
    {
        public DateTime 精确时间 { get; set; }
        public string 类型 { get; set; }
        public string VisitId { get; set; }
        public string 事件 { get; set; }
        public string 关键属性 { get; set; }
    }
    public class ClinicalTemp
    {
        public int SortNo { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DisChargeDate { get; set; }
        public string HospitalName { get; set; }
        public string DepartmentName { get; set; }
    }
    public class DiagnosisInfo
    {
        public string VisitId { get; set; }
        public string DiagnosisType { get; set; }
        public string DiagnosisTypeName { get; set; }
        public string DiagnosisNo { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisName { get; set; }
        public string Description { get; set; }
        public string RecordDate { get; set; }
        public string RecordDateShow { get; set; }
        public string Creator { get; set; }
        public string RecordDateCom { get; set; }
    }

    public class ExamInfo
    {
        public string VisitId { get; set; }
        public string SortNo { get; set; }
        public string ExamType { get; set; }
        public string ExamTypeName { get; set; }
        public string ExamDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ExamPara { get; set; }
        public string Description { get; set; }
        public string Impression { get; set; }
        public string Recommendation { get; set; }
        public string IsAbnormalCode { get; set; }
        public string IsAbnormal { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string ReqortDate { get; set; }
        public string ImageURL { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string Creator { get; set; }
        public string ExamDateCom { get; set; }
    }
}