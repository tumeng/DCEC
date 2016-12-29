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


public partial class Rmes_epd3801 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd3800";
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

        string sql = "select RMES_ID,STATION_CODE,STATION_NAME from CODE_STATION where PLINE_CODE='" + pline + "' order by STATION_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox station = sender as ASPxComboBox;
        station.DataSource = dt;
        station.ValueField = "RMES_ID";
        station.TextField = "STATION_NAME";
        station.DataBind();
    }
    protected void listBoxUser_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        ASPxListBox listUser = sender as ASPxListBox;

        string sql = "SELECT A.USER_ID,A.USER_CODE,A.USER_CODE||';'||A.USER_NAME USER_NAME FROM CODE_USER A "
                   + " WHERE NOT EXISTS(SELECT * FROM REL_STATION_USER C WHERE C.user_id=A.user_id   )  and user_type='B' "
                   + " ORDER BY A.USER_CODE";

        DataTable dt = dc.GetTable(sql);

        listUser.DataSource = dt;
        listUser.DataBind();
    }
    protected void butConfirm_Click(object sender, EventArgs e)
    {
        int count = 0;

        string userId, pline, location;
        
        pline = comboPlineCode.SelectedItem.Value.ToString();
        location = comboStationCode.SelectedItem.Value.ToString();

        for (count = 0; count < listChosedUser.Items.Count; count++)
        {
            userId = listChosedUser.Items[count].ToString();

            string[] sArray = userId.Split(';');
            string stacode = sArray[0];//站点代码
            string staid = dc.GetValue("select user_id from code_user where user_code='"+stacode+"' and rownum=1 ");
            string sql = "insert into REL_STATION_USER(rmes_id,company_code,pline_code,STATION_CODE,user_id)"
                       + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + location + "','" + staid + "')";
            dc.ExeSql(sql);
            
          }

        Response.Write("<script type='text/javascript'>alert('新增站点员工关系成功！');window.opener.location.reload();location.href='epd3801.aspx';</script>");//window.opener.location.reload();window.close();

    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

    
    
}