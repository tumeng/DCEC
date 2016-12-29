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
    internal class IMESPlanBOMDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<IMESCompleteBatchEntity></returns>
        public List<IMESPlanBOMEntity> GetAll()
        {
            return db.Fetch<IMESPlanBOMEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>IMESCompleteBatchEntity</returns>
        public IMESPlanBOMEntity GetByKey(string RmesID)
        {
            return db.First<IMESPlanBOMEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<IMESCompleteBatchEntity></returns>
        public List<IMESPlanBOMEntity> GetSingleByKey(string id)
        {
            return db.Fetch<IMESPlanBOMEntity>("WHERE ID=@0", id);
        }

        public List<IMESPlanBOMEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<IMESPlanBOMEntity>("where AUFNR=@0", orderCode);
        }

    }
}
