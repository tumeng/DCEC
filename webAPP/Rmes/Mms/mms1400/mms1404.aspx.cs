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
using Rmes.DA.Factory;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

/**
 * 功能概述：按项目进行物料替换
 * 作    者：caoly
 * 创建时间：2014-04-19
 */


public partial class Rmes_mms1404 : BasePage
{
    private dataConn dc = new dataConn();
    private DataTable dt = new DataTable();

    public Database db = DB.GetInstance();
    private PubCs pc = new PubCs();
    public string rmesID, theCompanyCode, theUserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserID = theUserManager.getUserId();

        List<WorkShopEntity> workShop = WorkShopFactory.GetUserWorkShops(theUserID);
        string strWorkShop = workShop[0].RMES_ID;

        string sql = "select PROJECT_CODE,PROJECT_CODE||' | '||PROJECT_NAME as SHOWNAME from DATA_PROJECT WHERE STATUS='Y'and WORKSHOP_ID = '" + strWorkShop + "' order by project_code";
        comboProject.DataSource = dc.GetTable(sql);
        comboProject.DataBind();
    }


    protected void butConfirm_Click(object sender, EventArgs e)
    {
        string project, oldBom, oldName, newBom, newName;

        project = comboProject.SelectedItem.Value.ToString();
        oldBom = ComboItemFrom.SelectedItem.Value.ToString();
        oldName = TextOldName.Text.Trim();
        newBom = TextNewBOM.Text.Trim();
        newName = TextNewName.Text.Trim();

        BomExchangeEntity bomRule = db.First<BomExchangeEntity>("where item_code_from=@0 and ENABLE_FLAG='Y'", oldBom);
        rmesID = bomRule.RMES_ID;

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

        ComboItemFrom.SelectedIndex = -1;
        TextOldName.Text = "";
        TextNewBOM.Text = "";
        TextNewName.Text = "";

        lblMessage.Text = "物料替换成功！原物料代码：" + oldBom + "；新代码：" + newBom + "；请继续...";
    }


    protected void butRestore_Click(object sender, EventArgs e)
    {
        string project, oldBom, oldName, newBom, newName;

        project = comboProject.SelectedItem.Value.ToString();
        oldBom = ComboItemFrom.SelectedItem.Value.ToString();
        oldName = TextOldName.Text.Trim();
        newBom = TextNewBOM.Text.Trim();
        newName = TextNewName.Text.Trim();

        BomExchangeEntity bomRule = db.First<BomExchangeEntity>("where item_code_from=@0 and ENABLE_FLAG='Y'", oldBom);
        rmesID = bomRule.RMES_ID;

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

        ComboItemFrom.SelectedIndex = -1;
        TextOldName.Text = "";
        TextNewBOM.Text = "";
        TextNewName.Text = "";

        lblMessage.Text = "物料还原成功！原物料代码：" + oldBom + "；新代码：" + newBom + "；请继续...";
    }


    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        char[] charSeparators = new char[] { ',' };
        string[] collection = e.Parameter.Split(charSeparators);
        string p = collection[0].ToString();
        string code = collection[1].ToString();

        string strName;

        switch (p)
        {
            case "checkValue":

                BomExchangeEntity bomRule = db.First<BomExchangeEntity>("where item_code_from=@0 and ENABLE_FLAG='Y'",code);

                strName = bomRule.ITEM_NAME_FROM + "," + bomRule.ITEM_CODE_TO + "," + bomRule.ITEM_NAME_TO;
                rmesID = bomRule.RMES_ID;

                e.Result = "checkOK," + strName;
                break;
        }
    }

    protected void ASPxComboBoxItemFrom_Callback(object sender, CallbackEventArgsBase e)
    {
        string project = e.Parameter;

        string sql = "select a.item_code_from,a.item_code_from||'-'||a.item_name_from SHOWNAME from data_xk_bom_exchange a "
            +" where exists(select * from data_xk_oldbom t where t.item_code=a.item_code_from and t.project_code='" + project + "')";
        dt = dc.GetTable(sql);

        ASPxComboBox list = sender as ASPxComboBox;
        list.DataSource = dt;
        list.DataBind();
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