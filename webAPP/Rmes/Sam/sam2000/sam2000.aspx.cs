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
 * 功能概述：用户菜单权限和生产线权限
 * 作    者：游晓航
 * 创建时间：2016-06-12
 */

public partial class Rmes_sam2000 : BasePage
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
        string sql = "select t.user_id,a.user_name,t.program_code,b.program_name,t.pline_code from REL_USER_PLINE_PROGRAM t left join code_user a on t.user_id=a.user_id left join code_program b on t.program_code=b.program_code "
            + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "'";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ////删除
        string userId = e.Values["USER_ID"].ToString();
        string proCode = e.Values["PROGRAM_CODE"].ToString();
        string pliCode = e.Values["PLINE_CODE"].ToString();
        string sql = "delete from REL_USER_PLINE_PROGRAM where USER_ID='" + userId + "'and PROGRAM_CODE='" + proCode + "' and PLINE_CODE='" + pliCode + "' ";

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
