using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class WMSstoreDal : Base.BaseDalClass
    {
        public WMSstoreDal()
        { }

        public List<WMSstoreEntity> GetAll()
        {
            return db.Fetch<WMSstoreEntity>("");
        }
        public WMSstoreEntity GetByItemCode(string itemCode)
        {
            List<WMSstoreEntity> lst = db.Fetch<WMSstoreEntity>("where m_id=@0", itemCode);
            if (lst.Count > 0)
                return lst.First();
            else
                return null;

        }

    }
}
