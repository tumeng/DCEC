using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;

namespace Rmes.DA.Factory
{
    public static class ScreenLedFactory
    {
        public static List<ScreenLEDEntity> GetAllLed()
        {
            return new LedDal().GetAllLeds();
        }
        public static ScreenLEDEntity GetByKey(string ipaddress)
        {
            return new LedDal().ReadByPrimaryKey(ipaddress);
        }
        public static int Update(ScreenLEDEntity en)
        {
            return new LedDal().Update(en);
        }
    }
}
