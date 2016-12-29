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
 * 功能概述：站点对应工位
 * 作    者：yangshx
 * 创建时间：2016-06-01
 */

public partial class Rmes_epd3400 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId, theUserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd3400";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_CODE PLINECODE1,D.PLINE_NAME,A.LOCATION_CODE,B.LOCATION_NAME,B.LOCATION_CODE AS LOCATION_CODE1,"
            + "A.STATION_CODE,C.STATION_NAME,C.STATION_CODE AS STATION_CODE1,A.LOCATION_FLAG,A.LOCATION_FLAG1,"
            + "E.INTERNAL_NAME LOCATION_FLAG_NAME,F.INTERNAL_NAME LOCATION_FLAG1_NAME FROM REL_STATION_LOCATION A "
            + " LEFT JOIN CODE_LOCATION B ON A.LOCATION_CODE =B.RMES_ID  LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
            + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
            + " LEFT JOIN (select * from CODE_INTERNAL where internal_type_code='001') E ON A.LOCATION_FLAG=E.INTERNAL_CODE  "
            + " LEFT JOIN (select * from CODE_INTERNAL where internal_type_code='002') F ON A.LOCATION_FLAG1=F.INTERNAL_CODE "
            +" WHERE A.COMPANY_CODE = '" + theCompanyCode + "' "
            + "and a.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
            + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "SELECT  A.*,B.LOCATION_CODE LOCATION ,C.STATION_CODE STATION from REL_STATION_LOCATION_LOG A LEFT JOIN  CODE_LOCATION B on A.LOCATION_CODE =B.RMES_ID  LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
            + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' "
            + "and A.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
            + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')";
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    //protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    setCondition();
      
    //}
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
 
        string id = e.Keys["RMES_ID"].ToString();

        //插入到日志表
        try
        {
            string Sql1 = " SELECT * FROM rel_station_location WHERE rmes_id='" + id + "'";
            dc.setTheSql(Sql1);
            string rmes_id = dc.GetTable().Rows[0]["RMES_ID"].ToString();
            string company_code = dc.GetTable().Rows[0]["COMPANY_CODE"].ToString();
            string pline_code = dc.GetTable().Rows[0]["PLINE_CODE"].ToString();
            string location_code = dc.GetTable().Rows[0]["LOCATION_CODE"].ToString();
            string station_code = dc.GetTable().Rows[0]["STATION_CODE"].ToString();
            string location_flag = dc.GetTable().Rows[0]["LOCATION_FLAG"].ToString();
            string location_flag1 = dc.GetTable().Rows[0]["LOCATION_FLAG1"].ToString();
            string Sql2 = " INSERT INTO REL_STATION_LOCATION_LOG(rmes_id,company_code,pline_code,location_code,station_code,location_flag,location_flag1,user_code,flag,rqsj)"
                        + " VALUES( '" + rmes_id + "', '" + company_code + "','" + pline_code + "','" + location_code + "','" + station_code + "','" + location_flag + "','" + location_flag1 + "','" + theUserCode + "','DEL',SYSDATE)";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        ////删除
      

        string sql = "delete from rel_station_location where rmes_id='" + id + "'";

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
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("站点工位对应关系导出");
    }
}
