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
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Net;
/**
 * 功能概述：ECM发动机履历
 * 作者：游晓航
 * 创建时间：2016-09-20
 */
namespace Rmes.WebApp.Rmes.Rept.rept2600
{
    public partial class rept2600 : BasePage
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
            theProgramCode = "rept2600";
            //MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            if (!IsPostBack)
            {
                initCode();

            }
            
            setCondition();

        }
        private void initCode()
        {
            //初始化下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
           


        }
        private void setCondition()
        {

            if (txtPCode.Text.Trim() != "")
            {
                string sql = "select RMES_ID, pline_Code,sn,plan_code,plan_so,PRODUCT_SERIES,PRODUCT_MODEL,ORDER_CODE,WORK_TIME ,USER_ID,COMPLETE_TIME,UNITNO  from data_record where sn = '" + txtSN.Text.Trim() + "' and pline_code='" + txtPCode.Value.ToString() + "' "
                    + " union select  RMES_ID, pline_Code,sn,plan_code,plan_so,PRODUCT_SERIES,PRODUCT_MODEL,ORDER_CODE,WORK_TIME ,USER_ID,COMPLETE_TIME,UNITNO  from data_PRODUCT where sn = '" + txtSN.Text.Trim() + "' and pline_code='" + txtPCode.Value.ToString() + "' ";
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
         
    }
}