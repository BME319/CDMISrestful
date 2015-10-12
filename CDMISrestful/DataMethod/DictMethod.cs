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
        /// <summary>
        /// 获取某个分类的类别 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Category"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        /// 获取生活方式细节 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetLifeStyleDetail", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        /// GetInsurance CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetInsurance", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        /// CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetInfoItem", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// GetMstInfoItemByCategoryCode CSQ 2015-10-10 
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetMstInfoItemByCategoryCode", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        /// GetTypeList 返回所有类型代码及名称 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// GetHypertensionDrug 返回所有数据信息 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetHypertensionDrug", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// <summary>
        ///  CmMstDiabetesDrugGetTypeList 返回所有类型代码及名称 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.CmMstDiabetesDrugGetTypeList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        ///  GetDiabetesDrug 返回所有数据信息 CSQ 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
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
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "DictMethod.GetDiabetesDrug", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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