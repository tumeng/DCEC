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
 * 功能概述：入库回冲查询
 * 作者：游晓航
 * 创建时间：2016-08-25
 */

public partial class Rmes_inv9200 : Rmes.Web.Base.BasePage
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
        theProgramCode = "inv9200";
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

        string strPCode = txtPCode.Text.Trim().ToUpper();
        //string strRLoction = txtRLoction.Text.Trim().ToUpper();
        string sql = "SELECT lsh1,b.PLAN_SO,b.product_model,b.plan_code,user1,rqsj1,gzdd1 from qad_bkfllog a,DATA_PRODUCT b where a.lsh1=b.sn ";
        if (strPCode != "" )
        {

            sql = sql + " AND GZDD1='" + txtPCode.Value.ToString() + "'";
        }
        else
        {
            sql = sql + " AND GZDD1 IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";

        }
       if (ASPxDateEdit1.Text.Trim() != "")
       {
           sql = sql + " AND RQSJ1>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
       }
       if (ASPxDateEdit3.Text.Trim() != "")
       {
           sql = sql + " AND RQSJ1<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
       }
        

        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    //查询
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
   
   
    


}
