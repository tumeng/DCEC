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
 * 功能概述：单机装配BOM查询
 * 作者：游晓航
 * 创建时间：2016-09-12
 */

public partial class Rmes_Rept_rept1700_rept1700 : Rmes.Web.Base.BasePage
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
        theProgramCode = "rept1700";
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        if (!IsPostBack)
        {

            //txtChose.SelectedIndex = 0;

            //txtPlanCode.SelectedIndex = 0;
            setCondition();
        }
        setCondition();

        if (Request["opFlag"] == "getEditSeries")
        {
            string str1 = "", str2 = "";
            string sn = Request["SN"].ToString().Trim();

            string sql = "SELECT DISTINCT(product_model),plan_so,plan_code FROM DATA_PRODUCT where sn='" + sn + "'";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                string Pmodel = dt.Rows[0][0].ToString();
                string planso = dt.Rows[0][1].ToString();
                str1 = Pmodel + "@" + planso;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str2 = dt.Rows[i][2].ToString();
                str1 = str1 + "@" + str2;
            }
            txtPlanCode.SelectedIndex = 0;
            this.Response.Write(str1);
            this.Response.End();
        }

    }

    private void setCondition()
    {



        string chose = "";
        if (txtChose.Text.Trim() != "")
        {
            chose = txtChose.Value.ToString();



            string pmodel = "", so = "", plancode = "", plinecode = "", xl = "", scode = "", pos = "";
            switch (chose)
            {

                case "A":

                    string sql = "SELECT product_model,plan_so,plan_code,pline_code FROM DATA_PRODUCT where sn='" + txtSN.Text.Trim() + "'";
                    DataTable dt = dc.GetTable(sql);
                    if (dt.Rows.Count <= 0)
                    { return; }
                    else
                    {
                        pmodel = txtPModel.Text.Trim();
                        so = txtSO.Text.Trim();
                        plancode = txtPlanCode.Text.Trim();
                        plinecode = dt.Rows[0][3].ToString();
                        string sql2 = "select xl from copy_engine_property where so='" + so.ToUpper() + "' and rownum=1 ";
                        DataTable dt2 = dc.GetTable(sql2);
                        if (dt2.Rows.Count > 0)
                        {
                            xl = dt2.Rows[0][0].ToString();
                        }
                        if (plinecode == "E")
                        {
                            scode = "ZDE068";
                        }
                        if (plinecode == "W")
                        {
                            scode = "ZD910";
                        }
                        if (plinecode == "R")
                        {
                            scode = "RONE001";
                        }

                        //else
                        //{
                        //    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                        //    ASPxGridView1.JSProperties.Add("cpCallbackRet", "没有记录！");
                        //}
                        PL_QUERY_BOMZJTS3 sp = new PL_QUERY_BOMZJTS3()
                        {
                            SO1 = so,
                            ZDDM1 = scode,
                            GZDD1 = plinecode,
                            FDJXL1 = xl,
                            JHDM1 = plancode
                        };
                        Procedure.run(sp);
                        PL_UPDATE_BOMZJTS_CRM3 sp2 = new PL_UPDATE_BOMZJTS_CRM3()
                        {
                            SO1 = so,
                            ZDDM1 = scode,
                            JHDM1 = plancode,
                            GZDD1 = plinecode
                        };
                        Procedure.run(sp2);
                        PL_UPDATE_BOMLSHTS3 sp3 = new PL_UPDATE_BOMLSHTS3()
                        {
                            LSH1 = txtSN.Text.Trim(),
                            ZDDM1 = scode

                        };
                        Procedure.run(sp3);
                        PL_UPDATE_BOMSOTHTS3 sp4 = new PL_UPDATE_BOMSOTHTS3()
                        {
                            SO1 = so,
                            JHDM1 = plancode,
                            ZDDM1 = scode

                        };
                        Procedure.run(sp4);
                    }
                    string ChSql1 = "select gwmc 工位,comp 零件代码,udesc 描述,qty 数量,gxmc 工序,gysmc 供应商 from rstbomqd_CRM where zddm='" + scode + "' order by gwmc,gxmc";
                    DataTable dt1 = dc.GetTable(ChSql1);
                    ASPxGridView1.DataSource = dt1;
                    ASPxGridView1.DataBind();
                     
                    ASPxGridView2.DataSource=null;
                    ASPxGridView2.DataBind();

                    break;
                case "B":
                    //string sql22 = "SELECT ggxhmc,so,jhdm FROM SJSXB WHERE GHTM='" + txtSN.Text.Trim() + "'  ";
                    //DataTable dt22 = dc.GetTable(sql22);
                    //pmodel = dt22.Rows[0][0].ToString();
                    //so = dt22.Rows[0][1].ToString();
                    //plancode = dt22.Rows[0][2].ToString();
                    string ChSql2 = " SELECT item_code 零件代码,item_name 零件名称,item_qty 数量,process_code 工序,location_code 工位  FROM vw_data_plan_standard_bom  WHERE plan_code='" + txtPlanCode.Text.Trim() + "' AND plan_so='" + txtSO.Text.Trim() + "' ORDER BY location_code,process_code";
                    DataTable dt22 = dc.GetTable(ChSql2);
                    ASPxGridView1.DataSource = dt22;
                    ASPxGridView1.DataBind();
                    string Csql2 = "SELECT ljdm1,ljdm2,gwmc FROM sjbomsoth WHERE so='" + txtSO.Text.Trim() + "'and jhdm='" + txtPlanCode.Text.Trim() + "' and istrue=1 order by gwmc,ljdm1";
                    DataTable dt12 = dc.GetTable(Csql2);
                    ASPxGridView2.DataSource = dt12;
                    ASPxGridView2.DataBind();
                    break;
                case "C":
                    //string ChSql3 = "select LOCATION_CODE 工位,ITEM_CODE 零件代码,ITEM_NAME 零件名称,ITEM_CODE 数量,PROCESS_CODE 工序,STATION_CODE 站点,VENDOR_NAME 供应商 from VW_rstlshbomqd where SN='" + txtSN.Text.Trim() + "' order by LOCATION_CODE,PROCESS_CODE";
                    //DataTable dt3 = dc.GetTable(ChSql3);
                    //ASPxGridView1.DataSource = dt3;
                    //ASPxGridView1.DataBind();
                   
                    break;

                default:
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "查询方式有误！");
                    break;
            }

        }
    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (txtSN.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "请输入流水号！");
            return;
        }
        if (txtChose.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "请选择查询内容！");
            return;
        }

        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }

    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView2.Selection.UnselectAll();

    }

}
