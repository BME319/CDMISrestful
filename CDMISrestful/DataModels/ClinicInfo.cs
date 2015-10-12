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
    public class LabTestList
    {
        public string VisitId { get; set; }
        public string SortNo { get; set; }
        public string LabItemType { get; set; }
        public string LabItemTypeName { get; set; }
        public string LabItemCode { get; set; }
        public string LabItemName { get; set; }
        public string LabTestDate { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string ReportDate { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string Creator { get; set; }
        public string LabTestDateCom { get; set; }
    }
    public class DrugRecordList
    {
        public string VisitId { get; set; }
        public string OrderNo { get; set; }
        public string OrderSubNo { get; set; }
        public string RepeatIndicatorCode { get; set; }
        public string RepeatIndicator { get; set; }
        public string OrderClassCode { get; set; }
        public string OrderClass { get; set; }
        public string OrderCode { get; set; }
        public string OrderContent { get; set; }
        public string Dosage { get; set; }
        public string DosageUnitsCode { get; set; }
        public string DosageUnits { get; set; }
        public string AdministrationCode { get; set; }
        public string Administration { get; set; }
        public string StartDateTime { get; set; }
        public string StopDateTime { get; set; }
        public string Frequency { get; set; }
        public string FreqCounter { get; set; }
        public string FreqInteval { get; set; }
        public string FreqIntevalUnitCode { get; set; }
        public string FreqIntevalUnit { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string Creator { get; set; }
        public string StartDateTimeCom { get; set; }
    }
    public class DrugRecord
    {
        public string VisitId { get; set; }
        public string OrderNo { get; set; }
        public string OrderSubNo { get; set; }
        public string RepeatIndicatorCode { get; set; }
        public string RepeatIndicator { get; set; }
        public string OrderClassCode { get; set; }
        public string OrderClass { get; set; }
        public string OrderCode { get; set; }
        public string OrderContent { get; set; }
        public string Dosage { get; set; }
        public string DosageUnitsCode { get; set; }
        public string DosageUnits { get; set; }
        public string AdministrationCode { get; set; }
        public string Administration { get; set; }
        public string StartDateTime { get; set; }
        public string StopDateTime { get; set; }
        public string Frequency { get; set; }
        public string FreqCounter { get; set; }
        public string FreqInteval { get; set; }
        public string FreqIntevalUnitCode { get; set; }
        public string FreqIntevalUnit { get; set; }
        public string HistoryContent { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
    }
    public class PsDrugRecord
    {
        public string VisitId { get; set; }
        public string OrderNo { get; set; }
        public string OrderSubNo { get; set; }
        public string RepeatIndicator { get; set; }
        public string OrderClass { get; set; }
        public string OrderCode { get; set; }
        public string DrugName { get; set; }
        public string CurativeEffect { get; set; }
        public string SideEffect { get; set; }
        public string Instruction { get; set; }
        public string HealthEffect { get; set; }
        public string Unit { get; set; }
        public string OrderContent { get; set; }
        public string Dosage { get; set; }
        public string DosageUnits { get; set; }
        public string Administration { get; set; }
        public string StartDateTime { get; set; }
        public string StopDateTime { get; set; }
        public string Frequency { get; set; }
    }
    public class NewExam
    {
        public string Name1 { get; set; }
        public string Value1 { get; set; }
        public string Name2 { get; set; }
        public string Value2 { get; set; }
        public string Name3 { get; set; }
        public string Value3 { get; set; }

    }
    public class NewLabTest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class LabTestDetails
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string IsAbnormalCode { get; set; }
        public string IsAbnormal { get; set; }
        public string UnitCode { get; set; }

        public string Unit { get; set; }
        public string Creator { get; set; }

    }



}