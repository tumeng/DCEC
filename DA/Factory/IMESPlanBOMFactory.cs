using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class IMESPlanBOMFactory
    {
        public static List<IMESPlanBOMEntity> GetByOrderCode(string orderCode)
        {
            return new IMESPlanBOMDal().GetByOrderCode(orderCode);
        }
    }
}
