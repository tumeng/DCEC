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
 * 功能概述：工位对应工序
 * 作    者：yangshx
 * 创建时间：2016-06-02
 */

public partial class Rmes_epd2700 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd2700";
        theUserId = theUserManager.getUserId();

        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_CODE PLINECODE1,D.PLINE_NAME,A.LOCATION_CODE,B.LOCATION_NAME,A.PROCESS_CODE,C.PROCESS_NAME, "
            + " B.LOCATION_CODE AS LOCATION_CODE1,C.PROCESS_CODE AS PROCESS_CODE1,a.PROCESS_SEQ "
            + " FROM REL_LOCATION_PROCESS A "
            + " LEFT JOIN CODE_LOCATION B ON A.LOCATION_CODE =B.RMES_ID "
            + " LEFT JOIN CODE_PROCESS C ON A.PROCESS_CODE =C.RMES_ID  "
            + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
            +" WHERE A.COMPANY_CODE = '" + theCompanyCode + "' "
            + "and a.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
            + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')"
            +"order by a.pline_code,a.location_code,a.process_seq";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ////删除
        string id = e.Keys["RMES_ID"].ToString();

        string sql = "delete from REL_LOCATION_PROCESS where rmes_id='" + id + "'";

        dc.ExeSql(sql);

        setCondition();
        e.Cancel = true;
    }

}
