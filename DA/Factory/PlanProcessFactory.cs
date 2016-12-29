using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class PlanProcessFactory
    {
        public static List<PlanProcessEntity> GetAll()
        {
            return new PlanProcessDal().GetAll();
        }


        public static List<PlanProcessEntity> GetByOrderCode(string orderCode)
        {
            return new PlanProcessDal().GetByOrderCode(orderCode);
        }
        public static List<PlanProcessEntity> GetByPlan(string PlanCode)
        {
            return new PlanProcessDal().GetByPlan(PlanCode);
        }

        public static List<PlanProcessEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            return new PlanProcessDal().GetByCreatePeriod(b, e);
        }
    }
}
