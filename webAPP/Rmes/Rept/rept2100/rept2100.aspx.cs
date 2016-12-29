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
 * 功能概述：装配BOM对比
 * 作者：游晓航
 * 创建时间：2016-09-15
 */
public partial class Rmes_Rept_rept2100_rept2100 : Rmes.Web.Base.BasePage
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
        theProgramCode = "rept2100";
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        if (!IsPostBack)
        {

            txtChose.SelectedIndex = 0;

        }
        //initCode();
        setCondition();

    }
    //private void initCode()
    //{
    //    //初始化下拉列表
    //    string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
    //    SqlCode.SelectCommand = sql;
    //    SqlCode.DataBind();
    //    txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;


    //}
    private void setCondition()
    {
        string  theCondition = "",chose="";
        
            chose = txtChose.Value.ToString();
            if (chose == "A")
            {
                
                theCondition = " where ghtm in('" + txt1.Text.Trim() + "','" + txt2.Text.Trim() + "') and one_flag='A' order by comp_flag,ghtm,abom_comp,abom_op,abom_qty ";

            }
            if (chose == "B")
            {
                
                theCondition = " where abom_jhdm in('" + txt1.Text.Trim() + "','" + txt2.Text.Trim() + "') and one_flag='B' order by comp_flag,abom_jhdm,abom_comp,abom_op,abom_qty";

            }
            string Chsql = "select  * from rst_one_bom_comp " + theCondition;

            DataTable Chdt = dc.GetTable(Chsql);
            ASPxGridView1.DataSource = Chdt;
            ASPxGridView1.DataBind();


        
    }


    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (txtChose.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "请选择对比方式！");
            return;

        }
        else if (txt1.Text.Trim() == "" || txt2.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "对比项目不能为空！");
            return;
        }
        else
        {
            string chose = "", jhdm1 = "", jhdm2 = "";
            chose = txtChose.Value.ToString();
            if (chose == "A")
            {
                string sql = "select plan_code from data_product where sn='" + txt1.Text.Trim() + "'";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "项目一输入的流水号非法,请重新输入！");
                    return;
                }
                else { jhdm1 = dt.Rows[0][0].ToString(); }
                string sql2 = "select plan_code from data_product where sn='" + txt2.Text.Trim() + "'";
                DataTable dt2 = dc.GetTable(sql2);
                if (dt2.Rows.Count <= 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "项目二输入的流水号非法,请重新输入！");
                    return;
                }
                else { jhdm2 = dt2.Rows[0][0].ToString(); }
                MW_COMPARE_ONE_BOM sp = new MW_COMPARE_ONE_BOM()
                {
                    GHTM1 = txt1.Text.Trim(),
                    GHTM2 = txt2.Text.Trim(),
                    JHDM1 = jhdm1,
                    JHDM2 = jhdm2,
                    ONEFLAG1 = "A"

                };
                Rmes.DA.Base.Procedure.run(sp);

            }
            if (chose == "B")
            {
                string sql = "select plan_code from data_plan where plan_code='" + txt2.Text.Trim() + "'";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "项目一输入的计划号非法,请重新输入！");
                    return;
                }
                string sql2 = "select plan_code from data_plan where plan_code='" + txt2.Text.Trim() + "'";
                DataTable dt2 = dc.GetTable(sql2);
                if (dt2.Rows.Count <= 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "项目二输入的计划号非法,请重新输入！");
                    return;
                }
                MW_COMPARE_ONE_BOM sp = new MW_COMPARE_ONE_BOM()
                 {
                     GHTM1 = "",
                     GHTM2 = "",
                     JHDM1 = txt1.Text.Trim(),
                     JHDM2 = txt2.Text.Trim(),
                     ONEFLAG1 = "B"

                 };
                Rmes.DA.Base.Procedure.run(sp);

            }
        }
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }




}

