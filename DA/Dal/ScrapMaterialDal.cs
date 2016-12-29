using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class ScrapMaterialDal : BaseDalClass
    {
        public object Insert(ScrapMaterialEntity entity)
        {
            return db.Insert("DATA_SCRAP_MATERIAL", "RMES_ID", entity);
        }
    }
}
