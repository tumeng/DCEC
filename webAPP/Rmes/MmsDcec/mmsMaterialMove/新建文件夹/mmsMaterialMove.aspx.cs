using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;


namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialMove
{
    public partial class mmsMaterialMove : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listPlan.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = listPlan.GetSelectedIndex();if(index!=-1) listPlan.RemoveItem(index);}";
                Session["mmsMaterialMove001"] = "";
            }
            if (Session["mmsMaterialMove001"] as string != "")
            {
                string sql = Session["mmsMaterialMove001"] as string;
                ASPxGridView1.DataSource = dc.GetTable(sql);
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
                + "where user_id='" + userId + "' and program_code='mmsMaterialMove' and company_code='" + companyCode + "'";
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
                string sql = "SELECT DISTINCT plan_code,plan_code||';'||plan_so||';'||PLAN_QTY||';'||TO_CHAR(ACCOUNT_DATE,'yyyy-mm-dd') plan_desc "
                    + "FROM data_plan WHERE  to_date(TO_CHAR(BEGIN_DATE,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + datePlanDate.Text
                    + "','yyyy-mm-dd') and to_date(TO_CHAR(BEGIN_DATE,'yyyy-mm-dd'),'yyyy-mm-dd')<=to_date('" + datePlanDateTo.Text
                    + "','yyyy-mm-dd') and PLINE_CODE='" + cmbPline.Value.ToString() + "' order by plan_code";
                cmbPlan.DataSource = dc.GetTable(sql);
                cmbPlan.TextField = "plan_desc";
                cmbPlan.ValueField = "plan_code";
                cmbPlan.DataBind();
            }
            catch { }
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //判断是否有人在转换,如果是退出，提示随后再加
            string sql = "select bomrunning,bomuser from atpusysstate1";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count > 0) {
                if (dt.Rows[0][0].ToString() == "1")
                    return;
            }

            //加入转换用户
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();
            sql = "update atpusysstate1 set bomuser='" + userName + "',bomrunning=1";

            //删除原来记录
            sql = "DELETE FROM RST_QAD_MOVEPART";
            dc.ExeSql(sql);

            string MachineName = Request.UserHostAddress;

            //批量生成
            if (listPlan.Items.Count > 0)
            {
                for (int i = 0; i < listPlan.Items.Count; i++)
                {
                    string str = listPlan.Items[i].Text;
                    string[] strs = str.Split(';');
                    string planCode = strs[0].ToString();
                    string so = strs[1].ToString();
                    string qty = strs[2].ToString();

                    BomReplaceFactory.QAD_CREATE_MOVEPART(cmbPline.Value.ToString(), so, planCode, qty);
                }
            }
            string qadsite = dc.GetValue("select FUNC_GET_PLANSITE( '" + cmbPline.Value.ToString() + "', 'D') from dual");
            //sql = "SELECT ABOM_COMP  ,ABOM_WKCTR ,ABOM_KW ,SUM(ABOM_QTY) QTY1 from rst_qad_movepart WHERE abom_user='" + cmbPline.Value.ToString()
            //    + "' GROUP BY ABOM_COMP,ABOM_WKCTR,ABOM_KW ORDER BY ABOM_COMP,ABOM_WKCTR";
            sql = "SELECT ABOM_COMP  ,ABOM_WKCTR ,ABOM_KW ,SUM(ABOM_QTY) QTY1 from rst_qad_movepart WHERE gzdd='" + qadsite
                + "' GROUP BY ABOM_COMP,ABOM_WKCTR,ABOM_KW ORDER BY ABOM_COMP,ABOM_WKCTR";
            Session["mmsMaterialMove001"] = sql;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            string sql = Session["mmsMaterialMove001"] as string;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
            ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void datePlanDateTo_Init(object sender, EventArgs e)
        {
            datePlanDateTo.Date = System.DateTime.Now;
        }

    }
}