using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using Oracle.DataAccess.Client;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;

namespace Rmes.WebApp
{
    /// <summary>
    /// RmesPageHandler 的摘要说明
    /// </summary>
    public class RmesPageHandler : IHttpHandler, IRequiresSessionState
    {
        public static string theCompanyCode = "1100";  //公司代码

        public RmesPageHandler()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void ProcessRequest(HttpContext httpContext)
        {

            //截获请求，判断程序权限
            string theUrl = httpContext.Request.ServerVariables["URL"];
            int ii = theUrl.IndexOf("/", 1);
            if (ii < 0) ii = 0;
            string theUrlTemp = theUrl.Substring(0, ii);
            string thePath = httpContext.Request.Path;
            //string theProgramValue = thePath.Substring(theUrlTemp.Length, thePath.Length - theUrlTemp.Length);
            string theProgramValue = theUrl;
            string theClientIp = httpContext.Request.UserHostAddress;

            string requestedUrl = "" ;
            string targetUrl="";
            int urlLength=0;

            // save requested, target url
            requestedUrl = httpContext.Request.RawUrl;
 
            targetUrl = requestedUrl;

            // save target url length
            urlLength = targetUrl.IndexOf("?");
            if (urlLength == -1)
                urlLength = targetUrl.Length;
            string theUserId = "";
            //得到session里面的当前用户，结合上面得到的程序，判断是否有权限访问
            userManager theUserManagerTemp = (userManager)httpContext.Session["theUserManager"];
            if (theUserManagerTemp == null)
            {
                if (targetUrl.Length>14 && targetUrl.Substring(1, 14) != "RmesLogin.aspx") //modified by liuzhy 2013/12/24，这里把参数改了一下（原来是从后往前找，如果带参会不正确），修正了url不对的情况会提示超时。。
                {

                    //targetUrl = theUrlTemp + "/Rmes/Login/RmesReLogin.aspx";
                    //改自动重新登录到默认出错页面
                    targetUrl = "~/Rmes/Exception/DefaultException.aspx";
                    urlLength = targetUrl.IndexOf("?");
                    if (urlLength == -1)
                        urlLength = targetUrl.Length;
                }
            }
            else
            {
                theUserId = theUserManagerTemp.getUserId().ToString();
                theCompanyCode = theUserManagerTemp.getCompanyCode().ToString();
            }

            //根据这两个值进行判断是否有登录权限，判断逻辑由存储过程完成

            string theRetStr = "";
            string theRetProgramCode = "";
            string theRetProgramName = "";


            MW_CHECK_USERRIGHT sp = new MW_CHECK_USERRIGHT() { 
                THECOMPANYCODE1 = theCompanyCode,
                THEUSERID1 = theUserId,
                THECLIENTIP1 = theClientIp,
                THEPROGRAMVALUE1 = theProgramValue,
                THERETSTR1="",
                THERETPROGRAMCODE1="",
                THERETPROGRAMNAME1=""
            };

            Procedure.run(sp);

            theRetStr = sp.THERETSTR1;
            theRetProgramCode = sp.THERETPROGRAMCODE1;
            theRetProgramName = sp.THERETPROGRAMNAME1;

            //dataConn theDataConn = new dataConn();
            //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
            //theDataConn.theComd.CommandText = "MW_CHECK_USERRIGHT";

            //theDataConn.theComd.Parameters.Clear();

            //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
            ////theDataConn.theComd.Parameters.Add("@THECOMPANYCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

            //theDataConn.theComd.Parameters.Add("THEUSERID1",  OracleDbType.Varchar2).Value = theUserId;
            ////theDataConn.theComd.Parameters.Add("@THEUSERCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

            //theDataConn.theComd.Parameters.Add("THECLIENTIP1", OracleDbType.Varchar2).Value = theClientIp;
            ////theDataConn.theComd.Parameters.Add("@THEUSERCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

            //theDataConn.theComd.Parameters.Add("THEPROGRAMVALUE1", OracleDbType.Varchar2).Value = theProgramValue;
            ////theDataConn.theComd.Parameters.Add("@THEPROGRAMVALUE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

            //theDataConn.theComd.Parameters.Add("THERETSTR1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

            //theDataConn.theComd.Parameters.Add("THERETPROGRAMCODE1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

            //theDataConn.theComd.Parameters.Add("THERETPROGRAMNAME1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

            //theDataConn.OpenConn();
            //theDataConn.theComd.ExecuteNonQuery();

            //theRetStr = theDataConn.theComd.Parameters["THERETSTR1"].Value.ToString();
            //theRetProgramCode = theDataConn.theComd.Parameters["THERETPROGRAMCODE1"].Value.ToString();
            //theRetProgramName = theDataConn.theComd.Parameters["THERETPROGRAMNAME1"].Value.ToString();

            //theDataConn.CloseConn();


            //根据返回数据判断，进行不同的处理
            switch (theRetStr) { 
                case "0":
                    //无需授权访问，只是继续请求，不做任何处理，包括未定义的程序，定义为无需授权的程序，比如登录和一些公用查询程序
                    
                    //保证登录程序的顺利执行，在session里面传递公司号过去
                    try
                    {
                        httpContext.Session["theCompanyCode"] = theCompanyCode;
                        httpContext.RewritePath(targetUrl);

                        IHttpHandler handler = PageParser.GetCompiledPageInstance(
                        targetUrl.Substring(0, urlLength), null, httpContext);
                        handler.ProcessRequest(httpContext);
                    }
                    catch (Exception ex)
                    {

                    }
                    break;
                case "1":
                    //没有权限，终止请求
                    //httpContext.Response.StatusCode = 400;
                    //httpContext.Response.StatusDescription = "你没有访问权限，请联系系统管理员!";

                    //映射到错误处理界面
                    targetUrl = "~/Rmes/Exception/DefaultException.aspx";
                    urlLength = targetUrl.IndexOf("?");
                    if (urlLength == -1)
                        urlLength = targetUrl.Length;

                    httpContext.Session["theCompanyCode"] = theCompanyCode;
                    httpContext.RewritePath(targetUrl);

                    IHttpHandler handler2 = PageParser.GetCompiledPageInstance(
                    targetUrl.Substring(0, urlLength), null, httpContext);
                    handler2.ProcessRequest(httpContext);
                    break;
                case "2":
                    //有权限访问，更新当前会话的程序号和程序名称信息
                    theUserManagerTemp.setProgCode(theRetProgramCode);
                    theUserManagerTemp.setProgName(theRetProgramName);
                    httpContext.Session["theUserManager"] = theUserManagerTemp;
                    //try
                    //{
                    httpContext.RewritePath(targetUrl);

                    IHttpHandler handler1 = PageParser.GetCompiledPageInstance(
                    targetUrl.Substring(0, urlLength), null, httpContext);

                    //IHttpHandler handler1 = PageParser.GetCompiledPageInstance(thePath, null, httpContext);

                    handler1.ProcessRequest(httpContext);

                    //}
                    //catch
                    //{
                    //    httpContext.RewritePath(targetUrl.Substring(0, urlLength));

                    //    IHttpHandler handler1 = PageParser.GetCompiledPageInstance(
                    //    targetUrl.Substring(0, urlLength), null, httpContext);
                    //    handler1.ProcessRequest(httpContext);
                    //}
                    break;
                default:
                    //没有权限，终止请求
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.StatusDescription = "你没有访问权限，请联系系统管理员!";
                    break;

            }


        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
