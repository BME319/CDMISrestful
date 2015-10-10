using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;

namespace CDMISrestful.DataMethod
{
    public class CommonMethod
    {
        #region CmMstNumbering
        //GetNo 自动编号 CSQ 20151010
        public string GetNo(DataConnection pclsCache, int NumberingType, string TargetDate)
        {
            string number = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return number;

                }
                number = Cm.MstNumbering.GetNo(pclsCache.CacheConnectionObject, NumberingType, TargetDate);
                return number;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取编号失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstNumbering.GetNo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return number;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        #endregion

        //GetServerTime 获取服务器时间 CSQ 20151010
        public string GetServerTime(DataConnection pclsCache)
        {
            string serverTime = string.Empty;
            try
            {
                if (!pclsCache.Connect())
                {
                    return serverTime;
                }
                serverTime = Cm.CommonLibrary.GetServerDateTime(pclsCache.CacheConnectionObject);   
                serverTime = serverTime.Replace("/", "-");
                return serverTime;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "GetServerTime", "WebService调用异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return serverTime;
                throw (ex);
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 时间格式转换 GL 2015-10-10
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string TransTime(string time)
        {
            int length = time.Length;
            string result = "";
            try
            {
                switch (length)
                {
                    case 1:
                        result = "00：0" + time;
                        break;
                    case 2:
                        result = "00：" + time;
                        break;
                    case 3:
                        result = "0" + time.Substring(0, 1) + "：" + time.Substring(1, 2);
                        break;
                    case 4:
                        result = time.Substring(0, 2) + "：" + time.Substring(2, 2);  //Substring(起始, 截取长度)
                        break;
                    default: break;
                }

                return result;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CommonMethod.TransTime", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
    }
}