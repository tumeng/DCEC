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
 * 功能概述：站点监测数据
 * 作    者：yangshx
 * 创建时间：2016-06-20
 */

namespace Rmes.WebApp.Rmes.Qms.qms2300
{
    public partial class qms2300 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "qms2300";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            setCondition();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_CODE PLINE_CODE1,D.PLINE_NAME,a.PRODUCT_SERIES,"
                + "A.STATION_CODE,C.STATION_NAME,C.STATION_CODE AS STATION_CODE1,"
                + "F.DETECT_CODE,F.DETECT_NAME,case F.DETECT_TYPE when '0' then '计量型' when '1' then '计点型' when '2' then '文本型' when '3' then '零件条码' end DETECT_TYPE,F.DETECT_STANDARD,F.DETECT_MAX,F.DETECT_MIN,F.DETECT_UNIT,A.DETECT_SEQ "
                + "FROM REL_STATION_DETECT A "
                + " LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
                + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
                 + " LEFT JOIN CODE_DETECT F ON A.DETECT_CODE =F.DETECT_CODE "
                + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "'"
                + "and a.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
                + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "') order by a.INPUT_TIME desc nulls last,a.DETECT_SEQ ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ////删除
            string id = e.Keys["RMES_ID"].ToString();

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO REL_STATION_DETECT_LOG (rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                            + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,user_code,flag,rqsj)"
                            + " SELECT rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                            + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,'"
                            + theUserCode + "' , 'DEL', SYSDATE FROM REL_STATION_DETECT WHERE  rmes_id =  '"
                            + id + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            string sql = "delete from REL_STATION_DETECT where rmes_id='" + id + "'";
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }

        protected void ASPxGridView1_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
        {

        }
    }
}