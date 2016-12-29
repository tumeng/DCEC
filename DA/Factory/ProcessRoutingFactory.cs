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
    public static  class ProcessRoutingFactory
    {
        public static List<ProcessRoutingEntity> GetByStationCode(string StationRmesID)
        {
            List<ProcessRoutingEntity> LIST = new List<ProcessRoutingEntity>();
            List<LocationEntity> LocationList = LocationFactory.GetByStationCode(StationRmesID);
            if (LocationList.Count < 1)
                return null;
            else
            {
                for (int i = 0; i < LocationList.Count; i++)
                {
                    List<ProcessRoutingEntity> ProcessList = new ProcessRoutingDal().GetByWorkunit(LocationList[i].LOCATION_CODE);
                    LIST.AddRange(ProcessList);
                }
                return LIST;
            }

        }

        public static ProcessRoutingEntity GetByRmesID(string id)
        {
            return new ProcessRoutingDal().GetByRmesID(id);
        }

        public static List<ProcessRoutingEntity> GetByPlineCode(string PlineCode)
        {
            return new ProcessRoutingDal().GetByWorkunit(PlineCode);
        }

        public static List<ProcessRoutingEntity> GetAll()
        {
            return new ProcessRoutingDal().GetAll();
        }

        public static void Insert(ProcessRoutingEntity p)
        {
            new ProcessRoutingDal().Insert(p);
        }

        public static int Update(ProcessRoutingEntity p)
        {
            return new ProcessRoutingDal().Update(p);
        }
    }
}
