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

namespace Rmes.DA.Dal
{
    internal class StoreDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<StoreEntity></returns>
        public List<StoreEntity> GetAll()
        {
            return db.Fetch<StoreEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>StoreEntity</returns>
        public StoreEntity GetByKey(string RmesID)
        {
            return db.First<StoreEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<StoreEntity></returns>
        public List<StoreEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<StoreEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<StoreEntity> GetByPlanCode(string planCode)
        {
            return db.Fetch<StoreEntity>("where PLAN_CODE=@0 order by WORK_TIME desc",planCode);
        }
    }
}
