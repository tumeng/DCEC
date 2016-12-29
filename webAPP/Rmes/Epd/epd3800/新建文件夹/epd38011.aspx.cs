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
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        string sql = "select RMES_ID,PLINE_NAME from CODE_PRODUCT_LINE where COMPANY_CODE = '" + theCompanyCode + "' order by PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

    }

    protected void comboLocationCode_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select RMES_ID,LOCATION_CODE,LOCATION_NAME from CODE_LOCATION where PLINE_CODE='" + pline + "' order by LOCATION_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox station = sender as ASPxComboBox;
        station.DataSource = dt;
        station.ValueField = "RMES_ID";
        station.TextField = "LOCATION_NAME";
        station.DataBind();
    }
    protected void listBoxUser_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        ASPxListBox listUser = sender as ASPxListBox;

        string sql = "SELECT A.USER_ID,A.USER_CODE,A.USER_NAME FROM CODE_USER A "
                   + " WHERE NOT EXISTS(SELECT * FROM rel_user_location C WHERE C.user_id=A.user_id   )   "
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
        location = comboLocationCode.SelectedItem.Value.ToString();

        for (count = 0; count < listBoxUser.SelectedValues.Count; count++)
        {
            userId = listBoxUser.SelectedValues[count].ToString();

            string sql = "insert into rel_user_location(rmes_id,company_code,pline_code,location_code,user_id)"
                       + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + location + "','" + userId + "')";
            dc.ExeSql(sql);
            
          }

        Response.Write("<script type='text/javascript'>alert('新增工位员工关系成功！');</script>");//window.opener.location.reload();window.close();

        comboPlineCode.Text = "";
        comboLocationCode.Text = "";
        listBoxUser.Items.Clear();
        
    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

    
    
}