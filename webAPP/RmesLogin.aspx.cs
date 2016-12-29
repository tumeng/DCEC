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
using Rmes.Pub.Function;

public partial class RmesLogin : System.Web.UI.Page
{
    private string callbackResult = "";
    public PubCs thePubCs = new PubCs();
    protected void Page_Load(object sender, EventArgs e)
    {
        string userAcc1;
        bool isValid = false,isOld=false;
        string userAcc = System.Web.HttpContext.Current.User.Identity.Name.Trim();
        userAcc1 = userAcc;
        int len = userAcc.IndexOf('\\', 0);
        userAcc = userAcc.Substring(len + 1, userAcc.Length - len - 1).ToUpper();
        //string strDomain = userAcc.Substring(0, len - 1);
        string strDomain = userAcc1.Substring(0, len);
        if (strDomain == "DCEC")
        //if (strDomain != "")
        //if (strDomain == "域名")
        {
            //判断用户名是否合法，并获取密码
            string sqlY = "select a.user_code,a.user_password,b.company_code,b.company_name from code_user a left join code_company b on a.company_code=b.company_code where upper(user_code)='" + userAcc.ToUpper() + "'";
            dataConn dc = new dataConn();
            dc.OpenConn();
            dc.setTheSql(sqlY);
            DataTable dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                isValid = true;
                string theUserCode = dc.GetTable().Rows[0][0].ToString();
                string theCompanyCode = dc.GetTable().Rows[0][2].ToString();
                string thePlineCode = dc.GetTable().Rows[0][2].ToString();
                string thePassword = thePubCs.AESDecrypt(dc.GetTable().Rows[0][1].ToString());
                string thePlineName = dc.GetTable().Rows[0][3].ToString();
                string theClientIp = Request.UserHostAddress;
                if (theUserCode.ToUpper() == thePassword.ToUpper())
                {
                    isOld = true;
                }

                string sql = string.Format("select func_get_user('{0}','MES','{1}','A') from dual", theCompanyCode, theUserCode);

                string theUserName = "";
                string theUserId = "";
                string theLoginStatus = "";
                string theSessionCode = "";
                bool theLoginFlag = false;


                dataConn theDataConn002 = new dataConn();
                theDataConn002.OpenConn();
                theDataConn002.setTheSql(sql);
                theUserId = theDataConn002.GetValue();

                theDataConn002.CloseConn();


                //在登录界面，从会话得到公司号，以后都是从用户对象里面得到

                //theCompanyCode = (string)Session["theCompanyCode"];
                loginManager theLoginManager = new loginManager();
                theLoginManager.setCompanyCode(theCompanyCode);
                theLoginFlag = theLoginManager.loginIn(theUserId, thePubCs.AESEncrypt(thePassword), theClientIp, thePlineCode);

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
                        if (isOld)
                        {
                            theUserManager.setProgValue("/Rmes/Sam/sam2400/sam2400.aspx");
                            theUserManager.setProgCode("sam2400");
                            theUserManager.setProgName("用户密码维护");
                        }
                        else
                        {
                            theUserManager.setProgValue("/Rmes/Login/RmesIndex.aspx");
                            theUserManager.setProgCode("rmesIndex");
                            theUserManager.setProgName("系统登录");
                        }
                        theUserManager.setUserId(theUserId);
                        theUserManager.setUserCode(theUserCode);
                        theUserManager.setUserName(theUserName);
                        theUserManager.setCompanyCode(theCompanyCode);

                        Session["theUserManager"] = theUserManager;
                        callbackResult = theLoginStatus;
                        if (isOld) 
                        {
                            Response.Redirect("/Rmes/Sam/sam2400/sam2400.aspx?progCode=sam2400&progName=用户密码维护");
                        }
                        else
                        {
                            Response.Redirect("/Rmes/Login/RmesIndex.aspx?progCode=rmesIndex&progName=系统登录");
                        }
                        break;
                    default:
                        callbackResult = theLoginStatus;
                        break;
                }
                if (callbackResult == "0" && isOld)
                {
                    callbackResult = "10";
                }
                //Response.Write(callbackResult);
                
                Response.End();
            }
            else
            {
                isValid = false;
            }
            dc.CloseConn();
        }
        else
        {
            isValid = false;
        }
        //if (!IsPostBack)
        //{
        //    Session.Abandon();
        //    Session.Clear();
        //}
            //现在只处理登录，从QueryString中得到ajax消息
            if (!string.IsNullOrWhiteSpace(Request.QueryString["method"]) && Request.QueryString["method"].Equals("login") && !isValid)
            {
                //document.forms[0]['DropDownListPline'].value 
                //document.forms[0]['TxtEmployeeCode'].value 
                //document.forms[0]['TxtPassword'].value
                //thePlineName;

                string theUserCode = Request.QueryString["usercode"];
                string theCompanyCode = Request.QueryString["companycode"];
                string thePlineCode = Request.QueryString["companycode"];
                string thePassword = Request.QueryString["password"];
                string thePlineName = Request.QueryString["companyname"];
                string theClientIp = Request.UserHostAddress;
                if (theUserCode.ToUpper() == thePassword.ToUpper())
                {
                    isOld = true;
                }
                string sql = string.Format("select func_get_user('{0}','MES','{1}','A') from dual", theCompanyCode, theUserCode);

                string theUserName = "";
                string theUserId = "";
                string theLoginStatus = "";
                string theSessionCode = "";
                bool theLoginFlag = false;


                dataConn theDataConn002 = new dataConn();
                theDataConn002.OpenConn();
                theDataConn002.setTheSql(sql);
                theUserId = theDataConn002.GetValue();

                theDataConn002.CloseConn();


                //在登录界面，从会话得到公司号，以后都是从用户对象里面得到

                //theCompanyCode = (string)Session["theCompanyCode"];
                loginManager theLoginManager = new loginManager();
                theLoginManager.setCompanyCode(theCompanyCode);
                theLoginFlag = theLoginManager.loginIn(theUserId, thePubCs.AESEncrypt(thePassword), theClientIp, thePlineCode);

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
                        if (isOld)
                        {
                            theUserManager.setProgValue("/Rmes/Sam/sam2400/sam2400.aspx");
                            theUserManager.setProgCode("sam2400");
                            theUserManager.setProgName("用户密码维护");
                        }
                        else
                        {
                            theUserManager.setProgValue("/Rmes/Login/RmesIndex.aspx");
                            theUserManager.setProgCode("rmesIndex");
                            theUserManager.setProgName("系统登录");
                        }
                        theUserManager.setUserId(theUserId);
                        theUserManager.setUserCode(theUserCode);
                        theUserManager.setUserName(theUserName);
                        theUserManager.setCompanyCode(theCompanyCode);

                        Session["theUserManager"] = theUserManager;
                        callbackResult = theLoginStatus;
                        break;
                    default:
                        callbackResult = theLoginStatus;
                        break;
                }
                if (callbackResult == "0" && isOld)
                {
                    callbackResult = "10";
                }
                Response.Write(callbackResult);
                Response.End();
            }
        
    }
}
