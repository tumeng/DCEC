using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class ProductDataFactory
    {
       //打印条码自动上线
        public static void OnLine(PlanSnEntity en)
        {
            //PlanSnEntity plansn1 = PlanSnFactory.GetBySn(en.PLAN_SN);
            PlanEntity plan1 = PlanFactory.GetByKey(en.PLAN_CODE);
            //if (en.SN_FLAG == "P") return;
            //else PrintDriver.SendStringToPrinter("S4M", code);
            if (en.SN_FLAG == "N")
            {
                en.SN_FLAG = "P";
                PlanSnFactory.update(en);
                plan1.ONLINE_QTY = plan1.ONLINE_QTY + 1;
                PlanFactory.Update(plan1);
            }
        }
       
        //public static void Station_OnLine(string sn)
        //{
        //    PlanSnEntity plansn1 = PlanSnFactory.GetBySn(sn);
        //    PlanEntity plan1 = PlanFactory.GetByKey(plansn1.PLAN_CODE);
        //    //if (en.SN_FLAG == "P") return;
        //    //else PrintDriver.SendStringToPrinter("S4M", code);
        //    if (plansn1.SN_FLAG == "N")
        //    {
        //        plansn1.SN_FLAG = "P";
        //        PlanSnFactory.update(plansn1);
        //        plan1.ONLINE_QTY = plan1.ONLINE_QTY + 1;
        //        PlanFactory.Update(plan1);
        //    }
        //}
        //扫描SN上线提示
        public static void Station_OnLine(string plan,string sn,string WorkDate,string statcode,string shiftcode,string teamcode,string usercode)
        {
            new ProductDataDal().Station_OnLine(LoginInfo.CompanyInfo.COMPANY_CODE, LoginInfo.ProductLineInfo.PLINE_CODE,
                                            statcode, plan, sn, WorkDate, shiftcode,
                                            teamcode, usercode);

        }
        //VEPS CHECK SO
        public static string VEPS_CHECK_SO(string plinecode, string so, out string veps)
        {
            veps = "";
            veps = new ProductDataDal().VEPS_CHECK_SO(plinecode, so,out veps);
            return veps;
        }
        //漏扫控制
        public static string PL_CHECK_FDJLS(string sn, string stationname,string plinecode, out string str1)
        {
            str1 = "";
            str1 = new ProductDataDal().PL_CHECK_FDJLS(sn,stationname,plinecode, out str1);
            return str1;
        }
        //缸盖报废
        public static string WGG_MODIFY_JHB(string sn, string plinecode, out string str1)
        {
            str1 = "";
            str1 = new ProductDataDal().WGG_MODIFY_JHB(sn, plinecode, out str1);
            return str1;
        }
        //灌录VEPS
        public static void VEPS_CREATE_SCQBCS()
        {
            new ProductDataDal().VEPS_CREATE_SCQBCS();
        }
        //初始化
        public static void Station_Start(string plan, string sn, string quality)
        {
            new ProductDataDal().Station_Start(LoginInfo.CompanyInfo.COMPANY_CODE, 
                LoginInfo.ProductLineInfo.PLINE_CODE, 
                LoginInfo.StationInfo.STATION_CODE, 
                plan, sn, DateTime.Now.ToString("yyyy-MM-dd"), quality, 
                LoginInfo.ShiftInfo.SHIFT_CODE, 
                LoginInfo.TeamInfo.TEAM_CODE, 
                LoginInfo.UserInfo.USER_CODE);

        }
        //ISDE上线生成匹配计划
        public static void ISDE_CREATE_JK_SJJHB(string plinecode)
        {
            new ProductDataDal().ISDE_CREATE_JK_SJJHB(plinecode);
        }
        //生产质量问答提示
        public static void PL_QUERY_QAZJTS(string so, string stationcode, string plinecode, string fdjxl, string plancode, string sn)
        {
            new ProductDataDal().PL_QUERY_QAZJTS(so, stationcode, plinecode, fdjxl, plancode, sn);
        }
        //生成装机BOM
        public static void PL_QUERY_BOMZJTS(string so,string stationcode,string plinecode,string fdjxl,string plancode,string sn)
        {
            new ProductDataDal().PL_QUERY_BOMZJTS(so, stationcode, plinecode, fdjxl, plancode, sn);
        }
        //生成装机BOM
        public static void PL_QUERY_ZJTS(string so, string stationcode, string plinecode, string fdjxl, string plancode, string sn)
        {
            new ProductDataDal().PL_QUERY_ZJTS(so, stationcode, plinecode, fdjxl, plancode, sn);
        }
        public static void PL_UPDATE_BOMZJTS(string so, string stationcode, string plancode, string plinecode, string sn)
        {
            new ProductDataDal().PL_UPDATE_BOMZJTS(so, stationcode, plancode, plinecode, sn);
        }
        public static void PL_UPDATE_BOMLSHTS(string sn, string stationcode)
        {
            new ProductDataDal().PL_UPDATE_BOMLSHTS(sn, stationcode);
        }
        public static void PL_UPDATE_BOMSOTHTS(string so, string plinecode, string stationcode, string sn, string usercode,string stationcode1)
        {
            new ProductDataDal().PL_UPDATE_BOMSOTHTS(so, plinecode, stationcode, sn, usercode,stationcode1);
        }
        public static void PL_UPDATE_BOMSOTHTS_BOM(string so, string plinecode, string stationcode, string sn, string usercode, string stationcode1)
        {
            new ProductDataDal().PL_UPDATE_BOMSOTHTS_BOM(so, plinecode, stationcode, sn, usercode, stationcode1);
        }
        public static void PL_UPDATE_BOMSOTHTS_BOMLQ(string so, string plinecode, string stationcode, string sn, string usercode, string stationcode1,string oldplinecode)
        {
            new ProductDataDal().PL_UPDATE_BOMSOTHTS_BOMLQ(so, plinecode, stationcode, sn, usercode, stationcode1, oldplinecode);
        }
        //查询产品信息从DATA_PRODUCT表
        public static List<ProductDataEntity> GetProductDataByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn)
        {
            return new ProductDataDal().GetProductDataByPlanSn( CompanyCode, PlineCode, PlanCode, Sn);
        }

        public static void Station_OffLine(string plan, string sn, string quality)
        {
            new ProductDataDal().Station_OffLine(LoginInfo.CompanyInfo.COMPANY_CODE, LoginInfo.ProductLineInfo.PLINE_CODE, LoginInfo.StationInfo.STATION_CODE, plan, sn, quality);
        }

        public static void ProcessControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            new ProductDataDal().ProcessControlComplete(CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
        public static void BomControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            new ProductDataDal().BomControlComplete(CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
        public static void QualityControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            new ProductDataDal().QualityControlComplete(CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
    }
}
