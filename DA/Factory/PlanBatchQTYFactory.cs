using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Factory
{
    public static class PlanBatchQTYFactory
    {
        public static PlanBatchQTYEntity GetByStation(string StationCode)
        {
            return new PlanBatchQTYDal().GetByStation(StationCode);

        }
        public static void UpdatePlanBatchQty(string PlineCode, string StationCode,string PlanCode,string VItemCode,int BatchQty)
        {
            new PlanBatchQTYDal().UpdatePlanBatchQty( PlineCode, StationCode,PlanCode,VItemCode, BatchQty);
        }
    }
}
