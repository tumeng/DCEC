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
    public partial class mmsMaterialRequisition : System.Web.UI.Page
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
                + "where user_id='" + userId + "' and program_code='mmsReplaceRelationshipSet' and company_code='" + companyCode + "'";
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
            //根据生产线显示未完成计划
            string sql = "SELECT DISTINCT plan_code,plan_code||';'||plan_so||';'||PLAN_QTY plan_desc "
                + "FROM data_plan WHERE  BOM_FLAG='Y' and TO_CHAR(BEGIN_DATE,'yyyy-mm-dd')='" + datePlanDate.Text
                + "' and PLINE_CODE='" + cmbPline.Value.ToString() + "' order by plan_code";
            cmbPlan.DataSource = dc.GetTable(sql);
            cmbPlan.TextField = "plan_desc";
            cmbPlan.ValueField = "plan_code";
            cmbPlan.DataBind();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string MachineName = Request.UserHostAddress;
            string sql = "delete FROM rst_qad_prtpart where dyyh='" + MachineName + "'";
            dc.ExeSql(sql);

            //按日期生成
            if (cmbPlan.Text == "" && listPlan.Items.Count == 0)
            {
                sql = "SELECT DISTINCT plan_code,plan_so,plan_qty FROM data_plan WHERE TO_CHAR(begin_date,'YYYYMMDD')='" + datePlanDate.Text
                    + "'and run=1 and pline_code='" + cmbPline.Value.ToString() + "'";
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BomReplaceFactory.QAD_CREATE_PRTPART("CAL", cmbPline.Value.ToString(), dt.Rows[i]["plan_so"].ToString(),
                        dt.Rows[i]["plan_code"].ToString(), dt.Rows[i]["plan_qty"].ToString(), MachineName);
                }
            }
            //按计划生成
            if (cmbPlan.Text != "" && listPlan.Items.Count == 0)
            {
                string planNum = "1";
                if (txtQTY.Text != "")
                    planNum = txtQTY.Text;

                BomReplaceFactory.QAD_CREATE_PRTPART("ONE", cmbPline.Value.ToString(), txtSo.Text, cmbPlan.Value.ToString()
                    , planNum, MachineName);
            }


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

                    BomReplaceFactory.QAD_CREATE_PRTPART("CAL", cmbPline.Value.ToString(), so,
                        planCode, qty, MachineName);
                }
            }

            sql = "select ljbgy,ljdm,ljmc,ljgw,flgw ,ljdd ,SUM(ljsl) ljsl1,thljdm ,OLDLJDM  ,get_zdbygw('" + cmbPline.Value.ToString()
                + "',flgw) flgw1 ,bz ,ljgys  from rst_qad_prtpart  where 1=1 ";
            string sqlDetail = "select jhdm,so,ljbgy,ljdm,ljmc,ljgw,flgw ,ljdd ,ljsl,thljdm ,OLDLJDM  ,get_zdbygw('" + cmbPline.Value.ToString()
                + "',flgw) flgw1 ,bz ,ljgys  from rst_qad_prtpart  where 1=1 ";
            
            string condition="";
            if (txtStoreman.Text != "")
                condition += " and upper(ljbgy)>=upper('" + txtStoreman.Text + "') and upper(ljbgy)<=upper('" + txtStoreman2.Text + "') ";
            if (txtGwdm.Text != "")
                condition += " and upper(ljgw)=upper('" + txtGwdm.Text + "')";
            if (txtLj.Text != "")
                condition +=" and upper(ljdm)=upper('" + txtLj.Text + "')";
            if (cmbPlan.Text != "" && listPlan.Items.Count == 0 && CheckSn.Checked)
                condition += " and so='" + txtSo.Text + "' and jhdm='" + cmbPlan.Value.ToString() + "'";
            sql = sql + condition + " and gzdd='" + cmbPline.Value.ToString() + "' and dyyh='" + MachineName + "' GROUP BY ljbgy,ljdm ,ljmc ,ljgw ,flgw,ljdd ,ljgys ,thljdm ,oldljdm,bz  order by ljbgy,ljgw,flgw,ljdm";
            sqlDetail = sqlDetail + condition + " and gzdd='" + cmbPline.Value.ToString() + "' and dyyh='" + MachineName + "'  order by ljbgy,ljgw,flgw,ljdm";

            Session["RequisitionDetail"] = sqlDetail;
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse();
        }
    }
}