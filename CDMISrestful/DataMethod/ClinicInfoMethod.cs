using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;


namespace CDMISrestful.DataMethod
{

    public class ClinicInfoMethod
    {
        //Ps.LabTest
        #region
        //SYF 2015-10-10
        public List<LabTestList> GetLabTestList(DataConnection pclsCache, string piUserId, string piVisitId)
        {

            List<LabTestList> list = new List<LabTestList>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.LabTest.GetLabTestList(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("piUserId", CacheDbType.NVarChar).Value = piUserId;
                cmd.Parameters.Add("piVisitId", CacheDbType.NVarChar).Value = piVisitId;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    //string LabTestDateShow = "", ReportDateShow = "";
                    //string str = cdr["LabTestDate"].ToString();
                    //if (str != "0")
                    //{
                    //    LabTestDateShow = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                    //}
                    //string str1 = cdr["ReportDate"].ToString();
                    //if (str1 != "0")
                    //{
                    //    ReportDateShow = str1.Substring(0, 4) + "-" + str1.Substring(4, 2) + "-" + str1.Substring(6, 2);
                    //}
                    string ReportDateShow = "";
                    if (cdr["ReportDate"].ToString() == "9999/1/1 0:00:00")
                    {
                        ReportDateShow = "";
                    }
                    else
                    {
                        ReportDateShow = cdr["ReportDate"].ToString();
                    }
                    LabTestList NewLine = new LabTestList();
                    NewLine.VisitId = cdr["VisitId"].ToString();
                    NewLine.SortNo = cdr["SortNo"].ToString();
                    NewLine.LabItemType = cdr["LabItemType"].ToString();
                    NewLine.LabItemTypeName = cdr["LabItemTypeName"].ToString();
                    NewLine.LabItemCode = cdr["LabItemCode"].ToString();
                    NewLine.LabItemName = cdr["LabItemName"].ToString();
                    NewLine.LabTestDate = cdr["LabTestDate"].ToString();
                    NewLine.StatusCode = cdr["StatusCode"].ToString();
                    NewLine.Status = cdr["Status"].ToString();
                    NewLine.ReportDate = cdr["ReportDate"].ToString();
                    NewLine.DeptCode = cdr["DeptCode"].ToString();
                    NewLine.DeptName = cdr["DeptName"].ToString();
                    NewLine.Creator = cdr["Creator"].ToString();
                    NewLine.LabTestDateCom = cdr["LabTestDateCom"].ToString(); 
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.LabTest.GetLabTestList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }
        #endregion

        //Ps.DrugRecord
        #region
        //syf 2015-10-10
        public List<DrugRecordList> GetDrugRecordList(DataConnection pclsCache, string piUserId, string piVisitId)
        {
            List<DrugRecordList> list = new List<DrugRecordList>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.DrugRecord.GetDrugRecordList(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("piUserId", CacheDbType.NVarChar).Value = piUserId;
                cmd.Parameters.Add("piVisitId", CacheDbType.NVarChar).Value = piVisitId;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    string startTime = "";
                    if (cdr["StartDateTime"].ToString() != null && cdr["StartDateTime"].ToString() != "")
                    {
                        startTime = Convert.ToDateTime(cdr["StartDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    string endTime = "";
                    if (cdr["StopDateTime"].ToString() != null && cdr["StopDateTime"].ToString() != "")
                    {
                        endTime = Convert.ToDateTime(cdr["StopDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    DrugRecordList NewLine = new DrugRecordList();
                    NewLine.VisitId = cdr["VisitId"].ToString();
                    NewLine.OrderNo = cdr["OrderNo"].ToString();
                    NewLine.OrderSubNo = cdr["OrderSubNo"].ToString();
                    NewLine.RepeatIndicatorCode = cdr["RepeatIndicatorCode"].ToString();
                    NewLine.RepeatIndicator = cdr["RepeatIndicator"].ToString();
                    NewLine.OrderClassCode = cdr["OrderClassCode"].ToString();
                    NewLine.OrderClass = cdr["OrderClass"].ToString();
                    NewLine.OrderCode = cdr["OrderCode"].ToString();
                    NewLine.OrderContent = cdr["OrderContent"].ToString();
                    NewLine.Dosage = cdr["Dosage"].ToString();
                    NewLine.DosageUnitsCode = cdr["DosageUnitsCode"].ToString();
                    NewLine.DosageUnits = cdr["DosageUnits"].ToString();
                    NewLine.AdministrationCode = cdr["AdministrationCode"].ToString();
                    NewLine.Administration = cdr["Administration"].ToString();
                    NewLine.StartDateTime = cdr["StartDateTime"].ToString();
                    NewLine.StopDateTime = cdr["StopDateTime"].ToString();
                    NewLine.Frequency = cdr["Frequency"].ToString();
                    NewLine.FreqCounter = cdr["FreqCounter"].ToString();
                    NewLine.FreqInteval = cdr["FreqInteval"].ToString();
                    NewLine.FreqIntevalUnitCode = cdr["FreqIntevalUnitCode"].ToString();
                    NewLine.FreqIntevalUnit = cdr["FreqIntevalUnit"].ToString();
                    NewLine.DeptCode = cdr["DeptCode"].ToString();
                    NewLine.DeptName = cdr["DeptName"].ToString();
                    NewLine.Creator = cdr["Creator"].ToString();
                    NewLine.StartDateTimeCom = cdr["StartDateTimeCom"].ToString();
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.DrugRecord.GetDrugRecordList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }
        #endregion
    }
}