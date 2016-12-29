using System;
using System.Net;
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

/**
 * 功能概述：生产完成统计
 * 作者：游晓航
 * 创建时间：2016-09-14
 */
namespace Rmes.WebApp.Rmes.Inv.inv8100
{
    public partial class inv8100 : BasePage
    {

        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        //public DateTime theBeginDate, theEndDate;
        public string theCompanyCode;
        private string theUserId, theUserCode, MachineName, MachineName2, MachineName3, MachineName4;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "inv8100";
            //MachineName = System.Net.Dns.GetHostName();
            
            //MachineName = Page.Request.UserHostName;

            //MachineName = System.Web.HttpContext.Current.Request.UserHostName;
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            

            //MachineName2 = System.Web.HttpContext.Current.Request.UserHostName + "BBB";
            //MachineName2 += "BBB";
             
            
            //MachineName4 = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
           

            
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
            DataTable dt=dc.GetTable(sql);
            string sql2 = "";
            if(dt.Rows.Count>0)
            {
                sql2 = "   select STATION_CODE,STATION_NAME from vw_CODE_STATION where pline_code= '" + dt.Rows[0][0].ToString() + "' order by STATION_NAME";
            }
            else
            {
                sql2 = "   select STATION_CODE,STATION_NAME from vw_CODE_STATION  order by STATION_NAME";
            }
            StationCode.SelectCommand = sql2;
            StationCode.DataBind();
            //txtSCode.SelectedIndex = txtSCode.Items.Count >= 0 ? 0 : -1;

        }
        private void setCondition()
        {

            string FUNC1 = "", BC1, ZD1, GZDD1 = "", MACHINENAME1 = "", check = "", rq1 = "", rq2 = "";
            //if (txtSCode.Text.Trim() == "") return;
             
            if (txtChose.Text.Trim() != "")
            {
                FUNC1 = txtChose.Value.ToString();
            }
            if (FUNC1 == "BC")
            {
                rq1 = ASPxDateEdit1.Text.Trim();
                rq2 = ASPxDateEdit1.Text.Trim();
            }
            else
            {
                rq1 = ASPxDateEdit1.Text.Trim();
                rq2 = ASPxDateEdit2.Text.Trim();
            }
            check = chkJX.Value.ToString();
            BC1 = txtBc.Text.Trim();
            ZD1 = txtSCode.Text.Trim();
            if (txtPCode.Text.Trim() != "")
            {
                GZDD1 = txtPCode.Value.ToString();
            }
            MACHINENAME1 = MachineName;
            //if (check == "False")
             
                //ASPxGridView1.ClientVisible = true;
                //ASPxGridView2.ClientVisible = false;
                MW_CREATE_RSTZZPTJ_NEW sp = new  MW_CREATE_RSTZZPTJ_NEW()
                {
                    FUNC1=FUNC1,
                    RQ1 = ASPxDateEdit1.Date,
                    RQ2 = ASPxDateEdit2.Date,
                    BCMC1=BC1,
                    ZDMC1=ZD1,
                    MACHINENAME1=MachineName
                 
                };
                Procedure.run(sp);
                 
             
                string ChSql = "SELECT zdmc 站点名称,JHDM , SO PLAN_SO,jhsl 计划数量,wcsl 完成数量 FROM RSTZZPTJ  WHERE MachineNAME='" + MachineName + "'";

                DataTable Chdt = dc.GetTable(ChSql);
               
                ASPxGridView1.DataSource = Chdt;
                ASPxGridView1.DataBind();

                string Chsql2 = "";
                if (FUNC1 == "BC")
                {
                    if (BC1 == "全部")
                    {
                        Chsql2 = "select STATION_NAME 站点名称,PLAN_SO, count(distinct SN) 完成数量 from VW_DATA_COMPLETE where STATION_NAME='" + ZD1 + "' and WORK_DATE=to_date('" + rq1 + "','yyyy-mm-dd hh24:mi:ss') group by STATION_NAME,PLAN_SO ";
                    }
                    else
                    {
                        Chsql2 = "select STATION_NAME 站点名称,PLAN_SO ,count(distinct SN) 完成数量 from VW_DATA_COMPLETE where STATION_NAME='" + ZD1 + "' and WORK_DATE=to_date('" + rq1 + "','yyyy-mm-dd hh24:mi:ss') and shift_name='" + BC1 + "'  group by STATION_NAME,PLAN_SO ";
                    }
                }
                else
                {
                    Chsql2 = "select STATION_NAME 站点名称,PLAN_SO ,count(distinct SN) 完成数量 from VW_DATA_COMPLETE where STATION_NAME='" + ZD1 + "' and WORK_DATE>=to_date('" + rq1 + "','yyyy-mm-dd hh24:mi:ss') and WORK_DATE<=to_date('" + rq2 + "','yyyy-mm-dd hh24:mi:ss') group by STATION_NAME,PLAN_SO  ";

                }
                DataTable dt2 = dc.GetTable(Chsql2);
                
                ASPxGridView2.DataSource = dt2;
                ASPxGridView2.DataBind();
         

        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            
            setCondition();
           

        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (txtSCode.Text.Trim() == "") return;
            setCondition();
            
        }

        protected void txtSCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string sql2 = "";
            {
                sql2 = "   select STATION_CODE,STATION_NAME from vw_CODE_STATION where pline_code= '" + e.Parameter + "' order by STATION_NAME";
            }
            StationCode.SelectCommand = sql2;
            StationCode.DataBind();
            txtSCode.DataBind();
        }

         

    }
}