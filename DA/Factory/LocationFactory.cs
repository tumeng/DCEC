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
    public static class LocationFactory
    {
        public static List<LocationEntity> GetAll()
        {
            return new LocationDal().FindAll<LocationEntity>();
        }

        public static List<LocationEntity> GetByUserID(string userid, string programcode)
        {
            List<ProductLineEntity> _plines = ProductLineFactory.GetByUserID(userid, programcode);
            List<string> _ps = (from s in _plines select s.RMES_ID).ToList<string>();
            return new LocationDal().FindBySql<LocationEntity>("where pline_code in (@plines)", new { plines = _ps });
        }

        public static LocationEntity GetByMasterKey(string RMES_ID)
        {
            return new LocationDal().GetByMasterKey(RMES_ID);
        }
        public static List<LocationEntity> GetByStationCode(string StationCode)
        {
            return new LocationDal().GetByStationCode(StationCode);
        }

        public static List<LocationEntity> GetByPlineCode(string plineCode)
        {
            return new LocationDal().GetByPlineCode(plineCode);
        }
    }
}
