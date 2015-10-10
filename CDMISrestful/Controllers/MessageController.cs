using CDMISrestful.DataModels;
using CDMISrestful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CDMISrestful.Controllers
{
    public class MessageController : ApiController
    {
        static readonly IMessageRepository repository = new MessageRepository();
        /// <summary>
        /// GetSMSDialogue 获取消息对话 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public List<Message> GetSMSDialogue(string Reciever, string SendBy)
        {
            return repository.GetSMSDialogue(Reciever, SendBy);
        }

        /// <summary>
        /// SetSMS 将消息写入数据库 GL 2015-10-10
        /// </summary>
        /// <param name="SendBy"></param>
        /// <param name="Reciever"></param>
        /// <param name="Content"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        public int SetSMS(string SendBy, string Reciever, string Content, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            return repository.SetSMS(SendBy, Reciever, Content, piUserId, piTerminalName, piTerminalIP, piDeviceType);
        }

        /// <summary>
        /// GetLatestSMS 获取最新一条消息 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public Message GetLatestSMS(string DoctorId, string PatientId)
        {
            return repository.GetLatestSMS(DoctorId, PatientId);
        }

        /// <summary>
        /// SetSMSRead 将多条消息设为已读 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        public int SetSMSRead(string Reciever, string SendBy, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            return repository.SetSMSRead(Reciever, SendBy, piUserId, piTerminalName, piTerminalIP, piDeviceType);
        }

        /// <summary>
        /// GetSMSCountForOne 获取一对一未读消息数 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public int GetSMSCountForOne(string piReciever, string piSendBy)
        {
            return repository.GetSMSCountForOne(piReciever, piSendBy);
        }

        /// <summary>
        /// GetSMSList 获取消息联系人列表 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<Message> GetSMSList(string DoctorId, string CategoryCode)
        {
            return repository.GetSMSList(DoctorId, CategoryCode);
        }

        /// <summary>
        /// GetSMSCountForAll 获取某医生未读消息总数 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public int GetSMSCountForAll(string DoctorId)
        {
            return repository.GetSMSCountForAll(DoctorId);
        }
    }
}
