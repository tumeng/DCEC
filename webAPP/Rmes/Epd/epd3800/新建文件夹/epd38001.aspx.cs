using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
 * 功能概述：工位员工对应关系
 * 作    者：yangshx
 * 创建时间：2016-06-12
 */

public partial class Rmes_epd3800 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_NAME,A.LOCATION_CODE,B.LOCATION_NAME,B.LOCATION_CODE AS LOCATION_CODE1,"
            + "A.USER_ID,C.USER_CODE,C.USER_NAME  "
            + " FROM rel_user_location A "
            + " LEFT JOIN CODE_LOCATION B ON A.LOCATION_CODE =B.RMES_ID  "
            + "LEFT JOIN CODE_USER C ON A.USER_ID =C.USER_ID "
            + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
            +" WHERE A.COMPANY_CODE = '" + theCompanyCode + "'";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ////删除
        string id = e.Keys["RMES_ID"].ToString();

        string sql = "delete from rel_user_location where rmes_id='" + id + "'";

        dc.ExeSql(sql);

        setCondition();
        e.Cancel = true;
    }

    protected void ASPxGridView1_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
    {
        
        if (e.Value.ToString() != "")
        {
            e.Value = e.Value.Replace("%", "");
            e.Criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse(e.Criteria.LegacyToString().Replace(e.Value + "%", "%" + e.Value + "%"));
            e.Value = e.Value.Replace("%", "");
        }

    

    }

}
