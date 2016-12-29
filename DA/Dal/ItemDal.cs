using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-08
//
#endregion

#region
namespace Rmes.DA.Dal
{
    internal class ItemDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ItemEntity> GetAll()
        {
            return db.Fetch<ItemEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public ItemEntity GetByKey(string RmesID)
        {
            return db.First<ItemEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ItemEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ItemEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public ItemEntity GetByItem(string CompanyCode, string ItemCode)
        {
            try
            {
                return db.First<ItemEntity>("WHERE COMPANY_CODE=@0 AND ITEM_CODE=@1", CompanyCode, ItemCode);
            }
            catch
            {
                return null;
            }
        }

       

    }
}
#endregion