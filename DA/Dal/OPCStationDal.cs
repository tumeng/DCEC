using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using System.Data;


namespace Rmes.DA.Dal
{
    internal class OPCStationDal : BaseDalClass
    {
        public OPCStationEntity GetByPlineCode(string pPlineCode)
        {
            return db.Fetch<OPCStationEntity>("WHERE pline_code=@0", pPlineCode).First();
        }
    }
}
