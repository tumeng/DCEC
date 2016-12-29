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
    internal class SAPPlanDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<SAPPlanEntity></returns>
        public List<SAPPlanEntity> GetAll()
        {
            return db.Fetch<SAPPlanEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>SAPPlanEntity</returns>
        public SAPPlanEntity GetByKey(string RmesID)
        {
            return db.First<SAPPlanEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<SAPPlanEntity></returns>
        public List<SAPPlanEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SAPPlanEntity>("WHERE RMES_ID=@0", RmesID);
        }


        public SAPPlanEntity GetByOrderCode(string orderCode)
        {
            try
            {
                return db.First<SAPPlanEntity>("where AUFNR=@0", orderCode);
            }
            catch
            {
                return null;
            }
        }

        public List<SAPPlanEntity> GetByProjectCode(string projectCode)
        {
            return db.Fetch<SAPPlanEntity>("where TEXT1=@0",projectCode);

        }

        public bool Insert(SAPPlanEntity entity)
        {
            try
            {
                db.Insert("ISAP_DATA_PLAN", "ID", entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
