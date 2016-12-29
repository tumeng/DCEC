using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;
using System.IO;
//移库至柳汽库位

namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialMove
{
    public partial class mmsMaterialMoveLQ : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listPlan.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = listPlan.GetSelectedIndex();if(index!=-1) listPlan.RemoveItem(index);}";
                Session["mmsMaterialMoveLQ001"] = "";
            }
            if (Session["mmsMaterialMoveLQ001"] as string != "")
            {
                string sql = Session["mmsMaterialMoveLQ001"] as string;
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
                + "where user_id='" + userId + "' and program_code='mmsMaterialMoveLQ' and company_code='" + companyCode + "'";
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
                string sql = "SELECT DISTINCT BILL_CODE,BILL_ID "
                    + "FROM MS_SFJIT_RESULT_SL WHERE  to_date(TO_CHAR(REQUIRE_TIME,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + datePlanDate.Text
                    + "','yyyy-mm-dd') and GZDD='" + cmbPline.Value.ToString() + "' order by BILL_CODE";
                cmbPlan.DataSource = dc.GetTable(sql);
                cmbPlan.TextField = "BILL_CODE";
                cmbPlan.ValueField = "BILL_CODE";
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
            dc.ExeSql(sql);

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

                    BomReplaceFactory.QAD_CREATE_MOVEPART_LQ1(cmbPline.Value.ToString(), str);
                }
            }

            //sql = "SELECT ABOM_COMP  ,ABOM_WKCTR ,ABOM_KW ,SUM(ABOM_QTY) QTY1 from rst_qad_movepart WHERE abom_user='" + cmbPline.Value.ToString()
            //    + "' GROUP BY ABOM_COMP,ABOM_WKCTR,ABOM_KW ORDER BY ABOM_COMP,ABOM_WKCTR";
            sql = "SELECT ABOM_COMP  ,ABOM_WKCTR ,ABOM_KW ,SUM(ABOM_QTY) QTY1 from rst_qad_movepart GROUP BY ABOM_COMP,ABOM_WKCTR,ABOM_KW ORDER BY ABOM_COMP,ABOM_WKCTR";
            Session["mmsMaterialMoveLQ001"] = sql;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();

            sql = "update atpusysstate1 set bomrunning=0";
            dc.ExeSql(sql);
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            string sql = Session["mmsMaterialMoveLQ001"] as string;
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