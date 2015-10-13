using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public class PlanInfoRepository
    {
        DataConnection pclsCache = new DataConnection();
        public void GetImplementationForPhone(string PatientId, string Module)
        {
            ImplementationPhone ImplementationPhone = new ImplementationPhone();
            string str_result = "";
            try
            {
                //病人基本信息-头像、姓名.. (由于手机版只针对换换咋用户，基本信息可不用获取
                // CacheSysList patientList = PsBasicInfo.GetPatientBasicInfo(_cnCache, PatientId);
                //if (patientList != null)
                //{
                //ImplementationPhone.PatientInfo.PatientName = patientList[0];
                //}

                int planStartDate = 0;
                int planEndDate = 0;
                string PlanNo = "";

                GPlanInfo planInfo = new PlanInfoMethod().GetExecutingPlanByM(pclsCache, PatientId, Module);
                if (planInfo != null)
                {
                    PlanNo = planInfo.PlanNo;
                    planStartDate = Convert.ToInt32(planInfo.StartDate);
                    planEndDate = Convert.ToInt32(planInfo.EndDate);  //未用到
                }

                if ((PlanNo != "") && (PlanNo != null)) //存在正在执行的计划
                {
                    ImplementationPhone.NowPlanNo = PlanNo;

                    //剩余天数和进度
                    Progressrate PRlist = new PlanInfoMethod().GetProgressRate(pclsCache, PlanNo);
                    if (PRlist != null)
                    {
                        ImplementationPhone.RemainingDays = PRlist.RemainingDays;  //"距离本次计划结束还剩"+PRlist[0]+"天";
                        ImplementationPhone.ProgressRate = (Convert.ToDouble(PRlist.ProgressRate) * 100).ToString();  //"进度："++"%";
                    }

                    //最近一周的依从率
                    Period weekPeriod = new PlanInfoMethod().GetWeekPeriod(pclsCache, planStartDate);
                    if (weekPeriod != null)
                    {
                        ImplementationPhone.CompliacneValue = new PlanInfoMethod().GetCompliacneRate(pclsCache, PatientId, PlanNo, Convert.ToInt32(weekPeriod[0]), Convert.ToInt32(weekPeriod[1]));
                        ImplementationPhone.StartDate = Convert.ToInt32(weekPeriod.StartDate);  //用于获取血压的详细数据
                        ImplementationPhone.EndDate = Convert.ToInt32(weekPeriod.EndDate);
                    }

                    #region  读取任务执行情况，血压、用药-最近一周的数据

                    //读取任务  phone版 此函数其他任务也显示
                    List<PsTask> TaskList = new PlanInfoMethod().GetTaskList(pclsCache, PlanNo);

                    //测量-体征切换下拉框  
                    List<PsTask> VitalSignRows = null;
                    foreach (PsTask item in TaskList)
                    {
                        if (item.Type == "VitalSign")
                        {
                            VitalSignRows.Add(item);
                        }
                    }
                    List<SignShow> SignList = new List<SignShow>();
                    foreach (PsTask item in VitalSignRows)
                    {
                        SignShow SignShow = new SignShow();
                        SignShow.SignName = item.CodeName;
                        SignShow.SignCode = item.Code;
                        SignList.Add(SignShow);
                    }
                    ImplementationPhone.SignList = SignList;


                    List<MstBloodPressure> reference = new List<MstBloodPressure>();
                    ChartData ChartData = new ChartData();
                    List<Graph> GraphList = new List<Graph>();
                    GraphGuide GraphGuide = new GraphGuide();

                    if (Module == "M1")  //后期维护的话，在这里添加不同的模块判断
                    {
                        condition = " Code = 'Bloodpressure|Bloodpressure_1' or  Code = 'Bloodpressure|Bloodpressure_2'or  Code = 'Pulserate|Pulserate_1'";
                        DataRow[] HyperTensionRows = TaskList.Select(condition);

                        //注意：需要兼容之前没有脉率的情况
                        if ((HyperTensionRows != null) && (HyperTensionRows.Length >= 2))  //M1 收缩压（默认显示）、舒张压、脉率  前两者肯定有，脉率不一定有
                        {
                            //获取血压的分级规则，脉率的分级原则写死在webservice
                            reference = CmMstBloodPressure.GetBPGrades(_cnCache);

                            //首次进入，默认加载收缩压
                            GraphList = CmMstBloodPressure.GetSignInfoByM1(_cnCache, PatientId, PlanNo, "Bloodpressure|Bloodpressure_1", Convert.ToInt32(weekPeriod[0]), Convert.ToInt32(weekPeriod[1]), reference);

                            //初始值、目标值、分级规则加工
                            if (GraphList.Count > 0)
                            {
                                GraphGuide = CmMstBloodPressure.GetGuidesByCode(_cnCache, PlanNo, "Bloodpressure|Bloodpressure_1", reference);
                                ChartData.GraphGuide = GraphGuide;
                            }
                        }

                    }
                    else
                    {

                    }

                    //必有测量任务，其他任务（例如吃药）可能没有
                    //其他任务依从情况 //是否有其他任务
                    List<CompliacneDetailByD> TasksComByPeriod = new List<CompliacneDetailByD>();
                    //string condition1 = " Type not in ('VitalSign,')";
                    if (TaskList.Rows.Count == VitalSignRows.Length)
                    {
                        ChartData.OtherTasks = "0";
                    }
                    else
                    {
                        ChartData.OtherTasks = "1";
                        TasksComByPeriod = PsCompliance.GetTasksComCountByPeriod(_cnCache, PatientId, PlanNo, Convert.ToInt32(weekPeriod[0]), Convert.ToInt32(weekPeriod[1]));
                        if ((TasksComByPeriod != null) && (TasksComByPeriod.Count == GraphList.Count))
                        {
                            for (int rowsCount = 0; rowsCount < TasksComByPeriod.Count; rowsCount++)
                            {
                                GraphList[rowsCount].DrugValue = "1";   //已经初始化过
                                GraphList[rowsCount].DrugBullet = TasksComByPeriod[rowsCount].drugBullet;
                                GraphList[rowsCount].DrugColor = TasksComByPeriod[rowsCount].drugColor;
                                GraphList[rowsCount].DrugDescription = TasksComByPeriod[rowsCount].Events;//+ "<br><a onclick= shuang shuang zz(); shuang shuang;>详细</a>"
                            }
                        }
                    }


                    #endregion

                    ChartData.GraphList = GraphList;
                    ImplementationPhone.ChartData = ChartData;
                }

                str_result = JSONHelper.ObjectToJson(ImplementationPhone);
                Context.Response.BinaryWrite(new byte[] { 0xEF, 0xBB, 0xBF });
                Context.Response.Write(str_result);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Context.Response.End();
                //return ImplementationInfo;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "GetImplementationForPhone", "WebService调用异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                //return null;
                throw (ex);
            }
        }
    }
}