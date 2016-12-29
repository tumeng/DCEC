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
 * 功能概述：ECM数据查询
 * 作者：游晓航
 * 创建时间：2016-09-13
 */
namespace Rmes.WebApp.Rmes.Rept.rept1900
{
    public partial class rept1900 : BasePage
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
            theProgramCode = "rept1900";
            //MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
                 
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
            

        }
        private void setCondition()
        {
            string sn = "", so="", ecmcode="";
            sn = txtSN.Text.Trim();
            so = txtSO.Text.Trim();
            ecmcode = txtECM.Text.Trim();

            string sql = "select ghtm 流水号,so,to_char(insitetime,'yyyy-mm-dd hh24:mi:ss') 操作时间,czy 操作员,ecmcode ECM,bd 标定,sqd 申请单,sl 数量,yhmc 客户名称,gzqy 工作区域  from vw_atpuecmlsh where 1=1";
            if (sn != "")
            {
                sql = sql + " and GHTM like '%" + sn + "%'";
            }
            if (so != "")
            {
                sql = sql + " and SO like '%" + so + "%'";
            }
            if (ecmcode != "")
            {
                sql = sql + " and ECMCODE like '%" + ecmcode + "%'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " AND INSITETIME >= TO_DATE('" + ASPxDateEdit1.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql = sql + " AND INSITETIME <= TO_DATE('" + ASPxDateEdit2.Text + "','YYYY-MM-DD HH24:MI:SS') ";
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
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("标定查询");
        }
    }
}