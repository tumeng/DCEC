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
 * 功能概述：改制扫描日志查询
 * 作者：游晓航
 * 创建时间：2016-09-16
 */
namespace Rmes.WebApp.Rmes.Rept.rept2200
{
    public partial class rept2200 : BasePage
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
            theProgramCode = "rept2200";
            string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
            if (!IsPostBack)
            {

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
            }  
            setCondition();
        }
       
        private void setCondition()
        {
            string datetime = ASPxDateEdit2.Date.ToShortDateString() + " 23:59:59";
            string sql = "SELECT a.SN ,a.station_name ,a.create_time , b.user_name,a.pline_code ,a.item_code ,a.item_sn ,a.item_name ,a.item_vendor ,"
                         + " a.barcode   FROM data_scan_item a left join code_user b on a.CREATE_USERID=b.USER_CODE  ";

            if (txtSN.Text.Trim() != "")
            {
                string gzsql = "select a.plan_type from data_plan a left join data_plan_sn b on a.plan_code=b.plan_code  where b.sn='" + txtSN.Text.Trim() + "' ";
                string type = dc.GetValue(gzsql);
                if (type != "C" || type != "D")
                {
                    return;
                }
                sql = sql + " WHERE a.SN='" + txtSN.Text.Trim() + "' ";
            }
            else
            {
                sql = sql + "where a.CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and a.CREATE_TIME<=to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss')";
            }
             
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
           
            if (ASPxDateEdit1.Date.AddDays(31) < ASPxDateEdit2.Date)
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "选择日期范围不能超过30天，请重新选择！");
                return;   

            }
            //判断是否改制发动机

            if (txtSN.Text.Trim() != "")
            {
                string gzsql = "select a.plan_type from data_plan a left join data_plan_sn b on a.plan_code=b.plan_code  where b.sn='" + txtSN.Text.Trim() + "'";
                string  type = dc.GetValue(gzsql);
                if (type!= "C"||type!="D")
                {
                    //弹出提示框不是改制流水号
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "该流水号不是改制流水号，请您重新输入！");
                    
                    return;
                }
                //下边程序动态生成一个中间表，保存发动机的实际扫描零件清单
                dc.ExeSql("delete from DATA_SCAN_ITEM where machinename='" + MachineName + "'");
                PL_INSERT_SJZJQD sp = new PL_INSERT_SJZJQD()
                {
                    SN1 = txtSN.Text.Trim(),
                    MACHINENAME1=MachineName
                };
                Procedure.run(sp);
            }
            else
            {
                string datetime = ASPxDateEdit2.Date.ToShortDateString() + " 23:59:59";
                string sql = " select sn from DATA_SN_BOM where sn in( select b.sn from data_plan a left join data_plan_sn b on a.plan_code=b.plan_code  where (a.plan_type='C' or a.plan_type='D')) ";
               if (ASPxDateEdit1.Text.Trim() != "")
                {
                    sql = sql + " and CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                if (ASPxDateEdit2.Text.Trim() != "")
                {
                    sql = sql + " and CREATE_TIME<=to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss')";
                }
                sql = sql + "union select sn from DATA_SN_BOM_TEMP where sn in( select sn from data_record )";
                 if (ASPxDateEdit1.Text.Trim() != "")
                 {
                     sql = sql + " and CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                 }
                 if (ASPxDateEdit2.Text.Trim() != "")
                 {
                     sql = sql + " and CREATE_TIME<=to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss')";
                 }
                 sql = sql + " order by SN";
                 DataTable dt = dc.GetTable(sql);
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     //if (i == 0) { dc.ExeSql("delete from DATA_SCAN_ITEM where machinename='" + MachineName + "'"); }
                    string sn = dt.Rows[i][0].ToString();
                     //下边调用存储过程生成一个中间表，保存发动机的实际扫描零件清单
                     PL_INSERT_SJZJQD sp = new PL_INSERT_SJZJQD()
                     {
                         SN1 = sn,
                         MACHINENAME1 = MachineName

                     };
                     Procedure.run(sp);
 
                 }
 
            }
            setCondition();
          
        }
         
    }
}