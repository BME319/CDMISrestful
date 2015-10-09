using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IUsersRepository
    {
        int LogOn(String userId, String password);

        //PatientListInfo GetPatientList(string DoctorId, string ModuleType, int Plan, int Compliance, int Goal);

    }
}