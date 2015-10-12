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
        }
    }
}