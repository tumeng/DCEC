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
    public static class PlanProcessNoteFactory
    {
        public static List<PlanProcessNoteEntity> GetAll()
        {
            return new PlanProcessNoteDal().GetAll();
        }
        public static PlanProcessNoteEntity GetByKey(string strID)
        {
            return new PlanProcessNoteDal().GetByKey(strID);
        }

        public static List<PlanProcessNoteEntity> GetSingleByKey(string RmesID)
        {
            return new PlanProcessNoteDal().GetSingleByKey(RmesID);
        }

        public static List<PlanProcessNoteEntity> GetByProcessProduct(string ComanyCode, string PlineCode, string ProcessCode, string ProductSeries, string PlanCode)
        {
            return new PlanProcessNoteDal().GetByProcessProduct(ComanyCode, PlineCode, ProcessCode, ProductSeries, PlanCode);
        }
    }
}