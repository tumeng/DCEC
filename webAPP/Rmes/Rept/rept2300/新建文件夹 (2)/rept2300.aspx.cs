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
            MachineName = System.Net.Dns.GetHostName();
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

            string sql = "select * from DATA_SN_DETECT_DATA  where    ";

            if (sn != "")
            {
                sql = sql + "  sn= '" + sn + "'";
            }
            else
            {
                sql = sql + "  sn in (select sn from data_record)";
            }

            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql += " AND work_time >= TO_DATE('" + ASPxDateEdit1.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql += " AND work_time <= TO_DATE('" + ASPxDateEdit2.Text + "','YYYY-MM-DD HH24:MI:SS') ";
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