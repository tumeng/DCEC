using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;


namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition
{
    public partial class mmsMaterialRequisitionQry : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cmbPlineCode_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();
            string companyCode = theUserManager.getCompanyCode();
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsMaterialRequisitionQry' and company_code='" + companyCode 
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
            string sql = "delete FROM rst_qad_movepartcal where machinename='" + MachineName + "'";
            dc.ExeSql(sql);


            sql = "SELECT DISTINCT plan_code,plan_so,plan_qty FROM data_plan WHERE to_date(to_char(begin_date,'YYYY-MM-DD'),'yyyy-mm-dd')>=to_date('" + datePlanDate.Text
                + "','yyyy-mm-dd') and to_date(to_char(begin_date,'YYYY-MM-DD'),'yyyy-mm-dd')<=to_date('" + datePlanDateTo.Text + "','yyyy-mm-dd') and pline_code='" + cmbPline.Value.ToString() + "'";
            DataTable dt = dc.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BomReplaceFactory.QAD_CREATE_MOVEPARTCAL(cmbPline.Value.ToString(), dt.Rows[i]["plan_so"].ToString(),
                    dt.Rows[i]["plan_code"].ToString(), dt.Rows[i]["plan_qty"].ToString(), MachineName);
            }

            string condition = "";
            if (txtStoreman.Text != "")
                condition += " and upper(ljbgy)>=upper('" + txtStoreman.Text + "') and upper(ljbgy)<=upper('" + txtStoreman2.Text + "') ";
            if (txtGwdm.Text != "")
                condition += " and upper(ljgw)=upper('" + txtGwdm.Text + "')";
            if (txtLj.Text != "")
                condition += " and upper(ljdm)=upper('" + txtLj.Text + "')";

            sql = "SELECT ABOM_COMP  ,ABOM_WKCTR ,ABOM_KW ,SUM(ABOM_QTY) ABOM_QTY from rst_qad_movepartcal where machinename='" + MachineName + "' ";
            if (CheckSn.Checked)
                sql += condition;
            else
                sql = sql + condition + " and abom_comp in(select upper(ljdm) from atpubomthreset where to_char(rqsj,'yyyy-mm')='" + datePlanDate.Text.Substring(0, 7) + "')";

            sql += " GROUP BY ABOM_COMP,ABOM_WKCTR,ABOM_KW ORDER BY ABOM_COMP,ABOM_WKCTR";


            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void datePlanDateTo_Init(object sender, EventArgs e)
        {
            datePlanDateTo.Date = System.DateTime.Now;
        }
    }
}