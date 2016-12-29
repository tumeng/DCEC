using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class IMESCompleteBatchFactory
    {
        public static object Insert(IMESCompleteBatchEntity entity)
        {
            return new IMESCompleteBatchDal().Insert(entity);
        }
    }
}
