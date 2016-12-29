using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class ProcessRoutingDal:BaseDalClass
    {
        public List<ProcessRoutingEntity> GetByWorkunit(string WorkUnit)
        {
            return db.Fetch<ProcessRoutingEntity>("where WORKUNIT_CODE=@0", WorkUnit);
        }

        public ProcessRoutingEntity GetByRmesID(string id)
        {
            try
            {
                return db.First<ProcessRoutingEntity>("where RMES_ID=@0",id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ProcessRoutingEntity> GetAll()
        {
            return db.Fetch<ProcessRoutingEntity>("order by RMES_ID desc");
        }

        public void Insert(ProcessRoutingEntity p)
        {
            db.Insert("DATA_PROCESS_ROUTING", "RMES_ID", p);
        }

        public int Update(ProcessRoutingEntity p)
        {
            return db.Update(p);
        }

        public void Delete(ProcessRoutingEntity p)
        {
            db.Delete(p);
        }
    }
}
