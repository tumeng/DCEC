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
/// 功能描述：SN功能模块相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class PlanSnFactory
    {
        public static List<PlanSnEntity> GetAll()
        {
            return new PlanSnDal().GetAll();
        }
        public static PlanSnEntity GetByKey(string strID)
        {
            return new PlanSnDal().GetByKey(strID);
        }

        public static List<PlanSnEntity> GetSingleByKey(string RmesID)
        {
            return new PlanSnDal().GetSingleByKey(RmesID);
        }

        public static PlanSnEntity GetBySn(string sn)
        {
            return new PlanSnDal().GetBySn(sn);
        }
        public static PlanSnEntity GetBySnPline(string sn, string pcode)
        {
            return new PlanSnDal().GetBySnPline(sn, pcode);
        }
        public static List<PlanSnEntity> GetByOrderCode(string orderCode)
        {
            return new PlanSnDal().GetByOrderCode(orderCode);
        }

        public static List<PlanSnEntity> WebGetByOrderCode(string orderCode)
        {
            return new PlanSnDal().WebGetByOrderCode(orderCode);
        }

        public static List<PlanSnEntity> GetSnByPlanCode(string planID)
        {
            return new PlanSnDal().GetByPlanCode(planID);
        }
        public static List<PlanSnEntity> GetSnByPlanCodedesc(string planID)
        {
            return new PlanSnDal().GetByPlanCodedesc(planID);
        }
        

        public static void InitStationControl(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn,string controlname)
        {
            new PlanSnDal().InitStationControl(CompanyCode, PlineCode, StationCode, PlanCode, Sn, controlname);
        }

        public static void InitPlanSnStationData(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn, string UserID)
        {
            new PlanSnDal().InitPlanSnStationData(CompanyCode, PlineCode, StationCode, PlanCode, Sn,UserID);
        }
        public static void update(PlanSnEntity en)
        {
            PlanSnDal dal = new PlanSnDal();
            dal.Update(en);
        }


    }
}