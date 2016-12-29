/**
 * 功能概述：首页
 * 作者：徐莹
 * 创建时间：2011-08-02
 */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;

public partial class Rmes_Login_RmesDefaultPage : System.Web.UI.Page
{
    private string theCompanyCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //得到公司号
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //初始化生产完成统计面板数据
        SqlDataSource1.SelectCommand = "SELECT DISTINCT DATE_MONTH ,MONTH_COMPLETE,MONTH_TARGET from DATA_PLAN_COMPLETE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlDataSource1.DataBind();

        //SqlDataSource2.SelectCommand = "SELECT PLAN_CODE, PLAN_ONLINE_QUANTITY, PLAN_OFFLINE_QUANTITY, ROUND(PLAN_OFFLINE_QUANTITY/PLAN_ONLINE_QUANTITY,2)*100 PLAN_COMPLETE FROM DATA_PLAN  WHERE COMPANY_CODE = '" + theCompanyCode
        //    + "' AND PLAN_BEGIN_DATE = TO_DATE('" + System.DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "','YYYY-MM-DD')";
        //SqlDataSource2.DataBind();
    }
}
