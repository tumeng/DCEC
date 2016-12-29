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
    internal class PlanStandardBOMDal : BaseDalClass
    {
        public List<PlanStandardBOMEntity> GetByPlanCode(string planCode)
        {
            return db.Fetch<PlanStandardBOMEntity>("where PLAN_CODE=@0",planCode);
        }

        public List<PlanStandardBOMEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<PlanStandardBOMEntity>("where ORDER_CODE=@0", orderCode);
        }
        public List<PlanStandardBOMEntity> GetByPlanCodes(string[] planCodes)
        {
            return db.Fetch<PlanStandardBOMEntity>("where PLAN_CODE in (@s)", new { s=planCodes});
        }

        public List<PlanStandardBOMEntity> GetByOrderCodeAndProcessCode(string orderCode, string processCode)
        {
            return db.Fetch<PlanStandardBOMEntity>("where ORDER_CODE=@0 and PROCESS_CODE=@1", orderCode, processCode);
        }


        public List<PlanStandardBOMEntity> GetByOrderCodeAndWorkUnit(string orderCode, string workunitCode)
        {
            return db.Fetch<PlanStandardBOMEntity>("where ORDER_CODE=@0 and workunit_code=@1", orderCode, workunitCode);
        }




        public PlanStandardBOMEntity GetByID(string id)
        {
            try
            {
                return db.First<PlanStandardBOMEntity>("where RMES_ID=@0", id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
