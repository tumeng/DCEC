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
 * 功能概述：改制扫描日志查询
 * 作者：游晓航
 * 创建时间：2016-09-16
 */
namespace Rmes.WebApp.Rmes.Rept.rept2200
{
    public partial class rept2200 : BasePage
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
            theProgramCode = "rept2200";
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
             string sql="select t.*,a.user_name,case t.ITEM_TYPE WHEN 'A' THEN  (t.ITEM_CODE||'^'||t.VENDOR_CODE||'^'||t.ITEM_BATCH) when 'B'   THEN   (t.ITEM_CODE||'^'||t.VENDOR_CODE||'^'||t.ITEM_SN) "
                       + "else t.ITEM_TYPE end BARCODE from vw_data_sn_bom t left join code_user a on a.user_id=t.create_userid where  sn in (select sn from data_record)";
             
           
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql += " AND CREATE_TIME >= TO_DATE('" + ASPxDateEdit1.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql += " AND CREATE_TIME <= TO_DATE('" + ASPxDateEdit2.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (txtSN.Text.Trim() != "")
            {
                sql += " AND SN = '"+txtSN.Text.Trim()+"' ";
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