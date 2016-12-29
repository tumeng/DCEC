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
/// 功能描述：工作站点BOM物料确认相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class SNBomFactory
    {
        public static List<SNBomEntity> GetAll()
        {
            return new SNBomDal().GetAll();
        }



        //public static List<SNBomEntity> ShowBom(string CompanyCode, string PlanID, string PlineCode, string StationCode)
        //{
        //    return new SNBomDal().GetByPlanStaton(CompanyCode, PlanID, PlineCode, StationCode);
        //}

        public static List<SNBomEntity> GetByPlanCode(string planCode)
        {
            return new SNBomDal().GetByPlanCode(planCode);
        }
       
    }

}