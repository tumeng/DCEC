/**************************
 * 功能：主界面
 * 
 * 说明：用ASPx控件提供的Menu控件重新写了菜单，并对主界面做了一些调整
 * 
 * 作者：刘征宇
 * 
 * 修订：20110803
 * 
 * 已知问题：暂无
 * 
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
using Oracle.DataAccess.Client;
using System.Xml;


public partial class Rmes_Login_RmesIndex : Rmes.Web.Base.BasePage
{
    public string theDisplayProgramName = "";
    public string theDisplayCompanyCode = "";
    public string theDisplayPlineName = "";
    public string theDisplayUserName = "";
    public string thePrintSql = "";
    public string theMenuHead = "";

    public string theHelpFile = "";
    public string theDisplayProgramCode = "";

    public string menu = "";
    public string m1 = "";
    private string baseURI = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        PubCs thePubCs = new PubCs();
        string theServerPath = Server.MapPath("~/").ToString();
        theServerPath = theServerPath + "Rmes/Pub/Xml/RmesConfig.xml";
        baseURI = thePubCs.ReadFromXml(theServerPath, "rootPath");


        userManager theUserManager = (userManager)Session["theUserManager"];

        theDisplayProgramName = theUserManager.getProgName();
        theDisplayPlineName = theUserManager.getPlineName();
        theDisplayUserName = theUserManager.getUserName();
        theDisplayCompanyCode = theUserManager.getCompanyCode();
        theDisplayProgramCode = theUserManager.getProgCode();

        string theMenuCompanyCode = theUserManager.getCompanyCode();
        string theMenuUserCode = theUserManager.getUserCode();
        string theMenuUserId = theUserManager.getUserId();

        string opt = Request["opt"] as string;
        string progid = Request["progCode"] as string;
        if (opt != null && progid != null && opt != string.Empty && progid != string.Empty)
            if (opt == "setdefaultpage")
            {
                try
                {
                    dataConn dc = new dataConn();
                    string _theSql = "SELECT * FROM REL_USER_DEFAULTPAGE WHERE COMPANY_CODE = '" + theDisplayCompanyCode + "' AND USER_ID = '" + theMenuUserId + "'";
                    dc.setTheSql(_theSql);
                    if (dc.GetState())
                    {
                        _theSql = "UPDATE REL_USER_DEFAULTPAGE SET DEFAULT_PAGE = '" + progid + "' WHERE  COMPANY_CODE = '" + theDisplayCompanyCode + "' AND USER_ID = '" + theMenuUserId + "'";
                    }
                    else
                    {
                        _theSql = "INSERT INTO REL_USER_DEFAULTPAGE(COMPANY_CODE,USER_ID,DEFAULT_PAGE)VALUES('" + theDisplayCompanyCode + "','" + theMenuUserId + "','" + progid + "')";
                    }
                    dc.ExeSql(_theSql);
                    Response.Write("设置成功:" + progid);
                }
                catch (Exception ex)
                {
                    Response.Write("设置失败，信息如下：\n" + ex.Message);
                }
                Response.End();
            }

        //得到菜单绝对地址
        string theHost = Request.ServerVariables["REMOTE_ADDR"];
        string thePort = Request.ServerVariables["SERVER_PORT"];
        string theUrl = Request.ServerVariables["URL"];
        string theUrlTemp = theUrl.Substring(0, theUrl.IndexOf("/", 1));

        string str = "欢迎使用RMES系统，" + theDisplayPlineName + " 的 " + theDisplayUserName + "，你当前打开的页面:" + theDisplayProgramName;
        ASPxRoundPanel1.HeaderText = str;

        //帮助 20080408

        theHelpFile = theUrlTemp + "/Rmes/Help/" + theDisplayProgramCode + ".htm";


        //考虑以后可能对程序内容做一些处理，暂且定义变量
        //string theProgTemp = "";

        string theSql = "select menu_code,menu_name,menu_code_father,menu_index,program_code,program_name,program_value from vw_rel_user_menu where company_code='" + theMenuCompanyCode + "' and user_id='" + theMenuUserId + "' order by menu_level,menu_index";
        dataConn theDataConn = new dataConn(theSql);
        //theDataConn.OpenConn();
        //theDataConn.theComd.CommandType = CommandType.Text;
        //theDataConn.theComd.CommandText = theSql;
        //OracleDataReader dr = theDataConn.theComd.ExecuteReader();

        DataTable dt = theDataConn.GetTable(theSql);
        DataView dv = new DataView(dt);

        dv.RowFilter = "menu_code_father is NULL and menu_code is not NUll and menu_name is not NULL";
        dv.Sort = "menu_index";

        string name = "", text = "", url = "";

        foreach(DataRowView dvr in dv)
        {
            name = dvr["menu_code"].ToString();
            text = dvr["menu_name"].ToString();
            url = dvr["program_value"].ToString().Trim();
            url = url.Equals("") ? "" : ("../.." + url + "?progCode=" + dvr["program_code"].ToString() + "&progName=" + dvr["program_name"]);
            if (!text.Trim().Equals(""))
            {
                DevExpress.Web.ASPxMenu.MenuItem m = new DevExpress.Web.ASPxMenu.MenuItem(text, name,"",url);
                ASPxMenu1.Items.Add(m);
                AddChildMenu(dt, m);
            }
        }
            if (ASPxMenu1.Items.Count > 0)
            {
                //modeify by thl 20161011 用户不需要 注释了
                ASPxMenu1.Target = "ifmain";
                ASPxMenu1.Items.Add("附加功能", "ext_menu", "", "");
                DevExpress.Web.ASPxMenu.MenuItem m = ASPxMenu1.Items.FindByName("ext_menu");
                m.Items.Add("设置为默认页", "ext_set_homepage", "", "javascript:$.setDefaultPage(window.ifmain.location.href)", "_self");
                //m.Items.Add("选择主题", "ext_set_theme", "", "");
                DevExpress.Web.ASPxMenu.MenuItem n = ASPxMenu1.Items.FindByName("ext_set_theme");


                string path = Server.MapPath("~/App_Themes");

                //string[] themes = System.IO.Directory.GetDirectories(path);

                //string sTemp = "";
                //foreach (string s in themes)
                //{
                //    sTemp = s.Substring(s.LastIndexOf("\\") + 1);
                //    if (!sTemp.StartsWith("."))
                //        n.Items.Add(sTemp, sTemp, "", "javascript:jQuery.setTheme('" + sTemp + "')", "_self");
                //}
            }
        }


    private void AddChildMenu(DataTable menuDataTable,DevExpress.Web.ASPxMenu.MenuItem parentMenuItem)
    {
        string text = "", name = "", url = "";
        DataView dv = new DataView(menuDataTable);
        dv.RowFilter = "menu_code_father = '" + parentMenuItem.Name + "'";
        dv.Sort = "menu_index";
        foreach(DataRowView dvr in dv)
        {
            name = dvr["menu_code"].ToString();
            text = dvr["menu_name"].ToString();
            if (!text.Trim().Equals(""))
            {
                url = dvr["program_value"].ToString().Trim();
                url = url.Equals("") ? "" : ("../.." + url + "?progCode=" + dvr["program_code"].ToString() + "&progName=" + dvr["program_name"]);
                DevExpress.Web.ASPxMenu.MenuItem m = new DevExpress.Web.ASPxMenu.MenuItem(text,name,"",url);
                parentMenuItem.Items.Add(m);
                AddChildMenu(menuDataTable, m);
            }
        }
        dv.Dispose();
    }

}
