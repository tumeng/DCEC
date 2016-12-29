using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using System.Data;

namespace Rmes.DA.Dal
{
    internal class SnEditDal:BaseDalClass
    {
        public SnEditEntity GetBySN(string sn)
        {
            return db.First<SnEditEntity>("where PLAN_SN=@0", sn);
        }
        public List<SnEditEntity> GetBySnS(string SN)
        {
            return db.Fetch<SnEditEntity>("where PLAN_SN=@0", SN);
        }
        public List<SnEditEntity> GetAll()
        {
            return db.Fetch<SnEditEntity>("");
        }
        public void insert(SnEditEntity sn)
        {
            db.Insert("DATA_SN_EDIT", "PLAN_SN",false,sn);
        }
    }
}
