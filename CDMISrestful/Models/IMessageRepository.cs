using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IMessageRepository
    {
        List<Message> GetSMSDialogue(string Reciever, string SendBy);
        int SetSMS(string SendBy, string Reciever, string Content, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        Message GetLatestSMS(string DoctorId, string PatientId);
        int SetSMSRead(string Reciever, string SendBy, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int GetSMSCountForOne(string Reciever, string SendBy);
        List<Message> GetSMSList(string DoctorId, string CategoryCode);
        int GetSMSCountForAll(string DoctorId);
    }
}