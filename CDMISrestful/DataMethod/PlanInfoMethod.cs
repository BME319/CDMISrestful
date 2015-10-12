using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;

namespace CDMISrestful.DataMethod
{
    public class PlanInfoMethod
    {
        public class PlanInfo
        {
            
            #region
            //SYF 20151010
            /// <summary>
            /// 
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PlanNo"></param>
            /// <param name="PatientId"></param>
            /// <param name="StartDate"></param>
            /// <param name="EndDate"></param>
            /// <param name="Module"></param>
            /// <param name="Status"></param>
            /// <param name="DoctorId"></param>
            /// <param name="piUserId"></param>
            /// <param name="piTerminalName"></param>
            /// <param name="piTerminalIP"></param>
            /// <param name="piDeviceType"></param>
            /// <returns></returns>
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
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.PsPlanSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return ret;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            //SYF 20151010
            /// <summary>
            /// 插入一条新纪录到Ps.Plan表
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PlanNo"></param>
            /// <param name="Status"></param>
            /// <param name="piUserId"></param>
            /// <param name="piTerminalName"></param>
            /// <param name="piTerminalIP"></param>
            /// <param name="piDeviceType"></param>
            /// <returns></returns>
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
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.ChangePlanStatus", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return ret;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

           
            /// <summary>
            /// 获取某计划的相关信息 SYF 2015-10-10
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PlanStartDate"></param>
            /// <returns></returns>
            public Period GetWeekPeriod(DataConnection pclsCache, int PlanStartDate)
            {
                try
                {
                    Period ret = new Period();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    InterSystems.Data.CacheTypes.CacheSysList list = null;
                    list = Ps.Plan.GetWeekPeriod(pclsCache.CacheConnectionObject, PlanStartDate);
                    if(list != null)
                    {
                        ret.StartDate = list[0];
                        ret.EndDate = list[1];
                        ret.DayCount = list[2];
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetWeekPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

            
            /// <summary>
            /// 获取某计划的进度和剩余天数 SYF 2015-10-10
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PlanNo"></param>
            /// <returns></returns>
            public Progressrate GetProgressRate(DataConnection pclsCache, string PlanNo)
            {
                try
                {
                    Progressrate ret = new Progressrate();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    InterSystems.Data.CacheTypes.CacheSysList list = null;
                    list = Ps.Plan.GetProgressRate(pclsCache.CacheConnectionObject, PlanNo);
                    if (list != null)
                    {
                        ret.RemainingDays = list[0];
                        ret.ProgressRate = list[1];
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetProgressRate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

           
            /// <summary>
            /// 获取某计划的相关信息 SYF 2015-10-10
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PlanNo"></param>
            /// <returns></returns>
            public GPlanInfo GetPlanInfo(DataConnection pclsCache, string PlanNo)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    InterSystems.Data.CacheTypes.CacheSysList list = null;
                    list = Ps.Plan.GetPlanInfo(pclsCache.CacheConnectionObject, PlanNo);
                    if(list != null)
                    {
                        ret.PlanNo = list[0];
                        ret.PatientId = list[1];
                        ret.StartDate = list[2];
                        ret.EndDate = list[3];
                        ret.Module = list[4];
                        ret.Status = list[5];
                        ret.DoctorId = list[6];
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetPlanInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

           
            /// <summary>
            /// SYF 20151010 获取健康专员负责的所有患者列表
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="DoctorId"></param>
            /// <param name="Module"></param>
            /// <returns></returns>
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
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetPatientsPlanByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
            /// SYF 2015-10-10 获取健康专员负责的所有患者最新结束(status = 4)计划列表
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="DoctorId"></param>
            /// <param name="Module"></param>
            /// <returns></returns>
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
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetOverDuePlanByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
            /// 获取某模块正在执行的计划 Syf 2015-10-10
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PatientId"></param>
            /// <param name="Module"></param>
            /// <returns></returns>
            public GPlanInfo GetExecutingPlanByM(DataConnection pclsCache, string PatientId, string Module)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    InterSystems.Data.CacheTypes.CacheSysList list = null;
                    list = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId);
                    if (list != null)
                    {
                        ret.PlanNo = list[0];
                        ret.PatientId = list[1];
                        ret.StartDate = list[2];
                        ret.EndDate = list[3];
                        ret.Module = list[4];
                        ret.Status = list[5];
                        ret.DoctorId = list[6];
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetExecutingPlanByM", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }

          
            /// <summary>
            /// 获取正在执行的计划 SYF 2015-10-10
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PatientId"></param>
            /// <returns></returns>
            public GPlanInfo GetExecutingPlan(DataConnection pclsCache, string PatientId)
            {
                try
                {
                    GPlanInfo ret = new GPlanInfo();
                    if (!pclsCache.Connect())
                    {
                        return null;
                    }
                    InterSystems.Data.CacheTypes.CacheSysList list = null;
                    list = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId);
                    if (list != null)
                    {
                        ret.PlanNo = list[0];
                        ret.PatientId = list[1];
                        ret.StartDate = list[2];
                        ret.EndDate = list[3];
                        ret.Module = list[4];
                        ret.Status = list[5];
                        ret.DoctorId = list[6];
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetExecutingPlan", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                    return null;
                }
                finally
                {
                    pclsCache.DisConnect();
                }
            }
            #endregion

            //Ps.Compliance
            #region
          
            /// <summary>
            /// 施宇帆 2015-10-10 插入子表PsComplianceDetail某条数据
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="Parent"></param>
            /// <param name="Id"></param>
            /// <param name="Status"></param>
            /// <param name="CoUserId"></param>
            /// <param name="CoTerminalName"></param>
            /// <param name="CoTerminalIP"></param>
            /// <param name="CoDeviceType"></param>
            /// <returns></returns>
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
                    HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.PsComplianceDetailSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                     return ret;
                }
                finally
                {
                     pclsCache.DisConnect();
                }
            }

           
            /// <summary>
           ///  syf 2015-10-10 插入PsCompliance某条数据
            /// </summary>
            /// <param name="pclsCache"></param>
            /// <param name="PatientId"></param>
            /// <param name="Date"></param>
            /// <param name="PlanNo"></param>
            /// <param name="Compliance"></param>
            /// <param name="revUserId"></param>
            /// <param name="TerminalName"></param>
            /// <param name="TerminalIP"></param>
            /// <param name="DeviceType"></param>
            /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.PsComplianceSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return ret;
               }
               finally
               {
                   pclsCache.DisConnect();
               }
           }

          
           /// <summary>
           /// 某一天所有任务的依从情况 DataTable数据库形式   用于点击事件显示详细   SYF 20151010
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="Date"></param>
           /// <returns></returns>
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
                   }
                   return list;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTasksComListByDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// 某段时间所有任务的依从情况  DataTable数据库形式  SYF 20151010
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="StartDate"></param>
           /// <param name="EndDate"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTasksComByPeriodDT", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// syf 2015-10-10根据计划编码和日期，获取依从率
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="Date"></param>
           /// <param name="PlanNo"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTasksByDate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// syf 2015-10-10 在当天根据任务状态的完成情况输出相应的任务
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="PiStatus"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetTaskByStatus", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// 通过Ps.Compliance中的date获取当天某项生理参数值，形成系列  DataTable 形式syf 2015-10-10
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="ItemType"></param>
           /// <param name="ItemCode"></param>
           /// <param name="StartDate"></param>
           /// <param name="EndDate"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetSignDetailByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// syf 2015-10-10 获取某计划的某段时间(包括端点)的依从率列表
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="StartDate"></param>
           /// <param name="EndDate"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetComplianceListByPeriod", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// syf 2015-10-10 获取患者某计划内某天的依从率
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="Date"></param>
           /// <param name="PlanNo"></param>
           /// <returns></returns>
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
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetComplianceByDay", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return ret;
               }
               finally
               {
                   pclsCache.DisConnect();
               }
           }

