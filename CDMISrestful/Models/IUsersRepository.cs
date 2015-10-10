using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IUsersRepository
    {
        int LogOn(string PwType, string userId, string password, string role);

        //PatientListInfo GetPatientList(string DoctorId, string ModuleType, int Plan, int Compliance, int Goal);

    }
}