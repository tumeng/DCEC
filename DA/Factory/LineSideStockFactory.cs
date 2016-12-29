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
/// 功能描述：线边库存操作
/// </summary>
/// 

namespace Rmes.DA.Factory
{
    public static class LineSideStockFactory
    {
        public static LineSideStockEntity GetByItem(string CompanyCode, string ItemCode)
        {
            return new Rmes.DA.Dal.LineSideStockDal().GetByItem(CompanyCode, ItemCode);
        }

        /// <summary>
        /// 函数说明：已过期，表中无SN字段
        /// </summary>
        /// 
        /// <returns>LineSideStockEntity</returns>
        public static LineSideStockEntity GetByItemSN(string CompanyCode, string ItemCode, string Sn)
        {
            return new LineSideStockDal().GetByItemSN(CompanyCode, ItemCode, Sn);
        }

        public static List<LineSideStockEntity> GetAll()
        {
            return new LineSideStockDal().GetAll();
        }

        public static LineSideStockEntity GetByItemVendorBatch(string CompanyCode, string ItemCode, string VendorCode, string BatchCode)
        {
            return new LineSideStockDal().GetByItemVendorBatch(CompanyCode, ItemCode, VendorCode, BatchCode);
        }

        /// <summary>
        /// 函数说明：入库操作，返回影响的行数
        /// </summary>
        /// 
        /// <returns>int</returns>
        public static bool Storage(string itemCode, string vendorCode, string SN, string locationCode, string plineCode, float itemQTY)
        {

            LineSideStockEntity item = new LineSideStockEntity()
            {
                COMPANY_CODE=LoginInfo.UserInfo.COMPANY_CODE,
                LOCATION_CODE=locationCode,
                ITEM_CODE=itemCode,
                VENDOR_CODE=vendorCode,
                PLINE_CODE=plineCode,
                ITEM_QTY=itemQTY
            };
            LineSideStockEntity temp = new LineSideStockEntity();
            LineSideStockDal lsDal = new LineSideStockDal();
            temp = lsDal.GetByItem(locationCode, plineCode, itemCode);
            if (temp == null)
            {
                lsDal.Insert(item);
                return true;
            }
            else
            {
                temp.ITEM_QTY += item.ITEM_QTY;
                lsDal.Update(temp);
                return true;
            }
            
        }

        /// <summary>
        /// 函数说明：入库操作，返回影响的行数
        /// </summary>
        /// 
        /// <returns>int</returns>
        public static int Storage(LineSideStockEntity item)
        {
            int result;
            LineSideStockEntity temp = new LineSideStockEntity();
            LineSideStockDal lsDal = new LineSideStockDal();
            temp = lsDal.GetByItem(item.LOCATION_CODE,item.PLINE_CODE,item.ITEM_CODE);
            if (temp == null)
            {
                lsDal.Insert(item);
                result = 1;
            }
            else
            {
                temp.ITEM_QTY += item.ITEM_QTY;
                result = lsDal.Update(temp);
            }
            //插入收料流水
            IssueReceivedEntity ent=new IssueReceivedEntity()
            {
                COMPANY_CODE=item.COMPANY_CODE,
                WORKSHOP_CODE=item.WORKSHOP_CODE,
                PLINE_CODE=item.PLINE_CODE,
                LOCATION_CODE=item.LOCATION_CODE,
                LINESIDE_STOCK_CODE=item.STORE_CODE,
                ITEM_CODE=item.ITEM_CODE,
                ITEM_NAME=item.ITEM_NAME,
                ITEM_QTY = item.ITEM_QTY,
                WORK_TIME=DateTime.Now,
                USER_ID=LoginInfo.UserInfo.USER_ID
            };
            new IssueReceivedDal().Insert(ent);

            return result;
        }

        /// <summary>
        /// 函数说明：批量入库操作，返回影响的行数
        /// </summary>
        /// 
        /// <returns>int</returns>
        public static int Storage(List<LineSideStockEntity> itemList)
        {
            int result = 0;
            LineSideStockDal lsDal = new LineSideStockDal();
            lsDal.DataBase.BeginTransaction();
            foreach (LineSideStockEntity lsEntity in itemList)
            {
                LineSideStockEntity temp = new LineSideStockEntity();
                temp = lsDal.GetByItem(lsEntity.LOCATION_CODE, lsEntity.PLINE_CODE, lsEntity.ITEM_CODE);
                if (temp == null)
                {
                    lsDal.Insert(lsEntity);
                    result += 1;
                }
                else
                {
                    temp.ITEM_QTY += lsEntity.ITEM_QTY;
                    result += lsDal.Update(temp);
                }
            }
            lsDal.DataBase.CompleteTransaction();
            return result;
        }

        

        /// <summary>
        ///物料  供应商  批次/SN  数量  工位  生产线
        /// </summary>
        /// <returns>int</returns>

        public static bool OutOfStorage(string itemCode, string vendorCode, string SN, string locationCode, string plineCode,float itemQTY)
        {
            try
            {
                LineSideStockEntity temp = new LineSideStockEntity();
                LineSideStockDal lsDal = new LineSideStockDal();
                temp = lsDal.GetByItem(locationCode, plineCode, itemCode);
                if (temp == null)
                {
                    Rmes.Public.ErrorHandle.EH.LASTMSG = "没有物料" + itemCode + "的相关记录，无法进行出库操作！\n";
                    lsDal.DataBase.AbortTransaction();
                    return false;
                }
                else
                {
                    temp.ITEM_QTY = temp.ITEM_QTY - itemQTY;
                    if (temp.ITEM_QTY < 0)
                    {
                        Rmes.Public.ErrorHandle.EH.LASTMSG = itemCode + "库存不足，无法进行出库操作！\n";
                        lsDal.DataBase.AbortTransaction();
                        return false;
                    }

                    lsDal.Update(temp);
                    return true;
                }
            }
            catch (Exception e)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = e.Message;
                return false;
            }
            
            
        }

