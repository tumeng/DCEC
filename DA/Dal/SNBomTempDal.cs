using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.Public.Base;

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
    internal class SNBomTempDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomTempEntity> GetAll()
        {
            return db.Fetch<SNBomTempEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public SNBomTempEntity GetByKey(string RmesID)
        {
            return db.First<SNBomTempEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomTempEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SNBomTempEntity>("WHERE RMES_ID=@0", RmesID);
        }


        public List<SNBomTempEntity> GetByPlanStaton(string CompanyCode, string PlanID, string PlineCode, string StationCode, string Sn, string fdjxl, string so)
        {
            try
            {
                ////调用存储过程 获取替换后的站点BOM 插入data_sn_bom_temp和rstbomqd、rstbomts
                //StationEntity ent_st = StationFactory.GetBySTATIONCODE(StationCode);
                //string station_name = ent_st.STATION_NAME;
                //ProductDataFactory.PL_QUERY_BOMZJTS(so, station_name, PlineCode, fdjxl, PlanID, Sn);
                //ProductDataFactory.PL_UPDATE_BOMZJTS(so, station_name, PlanID, PlineCode, Sn);
                //ProductDataFactory.PL_UPDATE_BOMLSHTS(Sn, StationCode);
                //ProductDataFactory.PL_UPDATE_BOMSOTHTS(so, PlanID, StationCode, Sn,"");

                string SQL = " left join vw_data_sn_bom on rstbomqd.gwmc=vw_data_sn_bom.location_name " 
                    + " and rstbomqd.gxmc=vw_data_sn_bom.PROCESS_CODE and rstbomqd.comp=vw_data_sn_bom.item_code  "
                    + " and vw_data_sn_bom.sn='"+Sn+"' and vw_data_sn_bom.plan_code='"+PlanID+"' and vw_data_sn_bom.pline_code='' "
                    + " where zddm='" + StationCode + "' order by gwmc,gxmc;  ";
                return db.Fetch<SNBomTempEntity>(SQL);
            }
            catch
            {
                return null;
            }
        }
        public void GetByPlanStatonDcec(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so,string stationcode1)
        {
            try
            {
                //大线生成计划对应的站点bom是根据当前登录的站点StationCode，返修点是根据选择的站点，这两个变量统一，但生成snbomtemp时，返修点要根据返修站点当前登录站点生成
                //调用存储过程 获取替换后的站点BOM 插入data_sn_bom_temp和rstbomqd、rstbomts
                StationEntity ent_st = StationFactory.GetBySTATIONCODE(StationCode);
                string station_name = ent_st.STATION_NAME;
                ProductDataFactory.PL_QUERY_BOMZJTS(so, StationCode, PlineCode, fdjxl, PlanCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMZJTS(so, StationCode, PlanCode, PlineCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMLSHTS(Sn, StationCode);
                ProductDataFactory.PL_UPDATE_BOMSOTHTS(so, PlanCode, StationCode, Sn,LoginInfo.UserInfo.USER_CODE,stationcode1);
                ProductDataFactory.PL_UPDATE_BOMSOTHTS_BOM(so, PlanCode, StationCode, Sn, LoginInfo.UserInfo.USER_CODE, stationcode1);
            }
            catch(Exception e1)
            {
                return;
            }
        }
        public void GetByPlanStatonDcecLQ(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string stationcode1, ProductInfoEntity productnew)
        {
            try
            {
                //大线生成计划对应的站点bom是根据当前登录的站点StationCode，返修点是根据选择的站点，这两个变量统一，但生成snbomtemp时，返修点要根据返修站点当前登录站点生成
                //调用存储过程 获取替换后的站点BOM 插入data_sn_bom_temp和rstbomqd、rstbomts
                StationEntity ent_st = StationFactory.GetBySTATIONCODE(StationCode);
                string station_name = ent_st.STATION_NAME;
                ProductDataFactory.PL_QUERY_BOMZJTS(so, StationCode, PlineCode, fdjxl, PlanCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMZJTS(so, StationCode, PlanCode, PlineCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMLSHTS(Sn, StationCode);
                ProductDataFactory.PL_UPDATE_BOMSOTHTS(so, PlanCode, StationCode, Sn, LoginInfo.UserInfo.USER_CODE, stationcode1);
                ProductDataFactory.PL_UPDATE_BOMSOTHTS_BOMLQ(productnew.PLAN_SO, productnew.PLAN_CODE, StationCode, Sn, LoginInfo.UserInfo.USER_CODE, stationcode1, PlineCode);
            }
            catch (Exception e1)
            {
                return;
            }
        }
        public void GetByPlanStatonDcec_CL(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string stationcode1)
        {
            try
            {
                //大线生成计划对应的站点bom是根据当前登录的站点StationCode，返修点是根据选择的站点，这两个变量统一，但生成snbomtemp时，返修点要根据返修站点当前登录站点生成
                //调用存储过程 获取替换后的站点BOM 插入data_sn_bom_temp和rstbomqd、rstbomts
                StationEntity ent_st = StationFactory.GetBySTATIONCODE(StationCode);
                string station_name = ent_st.STATION_NAME;
                ProductDataFactory.PL_QUERY_BOMZJTS(so, StationCode, PlineCode, fdjxl, PlanCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMZJTS(so, StationCode, PlanCode, PlineCode, Sn);
                ProductDataFactory.PL_UPDATE_BOMLSHTS(Sn, StationCode);
                ProductDataFactory.PL_UPDATE_BOMSOTHTS(so, PlanCode, StationCode, Sn, LoginInfo.UserInfo.USER_CODE, stationcode1);
            }
            catch (Exception e1)
            {
                return;
            }
        }
        public void GetByPlanStatonZJTS(string CompanyCode, string PlanCode, string PlineCode, string StationCode, string Sn, string fdjxl, string so, string stationcode1)
        {
            try
            {
                //大线生成计划对应的站点bom是根据当前登录的站点StationCode，返修点是根据选择的站点，这两个变量统一，但生成snbomtemp时，返修点要根据返修站点当前登录站点生成
                //调用存储过程 获取替换后的站点BOM 插入data_sn_bom_temp和rstbomqd、rstbomts
                StationEntity ent_st = StationFactory.GetBySTATIONCODE(StationCode);
                string station_name = ent_st.STATION_NAME;
                ProductDataFactory.PL_QUERY_ZJTS(so, StationCode, PlineCode, fdjxl, PlanCode, Sn);
            }
            catch (Exception e1)
            {
                return;
            }
        }
        public bool BomItemNotConfirmed(string CompanyCode, string PlanCode,string Sn,string PlineCode, string Station)
        {
            string SQL = "select count(*) from data_sn_bom_temp where company_code=@0 and plan_code=@1  and sn=@2 "+
                         "and pline_code=@3  and confirm_flag='N'";
            int count = db.ExecuteScalar<int>(SQL, CompanyCode, PlanCode,Sn,PlineCode, Station);
            return count>0;
        }

        public void InitSnBomTemp(string CompanyCode, string PlanCode,string StationCode,string Sn)
        {
            string UserID=LoginInfo.UserInfo.USER_ID;
            db.Execute("call PL_INIT_BOM_TEMP(@0,@1,@2,@3,@4)", CompanyCode, PlanCode, StationCode, Sn, UserID);
        }
        public void HandleBomItemComplete(string CompanyCode, string rmesid, string itemcode, string VendorCode, string ItemBatch, float CompleteQty, string stationname,string barcode,int num)
        {
            db.Execute("call PL_HANDLE_BOM_COMPLETE(@0,@1,@2,@3,@4,@5,@6,@7,@8)", CompanyCode, rmesid, itemcode, VendorCode, ItemBatch, CompleteQty, stationname,barcode,num);
        }


    }
}
#endregion