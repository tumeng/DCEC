using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.DA.Factory
{
    public static class SNCompleteInstoreFactory
    {
        public static List<SNCompleteInstoreEntity> GetAll()
        {
            return new SNCompleteInstoreDal().GetAll();
        }

        public static SNCompleteInstoreEntity GetByPlanCode(string planCode)
        {
            return new SNCompleteInstoreDal().GetByPlanCode(planCode); ;
        }
    }
}
