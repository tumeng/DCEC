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
    internal class PlanBOMDal : BaseDalClass
    {
        public List<PlanBomEntity> GetByPlanCode(string planCode)
        {
            return db.Fetch<PlanBomEntity>("where PLAN_CODE=@0", planCode);
        }

        public List<PlanBomEntity> GetAll()
        {
            return db.Fetch<PlanBomEntity>("");
        }

        public List<PlanBomEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            string _b = b.ToShortDateString() + " 00:00:00";
            string _e = e.ToShortDateString() + " 23:59:59";
            return db.Fetch<PlanBomEntity>("where create_time between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')", _b, _e);
        }

        public List<PlanBomEntity> GetByPlanCodes(string[] planCodes)
        {
            try
            {
                return db.Fetch<PlanBomEntity>("select d.* from DATA_PLAN_BOM d where d.PLAN_CODE in (@t) ORDER BY PLAN_CODE DESC", new { t = planCodes });
            }
            catch (Exception ex)
            {
                return new List<PlanBomEntity>(); ;
            }
        }

        public int Update(PlanBomEntity p)
        {
            return db.Update("DATA_PLAN_BOM","RMES_ID",p); 
        }

        public List<PlanBomEntity> GetByLineSideStockCode(string stockCode)
        {
            return db.Fetch<PlanBomEntity>("where LINESIDE_STOCK_CODE=@0", stockCode);
        }

        public List<PlanBomEntity> GetByTeamCode(string teamID)
        {
            return db.Fetch<PlanBomEntity>("where TEAM_CODE=@0",teamID);
        }

        public List<PlanBomEntity> GetByProjectCode(string projectCode)
        {
            return db.Fetch<PlanBomEntity>("where PROJECT_CODE=@0", projectCode);
        }

        public PlanBomEntity GetByItemCodeAndStockCode(string itemCode, string storeCode)
        {
            try
            {
                return db.First<PlanBomEntity>("where ITEM_CODE=@0 and LINESIDE_STOCK_CODE=@1",itemCode,storeCode);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<PlanBomEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<PlanBomEntity>("where ORDER_CODE=@0", orderCode);
        }
    }
}
