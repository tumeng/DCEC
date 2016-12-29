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
    public static class AtpuFactory
    {
        public static List<AtpuEntity> GetAll()
        {
            return new AtpuDal().GetAll();
        }

        public static AtpuEntity GetByStationCode(string scode,string plinecode)
        {
            if (string.IsNullOrWhiteSpace(scode) ) return null;
            try
            {
                return new AtpuDal().GetByStationCode(scode, plinecode).First<AtpuEntity>();
            }
            catch
            {
                return null;
            }
        }
    }
}
