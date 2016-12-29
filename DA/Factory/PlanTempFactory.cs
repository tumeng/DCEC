using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class PlanTempFactory
    {
        public static List<PlanTempEntity> GetUnfinishedPlan()
        {
            return new PlanTempDal().GetUnfinishedPlan();
        }

        public static List<PlanTempEntity> GetCurrentUnfinishedPlanByUserID(string userID, string programcode)
        {
            UserEntity user = UserFactory.GetByID(userID);
            List<ProductLineEntity> plines = ProductLineFactory.GetByUserID(userID, programcode);
            if (plines == null || plines.Count < 1)
            {
                return null;
            }
            else
            {
                List<string> plineCodes = new List<string>();
                foreach (var p in plines)
                {
                    plineCodes.Add(p.RMES_ID);
                }
                return new PlanTempDal().GetCurrentUnfinishedPlanByPlines(plineCodes.ToArray());
            }
        }

        public static PlanTempEntity GetByOrderCode(string orderCode)
        {
            return new PlanTempDal().GetByOrderCode(orderCode);
        }

        //public static bool CreatePlan(PlanTempEntity pt, string userID, int planQTY, List<PlanProcessMainEntity> processEntities)
        //{
        //    Database db = DB.GetInstance(); 
        //    try
        //    {
        //        db.BeginTransaction();

        //        var plines = processEntities.Select(m => m.PLINE_CODE).Distinct();
        //        foreach (var p in plines)
        //        {
        //            //若订单号已下达计划  删除上次的计划和相关信息
        //            PlanEntity _plan = PlanFactory.GetByOrder(pt.PLAN_CODE);
        //            if (_plan != null)
        //            {
        //                db.Execute("delete from DATA_PLAN where ORDER_CODE=@0", pt.PLAN_CODE);
        //                db.Execute("delete from DATA_PLAN_BOM where ORDER_CODE=@0", pt.PLAN_CODE);
        //                db.Execute("delete from DATA_PLAN_PROCESS where ORDER_CODE=@0", pt.PLAN_CODE);
        //            }
        //            PlanEntity plan = new PlanEntity
        //            {
        //                COMPANY_CODE = "01",
        //                PROJECT_CODE = pt.PROJECT_CODE,
        //                PLAN_BATCH = pt.PLAN_BATCH,
        //                PLAN_QTY = planQTY,
        //                PLINE_CODE = p,
        //                PLAN_TYPE_CODE = pt.PLAN_TYPE_CODE,
        //                PLAN_SO = pt.PLAN_SO,
        //                PLAN_SEQUENCE = pt.PLAN_SEQUENCE,
        //                CREATE_TIME = DateTime.Now,
        //                BEGIN_DATE = pt.BEGIN_DATE,
        //                END_DATE = pt.END_DATE,
        //                ONLINE_QTY = 0,
        //                OFFLINE_QTY = 0,
        //                SN_FLAG = pt.SN_FLAG,
        //                CONFIRM_FLAG = "N",
        //                BOM_FLAG = "Y",
        //                RUN_FLAG = "N",
        //                ITEM_FLAG = "N",
        //                ORDER_CODE = pt.PLAN_CODE,
        //                CREATE_USER_ID = userID,
        //                //STATION_//DE = stationCode,
        //                INSTORE_CODE = pt.INSTORE_CODE,
        //                WORKSHOP_CODE = pt.WORKSHOP_CODE,
        //                PRODUCT_SERIES = pt.SERIES_CODE,
        //                DETECT_BARCODE = pt.DETECT_BARCODE,
        //                WBS_CODE = pt.WBS_CODE
        //            };

        //            string seq = db.ExecuteScalar<string>("select SEQ_PLAN.Nextval from dual");
        //            plan.PLAN_CODE = "A1" + DateTime.Now.ToString("yyyyMMdd") + seq.PadLeft(5, '0') + "A";

        //            //List<PlanEntity> tempList = PlanFactory.GetByCreatePeriod(DateTime.Today, DateTime.Today);
        //            //int seq; //计划序号
        //            //if (tempList.Count == 0)
        //            //{
        //            //    seq = 0;
        //            //}
        //            //else
        //            //{
        //            //    seq = GetMaxSeq(tempList);
        //            //}
                    


        //            List<PlanProcessMainEntity> tempEntities = (from s in processEntities where s.PLINE_CODE == p select s).ToList<PlanProcessMainEntity>();
        //            List<PlanStandardBOMEntity> tempBOM = new List<PlanStandardBOMEntity>();
        //            foreach (var t in tempEntities)
        //            {


        //                //插入工序和BOM


        //                List<PlanStandardBOMEntity> SBOMEntities = PlanStandardBOMFactory.GetByOrderCodeAndProcessCode(t.ORDER_CODE, t.PROCESS_CODE);
        //                tempBOM.AddRange(SBOMEntities);

        //                //插入BOM
        //                foreach (var b in SBOMEntities)
        //                {
        //                    PlanBomEntity bom = new PlanBomEntity
        //                    {
        //                        COMPANY_CODE = "01",
        //                        PLAN_CODE = plan.PLAN_CODE,
        //                        PLINE_CODE = t.PLINE_CODE,
        //                        LOCATION_CODE = b.LOCATION_CODE,
        //                        ITEM_CODE = b.ITEM_CODE,
        //                        CREATE_TIME = DateTime.Now,
        //                        PROCESS_CODE = b.PROCESS_CODE,
        //                        ITEM_QTY = b.ITEM_QTY,
        //                        FLAG = "N",
        //                        RESOURCE_STORE = b.STORE_ID,
        //                        FACTORY = b.WORKSHOP_CODE,
        //                        LINESIDE_STOCK_CODE = b.LINESIDE_STOCK_CODE,
        //                        ORDER_CODE = b.ORDER_CODE,
        //                        ITEM_NAME = b.ITEM_NAME,
        //                        USER_CODE = b.USER_CODE,
        //                        VENDOR_CODE = b.VENDOR_CODE,
        //                        VIRTUAL_ITEM_CODE = b.VIRTUAL_ITEM_CODE,
        //                    };
        //                    db.Insert("DATA_PLAN_BOM", "RMES_ID", bom);
        //                }
        //                //插入工序
        //                PlanProcessEntity process = new PlanProcessEntity()
        //                {
        //                    FLAG = t.FLAG,
        //                    ORDER_CODE = t.ORDER_CODE,
        //                    PLAN_CODE = plan.PLAN_CODE,
        //                    PLAN_QTY = plan.PLAN_QTY.ToString(),
        //                    PLAN_SO = t.PLAN_SO,
        //                    PLINE_CODE = t.PLINE_CODE,
        //                    PROCESS_CODE = t.PROCESS_CODE,
        //                    PROCESS_CTRL_CODE = t.PROCESS_CTRL_CODE,
        //                    PROCESS_IMPFLAG = t.PROCESS_IMPFLAG,
        //                    PROCESS_NAME = t.PROCESS_NAME,
        //                    PROCESS_SEQ = t.PROCESS_SEQ,
        //                    PROCESS_START_DATE = t.PROCESS_START_DATE,
        //                    WORKSHOP_CODE = t.WORKSHOP_CODE,
        //                    WORKUNIT_CODE = t.WORKUNIT_CODE,
        //                    ROUTING_IMPFLAG = t.ROUTING_IMPFLAG,
        //                    MACHINE_WORKTIME_UNIT = t.MACHINE_WORKTIME_UNIT,
        //                    MAN_WORKTIME_UNIT = t.MAN_WORKTIME_UNIT,
        //                    STANDARD_MACHINE_WORKTIME = t.STANDARD_MACHINE_WORKTIME,
        //                    STANDARD_MAN_WORKTIME = t.STANDARD_MAN_WORKTIME
        //                };
        //                db.Insert(process);
        //                t.HANDLED_QTY += planQTY;
        //                if (t.HANDLED_QTY >= int.Parse(t.PLAN_QTY))
        //                {
        //                    t.HANDLED_FLAG = "F";
        //                }
        //                db.Update(t);
        //            }

        //            if (tempBOM.Count == 0)
        //                plan.BOM_FLAG = "N";
        //            db.Insert("DATA_PLAN", "RMES_ID", plan);
        //        }



        //        //更改已导入的订单信息

        //        var minQTY = (from o in processEntities select o.HANDLED_QTY).Min();
        //        pt.HANDLED_QTY = minQTY;
        //        if (pt.HANDLED_QTY >= pt.PLAN_QTY)
        //        {
        //            pt.HANDLE_FLAG = "F";
        //        }
        //        db.Update(pt);
        //        KeyWorklogFactory.InsertLog(userID, "下达计划");
        //        db.CompleteTransaction();
        //        return true;


        //    }
        //    catch (Exception e)
        //    {
        //        db.AbortTransaction();
        //        return false;
        //    }

        //}



        //public static bool CreatePlan(List<string> orderCodes, string userID)
        //{
        //    Database db = DB.GetInstance(); 
        //    DateTime time = DateTime.Now;
        //    try
        //    {
        //        db.BeginTransaction();
        //        foreach (var o in orderCodes)
        //        {

        //            PlanTempEntity planTemp = PlanTempFactory.GetByOrderCode(o);
        //            List<PlanProcessMainEntity> process = PlanProcessMainFactory.GetUnHandledByOrderCode(o);


        //            var plines = process.Select(m => m.PLINE_CODE).Distinct();
        //            foreach (var p in plines)
        //            {
        //                //若订单号已下达计划  删除上次的计划和相关信息
        //                PlanEntity _plan = PlanFactory.GetByOrder(planTemp.PLAN_CODE);
        //                if (_plan != null)
        //                {
        //                    db.Execute("delete from DATA_PLAN where ORDER_CODE=@0", planTemp.PLAN_CODE);
        //                    db.Execute("delete from DATA_PLAN_BOM where ORDER_CODE=@0", planTemp.PLAN_CODE);
        //                    db.Execute("delete from DATA_PLAN_PROCESS where ORDER_CODE=@0", planTemp.PLAN_CODE);
        //                }
        //                PlanEntity plan = new PlanEntity
        //                {
        //                    COMPANY_CODE = "01",
        //                    PROJECT_CODE = planTemp.PROJECT_CODE,
        //                    PLAN_BATCH = planTemp.PLAN_BATCH,
        //                    PLAN_QTY = planTemp.PLAN_QTY,
        //                    PLINE_CODE = p,
        //                    PLAN_TYPE_CODE = planTemp.PLAN_TYPE_CODE,
        //                    PLAN_SO = planTemp.PLAN_SO,
        //                    PLAN_SEQUENCE = planTemp.PLAN_SEQUENCE,
        //                    CREATE_TIME = time,
        //                    BEGIN_DATE = planTemp.BEGIN_DATE,
        //                    END_DATE = planTemp.END_DATE,
        //                    ONLINE_QTY = 0,
        //                    OFFLINE_QTY = 0,
        //                    SN_FLAG = planTemp.SN_FLAG,
        //                    CONFIRM_FLAG = "N",
        //                    BOM_FLAG = "Y",
        //                    RUN_FLAG = "N",
        //                    ITEM_FLAG = "N",
        //                    ORDER_CODE = planTemp.PLAN_CODE,
        //                    CREATE_USER_ID = userID,
        //                    //STATION_//DE = stationCode,
        //                    INSTORE_CODE = planTemp.INSTORE_CODE,
        //                    WORKSHOP_CODE = planTemp.WORKSHOP_CODE,
        //                    PRODUCT_SERIES = planTemp.SERIES_CODE,
        //                    DETECT_BARCODE = planTemp.DETECT_BARCODE,
        //                    WBS_CODE=planTemp.WBS_CODE
        //                };
        //                //List<PlanEntity> tempList = PlanFactory.GetByCreatePeriod(DateTime.Today, DateTime.Today);
        //                //int seq; //计划序号
        //                //if (tempList.Count == 0)
        //                //{
        //                //    seq = 0;
        //                //}
        //                //else
        //                //{
        //                //    seq = GetMaxSeq(tempList);
        //                //}
        //                string seq = db.ExecuteScalar<string>("select SEQ_PLAN.Nextval from dual");
        //                plan.PLAN_CODE = "A1" + DateTime.Now.ToString("yyyyMMdd") + seq.PadLeft(5, '0') + "A";


        //                List<PlanProcessMainEntity> tempEntities = (from s in process where s.PLINE_CODE == p select s).ToList<PlanProcessMainEntity>();
        //                List<PlanStandardBOMEntity> tempBOM = new List<PlanStandardBOMEntity>();
        //                foreach (var t in tempEntities)
        //                {


        //                    //插入工序和BOM


        //                    List<PlanStandardBOMEntity> SBOMEntities = PlanStandardBOMFactory.GetByOrderCodeAndProcessCode(t.ORDER_CODE, t.PROCESS_CODE);
        //                    tempBOM.AddRange(SBOMEntities);

        //                    //插入BOM
        //                    foreach (var b in SBOMEntities)
        //                    {
        //                        PlanBomEntity bom = new PlanBomEntity
        //                        {
        //                            COMPANY_CODE = "01",
        //                            PLAN_CODE = plan.PLAN_CODE,
        //                            PLINE_CODE = t.PLINE_CODE,
        //                            LOCATION_CODE = b.LOCATION_CODE,
        //                            ITEM_CODE = b.ITEM_CODE,
        //                            CREATE_TIME = time,
        //                            PROCESS_CODE = b.PROCESS_CODE,
        //                            ITEM_QTY = b.ITEM_QTY,
        //                            FLAG = "N",
        //                            RESOURCE_STORE = b.STORE_ID,
        //                            FACTORY = b.WORKSHOP_CODE,
        //                            LINESIDE_STOCK_CODE = b.LINESIDE_STOCK_CODE,
        //                            ORDER_CODE = b.ORDER_CODE,
        //                            ITEM_NAME = b.ITEM_NAME,
        //                            USER_CODE = b.USER_CODE,
        //                            VENDOR_CODE = b.VENDOR_CODE,
        //                            VIRTUAL_ITEM_CODE = b.VIRTUAL_ITEM_CODE,
        //                        };
        //                        db.Insert("DATA_PLAN_BOM", "RMES_ID", bom);
        //                    }
        //                    //插入工序
        //                    PlanProcessEntity processEntity = new PlanProcessEntity()
        //                    {
        //                        FLAG = t.FLAG,
        //                        ORDER_CODE = t.ORDER_CODE,
        //                        PLAN_CODE = plan.PLAN_CODE,
        //                        PLAN_QTY = plan.PLAN_QTY.ToString(),
        //                        PLAN_SO = t.PLAN_SO,
        //                        PLINE_CODE = t.PLINE_CODE,
        //                        PROCESS_CODE = t.PROCESS_CODE,
        //                        PROCESS_CTRL_CODE = t.PROCESS_CTRL_CODE,
        //                        PROCESS_IMPFLAG = t.PROCESS_IMPFLAG,
        //                        PROCESS_NAME = t.PROCESS_NAME,
        //                        PROCESS_SEQ = t.PROCESS_SEQ,
        //                        PROCESS_START_DATE = t.PROCESS_START_DATE,
        //                        WORKSHOP_CODE = t.WORKSHOP_CODE,
        //                        WORKUNIT_CODE = t.WORKUNIT_CODE,
        //                        ROUTING_IMPFLAG = t.ROUTING_IMPFLAG,
        //                        MACHINE_WORKTIME_UNIT = t.MACHINE_WORKTIME_UNIT,
        //                        MAN_WORKTIME_UNIT = t.MAN_WORKTIME_UNIT,
        //                        STANDARD_MACHINE_WORKTIME = t.STANDARD_MACHINE_WORKTIME,
        //                        STANDARD_MAN_WORKTIME = t.STANDARD_MAN_WORKTIME
        //                    };
        //                    db.Insert(processEntity);
        //                    t.HANDLED_QTY += Convert.ToInt32(t.PLAN_QTY);
        //                    if (t.HANDLED_QTY >= int.Parse(t.PLAN_QTY))
        //                    {
        //                        t.HANDLED_FLAG = "F";
        //                    }
        //                    db.Update(t);
        //                }

        //                if (tempBOM.Count == 0)
        //                    plan.BOM_FLAG = "N";
        //                db.Insert("DATA_PLAN", "RMES_ID", plan);
        //            }



        //            //更改已导入的订单信息

        //            //var minQTY = (from pro in process select pro.HANDLED_QTY).Min();
        //            planTemp.HANDLED_QTY = planTemp.PLAN_QTY;
        //            if (planTemp.HANDLED_QTY >= planTemp.PLAN_QTY)
        //            {
        //                planTemp.HANDLE_FLAG = "F";
        //            }
        //            db.Update(planTemp);
        //            KeyWorklogFactory.InsertLog(userID, "下达计划");
        //            //db.CompleteTransaction();
        //        }
        //        db.CompleteTransaction();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        db.AbortTransaction();
        //        return false;
        //    }
        //}

        private static int GetMaxSeq(List<PlanEntity> tempPlan)
        {
            List<int> seq = new List<int>();
            foreach (PlanEntity p in tempPlan)
            {
                seq.Add(int.Parse(p.PLAN_CODE.Substring(10, 5)));
            }
            int i = 0;
            for (int j = 1; j < seq.Count; j++)
            {
                int t;
                if (seq[i] < seq[j])
                {
                    t = seq[i];
                    seq[i] = seq[j];
                    seq[j] = t;
                }
            }
            return seq[0];
        }
    }
}
