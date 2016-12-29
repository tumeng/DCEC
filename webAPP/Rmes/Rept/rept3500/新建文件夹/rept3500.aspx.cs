using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
/**
 * 功能概述：查询计划对应的装机提示
 * 作者：游晓航
 * 创建时间：2016-11-28
 */
namespace Rmes.WebApp.Rmes.Rept.rept3500
{
    public partial class rept3500 : BasePage
    {

        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        //public DateTime theBeginDate, theEndDate;
        public string theCompanyCode;
        private string theUserId, theUserCode, MachineName;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "rept3500";
            MachineName = System.Net.Dns.GetHostName();
            if (!IsPostBack)
            {
                initCode();

            }

            setCondition();
            

            if (Request["opFlag"] == "getEditSeries2")
            {
                string str1 = "", Site = "", So = "";
                //string so = Request["SO"].ToString().Trim().ToUpper();
                string plancode = Request["PLANCODE"].ToString().Trim();
                string sql = "SELECT CUSTOMER_NAME ,PLAN_SO,ROUNTING_SITE FROM DATA_PLAN WHERE PLAN_CODE='" + plancode + "' ";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    str1 = dt.Rows[0][0].ToString();

                    So = dt.Rows[0][1].ToString();
                    Site = dt.Rows[0][2].ToString();
                }
                str1 = str1 + "@" + So + "@" + Site;
                this.Response.Write(str1);
                this.Response.End();
            }
        }
        private void initCode()
        {
            //初始化下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            //string sql2 = "select distinct a.plan_code  from data_plan where ";
            //SqlPlanCode.SelectCommand = sql2;
            //SqlPlanCode.DataBind();
        }
        private void setCondition()
        {

            if (txtPCode.Text.Trim() != "")
            {
                string sql = "select DISTINCT a.part ,a.czts ,a.gxdm ,b.LOCATION_CODE ,a.gzdd  from rst_atpu_zjts a left join rel_location_process b on a.gxdm=b.process_code "
                          +"where a.jhdm = '" + txtPlanCode.Text.Trim() + "' and a.jhso='"+txtSO.Text.Trim()+"' and a.gzdd='" + txtPCode.Value.ToString() + "' ORDER BY A.PART ";
                DataTable dt = dc.GetTable(sql);
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }


        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }
        protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
        {
            if (txtPCode.Text.Trim() != "" && txtSO.Text.Trim() != "")
            {
                string sql = "SELECT PLAN_CODE FROM DATA_PLAN WHERE PLAN_SO='" + txtSO.Text.Trim().ToUpper() + "' and PLINE_CODE='" + txtPCode.Value.ToString() + "' order by  BEGIN_DATE DESC";
                SqlPlanCode.SelectCommand = sql;
                SqlPlanCode.DataBind();
            }
            else if (txtPCode.Text.Trim() != "")
            {
                string sql = "SELECT PLAN_CODE FROM DATA_PLAN WHERE begin_date>=sysdate-30  and begin_Date<=sysdate and PLINE_CODE='" + txtPCode.Value.ToString() + "' order by BEGIN_DATE DESC  ";
                SqlPlanCode.SelectCommand = sql;
                SqlPlanCode.DataBind();

            }

        }

    }
}