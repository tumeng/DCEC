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
    public static class ProductLineFactory
    {
        public static List<ProductLineEntity> GetByDep(string dep)
        {
            return new ProductLineDal().GetByDep(dep);
        }

        public static List<ProductLineEntity> GetAll()
        {
            return new ProductLineDal().GetAll();
        }
        public static List<ProductLineEntity> GetAllSUB(string dep)
        {
            return new ProductLineDal().GetAllSUB(dep);
        }
        public static ProductLineEntity GetByKey(string PlineCode)
        {
            return new ProductLineDal().GetByKey(PlineCode);
        }
        public static ProductLineEntity GetByStationCode(string StationCode)
        {
            return new ProductLineDal().GetByStationCode(StationCode);
        }
        public static List<ProductLineEntity> GetByCompanyCode(string CompanyCode)
        {
            return new ProductLineDal().GetByCompanyCode(CompanyCode);
        }
        public static List<ProductLineEntity> GetByUserIDCS(string uid,string ustationcode)
        {
            return new ProductLineDal().GetByUserIDCS(uid, ustationcode);
        }
        public static List<RELStationEntity> GetByUS(string ustationcode)
        {
            return new ProductLineDal().GetByUS( ustationcode);
        }
        public static List<RELStationEntity> GetByUS1(string uid, string ustationcode)
        {
            return new ProductLineDal().GetByUS1(uid,ustationcode);
        }
        public static List<ProductLineEntity> GetByUserID(string uid, string programcode)
        {
            return new ProductLineDal().GetByUserID(uid, programcode);
        }
        public static List<ProductLineEntity> GetByUserIDSub(string uid, string program_code)
        {
            return new ProductLineDal().GetByUserIDSub(uid, program_code);
        }
        public static List<ProductLineEntity> GetByWorkShopID(string workshopRmesID)
        {
            return new ProductLineDal().FindBySql<ProductLineEntity>("where RMES_ID in (select PLINE_CODE from rel_workshop_pline where workshop_code=@0)",workshopRmesID);
        }

        public static List<ProductLineEntity> GetByIDs(string[] ids)
        {
            return new ProductLineDal().GetByIDs(ids);
        }
        public static string GetWorkdate(string company_code, string plinecode, string shiftcode)
        {
            return new ProductLineDal().GetWorkdate(company_code, plinecode, shiftcode);
        }
    }

}
