using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IUsersRepository
    {
        int LogOn(string PwType, string userId, string password, string role);
        int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int Activition(string UserId, string InviteCode);
        int ChangePassword(string OldPassword, string NewPassword, string ConfirmPassword, string UserId, string Device, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
    }
}