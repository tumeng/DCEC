using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;
using System.Net;


namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialSend
{
    public partial class mmsMaterialSend : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                listPlan.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = listPlan.GetSelectedIndex();if(index!=-1) listPlan.RemoveItem(index);}";
        }

        protected void cmbPlineCode_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();
            string companyCode = theUserManager.getCompanyCode();
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsMaterialSend' and company_code='" + companyCode + "'";
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
        protected void cmbPlan_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            try
            {
                //根据生产线显示计划
                string sql = "SELECT DISTINCT bill_code "
                    + "FROM MS_SFJIT_RESULT_SL WHERE gzdd='" + cmbPline.Value.ToString() + "' and  to_date(TO_CHAR(REQUIRE_TIME,'yyyy-mm-dd'),'yyyy-mm-dd')=to_date('" + datePlanDate.Text
                    + "','yyyy-mm-dd') order by bill_code";
                cmbPlan.DataSource = dc.GetTable(sql);
                cmbPlan.TextField = "bill_code";
                cmbPlan.ValueField = "bill_code";
                cmbPlan.DataBind();
            }
            catch { }
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string MachineName = Request.UserHostAddress;
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;

            //删除原来记录
            string sql = "DELETE FROM RST_QAD_BOMPART_LQ_TEMP where ABOM_USER='" + MachineName + "'";
            dc.ExeSql(sql);

            //批量生成
            if (listPlan.Items.Count > 0)
            {
                for (int i = 0; i < listPlan.Items.Count; i++)
                {
                    string str = listPlan.Items[i].Text;
                    BomReplaceFactory.QAD_CREATE_PRTPART_LQ(str, MachineName);
                }
            }

            sql = "select ABOM_JHDM ,ABOM_COMP ,ABOM_DESC ,ABOM_WKCTR ,ABOM_QTY ,SJ_FLAG  from RST_QAD_BOMPART_LQ where ABOM_USER='" + MachineName + "' order by SJ_FLAG DESC,ABOM_JHDM,ABOM_WKCTR";
            Session["mmsMaterialSend001"] = sql;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            string sql = Session["mmsMaterialSend001"] as string;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
            ASPxGridViewExporter1.WriteXlsToResponse();
        }
    }
}