using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridView;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

/**
 * 功能概述：物料替换规则 显示 删除页面
 * 作    者：caoly
 * 创建时间：2014-04-19
 * 修改时间：2014-05-09
 */

public partial class Rmes_mms1400 : BasePage
{
    private dataConn dc = new dataConn();
    public Database db = DB.GetInstance();

    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserID = theUserManager.getUserId();

        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT t.*,b.user_name from DATA_XK_BOM_EXCHANGE t "
            + " left join code_user b on b.user_id=t.user_id"
            + " where enable_flag='Y' order by t.item_code_from";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ////删除
        string id = e.Keys["RMES_ID"].ToString();

        //删除只是把enable_flag设置为N，不再显示，防止用户赖账，不承认有过该规则
        string sql = "update DATA_XK_BOM_EXCHANGE set enable_flag='N' where rmes_id='" + id + "'";

        dc.ExeSql(sql);

        setCondition();
        e.Cancel = true;
    }

}