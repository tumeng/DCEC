using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class PlanDal : BaseDalClass
    {
        public List<PlanEntity> GetPrintPlan()
        {
            return db.Fetch<PlanEntity>("where order_code in (select order_code from data_plan_sn where SN_FLAG='N')");
        }

        public List<PlanEntity> GetByPlanSO(string so)
        {
            return db.Fetch<PlanEntity>("where PLAN_SO=@0", so);
        }

        /// <summary>
        /// 根据执行状态获取计划
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<PlanEntity> GetByRunFlag(string flag)
        {
            return db.Fetch<PlanEntity>("where rnu_flag=@0",flag);
        }
        /// <summary>
        /// 函数说明：获取所有计划
        /// </summary>
        /// <returns></returns>
        public List<PlanEntity> GetAll()
        {
            return db.Fetch<PlanEntity>("");
        }
        /// <summary>
        /// 函数说明：根据计划id获取单个计划,可能返回null，请自行判断
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public PlanEntity GetByKey(string planId)
        {
            try
            {
                return db.First<PlanEntity>("WHERE  PLAN_CODE=@0", planId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public PlanEntity GetByOrder(string orderCode)
        {
            try
            {
                return db.First<PlanEntity>("WHERE  ORDER_CODE=@0", orderCode);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<PlanEntity> GetByOrders(string[] orderCodes)
        {
            try
            {
                return db.Fetch<PlanEntity>("WHERE  ORDER_CODE in (@i)", new { i=orderCodes});
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetOrderCodeByPlan(string pPlanCode)
        {
            string order_code = "";
            PlanEntity ent = new PlanEntity();
            try
            {
                ent=db.First<PlanEntity>("WHERE  PLAN_CODE=@0", pPlanCode);
                if (ent != null)
                {
                    order_code = ent.ORDER_CODE;
                }
            }
            catch (Exception e)
            {
            }
            return order_code;

        }

        public string GetPlanByOrder(string pOrderCode)
        {
            string plan_code="";
            PlanEntity ent = new PlanEntity();
            try
            {
                ent=db.First<PlanEntity>("WHERE  order_code=@0", pOrderCode);
                if (ent != null)
                {
                    plan_code = ent.PLAN_CODE;
                }
            }
            catch (Exception e)
            {
            }
            return plan_code;

        }
        /// <summary>
        /// 函数说明：根据站点ID获取当天可执行计划（包含暂停的计划）
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public List<PlanEntity> GetCurrentRunByPline(string plineCode)
        {
            return db.Fetch<PlanEntity>("WHERE  (PLINE_CODE=@0 OR PLINE_CODE IS NULL) AND RUN_FLAG <> 'N' AND trunc(BEGIN_DATE) <= trunc(sysdate) "
                + "AND END_DATE >=trunc(sysdate) ORDER BY PLAN_SEQUENCE", plineCode);
        }

        public List<PlanEntity> GetByPline(string plineCode)
        {
            return db.Fetch<PlanEntity>("WHERE  PLINE_CODE=@0 order by CREATE_TIME desc", plineCode);
        }
        public List<PlanEntity> GetByPlineCode(string plineCode)
        {
            //return db.Fetch<PlanEntity>("WHERE run_flag='Y' and plan_type!='C' and plan_type!='D' and confirm_flag='Y' and pline_code=@0 AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') order by begin_date,plan_seq ", plineCode);
            return db.Fetch<PlanEntity>("WHERE run_flag='Y' and plan_type!='C' and plan_type!='D' and confirm_flag='Y' and pline_code=@0 AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') order by begin_date,plan_seq ", plineCode);
        
        }
        public List<PlanEntity> GetByPlineCode1(string plineCode)
        {
            //return db.Fetch<PlanEntity>("WHERE run_flag='Y' and plan_type!='C' and plan_type!='D' and confirm_flag='Y' and pline_code=@0 AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') order by begin_date,plan_seq ", plineCode);
            return db.Fetch<PlanEntity>("WHERE run_flag='Y' and plan_type!='C' and plan_type!='D' and confirm_flag='Y' and pline_code=@0  AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') order by begin_date desc,plan_seq ", plineCode);

        }
        public List<PlanEntity> GetByPlineID(string plineid)
        {
            return db.Fetch<PlanEntity>("WHERE (PLINE_CODE=@0 OR PLINE_CODE IS NULL) AND RUN_FLAG <> 'N' AND trunc(BEGIN_DATE) <= trunc(sysdate) "
                + "AND END_DATE >=trunc(sysdate) ORDER BY PLAN_SEQUENCE", plineid);
        }
        public List<PlanEntity> GetByProductLine(string ProductLineID)
        {
            DateTime curDate = DB.GetServerTime();
            //string sqlQuery = "WHERE PLINE_CODE=@0 AND CONFIRM_FLAG=@1 AND RUN_FLAG=@2 " +
            //    "AND BEGIN_DATE <=@3 AND END_DATE>=@4 ORDER BY PLAN_SEQUENCE";
            //return db.Fetch<PlanEntity>(sqlQuery, ProductLineID, "Y", "Y", curDate, curDate);
            string sqlQuery = "WHERE PLINE_CODE=@0";
            return db.Fetch<PlanEntity>(sqlQuery,ProductLineID);
        }

        /// <summary>
        /// 函数说明：根据站点ID获取正在执行的计划（不包含暂停的计划）
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public string GetCurrentPlan(string stationCode)
        {
            DateTime curDate = DB.GetServerTime();
            string sqlQuery = "WHERE STATION_ID=@0 AND CLOSE_FLAG=@1 AND RUN_FLAG=@2 AND BEGIN_DATE <=@3 ORDER BY PLAN_SEQUENCE";
            List<PlanEntity> ListPlan = db.Fetch<PlanEntity>(sqlQuery, stationCode, "N", "Y", curDate);

            if (ListPlan.Count > 0)
                return ListPlan[0].PLAN_CODE;
            else
                return "";
        }

        public List<PlanEntity> GetCurrentPlan1()//需要打印的计划
        {

            return db.Fetch<PlanEntity>("WHERE  RUN_FLAG = 'Y' AND trunc(BEGIN_DATE) <= trunc(sysdate) "
               + " ORDER BY PLAN_SEQUENCE");
        }
        public List<PlanEntity> GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            string SQL = "WHERE COMPANY_CODE=@0 AND PLAN_CODE  IN(select plan_code from data_plan_sn where company_code=@0 and SN=@1)";
            return db.Fetch<PlanEntity>(SQL, CompanyCode, SN);
        }
        public List<PlanEntity> GetByCompanyCodeBatch(string CompanyCode, string PlanBatch)
        {

            return db.Fetch<PlanEntity>("WHERE COMPANY_CODE=@0 AND PLAN_BATCH=@1", CompanyCode, PlanBatch);
        }
        
        public List<PlanEntity> GetByBeginDate(DateTime date)
        {
            string _date = date.ToShortDateString();
            string _end = _date + " 23:59:59";
            return db.Fetch<PlanEntity>("where begin_date between to_date(@0,'yyyy-mm-dd') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')",_date,_end);
        }

        public List<PlanEntity> GetByPeriod(DateTime date1,DateTime date2)
        {
            string _date1 = date1.ToShortDateString();
            string _date2 = date2.ToShortDateString();
            return db.Fetch<PlanEntity>("where begin_date between to_date(@0,'yyyy-mm-dd') and to_date(@1,'yyyy-mm-dd')",_date1,_date2);
        
        }

        public List<PlanEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            string _b = b.ToShortDateString() + " 00:00:00";
            string _e = e.ToShortDateString() + " 23:59:59";
            return db.Fetch<PlanEntity>("where create_time between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')", _b, _e);
        }

        public List<PlanEntity> GetPlanRun()
        {
            string now = DateTime.Now.ToShortDateString();
            return db.Fetch<PlanEntity>("where (RUN_FLAG!='N' or ITEM_FLAG!='N')and (to_date(@0,'yyyy-mm-dd') between BEGIN_DATE and END_DATE) order by TEAM_CODE ASC, BEGIN_DATE,END_DATE", now);
        }

        public int Update(PlanEntity plan)
        {
            return db.Update(plan);
        }

        public List<PlanEntity> GetByUserID(string userID)
        {
            return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0", userID);
        }

        public List<PlanEntity> GetByUserID(string userID,string[] itemFlags,string[] runFlags)
        {
            return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0 and ITEM_FLAG in (@i) and RUN_FLAG in (@r) order by  PLAN_CODE DESC", userID, new { i = itemFlags }, new { r = runFlags });
        }

        public List<PlanEntity> GetByUserID(string userID, string flag,bool itemFlag=true)
        {
            if (itemFlag)
            {
                return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0 and ITEM_FLAG=@1 order by  PLAN_CODE DESC", userID, flag);
            }
            else
            {
                return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0 and RUN_FLAG=@1 order by  PLAN_CODE DESC", userID, flag);
            }
        }

        public List<PlanEntity> GetAllByProductLines(string[] _p)
        {
            return db.Fetch<PlanEntity>("where PLINE_CODE in (@t)", new { t=_p});
        }

        public List<PlanEntity> GetByUserID(string userID, string[] flags,bool itemFlag=true)
        {
            if (itemFlag)
            {
                return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0 and ITEM_FLAG in (@r) order by  PLAN_CODE DESC", userID, new { r = flags });
            }
            else
            {
                return db.Fetch<PlanEntity>("where CREATE_USER_ID=@0 and RUN_FLAG in (@r) order by  PLAN_CODE DESC", userID, new { r = flags });
            }
        }

        public List<PlanEntity> GetByProjectCode(string projectCode)
        {
            return db.Fetch<PlanEntity>("where PROJECT_CODE=@0 order by PLAN_CODE desc",projectCode);
        }

        public List<PlanEntity> GetByPlanCodes(string[] planCodes)
        {
            try
            {
                return db.Fetch<PlanEntity>("where PLAN_CODE in (@t)", new { t = planCodes });
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int MS_HANDLE_PLAN_ADJUST(string type, string PlineCode, string PlanCode, string so, string qty, string changeqty, string customerName)
        {
            return db.Execute("call MS_HANDLE_PLAN_ADJUST(@0,@1,@2,@3,@4,@5,@6)", type, PlineCode, PlanCode, so, qty, changeqty, customerName);
        }
        public int PL_CREATE_PLAN(string type, string CompanyCode, string PlineCode, string PlineName, string seq, string PlanCode, string so, string series, string type11, string theUserId, string username, string beginDate, string endDate, string accountdate1, string qty, string onlineqty, string offlineqty, string rountingsite, string gylx1, string customerName, string remark, string ROUNTING_CODE1, string ISBOM1)
        {
            return db.Execute("call PL_CREATE_PLAN(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22)", type, CompanyCode, PlineCode, PlineName, seq, PlanCode, so, series, type11, theUserId, username, beginDate, endDate, accountdate1, qty, onlineqty, offlineqty, rountingsite, gylx1, customerName, remark, ROUNTING_CODE1, ISBOM1);
        }
        public int PL_CREATE_PLANSN(string rmesid)
        {
            return db.Execute("call PL_CREATE_PLANSN(@0)", rmesid);
        }
        public int PL_CALCULATION_MATERIAL(string plinecode, string rountingsite, string manualflag,out string result1) //三方物料计算
        {
            result1 = "";
            return db.Execute("call MS_SF_JIT_R(@0,@1,@2,@3)", plinecode, rountingsite, manualflag, result1);
        }
        public int PL_CALCULATION_MATERIAL_SINGLEPLAN(string plinecode, string rountingsite, out string result1) //三方物料计算
        {
            result1 = "";
            return db.Execute("call MS_SF_JIT_SINGLE_R(@0,@1,@2)", plinecode, rountingsite,  result1);
        }
        public int PL_CALCULATION_MATERIAL_SINGLEMAT(string plinecode, string rountingsite, out string result1) //三方物料计算
        {
            result1 = "";
            return db.Execute("call MS_SF_JIT_SINGLE_MAT_R(@0,@1,@2)", plinecode, rountingsite,  result1);
        }
    }
}
