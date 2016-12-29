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
/// 功能描述：工作站站点控件操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class ProductControlFactory
    {
        public static List<ProductControlEntity> GetAll()
        {
            return new ProductControlDal().GetAll();
        }
        public static ProductControlEntity GetByKey(string strID)
        {
            return new ProductControlDal().GetByKey(strID);
        }

        public static List<ProductControlEntity> GetSingleByKey(string RmesID)
        {
            return new ProductControlDal().GetSingleByKey(RmesID);
        }

        public static List<ProductControlEntity> GetCurrentCompleteInfo(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            return new ProductControlDal().GetCurrentCompleteInfo(CompanyCode, PlineCode, StationCode, PlanCode, SN);

        }
        public static void HandleControlComplete(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {
            new ProductControlDal().HandleControlComplete(CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);
        }

        //public static void MW_COMPARE_GHTMBOM(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        //{
        //    new ProductControlDal().MW_COMPARE_GHTMBOM(CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);
        //}

    }
}