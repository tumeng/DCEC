using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：limm
/// 功能描述：站点产品装配完成相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class ProductSnFactory
    {
        public static List<ProductSnEntity> GetAll()
        {
            return new ProductSnDal().GetAll();
        }

        public static ProductSnEntity GetByKey(string strID)
        {
            return new ProductSnDal().GetByKey(strID);
        }

        public static List<ProductSnEntity> GetSingleByKey(string RmesID)
        {
            return new ProductSnDal().GetSingleByKey(RmesID);
        }

        public static List<ProductSnEntity> GetCurrentSN(string CompanyCode, string PlineCode, string StationCode)
        {
            return new ProductSnDal().GetCurrentSN(CompanyCode, PlineCode, StationCode);
        }

        public static void HandleStationComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN,
                                           string QualityStatus)
        {
            new ProductSnDal().HandleStationComplete(CompanyCode, PlineCode, StationCode, PlanCode, SN,  QualityStatus);
        }
        public static void HandleStationPause(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN,string QualityStatus)
        {
            new ProductSnDal().HandleStationPause(CompanyCode, PlineCode, StationCode, PlanCode, SN, QualityStatus);
        }
    }
}