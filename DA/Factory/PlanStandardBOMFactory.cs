using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class PlanStandardBOMFactory
    {
        public static List<PlanStandardBOMEntity> GetByPlanCode(string planCode)
        {
            return new PlanStandardBOMDal().GetByPlanCode(planCode);
        }
        public static List<PlanStandardBOMEntity> GetByOrderCode(string orderCode)
        {
            return new PlanStandardBOMDal().GetByOrderCode(orderCode);
        }
        public static List<PlanStandardBOMEntity> GetByPlanCodes(string[] planCodes)
        {
            return new PlanStandardBOMDal().GetByPlanCodes(planCodes);
        }

        public static List<PlanStandardBOMEntity> GetAll()
        {
            return new PlanStandardBOMDal().FindAll<PlanStandardBOMEntity>();
        }

        public static List<PlanStandardBOMEntity> GetByOrderCodeAndProcessCode(string orderCode, string processCode)
        {
            return new PlanStandardBOMDal().GetByOrderCodeAndProcessCode(orderCode,processCode);
        }


        public static List<PlanStandardBOMEntity> GetByOrderCodeAndWorkUnit(string orderCode, string workunitCode)
        {
            return new PlanStandardBOMDal().GetByOrderCodeAndWorkUnit(orderCode, workunitCode);
        }


        public static PlanStandardBOMEntity GeyByID(string id)
        {
            return new PlanStandardBOMDal().GetByID(id);
        }

        public static List<PlanStandardBOMEntity> GetByPlanSO(string planSO)
        {
            List<PlanEntity> plans = PlanFactory.GetByPlanSO(planSO);
            if (plans.Count > 0)
            {
                return new PlanStandardBOMDal().GetByOrderCode(plans[0].ORDER_CODE);
            }
            else
            {
                return null;
            }
        }
    }
}
