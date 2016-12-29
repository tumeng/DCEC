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
    public static class ProductDataFactory
    {
        public static void OnLine(PlanSnEntity en)
        {
            //PlanSnEntity plansn1 = PlanSnFactory.GetBySn(en.PLAN_SN);
            PlanEntity plan1 = PlanFactory.GetByKey(en.PLAN_CODE);
            //if (en.SN_FLAG == "P") return;
            //else PrintDriver.SendStringToPrinter("S4M", code);
            if (en.SN_FLAG == "N")
            {
                en.SN_FLAG = "P";
                PlanSnFactory.update(en);
                plan1.ONLINE_QTY = plan1.ONLINE_QTY + 1;
                PlanFactory.Update(plan1);
            }
        }
    }
}
