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
/// 功能描述：工作站站点工序完成确认相关数据查询及操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class SNProcessTempFactory
    {
        public static List<SNProcessTempEntity> GetAll()
        {
            return new SNProcessTempDal().GetAll();
        }
        public static SNProcessTempEntity GetByKey(string strID)
        {
            return new SNProcessTempDal().GetByKey(strID);
        }

        public static List<SNProcessTempEntity> GetSingleByKey(string RmesID)
        {
            return new SNProcessTempDal().GetSingleByKey(RmesID);
        }

        public static List<SNProcessTempEntity> GetByPlanSo(string planso)
        {
            return new SNProcessTempDal().FindBySql<SNProcessTempEntity>("where process_code=@0",planso);
        }

        public static List<SNProcessTempEntity> GetProcessList(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            return new SNProcessTempDal().GetProcessList(CompanyCode, PlineCode, StationCode, PlanCode, SN);
        }

        public static bool ProcessNotConfirmed(string CompanyCode, string PlanCode, string Sn, string PlineCode, string Station)
        {
            return (new SNProcessTempDal()).ProcessNotConfirmed(CompanyCode, PlanCode, Sn, PlineCode, Station);
        }

        public static void HandleProcessComplete(string RmesID, string StationCode, string CompleteFlag)
        {
            new SNProcessTempDal().HandleProcessComplete(RmesID, StationCode, CompleteFlag);
        }
        


    }
}