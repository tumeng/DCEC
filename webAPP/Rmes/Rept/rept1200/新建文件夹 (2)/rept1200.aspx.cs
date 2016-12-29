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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Net;
/**
 * 功能概述：产品制造档案
 * 作者：游晓航
 * 创建时间：2016-09-09
 */
namespace Rmes.WebApp.Rmes.Rept.rept1200
{
    public partial class rept1200 : BasePage
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
            theProgramCode = "rept1200";
            MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostInfo.HostName;
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
                txtChose.SelectedIndex = 0;
                txtBc.SelectedIndex = 0;
               
            }
            initCode();
            setCondition();

        }
        private void initCode()
        {
            //初始化下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
            string sql2 = "";
            sql2 = "  select 'All'  as station_code,'全部' as station_name from code_station union select STATION_CODE,STATION_NAME from CODE_STATION order by STATION_NAME";
            StationCode.SelectCommand = sql2;
            StationCode.DataBind();
            //txtSCode.SelectedIndex = txtSCode.Items.Count >= 0 ? 0 : -1;

        }
        private void setCondition()
        {
          
            //初始化界面时如何让他不显示，还有建立索引
            if (txtPCode.Text.Trim() != "")
            {
                string Chsql = "select  * from rstzzpqd where MachineNAME='" + MachineName + "'  ORDER BY zdmc, RQSJ ";

                DataTable dt = dc.GetTable(Chsql);
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }

        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string FUNC1 = "", BC1, ZD1, GZDD1 = "", MACHINENAME1;
            if (txtChose.Text.Trim() != "")
            {
                FUNC1 = txtChose.Value.ToString();
            }

            BC1 = txtBc.Text.Trim();
            ZD1 = txtSCode.Text.Trim();
            if (txtPCode.Text.Trim() != "")
            {
                GZDD1 = txtPCode.Value.ToString();
            }
            MACHINENAME1 = MachineName;
            string sql = "DELETE FROM RSTZZPQD WHERE MACHINENAME='" + MachineName + "'";
            dc.ExeSql(sql);
            string InSql = "INSERT INTO RSTZZPQD (GHTM,SO,RQSJ,GZRQ,ZDMC,GGXHMC,YGMC,BCMC,BZMC,MACHINENAME,JHDM) "
            + " SELECT a.SN,a.PLAN_SO,a.START_TIME,a.WORK_DATE,c.STATION_NAME,a.PRODUCT_MODEL,b.user_name,d.shift_name,e.team_name,'" + MachineName + "',a.PLAN_CODE "
            + " FROM DATA_COMPLETE a left join code_user b on a.user_id=b.user_code left join code_station c on c.station_code=a.station_code left join code_shift d on a.shift_code=d.shift_code left join code_team e on e.team_code=a.team_code WHERE 1=1 ";
            if (FUNC1 == "BC")
            {
                if (BC1 == "全部" || BC1 == "")
                {
                    InSql = InSql + " and WORK_DATE=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') ";
                }
                else
                {
                    InSql = InSql + " and WORK_DATE=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and d.shift_name='" + BC1 + "'";
                }
                if (ZD1 == "全部" || ZD1 == "")
                {
                    InSql = InSql + " and a.pline_code='" + GZDD1 + "' ";
                }
                else
                {
                    InSql = InSql + " and a.pline_code='" + GZDD1 + "' and c.STATION_NAME='" + ZD1 + "' ";
                }


            }
            else
            {
                InSql = InSql + " and WORK_DATE>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and WORK_DATE<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') ";
                if (ZD1 == "全部" || ZD1 == "")
                {
                    InSql = InSql + " and a.pline_code='" + GZDD1 + "' ";
                }
                else
                {
                    InSql = InSql + " and a.pline_code='" + GZDD1 + "' and c.STATION_NAME='" + ZD1 + "' ";
                }
            }
            dc.ExeSql(InSql);

            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("产品制造档案信息导出");
        }
        protected void cmbScode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //
            //string pline = txtPCode.Value.ToString();
            string sql2 = "select a.STATION_CODE,a.STATION_NAME from CODE_STATION a where a.pline_code=(select t.rmes_id from Code_Product_Line t WHERE t.pline_code='" + e.Parameter + "') order by a.STATION_NAME";
            StationCode.SelectCommand = sql2;
            StationCode.DataBind();
            txtSCode.DataBind();
        }
    }
}