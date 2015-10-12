using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;


namespace CDMISrestful.DataMethod
{

    public class ClinicInfoMethod
    {
        #region <PsClinicalInfo>
        /// <summary>
        /// 住院-转科处理 LY 2015-10-10  
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetClinicalInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// 检查化验等信息 LY 2015-10-10
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
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
                //用药    
                List<DrugRecord> List_DrugRecord = new List<DrugRecord>();
                List_DrugRecord = GetDrugRecord(pclsCache, UserId, VisitId);
                for (int i = 0; i < List_DrugRecord.Count; i++)
                {
                    ClinicalTrans NewLine = new ClinicalTrans();
                    NewLine.精确时间 = Convert.ToDateTime(List_DrugRecord[i].StartDateTime);
                    NewLine.类型 = "用药";
                    NewLine.VisitId = VisitId;
                    NewLine.事件 = "用药：" + List_DrugRecord[i].HistoryContent;
                    NewLine.关键属性 = "DrugRecord" + "|" + VisitId + "|" + Convert.ToDateTime(List_DrugRecord[i].StartDateTime).ToString("yyyy-MM-ddHH:mm:ss");
                    List_Trans.Add(NewLine);
                }
                return List_Trans;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetClinicalInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 获取门诊的下一日期 LY 2015-10-10
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="ClinicDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取住院的下一日期 LY 2015-10-10
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="AdmissionDate"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetNextDatebyDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 颜色分配：根据首标签决定 LY 2015-10-10
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetColor", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }

