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
    public class IMESPlanChangeDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<IMESPlanChangeEntity></returns>
        public List<IMESPlanChangeEntity> GetAll()
        {
            return db.Fetch<IMESPlanChangeEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>IMESPlanChangeEntity</returns>
        public IMESPlanChangeEntity GetByKey(string RmesID)
        {
            return db.First<IMESPlanChangeEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<IMESPlanChangeEntity></returns>
        public List<IMESPlanChangeEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<IMESPlanChangeEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public string Insert(IMESPlanChangeEntity change)
        {
            return db.Insert("IMES_DATA_PLAN_CHANGE", "ID", true, change).ToString();
        }

    }
}
