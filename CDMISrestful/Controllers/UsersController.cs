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

        [Route("Api/v1/Users/Register")]
        [ModelValidationFilter]
        public HttpResponseMessage Register(Register Register)
        {
            int ret = repository.Register(Register.PwType, Register.userId, Register.UserName, Register.Password, Register.role, Register.revUserId, Register.TerminalName, Register.TerminalIP, Register.DeviceType);
            return new ExceptionHandler().Register(ret);
        }

        [Route("Api/v1/Users/Activition")]
        [ModelValidationFilter]
        public HttpResponseMessage Activition(Activition Activition)
        {
            int ret = repository.Activition(Activition.UserId, Activition.InviteCode);
            return new ExceptionHandler().Activition(ret);
        }

        [Route("Api/v1/Users/ChangePassword")]
        [ModelValidationFilter]
        public HttpResponseMessage ChangePassword(ChangePassword ChangePassword)
        {
            int ret = repository.ChangePassword(ChangePassword.OldPassword, ChangePassword.NewPassword, ChangePassword.ConfirmPassword, ChangePassword.UserId, ChangePassword.Device, ChangePassword.revUserId, ChangePassword.TerminalName, ChangePassword.TerminalIP, ChangePassword.DeviceType);
            return new ExceptionHandler().ChangePassword(ret);
        }
    }
}
