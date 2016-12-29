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
    public static class SNBomTempFactory
    {
        public static List<SNBomTempEntity> GetAll()
        {
            return new SNBomTempDal().GetAll();
        }
        public static SNBomTempEntity GetByKey(string strID)
        {
            return new SNBomTempDal().GetByKey(strID);
        }

        public static List<SNBomTempEntity> GetSingleByKey(string RmesID)
        {
            return new SNBomTempDal().GetSingleByKey(RmesID);
        }

        public static List<SNBomTempEntity> ShowBom(string CompanyCode, string PlanID, string PlineCode, string StationCode, string Sn, string fdjxl, string so)
        {
            return new SNBomTempDal().GetByPlanStaton(CompanyCode, PlanID, PlineCode, StationCode,Sn,fdjxl,so);
        }
        public static void ShowBomDCEC(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so,string staioncode1)
        {
            new SNBomTempDal().GetByPlanStatonDcec(CompanyCode, PlanCode, PlineCode, StationCode, Sn, fdjxl, so, staioncode1);
        }
        public static void ShowBomDCECLQ(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string staioncode1, ProductInfoEntity productnew)
        {
            new SNBomTempDal().GetByPlanStatonDcecLQ(CompanyCode, PlanCode, PlineCode, StationCode, Sn, fdjxl, so, staioncode1,productnew);
        }
        public static void ShowBomDCEC_CL(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string staioncode1)
        {
            new SNBomTempDal().GetByPlanStatonDcec_CL(CompanyCode, PlanCode, PlineCode, StationCode, Sn, fdjxl, so, staioncode1);
        }
        public static void ShowBomZJTS(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string staioncode1)
        {
            new SNBomTempDal().GetByPlanStatonZJTS(CompanyCode, PlanCode, PlineCode, StationCode, Sn, fdjxl, so, staioncode1);
        }
        public static List<SNBomTempEntity> GetByPlanStaton(string CompanyCode, string PlanID, string PlineCode, string StationCode, string Sn, string fdjxl, string so)
        {
            return new SNBomTempDal().GetByPlanStaton(CompanyCode, PlanID, PlineCode, StationCode, Sn, fdjxl,so);
        }

        public static void InitSnBomTemp(string CompanyCode, string PlanCode,string StationCode, string Sn)
        {
            SNBomTempDal dal = new SNBomTempDal();
            dal.InitSnBomTemp(CompanyCode, PlanCode,StationCode,Sn);
        }

        public static void HandleBomItemComplete(string CompanyCode, string rmesid, string itemcode, string VendorCode, string ItemBatch, float CompleteQty, string stationname,string barcode,int num)
        {
            SNBomTempDal dal = new SNBomTempDal();

            dal.HandleBomItemComplete(CompanyCode, rmesid, itemcode, VendorCode, ItemBatch, CompleteQty, stationname,barcode,num);
        }

        public static bool BomItemNotConfirmed(string CompanyCode, string PlanCode, string Sn, string PlineCode,string Station)
        {
            return (new SNBomTempDal()).BomItemNotConfirmed( CompanyCode, PlanCode,Sn,PlineCode, Station);
        }
    }

}