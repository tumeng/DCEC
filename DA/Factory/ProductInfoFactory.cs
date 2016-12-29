using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：caoly
/// 功能描述：获取分装站点相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class ProductInfoFactory
    {
        public static List<ProductInfoEntity> GetAll()
        {
            return new ProductInfoDal().GetAll();
        }
        public static ProductInfoEntity GetByKey(string strID)
        {
            return new ProductInfoDal().GetByKey(strID);
        }
        public static List<ProductInfoEntity> GetSingleByKey(string RmesID)
        {
            return new ProductInfoDal().GetSingleByKey(RmesID);
        }
        public static List<ProductInfoEntity> GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            return new ProductInfoDal().GetByCompanyCodeSN(CompanyCode, SN);
        }
        public static List<ProductInfoEntity> GetByCompanyCodeSNPline(string CompanyCode, string SN,string pline)
        {
            return new ProductInfoDal().GetByCompanyCodeSNPline(CompanyCode, SN,pline);
        }
        public static List<ProductInfoEntity> GetByCompanyCodeSNPline1(string CompanyCode, string SN, string pline)
        {
            return new ProductInfoDal().GetByCompanyCodeSNPline1(CompanyCode, SN, pline);
        }
        //ADD by liuzhy，上一个GetByCompanyCodeSN不知为什么要返回List，按理SN应该返回唯一产品或者是空，因此写了下面这个。
        public static ProductInfoEntity GetByCompanyCodeSNSingle(string CompanyCode, string SN)
        {
            if (string.IsNullOrWhiteSpace(CompanyCode) || string.IsNullOrWhiteSpace(SN)) return null;
            try
            {
                return new ProductInfoDal().GetByCompanyCodeSN(CompanyCode, SN).First<ProductInfoEntity>();
            }
            catch
            {
                return null;
            }
        }
        public static ProductInfoEntity GetByCompanyCodeSNSingleLQ(string CompanyCode, string SN,string oldplan)
        {
            if (string.IsNullOrWhiteSpace(CompanyCode) || string.IsNullOrWhiteSpace(SN) || string.IsNullOrWhiteSpace(oldplan)) return null;
            try
            {
                return new ProductInfoDal().GetByCompanyCodeSNLQ(CompanyCode, SN,oldplan).First<ProductInfoEntity>();
            }
            catch
            {
                return null;
            }
        }
        public static List<ProductInfoEntity> GetByCompanyCodeBatch(string CompanyCode, string PlanBatch)
        {
            return new ProductInfoDal().GetByCompanyCodeBatch(CompanyCode, PlanBatch);
        }
        public static ProductInfoEntity GetByCompanyPlan(string CompanyCode, string PlanCode)
        {
            return new ProductInfoDal().GetByCompanyPlan(CompanyCode, PlanCode);
        }

    }
}