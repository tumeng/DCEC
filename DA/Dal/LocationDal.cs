using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class LocationDal:BaseDalClass
    {
        public LocationEntity GetByMasterKey(string RMES_ID)
        {
            try
            {
                return db.First<LocationEntity>("where RMES_ID=@0",RMES_ID);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<LocationEntity> GetByStationCode(string StationCode)
        {
            return db.Fetch<LocationEntity>("select * from code_location a where a.LOCATION_CODE in (select t.location_code from rel_station_location t where t.station_code=@0)", StationCode);
        }

        public List<LocationEntity> GetByPlineCode(string plineCode)
        {
            return db.Fetch<LocationEntity>("where PLINE_CODE=@0 order by LOCATION_CODE desc",plineCode);
        }
    }
}
