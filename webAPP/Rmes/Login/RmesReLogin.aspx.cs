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

public partial class Rmes_Login_RmesReLogin : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    private string callbackResult = "";
    public string theServerPath = "";
    public PubCs thePubCs = new PubCs();

    protected void Page_Load(object sender, EventArgs e)
    {
        TxtEmployeeCode.Attributes.Add("onkeypress", "checkEmployeeCode(event);");
        TxtEmployeeCode.Focus();

        TxtPassword.Attributes.Add("onkeypress", "checkPassword(event);");

        //增加回掉部分
        string cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "GetReturnFlagFromServer", "context");
        string cbScript = "function UseCallback(arg,context)" + "{" + cbReference + ";" + "}";

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UseCallback", cbScript, true);

        //JS读取XML，得到配置文件地址
        theServerPath = Server.MapPath("~/").ToString();
        //由于"\\"在JS里面丢失了，在这里做个替换试试
        theServerPath = theServerPath.Replace("\\", "\\\\");
        theServerPath = theServerPath + "Rmes/Pub/Xml/RmesConfig.xml";

    }
   public void RaiseCallbackEvent(string eventArg) { 
    
        //在这里调用登录处理事件
        //处理登录
        string thePlineCode = "";
        string theUserCode = "";
        string thePassword = "";
        string theLoginStatus = "";
        Boolean theLoginFlag = false;
        string theSessionCode = "";
        string theCompanyCode = "";

        string thePlineName = "";  //20071219 增加生产线名称
        string theUserName = "";
        string theUserId = "";
        string theClientIp = Request.UserHostAddress;





        PubCs thePubCs = new PubCs();
        //测试读取xml
        string theServerPath1 = Server.MapPath("~/").ToString();
        theServerPath1 = theServerPath1 + "Rmes/Pub/Xml/RmesConfig.xml";
        string theRet = thePubCs.ReadFromXml(theServerPath1, "SeparatorStr");

        ArrayList theArrayList = thePubCs.SplitBySeparator(eventArg, theRet);
        string[] theString = thePubCs.ArrayListToString(theArrayList);

        theCompanyCode = theString[0];
        theUserCode = theString[1].ToUpper();
        thePassword = theString[2];
        thePlineName = theString[3];

        //用户代码和用户ID的转换 20110722

        dataConn theDataConn002 = new dataConn();
        theDataConn002.OpenConn();
        theDataConn002.setTheSql("select func_get_user('" + theCompanyCode + "','MES','" + theUserCode + "','A') from dual");
        theUserId = theDataConn002.GetValue();

        theDataConn002.CloseConn();


        //得到当前会话和公司号
        userManager theUserManager1 = (userManager)Session["theUserManager"];
        if (theUserManager1 != null)
        {
            theSessionCode = theUserManager1.theSessionCode;
            //theCompanyCode = theUserManager1.getCompanyCode();
        }
        else
        {
            //theCompanyCode = (string)Session["theCompanyCode"];
        }


        loginManager theLoginManager = new loginManager();
        theLoginManager.setCompanyCode(theCompanyCode);
        theLoginFlag = theLoginManager.ReLoginIn(theUserId, thePubCs.AESEncrypt(thePassword), theClientIp, theSessionCode,thePlineCode);


        //得到用户名称
        theUserName = theLoginManager.getUserName(); 
        theUserCode = theLoginManager.getUserCode();

        if (theLoginFlag)
        {
            TxtEmployeeCode.Text = "ok";
            TxtPassword.Text = theLoginManager.theLoginFlag;
        }
        else
        {
            TxtEmployeeCode.Text = "error";
            TxtPassword.Text = theLoginManager.theLoginFlag;
        }

        theLoginStatus = theLoginManager.theLoginFlag;
        theSessionCode = theLoginManager.theSessionCode;
        theCompanyCode = theLoginManager.getTheCompanyCode();

        //处理不同情况
        switch (theLoginStatus)
        {
            case "0":
                //登录成功
                //处理登录用户信息

                userManager theUserManager = new userManager(theSessionCode);
                theUserManager.setLoginFlag(theLoginFlag);
                theUserManager.setPlineCode(thePlineCode);
                theUserManager.setPlineName(thePlineName);

                theUserManager.setProgValue("/Rmes/Login/RmesIndex.aspx");
                theUserManager.setProgCode("rmesIndex");
                theUserManager.setProgName("系统登录");
                theUserManager.setUserId(theUserId);
                theUserManager.setUserCode(theUserCode);
                theUserManager.setUserName(theUserName);
                theUserManager.setCompanyCode(theCompanyCode);

                Session["theUserManager"] = theUserManager;
 

                //在新的窗口打开无标题栏等信息
                

                callbackResult = theLoginStatus;
                break;
            default:
                callbackResult = theLoginStatus;
                break;
        }

    }

    public string GetCallbackResult() {
        return callbackResult;
    }
   
}
