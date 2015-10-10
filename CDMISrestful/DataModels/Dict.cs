using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class Dict
    {
    }

    public class LifeStyleDetail
    {
        public string StyleId{get;set;}
        public string Module{get;set;}
        public string CurativeEffect{get;set;}
        public string SideEffect{get;set;}
        public string Instruction{get;set;}
        public string HealthEffect{get;set;}
        public string Unit{get;set;}
        public string Redundance{get;set;}        
    }

    public class Insurance
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string InputCode { get; set; }
        public string Redundance { get; set; }
        //public string InvalidFlag { get; set; }
    }

    public class MstInfoItemByCategoryCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public int SortNo { get; set; }
        public int GroupHeaderFlag { get; set; }
        public string ControlType { get; set; }
        public string OptionCategory { get; set; }

    }

    public class CmAbsType
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string InputCode { get; set; }
        public int SortNo { get; set; }
        public string Redundance { get; set; }
        public int InvalidFlag { get; set; }
    }
}