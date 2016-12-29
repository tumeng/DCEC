using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class IMESLine2LineFactory
    {
        public static List<IMESLine2LineEntity> GetByOrderCode(string orderCode)
        {
            return new IMESLine2LineDal().GetByOrderCode(orderCode);

        }

    }
}
