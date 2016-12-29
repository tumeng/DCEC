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
    public  class ProductCompleteDal : BaseDalClass
    {
        
        //查询产品信息从DATA_COMPLETE表
        public List<ProductCompleteEntity> GetByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn)
        {
            string SQL = "where company_code=@0 and pline_code=@1 and plan_code=@2 and sn=@3";
            return db.Fetch<ProductCompleteEntity>(SQL, CompanyCode, PlineCode, PlanCode, Sn);
        }
        public List<ProductCompleteEntity> GetByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn, string StationCode)
        {
            string SQL = "where company_code=@0 and pline_code=@1 and plan_code=@2 and sn=@3 and Station_Code = @4";
            return db.Fetch<ProductCompleteEntity>(SQL, CompanyCode, PlineCode, PlanCode, Sn, StationCode);
        }
        public List<ProductCompleteEntity> GetByPlanStation(string PlanCode, string StationCode)
        {
            string SQL = "where plan_code=@0 and Station_Code = @1";
            return db.Fetch<ProductCompleteEntity>(SQL, PlanCode,StationCode);
        }
        public int GetCompleteQtyByPlanStation(string CompanyCode, string PlineCode, string PlanCode, string StationCode)
        {
            string SQL="where company_code=@0 and pline_code=@1 and plan_code=@2 and station_code=@3 and complete_time is not null";
            List<ProductCompleteEntity> lst = db.Fetch<ProductCompleteEntity>(SQL, CompanyCode, PlineCode, PlanCode, StationCode);
            if (lst.Count > 0)
            {
                int complete_qty = lst.Sum(a => a.BATCH_QTY);
                return complete_qty;
            }
            else return 0;

        }
    }
}
