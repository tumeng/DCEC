using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;


namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsReplaceReport : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["mmsReplaceReport001"] = "";
            }
            if (Session["mmsReplaceReport001"] as string != "")
            {
                ASPxGridView1.DataSource = dc.GetTable(Session["mmsReplaceReport001"] as string);
                ASPxGridView1.DataBind();
            }
        }

        protected void cmbPlineCode_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();
            string companyCode = theUserManager.getCompanyCode();
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsReplaceReport' and company_code='" + companyCode 
                + "' ";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }

        protected void datePlanDate_Init(object sender, EventArgs e)
        {
            datePlanDate.Date = System.DateTime.Now;
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string MachineName = Request.UserHostAddress;
            string gzdd1 = "";
            try
            { gzdd1 = cmbPline.Value.ToString(); }
            catch
            { }
            string sql = "select so ,ljdm1 ,ljdm2 , jhdm ,lrsj , ygmc  from SJBOMSOTH  where gzdd='" + gzdd1 + "' and LRSJ>=TO_DATE('" + datePlanDate.Text + "','YYYY-MM-DD') and LRSJ<=TO_DATE('" + datePlanDateTo.Text + "','YYYY-MM-DD')";
            Session["mmsReplaceReport001"] = sql;
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("导出报表");
        }
        protected void datePlanDateTo_Init(object sender, EventArgs e)
        {
            datePlanDateTo.Date = System.DateTime.Now;
        }
    }
}