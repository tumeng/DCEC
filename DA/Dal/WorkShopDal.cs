using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class WorkShopDal : BaseDalClass
    {
        public List<WorkShopEntity> GetAll()
        {
            return db.Fetch<WorkShopEntity>("");
        }
        public WorkShopEntity GetByKey(string key)
        {
            return db.First<WorkShopEntity>("where WORKSHOP_CODE=@0", key);
        }
        public WorkShopEntity GetByNumber(string rmes_id)
        {
            //List<WorkShopEntity> list = this.GetAll();
            
            return db.First <WorkShopEntity>("where RMES_ID=@0", rmes_id);
        }
    }
}
