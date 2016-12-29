using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class SnEditFactory
    {
        public static void Update(SnEditEntity sn)
        {
            SnEditDal dal = new SnEditDal();
            dal.Update(sn);
        }
        public static void  Save(SnEditEntity sn)
        {
            SnEditDal dal = new SnEditDal();
            dal.insert(sn);
            
        }
        public static SnEditEntity GetBySN(string sn)
        {
            return new SnEditDal().GetBySN(sn);
        }
        public static List<SnEditEntity> GetBySnS(string SN)
        {
            return new SnEditDal().GetBySnS(SN);
        }
        public static List<SnEditEntity> GetAll()
        {
            return new SnEditDal().GetAll();
        }
    }
}
