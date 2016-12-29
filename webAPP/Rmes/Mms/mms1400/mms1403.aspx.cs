using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

/**
 * 功能概述：应用物料替换规则
 * 作    者：caoly
 * 创建时间：2014-04-21
 */


public partial class Rmes_mms1403 : BasePage
{
    private dataConn dc = new dataConn();
    private DataTable dt = new DataTable();

    public Database db = DB.GetInstance();

    public string theCompanyCode, theUserID;

    public string rmesID, project, oldBom, oldName, newBom, newName;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserID = theUserManager.getUserId();

        if (string.IsNullOrEmpty(Request.QueryString["rmesid"])) return;

        rmesID = Request.QueryString["rmesid"].ToString();
        BomExchangeEntity bomRule = db.First<BomExchangeEntity>("where rmes_id=@0", rmesID);

        oldBom = bomRule.ITEM_CODE_FROM;
        oldName = bomRule.ITEM_NAME_FROM;
        newBom = bomRule.ITEM_CODE_TO;
        newName = bomRule.ITEM_NAME_TO;

        TextOldBOM.Text = oldBom;
        TextOldName.Text = oldName;
        TextNewBOM.Text = newBom;
        TextNewName.Text = newName;

        string sql = "select t.project_code,t.project_code||'-'||t.project_name SHOWNAME from data_project t "
            + " where t.status='Y' and exists(select distinct a.project_code from data_xk_oldbom a where a.item_code in"
            + " ( '" + oldBom + "','" + newBom + "') and t.project_code = a.project_code) order by t.project_code";
        comboProject.DataSource = dc.GetTable(sql);
        comboProject.DataBind();
    }


    protected void butConfirm_Click(object sender, EventArgs e)
    {
        project = comboProject.SelectedItem.Value.ToString();
        
        //替换物料
        int rows = dc.ExeSql("update DATA_XK_OLDBOM set ITEM_CODE='" + newBom + "', ITEM_NAME='" + newName + "' where ITEM_CODE = '" + oldBom + "' and ITEM_NAME='" + oldName + "' and PROJECT_CODE = '" + project + "'");

        //写入事件日志
        string IPaddress = GetIP();
        KeyWorklogEntity log = new KeyWorklogEntity
        {
            CREATE_TIME = DateTime.Now,
            USER_ID = theUserID,
            USER_IP = IPaddress,
            WORK_TYPE = "物料替换",
            DELETE_FLAG = "N",
            CONTENT_LOG1 = "从物料代码：" + oldBom,
            CONTENT_LOG2 = "替换成：" + newBom,
            CONTENT_LOG3 = "合同号：" + project + "；替换规则ID：" + rmesID,
            AFFECT_ROWS = rows
        };

        string rmesId = db.Insert(log).ToString();

        Response.Write("<script type='text/javascript'>alert('项目物料替换成功！');window.opener.location.reload();window.close();</script>");
    }


    protected void butRestore_Click(object sender, EventArgs e)
    {
        project = comboProject.SelectedItem.Value.ToString();

        //还原物料
        int rows = dc.ExeSql("update DATA_XK_OLDBOM set ITEM_CODE='" + oldBom + "', ITEM_NAME='" + oldName + "' where ITEM_CODE = '" + newBom + "' and ITEM_NAME='" + newName + "' and PROJECT_CODE = '" + project + "'");

        //写入事件日志
        string IPaddress = GetIP();
        KeyWorklogEntity log = new KeyWorklogEntity
        {
            CREATE_TIME = DateTime.Now,
            USER_ID = theUserID,
            USER_IP = IPaddress,
            WORK_TYPE = "物料替换",
            DELETE_FLAG = "N",
            CONTENT_LOG1 = "从物料代码：" + newBom,
            CONTENT_LOG2 = "还原成：" + oldBom,
            CONTENT_LOG3 = "合同号：" + project + "；替换规则ID：" + rmesID,
            AFFECT_ROWS = rows
        };

        string rmesId = db.Insert(log).ToString();

        Response.Write("<script type='text/javascript'>alert('项目物料还原成功！');window.opener.location.reload();window.close();</script>");
    }

    /// <summary>
    /// 获得当前页面客户端的IP
    /// </summary>
    /// <returns>当前页面客户端的IP</returns>
    public static string GetIP()
    {
        string result = String.Empty;
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)//可能有代理
        {
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        if (!string.IsNullOrEmpty(result) && result.IndexOf(",") != -1)//有“,”，估计多个代理。取第一个不是内网的IP。
        {
            result = result.Replace(" ", "").Replace("'", "");
            string[] temparyip = result.Split(",;".ToCharArray());
            foreach (string tempip in temparyip)
            {
                if (tempip.Substring(0, 3) != "10."
                    && tempip.Substring(0, 7) != "192.168"
                    && tempip.Substring(0, 7) != "172.16")
                {
                    result = tempip; //找到不是内网的地址
                    break;
                }
            }
        }
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(result))
        {
            result = "0.0.0.0";
        }

        return result;
    }

}