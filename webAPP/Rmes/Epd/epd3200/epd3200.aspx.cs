using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxPanel;
using DevExpress.Web.ASPxClasses.Internal;
using System.Collections.Generic;

/**
 * 功能概述：现场事件定义
 * 作者：杨少霞
 * 创建时间：2011-07-28
 */

public partial class Rmes_epd3200 : Rmes.Web.Base.BasePage
{
    public string theCompanyCode;
    public dataConn theDc = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        queryFunction();
    }

    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT A.COMPANY_CODE, A.PLINE_CODE, A.EVENT_CODE, A.EVENT_NAME, DECODE(A.EVENT_FLAG,'Y',1,'N',0) EVENT_FLAG,B.PLINE_NAME FROM CODE_CONTROL_EVENT A"
            + " LEFT JOIN CODE_PRODUCT_LINE B ON A.COMPANY_CODE=B.COMPANY_CODE AND A.PLINE_CODE=B.RMES_ID WHERE A.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE,EVENT_CODE";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt ;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string eFlag = "";

        if (e.NewValues["EVENT_FLAG"].ToString() == "1")
        {
            eFlag = "Y";
        }
        else
        {
            eFlag = "N";
        }

        string upSql = "UPDATE CODE_CONTROL_EVENT SET EVENT_FLAG='" + eFlag + "'"
                     + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "' AND EVENT_CODE='" + e.NewValues["EVENT_CODE"].ToString() + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    //处理表头否控制字段的过滤
    protected void ASPxGridView1_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
    {
        if (object.Equals(e.Column, ASPxGridView1.Columns["EVENT_FLAG"]))
        {
            e.Values.Clear();
            e.AddShowAll();
            e.AddValue("Yes", "1");
            e.AddValue("No", "0");
            return;
        }
    }
    

}
