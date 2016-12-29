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
    public static class SAPPlanProcessFactory
    {
        public static List<SAPPlanProcessEntity> GetAll()
        {
            return new SAPPlanProcessDal().GetAll();
        }

        public static bool Insert(SAPPlanProcessEntity entity)
        {
            return new SAPPlanProcessDal().Insert(entity);
        }

        public static List<SAPPlanProcessEntity> GetByOrderCode(string orderCode)
        {
            return new SAPPlanProcessDal().GetByOrderCode(orderCode);
        }

        public static SAPPlanProcessEntity GetByID(string id)
        {
            return new SAPPlanProcessDal().GetByKey(id);
        }

        public static bool Update(SAPPlanProcessEntity entity)
        {
            return new SAPPlanProcessDal().Update(entity);
        }
    }
}
