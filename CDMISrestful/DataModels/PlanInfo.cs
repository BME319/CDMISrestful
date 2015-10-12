using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.DataModels
{

    public class Period
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DayCount { get; set; }
    }

    public class Progressrate
    {
        public string RemainingDays { get; set; }
        public string ProgressRate { get; set; }
    }

    public class GPlanInfo
    {
        public string PlanNo { get; set; }
        public string PatientId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Module { get; set; }
        public string Status { get; set; }
        public string DoctorId { get; set; }
    }

    public class PatientPlan
    {
        public string PatientId { get; set; }
        public string PlanNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TotalDays { get; set; }
        public string RemainingDays { get; set; }
        public string Status { get; set; }
    }

    public class TasksComList
    {
        public string Date { get; set; }
        public string ComplianceValue { get; set; }
        public string TaskType { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string TaskCode { get; set; }
        public string Type { get; set; }
    }

    public class TasksComByPeriodDT
    {
        public string Date { get; set; }
        public string ComplianceValue { get; set; }
        public string TaskType { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    public class TasksByDate
    {
        public string TaskID { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
    }

    public class TasksByStatus
    {
         public string Id { get; set; }
        public string Status { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Instruction { get; set; }
    }

    public class SignDetailByPeriod
    {
         public string RecordDate { get; set; }
        public string RecordTime { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class ComplianceListByPeriod
    {
         public string Date { get; set; }
        public string Compliance { get; set; }
    }
    public class PlanDeatil
    {
        /// <summary>
        /// 计划名称  “计划”+序号+起止时间  用于计划列表的显示
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 计划编码
        /// </summary>
        public string PlanNo { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public int StartDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public int EndDate { get; set; }
    }

    public class PsTaskByType
    { 
        public string Id {get; set;}
        public string Code {get; set;}
        public string CodeName {get; set;}
        public string Instruction {get; set;}
     
    }
    public class PsTask
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string Instruction { get; set; }

    }
    public class TargetByCode
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Origin { get; set; }
        public string Instruction { get; set; }
        public string Unit { get; set; }

    }

    public class MstBloodPressure
    {
        public string Code { get; set; }                //编码  
        public string Name { get; set; }               //名称：很高、偏高、警戒、正常
        public string Description { get; set; }       //描述
        public int SBP { get; set; }              //收缩压
        public int DBP { get; set; }             //舒张压
        public string PatientClass { get; set; }       //患者类型
        public string Redundance { get; set; }        //冗余
    }

    public class Graph         //图的主要点数据
    {
        //日期
        public string Date { get; set; }       //日期，到天
        //图-测量任务，体征数据部分
        public string SignValue { get; set; }          //Y值
        public string SignGrade { get; set; }          //Y值级别  暂时只用来确定颜色，后期可作文字显示 “偏高、很高等”
        public string SignColor { get; set; }         //点颜色  
        public string SignShape { get; set; }         //点形状  
        public string SignDescription { get; set; }    //点的气球文本   样式——日期  <br> 收缩压/舒张压 mmHg  <br> 脉率 次/分
        //图-其他任务依从情况（包括用药、生活方式等）
        public string DrugValue { get; set; }         //画在下部图，保持Y=1
        public string DrugBullet { get; set; }       //客制化颜色 用图片-部分完成 "amcharts-images/drug.png" 半白半黑图片
        public string DrugColor { get; set; }        //药的其他颜色-完全未完成、完成	
        public string DrugDescription { get; set; }       //任务依从情况描述 "部分完成；未吃:阿司匹林、青霉素；已吃：钙片、板蓝根"  使用叉勾图标
        //暂时用不到的
        //public string BPBullet { get; set; }       //客制化血压点 "amcharts-images/star.png"
        //public string timeDetail { get; set; }    //最新测试的具体时间，到min
        public Graph()
        {
            //初始化  初始化为无记录状态，还是未完成任务状态？
            //暂时未完成任务状态  因为从PsCompliance取出那天，最初默认的就是未完成任务！
            //SignValue = "";  //string默认初始化为""，所以不需要再赋值
            //SignGrade = "";
            // SignColor = "";
            // SignShape = "";
            //SignDescription = "";
            DrugValue = "";            //可能没有用药或其他任务
            DrugBullet = "";         //初始化  时间肯定有  默认所有任务（生理测量、用药）为未完成任务状态
            DrugColor = "";  //白色 "#FFFFFF"
            DrugDescription = "";  //可能无任务，也可能任务未完成 不确定状态  "无记录";
        }
    }

    public class GraphGuide      //血压分级区域和最大最小值     
    {
        public List<Guide> GuideList { get; set; }   //血压分级区域
        public string original { get; set; }      //初始值
        public string target { get; set; }        //目标值
        public double minimum { get; set; }       //Y值下限
        public double maximum { get; set; }       //Y值上限
        public GraphGuide()
        {
            GuideList = new List<Guide>();
        }
    }

    public class Guide          //图的区域划分-风险分级、目标线、初始线    目标线、初始线 字体、线密度不同  分级区域颜色不同，文字不同
    {
        //变量-来自数据库
        public string value { get; set; }       //值或起始值
        public string toValue { get; set; }       //终值或""
        public string label { get; set; }        //中文定义 目标线、偏低、偏高等
        //恒量-根据图设定
        public string lineColor { get; set; }       //直线颜色  目标线  初始线
        public string lineAlpha { get; set; }       //直线透明度 0全透~1
        public string dashLength { get; set; }       //虚线密度  4  8
        public string color { get; set; }            //字体颜色
        public string fontSize { get; set; }       //字体大小  默认14
        public string position { get; set; }       //字体位置 right left
        public string inside { get; set; }        //坐标系的内或外  false
        public string fillAlpha { get; set; }       //区域透明度
        public string fillColor { get; set; }       //
        //public string balloonText { get; set; }       //气球弹出框   
    }
}