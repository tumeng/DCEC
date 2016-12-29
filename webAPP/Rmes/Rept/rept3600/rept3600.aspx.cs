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
 * 功能概述：合格证打印查询
 * 作者：游晓航
 * 创建时间：2016-12-22
 */
namespace Rmes.WebApp.Rmes.Rept.rept3600
{
    public partial class rept3600 : BasePage
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
            theProgramCode = "rept3600";
            //MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            if (!IsPostBack)
            {

                ASPxDateEdit1.Date = DateTime.Now.Date;
                ASPxDateEdit2.Date = DateTime.Now;
            }

            setCondition();

        }

        private void setCondition()
        {
            //string sn = "";
            //sn = txtSN.Text.Trim();

            string sql = "select t.*,t.rowid from ATPUHGZ  t WHERE t.RQ>= TO_DATE('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS') AND t.RQ<=TO_DATE('" + ASPxDateEdit2.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS')  ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();


        }
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {        
            ASPxTextBox txtXLH = ASPxGridView1.FindEditFormTemplateControl("txtXLH") as ASPxTextBox;
            ASPxTextBox txtJYY = ASPxGridView1.FindEditFormTemplateControl("txtJYY") as ASPxTextBox;
            ASPxTextBox txtROWID = ASPxGridView1.FindEditFormTemplateControl("txtROWID") as ASPxTextBox;
            try
            {
                string Sql2 = "UPDATE ATPUHGZ T SET T.XLH='" + txtXLH.Text.Trim() + "' , T.JYY='"+txtJYY.Text.Trim()+"'  WHERE T.ROWID='" + txtROWID.Text.Trim() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
              
            }
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }


    }
}