           /// <summary>
           /// 计算某段时间的总依从率 syf 2015-10-10
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PatientId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="StartDate"></param>
           /// <param name="EndDate"></param>
           /// <returns></returns>
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
                       compliacneRate = (Math.Round(sum / count, 2, MidpointRounding.AwayFromZero) * 100).ToString(); //保留整数

                   }

                   return compliacneRate;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "PlanInfoMethod.GetCompliacneRate", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

           #region <CmMstBloodPressure>
           /// <summary>
           /// 获取某血压值的级别 LY 2015-10-12
           /// </summary>
           /// <param name="BPType"></param>
           /// <param name="Value"></param>
           /// <param name="Reference"></param>
           /// <returns></returns>
           public string GetSignBPGrade(string BPType, int Value, List<MstBloodPressure> Reference)
           {
               //算法：将值Value与分级的值一一相比，注意  >Value；   <= Value <；   <=Value 等区间，确定其级别（取左边的值）  目前Name少了一个，暂时默认少了偏高
               string Name = "不明范围";  //待标记颜色
               try
               {
                   if (BPType == "Bloodpressure_1")  //收缩压 3  标准肯定不只1个值！
                   {
                       //按照血压规定  分级保证>2
                       if (Value < Reference[0].SBP)
                       {
                           Name = "错误";
                       }
                       if (Value > Reference[Reference.Count - 1].SBP)
                       {
                           Name = "错误";
                       }
                       if (Name == "不明范围")
                       {
                           if (Reference.Count >= 2)  //前两步已经保证了，数量肯定》2   
                           {
                               for (int i = 0; i <= Reference.Count - 2; i++)  //要求个数>=2
                               {
                                   if ((Value >= Reference[i].SBP) && (Value < Reference[i + 1].SBP)) //左闭右开
                                   {
                                       Name = Reference[i].Name;  //名字就低
                                       break;
                                   }
                               }
                           }
                       }
                   }
                   else  //舒张压
                   {
                       if (Value < Reference[0].DBP)
                       {
                           Name = "错误";
                       }
                       if (Value > Reference[Reference.Count - 1].DBP)
                       {
                           Name = "错误";
                       }
                       if (Name == "不明范围")
                       {
                           if (Reference.Count >= 2)  //前两步已经保证了，数量肯定》2
                           {
                               for (int i = 0; i <= Reference.Count - 2; i++)  //要求个数>=2
                               {
                                   if ((Value >= Reference[i].DBP) && (Value < Reference[i + 1].DBP))
                                   {
                                       Name = Reference[i].Name;
                                       break;
                                   }
                               }
                           }
                       }
                   }
                   return Name;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstBloodPressure.GetSignBPGrade", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return null;
               }
           }

