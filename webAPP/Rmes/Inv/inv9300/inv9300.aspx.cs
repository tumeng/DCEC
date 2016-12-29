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
/**
 * 功能概述：上线入库回冲对比界面
 * 作者：游晓航
 * 创建时间：2016-08-25
 */

public partial class Rmes_inv9300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "inv9300";
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit3.Date = DateTime.Now.AddDays(1);
        }
        initCode();
        setCondition();
    }

    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select pline_code,pline_name from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'";

        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    private void setCondition()
    {
      string  strDB = txtDB.Text.ToString();
        
        
        switch (strDB)
        {
            case "已上线未入库":
                Contrast_A();
                break;
            case "已入库未出库":
                Contrast_B();
                break;
            case "已出库未入库":
                Contrast_C();
                break;
            case "已入库未回冲":
                Contrast_D();
                break;
            default:
                Contrast_A();
                break;
        }
        
         
    }
    //查询
    private void Contrast_A()
    {
        string sql = "Select PLAN_SO,SN,to_char(WORK_TIME,'yyyy-mm-dd hh24:mi:ss') WORK_TIME from DATA_PRODUCT a where  ";
        if (txtPCode.Text.Trim() != "")
        {

            sql = sql + " PLINE_CODE='" + txtPCode.Value.ToString() + "' and  not exists (select ghtm from dp_rckwcb b where a.SN=b.ghtm and gzdd='" + txtPCode.Value.ToString() + "' and Rc='入库') ";
        }
        else
        {
            sql = sql + "  PLINE_CODE IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  and  not exists "
                + " (select ghtm from dp_rckwcb b where a.SN=b.ghtm and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  and Rc='入库') ";

        }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + "AND  WORK_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
        }
        if (ASPxDateEdit3.Text.Trim() != "")
        {
            sql = sql + " AND WORK_TIME<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
        }

        sql = sql + " order by SN";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    private void Contrast_B()
    {
        string sql = "SELECT SO PLAN_SO,GHTM SN, to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss') WORK_TIME from dp_rckwcb a where  ";
        
        if (txtPCode.Text.Trim() != "")
        {

            sql = sql + " gzdd='" + txtPCode.Value.ToString() + "'and rc='入库' and  not exists (select ghtm from dp_rckwcb b where a.ghtm=b.ghtm and gzdd='" + txtPCode.Value.ToString() + "' and Rc='出库') ";
        }
        else
        {
            sql = sql + "  gzdd IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and rc='入库'  and  not exists "
                + " (select ghtm from dp_rckwcb b where a.ghtm=b.ghtm and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  and Rc='出库') ";

        }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + "AND  to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD ') ";
        }
        if (ASPxDateEdit3.Text.Trim() != "")
        {
            sql = sql + " AND to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD ') ";
        }

       
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    private void Contrast_C()
    {
        string sql = "SELECT SO PLAN_SO,GHTM SN, to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss') WORK_TIME from dp_rckwcb a where  ";

        if (txtPCode.Text.Trim() != "")
        {

            sql = sql + " gzdd='" + txtPCode.Value.ToString() + "'and rc='出库' and  not exists (select ghtm from dp_rckwcb b where a.ghtm=b.ghtm and gzdd='" + txtPCode.Value.ToString() + "' and Rc='入库') ";
        }
        else
        {
            sql = sql + "  gzdd IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and rc='出库'  and  not exists "
                + " (select ghtm from dp_rckwcb b where a.ghtm=b.ghtm and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  and Rc='入库') ";

        }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + "AND  to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD ') ";
        }
        if (ASPxDateEdit3.Text.Trim() != "")
        {
            sql = sql + " AND to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD ') ";
        }


        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    private void Contrast_D()
    {
        string sql = "SELECT SO PLAN_SO,GHTM SN, to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss') WORK_TIME from dp_rckwcb a where  ";

        if (txtPCode.Text.Trim() != "")
        {

            sql = sql + " gzdd='" + txtPCode.Value.ToString() + "'and rc='入库' and  not exists (select lsh from qad_bkfl b where a.ghtm=b.lsh) ";
        }
        else
        {
            sql = sql + "  gzdd IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and rc='入库'  and  not exists "
                + " (select lsh from qad_bkfl b where a.ghtm=b.lsh ) ";

        }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + "AND  to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD ') ";
        }
        if (ASPxDateEdit3.Text.Trim() != "")
        {
            sql = sql + " AND to_date(GZRQ,'yyyy-mm-dd hh24:mi:ss')<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD ') ";
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





}
