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

namespace Rmes.DA.Factory
{
    public static class StoreFactory
    {
        public static List<StoreEntity> GetByPlanCode(string planCode)
        {
            return new StoreDal().GetByPlanCode(planCode);
        }
    }
}
