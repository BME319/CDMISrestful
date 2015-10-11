using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using CDMISrestful.DataModels;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public class MessageRepository : IMessageRepository
    {
        DataConnection pclsCache = new DataConnection();
        /// <summary>
        /// 获取消息对话 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public List<Message> GetSMSDialogue(string Reciever, string SendBy)
        {
            return new MessageMethod().GetSMSDialogue(pclsCache, Reciever, SendBy);
        }

        /// <summary>
        /// 将消息写入数据库 GL 2015-10-10
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
            return new MessageMethod().SetSMS(pclsCache, SendBy, Reciever, Content, piUserId, piTerminalName, piTerminalIP, piDeviceType);         
        }

        /// <summary>
        /// 获取最新一条消息 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public Message GetLatestSMS(string DoctorId, string PatientId)
        {
            return new MessageMethod().GetLatestSMS(pclsCache, DoctorId, PatientId);             
        }

        /// <summary>
        /// 将多条消息设为已读 GL 2015-10-10
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
            return new MessageMethod().SetSMSRead(pclsCache, Reciever, SendBy, piUserId, piTerminalName, piTerminalIP, piDeviceType);       
        }

        /// <summary>
        /// 获取一对一未读消息数 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public int GetSMSCountForOne(string Reciever, string SendBy)
        {
            return new MessageMethod().GetSMSCountForOne(pclsCache, Reciever, SendBy);         
        }

        /// <summary>
        /// 获取消息联系人列表 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<Message> GetSMSList(string DoctorId, string CategoryCode)
        {
            return new MessageMethod().GetSMSList(pclsCache, DoctorId, CategoryCode);
        }

        /// <summary>
        /// 获取某医生未读消息总数 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public int GetSMSCountForAll(string DoctorId)
        {
            return new MessageMethod().GetSMSCountForAll(pclsCache, DoctorId);        
        }
    }
}