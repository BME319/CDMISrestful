using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.Models
{
    public interface ICmMstInfoItemRepository
    {
        IEnumerable<CmMstInfoItem> GetAll();
        //CmMstInfoItem Get(string CategoryCode, string Code, int StartDate);
        bool AddItem(CmMstInfoItem item);
        int Remove(string CategoryCode, string Code, int StartDate);
        bool Update(CmMstInfoItem item);
     
    }
}