        public static List<LineSideStockEntity> GetByWorkShopID(string workShopID)
        {
            List<ProductLineEntity> pls = ProductLineFactory.GetByWorkShopID(workShopID);
            string[] _p=new string[pls.Count];
            for (int i = 0; i < pls.Count; i++)
            {
                _p[i] = pls[i].RMES_ID;
            }
            return new LineSideStockDal().GetByProductLines(_p);
        }

        public static List<LineSideStockEntity> GetByStoreCode(string storeCode)
        {
            return new LineSideStockDal().GetByStoreCode(storeCode);
        }

        public static string TransStore(string planCode,string resourceStore, string itemCode, string destinationStore, int transQTY)
        {
            DB.GetInstance().BeginTransaction();
            try
            {
                List<PlanBomEntity> planBOMs = PlanBOMFactory.GetByPlanCode(planCode);
                planBOMs = (from p in planBOMs where p.ITEM_CODE == itemCode && p.LINESIDE_STOCK_CODE == resourceStore select p).ToList<PlanBomEntity>();
                List<LineSideStockEntity> all = DB.GetInstance().Fetch<LineSideStockEntity>("");
                PlanBomEntity resouece = planBOMs[0];
                LineSideStockEntity rsourceStore = LineSideStockFactory.GetStoreItem(resourceStore,itemCode);
                LineSideStockEntity destination = LineSideStockFactory.GetStoreItem(destinationStore, itemCode);
                rsourceStore.ITEM_QTY = rsourceStore.ITEM_QTY - transQTY;
                DB.GetInstance().Update(rsourceStore);
                if (destination==null)
                {
                    LineSideStockEntity newEntity = new LineSideStockEntity()//
                    {
                        ITEM_CODE = itemCode,
                        ITEM_QTY = transQTY,
                        STORE_CODE= destinationStore,
                        COMPANY_CODE = resouece.COMPANY_CODE,
                        VENDOR_CODE = resouece.VENDOR_CODE,
                        PLINE_CODE=resouece.PLINE_CODE,
                    };
                    DB.GetInstance().Insert(newEntity);
                }
                else
                {
                    destination.ITEM_QTY = destination.ITEM_QTY + transQTY;
                    DB.GetInstance().Update(destination);
                }

                IMESLine2LineEntity line2lineEntity = new IMESLine2LineEntity
                {
                    AUFNR=resouece.ORDER_CODE,
                    WERKS=resouece.FACTORY,
                    VORNR=resouece.PROCESS_CODE,
                    SUBMAT=resouece.ITEM_CODE,
                    MENGE=transQTY.ToString(),
                    TLGORT=destinationStore,
                    SLGORT=resourceStore,
                    PRIND="0",
                };
                DB.GetInstance().Insert(line2lineEntity);


                SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
                {
                    MESSAGE_CODE = "0047",
                    WORK_DATE = DateTime.Now,
                    HANDLE_FLAG = "0",
                };
                DB.GetInstance().Insert(msgEntity);
                DB.GetInstance().CompleteTransaction();
                return "true";
            }
            catch (Exception e)
            {
                DB.GetInstance().AbortTransaction();
                return e.Message;
            }
        }


        public static string TransToWMSStore(string planCode, string resourceStore, string itemCode, string destinationStore, int transQTY)
        {
            DB.GetInstance().BeginTransaction();
            try
            {
                List<PlanBomEntity> planBOMs = PlanBOMFactory.GetByOrderCode(planCode);
                planBOMs = (from p in planBOMs where p.ITEM_CODE == itemCode && p.LINESIDE_STOCK_CODE == resourceStore select p).ToList<PlanBomEntity>();
                List<LineSideStockEntity> all = DB.GetInstance().Fetch<LineSideStockEntity>("");
                PlanBomEntity resouece = planBOMs[0];
                LineSideStockEntity rsourceStore = LineSideStockFactory.GetStoreItem(resourceStore, itemCode);
                LineSideStockEntity destination = LineSideStockFactory.GetStoreItem(destinationStore, itemCode);
                rsourceStore.ITEM_QTY = rsourceStore.ITEM_QTY - transQTY;
                DB.GetInstance().Update(rsourceStore);

                IMESLine2LineEntity line2lineEntity = new IMESLine2LineEntity
                {
                    AUFNR = resouece.ORDER_CODE,
                    WERKS = resouece.FACTORY,
                    VORNR = resouece.PROCESS_CODE,
                    SUBMAT = resouece.ITEM_CODE,
                    MENGE = transQTY.ToString(),
                    TLGORT = destinationStore,
                    SLGORT = resourceStore,
                    PRIND = "0",
                };
                DB.GetInstance().Insert(line2lineEntity);


                SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
                {
                    MESSAGE_CODE = "0047",
                    WORK_DATE = DateTime.Now,
                    HANDLE_FLAG = "0",
                };
                DB.GetInstance().Insert(msgEntity);
                DB.GetInstance().CompleteTransaction();
                return "true";
            }
            catch (Exception e)
            {
                DB.GetInstance().AbortTransaction();
                return e.Message;
            }
        }

        public static LineSideStockEntity GetStoreItem(string storeCode, string itemCode)
        {
            return new LineSideStockDal().GetStoreItem(storeCode,itemCode);
        }
    }
}
