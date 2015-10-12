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
        #region <PsClinicalInfo>
        //住院-转科处理 LY 2015-10-10  
        public List<ClinicalTrans> PsClinicalInfoGetTransClinicalInfo(DataConnection pclsCache, string UserId, string VisitId)
        {
            //最终输出
            List<ClinicalTrans> List_Trans = new List<ClinicalTrans>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                //取出原始数据
                List<ClinicalTemp> List_Temp = new List<ClinicalTemp>();
                cmd = new CacheCommand();
                cmd = Ps.InPatientInfo.GetInfobyVisitId(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;
                cmd.Parameters.Add("VisitId", CacheDbType.NVarChar).Value = VisitId;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    ClinicalTemp NewLine = new ClinicalTemp();
                    NewLine.SortNo = Convert.ToInt32(cdr["SortNo"]);
                    NewLine.AdmissionDate = Convert.ToDateTime(cdr["AdmissionDate"]);
                    NewLine.DisChargeDate = Convert.ToDateTime(cdr["DischargeDate"]);
                    NewLine.HospitalName = cdr["HospitalName"].ToString();
                    NewLine.DepartmentName = cdr["DepartmentName"].ToString();
                    List_Temp.Add(NewLine);
                }
                //有转科 
                //转科处理  转科内容：什么时候从哪里转出，什么时候转到哪
                if (List_Temp.Count > 1)
                {
                    for (int n = 0; n < List_Temp.Count - 1; n++)
                    {
                        //只科室
                        string things = List_Temp[n].DepartmentName + "(转出)" + "  ";
                        things += List_Temp[n + 1].DepartmentName + "(转入)";
                        ClinicalTrans NewLine = new ClinicalTrans();
                        NewLine.精确时间 = List_Temp[n + 1].AdmissionDate;
                        NewLine.类型 = "转科";
                        NewLine.VisitId = VisitId;
                        NewLine.事件 = things;
                        List_Trans.Add(NewLine);
                    }
                }
                return List_Trans;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsClinicalInfo.GetClinicalInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        //检查化验等信息 LY 2015-10-10
        public List<ClinicalTrans> PsClinicalInfoGetOtherTable(DataConnection pclsCache, string UserId, string VisitId)
        {
            //输出表
            List<ClinicalTrans> List_Trans = new List<ClinicalTrans>();
            try
            {
                //诊断
                List<DiagnosisInfo> List_Diagnosis = new List<DiagnosisInfo>();
                List_Diagnosis = PsDiagnosisGetDiagnosisInfo(pclsCache, UserId, VisitId);
                for (int i = 0; i < List_Diagnosis.Count; i++)
                {
                    ClinicalTrans NewLine = new ClinicalTrans();
                    NewLine.精确时间 = Convert.ToDateTime(List_Diagnosis[i].RecordDate);
                    NewLine.类型 = "诊断";
                    NewLine.VisitId = VisitId;
                    NewLine.事件 = "诊断：" + List_Diagnosis[i].TypeName;
                    NewLine.关键属性 = "DiagnosisInfo" + "|" + VisitId + "|" + Convert.ToDateTime(List_Diagnosis[i].RecordDate).ToString("yyyy-MM-ddHH:mm:ss");
                    List_Trans.Add(NewLine);
                }
                //检查
                List<ExamInfo> List_Examination = new List<ExamInfo>();
                List_Examination = PsExaminationGetExaminationList(pclsCache, UserId, VisitId);
                for (int i = 0; i < List_Examination.Count; i++)
                {
                    ClinicalTrans NewLine = new ClinicalTrans();
                    NewLine.精确时间 = Convert.ToDateTime(List_Examination[i].ExamDate);
                    NewLine.类型 = "检查";
                    NewLine.VisitId = VisitId;
                    NewLine.事件 = "检查：" + List_Examination[i].ExamTypeName;
                    NewLine.关键属性 = "ExaminationInfo" + "|" + VisitId + "|" + Convert.ToDateTime(List_Examination[i].ExamDate).ToString("yyyy-MM-ddHH:mm:ss");
                    List_Trans.Add(NewLine);
                }
                //化验
                List<LabTestList> List_LabTest = new List<LabTestList>();
                List_LabTest = GetLabTestList(pclsCache, UserId, VisitId);
                for (int i = 0; i < List_LabTest.Count; i++)
                {
                    ClinicalTrans NewLine = new ClinicalTrans();
                    NewLine.精确时间 = Convert.ToDateTime(List_LabTest[i].LabTestDate);
                    NewLine.类型 = "化验";
                    NewLine.VisitId = VisitId;
                    NewLine.事件 = "化验：" + List_LabTest[i].LabItemName;
                    NewLine.关键属性 = "LabTestInfo" + "|" + VisitId + "|" + Convert.ToDateTime(List_LabTest[i].LabTestDate).ToString("yyyy-MM-ddHH:mm:ss");
                    List_Trans.Add(NewLine);
                }
                //用药    这部分要等syf写完GetDrugRecord才行
                //List<DrugRecordList> List_DrugRecord = new List<DrugRecordList>();
                //List_DrugRecord = GetDrugRecordList(pclsCache, UserId, VisitId);
                //for (int i = 0; i < List_DrugRecord.Count; i++)
                //{
                //    ClinicalTrans NewLine = new ClinicalTrans();
                //    NewLine.精确时间 = Convert.ToDateTime(List_DrugRecord[i].StartDateTime);
                //    NewLine.类型 = "用药";
                //    NewLine.VisitId = VisitId;
                //    NewLine.事件 = "用药：" + List_DrugRecord[i].HistoryContent;
                //    NewLine.关键属性 = "DrugRecord" + "|" + VisitId + "|" + Convert.ToDateTime(List_DrugRecord[i].StartDateTime).ToString("yyyy-MM-ddHH:mm:ss");
                //    List_Trans.Add(NewLine);
                //}
                return List_Trans;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsClinicalInfo.GetClinicalInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
            }
        }

        //获取门诊的下一日期 LY 2015-10-10
        public string PsClinicalInfoGetNextOutDate(DataConnection pclsCache, string UserId, string ClinicDate)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (string)Ps.OutPatientInfo.GetNextDatebyDate(pclsCache.CacheConnectionObject, UserId, Convert.ToDateTime(ClinicDate).ToString("yyyy-MM-dd HH:mm:ss"));
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.OutPatientInfo.GetNextDatebyDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //获取住院的下一日期 LY 2015-10-10
        public string PsClinicalInfoGetNextInDate(DataConnection pclsCache, string UserId, string AdmissionDate)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (string)Ps.InPatientInfo.GetNextDatebyDate(pclsCache.CacheConnectionObject, UserId, Convert.ToDateTime(AdmissionDate).ToString("yyyy-MM-dd HH:mm:ss"));
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.InPatientInfo.GetNextDatebyDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //颜色分配：根据首标签决定 LY 2015-10-10
        public string PsClinicalInfoGetColor(string type)
        {
            string colorShow = "clolor_default";
            try
            {
                switch (type)
                {
                    case "入院": colorShow = "clolor_in";
                        break;
                    case "出院": colorShow = "clolor_in";
                        break;
                    case "转科": colorShow = "clolor_trans";
                        break;
                    case "门诊": colorShow = "clolor_out";
                        break;
                    case "急诊": colorShow = "clolor_emer";
                        break;
                    case "住院中": colorShow = "clolor_inHos";
                        break;
                    case "当前住院中": colorShow = "clolor_inHos";
                        break;
                    default: break;
                }
                return colorShow;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsClinicalInfo.GetColor", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }
        #endregion
        #region <PsDiagnosis>
        //得到诊断信息 LY 2015-10-10
        public List<DiagnosisInfo> PsDiagnosisGetDiagnosisInfo(DataConnection pclsCache, string UserId, string VisitId)
        {
            List<DiagnosisInfo> list = new List<DiagnosisInfo>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Diagnosis.GetDiagnosisInfo(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;
                cmd.Parameters.Add("VisitId", CacheDbType.NVarChar).Value = VisitId;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    string RecordDateShow = "";
                    string str1 = cdr["RecordDate"].ToString();
                    if (str1 != "")
                    {
                        RecordDateShow = str1.Substring(0, 4) + "-" + str1.Substring(4, 2) + "-" + str1.Substring(6, 2);
                    }
                    DiagnosisInfo NewLine = new DiagnosisInfo();
                    NewLine.VisitId = cdr["VisitId"].ToString();
                    NewLine.DiagnosisType = cdr["DiagnosisType"].ToString();
                    NewLine.DiagnosisTypeName = cdr["DiagnosisTypeName"].ToString();
                    NewLine.DiagnosisNo = cdr["DiagnosisNo"].ToString();
                    NewLine.Type = cdr["Type"].ToString();
                    NewLine.TypeName = cdr["TypeName"].ToString();
                    NewLine.DiagnosisCode = cdr["DiagnosisCode"].ToString();
                    NewLine.DiagnosisName = cdr["DiagnosisName"].ToString();
                    NewLine.Description = cdr["Description"].ToString();
                    NewLine.RecordDate = cdr["RecordDate"].ToString();
                    NewLine.RecordDateShow = RecordDateShow;
                    NewLine.Creator = cdr["Creator"].ToString();
                    NewLine.RecordDateCom = Convert.ToDateTime(cdr["RecordDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    list.Add(NewLine);
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Diagnosis.GetDiagnosisInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        #region <PsExamination>
        //得到检查信息 LY 2015-10-10
        public List<ExamInfo> PsExaminationGetExaminationList(DataConnection pclsCache, string piUserId, string piVisitId)
        {
            List<ExamInfo> list = new List<ExamInfo>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Examination.GetExaminationList(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("piUserId", CacheDbType.NVarChar).Value = piUserId;
                cmd.Parameters.Add("piVisitId", CacheDbType.NVarChar).Value = piVisitId;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    string ReportDateShow = "";
                    if (cdr["ReportDate"].ToString() == "9999/1/1 0:00:00")
                    {
                        ReportDateShow = "";
                    }
                    else
                    {
                        ReportDateShow = cdr["ReportDate"].ToString();
                    }
                    ExamInfo NewLine = new ExamInfo();
                    NewLine.VisitId = cdr["VisitId"].ToString();
                    NewLine.SortNo = cdr["SortNo"].ToString();
                    NewLine.ExamType = cdr["ExamType"].ToString();
                    NewLine.ExamTypeName = cdr["ExamTypeName"].ToString();
                    NewLine.ExamDate = cdr["ExamDate"].ToString();
                    NewLine.ItemCode = cdr["ItemCode"].ToString();
                    NewLine.ItemName = cdr["ItemName"].ToString();
                    NewLine.ExamPara = cdr["ExamPara"].ToString();
                    NewLine.Description = cdr["Description"].ToString();
                    NewLine.Impression = cdr["Impression"].ToString();
                    NewLine.Recommendation = cdr["Recommendation"].ToString();
                    NewLine.IsAbnormalCode = cdr["IsAbnormalCode"].ToString();
                    NewLine.IsAbnormal = cdr["IsAbnormal"].ToString();
                    NewLine.StatusCode = cdr["StatusCode"].ToString();
                    NewLine.Status = cdr["Status"].ToString();
                    NewLine.ReqortDate = ReportDateShow;
                    NewLine.ImageURL = cdr["ImageURL"].ToString();
                    NewLine.DeptCode = cdr["DeptCode"].ToString();
                    NewLine.DeptName = cdr["DeptName"].ToString();
                    NewLine.Creator = cdr["Creator"].ToString();
                    NewLine.ExamDateCom = Convert.ToDateTime(cdr["ExamDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    list.Add(NewLine);
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Examination.GetExaminationList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region Ps.LabTest
        //SYF 2015-10-10
        /// <summary>
        /// 根据piUserId和piVisitId获取化验结果
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="piUserId"></param>
        /// <param name="piVisitId"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetLabTestList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        
        #region Ps.DrugRecord
        //syf 2015-10-10
        /// <summary>
        /// 根据piUserId和piVisitId获取用药结果
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="piUserId"></param>
        /// <param name="piVisitId"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetDrugRecordList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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