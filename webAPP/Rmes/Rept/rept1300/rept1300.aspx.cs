﻿using System;
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
 * 功能概述：生产日报表
 * 作者：游晓航
 * 创建时间：2016-09-10
 */
public partial class Rmes_Rept_rept1300_rept1300 : Rmes.Web.Base.BasePage
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
        theProgramCode = "rept1300";
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now;//.AddDays(1);
            ASPxDateEdit3.Date = DateTime.Now;
            ASPxDateEdit4.Date = DateTime.Now;//.AddDays(1);
            ASPxDateEdit5.Date = DateTime.Now;
            ASPxDateEdit6.Date = DateTime.Now;//.AddDays(1);
            ASPxDateEdit7.Date = DateTime.Now;
            ASPxDateEdit8.Date = DateTime.Now;//.AddDays(1);
            //txtPCode.SelectedIndex = 0;
            
        }
        initCode();
        setCondition();


    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'"
                       +" and (A.pline_code='E' OR A.PLINE_CODE='W')";
        SqlCode1.SelectCommand = sql;
        SqlCode1.DataBind();
        SqlCode2.SelectCommand = sql;
        SqlCode2.DataBind();
        SqlCode3.SelectCommand = sql;
        SqlCode3.DataBind();
        SqlCode4.SelectCommand = sql;
        SqlCode4.DataBind();

    }
    
    private void setCondition()
    {
        string StationName = "";
        if (pageControl.ActiveTabIndex == 0)
        {
            //if (txtChose.Text.Trim() == "")
            //{
            //    return;
            //}
            //else
            //{
            

            //    chose = txtChose.Value.ToString();
            if (txtPCode.Text.Trim() != "")
            {
                if (txtPCode.Text.Trim() == "东区")
                {
                    StationName = "ZF200";
                }
                if (txtPCode.Text.Trim() == "西区")
                {
                    StationName = "ATPU-U860";
                }
                string datetime = ASPxDateEdit2.Date.ToShortDateString() + " 23:59:59";



                MW_RST_MonthBB sp = new MW_RST_MonthBB()
             {
                 ZDMC1 = StationName,
                 GZRQ1 = ASPxDateEdit1.Text.Trim(),
                 GZRQ2 = datetime,
                 MACHINENAME1 = MachineName

             };
                Procedure.run(sp);

                string ChSql1 = "select distinct jhdm 计划代码,so 计划SO,ggxhmc 机型,sl 计划数量 ,wcsl 完成数量 from rstdaybb where machinename='" + MachineName + "'order by jhdm ";
                DataTable dt1 = dc.GetTable(ChSql1);

                ASPxGridView1.DataSource = dt1;

                ASPxGridView1.DataBind();
            }
        }
        //int width = 40 / 5;
        // foreach (DevExpress.Web.ASPxGridView.GridViewColumn col in ASPxGridView1.VisibleColumns)
        // {  
        //     col.Width = Unit.Percentage(width);  
        //  } 
        //DataTable Table1 = new DataTable();
        //Table1.Columns.Add("计划代码");
        //Table1.Columns.Add("计划SO");
        //Table1.Columns.Add("机型");
        //Table1.Columns.Add("计划数量");
        //Table1.Columns.Add("完成数量");

        //for (int i = 0; i < dt1.Rows.Count; i++)
        //{
        //    string jhdm = dt1.Rows[i][0].ToString();
        //    string so = dt1.Rows[i][1].ToString();
        //    string ggxhmc = dt1.Rows[i][2].ToString();
        //    string sl = dt1.Rows[i][3].ToString();
        //    string wcsl = dt1.Rows[i][4].ToString();
        //    Table1.Rows.Add(jhdm, so, ggxhmc, sl, wcsl);
        //}

        //ASPxGridView1.DataSource = Table1;
        //ASPxGridView1.DataBind();
        if (pageControl.ActiveTabIndex == 1)
        {
            if (txtPCode2.Text.Trim() != "")
            {
                if (txtPCode2.Text.Trim() == "东区")
                {
                    StationName = "ZF200";
                }
                if (txtPCode2.Text.Trim() == "西区")
                {
                    StationName = "ATPU-U860";
                }
                string datetime = ASPxDateEdit4.Date.ToShortDateString() + " 23:59:59";
                string ChSql2 = " select WORK_DATE 工作日期, PLAN_SO 产品代码,count(distinct SN) 完成数量 from VW_DATA_COMPLETE a left join code_station b on a.station_code=b.station_code "
                + "where b.station_name='" + StationName + "'and WORK_DATE>=to_date('" + ASPxDateEdit3.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')" + " and WORK_DATE<=to_date('" + datetime + "','yyyy-mm-dd hh24:mi:ss') group by a.work_date ,a.plan_so";

                DataTable dt2 = dc.GetTable(ChSql2);
                ASPxGridView2.DataSource = dt2;
                ASPxGridView2.DataBind();
            }
        }
        if (pageControl.ActiveTabIndex == 2)
        {
            if (txtPCode3.Text.Trim() != "")
            {
                //if (txtPCode3.Text.Trim() == "东区")
                //{
                //    StationName = "ZF200";
                //}
                //if (txtPCode3.Text.Trim() == "西区")
                //{
                //    StationName = "ATPU-U860";
                //}
                string begintime = ASPxDateEdit5.Date.ToShortDateString() + " 00:00:00";
                string endtime = ASPxDateEdit6.Date.ToShortDateString() + " 23:59:59";
                DateTime Begin = Convert.ToDateTime(begintime);
                DateTime End = Convert.ToDateTime(endtime);
                MW_CREATE_ZDLSHTJ sp2 = new MW_CREATE_ZDLSHTJ()
        {

            GZRQ1 = Begin,
            GZRQ2 = End,
            GZDD1 = txtPCode3.Value.ToString()

        };
                Procedure.run(sp2);
                string ChSql3 = "SELECT GHTM 漏扫条码,ZDMC 工位号    FROM ATPUZDLSHQD ORDER BY GHTM";
                DataTable dt3 = dc.GetTable(ChSql3);

                ASPxGridView3.DataSource = dt3;
                ASPxGridView3.DataBind();
            }
        }
        if (pageControl.ActiveTabIndex == 3)
        {
            if (txtPCode4.Text.Trim() != "")
            {
                //if (txtPCode4.Text.Trim() == "东区")
                //{
                //    StationName = "ZF200";
                //}
                //if (txtPCode4.Text.Trim() == "西区")
                //{
                //    StationName = "ATPU-U860";
                //}
                //与原程序多调用了一次存储过程，因为分了两个页面查询
                string begintime = ASPxDateEdit7.Date.ToShortDateString() + " 00:00:00";
                string endtime = ASPxDateEdit8.Date.ToShortDateString() + " 23:59:59";
                DateTime Begin = Convert.ToDateTime(begintime);
                DateTime End = Convert.ToDateTime(endtime);
                MW_CREATE_ZDLSHTJ sp3 = new MW_CREATE_ZDLSHTJ()
        {

            GZRQ1 = Begin,
            GZRQ2 = End,
            GZDD1 = txtPCode4.Value.ToString()

        };
                Procedure.run(sp3);
                string ChSql4 = "SELECT ZDMC 工位,AA 漏扫数量 FROM (select ZDMC,ZDDM,COUNT(*) AA from ATPUZDLSHQD GROUP BY ZDMC,ZDDM ) ORDER BY ZDDM";
                DataTable dt4 = dc.GetTable(ChSql4);
                ASPxGridView4.DataSource = dt4;
                ASPxGridView4.DataBind();
            }
        }
        //ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
        //ASPxGridView1.JSProperties.Add("cpCallbackRet", "统计方式有误！");





    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();


    }
    protected void ASPxGridView3_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();


    }
    protected void ASPxGridView4_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("一般统计信息导出");
    }
    protected void btnXlsExport2_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter2.WriteXlsToResponse("按型号统计导出");
    }
    protected void btnXlsExport3_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter3.WriteXlsToResponse("统计工位漏扫导出");
    }
    protected void btnXlsExport4_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter4.WriteXlsToResponse("漏扫数量信息导出");
    }

     
     
}
