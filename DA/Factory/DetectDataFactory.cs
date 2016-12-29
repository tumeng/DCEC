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
    public static class DetectDataFactory
    {
        public static List<DetectDataEntity> GetAll()
        {
            return new DetectDataDal().GetAll();
        }
        public static List<DetectDataEntity> GetByUser(string userid, string proid)
        {
            return new DetectDataDal().GetByUser(userid,proid);
        }
        public static DetectDataEntity GetByKey(string strID)
        {
            return new DetectDataDal().GetByKey(strID);
        }

        public static List<DetectDataEntity> GetSingleByKey(string RmesID)
        {
            return new DetectDataDal().GetSingleByKey(RmesID);
        }

        public static List<DetectDataEntity> GetByDetectCode(string CompanyCode, string DecectCode)
        {
            return new DetectDataDal().GetByDetectCode(CompanyCode, DecectCode);
        }

        public static List<DetectDataEntity> GetByWorkunit(string CompanyCode, string WorkunitCode)
        {
            return new DetectDataDal().GetByWorkunit(CompanyCode, WorkunitCode);
        }

    }
}