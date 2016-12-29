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
    internal class SAPPlanProcessDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<SAPPlanProcessEntity></returns>
        public List<SAPPlanProcessEntity> GetAll()
        {
            return db.Fetch<SAPPlanProcessEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>SAPPlanProcessEntity</returns>
        public SAPPlanProcessEntity GetByKey(string RmesID)
        {
            return db.First<SAPPlanProcessEntity>("WHERE ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<SAPPlanProcessEntity></returns>
        public List<SAPPlanProcessEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SAPPlanProcessEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public bool Insert(SAPPlanProcessEntity entity)
        {
            try
            {
                db.Insert("ISAP_DATA_PLAN_PROCESS", "ID", entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SAPPlanProcessEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<SAPPlanProcessEntity>("WHERE AUFNR=@0", orderCode);
        }

        public bool Update(SAPPlanProcessEntity entity)
        {
            try
            {
                db.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
