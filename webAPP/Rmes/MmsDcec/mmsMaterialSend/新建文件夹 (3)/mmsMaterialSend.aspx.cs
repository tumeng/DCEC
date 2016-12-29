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
            //逐行显示 YXH2016/12/17 21:15
            #region  逐行显示

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ABOM_JHDM");
            dt1.Columns.Add("ABOM_COMP");
            dt1.Columns.Add("ABOM_DESC");
            dt1.Columns.Add("ABOM_WKCTR");
            dt1.Columns.Add("ABOM_QTY");
            dt1.Columns.Add("SJ_FLAG");
            //批量生成
            if (listPlan.Items.Count > 0)
            {
                for (int i = 0; i < listPlan.Items.Count; i++)
                {
                    string str = listPlan.Items[i].Text;
                    BomReplaceFactory.QAD_CREATE_PRTPART_LQ(str, MachineName);
                    string sqlSo = "select t.plan_code,a.plan_so ABOM_COMP from ms_sfjit_plan_log t  left join data_plan a on a.plan_code=t.plan_code where t.SF_JIT_ID='" + str + "' and t.PLAN_CODE IN (SELECT plan_code FROM data_plan WHERE LQ_FLAG='Y') ";
                    DataTable dt = dc.GetTable(sqlSo);
                    if (dt.Rows.Count < 1)  return;
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        dt1.Rows.Add(dt.Rows[s][0], dt.Rows[s][1], "", "", "", "");

                        string sqlSn = "select sn  from data_product where plan_code='" + dt.Rows[s][0].ToString() + "'";
                        DataTable dt2 = dc.GetTable(sqlSn);
                        int integer = dt2.Rows.Count / 6;
                        
                        for (int n = 0; n <= integer; n++)
                        {   
                            DataRow dr2 = dt1.NewRow();
                            for (int m = 0; m<6; m++)
                            {
                                dr2[m] = dt2.Rows[m+n*6][0];
                               if(m>dt2.Rows.Count-2-6*n) break;  
                            }
                            dt1.Rows.Add(dr2);
                        }
                    }
                    string sqlDh = "select ABOM_JHDM ,ABOM_COMP ,ABOM_DESC ,ABOM_WKCTR ,ABOM_QTY ,SJ_FLAG  from RST_QAD_BOMPART_LQ where ABOM_USER='" + MachineName + "' order by SJ_FLAG DESC,ABOM_JHDM,ABOM_WKCTR";
                    DataTable dt3 = dc.GetTable(sqlDh);
                    if (dt3.Rows.Count < 1) return;
                    dt1.Rows.Add("MES单号", "零件代码", "零件名称", "工位","数量", "是否随机件");
                    for (int x = 0; x < dt3.Rows.Count; x++)
                    {
                        dt1.Rows.Add(dt3.Rows[x][0], dt3.Rows[x][1], dt3.Rows[x][2], dt3.Rows[x][3], dt3.Rows[x][4], dt3.Rows[x][5]);
                    }
                    
                }
            }
            
            #endregion
            Session["MaterialSend_YXH"] = dt1;
            ASPxGridView1.DataSource = dt1;
            ASPxGridView1.DataBind();

            
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable table1 = Session["MaterialSend_YXH"] as DataTable;
            ASPxGridView1.DataSource = table1;
            ASPxGridView1.DataBind();
            ASPxGridViewExporter1.WriteXlsToResponse("柳汽发料打印--导出");
            
        }
    }
}