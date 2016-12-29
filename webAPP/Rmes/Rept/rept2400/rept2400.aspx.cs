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
using DevExpress.Utils;
using System.Net;
/**
 * 功能概述：改制扫描确认
 * 作者：游晓航
 * 创建时间：2016-09-11
 */

public partial class Rmes_Rept_rept2400_rept2400 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();
    //public DateTime theBeginDate, theEndDate;
    public string theCompanyCode;
    private string theUserId, MachineName;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "rept2400";
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        initCode();

        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now.AddDays(-1);
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
             
        }
        setCondition();

    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
        SqlCode2.SelectCommand = sql;
        SqlCode2.DataBind();
    }

    private void setCondition()
    {
        string sql = "";
        if (txtFlag.Text.Trim() == "") return;
        else
        {

            sql = "SELECT  distinct A.* from DATA_SN_DETECT_DATA_TEMP A LEFT JOIN DATA_SN_DETECT_QUERY B ON A.SN=B.SN and A.PLAN_CODE=B.PLAN_CODE where B.DETECT_flag='" + txtFlag.Value.ToString() + "'";

            //sql = "SELECT  * from DATA_SN_DETECT_DATA_TEMP where query_flag='" + txtFlag.Value.ToString() + "' ";

            if (txtPCode.Text.Trim() == "")
            {
                sql = sql + " and  A.pline_code in (select distinct a.pline_code from vw_user_role_program a where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "')";
            }
            else
            {
                sql = sql + " and A.pline_code='" + txtPCode.Value.ToString() + "'";
            }

            if (txtSN.Text.Trim() != "")
            {
                sql = sql + " and A.SN='" + txtSN.Text.Trim() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + "  AND A.WORK_TIME >= TO_DATE('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS')";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql = sql + "  AND A.WORK_TIME <= TO_DATE('" + ASPxDateEdit2.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS')";
            }
            //if (txtFlag.Text.Trim() != "")
            //{
            //    sql = sql + " and detect_flag='" + txtFlag.Value.ToString() + "'";
            //}

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

            string sql2 = "SELECT distinct A.* from DATA_SN_BOM_TEMP  A LEFT JOIN DATA_SN_DETECT_QUERY B ON A.SN=B.SN and A.PLAN_CODE=B.PLAN_CODE where B.SCAN_flag='" + txtFlag.Value.ToString() + "'  ";

            if (txtPCode.Text.Trim() == "")
            {
                sql2 = sql2 + " and  A.pline_code in (select distinct a.pline_code from vw_user_role_program a where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "')";
            }
            else
            {
                sql2 = sql2 + " and A.pline_code='" + txtPCode.Value.ToString() + "'";
            }

            if (txtSN.Text.Trim() != "")
            {
                sql2 = sql2 + " and A.SN='" + txtSN.Text.Trim() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql2 = sql2 + "  AND A.CREATE_TIME >= TO_DATE('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS')";
            }
            if (ASPxDateEdit2.Text.Trim() != "")
            {
                sql2 = sql2 + "  AND A.CREATE_TIME <= TO_DATE('" + ASPxDateEdit2.Text.Trim() + "','YYYY-MM-DD HH24:MI:SS')";
            }


            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
    public void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        //string sql="select * from DATA_SN_DETECT_DATA_TEMP where "
        //string flag = grid.GetRowValues(e.VisibleIndex, "DETECT_FLAG") as string;
        string flag = "Y";
        if (txtFlag.Text.Trim() != "")
        {
            if (txtFlag.Value.ToString() == "N")
            { flag = "N"; }
            else
            {
                flag = "Y"; 
            }
        }
        switch (e.ButtonID)
        {
            case "Treated":
                if (e.CellType == GridViewTableCommandCellType.Filter) break;
                if (flag != "Y")
                    e.Visible = DefaultBoolean.False;
                   
                if (flag == "Y")
                    e.Visible = DefaultBoolean.True;
                 
                break;
            case "Untreated":
                if (e.CellType == GridViewTableCommandCellType.Filter) break;
                if (flag != "Y")
                    e.Visible = DefaultBoolean.True;
                if (flag == "Y")
                    e.Visible = DefaultBoolean.False;
                break;


        }
    }
    public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        string[] s1 = s.Split('|');
        string type1 = s1[0];
        int rowIndex = int.Parse(s1[1]);
        //string sn="",xmdm="",fdjxl="",xmmc="",RunValue="",sj1="",sj2="",zdmc="";
        ASPxGridView atl1 = (ASPxGridView)sender;
        int count1 = atl1.Selection.Count;
     
        List<object> aa = atl1.GetSelectedFieldValues("SN");

        string xmdm = ASPxGridView1.GetRowValues(rowIndex, "DETECT_CODE").ToString();
        string fdjxl = ASPxGridView1.GetRowValues(rowIndex, "PRODUCT_SERIES").ToString();
        string xmmc = ASPxGridView1.GetRowValues(rowIndex, "DETECT_NAME").ToString();
        string RunValue = ASPxGridView1.GetRowValues(rowIndex, "WORK_TIME").ToString();
        string sj1 = ASPxGridView1.GetRowValues(rowIndex, "DETECT_VALUE").ToString();
        string sj2 = ASPxGridView1.GetRowValues(rowIndex, "DETECT_VALUE").ToString();
        string zdmc = ASPxGridView1.GetRowValues(rowIndex, "STATION_NAME").ToString();
        string sn = ASPxGridView1.GetRowValues(rowIndex, "SN").ToString();
        string rmesid = ASPxGridView1.GetRowValues(rowIndex, "RMES_ID").ToString();
        switch (type1)
        {
            case "Treated":

                for (int i = 0; i < count1; i++)
                {
                    //取消确认

                    string sql7 = " update  DATA_SN_DETECT_QUERY set  USER_ID='" + theUserId + "',DETECT_TIME=SYSDATE, DETECT_FLAG='N' WHERE SN ='" + aa[i] + "'";
                    dc.ExeSql(sql7);
                }
                break;
            case "Untreated":

                for (int i = 0; i < count1; i++)
                {
                     //进行确认
                    string sql6 = "UPDATE DATA_SN_DETECT_QUERY SET USER_ID='" + theUserId + "',DETECT_TIME=SYSDATE,DETECT_FLAG='Y'  where SN =  '" + aa[i] + "' ";
                    dc.ExeSql(sql6);
                   
                }
                break;

            default:

                break;
        }
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");

    }
    public void ASPxGridView2_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
         
        string flag = "Y";
        if (txtFlag.Text.Trim() != "")
        {
            if (txtFlag.Value.ToString() == "N")
            { flag = "N"; }
            else
            {
                flag = "Y";
            }
        }
        switch (e.ButtonID)
        {
            case "Treated2":
                if (e.CellType == GridViewTableCommandCellType.Filter) break;
                if (flag != "Y")
                    e.Visible = DefaultBoolean.False;

                if (flag == "Y")
                    e.Visible = DefaultBoolean.True;
                
                break;
            case "Untreated2":
                if (e.CellType == GridViewTableCommandCellType.Filter) break;
                if (flag != "Y")
                    e.Visible = DefaultBoolean.True;
                if (flag == "Y")
                    e.Visible = DefaultBoolean.False;
                break;


        }
    }
    public void ASPxGridView2_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        string[] s1 = s.Split('|');
        string type1 = s1[0];
        int rowIndex = int.Parse(s1[1]);
         
        ASPxGridView atl1 = (ASPxGridView)sender;
        int count1 = atl1.Selection.Count;
        //string rmesid = ASPxGridView2.GetRowValues(rowIndex, "RMES_ID").ToString();
        List<object> aa = atl1.GetSelectedFieldValues("SN");
 
        switch (type1)
        {
            case "Treated2":
 
                for (int i = 0; i < count1; i++)
                {
                    //取消确认
                    string sql = " update  DATA_SN_DETECT_QUERY set  USER_ID='" + theUserId + "',SCAN_TIME=SYSDATE, SCAN_FLAG='N' WHERE SN ='" + aa[i] + "'";
                    dc.ExeSql(sql);
                    
                }
                break;
            case "Untreated2":

                for (int i = 0; i < count1; i++)
                {
                    //进行确认
                    string sql4 = " update  DATA_SN_DETECT_QUERY set  USER_ID='" + theUserId + "',SCAN_TIME=SYSDATE, SCAN_FLAG='Y' WHERE SN ='" + aa[i] + "'";
                    dc.ExeSql(sql4);
 

                }
                break;

            default:

                break;
        }
        ASPxGridView2.JSProperties.Add("cpCallbackName", "refresh");
        //string s = e.Parameters;
        //string[] s1 = s.Split('|');
        //string type1 = s1[0];
        //int rowIndex = int.Parse(s1[1]);
         
        //string rmesid = ASPxGridView2.GetRowValues(rowIndex, "RMES_ID").ToString();
        //switch (type1)
        //{
        //    case "Treated2":


        //        string sql = "update DATA_SN_BOM_TEMP  set CONFIRM_FLAG='N' where rmes_id='" + rmesid + "'";
        //        dc.ExeSql(sql);
        //        string sql2 = "delete from DATA_SN_BOM where  rmes_id =  '" + rmesid + "' ";

        //        dc.ExeSql(sql2);



        //        break;
        //    case "Untreated2":

        //        string sql4 = "insert into DATA_SN_BOM select * from DATA_SN_BOM_TEMP WHERE RMES_ID=  '" + rmesid + "'";
        //        dc.ExeSql(sql4);

        //        string sql5 = "delete from  DATA_SN_BOM_TEMP where rmes_id =  '" + rmesid + "' ";
        //        dc.ExeSql(sql5);

        //        break;

        //    default:

        //        break;
        //}

    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView2.Selection.UnselectAll();

    }

}
