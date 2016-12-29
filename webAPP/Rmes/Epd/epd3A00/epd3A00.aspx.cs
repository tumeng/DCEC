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
 * 功能概述：分装站点取点维护
 * 作    者：yangshx
 * 创建时间：2016-08-01
 */


namespace Rmes.WebApp.Rmes.Epd.epd3A00
{
    public partial class epd3A00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode;
        private string theUserId, theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            theProgramCode = "epd3A00";
            setCondition();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_CODE PLINECODE1,D.PLINE_NAME,A.SUBSTATION_ID,A.SUBSTATION_CODE,A.SUBSTATION_NAME,A.CFSTATION_ID,"
                       +"B.STATION_NAME CFSTATION_NAME,B.STATION_CODE AS CFSTATION_CODE,"
                       + "A.ZPSTATION_ID,C.STATION_NAME AS ZPSTATION_NAME,C.STATION_CODE AS ZPSTATION_CODE,A.SUB_ZC,A.CHECK_FLAG,E.INTERNAL_NAME CHECK_FLAG_NAME  "
                       + " FROM CODE_STATION_SUB A "
                       + " LEFT JOIN CODE_STATION B ON A.CFSTATION_ID =B.RMES_ID  "
                       + "LEFT JOIN CODE_STATION C ON A.ZPSTATION_ID =C.RMES_ID "
                       + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
                       + " LEFT JOIN (select * from CODE_INTERNAL where internal_type_code='005') E ON A.CHECK_FLAG=E.INTERNAL_CODE  "
                       + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code in "
                       +"(select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "'"
                       +" and company_code='" + theCompanyCode + "') order by a.input_time desc nulls last ";
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
                string Sql2 = "INSERT INTO CODE_STATION_SUB_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SUBSTATION_ID,SUBSTATION_CODE,SUBSTATION_NAME,"
                             + "CFSTATION_ID,ZPSTATION_ID,SUB_ZC,CHECK_FLAG,user_code,flag,rqsj)"
                    + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,SUBSTATION_ID,SUBSTATION_CODE,SUBSTATION_NAME,"
                             + "CFSTATION_ID,ZPSTATION_ID,SUB_ZC,CHECK_FLAG,'" + theUserCode + "' , 'DEL', SYSDATE FROM CODE_STATION_SUB WHERE rmes_id='" + id + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string sql = "delete from CODE_STATION_SUB where rmes_id='" + id + "'";
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }
    }
}