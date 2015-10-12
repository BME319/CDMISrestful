using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.DataViewModels
{
    public class VClinicInfo
    {
        public List<DiagnosisInfo> DiagnosisInfo_DataViewModel { get; set; }
        public List<ExamInfo> ExamInfo_DataViewModel { get; set; }
        public List<LabTestList> LabTestList_DataViewModel { get; set; }
        public List<DrugRecordList> DrugRecordList_DataViewModel { get; set; }

        public VClinicInfo()
        {
            DiagnosisInfo_DataViewModel = new List<DiagnosisInfo>();
            ExamInfo_DataViewModel = new List<ExamInfo>();
            LabTestList_DataViewModel = new List<LabTestList>();
            DrugRecordList_DataViewModel = new List<DrugRecordList>();
        }

    }
   
}