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
/// 功能描述：质量检测站点相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class DetectReportFactory
    {
        public static List<DetectReportEntity> GetAll()
        {
            return new DetectReportDal().GetAll();
        }
        public static DetectReportEntity GetByKey(string RmesID)
        {
            return new DetectReportDal().GetByKey(RmesID);
        }
        public static List<DetectReportEntity> GetByPlineAndDate(string pCompanyCode, string pPlineCode, string pFromDate, string pToDate)
        {
            return new DetectReportDal().GetByPlineAndDate(pCompanyCode, pPlineCode, pFromDate, pToDate);
        }
        public static void InsertRecord(DetectReportEntity ent)
        {
            new DetectReportDal().InsertRecord(ent);
        }
        public static void UpdateInsertRecord(DetectReportEntity ent)
        {
            new DetectReportDal().UpdateRecord(ent);
        }
    }
}