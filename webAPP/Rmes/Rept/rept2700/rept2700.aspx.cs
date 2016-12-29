using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
/**
 * 功能概述：消息中心
 * 作者：游晓航
 * 创建时间：2016-09-28
 */
namespace Rmes.WebApp.Rmes.Rept.rept2700
{
    public partial class rept2700 : BasePage
    {

        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        //public DateTime theBeginDate, theEndDate;
        public string theCompanyCode;
        private string theUserId, theUserName, MachineName;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theProgramCode = "rept2700";
            
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now.AddDays(-3);
                ASPxDateEdit2.Date = DateTime.Now;

            }
            
            setCondition();

        }
        //private void initCode()
        //{
        //    //初始化下拉列表
        //    string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        //    SqlCode.SelectCommand = sql;
        //    SqlCode.DataBind();    

        //}
        private void setCondition()
        {
            string sql = "";
            if (chkProcess.Value.ToString() == "True")
            {
               sql = "select send ,title ,body ,to_char(msgtime,'yyyy-mm-dd') msgday,to_char(msgtime,'hh24:mi:ss') msgtime,rowid from msg_mstr where  deleted='0' and msgtime>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and msgtime<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and recieve='" + theUserName + "'";

            }
            else
            {
                sql = "select send ,title ,body ,to_char(msgtime,'yyyy-mm-dd') msgday,to_char(msgtime,'hh24:mi:ss') msgtime,rowid from msg_mstr where read='0' and deleted='0' and msgtime>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and msgtime<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and recieve='" + theUserName + "'";
            }

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }
         
    }
}