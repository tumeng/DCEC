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
using Rmes.Pub.Function;
using Rmes.Pub.Data;

public partial class Rmes_Pub_CommonHandle_commonSign : System.Web.UI.Page
{
    private String theCompanyCode, theUserTransferName, theUserTransferCode;
    private PubJs thePubJs = new PubJs();
    private dataConn theDc = new dataConn();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        theUserTransferCode = Request.QueryString["paraPassCode"].ToString();
        theUserTransferName = Request.QueryString["paraPassName"].ToString();
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        string selSql = "select USER_PASSWORD,USER_NAME from CODE_USER where COMPANY_CODE='" + theCompanyCode + "' AND USER_CODE='" + txtUserCode.Text.Trim() + "'";
        dataConn selDc = new dataConn(selSql);
        DataTable selDt = selDc.GetTable();

        if (selDt.Rows.Count > 0)
        {
            if (selDt.Rows[0]["USER_PASSWORD"].ToString() != txtPassOne.Text.Trim() )
            {
                thePubJs.Alert("输入密码错误，请重新输入");
                return;
            }

            Response.Write("<script>");
            Response.Write("window.opener.document.getElementById('" + theUserTransferCode + "').value='" + txtUserCode.Text + "';");
            Response.Write("window.opener.document.getElementById('" + theUserTransferName + "').value='" + selDt.Rows[0]["USER_NAME"].ToString() + "';");
            Response.Write("close();");
            Response.Write("</script>");
            
        }
    }
}
