using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class VitalInfo
    {   
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RecordDate{ get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecordTime { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public string ItemType { get; set; }
        /// <summary>
        /// 记录项目
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 项目数据
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 项目单位
        /// </summary>
        public string  Unit{ get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string  StartDate{ get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string  EndDate{ get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string SignType { get; set; }
    }
}