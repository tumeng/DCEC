using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;


/**
 * 功能概述：现场事件控制维护
 * 作    者：yangshx
 * 创建时间：2016-08-10
 */

namespace Rmes.WebApp.Rmes.Atpu.atpu1E00
{
    public partial class atpu1E01 : BasePage
    {
        private dataConn dc = new dataConn();
        public Database db = DB.GetInstance();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "atpu1E00";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            //string Sql2 = "SELECT STATION_CODE,STATION_NAME FROM CODE_STATION order by STATION_CODE ";
            //SqlDataSource2.SelectCommand = Sql2;
            //SqlDataSource2.DataBind();

            string Sql4 = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                        + "left join code_product_line b on a.pline_code=b.pline_code "
                        + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            SqlDataSource4.SelectCommand = Sql4;
            SqlDataSource4.DataBind();
        }
        protected void comboStationName_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select A.STATION_CODE,A.STATION_CODE||'--'||A.STATION_NAME STATION_NAME,B.PLINE_CODE  from CODE_STATION A LEFT JOIN CODE_PRODUCT_LINE B ON A.PLINE_CODE=B.RMES_ID where B.PLINE_CODE='" + pline + "' order by A.STATION_NAME";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "STATION_CODE";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }
        

        protected void butConfirm_Click(object sender, EventArgs e)
        {
            string pline, stationCode, sjdm, sjmc;

            pline = comboPLine.SelectedItem.Value.ToString();
            stationCode = comboStationName.SelectedItem.Value.ToString();
            sjdm = txtSJDM.Text.Trim();
            sjmc = txtSJMC.Text.Trim();

            string sSql = "select station_name from code_station where station_code='" + stationCode + "'";
            dataConn sDc = new dataConn(sSql);
            DataTable sDt = sDc.GetTable();
            string stattionName = "";
            if (sDt.Rows.Count > 0)
            {
                stattionName = sDt.Rows[0]["station_name"].ToString();
            }

            string sql = "insert into ATPUXCSJB(GZDD,SJDM,SJMC,SJBS)"
                       + "VALUES( '" + pline + "','" + sjdm + "','" + sjmc + "','0')";
            dc.ExeSql(sql);
            //插入到日志表161103
            try
            {
                string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                    + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUXCSJB WHERE  GZDD =  '"
                    + pline + "' and SJDM =  '" + sjdm + "' and SJMC =  '" + sjmc + "' and SJBS =  '0' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            Response.Write("<script type='text/javascript'>alert('新增成功！');window.opener.location.reload();location.href='atpu1E01.aspx';</script>");
        }
        protected void butCloseWindow_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }
    }
}