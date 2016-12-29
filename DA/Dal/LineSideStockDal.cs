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
    internal class LineSideStockDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<LineSideStockEntity></returns>
        public List<LineSideStockEntity> GetAll()
        {
            return db.Fetch<LineSideStockEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>LineSideStockEntity</returns>
        public LineSideStockEntity GetByKey(string RmesID)
        {
            return db.First<LineSideStockEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<LineSideStockEntity></returns>
        public List<LineSideStockEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<LineSideStockEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public LineSideStockEntity GetByItem(string CompanyCode, string ItemCode)
        {
            try
            {
                return db.First<LineSideStockEntity>("WHERE COMPANY_CODE=@0 AND ITEM_CODE=@1", CompanyCode, ItemCode);
            }
            catch
            {
                return null;
            }
        }

        public LineSideStockEntity GetByItem(string locationCode, string plineCode, string ItemCode)
        {
            try
            {
                return db.First<LineSideStockEntity>("where location_code=@0 and pline_code=@1 and item_code=@2", locationCode, plineCode, ItemCode);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 函数说明：已过期，表中无SN字段
        /// </summary>
        /// 
        /// <returns>LineSideStockEntity</returns>
        public LineSideStockEntity GetByItemSN(string CompanyCode, string ItemCode, string Sn)
        {
            try
            {
                return db.First<LineSideStockEntity>("WHERE COMPANY_CODE=@0 AND ITEM_CODE=@1 AND ITEM_SN=@2", CompanyCode, ItemCode, Sn);
            }
            catch
            {
                return null;
            }
        }

        public LineSideStockEntity GetByItemVendorBatch(string CompanyCode, string ItemCode, string VendorCode, string BatchCode)
        {
            string SQL = "WHERE COMPANY_CODE=@0 AND ITEM_CODE=@1 AND VENDOR_CODE=@2 AND BATCH_CODE=@3";
            try
            {
                return db.First<LineSideStockEntity>(SQL, CompanyCode, ItemCode, VendorCode, BatchCode);
            }
            catch
            {
                return null;
            }
        }

        public void Insert(LineSideStockEntity item)
        {
            db.Insert("DATA_LINESIDE_STOCK", "RMES_ID", item);
        }

        public int Update(LineSideStockEntity item)
        {
            return db.Update(item);
        }

        public List<LineSideStockEntity> GetByProductLines(string[] plines)
        {
            return db.Fetch<LineSideStockEntity>("where PLINE_CODE in(@t)", new { t=plines});
        }

        public List<LineSideStockEntity> GetByStoreCode(string storeCode)
        {
            return db.Fetch<LineSideStockEntity>("where STORE_CODE=@0", storeCode);
        }

        public LineSideStockEntity GetStoreItem(string storeCode, string itemCode)
        {
            try
            {
                return db.First<LineSideStockEntity>("where STORE_CODE=@0 and ITEM_CODE=@1", storeCode, itemCode);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
#endregion