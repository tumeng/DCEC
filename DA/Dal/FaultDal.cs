using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using System.Data;

namespace Rmes.DA.Dal
{
   internal  class FaultDal:BaseDalClass
    {
       public List<FaultEntity> GetAll()
       {
           return db.Fetch<FaultEntity>("");
       }
    }
}
