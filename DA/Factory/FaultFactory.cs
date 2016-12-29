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
    public static  class FaultFactory
    {
        public static List<FaultEntity> GetAll()
        {
            return new FaultDal().GetAll();
        }

    }
}