           /// <summary>
           /// 根据收缩压获取血压等级说明 LY 2015-10-12
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="SBP"></param>
           /// <returns></returns>
           public string GetDescription(DataConnection pclsCache, int SBP)
           {
               string ret = "";
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return ret;
                   }
                   ret = (string)Cm.MstBloodPressure.GetDescription(pclsCache.CacheConnectionObject, SBP);
                   return ret;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstBloodPressure.GetDescription", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return ret;
               }
               finally
               {
                   pclsCache.DisConnect();
               }
           }

           /// <summary>
           /// 从数据库获取血压分级规则 LY 2015-10-12
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <returns></returns>
           public List<MstBloodPressure> GetBPGrades(DataConnection pclsCache)
           {
               List<MstBloodPressure> result = new List<MstBloodPressure>();
               CacheCommand cmd = null;
               CacheDataReader cdr = null;
               try
               {
                   if (!pclsCache.Connect())
                   {
                       return null;
                   }
                   cmd = new CacheCommand();
                   cmd = Cm.MstBloodPressure.GetBPGrades(pclsCache.CacheConnectionObject);
                   cdr = cmd.ExecuteReader();
                   while (cdr.Read())
                   {
                       MstBloodPressure MstBloodPressure = new MstBloodPressure();
                       MstBloodPressure.Code = cdr["Code"].ToString();
                       MstBloodPressure.Name = cdr["Name"].ToString();
                       MstBloodPressure.Description = cdr["Description"].ToString();
                       MstBloodPressure.SBP = Convert.ToInt32(cdr["SBP"]);
                       MstBloodPressure.DBP = Convert.ToInt32(cdr["DBP"]);
                       MstBloodPressure.PatientClass = cdr["PatientClass"].ToString();
                       MstBloodPressure.Redundance = cdr["Redundance"].ToString();
                       result.Add(MstBloodPressure);
                   }
                   return result;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstBloodPressure.GetBPGrades", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
           /// 获取血压点和数据图区块的颜色 LY 2015-10-12
           /// </summary>
           /// <param name="name"></param>
           /// <param name="type"></param>
           /// <returns></returns>
           public string GetBPColor(string name, string type)
           {
               string colorShow = "";  //默认颜色
               try
               {
                   if (type == "bullet")
                   {
                       colorShow = "#FFC78E";
                       switch (name)
                       {
                           case "很高": colorShow = "#930000";  //红色
                               break;
                           case "偏高": colorShow = "#CC0000";  //
                               break;
                           case "警戒": colorShow = "#0000cc"; //
                               break;
                           case "正常": colorShow = "#2894FF";  //
                               break;
                           case "偏低": colorShow = "#FFC78E";  //
                               break;
                           default: break;
                       }
                   }
                   else   //fill
                   {
                       colorShow = "#8080C0";
                       switch (name)
                       {
                           case "很高": colorShow = "#FF0000";  //深红色
                               break;
                           case "偏高": colorShow = "#FF60AF";  //微红
                               break;
                           case "警戒": colorShow = "#FFA042"; //橙色
                               break;
                           case "正常": colorShow = "#00DB00";  //绿色
                               break;
                           case "偏低": colorShow = "#8080C0";  //微紫
                               break;
                           default: break;
                       }
                   }
                   return colorShow;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstBloodPressure.GetBPColor", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return null;
               }
           }

           /// <summary>
           /// 生成收缩压/舒张压/脉率点图，并分级 LY 2015-10-12
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="UserId"></param>
           /// <param name="PlanNo"></param>
           /// <param name="Code"></param>
           /// <param name="StartDate"></param>
           /// <param name="EndDate"></param>
           /// <param name="reference"></param>
           /// <returns></returns>
           public List<Graph> GetSignInfoByM1(DataConnection pclsCache, string UserId, string PlanNo, string Code, int StartDate, int EndDate, List<MstBloodPressure> reference)
           {
               List<Graph> graphList = new List<Graph>();
               //获取系统时间  数据库连接，这样写，应该对的
               string serverTime = "";
               if (pclsCache.Connect())
               {
                   serverTime = Convert.ToDateTime(Cm.CommonLibrary.GetServerDateTime(pclsCache.CacheConnectionObject)).ToString("yyyyMMdd");
               }
               pclsCache.DisConnect();
               try
               {
                   //输入的code是拼接字段
                   //string[] strCode = Code.Split(new char[] { '|' });
                   //string ItemType = strCode[0];
                   //string ItemCode = strCode[1];
                   //M1的关注含下列几表   PsCompliance.GetSignDetailByPeriod方法保证几表条数相等 即使某天没体征数据，也会输出""字符串  
                   //收缩压表
                   List<SignDetailByPeriod> sysInfo = new List<SignDetailByPeriod>();
                   sysInfo = GetSignDetailByPeriod(pclsCache, UserId, PlanNo, "Bloodpressure", "Bloodpressure_1", StartDate, EndDate);
                   //RecordDate、RecordTime、Value、Unit
                   //舒张压表
                   List<SignDetailByPeriod> diaInfo = new List<SignDetailByPeriod>();
                   diaInfo = GetSignDetailByPeriod(pclsCache, UserId, PlanNo, "Bloodpressure", "Bloodpressure_2", StartDate, EndDate);
                   //脉率表
                   List<SignDetailByPeriod> pulInfo = new List<SignDetailByPeriod>();
                   pulInfo = GetSignDetailByPeriod(pclsCache, UserId, PlanNo, "Pulserate", "Pulserate_1", StartDate, EndDate);
                   //三张表都有数据
                   if ((sysInfo.Count == diaInfo.Count) && (sysInfo.Count == pulInfo.Count) && (sysInfo.Count > 0))
                   {
                       for (int rowsCount = 0; rowsCount < sysInfo.Count; rowsCount++)
                       {
                           Graph Graph = new Graph();
                           Graph.Date = sysInfo[rowsCount].RecordDate;
                           #region 值、等级、颜色
                           //值、等级、颜色、描述文本
                           if ((Code == "Bloodpressure|Bloodpressure_1") && (reference != null)) //血压要求 reference 不为空
                           {
                               #region 收缩压
                               Graph.SignValue = "#";
                               if (sysInfo[rowsCount].Value != "")
                               {
                                   Graph.SignValue = sysInfo[rowsCount].Value;
                               }
                               //Graph.SignValue = sysInfo.Rows[rowsCount]["Value"].ToString();
                               if (Graph.SignValue != "#")
                               {
                                   Graph.SignGrade = GetSignBPGrade("Bloodpressure_1", Convert.ToInt32(Graph.SignValue), reference);
                                   Graph.SignColor = GetBPColor(Graph.SignGrade, "bullet");
                                   //判断是否都有值
                                   if ((sysInfo[rowsCount].Value != "") && (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value != ""))
                                   {
                                       Graph.SignDescription = "血压：<b><span style='font-size:14px;'>" + sysInfo[rowsCount].Value + "</span></b>/" + diaInfo[rowsCount].Value + "mmHg<br>脉搏：" + pulInfo[rowsCount].Value + "次/分";
                                   }
                                   else if ((sysInfo[rowsCount].Value != "") || (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value == ""))
                                   {
                                       Graph.SignDescription = "血压：<b><span style='font-size:14px;'>" + sysInfo[rowsCount].Value + "</span></b>/" + diaInfo[rowsCount].Value + "mmHg";
                                   }
                                   else if ((sysInfo[rowsCount].Value == "") && (diaInfo[rowsCount].Value == "") && (pulInfo[rowsCount].Value != ""))
                                   {
                                       Graph.SignDescription = "脉搏：" + pulInfo[rowsCount].Value + "次/分";
                                   }
                                   //Graph.SignDescription = "血压：<b><span style='font-size:14px;'>" + sysInfo.Rows[rowsCount]["Value"].ToString() + "</span></b>/" + diaInfo.Rows[rowsCount]["Value"].ToString() + "mmHg<br>脉搏：" + pulInfo.Rows[rowsCount]["Value"].ToString() + "次/分";
                                   //"[[category]]<br>血压：<b><span style='font-size:14px;'>[[SBPvalue]] </span></b>/[[DBPvalue]]mmHg<br>脉搏：66次/分"
                               }
                               else
                               {
                                   Graph.SignGrade = "";
                                   Graph.SignColor = "";
                                   Graph.SignDescription = "";
                               }
                               #endregion
                           }
                           else if ((Code == "Bloodpressure|Bloodpressure_2") && (reference != null))  //舒张压
                           {
                               #region 舒张压
                               Graph.SignValue = "#";
                               if (diaInfo[rowsCount].Value != "")
                               {
                                   Graph.SignValue = diaInfo[rowsCount].Value;
                               }
                               //Graph.SignValue = diaInfo.Rows[rowsCount]["Value"].ToString();
                               if (Graph.SignValue != "#")
                               {
                                   Graph.SignGrade = GetSignBPGrade("Bloodpressure_2", Convert.ToInt32(Graph.SignValue), reference);
                                   Graph.SignColor = GetBPColor(Graph.SignGrade, "bullet");
                                   //Graph.SignDescription = "血压：" + sysInfo.Rows[rowsCount]["Value"].ToString() + "/<b><span style='font-size:14px;'>" + diaInfo.Rows[rowsCount]["Value"].ToString() + "</span></b>mmHg<br>脉搏：" + pulInfo.Rows[rowsCount]["Value"].ToString() + "次/分";
                                   //判断是否都有值
                                   if ((sysInfo[rowsCount].Value != "") && (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value != ""))
                                   {
                                       Graph.SignDescription = "血压：" + sysInfo[rowsCount].Value + "/<b><span style='font-size:14px;'>" + diaInfo[rowsCount].Value + "</span></b>mmHg<br>脉搏：" + pulInfo[rowsCount].Value + "次/分";
                                   }
                                   else if ((sysInfo[rowsCount].Value != "") || (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value == ""))
                                   {
                                       Graph.SignDescription = "血压：" + sysInfo[rowsCount].Value + "/<b><span style='font-size:14px;'>" + diaInfo[rowsCount].Value + "</span></b>mmHg";
                                   }
                                   else if ((sysInfo[rowsCount].Value == "") && (diaInfo[rowsCount].Value == "") && (pulInfo[rowsCount].Value != ""))
                                   {
                                       Graph.SignDescription = "脉搏：" + pulInfo[rowsCount].Value + "次/分";
                                   }
                               }
                               else
                               {
                                   Graph.SignGrade = "";
                                   Graph.SignColor = "";
                                   Graph.SignDescription = "";
                               }
                               #endregion
                           }
                           else if (Code == "Pulserate|Pulserate_1")  //脉率
                           {
                               #region 脉率
                               Graph.SignValue = "#";
                               if (pulInfo[rowsCount].Value != "")
                               {
                                   Graph.SignValue = pulInfo[rowsCount].Value;
                               }
                               //Graph.SignValue = pulInfo.Rows[rowsCount]["Value"].ToString();
                               //Graph.SignDescription = "血压：" + sysInfo.Rows[rowsCount]["Value"].ToString() + "/" + diaInfo.Rows[rowsCount]["Value"].ToString() + "mmHg<br>脉率：<b><span style='font-size:14px;'>" + pulInfo.Rows[rowsCount]["Value"].ToString() + "</span></b>次/分";
                               //判断是否都有值
                               if ((sysInfo[rowsCount].Value != "") && (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value != ""))
                               {
                                   Graph.SignDescription = "血压：" + sysInfo[rowsCount].Value + "/" + diaInfo[rowsCount].Value + "mmHg<br>脉率：<b><span style='font-size:14px;'>" + pulInfo[rowsCount].Value + "</span></b>次/分";
                               }
                               else if ((sysInfo[rowsCount].Value != "") || (diaInfo[rowsCount].Value != "") && (pulInfo[rowsCount].Value == ""))
                               {
                                   Graph.SignDescription = "血压：" + sysInfo[rowsCount].Value + "/" + diaInfo[rowsCount].Value + "mmHg";
                               }
                               else if ((sysInfo[rowsCount].Value == "") && (diaInfo[rowsCount].Value == "") && (pulInfo[rowsCount].Value != ""))
                               {
                                   Graph.SignDescription = "脉率：<b><span style='font-size:14px;'>" + pulInfo[rowsCount].Value + "</span></b>次/分";
                               }
                               if (Graph.SignValue != "#")
                               {
                                   //脉率的分级 写死
                                   if (Convert.ToDouble(Graph.SignValue) < 60)  //过慢
                                   {
                                       Graph.SignGrade = "过慢";
                                       Graph.SignColor = "#8080C0"; //微紫
                                   }
                                   else if (Convert.ToDouble(Graph.SignValue) > 100) //过快
                                   {
                                       Graph.SignGrade = "过快";
                                       Graph.SignColor = "#FF60AF";  //微红
                                   }
                                   else //成人 60~100之间包括端点 正常
                                   {
                                       Graph.SignGrade = "正常";
                                       Graph.SignColor = "#00DB00";  //绿色
                                   }
                               }
                               else
                               {
                                   Graph.SignGrade = "";
                                   Graph.SignColor = "";
                                   Graph.SignDescription = "";
                               }
                               #endregion
                           }
                           #endregion
                           //形状
                           if (rowsCount != sysInfo.Count - 1)
                           {
                               Graph.SignShape = "round";
                               Graph.SignShape = "round";
                           }
                           else
                           {
                               if (serverTime == Graph.Date)  //当天的血压点形状用菱形
                               {
                                   Graph.SignShape = "diamond";
                                   Graph.SignShape = "diamond";
                               }
                               else
                               {
                                   Graph.SignShape = "round";
                                   Graph.SignShape = "round";
                               }
                           }
                           graphList.Add(Graph);
                       }
                   }
                   //有血压任务，没有脉率
                   return graphList;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstBloodPressure.GetSignInfoByBP", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return null;
               }
               finally
               {
               }
           }

           /// <summary>
           /// 输出用于图形的分级区域 LY 2015-10-12
           /// </summary>
           /// <param name="pclsCache"></param>
           /// <param name="PlanNo"></param>
           /// <param name="Code"></param>
           /// <param name="Reference"></param>
           /// <returns></returns>
           public GraphGuide GetGuidesByCode(DataConnection pclsCache, string PlanNo, string Code, List<MstBloodPressure> Reference)
           {
               GraphGuide GraphGuide = new GraphGuide();   //输出
               List<Guide> GuideList = new List<Guide>();
               try
               {
                   if ((Code == "Bloodpressure|Bloodpressure_1") && (Reference != null))
                   {
                       #region 收缩压
                       GraphGuide.minimum = Convert.ToDouble(Reference[0].SBP);
                       GraphGuide.maximum = Convert.ToDouble(Reference[Reference.Count - 1].SBP);
                       InterSystems.Data.CacheTypes.CacheSysList SysTarget = null;
                       SysTarget = GetTargetByCode(pclsCache, PlanNo, "Bloodpressure", "Bloodpressure_1");
                       if (SysTarget != null)
                       {
                           //初始值
                           Guide originalGuide = new Guide();
                           originalGuide.value = SysTarget[4].ToString(); //值或起始值
                           originalGuide.toValue = "#CC0000";           //终值或""
                           originalGuide.label = "";      //中文定义 目标线、偏低、偏高等
                           //originalGuide.label = "起始" + "：" + originalGuide.value;
                           originalGuide.lineColor = "#FF5151";          //直线颜色  目标线  初始线
                           originalGuide.lineAlpha = "1";//直线透明度 0全透~1
                           originalGuide.dashLength = "8"; //虚线密度  4  8
                           originalGuide.color = "#CC0000";    //字体颜色
                           originalGuide.fontSize = "8"; //字体大小  默认14
                           originalGuide.position = "right"; //字体位置 right left
                           originalGuide.inside = "";//坐标系的内或外  false
                           originalGuide.fillAlpha = "";
                           originalGuide.fillColor = "";
                           GuideList.Add(originalGuide);
                           GraphGuide.original = originalGuide.value + "mmHg";
                           //目标值
                           Guide tagetGuide = new Guide();
                           tagetGuide.value = SysTarget[3].ToString();
                           tagetGuide.toValue = "#CC0000";
                           tagetGuide.label = "";
                           //tagetGuide.label = "目标" + "：" + tagetGuide.value;
                           tagetGuide.lineColor = "#CC0000";
                           tagetGuide.lineAlpha = "1";
                           tagetGuide.dashLength = "4";
                           tagetGuide.color = "#CC0000";
                           tagetGuide.fontSize = "14";
                           tagetGuide.position = "right";
                           tagetGuide.inside = "";
                           tagetGuide.fillAlpha = "";
                           tagetGuide.fillColor = "";
                           GuideList.Add(tagetGuide);
                           GraphGuide.target = tagetGuide.value + "mmHg";
                       }
                       //风险范围
                       for (int i = 0; i <= Reference.Count - 2; i++)
                       {
                           //收缩压
                           Guide SysGuide = new Guide();
                           SysGuide.value = Reference[i].SBP.ToString(); //起始值
                           SysGuide.toValue = Reference[i + 1].SBP.ToString();                //终值
                           SysGuide.label = Reference[i].Name;          //偏低、偏高等
                           SysGuide.lineColor = "";     //直线颜色  目标线  初始线
                           SysGuide.lineAlpha = "";   //直线透明度 0全透~1
                           SysGuide.dashLength = "";  //虚线密度  4  8
                           SysGuide.color = "#CC0000";     //字体颜色
                           SysGuide.fontSize = "14";   //字体大小  默认14
                           SysGuide.position = "right";    //字体位置 right left
                           SysGuide.inside = "true";      //坐标系的内或外  false
                           SysGuide.fillAlpha = "0.1";
                           SysGuide.fillColor = GetBPColor(SysGuide.label, "fill");   //GetFillColor
                           GuideList.Add(SysGuide);
                       }
                       //一般线
                       for (int i = 0; i <= Reference.Count - 1; i++)
                       {
                           //收缩压
                           Guide SysGuide = new Guide();
                           SysGuide.value = Reference[i].SBP.ToString();
                           SysGuide.toValue = "#CC0000";
                           SysGuide.label = Reference[i].SBP.ToString();
                           SysGuide.lineColor = "#CC0000";
                           SysGuide.lineAlpha = "0.15";
                           SysGuide.dashLength = "";
                           SysGuide.color = "#CC0000";
                           SysGuide.fontSize = "8";
                           SysGuide.position = "left";
                           SysGuide.inside = "";
                           SysGuide.fillAlpha = "";
                           SysGuide.fillColor = "";
                           GuideList.Add(SysGuide);
                       }
                       #endregion
                   }
                   else if ((Code == "Bloodpressure|Bloodpressure_2") && (Reference != null))
                   {
                       #region 舒张压
                       GraphGuide.minimum = Convert.ToDouble(Reference[0].DBP);
                       GraphGuide.maximum = Convert.ToDouble(Reference[Reference.Count - 1].DBP);
                       InterSystems.Data.CacheTypes.CacheSysList DiaTarget = null;
                       DiaTarget = GetTargetByCode(pclsCache, PlanNo, "Bloodpressure", "Bloodpressure_2");
                       if (DiaTarget != null)
                       {
                           //初始值
                           Guide originalGuide = new Guide();
                           originalGuide.value = DiaTarget[4].ToString(); //值或起始值
                           originalGuide.toValue = "#CC0000";           //终值或""
                           originalGuide.label = "";      //中文定义 目标线、偏低、偏高等
                           //originalGuide.label = "起始" + "：" + originalGuide.value;
                           originalGuide.lineColor = "#FF5151";          //直线颜色  目标线  初始线
                           originalGuide.lineAlpha = "1";//直线透明度 0全透~1
                           originalGuide.dashLength = "8"; //虚线密度  4  8
                           originalGuide.color = "#CC0000";    //字体颜色
                           originalGuide.fontSize = "8"; //字体大小  默认14
                           originalGuide.position = "right"; //字体位置 right left
                           originalGuide.inside = "";//坐标系的内或外  false
                           originalGuide.fillAlpha = "";
                           originalGuide.fillColor = "";
                           GuideList.Add(originalGuide);
                           GraphGuide.original = originalGuide.value + "mmHg";
                           //目标值
                           Guide tagetGuide = new Guide();
                           tagetGuide.value = DiaTarget[3].ToString();
                           tagetGuide.toValue = "#CC0000";
                           tagetGuide.label = "";
                           //tagetGuide.label = "目标" + "：" + tagetGuide.value;
                           tagetGuide.lineColor = "#CC0000";
                           tagetGuide.lineAlpha = "1";
                           tagetGuide.dashLength = "4";
                           tagetGuide.color = "#CC0000";
                           tagetGuide.fontSize = "14";
                           tagetGuide.position = "right";
                           tagetGuide.inside = "";
                           tagetGuide.fillColor = "";
                           tagetGuide.fillAlpha = "";
                           GuideList.Add(tagetGuide);
                           GraphGuide.target = tagetGuide.value + "mmHg";
                       }
                       //风险范围
                       for (int i = 0; i <= Reference.Count - 2; i++)
                       {
                           //舒张压
                           Guide DiaGuide = new Guide();
                           DiaGuide.value = Reference[i].DBP.ToString();
                           DiaGuide.toValue = Reference[i + 1].DBP.ToString();
                           DiaGuide.label = Reference[i].Name;
                           DiaGuide.lineColor = "";
                           DiaGuide.lineAlpha = "";
                           DiaGuide.dashLength = "";
                           DiaGuide.color = "#CC0000";
                           DiaGuide.fontSize = "14";
                           DiaGuide.position = "right";
                           DiaGuide.inside = "true";
                           DiaGuide.fillAlpha = "0.1";
                           DiaGuide.fillColor = GetBPColor(DiaGuide.label, "fill");
                           GuideList.Add(DiaGuide);
                       }
                       //一般线
                       for (int i = 0; i <= Reference.Count - 1; i++)
                       {
                           //舒张压
                           Guide DiaGuide = new Guide();
                           DiaGuide.value = Reference[i].DBP.ToString();
                           DiaGuide.toValue = "#CC0000";
                           DiaGuide.label = Reference[i].DBP.ToString();
                           DiaGuide.lineColor = "#CC0000";
                           DiaGuide.lineAlpha = "0.15";
                           DiaGuide.dashLength = "";
                           DiaGuide.color = "#CC0000";
                           DiaGuide.fontSize = "8";
                           DiaGuide.position = "left";
                           DiaGuide.inside = "";
                           DiaGuide.fillAlpha = "";
                           DiaGuide.fillColor = "";
                           GuideList.Add(DiaGuide);
                       }
                       #endregion
                   }
                   else if (Code == "Pulserate|Pulserate_1") //脉率没有 初始值和目标值
                   {
                       #region 脉率
                       GraphGuide.minimum = 30;
                       GraphGuide.maximum = 150;
                       ////初始值
                       //Guide originalGuide = new Guide();
                       //originalGuide.value =""; //值或起始值
                       //originalGuide.toValue = "#CC0000";           //终值或""
                       //originalGuide.label = "";      //中文定义 目标线、偏低、偏高等
                       ////originalGuide.label = "起始" + "：" + originalGuide.value;
                       //originalGuide.lineColor = "#FF5151";          //直线颜色  目标线  初始线
                       //originalGuide.lineAlpha = "1";//直线透明度 0全透~1
                       //originalGuide.dashLength = "8"; //虚线密度  4  8
                       //originalGuide.color = "#CC0000";    //字体颜色
                       //originalGuide.fontSize = "8"; //字体大小  默认14
                       //originalGuide.position = "right"; //字体位置 right left
                       //originalGuide.inside = "";//坐标系的内或外  false
                       //originalGuide.fillAlpha = "";
                       //originalGuide.fillColor = "";
                       //GuideList.Add(originalGuide);
                       //GraphGuide.original = originalGuide.value;
                       ////目标值
                       //Guide tagetGuide = new Guide();
                       //tagetGuide.value = "";
                       //tagetGuide.toValue = "#CC0000";
                       //tagetGuide.label = "";
                       ////tagetGuide.label = "目标" + "：" + tagetGuide.value;
                       //tagetGuide.lineColor = "#CC0000";
                       //tagetGuide.lineAlpha = "1";
                       //tagetGuide.dashLength = "4";
                       //tagetGuide.color = "#CC0000";
                       //tagetGuide.fontSize = "14";
                       //tagetGuide.position = "right";
                       //tagetGuide.inside = "";
                       //tagetGuide.fillColor = "";
                       //tagetGuide.fillAlpha = "";
                       //风险范围
                       //正常
                       Guide PulseGuide = new Guide();
                       PulseGuide.value = "60";
                       PulseGuide.toValue = "100";
                       PulseGuide.label = "正常";
                       PulseGuide.lineColor = "";
                       PulseGuide.lineAlpha = "";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#00DB00";  //字的颜色
                       PulseGuide.fontSize = "14";
                       PulseGuide.position = "right";
                       PulseGuide.inside = "true";
                       PulseGuide.fillAlpha = "0.1";
                       PulseGuide.fillColor = "#2894FF";  //区域颜色
                       GuideList.Add(PulseGuide);
                       //偏高 #CC0000
                       PulseGuide = new Guide();
                       PulseGuide.value = "100";
                       PulseGuide.toValue = "150";
                       PulseGuide.label = "偏高";
                       PulseGuide.lineColor = "";
                       PulseGuide.lineAlpha = "";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#FF60AF";
                       PulseGuide.fontSize = "14";
                       PulseGuide.position = "right";
                       PulseGuide.inside = "true";
                       PulseGuide.fillAlpha = "0.1";
                       PulseGuide.fillColor = "#CC0000";
                       GuideList.Add(PulseGuide);
                       //偏低
                       PulseGuide = new Guide();
                       PulseGuide.value = "30";
                       PulseGuide.toValue = "60";
                       PulseGuide.label = "偏低";
                       PulseGuide.lineColor = "";
                       PulseGuide.lineAlpha = "";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#8080C0";
                       PulseGuide.fontSize = "14";
                       PulseGuide.position = "right";
                       PulseGuide.inside = "true";
                       PulseGuide.fillAlpha = "0.1";
                       PulseGuide.fillColor = "#FFC78E";
                       GuideList.Add(PulseGuide);
                       //一般线
                       //30
                       PulseGuide = new Guide();
                       PulseGuide.value = "30";
                       PulseGuide.toValue = "#CC0000";
                       PulseGuide.label = "30";
                       PulseGuide.lineColor = "#CC0000";
                       PulseGuide.lineAlpha = "0.15";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#CC0000";
                       PulseGuide.fontSize = "8";
                       PulseGuide.position = "left";
                       PulseGuide.inside = "";
                       PulseGuide.fillAlpha = "";
                       PulseGuide.fillColor = "";
                       GuideList.Add(PulseGuide);
                       //60
                       PulseGuide = new Guide();
                       PulseGuide.value = "60";
                       PulseGuide.toValue = "#CC0000";
                       PulseGuide.label = "60";
                       PulseGuide.lineColor = "#CC0000";
                       PulseGuide.lineAlpha = "0.15";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#CC0000";
                       PulseGuide.fontSize = "8";
                       PulseGuide.position = "left";
                       PulseGuide.inside = "";
                       PulseGuide.fillAlpha = "";
                       PulseGuide.fillColor = "";
                       GuideList.Add(PulseGuide);
                       //100
                       PulseGuide = new Guide();
                       PulseGuide.value = "100";
                       PulseGuide.toValue = "#CC0000";
                       PulseGuide.label = "100";
                       PulseGuide.lineColor = "#CC0000";
                       PulseGuide.lineAlpha = "0.15";
                       PulseGuide.dashLength = "";
                       PulseGuide.color = "#CC0000";
                       PulseGuide.fontSize = "8";
                       PulseGuide.position = "left";
                       PulseGuide.inside = "";
                       PulseGuide.fillAlpha = "";
                       PulseGuide.fillColor = "";
                       GuideList.Add(PulseGuide);
                       #endregion
                   }
                   GraphGuide.GuideList = GuideList;
                   return GraphGuide;
               }
               catch (Exception ex)
               {
                   HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstBloodPressure.GetGuidesByCode", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                   return null;
               }
               finally
               {
               }
           }
           #endregion
        }
    }
}