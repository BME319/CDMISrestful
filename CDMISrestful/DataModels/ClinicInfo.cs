using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class ClinicInfo
    {

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
}