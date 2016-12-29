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
/**
 * 功能概述：装配BOM对比
 * 作者：游晓航
 * 创建时间：2016-09-15
 */
public partial class Rmes_Rept_rept2500_rept2500 : Rmes.Web.Base.BasePage
{


    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();
    //public DateTime theBeginDate, theEndDate;
    public string theCompanyCode;
    private string theUserName, theUserCode, theUserId,MachineName;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserName = theUserManager.getUserName();
        theUserId = theUserManager.getUserId();
        theProgramCode = "rept2500";
        MachineName = System.Net.Dns.GetHostName();
        if (!IsPostBack)
        {



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
        string  Chsql = "";
        if (Plan1.Text.Trim() == "" || Plan2.Text.Trim() == "")
        {
            return;
        }
        else
        {
            if (chkProcess.Value.ToString() == "True")
            {

                Chsql = "select ABOM_COMP 零件,ABOM_DESC 名称,ABOM_QTY 数量,ABOM_OP 工序,ABOM_WKCTR 工位,ABOM_BOM1 BOM1中有,ABOM_BOM2 BOM2中有,ABOM_USER 比较用户 from rst_bom_compare where abom_user='" + theUserName + "'";


            }
            else
            {
                Chsql = "select abom_comp 零件,abom_desc 名称,sum(cc) 对比数量 from ( select abom_comp,abom_desc,-1*bb cc from (SELECT abom_comp,abom_desc,sum(abom_qty) bb from rst_bom_compare  where abom_user='" + theUserName + "' "
                        + " AND ABOM_BOM1='1' group by (abom_comp,abom_desc))  where (abom_comp,abom_desc,bb) not in(  SELECT abom_comp,abom_desc,sum(abom_qty) from rst_bom_compare "
                        + " where abom_user='" + theUserName + "' AND ABOM_BOM1='0' group by (abom_comp,abom_desc) ) Union  select abom_comp,abom_desc,cc from (SELECT abom_comp,abom_desc,sum(abom_qty) cc from rst_bom_compare "
                        + " where abom_user='" + theUserName + "' AND ABOM_BOM1='0' group by (abom_comp,abom_desc)) where (abom_comp,abom_desc,cc) not in (SELECT abom_comp,abom_desc,sum(abom_qty) from rst_bom_compare where abom_user='" + theUserName + "' AND ABOM_BOM1='1' group by (abom_comp,abom_desc)))group by abom_comp,abom_desc";
            }


            DataTable Chdt = dc.GetTable(Chsql);
            ASPxGridView1.DataSource = Chdt;
            ASPxGridView1.DataBind();
        }



    }


    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
          if (Plan1.Text.Trim() == "" || Plan2.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "对比计划号不能为空！");
            return;
        }
        else
        {
            string  so1 = "", so2 = "";
            string sql = "select plan_so from data_plan where plan_code='" + Plan1.Text.Trim() + "'";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count <= 0)
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "计划号一非法,请重新输入！");
                return;
            }
            else { so1 = dt.Rows[0][0].ToString(); }
            string sql2 = "select plan_so from data_plan where plan_code='" + Plan2.Text.Trim() + "'";
            DataTable dt2 = dc.GetTable(sql2);
            if (dt2.Rows.Count <= 0)
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "计划号二非法,请重新输入！");
                return;
            }
            else { so2 = dt2.Rows[0][0].ToString(); }

            if (chkProcess.Value.ToString() == "True")
            {
                
                MW_CREATE_BOMCOMP sp = new MW_CREATE_BOMCOMP()
                {
                    FUNC1 = "OP",
                    SO1 = so1,
                    JHDM1 = Plan1.Text.Trim(),
                    SO2 = so2,
                    JHDM2 = Plan2.Text.Trim(),
                    USER1 = theUserName

                };
                Rmes.DA.Base.Procedure.run(sp);

            }
            else 
            {
                
                MW_CREATE_BOMCOMP sp = new MW_CREATE_BOMCOMP()
                {
                    FUNC1 = "NOOP",
                    SO1 = so1,
                    JHDM1 = Plan1.Text.Trim(),
                    SO2 = so2,
                    JHDM2 = Plan2.Text.Trim(),
                    USER1 = theUserName

                };
                Rmes.DA.Base.Procedure.run(sp);

            }
        }
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }




}

