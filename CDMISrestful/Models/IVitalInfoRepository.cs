using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IVitalInfoRepository
    {
        string GetLatestPatientVitalSigns(string UserId, string ItemType, string ItemCode);
        int SetPatientVitalSigns(string UserId, int RecordDate, int RecordTime, string ItemType, string ItemCode, string Value, string Unit, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        SignDetailByP GetSignsDetailByPeriod(string PatientId, string Module, int StartDate, int Num);
    }
}