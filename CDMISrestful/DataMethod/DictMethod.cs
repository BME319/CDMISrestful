using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.DataMethod
{
    public class DictMethod
    {
        #region CmMstType
        // GetTypeList 获取某个分类的类别 CSQ 20151010
        public List<TypeAndName> CmMstTypeGetTypeList(DataConnection pclsCache, string Category)
        {
            List<TypeAndName> list = new List<TypeAndName>();
           
            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstType.GetTypeList(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("Category", CacheDbType.NVarChar).Value = Category;
          
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new TypeAndName { Type = cdr["Type"].ToString(), Name = cdr["Name"].ToString() });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstType.GetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
       
        #region CmMstLifeStyleDetail
        public List<LifeStyleDetail> GetLifeStyleDetail(DataConnection pclsCache, string Module)
        {
            List<LifeStyleDetail> list = new List<LifeStyleDetail>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Cm.MstLifeStyleDetail.GetLifeStyleDetail(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("Module", CacheDbType.NVarChar).Value = Module;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new LifeStyleDetail
                    {
                        StyleId = cdr["StyleId"].ToString(),
                        Module = cdr["Module"].ToString(),
                        CurativeEffect = cdr["CurativeEffect"].ToString(),
                        SideEffect = cdr["SideEffect"].ToString(),
                        Instruction = cdr["Instruction"].ToString(),
                        HealthEffect = cdr["HealthEffect"].ToString(),
                        Unit = cdr["Unit"].ToString(),
                        Redundance = cdr["Redundance"].ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "Cm.MstLifeStyleDetail.GetLifeStyleDetail", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region Cm.MstInsurance
        //GetInsurance CSQ 20151010
        public List<Insurance> GetInsurance(DataConnection pclsCache)
        {
            List<Insurance> list = new List<Insurance>();
           
            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstInsurance.GetInsuranceType(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new Insurance
                    {
                        Code = cdr["Code"].ToString(),
                        Name = cdr["Name"].ToString(),
                        InputCode = cdr["InputCode"].ToString(),
                        Redundance = cdr["Redundance"].ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstInsurance.GetInsurance", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region Cm.MstInfoItem
        public List<CmMstInfoItem> GetCmMstInfoItem(DataConnection pclsCache)
        {
            List<CmMstInfoItem> CmMstInfoItemList = new List<CmMstInfoItem>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                //cmd = new CacheCommand();
                cmd = Cm.MstInfoItem.GetInfoItem(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    CmMstInfoItemList.Add(new CmMstInfoItem
                    {
                        CategoryCode = cdr["CategoryCode"].ToString(),
                        Code = cdr["Code"].ToString(),
                        Name = cdr["Name"].ToString(),
                        ParentCode = cdr["ParentCode"].ToString(),
                        SortNo = Convert.ToInt32(cdr["SortNo"].ToString()),
                        StartDate = Convert.ToInt32(cdr["StartDate"].ToString()),
                        EndDate = Convert.ToInt32(cdr["EndDate"].ToString()),
                        GroupHeaderFlag = Convert.ToInt32(cdr["GroupHeaderFlag"].ToString()),
                        ControlType = cdr["ControlType"].ToString(),
                        OptionCategory = cdr["OptionCategory"].ToString(),
                        RevUserId = "",
                        TerminalName = "",
                        TerminalIP = "",
                        DeviceType = 0
                    });
                }
                return CmMstInfoItemList;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstInfoItem.GetInfoItem", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        //GetMstInfoItemByCategoryCode CSQ 2014-12-04 
        public List<MstInfoItemByCategoryCode> GetMstInfoItemByCategoryCode(DataConnection pclsCache, string CategoryCode)
        {
            List<MstInfoItemByCategoryCode> list = new List<MstInfoItemByCategoryCode>();          

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstInfoItem.GetMstInfoItemByCategoryCode(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("CategoryCode", CacheDbType.NVarChar).Value = CategoryCode;

                cdr = cmd.ExecuteReader();
                int SortNo;
                int GroupHeaderFlag;
                //int ControlType;
                while (cdr.Read())
                {
                    if (cdr["SortNo"].ToString() == "")
                    {
                        SortNo = 0;
                    }
                    else
                    {
                        SortNo = Convert.ToInt32(cdr["SortNo"]);
                    }
                    if (cdr["GroupHeaderFlag"].ToString() == "")
                    {
                        GroupHeaderFlag = 0;
                    }
                    else
                    {
                        GroupHeaderFlag = Convert.ToInt32(cdr["GroupHeaderFlag"]);
                    }

                    list.Add(new MstInfoItemByCategoryCode
                    {                      
                        Code = cdr["Code"].ToString(),
                        Name = cdr["Name"].ToString(),
                        ParentCode = cdr["ParentCode"].ToString(),
                        SortNo = SortNo,
                        GroupHeaderFlag = GroupHeaderFlag,
                        ControlType = cdr["ControlType"].ToString(),
                        OptionCategory = cdr["OptionCategory"].ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstInfoItem.GetMstInfoItemByCategoryCode", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region Cm.MstHypertensionDrug
        // GetTypeList 返回所有类型代码及名称 CSQ 20151010
        public List<TypeAndName> CmMstHypertensionDrugGetTypeList(DataConnection pclsCache)
        {
            List<TypeAndName> list = new List<TypeAndName>();
           
            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstHypertensionDrug.GetTypeList(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new TypeAndName
                    {
                        Type = cdr["Type"].ToString(),
                        Name = cdr["TypeName"].ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstHypertensionDrug.GetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        // GetHypertensionDrug 返回所有数据信息 CSQ 20151010
        public List<CmAbsType> GetHypertensionDrug(DataConnection pclsCache)
        {
            List<CmAbsType> list = new List<CmAbsType>();

            int int_InvalidFlag = 0;

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstHypertensionDrug.GetHypertensionDrug(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    if (cdr["InvalidFlag"].ToString() == "")
                    {
                        int_InvalidFlag = 0;
                    }
                    else
                    {
                        int_InvalidFlag = Convert.ToInt32(cdr["InvalidFlag"]);
                    }

                    list.Add(new CmAbsType
                    {
                        Type = cdr["Type"].ToString(),
                        Code = cdr["Code"].ToString(),
                        TypeName = cdr["TypeName"].ToString(),
                        Name = cdr["Name"].ToString(),
                        InputCode = cdr["InputCode"].ToString(),
                        SortNo = Convert.ToInt32(cdr["SortNo"]),
                        Redundance = cdr["Redundance"].ToString(),
                        InvalidFlag = int_InvalidFlag
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstHypertensionDrug.GetHypertensionDrug", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region CmMstDiabetesDrug GetTypeList
        // CmMstDiabetesDrugGetTypeList 返回所有类型代码及名称 CSQ 20151010
        public List<TypeAndName> CmMstDiabetesDrugGetTypeList(DataConnection pclsCache)
        {
            List<TypeAndName> list = new List<TypeAndName>();          

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstDiabetesDrug.GetTypeList(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new TypeAndName { Type = cdr["Type"].ToString(), Name = cdr["TypeName"].ToString() });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstDiabetesDrug.CmMstDiabetesDrugGetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        // GetDiabetesDrug 返回所有数据信息 CSQ 20151010
        public List<CmAbsType> GetDiabetesDrug(DataConnection pclsCache)
        {
            List<CmAbsType> list = new List<CmAbsType>();

            int int_InvalidFlag = 0;

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }

                cmd = new CacheCommand();
                cmd = Cm.MstDiabetesDrug.GetDiabetesDrug(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    if (cdr["InvalidFlag"].ToString() == "")
                    {
                        int_InvalidFlag = 0;
                    }
                    else
                    {
                        int_InvalidFlag = Convert.ToInt32(cdr["InvalidFlag"]);
                    }

                    list.Add(new CmAbsType
                    {
                        Type = cdr["Type"].ToString(),
                        Code = cdr["Code"].ToString(),
                        TypeName = cdr["TypeName"].ToString(),
                        Name = cdr["Name"].ToString(),
                        InputCode = cdr["InputCode"].ToString(),
                        SortNo = Convert.ToInt32(cdr["SortNo"]),
                        Redundance = cdr["Redundance"].ToString(),
                        InvalidFlag = int_InvalidFlag
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstHypertensionDrug.GetDiabetesDrug", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region CmMstBloodPressure

        #endregion
    }
}