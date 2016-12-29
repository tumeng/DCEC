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
 * 功能概述：工位对应工序新增专用页面
 * 作    者：caoly
 * 创建时间：2014-04-02
 */


public partial class Rmes_epd2701 : BasePage
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

    protected void listBoxLocation_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select RMES_ID,LOCATION_CODE||'--'||LOCATION_NAME SHOWNAME from CODE_LOCATION where PLINE_CODE='" + pline + "' order by LOCATION_NAME";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox location = sender as ASPxComboBox;
        location.DataSource = dt;
        location.ValueField = "RMES_ID";
        location.TextField = "SHOWNAME";
        location.DataBind();
    }

    protected void listBoxProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string pline = e.Parameter;
        ASPxListBox location = sender as ASPxListBox;

        string sql = "SELECT A.RMES_ID,A.PROCESS_CODE,A.PROCESS_NAME FROM CODE_PROCESS A WHERE A.PLINE_CODE='"
            + pline + "' ORDER BY A.PROCESS_NAME";
        DataTable dt = dc.GetTable(sql);

        location.DataSource = dt;
        location.DataBind();
    }

    protected void butConfirm_Click(object sender, EventArgs e)
    {
        int count = 0;

        string location, pline, process;

        pline = comboPlineCode.SelectedItem.Value.ToString();
        location = ASPxListBoxLocation.SelectedItem.Value.ToString();

        for (count = 0; count < ASPxListBoxProcess.SelectedValues.Count; count++)
        {
            process = ASPxListBoxProcess.SelectedValues[count].ToString();

            if (dc.GetTable("select * from REL_LOCATION_PROCESS where pline_code='" + pline + "' and location_code='" + location + "' and PROCESS_CODE='" + process + "'").Rows.Count == 0)
            {
                string sql = "insert into REL_LOCATION_PROCESS(rmes_id,company_code,pline_code,location_code,PROCESS_CODE)"
                    + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + location + "','" + process + "')";
                dc.ExeSql(sql);
            }
        }

        Response.Write("<script type='text/javascript'>alert('新增工位工序关系成功！');window.opener.location.reload();window.close();</script>");
    }
    
}