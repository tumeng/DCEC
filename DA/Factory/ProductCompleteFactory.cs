using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class ProductCompleteFactory
    {
        public static  List<ProductCompleteEntity> GetByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn)
        {
            return new  ProductCompleteDal().GetByPlanSn( CompanyCode, PlineCode, PlanCode, Sn);
        }
        public static List<ProductCompleteEntity> GetByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn, string StationCode)
        {
            return new ProductCompleteDal().GetByPlanSn(CompanyCode, PlineCode, PlanCode, Sn, StationCode);
        }
        public static List<ProductCompleteEntity> GetByPlanStation(string PlanCode, string StationCode)
        {
            return new ProductCompleteDal().GetByPlanStation(PlanCode, StationCode);
        }
        public static int GetCompleteQtyByPlanStation(string CompanyCode, string PlineCode, string PlanCode,string StationCode)
        {
            return new ProductCompleteDal().GetCompleteQtyByPlanStation(CompanyCode, PlineCode, PlanCode, StationCode);
        }

    }
}
