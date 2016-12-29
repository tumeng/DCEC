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
    public static class PlanSubpackFactory
    {
        public static List<PlanSubpackEntity> GetAll()
        {
            return new PlanSubpackDal().GetAll();
        }
        public static PlanSubpackEntity GetByKey(string strID)
        {
            return new PlanSubpackDal().GetByKey(strID);
        }

        public static List<PlanSubpackEntity> GetSingleByKey(string RmesID)
        {
            return new PlanSubpackDal().GetSingleByKey(RmesID);
        }

        public static List<PlanSubpackEntity> GetSubpackPlanByStation(string CompanyCode, string PLineCode, string StationCode)
        {
            return new PlanSubpackDal().GetSubpackPlanByStation(CompanyCode, PLineCode, StationCode);

        }
    }
}