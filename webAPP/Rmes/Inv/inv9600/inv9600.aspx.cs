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
 * 功能概述：临时措施查询
 * 作者：游晓航
 * 创建时间：2016-09-07
 */

public partial class Rmes_inv9600 : Rmes.Web.Base.BasePage
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
        theProgramCode = "inv9600";
        
       
        //setCondition();
    }

    
    private void setCondition()
    {
        string sql = "select * from sjbomsothxc where 1=1 ";
        if (txtDH.Text.Trim() != "")
        {
            sql = sql + "and THGROUP='" + txtDH.Value.ToString() + "'";
 
        }
        if (txtJHDM.Text.Trim() != "")
        {
            sql = sql + "and JHDM='" + txtJHDM.Value.ToString() + "'";

        }

        sql = sql + " order by rqsj desc";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        sql = "select * from RST_ATPU_ZJTS_LOG where 1=1 ";
        if (txtJHDM.Text.Trim() != "")
        {
            sql = sql + "and JHDM='" + txtJHDM.Value.ToString() + "'";

        }

        sql = sql + " order by rqsj desc";
        DataTable dt1 = dc.GetTable(sql);

        ASPxGridView2.DataSource = dt1;
        ASPxGridView2.DataBind();

    }
    //查询
    
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }

    protected void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView2.Selection.UnselectAll();
    }

    protected void ASPxGridView2_PageIndexChanged(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void ASPxGridView2_Init(object sender, EventArgs e)
    {
        setCondition();
    }

}
