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
    public static class SNDetectTempFactory
    {
        public static List<SNDetectTempEntity> GetAll()
        {
            return new SNDetectTempDal().GetAll();
        }
        public static SNDetectTempEntity GetByKey(string strID)
        {
            return new SNDetectTempDal().GetByKey(strID);
        }

        public static List<SNDetectTempEntity> GetSingleByKey(string RmesID)
        {
            return new SNDetectTempDal().GetSingleByKey(RmesID);
        }


        public static List<SNDetectTempEntity> GetBySNStation(string SN, string StationCode)
        {
            return new SNDetectTempDal().GetBySNStation(SN, StationCode);
        }

        public static void InitQualitDetectList(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string UserID,string stationid1)
        {
            new SNDetectTempDal().InitQualitDetectList(CompanyCode, PlineCode, StationCode, PlanCode, SN, UserID,stationid1);
        }

        public static void HandleDetectData(string CompanyCode, string RmesID, string QuanValule,string QualValue,string faultCode,string Remark, string UserID,string stationcode,string stationname)
        {
            new SNDetectTempDal().HandleDetectData(CompanyCode, RmesID, QuanValule,QualValue,faultCode, Remark, UserID,stationcode,stationname);
        }
    }
}