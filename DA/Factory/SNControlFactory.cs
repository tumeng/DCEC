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
    public static class SNControlFactory
    {
        public static List<SNControlEntity> GetAll()
        {
            return new SNControlDal().GetAll();
        }


        public static List<SNControlEntity> GetByPlanStatonSn(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            return new SNControlDal().GetByPlanStatonSn(CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }

        public static List<SNControlEntity> GetByPlanStaton(string CompanyCode, string PlineCode, string StationCode, string PlanCode)
        {
            return new SNControlDal().GetByPlanStaton(CompanyCode, PlineCode, StationCode, PlanCode);
        }
    }

}