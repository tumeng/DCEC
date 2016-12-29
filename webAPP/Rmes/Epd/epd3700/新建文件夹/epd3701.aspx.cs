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


public partial class Rmes_epd3701 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd3700";
        theUserId = theUserManager.getUserId();

        string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

    }

    protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE='" + pline + "' order by STATION_NAME";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox station = sender as ASPxComboBox;
        station.DataSource = dt;
        station.ValueField = "RMES_ID";
        station.TextField = "STATION_NAME";
        station.DataBind();
    }

    protected void listBoxPreStation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string pline = e.Parameter;
        
        ASPxListBox preStation = sender as ASPxListBox;

        string sql = "SELECT A.RMES_ID,A.STATION_CODE,A.STATION_NAME STATION_NAME FROM CODE_STATION A WHERE A.PLINE_CODE='" + pline + "'  "  
                + " ORDER BY A.STATION_NAME";
       
        DataTable dt = dc.GetTable(sql);

        preStation.DataSource = dt;
        preStation.DataBind();
        
    }
    
    protected void butConfirm_Click(object sender, EventArgs e)
    {
        int count = 0;

        string preStation, pline, station;
        string stationName = "";
        string preStationName = "";

        pline = comboPlineCode.SelectedItem.Value.ToString();
        station = comboStationCode.SelectedItem.Value.ToString();

        string sql1 = "select station_code,station_name from code_station where company_code='"+theCompanyCode+"' and rmes_id='"+station+"'";
        DataTable dt1 = dc.GetTable(sql1);
        if (dt1.Rows.Count > 0)
        {
            stationName = dt1.Rows[0]["station_name"].ToString();
        }

        for (count = 0; count < listChosedStation.Items.Count; count++)
        {
            preStation = listChosedStation.Items[count].ToString();
            string[] preStation1 = preStation.Split(';');
            preStation = dc.GetValue("select rmes_id from code_station where station_code='"+preStation1[0]+"'");
            string sql2 = "select station_code,station_name from code_station where company_code='" + theCompanyCode + "' and rmes_id='" + preStation + "'";
            DataTable dt2 = dc.GetTable(sql2);
            if (dt2.Rows.Count > 0)
            {
                preStationName = dt1.Rows[0]["station_name"].ToString();
            }

            string sql = "insert into REL_STATION_PRESTATION(rmes_id,company_code,pline_code,station_code,station_name,PRESTATION_CODE,PRESTATION_NAME)"
                       + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + station + "','" + stationName + "','" + preStation + "','" + preStationName + "')";
            dc.ExeSql(sql);
            
          }

        Response.Write("<script type='text/javascript'>alert('新增站点前道站点关系成功！');window.opener.location.reload();location.href='epd3701.aspx';</script>");
    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }
    
}