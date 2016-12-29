using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;
/**
 * 功能概述：分装站点取点维护
 * 作    者：yangshx
 * 创建时间：2016-08-01
 */

namespace Rmes.WebApp.Rmes.Epd.epd3A00
{
    public partial class epd3A01 : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "epd3A00";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                       + "left join code_product_line b on a.pline_code=b.pline_code "
                       + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            comboPlineCode.DataSource = dc.GetTable(sql);
            comboPlineCode.DataBind();

            string sql1 = "select a.internal_code,a.internal_name from code_internal a where a.internal_type_code='005' order by a .internal_code";
            comboCFlag.DataSource = dc.GetTable(sql1);
            comboCFlag.TextField = "internal_name";
            comboCFlag.ValueField = "internal_code";
            comboCFlag.DataBind();
        }
        protected void comboSUBStation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select t.RMES_ID,t.STATION_CODE,t.STATION_NAME from CODE_STATION t left join code_product_line a on t.pline_code=a.pline_code where t.pline_code='" + pline + "' AND t.STATION_TYPE='ST04'"
                       + " and t.RMES_ID not in(select distinct substation_id from CODE_STATION_SUB) order by t.STATION_NAME";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }
        protected void comboCFStation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select t.RMES_ID,t.STATION_CODE,t.STATION_NAME from CODE_STATION t left join code_product_line a on t.pline_code=a.pline_code where t.pline_code='" + pline + "'  order by t.STATION_NAME";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }
        protected void comboZPStation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select t.RMES_ID,t.STATION_CODE,t.STATION_NAME from CODE_STATION t left join code_product_line a on t.pline_code=a.pline_code where t.pline_code='" + pline + "'  order by STATION_NAME";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }
        
        protected void butConfirm_Click(object sender, EventArgs e)
        {
            string pline, subStation, cfStation, zpStation, subZc, cFlag = "" ;
            string subStationCode = "";
            string sunStationName = "";

            pline = comboPlineCode.SelectedItem.Value.ToString();
            subStation = comboSUBStation.SelectedItem.Value.ToString();

            string sql = "select station_code,station_name from code_station where rmes_id='"+subStation+"'";
            dataConn cdc = new dataConn(sql);
            DataTable cdt = cdc.GetTable();
            if (cdt.Rows.Count > 0)
            {
                subStationCode = cdt.Rows[0]["station_code"].ToString();
                sunStationName = cdt.Rows[0]["station_name"].ToString();
            }

            cfStation = comboCFStation.SelectedItem.Value.ToString();
            zpStation = comboZPStation.SelectedItem.Value.ToString();
            subZc = txtPlineCode.Text.Trim();
            if (subZc != "")
            {
                if (comboCFlag.SelectedIndex >= 0)
                {
                    cFlag = comboCFlag.SelectedItem.Value.ToString();
                }
            }

            string chSql = "select PT_PART from COPY_PT_MSTR where PT_PART='" + subZc + "' ";
            dataConn chDc = new dataConn(chSql);
            Boolean t = chDc.GetState();
            //if (subZc == "")//subZc!=""&&chDc.GetState() == false
            //{
            //    lblMessage.Text = "录入的分装总成号" + subZc + "错误！";
            //}
            //else
            {
                //取RMES_ID的值
                string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
                dc.setTheSql(sql_rmes_id);
                string rmes_id = dc.GetTable().Rows[0][0].ToString();

                string inSql = "insert into CODE_STATION_SUB (RMES_ID,COMPANY_CODE,PLINE_CODE,SUBSTATION_ID,SUBSTATION_CODE,SUBSTATION_NAME,"
                             + "CFSTATION_ID,ZPSTATION_ID,SUB_ZC,CHECK_FLAG,INPUT_PERSON,INPUT_TIME) values('" + rmes_id + "','" + theCompanyCode + "','" + pline + "',"
                             + "'" + subStation + "','" + subStationCode + "','" + sunStationName + "','" + cfStation + "','" + zpStation + "','" + subZc + "','" + cFlag + "','"+theUserId+"',SYSDATE)";
                dc.ExeSql(inSql);

                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO CODE_STATION_SUB_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SUBSTATION_ID,SUBSTATION_CODE,SUBSTATION_NAME,"
                                 + "CFSTATION_ID,ZPSTATION_ID,SUB_ZC,CHECK_FLAG,user_code,flag,rqsj)"
                        + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,SUBSTATION_ID,SUBSTATION_CODE,SUBSTATION_NAME,"
                                 + "CFSTATION_ID,ZPSTATION_ID,SUB_ZC,CHECK_FLAG,'" + theUserCode + "' , 'ADD', SYSDATE FROM CODE_STATION_SUB WHERE RMES_ID='" + rmes_id + "'";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }

                Response.Write("<script type='text/javascript'>alert('新增分装工作站取点成功！');window.opener.location.reload();location.href='epd3A01.aspx';</script>");
            }
        }
        protected void butCloseWindow_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }
    }
}