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
using Oracle.DataAccess.Client;
public partial class MasterPage : System.Web.UI.MasterPage
{
    public string theDisplayProgramName = "";
    public string theDisplayCompanyCode = "";
    public string theDisplayPlineName = "";
    public string theDisplayUserName = "";
    public string thePrintSql = "";
    public string theMenuUserCode = "";
    public string theProgValue = "";
    public string theHelpFile = "";
    public string theDisplayProgramCode = "";
    private dataConn dc = new dataConn();


    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];

        theDisplayProgramName = theUserManager.getProgName();
        theDisplayPlineName = theUserManager.getPlineName();
        theDisplayUserName = theUserManager.getUserName();
        theDisplayCompanyCode = theUserManager.getCompanyCode();
        theDisplayProgramCode = theUserManager.getProgCode();
        theProgValue = theUserManager.getProgVlaue();
        string theMenuCompanyCode = theUserManager.getCompanyCode();
        theMenuUserCode = theUserManager.getUserCode();

        Response.Cookies["CurrentProgramCode"].Value = theDisplayProgramCode;

        ////打印
        //thePrintSql = (string)Session["thePrintSql"];


        //显示提示信息  20071219
        //string str = theDisplayPlineName + "-->" + theDisplayUserName + "___" + theDisplayProgramName;
        //Response.Write("<script>window.status='" + str + "';</script>");

        //theHelpFile = theUrlTemp + "/Rmes/Help/" + theDisplayProgramCode + ".htm";

        //考虑以后可能对程序内容做一些处理，暂且定义变量
        //string theProgTemp = "";

    }

    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
    //    //设为首页
    //    string theSql = "SELECT * FROM RMES_REL_USER_DEFAULTPAGE WHERE COMPANY_CODE = '" + theDisplayCompanyCode + "' AND USER_CODE = '" + theMenuUserCode + "'";
    //    dc.setTheSql(theSql);
    //    if (dc.GetState())
    //    {
    //        theSql = "UPDATE RMES_REL_USER_DEFAULTPAGE SET DEFAULT_PAGE = '" + theDisplayProgramCode + "' WHERE  COMPANY_CODE = '" + theDisplayCompanyCode + "' AND USER_CODE = '" + theMenuUserCode + "'";
    //    }
    //    else
    //    {
    //        theSql = "INSERT INTO RMES_REL_USER_DEFAULTPAGE(COMPANY_CODE,USER_CODE,DEFAULT_PAGE)VALUES('" + theDisplayCompanyCode + "','" + theMenuUserCode + "','" + theDisplayProgramCode + "')";
    //    }
    //    dc.ExeSql(theSql);
    //    Response.Write("<script>alert('设置成功.');</script>");
    //}
}
