using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;

public partial class _Default : System.Web.UI.Page 
{   

    protected void Page_Load(object sender, EventArgs e)
    {
        //string userAcc1;
        //string userAcc = System.Web.HttpContext.Current.User.Identity.Name.Trim();
        //userAcc1 = userAcc;
        //int len = userAcc.IndexOf('\\', 0);
        //userAcc = userAcc.Substring(len + 1, userAcc.Length - len - 1);
        ////string strDomain = userAcc.Substring(0, len - 1);
        //string strDomain = userAcc1.Substring(0, len - 1);
        //if (strDomain != "")
        //    //if (strDomain == "域名")
        //{
        //    //直接进入系统
        //    Response.Redirect("/Rmes/Login/RmesIndex.aspx");
        //}
        //else
        //{
        //    //跳转到LOGIN.aspx页面进行验证
        //    Response.Redirect("./RmesLogin.aspx");
        //}

        Response.Redirect("./RmesLogin.aspx");
    }
}
