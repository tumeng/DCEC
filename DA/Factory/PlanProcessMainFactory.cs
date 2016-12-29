using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class PlanProcessMainFactory
    {
        public static List<PlanProcessMainEntity> GetAll()
        {
            return new PlanProcessMainDal().GetAll();
        }

        public static List<PlanProcessMainEntity> GetUnHandledByOrderCode(string orderCode)
        {
            return new PlanProcessMainDal().GetUnHandledByOrderCode(orderCode);
        }

        public static List<PlanProcessMainEntity> GetByOrderCode(string orderCode)
        {
            return new PlanProcessMainDal().GetByOrderCode(orderCode);
        }

        public static PlanProcessMainEntity GetByRmesID(string id)
        {
            return new PlanProcessMainDal().GetByID(id);
        }

        public static List<PlanProcessMainEntity> GetByIDs(string[] id)
        {
            return new PlanProcessMainDal().GetByIDs(id);
        }

        public static PlanProcessMainEntity GetByOrderCodeAndProcessCode(string orderCode,string processCode)
        {
            return new PlanProcessMainDal().GetByOrderCodeAndProcessCode(orderCode,processCode);
        }
    }
}
