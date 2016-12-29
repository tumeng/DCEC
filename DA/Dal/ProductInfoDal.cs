using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-08
//
#endregion

#region
namespace Rmes.DA.Dal
{
    internal class ProductInfoDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ProductInfoEntity> GetAll()
        {
            return db.Fetch<ProductInfoEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public ProductInfoEntity GetByKey(string RmesID)
        {
            return db.First<ProductInfoEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ProductInfoEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ProductInfoEntity>("WHERE RMES_ID=@0", RmesID);
        }
        public List<ProductInfoEntity> GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            return db.Fetch<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND SN=@1 and is_valid='Y'  ", CompanyCode, SN);
        }
        public List<ProductInfoEntity> GetByCompanyCodeSNLQ(string CompanyCode, string SN,string oldplan)
        {
            return db.Fetch<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND SN=@1 and plan_code=@2   ", CompanyCode, SN,oldplan);
        }
        public List<ProductInfoEntity> GetByCompanyCodeSNPline(string CompanyCode, string SN,string pline)
        {
            return db.Fetch<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND SN=@1 and pline_code=@2 and is_valid='Y'", CompanyCode, SN,pline);
        }
        public List<ProductInfoEntity> GetByCompanyCodeSNPline1(string CompanyCode, string SN, string pline)
        {
            return db.Fetch<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND SN=@1 and is_valid='Y' and rownum=1 ", CompanyCode, SN, pline);
        }
        public List<ProductInfoEntity> GetByCompanyCodeBatch(string CompanyCode, string PlanBatch)
        {
            return db.Fetch<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND PLAN_BATCH=@1", CompanyCode, PlanBatch);
        }
        public ProductInfoEntity GetByCompanyPlan(string CompanyCode, string PlanCode)
        {
            return db.First<ProductInfoEntity>("WHERE COMPANY_CODE=@0 AND PLAN_CODE=@1", CompanyCode, PlanCode);
        }

    }
}
#endregion