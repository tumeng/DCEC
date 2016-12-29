using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：徐莹
/// 功能描述：获取计划相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class PlanFactory
    {
        public static List<PlanEntity> GetByRunFlag(string flag)
        {
            return new PlanDal().GetByRunFlag(flag);
        }

        public static List<PlanEntity> GetByPlanSO(string planSO)
        {
            return new PlanDal().GetByPlanSO(planSO);
        }

        public static List<PlanEntity> GetByPlanCodes(string[] planCodes)
        {
            return new PlanDal().GetByPlanCodes(planCodes);
        }

        public static List<PlanEntity> GetAll()
        {
            return new PlanDal().GetAll();
        }
        public static string GetOrderCodeByPlan(string planCode)
        {
            return new PlanDal().GetOrderCodeByPlan(planCode);
        }

        public static string GetPlanByOrder(string orderCode)
        {
            return new PlanDal().GetPlanByOrder(orderCode);
        }
        /// <summary>
        /// 函数说明：根据计划id获取单个计划,可能返回null，请自行判断
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static PlanEntity GetByKey(string planId)
        {
            return new PlanDal().GetByKey(planId);
        }

        public static PlanEntity GetByOrder(string orderCode)
        {
            return new PlanDal().GetByOrder(orderCode);
        }

        public static List<PlanEntity> GetByOrders(string[] orderCodes)
        {
            return new PlanDal().GetByOrders(orderCodes);
        }


        private static int GetMaxSeq(List<PlanEntity> tempPlan)
        {
            if (tempPlan.Count < 1)
                return 0;
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


        public static List<PlanEntity> GetByUserID(string userID)
        {
            return new PlanDal().GetByUserID(userID);
        }

        public static List<PlanEntity> GetByUserID(string userID, string[] itemFlags, string[] runFlags)
        {
            return new PlanDal().GetByUserID(userID, itemFlags, runFlags);
        }

        /// <summary>
        /// 函数说明：根据可选参数itemFlag值决定flag为itemFlag还是runFlag：TRUE为itemFLag；FALSE为runFlag.默认值为TRUE
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static List<PlanEntity> GetByUserID(string userID, string flag,bool itemFlag=true)
        {
            return new PlanDal().GetByUserID(userID, flag, itemFlag);
        }

        /// <summary>
        /// 函数说明：根据可选参数itemFlag值决定flag为itemFlag还是runFlag：TRUE为itemFLag；FALSE为runFlag.默认值为TRUE
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static List<PlanEntity> GetByUserID(string userID, string[] flag, bool itemFlag = true)
        {
            return new PlanDal().GetByUserID(userID, flag, itemFlag);
        }

        /// <summary>
        /// 用plineid和teamid取得当前的计划
        /// </summary>
        /// <param name="productlineID"></param>
        /// <param name="teamid"></param>
        /// <returns></returns>
        public static List<PlanEntity> GetByPlineTeam(string productlineID, string teamid)
        {
            if (string.IsNullOrWhiteSpace(productlineID) && string.IsNullOrWhiteSpace(teamid)) 
                return new List<PlanEntity>();
            if(string.IsNullOrWhiteSpace(teamid))
                return new PlanDal().GetByPlineID(productlineID);
            if (string.IsNullOrWhiteSpace(productlineID))
                return new PlanDal().FindBySql<PlanEntity>("WHERE TEAM_CODE=@0  AND trunc(BEGIN_DATE) <= trunc(sysdate) "
                + "AND   RUN_FLAG <> 'N' AND RUN_FLAG<>'F' ORDER BY PLAN_SEQUENCE", teamid);
            return new PlanDal().FindBySql<PlanEntity>("WHERE  PLINE_CODE=@0  AND trunc(BEGIN_DATE) <= trunc(sysdate) "
                + "AND RUN_FLAG <> 'N' AND RUN_FLAG<>'F' ORDER BY PLAN_SEQUENCE", productlineID);
        }

        public static List<PlanEntity> GetCurrentRunByPline(string PLineCode)
        {
            return new PlanDal().GetByPline(PLineCode);
        }
        public static List<PlanEntity> GetByPlineCode(string PLineCode)
        {
            return new PlanDal().GetByPlineCode(PLineCode);
        }
        public static List<PlanEntity> GetByPlineCode1(string PLineCode)
        {
            return new PlanDal().GetByPlineCode1(PLineCode);
        }
        public static List<PlanEntity> GetByPline(string PlineCode)
        {
            return new PlanDal().GetByPline(PlineCode);
        }

        public static List<PlanEntity> GetByPlineID(string PlineId)
        {
            return new PlanDal().GetByPlineID(PlineId);
        }
        public static List<PlanEntity> GetByProductLine(string productlineid)
        {
            return new PlanDal().GetByProductLine(productlineid);
        }
        public static PlanEntity GetWorkStationCurrentPlan(string StationRmesID)
        {
            List<PlanEntity> plans = GetWorkStationPlans(StationRmesID);
            if (plans!=null && plans.Count > 0) 
                return plans[0];
            else
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "对应日期没有任何计划";
                return null;
            }
        }

        public static List<PlanEntity> GetWorkStationPlans(string StationRmesID)
        {
            StationEntity station = StationFactory.GetByPrimaryKey(StationRmesID);
            if (station == null)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "传入的StationRmesID不正确，没有找到相应Station";
                return null;
            }
            List<PlanEntity> plans = new PlanDal().FindBySql<PlanEntity>("where RUN_FLAG<>'N' and PLINE_CODE=@0 and sysdate between BEGIN_DATE and END_DATE order by PLAN_SEQUENCE,PLAN_CODE", station.PLINE_CODE);
            return plans;
        }

        public static List<PlanEntity> GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            return new PlanDal().GetByCompanyCodeSN(CompanyCode, SN);
        }
        public static List<PlanEntity> GetByCompanyCodeBatch(string CompanyCode, string PlanBatch)
        {
            return new PlanDal().GetByCompanyCodeBatch(CompanyCode, PlanBatch);
        }
        public static List<PlanEntity> GetByBeginDate(DateTime date)
        {
            return new PlanDal().GetByBeginDate(date);
        }
        public static List<PlanEntity> GetByPeriod(DateTime date1,DateTime date2)
        {
            return new PlanDal().GetByPeriod(date1,date2);
        }
        public static List<PlanEntity> GetRunByWorkshop(string workshopcode)
        {
            List<PlanEntity> plan=new List<PlanEntity>();
            List<PlanEntity> allplan = new PlanDal().GetPlanRun();
            List<Workshop_PlineEntity> pline = Workshop_PlineFactory.GetByWorkshopCode(workshopcode);
            for (int j = 0; j < allplan.Count; j++)
            {
                for (int k = 0; k < pline.Count; k++)
                {
                    if (allplan[j].PLINE_CODE == pline[k].PLINE_CODE)
                        plan.Add(allplan[j]);
                }
            }
            return plan;
        }
        public static List<PlanEntity> GetAllByWorkShop(string workShopID)
        {
            List<ProductLineEntity> pls = ProductLineFactory.GetByWorkShopID(workShopID);
            string[] _p = new string[pls.Count];
            for (int i = 0; i < pls.Count; i++)
            {
                _p[i] = pls[i].RMES_ID;
            }
            return new PlanDal().GetAllByProductLines(_p);
        }
        public static List<PlanEntity> GetByWorkshop1(string workshopcode)//需要打印的计划
        {
            List<PlanEntity> plan = new List<PlanEntity>();
            List<PlanEntity> allplan = new PlanDal().GetCurrentPlan1();
            List<Workshop_PlineEntity> pline = Workshop_PlineFactory.GetByWorkshopCode(workshopcode);
            for (int j = 0; j < allplan.Count; j++)
            {
                for (int k = 0; k < pline.Count; k++)
                {
                    if (allplan[j].PLINE_CODE == pline[k].PLINE_CODE)
                        plan.Add(allplan[j]);
                }
            }
            return plan;
 
        }
        public static List<PlanEntity> GetByLED(string pline_code)
        {
            string[] _plines = pline_code.Split('^');
            List<PlanEntity> plan = new List<PlanEntity>();
           
            List<PlanEntity> allplan = new PlanDal().GetPlanRun();
            for (int i = 0; i < allplan.Count; i++)
            {
                foreach (string pline in _plines)
                {
                    if (allplan[i].PLINE_CODE == pline)
                        plan.Add(allplan[i]);
                }
            }
            return plan;
        }

        public static int Update(PlanEntity plan)
        {
            return new PlanDal().Update(plan);
        }

        public static List<PlanEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            return new PlanDal().GetByCreatePeriod(b,e);
        }

        public static List<PlanEntity> GetByProjectCode(string projectCode)
        {
            return new PlanDal().GetByProjectCode(projectCode);
        }

        public static List<PlanEntity> GetPrintPlan()
        {
            return new PlanDal().GetPrintPlan();
        }

        public static bool ChangePlanQTY(string planCode, int num, string userID)
        {
            DB.GetInstance().BeginTransaction();
            try
            {
                PlanEntity entPlan = PlanFactory.GetByKey(planCode);
                List<PlanBomEntity> allBOMs = PlanBOMFactory.GetByPlanCode(planCode);
                List<PlanProcessEntity> allProcesses = PlanProcessFactory.GetByPlan(planCode);
                //更改所有BOM的数量信息
                foreach (var b in allBOMs)
                {
                    b.ITEM_QTY = (b.ITEM_QTY / entPlan.PLAN_QTY) * num;
                    DB.GetInstance().Update(b);
                }
                //更改所有工序的数量信息和标准工序的处理数量信息
                foreach (var p in allProcesses)
                {
                    PlanProcessMainEntity mainProcess = PlanProcessMainFactory.GetByOrderCodeAndProcessCode(p.ORDER_CODE,p.PROCESS_CODE);
                    mainProcess.HANDLED_QTY = mainProcess.HANDLED_QTY - entPlan.PLAN_QTY + num;
                    if (mainProcess.HANDLED_QTY >= int.Parse(mainProcess.PLAN_QTY))
                    {
                        mainProcess.HANDLED_FLAG = "F";
                    }
                    else if (mainProcess.HANDLED_QTY < int.Parse(mainProcess.PLAN_QTY))
                    {
                        mainProcess.HANDLED_FLAG = "N";
                    }
                    p.PLAN_QTY = num.ToString();
                    DB.GetInstance().Update(mainProcess);
                    DB.GetInstance().Update(p);
                }
                //更改已导入的订单的处理数量信息
                List<PlanProcessMainEntity> orderProcesses = PlanProcessMainFactory.GetByOrderCode(entPlan.ORDER_CODE);
                var minQTY = (from o in orderProcesses select o.HANDLED_QTY).Min();
                PlanTempEntity pt = PlanTempFactory.GetByOrderCode(entPlan.ORDER_CODE);
                pt.HANDLED_QTY = minQTY;
                if (pt.HANDLED_QTY >= pt.PLAN_QTY)
                {
                    pt.HANDLE_FLAG = "F";
                }
                else if (pt.HANDLED_QTY < pt.PLAN_QTY)
                {
                    pt.HANDLE_FLAG = "N";
                }
                DB.GetInstance().Update(pt);
                //更改计划数量信息
                entPlan.PLAN_QTY = num;
                DB.GetInstance().Update(entPlan);
                DB.GetInstance().CompleteTransaction();
                return true;
            }
            catch (Exception e)
            {
                DB.GetInstance().AbortTransaction();
                return false;
            } 
        }
        public static int MS_HANDLE_PLAN_ADJUST(string type, string PlineCode,string PlanCode, string so,string qty, string changeqty,string customerName)
        {
            return new PlanDal().MS_HANDLE_PLAN_ADJUST(type, PlineCode, PlanCode, so, qty, changeqty, customerName);
        }
        public static int PL_CREATE_PLAN(string type, string CompanyCode, string PlineCode, string PlineName, string seq, string PlanCode, string so, string series, string type11, string theUserId, string username, string beginDate, string endDate, string accountdate1, string qty, string onlineqty, string offlineqty, string rountingsite, string gylx1, string customerName, string remark, string ROUNTING_CODE1,string ISBOM1)
        {
            return new PlanDal().PL_CREATE_PLAN(type, CompanyCode, PlineCode, PlineName, seq, PlanCode, so, series, type11, theUserId, username, beginDate, endDate, accountdate1, qty, onlineqty, offlineqty, rountingsite, gylx1, customerName, remark, ROUNTING_CODE1,ISBOM1);
        }
        public static int PL_CREATE_PLANSN(string rmesid)
        {
            return new PlanDal().PL_CREATE_PLANSN(rmesid);
        }
        public static int PL_CALCULATION_MATERIAL(string plinecode, string rountingsite, string manualflag, out string result1) //三方物料计算
        {
            return new PlanDal().PL_CALCULATION_MATERIAL(plinecode, rountingsite, manualflag,out result1);
        }
        public static int PL_CALCULATION_MATERIAL_SINGLEPLAN(string plinecode, string rountingsite, out string result1) //三方物料计算
        {
            return new PlanDal().PL_CALCULATION_MATERIAL_SINGLEPLAN(plinecode, rountingsite,out result1);
        }
        public static int PL_CALCULATION_MATERIAL_SINGLEMAT(string plinecode, string rountingsite, out string result1) //三方物料计算
        {
            return new PlanDal().PL_CALCULATION_MATERIAL_SINGLEMAT(plinecode, rountingsite,out result1);
        }
    }
}

