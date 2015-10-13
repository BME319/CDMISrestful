using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;
using System.Data;

namespace CDMISrestful.DataMethod
{
    public class PlanInfoMethod
    {
        
            //Ps.Plan
            #region Ps.Plan
            //SYF 20151010
            public int PsPlanSetData(DataConnection pclsCache, string PlanNo, string PatientId, int StartDate, int EndDate, string Module, int Status, string DoctorId, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
            {
                int ret = 0;
                try
                {
                    if (!pclsCache.Connect())
                    {
                        return ret;
                    }

                    ret = (int)Ps.Plan.SetData(pclsCache.CacheConnectionObject, PlanNo, PatientId, StartDate, EndDate, Module, Status, DoctorId, piUserId, piTerminalName, piTerminalIP, piDeviceType);
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Plan.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return ret;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //SYF 20151010
            public int ChangePlanStatus(DataConnection pclsCache, string PlanNo, int Status, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
            {
                int ret = 0;

                try
                {
                    if (!pclsCache.Connect())
                    {
                        return ret;
                    }

                    ret = (int)Ps.Plan.PlanStart(pclsCache.CacheConnectionObject, PlanNo, Status, piUserId, piTerminalName, piTerminalIP, piDeviceType);
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Plan.PlanStart", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return ret;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //GetWeekPeriod 获取某计划的相关信息 SYF 2015-10-10
            public Period GetWeekPeriod(DataConnection pclsCache, int PlanStartDate)
            {
                try
                {
                    Period ret = new Period();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    ret.StartDate = Ps.Plan.GetWeekPeriod(pclsCache.CacheConnectionObject, PlanStartDate)[0];
                    ret.EndDate = Ps.Plan.GetWeekPeriod(pclsCache.CacheConnectionObject, PlanStartDate)[1];
                    ret.DayCount = Ps.Plan.GetWeekPeriod(pclsCache.CacheConnectionObject, PlanStartDate)[2];
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsPlan.GetWeekPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //GetProgressRate 获取某计划的进度和剩余天数 SYF 2015-10-10
            public Progressrate GetProgressRate(DataConnection pclsCache, string PlanNo)
            {
                try
                {
                    Progressrate ret = new Progressrate();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    ret.RemainingDays = Ps.Plan.GetProgressRate(pclsCache.CacheConnectionObject, PlanNo)[0];
                    ret.ProgressRate = Ps.Plan.GetProgressRate(pclsCache.CacheConnectionObject, PlanNo)[1];
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsPlan.GetProgressRate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //GetPlanInfo 获取某计划的相关信息 SYF 2015-10-10
            public GPlanInfo GetPlanInfo(DataConnection pclsCache, string PlanNo)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    ret.PlanNo = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[0];
                    ret.PatientId = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[1];
                    ret.StartDate = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[2];
                    ret.EndDate = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[3];
                    ret.Module = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[4];
                    ret.Status = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[5];
                    ret.DoctorId = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo)[6];
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsPlan.GetPlanInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //SYF 20151010 获取健康专员负责的所有患者列表
            public List<PatientPlan> GetPatientsPlanByDoctorId(DataConnection pclsCache, string DoctorId, string Module)
            {
                List<PatientPlan> list = new List<PatientPlan>();
                CacheCommand cmd = null;
                CacheDataReader cdr = null;
                try
                {
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    cmd = new CacheCommand();
                    cmd = Ps.Plan.GetPatientsPlanByDoctorId(pclsCache.CacheConnectionObject);
                    cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;
                    cmd.Parameters.Add("Module", CacheDbType.NVarChar).Value = Module;

                    cdr = cmd.ExecuteReader();
                    while (cdr.Read())
                    {
                        //DataCheck ZAM 2015-4-16
                        if (cdr["PatientId"].ToString() == string.Empty)
                        {
                            continue;
                        }
                        PatientPlan NewLine = new PatientPlan();
                        NewLine.PatientId = cdr["PatientId"].ToString();
                        NewLine.PlanNo = cdr["PlanNo"].ToString();
                        NewLine.StartDate = cdr["StartDate"].ToString();
                        NewLine.EndDate = cdr["EndDate"].ToString();
                        NewLine.TotalDays = cdr["TotalDays"].ToString();
                        NewLine.RemainingDays = cdr["RemainingDays"].ToString();
                        NewLine.Status = cdr["Status"].ToString(); 
                        
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Plan.GetPatientsPlanByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

            // SYF 2015-10-10 获取健康专员负责的所有患者最新结束(status = 4)计划列表
            public List<PatientPlan> GetOverDuePlanByDoctorId(DataConnection pclsCache, string DoctorId, string Module)
            {
                List<PatientPlan> list = new List<PatientPlan>();
                CacheCommand cmd = null;
                CacheDataReader cdr = null;
                try
                {
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    cmd = new CacheCommand();
                    cmd = Ps.Plan.GetOverDuePlanByDoctorId(pclsCache.CacheConnectionObject);
                    cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;
                    cmd.Parameters.Add("Module", CacheDbType.NVarChar).Value = Module;

                    cdr = cmd.ExecuteReader();
                    while (cdr.Read())
                    {
                        PatientPlan NewLine = new PatientPlan();
                        NewLine.PatientId = cdr["PatientId"].ToString();
                        NewLine.PlanNo = cdr["PlanNo"].ToString();
                        NewLine.StartDate = cdr["StartDate"].ToString();
                        NewLine.EndDate = cdr["EndDate"].ToString();
                        NewLine.TotalDays = cdr["TotalDays"].ToString();
                        NewLine.RemainingDays = cdr["RemainingDays"].ToString();
                        NewLine.Status = cdr["Status"].ToString(); 
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Plan.GetOverDuePlanByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

            //GetExecutingPlanByM  获取某模块正在执行的计划 Syf 2015-10-10
            public GPlanInfo GetExecutingPlanByM(DataConnection pclsCache, string PatientId, string Module)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    ret.PlanNo = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[0];
                    ret.PatientId = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[1];
                    ret.StartDate = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[2];
                    ret.EndDate = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[3];
                    ret.Module = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[4];
                    ret.Status = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[5];
                    ret.DoctorId = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[6];
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsPlan.GetExecutingPlanByM", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //GetExecutingPlan 获取正在执行的计划 SYF 2015-10-10
            public GPlanInfo GetExecutingPlan(DataConnection pclsCache, string PatientId)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    ret.PlanNo = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[0];
                    ret.PatientId = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[1];
                    ret.StartDate = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[2];
                    ret.EndDate = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[3];
                    ret.Module = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[4];
                    ret.Status = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[5];
                    ret.DoctorId = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId)[6];
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsPlan.GetExecutingPlan", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //GetPlanList34ByM 获取某模块患者的正在执行的和结束的计划列表 GL 2015-10-12
            public List<PlanDeatil> GetPlanList34ByM(DataConnection pclsCache, string PatientId, string Module)
            {
                List<PlanDeatil> result = new List<PlanDeatil>();
                try
                {
                    GPlanInfo list = GetExecutingPlanByM(pclsCache, PatientId, Module);
                    if (list != null)
                    {
                        PlanDeatil PlanDeatil = new PlanDeatil();
                        PlanDeatil.PlanNo = list.PlanNo;
                        PlanDeatil.StartDate = Convert.ToInt32(list.StartDate);
                        PlanDeatil.EndDate = Convert.ToInt32(list.EndDate);
                        string temp = PlanDeatil.StartDate.ToString().Substring(0, 4) + "/" + PlanDeatil.StartDate.ToString().Substring(4, 2) + "/" + PlanDeatil.StartDate.ToString().Substring(6, 2);
                        string temp1 = PlanDeatil.EndDate.ToString().Substring(0, 4) + "/" + PlanDeatil.EndDate.ToString().Substring(4, 2) + "/" + PlanDeatil.EndDate.ToString().Substring(6, 2);
                        PlanDeatil.PlanName = "当前计划：" + temp + "-" + temp1;
                        result.Add(PlanDeatil);
                    }
                    else
                    {
                        PlanDeatil PlanDeatil = new PlanDeatil();
                        PlanDeatil.PlanNo = "";
                        PlanDeatil.PlanName = "当前计划";
                        result.Add(PlanDeatil);
                    }


                    List<PlanDeatil> endingPlanList = new List<PlanDeatil>();
                    endingPlanList = GetEndingPlan(pclsCache, PatientId, Module);
                    foreach (PlanDeatil item in endingPlanList)
                    {
                        PlanDeatil PlanDeatil = new PlanDeatil();
                        PlanDeatil.PlanNo = item.PlanNo;
                        PlanDeatil.StartDate = item.StartDate;
                        PlanDeatil.EndDate = item.EndDate;
                        string temp = PlanDeatil.StartDate.ToString().Substring(0, 4) + "/" + PlanDeatil.StartDate.ToString().Substring(4, 2) + "/" + PlanDeatil.StartDate.ToString().Substring(6, 2);
                        string temp1 = PlanDeatil.EndDate.ToString().Substring(0, 4) + "/" + PlanDeatil.EndDate.ToString().Substring(4, 2) + "/" + PlanDeatil.EndDate.ToString().Substring(6, 2);
                        PlanDeatil.PlanName = "往期：" + temp + "-" + temp1;
                        result.Add(PlanDeatil);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetPlanList34ByM", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }
            //GetEndingPlan 获取某模块已经结束的计划 GL 2015-10-12
            public List<PlanDeatil> GetEndingPlan(DataConnection pclsCache, string PatientId, string Module)
            {
                List<PlanDeatil> items = new List<PlanDeatil>();
                CacheCommand cmd = null;
                CacheDataReader cdr = null;
                try
                {
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    cmd = new CacheCommand();
                    cmd = Ps.Plan.GetPlanList4ByM(pclsCache.CacheConnectionObject);
                    cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                    cmd.Parameters.Add("Module", CacheDbType.NVarChar).Value = Module;

                    cdr = cmd.ExecuteReader();
                    while (cdr.Read())
                    {
                        PlanDeatil item = new PlanDeatil();
                        item.PlanNo = cdr["PlanNo"].ToString();
                        item.StartDate = Convert.ToInt32(cdr["StartDate"]);
                        item.EndDate = Convert.ToInt32(cdr["EndDate"]);
                        items.Add(item);
                    }
                    return items;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetEndingPlan", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

            //Ps.Compliance
            #region Ps.Compliance
            // 施宇帆 2015-10-10 插入子表PsComplianceDetail某条数据
           public int PsComplianceDetailSetData(DataConnection pclsCache, string Parent, string Id, int Status, string CoUserId, string CoTerminalName, string CoTerminalIP, int CoDeviceType)
           {
                int ret = 0;
                try
                {
                     if (!pclsCache.Connect())
                     {
                         return ret;
                     }

                     ret = (int)Ps.ComplianceDetail.SetData(pclsCache.CacheConnectionObject, Parent, Id, Status, CoUserId, CoTerminalName, CoTerminalIP, CoDeviceType);
                     return ret;
                }
                catch (Exception ex)
                {
                     HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.ComplianceDetail.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                     return ret;
                }
                finally
                {
                     pclsCache.DisConnect();
                }
            }

           // syf 2015-10-10 插入PsCompliance某条数据
           public int PsComplianceSetData(DataConnection pclsCache, string PatientId, int Date, string PlanNo, Double Compliance, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
           {
               int ret = 0;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return ret;
                   }

                   ret = (int)Ps.Compliance.SetData(pclsCache.CacheConnectionObject, PatientId, Date, PlanNo, Compliance, revUserId, TerminalName, TerminalIP, DeviceType);
                   return ret;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return ret;
               }
               finally
               {
                   pclsCache.DisConnect();
               }
           }

           //GetTasksComByPeriod  某一天所有任务的依从情况 DataTable数据库形式   用于点击事件显示详细   SYF 20151010
           public List<TasksComList> GetTasksComListByDate(DataConnection pclsCache, string PatientId, string PlanNo, int Date)
           {
               List<TasksComList> list = new List<TasksComList>(); 

               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetTasksComListByDate(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("Date", CacheDbType.Int).Value = Date;

                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       TasksComList NewLine = new TasksComList();
                       NewLine.Date = cdr["Date"].ToString();
                       NewLine.ComplianceValue = cdr["ComplianceValue"].ToString();
                       NewLine.TaskType = cdr["TaskType"].ToString();
                       NewLine.TaskId = cdr["TaskId"].ToString();
                       NewLine.TaskName = cdr["TaskName"].ToString();
                       NewLine.Status = cdr["Status"].ToString();
                       NewLine.TaskCode = cdr["TaskCode"].ToString();
                       NewLine.Type = cdr["Type"].ToString();
                       list.Add(NewLine);
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetTasksComListByDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           //GetTasksComByPeriod  某段时间所有任务的依从情况  DataTable数据库形式  SYF 20151010
           public List<TasksComByPeriodDT> GetTasksComByPeriodDT(DataConnection pclsCache, string PatientId, string PlanNo, int StartDate, int EndDate)
           {

               List<TasksComByPeriodDT> list = new List<TasksComByPeriodDT>(); 

               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetTasksComByPeriod(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("StartDate", CacheDbType.Int).Value = StartDate;
                   cmd.Parameters.Add("EndDate", CacheDbType.Int).Value = EndDate;

                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       TasksComByPeriodDT NewLine = new TasksComByPeriodDT();
                       NewLine.Date = cdr["Date"].ToString();
                       NewLine.ComplianceValue = cdr["ComplianceValue"].ToString();
                       NewLine.TaskType = cdr["TaskType"].ToString();
                       NewLine.TaskId = cdr["TaskId"].ToString();
                       NewLine.TaskName = cdr["TaskName"].ToString();
                       NewLine.Status = cdr["Status"].ToString();
                       NewLine.Type = cdr["Type"].ToString(); 
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetTasksComByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           // syf 2015-10-10根据计划编码和日期，获取依从率
           public List<TasksByDate> GetTasksByDate(DataConnection pclsCache, string PatientId, int Date, string PlanNo)
           {
               List<TasksByDate> list = new List<TasksByDate>(); 

               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetTasksByDate(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("Date", CacheDbType.Int).Value = Date;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;

                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       TasksByDate NewLine = new TasksByDate();
                       NewLine.TaskID = cdr["TaskID"].ToString();
                       NewLine.TaskName = cdr["TaskName"].ToString();
                       NewLine.Status = cdr["Status"].ToString();
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetTasksByDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           // syf 2015-10-10 在当天根据任务状态的完成情况输出相应的任务
           public List<TasksByStatus> GetTaskByStatus(DataConnection pclsCache, string PatientId, string PlanNo, int PiStatus)
           {
               List<TasksByStatus> list = new List<TasksByStatus>();
               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetTaskByStatus(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("PiStatus", CacheDbType.Int).Value = PiStatus;

                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       TasksByStatus NewLine = new TasksByStatus();
                       NewLine.Id = cdr["Id"].ToString();
                       NewLine.Status = cdr["Status"].ToString();
                       NewLine.TaskCode = cdr["TaskCode"].ToString();
                       NewLine.TaskName = cdr["TaskName"].ToString();
                       NewLine.TaskType = cdr["TaskType"].ToString();
                       NewLine.Instruction = cdr["Instruction"].ToString();
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetTaskByStatus", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           //GetSignDetailByPeriod 通过Ps.Compliance中的date获取当天某项生理参数值，形成系列  DataTable 形式syf 2015-10-10
           public List<SignDetailByPeriod> GetSignDetailByPeriod(DataConnection pclsCache, string PatientId, string PlanNo, string ItemType, string ItemCode, int StartDate, int EndDate)
           {
               List<SignDetailByPeriod> list = new List<SignDetailByPeriod>();
               CacheCommand cmd = null;
               CacheDataReader cdr = null;


               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }

                   cmd = new CacheCommand();
                   cmd = Ps.ComplianceDetail.GetSignDetailByPeriod(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("ItemType", CacheDbType.NVarChar).Value = ItemType;
                   cmd.Parameters.Add("ItemCode", CacheDbType.NVarChar).Value = ItemCode;
                   cmd.Parameters.Add("StartDate", CacheDbType.NVarChar).Value = StartDate;
                   cmd.Parameters.Add("EndDate", CacheDbType.NVarChar).Value = EndDate;

                   cdr = cmd.ExecuteReader();

                   while (cdr.Read())
                   {
                       SignDetailByPeriod NewLine = new SignDetailByPeriod();
                       NewLine.RecordDate = cdr["RecordDate"].ToString();
                       NewLine.RecordTime = cdr["RecordTime"].ToString();
                       NewLine.Value = cdr["Value"].ToString();
                       NewLine.Unit = cdr["Unit"].ToString();
                   }

                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsCompliacne.GetSignDetailByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           // syf 2015-10-10 获取某计划的某段时间(包括端点)的依从率列表
           public List<ComplianceListByPeriod> GetComplianceListByPeriod(DataConnection pclsCache, string PatientId, string PlanNo, int StartDate, int EndDate)
           {
               List<ComplianceListByPeriod> list = new List<ComplianceListByPeriod>();

               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetComplianceListByPeriod(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("StartDate", CacheDbType.Int).Value = StartDate;
                   cmd.Parameters.Add("EndDate", CacheDbType.Int).Value = EndDate;

                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       ComplianceListByPeriod NewLine = new ComplianceListByPeriod();
                       NewLine.Date = cdr["Date"].ToString();
                       NewLine.Compliance = cdr["Compliance"].ToString();
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetComplianceListByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           // syf 2015-10-10 获取患者某计划内某天的依从率
           public double GetComplianceByDay(DataConnection pclsCache, string PatientId, int Date, string PlanNo)
           {
               double ret = 0.0;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return ret;
                   }

                   ret = (double)Ps.Compliance.GetComplianceByDay(pclsCache.CacheConnectionObject, PatientId, Date, PlanNo);
                   return ret;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Ps.Compliance.GetComplianceByDay", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return ret;
               }
               finally
               {
                   pclsCache.DisConnect();
               }
           }

           //GetCompliacneRate 计算某段时间的总依从率 syf 2015-10-10
           public string GetCompliacneRate(DataConnection pclsCache, string PatientId, string PlanNo, int StartDate, int EndDate)
           {
               string compliacneRate = "";
               CacheCommand cmd = null;
               CacheDataReader cdr = null;

               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }

                   cmd = new CacheCommand();
                   cmd = Ps.Compliance.GetComplianceListByPeriod(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                   cmd.Parameters.Add("PlanNo", CacheDbType.NVarChar).Value = PlanNo;
                   cmd.Parameters.Add("StartDate", CacheDbType.NVarChar).Value = StartDate;
                   cmd.Parameters.Add("EndDate", CacheDbType.NVarChar).Value = EndDate;

                   cdr = cmd.ExecuteReader();

                   double sum = 0;
                   int count = 0;
                   while (cdr.Read())
                   {
                       sum += Convert.ToDouble(cdr["Compliance"]);
                       count++;
                   }

                   if (count != 0)
                   {
                       //compliacneRate = ((int)((sum / count) * 100)).ToString();
                       compliacneRate = (Math.Round(sum / count, 2, MidpointRounding.AwayFromZero) * 100).ToString(); //保留整数

                   }

                   return compliacneRate;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsCompliacne.GetCompliacneRate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           //所有任务的依从情况简化版 不再全部显示，只对完成数量就行统计  pad、phone使用中 GL 2015-10-12
           public List<CompliacneDetailByD> GetTasksComCountByPeriod(DataConnection pclsCache, string PatientId, string PlanNo, int StartDate, int EndDate)
           {

               List<CompliacneDetailByD> resultList = new List<CompliacneDetailByD>();

               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   //DataTable list = new DataTable();
                   //list = PsCompliance.GetTasksComByPeriodDT(pclsCache, PatientId, PlanNo, StartDate, EndDate);
                   List<TasksComByPeriodDT> list0 = GetTasksComByPeriodDT(pclsCache, PatientId, PlanNo, StartDate, EndDate);

                   DataTable list = new DataTable();
                   list.Columns.Add(new DataColumn("Date", typeof(string)));
                   list.Columns.Add(new DataColumn("ComplianceValue", typeof(double)));
                   list.Columns.Add(new DataColumn("TaskType", typeof(string))); //中文
                   list.Columns.Add(new DataColumn("TaskId", typeof(string)));
                   list.Columns.Add(new DataColumn("TaskName", typeof(string)));
                   list.Columns.Add(new DataColumn("Status", typeof(int)));
                   list.Columns.Add(new DataColumn("Type", typeof(string))); //英文

                   foreach(TasksComByPeriodDT item in list0)
                   {
                       list.Rows.Add(item.Date, item.ComplianceValue, item.TaskType, item.TaskId, item.TaskName, item.Status, item.Type);
                   }

                   //确保排序
                   DataView dv = list.DefaultView;
                   dv.Sort = "Date Asc, Type desc, Status Asc"; //体征s 生活l 用药d   前提：某计划内任务维持不变  即计划内每天的任务是一样的
                   DataTable list_sort = dv.ToTable();
                   list_sort.Rows.Add("end", 0, "", "", "", 0);  //用于最后一天输出

                   if (list_sort.Rows.Count > 1)
                   {
                       string temp_date = list_sort.Rows[0]["Date"].ToString();
                       string temp_type = list_sort.Rows[0]["TaskType"].ToString();  //中文
                       int complete = 0; int count = 0;  //完成数量统计


                       string temp_str = "";
                       temp_str += "该天依从率：" + list_sort.Rows[0]["ComplianceValue"].ToString() + "<br>";
                       temp_str += "<b><span style='font-size:14px;'>" + list_sort.Rows[0]["TaskType"].ToString() + "：</span></b>";

                       CompliacneDetailByD CompliacneDetailByD = new CompliacneDetailByD();
                       CompliacneDetailByD.Date = list_sort.Rows[0]["Date"].ToString();
                       //CompliacneDetailByD.ComplianceValue = list_sort.Rows[0]["ComplianceValue"].ToString();

                       if (Convert.ToDouble(list_sort.Rows[0]["ComplianceValue"]) == 0)  //点的颜色由该天依从率决定
                       {
                           CompliacneDetailByD.drugBullet = "";
                           CompliacneDetailByD.drugColor = "#DADADA";
                       }
                       else if (Convert.ToDouble(list_sort.Rows[0]["ComplianceValue"]) == 1)
                       {
                           CompliacneDetailByD.drugBullet = "";
                           CompliacneDetailByD.drugColor = "#777777";
                       }
                       else
                       {
                           CompliacneDetailByD.drugBullet = "amcharts-images/drug.png";
                           CompliacneDetailByD.drugColor = "";
                       }


                       if (Convert.ToInt32(list_sort.Rows[0]["Status"]) == 1)  //某天某项任务的完成情况
                       {
                           complete++;
                           count++;
                       }
                       else
                       {
                           count++;
                       }


                       //只有一条数据
                       if (list_sort.Rows.Count == 2)
                       {
                           temp_str += complete + "/" + count;
                           CompliacneDetailByD.Events = temp_str;
                           resultList.Add(CompliacneDetailByD);
                       }

                       //＞一条数据
                       if (list_sort.Rows.Count > 2)
                       {
                           for (int i = 1; i <= list_sort.Rows.Count - 1; i++)
                           {
                               if (temp_date == list_sort.Rows[i]["Date"].ToString())  //同一天
                               {
                                   if (temp_type == list_sort.Rows[i]["TaskType"].ToString())     //同天同任务类型
                                   {
                                       if (Convert.ToInt32(list_sort.Rows[i]["Status"]) == 1)  //某天某项任务的完成情况
                                       {
                                           complete++;
                                           count++;
                                       }
                                       else
                                       {
                                           count++;
                                       }
                                   }
                                   else   //同天不同任务类型
                                   {
                                       temp_str += complete + "/" + count;
                                       complete = 0; count = 0;  //清空统计量
                                       temp_str += "<br><b><span style='font-size:14px;'>" + list_sort.Rows[i]["TaskType"].ToString() + "：</span></b>";

                                       if (Convert.ToInt32(list_sort.Rows[i]["Status"]) == 1)  //某天某项任务的完成情况
                                       {
                                           complete++;
                                           count++;
                                       }
                                       else
                                       {
                                           count++;
                                       }

                                       temp_type = list_sort.Rows[i]["TaskType"].ToString();
                                   }

                               }
                               else   //不同天
                               {
                                   //上一天输出

                                   temp_str += complete + "/" + count;
                                   complete = 0; count = 0;  //清空统计量
                                   CompliacneDetailByD.Events = temp_str;
                                   resultList.Add(CompliacneDetailByD);

                                   if (list_sort.Rows[i]["Date"].ToString() != "end")
                                   {
                                       //获取新一天
                                       CompliacneDetailByD = new CompliacneDetailByD();
                                       CompliacneDetailByD.Date = list_sort.Rows[i]["Date"].ToString();
                                       //CompliacneDetailByD.ComplianceValue = list_sort.Rows[i]["ComplianceValue"].ToString();

                                       if (Convert.ToDouble(list_sort.Rows[i]["ComplianceValue"]) == 0)  //某天依从率
                                       {
                                           CompliacneDetailByD.drugBullet = "";
                                           CompliacneDetailByD.drugColor = "#DADADA";
                                       }
                                       else if (Convert.ToDouble(list_sort.Rows[i]["ComplianceValue"]) == 1)
                                       {
                                           CompliacneDetailByD.drugBullet = "";
                                           CompliacneDetailByD.drugColor = "#777777";
                                       }
                                       else
                                       {
                                           CompliacneDetailByD.drugBullet = "amcharts-images/drug.png";
                                           CompliacneDetailByD.drugColor = "";
                                       }

                                       temp_str = "";
                                       temp_str += "该天依从率：" + list_sort.Rows[i]["ComplianceValue"].ToString() + "<br>";
                                       temp_str += "<b><span style='font-size:14px;'>" + list_sort.Rows[i]["TaskType"].ToString() + "：</span></b>";

                                       if (Convert.ToInt32(list_sort.Rows[i]["Status"]) == 1)  //某天某项任务的完成情况
                                       {
                                           complete++;
                                           count++;
                                       }
                                       else
                                       {
                                           count++;
                                       }

                                       temp_date = list_sort.Rows[i]["Date"].ToString();
                                       temp_type = list_sort.Rows[i]["TaskType"].ToString();
                                   }
                               }
                           }

                       }

                   }

                   return resultList;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTasksComCountByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           //某天所有任务的依从情况 整理加工 GL 2015-10-12
           public TaskComDetailByD GetImplementationByDate(DataConnection pclsCache, string PatientId, string PlanNo, int Date)
           {
               TaskComDetailByD TaskComDetailByD = new TaskComDetailByD();
               try
               {
                   TaskComDetailByD.Date = Date.ToString().Substring(0, 4) + "-" + Date.ToString().Substring(4, 2) + "-" + Date.ToString().Substring(6, 2);
                   TaskComDetailByD.WeekDay = new CommonFunction().CaculateWeekDay(TaskComDetailByD.Date);

                   List<TasksComList> ComplianceList = new List<TasksComList>();
                   ComplianceList = GetTasksComListByDate(pclsCache, PatientId, PlanNo, Date);

                   #region 后期可能用于优化
                   //先读任务表，读取体征，拿出新数据；再读药物表，超过三个则省略号
                   //DataTable TaskList = new DataTable();
                   //TaskList = PsTask.GetTaskList(pclsCache, PlanNo);

                   ////读取体征，拿出当天最新数据
                   //string condition = " Type = 'VitalSign'";
                   //DataRow[] VitalSignRows = TaskList.Select(condition);

                   //CacheSysList VitalSignList = new InterSystems.Data.CacheTypes.CacheSysList(System.Text.Encoding.Unicode, true, true);
                   //for (int j=0; j < VitalSignRows.Length; j++)
                   //{
                   //    string code = VitalSignRows[j]["Type"].ToString();
                   //    string[] sArray = code.Split(new char[] { '|' });;//拆分
                   //    string type = sArray[0].ToString();
                   //    VitalSignList = new InterSystems.Data.CacheTypes.CacheSysList(System.Text.Encoding.Unicode, true, true);
                   //    VitalSignList = PsVitalSigns.GetSignByDay(pclsCache, PatientId, code, type, Date);
                   //    if (VitalSignList != null)
                   //    {

                   //    }
                   //}





                   //体征
                   /*
                   string condition = " Type = 'VitalSign'";
                   DataRow[] VitalSignRows = ComplianceList.Select(condition);

                   List<TaskCom> TaskComList = new List<TaskCom>();
                   TaskCom TaskCom = new TaskCom();
                   for (int j = 0; j < VitalSignRows.Length; j++)
                   {
                       VitalTaskCom = new VitalTaskCom();
                       VitalTaskCom.SignName = VitalSignRows[j]["TaskName"].ToString();
                       VitalTaskCom.Status = VitalSignRows[j]["Status"].ToString();
                       if (TaskCom.TaskStatus == "1")
                       {
                           string code = VitalSignRows[j]["TaskCode"].ToString();
                           string[] sArray = code.Split(new char[] { '|' }); ;//拆分
                           string type = sArray[0].ToString();
                           //CacheSysList VitalSignList = new InterSystems.Data.CacheTypes.CacheSysList(System.Text.Encoding.Unicode, true, true);
                           CacheSysList VitalSignList = new InterSystems.Data.CacheTypes.CacheSysList(System.Text.Encoding.Unicode, true, true);
                           VitalSignList = PsVitalSigns.GetSignByDay(pclsCache, PatientId, code, type, Date);
                           if (VitalSignList != null)
                           {
                            
                               VitalTaskCom.Time = PulList[1].ToString();
                               VitalTaskCom.Value = PulList[2].ToString();
                               VitalTaskCom.Unit = PulList[3].ToString();
                            
                           } 
                       }
                       VitalTaskComList.Add(VitalTaskCom);
                   }
                   TaskComDetailByD.VitalTaskComList = VitalTaskComList;

                
                    string vitalCondition = " Type = 'VitalSign'";
                       DataRow[] VitalSignRows = ComplianceList.Select(vitalCondition);

                       if ((VitalSignRows != null) && (VitalSignRows.Length >= 2))
                       {



                           if (VitalSignRows.Length == 2)  //只有血压
                           {

                           }
                           else //血压和脉率
                           {

                           }
                       }
                
                   */
                   #endregion

                   //取出当天的体征测量 若有与测试任务拼接好了
                   //先写死取的生理参数
                   List<VitalTaskCom> VitalTaskComList = new List<VitalTaskCom>();
                   VitalTaskCom VitalTaskCom = new VitalTaskCom();

                   string Module = "";
                   GPlanInfo planInfo = GetPlanInfo(pclsCache, PlanNo);
                   if (planInfo != null)
                   {
                       Module = planInfo.Module;
                   }

                   if (Module == "M1")
                   {
                       #region  高血压模块  需要考虑没有脉率任务的情况

                       //血压任务肯定有
                       int BPTime = 0;
                       int mark = 0;
                       string SysValue = "";
                       string DiaValue = "";
                       string Unit = "";

                       //string conditionBP1 = " TaskCode = 'Bloodpressure|Bloodpressure_1'";
                       List<TasksComList> BP1Rows = new List<TasksComList>();
                       foreach (TasksComList item in ComplianceList)
                       {
                           if (item.TaskCode == "Bloodpressure|Bloodpressure_1")
                           {
                               BP1Rows.Add(item);
                           }
                       }
                       if ((BP1Rows != null) && (BP1Rows.Count == 1))
                       {
                           if (BP1Rows[0].Status == "1")
                           {
                               VitalInfo SysList = new VitalInfoMethod().GetSignByDay(pclsCache, PatientId, "Bloodpressure", "Bloodpressure_1", Date);
                               if (SysList != null)
                               {
                                   mark = 1;
                                   BPTime = Convert.ToInt32(SysList.RecordTime);  //时刻数据库是"1043"形式，需要转换  取两者最新的那个时间好了 即谁大取谁
                                   SysValue = SysList.Value;
                                   Unit = SysList.Unit;
                               }
                           }
                       }

                      // string conditionBP2 = " TaskCode = 'Bloodpressure|Bloodpressure_2'";
                       List<TasksComList> BP2Rows = new List<TasksComList>();
                       foreach (TasksComList item in ComplianceList)
                       {
                           if (item.TaskCode == "Bloodpressure|Bloodpressure_2")
                           {
                               BP2Rows.Add(item);
                           }
                       }
                       if ((BP2Rows != null) && (BP2Rows.Count == 1))
                       {
                           if (BP2Rows[0].Status == "1")
                           {
                               VitalInfo DiaList = new VitalInfoMethod().GetSignByDay(pclsCache, PatientId, "Bloodpressure", "Bloodpressure_2", Date);
                               if (DiaList != null)
                               {
                                   mark = 1;
                                   int BPTime1 = Convert.ToInt32(DiaList.RecordTime);
                                   if (BPTime <= BPTime1)
                                   {
                                       BPTime = BPTime1;
                                   }
                                   DiaValue = DiaList.Value;
                               }
                           }
                       }

                       VitalTaskCom = new VitalTaskCom();
                       VitalTaskCom.SignName = "血压";
                       if (mark == 1)
                       {
                           VitalTaskCom.Status = "1";
                           VitalTaskCom.Time = new CommonFunction().TransTime(BPTime.ToString());
                           VitalTaskCom.Value = SysValue + "/" + DiaValue;
                           VitalTaskCom.Unit = Unit;
                       }
                       else
                       {
                           VitalTaskCom.Status = "0";
                       }
                       VitalTaskComList.Add(VitalTaskCom);



                       //脉率任务可能没没有，需要确认
                       //string conditionPR = " TaskCode = 'Pulserate|Pulserate_1'";
                       List<TasksComList> PulserateRows = new List<TasksComList>();
                       foreach (TasksComList item in ComplianceList)
                       {
                           if (item.TaskCode == "Pulserate|Pulserate_1")
                           {
                               BP2Rows.Add(item);
                           }
                       }
                       if ((PulserateRows != null) && (PulserateRows.Count == 1))
                       {
                           VitalTaskCom = new VitalTaskCom();
                           VitalTaskCom.SignName = "脉率";

                           if (PulserateRows[0].Status == "1")
                           {
                               VitalInfo PulList = new VitalInfoMethod().GetSignByDay(pclsCache, PatientId, "Pulserate", "Pulserate_1", Date);
                               if (PulList != null)
                               {

                                   VitalTaskCom.Status = "1";
                                   VitalTaskCom.Time = new CommonFunction().TransTime(PulList.RecordTime);
                                   VitalTaskCom.Value = PulList.Value;
                                   VitalTaskCom.Unit = PulList.Unit;
                               }
                               else
                               {
                                   VitalTaskCom.Status = "0";

                               }
                           }
                           else
                           {
                               VitalTaskCom.Status = "0";

                           }
                           VitalTaskComList.Add(VitalTaskCom);
                       }
                       #endregion
                   }

                   TaskComDetailByD.VitalTaskComList = VitalTaskComList;

                   TaskComByType TaskComByType = new TaskComByType();
                   List<TaskCom> TaskComList = new List<TaskCom>();
                   TaskCom TaskCom = new TaskCom();

                   //生活方式 
                   //string condition = " Type = 'LifeStyle'";
                   List<TasksComList> LifeStyleRows = new List<TasksComList>();
                   foreach (TasksComList item in ComplianceList)
                   {
                       if (item.Type == "LifeStyle")
                       {
                           LifeStyleRows.Add(item);
                       }
                   }

                   if ((LifeStyleRows != null) && (LifeStyleRows.Count > 0))
                   {
                       TaskComByType = new TaskComByType();
                       TaskComByType.TaskType = "生活方式";
                       TaskComList = new List<TaskCom>();
                       TaskCom = new TaskCom();

                       foreach (TasksComList item in LifeStyleRows)
                       {
                           TaskCom = new TaskCom();
                           TaskCom.TaskName = item.TaskName;
                           TaskCom.TaskStatus = item.Status;
                           TaskComList.Add(TaskCom);
                       }
                       TaskComByType.TaskComList = TaskComList;
                       TaskComDetailByD.TaskComByTypeList.Add(TaskComByType);
                   }

                   //用药情况
                   //condition = " Type = 'Drug'";
                   List<TasksComList> DrugRows = new List<TasksComList>();
                   foreach (TasksComList item in ComplianceList)
                   {
                       if (item.Type == "Drug")
                       {
                           DrugRows.Add(item);
                       }
                   }
                   if ((DrugRows != null) && (DrugRows.Count > 0))
                   {
                       TaskComByType = new TaskComByType();
                       TaskComByType.TaskType = "用药情况";
                       TaskComList = new List<TaskCom>();
                       TaskCom = new TaskCom();
                       foreach (TasksComList item in DrugRows)
                       {
                           TaskCom = new TaskCom();
                           TaskCom.TaskName = item.TaskName;
                           TaskCom.TaskStatus = item.Status;
                           TaskComList.Add(TaskCom);
                       }
                       TaskComByType.TaskComList = TaskComList;
                       TaskComDetailByD.TaskComByTypeList.Add(TaskComByType);
                   }
                   return TaskComDetailByD;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PsCompliance.GetImplementationByDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return null;
               }
           }
        
            #endregion

            //Ps.Task
            #region Ps.Task
           public List<PsTask> GetTaskList(DataConnection pclsCache, string PlanNo)
           {
               List<PsTask> items = new List<PsTask>();
               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }

                   cmd = new CacheCommand();
                   cmd = Ps.Task.GetPsTask(pclsCache.CacheConnectionObject);
                   cmd.Parameters.Add("piUserId", CacheDbType.NVarChar).Value = PlanNo;
                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       PsTask item = new PsTask();
                       item.Id = cdr["Id"].ToString();
                       item.Type = cdr["Type"].ToString();
                       item.Code = cdr["Code"].ToString();
                       item.CodeName = cdr["CodeName"].ToString();
                       item.Instruction = cdr["Instruction"].ToString();
                       items.Add(item);
                   }
                   return items;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTaskList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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