using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class IMESPlanProcessFactory
    {
        public static List<IMESPlanProcessEntity> GetByOrderCode(string orderCode)
        {
            return new IMESPlanProcessDal().GetByOrderCode(orderCode);
        }
    }
}