        //检查化验详细信息查看
        public VClinicInfo GetClinicInfoDetail(DataConnection pclsCache, string UserId, string Type, string VisitId, string Date)
        {
            VClinicInfo list = new VClinicInfo();
            List<DiagnosisInfo> DiagnosisInfo_DataViewModel = null;
            List<ExamInfo> ExamInfo_DataViewModel = null;
            List<LabTestList> LabTestList_DataViewModel = null;
            List<DrugRecordList> DrugRecordList_DataViewModel = null;
            string condition = "";
            try
            {
                switch (Type)
                {
                    case "DiagnosisInfo": DiagnosisInfo_DataViewModel = new ClinicInfoMethod().PsDiagnosisGetDiagnosisInfo(pclsCache, UserId, VisitId);//诊断表
                                          List<DiagnosisInfo> DiagnosisInfo_DataViewModel_copy = null;
                                          foreach(DiagnosisInfo item in DiagnosisInfo_DataViewModel)
                                          {
                                               if(item.RecordDateCom == Date)
                                               {
                                                    DiagnosisInfo_DataViewModel_copy.Add(item);
                                               }
                                           }
                                           list.DiagnosisInfo_DataViewModel = DiagnosisInfo_DataViewModel_copy;
                        break;

                    case "ExaminationInfo": ExamInfo_DataViewModel = new ClinicInfoMethod().PsExaminationGetExaminationList(pclsCache, UserId, VisitId); //检查表（有子表）
                                            List<ExamInfo> ExamInfo_DataViewModel_copy = null;
                                            foreach (ExamInfo item in ExamInfo_DataViewModel)
                                            {
                                              if (item.ExamDateCom == Date)
                                               {
                                                   ExamInfo_DataViewModel_copy.Add(item);
                                               }
                                            }
                                            list.ExamInfo_DataViewModel = ExamInfo_DataViewModel_copy;
                        break;

                    case "LabTestInfo": LabTestList_DataViewModel = new ClinicInfoMethod().GetLabTestList(pclsCache, UserId, VisitId); //化验表（有子表）
                                        List<LabTestList> LabTestList_DataViewModel_copy = null;
                                        foreach (LabTestList item in LabTestList_DataViewModel)
                                        {
                                              if (item.LabTestDateCom == Date)
                                              {
                                                   LabTestList_DataViewModel_copy.Add(item);
                                              }
                                         }
                                        list.LabTestList_DataViewModel = LabTestList_DataViewModel_copy;
                        break;
                    case "DrugRecord": DrugRecordList_DataViewModel = new ClinicInfoMethod().GetDrugRecordList(pclsCache, UserId, VisitId); //用药
                                       condition = "StartDateTimeCom = '" + Date + "'";
                                       list.DrugRecordList_DataViewModel = DrugRecordList_DataViewModel;
                        break;
                    default: break;
                }
                
                //list肯定不为空



                
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetClinicInfoDetail", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        
        finally
            {
                pclsCache.DisConnect();
            }
    }
        #endregion


        #region <PsDiagnosis>
        /// <summary>
        /// 得到诊断信息 LY 2015-10-10
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetDiagnosisInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        /// 得到检查信息 LY 2015-10-10
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="piUserId"></param>
        /// <param name="piVisitId"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetExaminationList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// CSQ 20150714 //20151012 SYF
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<NewExam> GetNewExam(DataConnection pclsCache, string UserId)
        {
            List<NewExam> list = new List<NewExam>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Examination.GetNewExam(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    NewExam NewLine = new NewExam();
                    NewLine.Name1 = cdr["Name1"].ToString();
                    NewLine.Value1 = cdr["Value1"].ToString();
                    NewLine.Name2 = cdr["Name2"].ToString();
                    NewLine.Value2 = cdr["Value2"].ToString();
                    NewLine.Name3 = cdr["Name3"].ToString();
                    NewLine.Value3 = cdr["Value3"].ToString();
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetNewExam", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// GetNewLabTest CSQ 20150714 //20151012 SYF
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<NewLabTest> GetNewLabTest(DataConnection pclsCache, string UserId)
        {
            List<NewLabTest> list = new List<NewLabTest>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.LabTestDetails.GetNewLabTest(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;
                cdr = cmd.ExecuteReader();

                while (cdr.Read())
                {
                    NewLabTest NewLine = new NewLabTest();
                    NewLine.Code = cdr["Code"].ToString();
                    NewLine.Name = cdr["Name"].ToString();
                    NewLine.Value = cdr["Value"].ToString();
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetNewLabTest", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// GetLabTestDetails 获取患者某次化验的所有详细信息 ZC 2014-12-2 //CSQ 2015-06-25 //syf20151012
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="VisitId"></param>
        /// <param name="SortNo"></param>
        /// <returns></returns>
        public List<LabTestDetails> GetLabTestDetails(DataConnection pclsCache, string UserId, string VisitId, string SortNo)
        {
            List<LabTestDetails> list = new List<LabTestDetails>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.LabTestDetails.GetLabTestDetails(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;
                cmd.Parameters.Add("VisitId", CacheDbType.NVarChar).Value = VisitId;
                cmd.Parameters.Add("SortNo", CacheDbType.NVarChar).Value = SortNo;
                cdr = cmd.ExecuteReader();

                while (cdr.Read())
                {
                    LabTestDetails NewLine = new LabTestDetails();
                    NewLine.Code = cdr["Code"].ToString();
                    NewLine.Name = cdr["Name"].ToString();
                    NewLine.Value = cdr["Value"].ToString();
                    NewLine.IsAbnormalCode = cdr["IsAbnormalCode"].ToString();
                    NewLine.IsAbnormal = cdr["IsAbnormal"].ToString();
                    NewLine.UnitCode = cdr["UnitCode"].ToString();
                    NewLine.Unit = cdr["Unit"].ToString();
                    NewLine.Creator = cdr["Creator"].ToString(); 
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetLabTestDetails", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// SYF 20151012
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="piUserId"></param>
        /// <param name="piVisitId"></param>
        /// <returns></returns>
        public List<DrugRecord> GetDrugRecord(DataConnection pclsCache, string piUserId, string piVisitId)
        {
            List<DrugRecord> list = new List<DrugRecord>();
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
                    int OrderSubNo = 0;
                    int FreqCounter = 0;
                    int FreqInteval = 0;
                    if (cdr["OrderSubNo"].ToString() != "")
                    {
                        OrderSubNo = Convert.ToInt32(cdr["OrderSubNo"].ToString());
                    }

                    if (OrderSubNo == 1)       //只拿出OrderSubNo为1的记录
                    {
                        if (cdr["FreqCounter"].ToString() != "")
                        {
                            FreqCounter = Convert.ToInt32(cdr["FreqCounter"].ToString());
                        }
                        if (cdr["FreqInteval"].ToString() != "")
                        {
                            FreqInteval = Convert.ToInt32(cdr["FreqInteval"].ToString());
                        }

                        //用药开始时间处理（时间轴标准）
                        string StartDate = DateTime.Parse(cdr["StartDateTime"].ToString()).ToString("yyyyMMdd");

                        //用药结束时间处理
                        string EndDate = cdr["StopDateTime"].ToString();

                        string Content = "";
                        Content += cdr["OrderContent"].ToString();
                        DrugRecord NewLine = new DrugRecord();
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
                        NewLine.HistoryContent = cdr["HistoryContent"].ToString();
                        NewLine.StartDate = cdr["StartDate"].ToString();
                        NewLine.StopDate = cdr["StopDate"].ToString();
                    }
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

        /// <summary>
        /// syf 2015-10-12 获取某病人所有药嘱
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="piUserId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
        public List<PsDrugRecord> GetPsDrugRecord(DataConnection pclsCache, string piUserId, string Module)
        {
            List<PsDrugRecord> list = new List<PsDrugRecord>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.DrugRecord.GetPsDrugRecord(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("piUserId", CacheDbType.NVarChar).Value = piUserId;
                cmd.Parameters.Add("Module", CacheDbType.NVarChar).Value = Module;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    PsDrugRecord NewLine = new PsDrugRecord();
                    NewLine.VisitId = cdr["VisitId"].ToString();
                    NewLine.OrderNo = cdr["OrderNo"].ToString();
                    NewLine.OrderSubNo = cdr["OrderSubNo"].ToString();
                    NewLine.RepeatIndicator = cdr["RepeatIndicator"].ToString();
                    NewLine.OrderClass = cdr["OrderClass"].ToString();
                    NewLine.OrderCode = cdr["OrderCode"].ToString();
                    NewLine.DrugName = cdr["DrugName"].ToString();
                    NewLine.CurativeEffect = cdr["CurativeEffect"].ToString();
                    NewLine.SideEffect = cdr["SideEffect"].ToString();
                    NewLine.Instruction = cdr["Instruction"].ToString();
                    NewLine.HealthEffect = cdr["HealthEffect"].ToString();
                    NewLine.Unit = cdr["Unit"].ToString();
                    NewLine.OrderContent = cdr["OrderContent"].ToString();
                    NewLine.Dosage = cdr["Dosage"].ToString();
                    NewLine.DosageUnits = cdr["DosageUnits"].ToString();
                    NewLine.Administration = cdr["Administration"].ToString();
                    NewLine.StartDateTime = cdr["StartDateTime"].ToString();
                    NewLine.StopDateTime = cdr["StopDateTime"].ToString();
                    NewLine.Frequency = cdr["Frequency"].ToString();
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "ClinicInfoMethod.GetPsDrugRecord", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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