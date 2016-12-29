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
    public static class WorkShopFactory
    {
        public static List<WorkShopEntity> GetUserWorkShops(string userid)
        {
            List<ProductLineEntity> _plines = new ProductLineDal().GetByUserIDCS(userid,"");
            List<string> _p = (from s in _plines select s.RMES_ID).ToList<string>();
            return new WorkShopDal().FindBySql<WorkShopEntity>("where rmes_id in (select workshop_code from rel_workshop_pline where pline_code in (@plines))", new { plines=_p });
        }
        public static WorkShopEntity GetByKey(string WorkshopCode)
        {
            try
            {
                return new WorkShopDal().GetByKey(WorkshopCode);
            }
            catch
            {
                return null;
            }
        }
        public static List<WorkShopEntity> GetAll()
        {
            return new WorkShopDal().FindAll<WorkShopEntity>();
        }
        public static List<WorkShopEntity> GetByWorkShopID(string wid)
        {
            return new WorkShopDal().FindBySql<WorkShopEntity>("where rmes_id=@0",wid);
        }
        public static WorkShopEntity GetByNumber(string rmes_id)
        {
            return new WorkShopDal().GetByNumber(rmes_id);
        }
    }
}
