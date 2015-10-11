using CDMISrestful.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataMethod
{
    public class RiskInfoMethod
    {
        #region < "Ps.TreatmentIndicators" >

        /// <summary>
        /// Ps.TreatmentIndicators.SetData GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SortNo"></param>
        /// <param name="AssessmentType"></param>
        /// <param name="AssessmentName"></param>
        /// <param name="AssessmentTime"></param>
        /// <param name="Result"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int PsTreatmentIndicatorsSetData(DataConnection pclsCache, string UserId, int SortNo, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.TreatmentIndicators.SetData(pclsCache.CacheConnectionObject, UserId, SortNo, AssessmentType, AssessmentName, AssessmentTime, Result, revUserId, TerminalName, TerminalIP, DeviceType);               
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return 2;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int GetMaxSortNo(DataConnection pclsCache, string UserId)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;

                }
                ret = (int)Ps.TreatmentIndicators.GetMaxSortNo(pclsCache.CacheConnectionObject, UserId);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetMaxSortNo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SortNo"></param>
        /// <returns></returns>
        public string GetResult(DataConnection pclsCache, string UserId, int SortNo)
        {
            string Result = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return "";

                }
                Result = Ps.TreatmentIndicators.GetResult(pclsCache.CacheConnectionObject, UserId, SortNo);
                return Result;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetResult", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return Result;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        #endregion
    }
}