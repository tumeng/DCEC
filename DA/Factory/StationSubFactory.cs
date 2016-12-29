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
/// 功能描述：获取分装站点相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class StationSubFactory
    {
        public static List<StationSubEntity> GetAll()
        {
            return new StationSubDal().GetAll();
        }
        public static StationSubEntity GetByKey(string strID)
        {
            return new StationSubDal().GetByKey(strID);
        }

        public static List<StationSubEntity> GetSingleByKey(string RmesID)
        {
            return new StationSubDal().GetSingleByKey(RmesID);
        }

        public static void Insert(StationSubEntity entity)
        {
             new StationSubDal().Insert(entity);
        }

        public static void Update(StationSubEntity entity)
        {
            new StationSubDal().Update(entity);
        }

        public static void Delete(StationSubEntity entity)
        {
            new StationSubDal().Delete(entity);
        }
    }
}