using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using PetaPoco;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class LedDal:BaseDalClass
    {
        public List<ScreenLEDEntity> GetAllLeds()
        {
            return db.Fetch<ScreenLEDEntity>("");
        }
        public ScreenLEDEntity ReadByPrimaryKey(string ipaddress)
        {
            List<ScreenLEDEntity> list1 = db.Fetch<ScreenLEDEntity>("where IPADDRESS=@0", ipaddress);
            if (list1.Count < 1)
                return null;
            else
                return
                    list1[0];

        }
        public int Update(ScreenLEDEntity en)
        {
            return db.Update(en);
        }
    }
}
