using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
/**
 * 功能概述：实际装机清单查询
 * 作者：游晓航
 * 创建时间：2016-09-08
 */

public partial class Rmes_Rept_rept1100_rept1100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, MachineName;
    private string theUserId;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "rept1100";
        MachineName = System.Net.Dns.GetHostName();
        if (!IsPostBack)
        {
            initCode();
           
        }

        setCondition();
    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
        //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        string sql2 = "select STATION_CODE,STATION_NAME from CODE_STATION where pline_code in(select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "') order by STATION_NAME";
        StationCode.SelectCommand = sql2;
        StationCode.DataBind();
        
    }


    private void setCondition()
    {
        string sn1 = txtLSH1.Text.Trim();
        string sn2 = txtLSH2.Text.Trim();
        string ZD1 = txtSCode.Text.Trim();
        if (txtLSH1.Text.Trim() != "" && txtLSH2.Text.Trim() != "")
        {
            int lsh1 = Convert.ToInt32(txtLSH1.Text.Trim());
            int lsh2 = Convert.ToInt32(txtLSH2.Text.Trim());

            string Chsql1 = "select  * from DATA_PRODUCT where SN='" + txtLSH1.Text.Trim() + "'  ";
            DataTable Chdt1 = dc.GetTable(Chsql1);
            if (Chdt1.Rows.Count <= 0)
            {
                sn1 = "";
                sn2 = "";

            }
            string Chsql2 = "select  * from DATA_PRODUCT where SN='" + txtLSH2.Text.Trim() + "'  ";
            DataTable Chdt2 = dc.GetTable(Chsql2);
            if (Chdt2.Rows.Count <= 0)
            {
                sn1 = "";
                sn2 = "";

            }
            if (lsh1 > lsh2)
            {
                sn1 = "";
                sn2 = "";

            }
        }
        else
        {
            sn1 = "";
            sn2 = "";
        }
        if (ZD1 != "")
        {
            string sql = " select * from DATA_SCAN_ITEM where STATION_NAME='" + ZD1 + "' AND SN>='" + sn1 + "' and SN <='" + sn2 + "' and machinename='"+MachineName+"' order by SN";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            string sql2 = " select  ITEM_CODE ,ITEM_NAME ,sum(ITEM_QTY) as QTY from DATA_SCAN_ITEM where STATION_NAME='" + ZD1 + "' AND SN>='" + sn1 + "' and SN <='" + sn2 + "' and machinename='" + MachineName + "' group by ITEM_CODE,ITEM_NAME";
            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }
        else
        {

            string sql = " select * from DATA_SCAN_ITEM where SN>='" + sn1 + "' and SN <='" + sn2 + "'  and machinename='" + MachineName + "' order by SN";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            string sql2 = " select  ITEM_CODE ,ITEM_NAME ,sum(ITEM_QTY) as QTY from DATA_SCAN_ITEM where SN>='" + sn1 + "' and SN <='" + sn2 + "' and machinename='" + MachineName + "' group by ITEM_CODE,ITEM_NAME";
            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }

    }
    //查询

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {   
        if (txtLSH1.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "起始流水号不能为空！");
            return;
        }
        else  if (txtLSH2.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "截止流水号不能为空！");
            return;
        }

        int lsh1 = Convert.ToInt32(txtLSH1.Text.Trim());
        int lsh2 = Convert.ToInt32(txtLSH2.Text.Trim());

        string Chsql1 = "select  * from DATA_PRODUCT where SN='" + txtLSH1.Text.Trim() + "'  ";
        DataTable Chdt1 = dc.GetTable(Chsql1);


        if (Chdt1.Rows.Count <= 0)
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "起始流水号不存在，请您重新输入！");
            return;
        }
        string Chsql2 = "select  * from DATA_PRODUCT where SN='" + txtLSH2.Text.Trim() + "'  ";
        DataTable Chdt2 = dc.GetTable(Chsql2);
        if (Chdt2.Rows.Count <= 0)
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "截止流水号不存在，请您重新输入！");
            return;
        }
        if (lsh1 > lsh2)
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "起始流水号应小于截止流水号！");
            return;
        }
        string ZD1 = txtSCode.Text.Trim();
        string sql = "",sn="";
        if (ZD1 != "")
        {
             sql = " select sn from DATA_SN_BOM where STATION_NAME='" + ZD1 + "' AND SN>='" + txtLSH1.Text.Trim() + "' and SN <='" + txtLSH2.Text.Trim() + "'  "
                 + "union select sn from DATA_SN_BOM_TEMP where STATION_NAME='" + ZD1 + "' AND SN>='" + txtLSH1.Text.Trim() + "' and SN <='" + txtLSH2.Text.Trim() + "'order by SN";
        }
        else
        {
            sql = " select sn from DATA_SN_BOM where SN>='" + txtLSH1.Text.Trim() + "' and SN <='" + txtLSH2.Text.Trim() + "' "
                + " union select sn from DATA_SN_BOM where SN>='" + txtLSH1.Text.Trim() + "' and SN <='" + txtLSH2.Text.Trim() + "'order by SN";         
        }
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
            setCondition();
        ASPxGridView1.Selection.UnselectAll();
    

    }

    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
         
    
        setCondition();
        ASPxGridView2.Selection.UnselectAll();

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
