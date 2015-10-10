﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class Message
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public string MessageNo { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 发送状态
        /// </summary>
        public int SendStatus { get; set; }
        /// <summary>
        ///阅读状态
        /// </summary>
        public int ReadStatus { get; set; }
        /// <summary>
        /// 发送者Id
        /// </summary>
        public string SendBy { get; set; }
        /// <summary>
        /// 接收者Id
        /// </summary>
        public string SendByName { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendDateTime { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 接收者Id
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 接收者姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 短信标志
        /// </summary>
        public int SMSFlag { get; set; }

        /// <summary>
        /// 区分接收和发送
        /// </summary>
        public string IDFlag { get; set; }

        /// <summary>
        ///区分是否为当天第一条消息
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 未读消息数
        /// </summary>
        public string Count { get; set; }
    }
}