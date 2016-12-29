using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;

namespace Rmes.DA.Dal
{
    internal class ProductDataDal:BaseDalClass
    {
        public void Station_OnLine(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN,string workdate,
                                            string ShiftCode, string TeamCode, string UserID)
        {
            db.Execute("call PL_HANDLE_STATION_ONLINE(@0,@1,@2,@3,@4,@5,@6,@7,@8)", CompanyCode, PlineCode, StationCode, PlanCode,
                                                                        SN,workdate, ShiftCode, TeamCode, UserID);
        }
        public string VEPS_CHECK_SO(string plinecode, string so, out string veps)
        {
            veps = "";
            db.Execute("call VEPS_CHECK_SO(@0,@1,@2)", plinecode, so, veps);
            return veps;
        }
        public string PL_CHECK_FDJLS(string sn, string stationname,string plinecode, out string str1)
        {
            str1 = "";
            db.Execute("call PL_CHECK_FDJLS(@0,@1,@2,@3)", sn, stationname,plinecode, str1);
            return str1;
        }
        public string WGG_MODIFY_JHB(string sn, string plinecode, out string str1)
        {
            str1 = "";
            db.Execute("call WGG_MODIFY_JHB(@0,@1,@2)", sn, plinecode, str1);
            return str1;
        }
        public void VEPS_CREATE_SCQBCS()
        {
            db.Execute("call VEPS_CREATE_SCQBCS()");
        }
        public void PL_QUERY_QAZJTS(string so, string stationncode, string plinecode, string fdjxl, string plancode, string sn)
        {
            db.Execute("call PL_QUERY_QAZJTS(@0,@1,@2,@3,@4,@5)", so, stationncode, plinecode, fdjxl, plancode, sn);
        }
        //生成装机BOM
        public void PL_QUERY_BOMZJTS(string so, string stationncode,string plinecode, string fdjxl, string plancode,string sn)
        {
            db.Execute("call PL_QUERY_BOMZJTS(@0,@1,@2,@3,@4,@5)", so, stationncode, plinecode, fdjxl, plancode, sn);
        }
        //生成装机BOM
        public void PL_QUERY_ZJTS(string so, string stationncode, string plinecode, string fdjxl, string plancode, string sn)
        {
            db.Execute("call PL_QUERY_ZJTS(@0,@1,@2,@3,@4,@5)", so, stationncode, plinecode, fdjxl, plancode, sn);
        }
        public void PL_UPDATE_BOMZJTS(string so, string stationncode, string plancode, string plinecode, string sn)
        {
            db.Execute("call PL_UPDATE_BOMZJTS(@0,@1,@2,@3,@4)", so, stationncode, plancode, plinecode, sn);
        }
        public void PL_UPDATE_BOMLSHTS(string sn, string stationcode)
        {
            db.Execute("call PL_UPDATE_BOMLSHTS(@0,@1)",sn,stationcode);
        }
        public void PL_UPDATE_BOMSOTHTS(string so, string plinecode, string stationcode, string sn,string usercode,string stationcode1)
        {
            db.Execute("call PL_UPDATE_BOMSOTHTS(@0,@1,@2,@3,@4,@5)", so, plinecode, stationcode, sn, usercode,stationcode1);
        }
        public void PL_UPDATE_BOMSOTHTS_BOM(string so, string plinecode, string stationcode, string sn, string usercode, string stationcode1)
        {
            db.Execute("call PL_UPDATE_BOMSOTHTS_BOM(@0,@1,@2,@3,@4,@5)", so, plinecode, stationcode, sn, usercode, stationcode1);
        }
        public void PL_UPDATE_BOMSOTHTS_BOMLQ(string so, string plinecode, string stationcode, string sn, string usercode, string stationcode1,string oldplinecode)
        {
            db.Execute("call PL_UPDATE_BOMSOTHTS_BOMLQ(@0,@1,@2,@3,@4,@5,@6)", so, plinecode, stationcode, sn, usercode, stationcode1,oldplinecode);
        }
        public void Station_Start(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string WorkDate, string QualityStauts,
                                            string ShiftCode, string TeamCode, string UserID)
        {
            db.Execute("call PL_HANDLE_STATION_START(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)", CompanyCode, PlineCode, StationCode, PlanCode,
                                                                                  SN, WorkDate, QualityStauts,ShiftCode, TeamCode, UserID);
        }
        public void Station_OffLine(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string quality)
        {
            db.Execute("call PL_HANDLE_STATION_OFFLINE(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, SN, quality);
        }

        public List<ProductDataEntity> GetProductDataByPlanSn(string CompanyCode, string PlineCode, string PlanCode, string Sn)
        {
            string SQL = "where company_code=@0 and pline_code=@1 and plan_code=@2 and sn=@3";
            return db.Fetch<ProductDataEntity>(SQL,CompanyCode, PlineCode, PlanCode, Sn);
        }

        public void ProcessControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            db.Execute("call PL_HANDLE_STATION_COMPLETE_PRC(@0,@1,@2,@3,@4)", CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
        public void BomControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            db.Execute("call PL_HANDLE_STATION_COMPLETE_BOM(@0,@1,@2,@3,@4)", CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
        public void QualityControlComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn)
        {
            db.Execute("call PL_HANDLE_STATION_COMPLETE_QUA(@0,@1,@2,@3,@4)", CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }

        //生成ISDE上线计划
        public void ISDE_CREATE_JK_SJJHB(string plinecode)
        {
            db.Execute("call ISDE_CREATE_JK_SJJHB(@0)", plinecode);
        }
                            
    }
}
