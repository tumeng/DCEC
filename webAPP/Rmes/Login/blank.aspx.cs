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

public partial class Rmes_Login_blank : System.Web.UI.Page
{
    private dataConn dc = new dataConn();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        string theDisplayCompanyCode = theUserManager.getCompanyCode();
        string theMenuUserId = theUserManager.getUserId();

        //根据默认首页判断转向页面
        string theSql = "SELECT PROGRAM_VALUE FROM REL_USER_DEFAULTPAGE A "
            + "LEFT JOIN CODE_PROGRAM B ON A.DEFAULT_PAGE = B.PROGRAM_CODE AND A.COMPANY_CODE = B.COMPANY_CODE "
            + "WHERE A.COMPANY_CODE = '" + theDisplayCompanyCode + "' AND USER_ID = '" + theMenuUserId + "'";
        dc.setTheSql(theSql);
        DataTable dt = dc.GetTable();
        string currentPage = "";
        if (dt.Rows.Count > 0)
        {
            currentPage = dc.GetTable().Rows[0]["PROGRAM_VALUE"].ToString();
            Response.Write("<script>location.href='" + "../.." + currentPage + "';</script>");
        }
        else
        {
            Response.Write("<script>location.href='../Login/RmesDefaultPage.aspx';</script>");
        }
        
    }
}
