using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;
using PetaPoco;


namespace Rmes.DA.Factory
{
    public static class PlanBOMFactory
    {
        public static List<PlanBomEntity> GetByPlanCode(string planCode)
        {
            return new PlanBOMDal().GetByPlanCode(planCode);

        }

        public static List<PlanBomEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            return new PlanBOMDal().GetByCreatePeriod(b,e);
        }

        public static List<PlanBomEntity> GetByOrderCode(string orderCode)
        {
            return new PlanBOMDal().GetByOrderCode(orderCode);
        }

        public static List<PlanBomEntity> GetAll()
        {
            return new PlanBOMDal().GetAll();
        }

        public static List<PlanBomEntity> GetByLineSideStockCode(string stockCode)
        {
            return new PlanBOMDal().GetByLineSideStockCode(stockCode);
        }

        public static int GetWMSQTY(string itemCode)
        {
            return 1;
        }

        public static List<PlanBomEntity> GetByPlanCodes(string[] planCodes)
        {
            return new PlanBOMDal().GetByPlanCodes(planCodes);
        }

        public static int Update(PlanBomEntity p)
        {
            return new PlanBOMDal().Update(p);
        }

        public static List<PlanBomEntity> GetByTeamID(string teamID)
        {
            return new PlanBOMDal().GetByTeamCode(teamID);
        }

        public static List<PlanBomEntity> GetByProjectCode(string projectCode)
        {
            return new PlanBOMDal().GetByProjectCode(projectCode);
        }

        public static PlanBomEntity GetByItemCodeAndStockCode(string itemCode,string storeCode)
        {
            return new PlanBOMDal().GetByItemCodeAndStockCode(itemCode,storeCode);
        }

        //public static bool SendOrder(string[] planCodes)
        //{
        //    Database db=DB.GetInstance();
        //    //db.BeginTransaction();
        //    try
        //    {
        //        db.BeginTransaction();
        //        string batch_temp = "'B'||TO_CHAR(SYSDATE,'yyyymmdd')||to_char(SEQ_BATCHID.NEXTVAL,'fm0000')";
        //        string sql = "select " + batch_temp + " from dual";

        //        string batch_id = db.ExecuteScalar<string>(sql);

        //        DateTime time = DateTime.Now;
        //        int num = 0;
        //        List<ItemLineSideEntity> lineSideItem = ItemLineSideFactory.GetAll();
        //        foreach (var p in planCodes)
        //        {
                    
        //            List<PlanBomEntity> boms = PlanBOMFactory.GetByPlanCode(p);
                    
        //            if (lineSideItem != null)
        //            {
        //                boms = (from b in boms where !(lineSideItem.Select(m => m.ITEM_CODE).Contains(b.ITEM_CODE)) select b).ToList<PlanBomEntity>();
        //            }
        //            foreach (var b in boms)
        //            {
        //                PlanEntity plan = PlanFactory.GetByKey(b.PLAN_CODE);
        //                if (b.VIRTUAL_ITEM_CODE == "X") continue;
        //                IMESStore2LineEntity storeEntity = new IMESStore2LineEntity
        //                {
        //                    AUFNR = plan.ORDER_CODE,
        //                    WERKS = b.FACTORY,
        //                    VORNR = b.PROCESS_CODE,
        //                    SUBMAT = b.ITEM_CODE,
        //                    MATKT = b.ITEM_NAME,
        //                    MENGE = b.ITEM_QTY.ToString(),
        //                    SLGORT = b.RESOURCE_STORE,
        //                    TLGORT = b.LINESIDE_STOCK_CODE,
        //                    PROJN=plan.PROJECT_CODE,
        //                    SERIAL = time.ToString("yyyyMMddhhmmss"),
        //                    WKDT = time,
        //                    BATCH=batch_id,
        //                    CHARG1=batch_id,
        //                    CHARG2=batch_id,
        //                    PRIND = "0"
        //                };
        //                db.Insert(storeEntity);
        //                num++;
        //            }
        //            string sql1="update IMES_DATA_STORE2LINE set KUNNR="+num+" where BATCH='"+batch_id+"'";
        //            db.Execute(sql1);
        //            List<PlanEntity> plans = PlanFactory.GetByPlanCodes(new string[]{p});
        //            foreach (var plan in plans)
        //            {
        //                plan.ITEM_FLAG = "B";
        //                db.Update(plan);
        //            }
        //        }
        //        SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
        //        {
        //            MESSAGE_CODE = "0046",
        //            WORK_DATE = DateTime.Now,
        //            HANDLE_FLAG = "0",
        //        };
        //        db.Insert(msgEntity);
        //        db.CompleteTransaction();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        db.AbortTransaction();
        //        return false;
        //    }
        //}

        public static bool SendOrder(List<PlanStandardBOMEntity> items,string userid)
        {
            Database db = DB.GetInstance();
            try
            {
                db.BeginTransaction();
                string batch_temp = "'B'||TO_CHAR(SYSDATE,'yyyymmdd')||to_char(SEQ_BATCHID.NEXTVAL,'fm0000')";
                string sql = "select " + batch_temp + " from dual";

                string batch_id = db.ExecuteScalar<string>(sql);

                DateTime time = DateTime.Now;
                int num = 0;

                
                foreach (var i in items)
                {
                    if (i.VIRTUAL_ITEM_CODE == "X") continue;
                    EmergencyStore2LineEntity eEntity = new EmergencyStore2LineEntity
                        {
                            AUFNR = i.ORDER_CODE,
                            WERKS = i.WORKSHOP_CODE,
                            VORNR = i.PROCESS_CODE,
                            SUBMAT = i.ITEM_CODE,
                            MENGE = i.ITEM_QTY.ToString(),
                            SLGORT = i.STORE_ID,
                            TLGORT = i.LINESIDE_STOCK_CODE,
                            MATKT = i.ITEM_NAME,
                            SERIAL = DateTime.Now.ToString("yyyyMMddhhmmss"),
                            WKDT = DateTime.Now,
                            PRIND = "0",
                            BATCH=batch_id,
                            CREATE_USER_ID=userid
                        };
                    db.Insert(eEntity);
                    IMESStore2LineEntity storeEntity = new IMESStore2LineEntity
                    {
                        AUFNR = i.ORDER_CODE,
                        WERKS = i.WORKSHOP_CODE,
                        VORNR = i.PROCESS_CODE,
                        SUBMAT = i.ITEM_CODE,
                        MENGE = i.ITEM_QTY.ToString(),
                        SLGORT = i.STORE_ID,
                        TLGORT = i.LINESIDE_STOCK_CODE,
                        MATKT = i.ITEM_NAME,
                        SERIAL = DateTime.Now.ToString("yyyyMMddhhmmss"),
                        WKDT = DateTime.Now,
                        BATCH=batch_id,
                        CHARG1 = batch_id,
                        CHARG2 = batch_id,
                        PRIND = "0"
                    };
                    db.Insert(storeEntity);
                    num++;
                }

                string sql1 = "update IMES_DATA_STORE2LINE set KUNNR=" + num + " where BATCH='" + batch_id + "'";
                db.Execute(sql1);

                SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
                {
                    MESSAGE_CODE = "0046",
                    WORK_DATE = DateTime.Now,
                    HANDLE_FLAG = "0",
                };
                db.Insert(msgEntity);
                db.CompleteTransaction();
                return true;
            }
            catch (Exception e)
            {
                db.AbortTransaction();
                return false;
            }
        }
    }
}
