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
 * 功能概述：改制测量日志查询
 * 作者：游晓航
 * 创建时间：2016-09-16
 */
namespace Rmes.WebApp.Rmes.Rept.rept2300
{
    public partial class rept2300 : BasePage
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
            theProgramCode = "rept2300";
            //MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            if (!IsPostBack)
            {

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
            }

            setCondition();

        }

        private void setCondition()
        {
            string sn = "";
            sn = txtSN.Text.Trim();

            string sql = "select a.* from DATA_SN_DETECT_DATA  a   ";

            if (sn != "")
            {
                sql = sql + " where a.sn= '" + sn + "'";
            }
            else
            {
                sql = sql + "  left join data_plan b on a.plan_Code=b.plan_code  where  ( b.plan_type='C' OR  b.plan_type='D'）";
                //sql = sql + "  sn in (select ghtm from rst_ghtm_gz_log)";  
            }

            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql += " AND a.work_time >= TO_DATE('" + ASPxDateEdit1.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql += " AND a.work_time <= TO_DATE('" + ASPxDateEdit2.Text + "','YYYY-MM-DD HH24:MI:SS') ";
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