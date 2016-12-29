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

public partial class Rmes_Exception_DefaultException : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext cc = HttpContext.Current;
        string aa = cc.Session.SessionID.ToString();
    }
    protected void Button_Confirm_Click(object sender, EventArgs e)
    {
        ////重新打开主登录界面

        ////关闭页面，但不再提示是否关闭
        //Response.Write("<script language=JavaScript>");

        //string thePath = Page.ResolveUrl("~/RmesLogin.aspx");
        //Response.Write("var m_url='"+thePath+"';");
        //Response.Write("wh=window.open(m_url);");
        //Response.Write("wh.focus();");
        //Response.Write("opener = 'any';close()");
        ////Response.Write("window.opener=null; window.close();");
        //Response.Write("</script>");
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        ////页面返回上一页
        ////Response.Write(" <script language=JavaScript>window.opener=null; window.close(); </script >"); 
        //Response.Write(" <script language=JavaScript>parent.window.close(); </script >"); 
    }
}
