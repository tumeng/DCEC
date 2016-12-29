using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
namespace Rmes.DA.Dal
{
    internal class PlanTempDal:BaseDalClass
    {
        public List<PlanTempEntity> GetUnfinishedPlan()
        {
            return db.Fetch<PlanTempEntity>("where HANDLE_FLAG!='F'");
        }

        public PlanTempEntity GetByOrderCode(string OrderCode)
        {
            try
            {
                return db.First<PlanTempEntity>("where PLAN_CODE=@0",OrderCode);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<PlanTempEntity> GetCurrentUnfinishedPlanByPlines(string[] plineCodes)
        {
            DateTime curDate = DB.GetServerTime();
            return db.Fetch<PlanTempEntity>("where PLINE_CODE in (@p) and HANDLE_FLAG!='F'", new { p = plineCodes });
        }
    }
}
 