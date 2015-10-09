using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.CommonLibrary;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();
        public HttpResponseMessage LogOn(String userId, String password)
        {
            int ret = repository.LogOn(userId, password);
            return new ExceptionHandler().LogOn(ret);
        }
    }
}
