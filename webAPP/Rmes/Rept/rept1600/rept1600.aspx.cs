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
 * 功能概述：装配零件批次查询
 * 作者：游晓航
 * 创建时间：2016-09-11
 */

public partial class Rmes_Rept_rept1600_rept1600 : Rmes.Web.Base.BasePage
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
        theProgramCode = "rept1600";
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        string sql2 = "select STATION_CODE,STATION_NAME from CODE_STATION order by STATION_NAME";
        StationCode.SelectCommand = sql2;
        StationCode.DataBind();
        setCondition(); 
    }
    
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("装配零件批次信息导出");
    }
    private void setCondition()
    {
        if (txtSN.Text.Trim() != "" || ASPxDateEdit1.Text.Trim() != "" || ASPxDateEdit2.Text.Trim() != "" || txtItem.Text.Trim() != "" || txtGYS.Text.Trim() != "" || txtPC.Text.Trim() != "" || txtSCode.Text.Trim() != "")
        {
            string sql = "select c.sn,c.item_code,c.item_vendor,c.item_sn,c.create_time,c.station_name,b.user_name FROM data_scan_item c left join code_user b on c.create_userid=b.user_code  where   machinename='" + MachineName + "' ";
            if (txtSN.Text.Trim() != "")
            {
                sql = sql + " and sn='" + txtSN.Text.Trim().ToUpper() + "'";
            }
            if (txtItem.Text.Trim() != "")
            {
                sql = sql + " and item_code='" + txtItem.Text.Trim().ToUpper() + "'";
            }
            if (txtGYS.Text.Trim() != "")
            {
                sql = sql + " and item_vendor='" + txtGYS.Text.Trim() + "'";
            }
            if (txtPC.Text.Trim() != "")
            {
                sql = sql + " and item_sn='" + txtPC.Text.Trim() + "'";
            }
            if (txtSCode.Text.Trim() != "")
            {
                sql += "AND STATION_NAME= '" + txtSCode.Text.Trim() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";

            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
            }
            sql += "order by sn,item_code ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (txtSN.Text.Trim() == "" && ASPxDateEdit1.Text.Trim() == "" && ASPxDateEdit2.Text.Trim() == "" && txtItem.Text.Trim() == "" && txtGYS.Text.Trim() == "" && txtPC.Text.Trim() == "" && txtSCode.Text.Trim() != "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "请指定查询条件！");
            return;
        }
        else
        { string sql = "" ,sn="";
          //string flag = dc.GetValue("select is_ENGoffline('" + txtSN.Text.Trim() + "') from dual");
            //if (flag == "0")//未下线
            //{
              
            //}
            //else { sql = "select  a.* ,b.user_name from  VW_DATA_SN_BOM  a  left join code_user b on a.create_userid=b.user_code where 1=1 "; }
            sql = "(select  distinct a.sn  from DATA_SN_BOM a  where 1=1 ";

            if (txtSN.Text.Trim() != "")
            {
                sql = sql + " and sn='" + txtSN.Text.Trim().ToUpper() + "'";
            }
            if (txtItem.Text.Trim() != "")
            {
                sql = sql + " and item_code='" + txtItem.Text.Trim().ToUpper() + "'";
            }
            if (txtGYS.Text.Trim() != "")
            {
                sql = sql + " and vendor_code='" + txtGYS.Text.Trim() + "'";
            }
            if (txtPC.Text.Trim() != "")
            {
                sql = sql + " and item_batch='" + txtPC.Text.Trim() + "'";
            }
            if (txtSCode.Text.Trim() != "")
            {
                sql += "AND STATION_NAME= '" + txtSCode.Text.Trim() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
 
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";
            }
            sql = sql + ") union ";
            sql = sql + "(select distinct  c.sn  from DATA_SN_BOM_TEMP c  where 1=1 ";

            if (txtSN.Text.Trim() != "")
            {
                sql = sql + " and sn='" + txtSN.Text.Trim().ToUpper() + "'";
            }
            if (txtItem.Text.Trim() != "")
            {
                sql = sql + " and item_code='" + txtItem.Text.Trim().ToUpper() + "'";
            }
            if (txtGYS.Text.Trim() != "")
            {
                sql = sql + " and vendor_code='" + txtGYS.Text.Trim() + "'";
            }
            if (txtPC.Text.Trim() != "")
            {
                sql = sql + " and item_batch='" + txtPC.Text.Trim() + "'";
            }
            if (txtSCode.Text.Trim() != "")
            {
                sql += "AND STATION_NAME= '" + txtSCode.Text.Trim() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";

            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql = sql + " and CREATE_TIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')";

            }
            sql = sql + " ) ORDER BY SN ";
            DataTable dt = dc.GetTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0) { dc.ExeSql("delete from DATA_SCAN_ITEM where machinename='" + MachineName + "'"); }
                sn = dt.Rows[i][0].ToString();
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
        ASPxGridView1.Selection.UnselectAll();

    }

}
