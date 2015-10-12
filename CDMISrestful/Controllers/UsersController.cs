using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();

        [Route("Api/v1/Users/LogOn")]
        [ModelValidationFilter]
        public HttpResponseMessage LogOn(LogOn logOn)
        {
            int ret = repository.LogOn(logOn.PwType, logOn.username, logOn.password, logOn.role);
            return new ExceptionHandler().LogOn(ret);
        }
    }
}
