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

public partial class Rmes_Login_RmesLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Button_Confirm.Attributes.Add("onclick", "window.close()");
        
    }

    protected void Button_Confirm_Click(object sender, EventArgs e)
    {
        //得到当前用户信息
        Boolean theLoginFlag = false;
        string theCompanyCode = "";

        userManager theUserManger1 = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManger1.getCompanyCode();

        loginManager theLoginManager = new loginManager();
        theLoginManager.setCompanyCode(theCompanyCode);
        theLoginFlag = theLoginManager.loginOut(theUserManger1.theSessionCode);

        //关闭页面，但不再提示是否关闭
        Response.Write("<script language=JavaScript>window.opener=null; window.parent.close();</script>");
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        //if (Request.UrlReferrer != null)
          //  Response.Redirect(Request.UrlReferrer.ToString()); 
        //页面返回上一页
        Response.Write(" <script language=JavaScript>location= 'javascript:history.go(-2);'; </script >"); 

    }
}
