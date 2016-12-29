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
/// 功能描述：工作站点BOM物料确认SN相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class SNBomItemSnFactory
    {
        public static List<SNBomItemSnEntity> GetAll()
        {
            return new SNBomItemSnDal().GetAll();
        }

        public static List<SNBomItemSnEntity> GetByRmesID(string BomRmesID)
        {
            return new SNBomItemSnDal().GetByRmesID(BomRmesID);
        }

        public static void InsertBomItemSn(string BomRmesID, string Sn)
        {
            new SNBomItemSnDal().InsertBomItemSn(BomRmesID, Sn);

        }

    }
    public static class SNBomItemBatchFactory
    {
        public static List<SNBomItemBatchEntity> GetAll()
        {
            return new SNBomItemBatchDal().GetAll();
        }

        public static List<SNBomItemBatchEntity> GetByRmesID(string BomRmesID)
        {
            return new SNBomItemBatchDal().GetByRmesID(BomRmesID);
        }

        public static void InsertBomItemSn(string BomRmesID, string Sn)
        {
            new SNBomItemBatchDal().InsertBomItemSn(BomRmesID, Sn);

        }





    }

}