using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-08
//
#endregion

#region
namespace Rmes.DA.Dal
{
    internal class SNBomItemSnDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomItemSnEntity> GetAll()
        {
            return db.Fetch<SNBomItemSnEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public SNBomItemSnEntity GetByKey(string ItemSn)
        {
            return db.First<SNBomItemSnEntity>("WHERE ITEM_SN=@0", ItemSn);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomItemSnEntity> GetByRmesID(string BomRmesID)
        {
            return db.Fetch<SNBomItemSnEntity>("WHERE BOM_RMES_ID=@0", BomRmesID);
        }

        public void InsertBomItemSn(string BomRmesID, string Sn)
        {
            SNBomItemSnEntity ent = new SNBomItemSnEntity();
            ent.ITEM_SN = Sn;
            ent.BOM_RMES_ID = BomRmesID;
            db.Insert("DATA_SN_BOM_ITEMSN", "RMES_ID", ent);

        }

        

    }
    internal class SNBomItemBatchDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomItemBatchEntity> GetAll()
        {
            return db.Fetch<SNBomItemBatchEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public SNBomItemBatchEntity GetByKey(string ItemSn)
        {
            return db.First<SNBomItemBatchEntity>("WHERE ITEM_SN=@0", ItemSn);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomItemBatchEntity> GetByRmesID(string BomRmesID)
        {
            return db.Fetch<SNBomItemBatchEntity>("WHERE BOM_RMES_ID=@0", BomRmesID);
        }

        public void InsertBomItemSn(string BomRmesID, string Sn)
        {
            SNBomItemBatchEntity ent = new SNBomItemBatchEntity();
            ent.ITEM_SN = Sn;
            ent.BOM_RMES_ID = BomRmesID;
            db.Insert("DATA_SN_BOM_ITEMBATCH", "RMES_ID", ent);

        }



    }
}
#endregion